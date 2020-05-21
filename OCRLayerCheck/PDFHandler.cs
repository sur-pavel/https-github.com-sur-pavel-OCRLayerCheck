using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace OCRLayerCheck
{
    internal class PDFHandler : System.Web.UI.Page
    {
        internal Log log;
        internal bool notPDF;
        private Patterns patterns;
        private Article article;
        private string inputFile;
        private string outputFile;

        public PDFHandler(Log log, Patterns patterns)
        {
            this.log = log;
            this.patterns = patterns;
        }

        internal int GetMiddlePage(FileInfo file)
        {
            int pages;
            int middlePage = 0;
            PdfReader pdfReader = GetPdfReader(file);
            if (pdfReader != null)
            {
                pages = pdfReader.NumberOfPages;
                pdfReader.Close();
                middlePage = pages / 2;
                log.WriteLine("Middle page: " + middlePage);
            }

            return middlePage;
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
                log.WriteLine("Not a pdf file");
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