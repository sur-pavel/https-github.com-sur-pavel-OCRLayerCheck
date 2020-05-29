using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;
using System.Windows.Forms;

namespace OCRLayerCheck
{
    internal class PDFHandler : System.Web.UI.Page
    {
        internal Log log;
        internal bool notPDF;
        private Patterns patterns;
        private string inputFile;
        private string outputFile;
        private string firstPages;
        private string lastPages;
        private const int COVER_PAGE = 1;

        public PDFHandler(Log log, Patterns patterns)
        {
            this.log = log;
            this.patterns = patterns;
        }

        internal Article GetPdfPageText(FileInfo file, Article article)
        {
            patterns = new Patterns();
            bool isBook = false;
            PdfReader pdfReader = GetPdfReader(file);
            if (pdfReader != null)
            {
                int pageNumber = 1 + COVER_PAGE;
                for (; pageNumber <= 10; pageNumber++)
                {
                    string pdfText = PdfTextExtractor.GetTextFromPage(pdfReader, pageNumber, new LocationTextExtractionStrategy());
                    if (!string.IsNullOrEmpty(pdfText) && (pdfText.Contains("openedition.org") || pdfText.Contains("ISBN")))
                    {
                        article.PdfText.Append(pdfText);
                        if (pdfText.Contains("ISBN"))
                        {
                            isBook = true;
                        }
                        break;
                    }
                }
                for (; pageNumber <= 10; pageNumber++)
                {
                    string pdfText = PdfTextExtractor.GetTextFromPage(pdfReader, pageNumber, new LocationTextExtractionStrategy());
                    if (patterns.MatchStringWithPage(pdfText).Success)
                    {
                        firstPages = patterns.MatchStringWithPage(pdfText).Value;
                        article.Pages = firstPages.Substring(0, firstPages.Length - 2);
                        break;
                    }
                }
                int lastPageNumber = pdfReader.NumberOfPages;
                int pageNumberFor = lastPageNumber - 20;
                for (; lastPageNumber > pageNumberFor; lastPageNumber--)
                {
                    string pdfText = PdfTextExtractor.GetTextFromPage(pdfReader, lastPageNumber, new LocationTextExtractionStrategy());
                    if (patterns.MatchStringWithPage(pdfText).Success)
                    {
                        lastPages = patterns.MatchStringWithPage(pdfText).Value;
                        int pages = int.Parse(patterns.MatchPageNumber(lastPages).Value);
                        if (pages > pageNumberFor)
                        {
                            if (isBook)
                            {
                                article.Pages = pages + " p";
                            }
                            else
                            {
                                article.Pages += "-" + pages + " p";
                            }
                            break;
                        }
                    }
                }
                if (string.IsNullOrEmpty(article.PdfText.ToString()))
                {
                    log.WriteLine($"{file.Name} not contains any text");
                }
                pdfReader.Close();
            }
            return article;
        }

        private PdfReader GetPdfReader(FileInfo file)
        {
            try
            {
                PdfReader pdfReader = new PdfReader(file.FullName);
                return pdfReader;
            }
            catch (iTextSharp.text.exceptions.InvalidPdfException)
            {
                log.WriteLine("Not a pdf file: " + file.Name);
                notPDF = true;
                return null;
            }
        }

        internal void CreatePDF(Article article, FileInfo fileInfo, string outPutPath)
        {
            inputFile = fileInfo.FullName;
            outputFile = fileInfo.DirectoryName + article.FileName;
            Document doc = new Document();
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
            doc.Open();
            doc.Add(new Paragraph("Some Text"));
            writer.CloseStream = false;
            doc.Close();
            memoryStream.Position = 0;

            SavePDF();
        }

        private void SavePDF()
        {
            using (Document document = new Document())
            {
                using (PdfSmartCopy copy = new PdfSmartCopy(document, new FileStream(Server.MapPath(outputFile),
                    FileMode.Create)))
                {
                    document.Open();
                    for (int i = 0; i < 2; ++i)
                    {
                        PdfReader reader = new PdfReader(AddDataSheets(i.ToString()));
                        copy.AddPage(copy.GetImportedPage(reader, 1));
                    }
                }
            }
        }

        public byte[] AddDataSheets(string _data)
        {
            string pdfTemplatePath = Server.MapPath(inputFile);
            PdfReader reader = new PdfReader(pdfTemplatePath);
            using (MemoryStream ms = new MemoryStream())
            {
                using (PdfStamper stamper = new PdfStamper(reader, ms))
                {
                    AcroFields form = stamper.AcroFields;
                    var fieldKeys = form.Fields.Keys;
                    foreach (string fieldKey in fieldKeys)
                    {
                        //Change some data
                        if (fieldKey.Contains("Address"))
                        {
                            form.SetField(fieldKey, _data);
                        }
                    }
                    stamper.FormFlattening = true;
                }
                return ms.ToArray();
            }
        }
    }
}