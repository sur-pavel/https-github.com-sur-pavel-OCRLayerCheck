using IronOcr;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace OCRLayerCheck
{
    internal class PDFHandler : System.Web.UI.Page
    {
        private int pageNumber;
        internal Log log;
        internal bool notPDF;
        private Patterns patterns;
        private Article article;
        private string inputFile;
        private string outputFile;

        public PDFHandler(int pageNumber, Log log, Patterns patterns)
        {
            this.pageNumber = pageNumber;
            this.log = log;
            this.patterns = patterns;
        }

        internal Article ParsePage(FileInfo file, int pageNumber)
        {
            patterns = new Patterns();
            article = new Article();
            PdfReader pdfReader = GetPdfReader(file);
            if (pageNumber != 0 && pageNumber < 10)
            {
                string text = string.Empty;

                string pdfText = PdfTextExtractor.GetTextFromPage(pdfReader, pageNumber, new LocationTextExtractionStrategy()).
                    Replace(".", @".<br>");

                article.PdfText = pdfText;
                Console.WriteLine(pdfText);
            }

            pdfReader.Close();
            return article;
        }

        private Article GetArticle(string fileName, Article article)
        {
            Match articleM = Regex.Match(fileName, patterns.FileName);
            if (articleM.Success)
            {
                log.WriteLine("Right file name");
                article.FileName = fileName;
            }
            else
            {
                log.WriteLine("Wrong file name");
            }
            return article;
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

        private string GetText(FileInfo file, int pageNumber)
        {
            var Ocr = new AdvancedOcr()
            {
                CleanBackgroundNoise = false,
                ColorDepth = 4,
                ColorSpace = AdvancedOcr.OcrColorSpace.Color,
                EnhanceContrast = false,
                DetectWhiteTextOnDarkBackgrounds = false,
                RotateAndStraighten = false,
                EnhanceResolution = false,
                InputImageType = AdvancedOcr.InputTypes.Document,
                ReadBarCodes = true,
                Strategy = AdvancedOcr.OcrStrategy.Fast
            };
            Ocr.Language = new IronOcr.Languages.MultiLanguage(IronOcr.Languages.English.OcrLanguagePack,
                IronOcr.Languages.Russian.OcrLanguagePack,
                IronOcr.Languages.French.OcrLanguagePack,
                IronOcr.Languages.German.OcrLanguagePack,
                IronOcr.Languages.Spanish.OcrLanguagePack,
                IronOcr.Languages.Italian.OcrLanguagePack);

            log.WriteLine("Getting pdf text");
            OcrResult Results;
            try
            {
                Results = Ocr.ReadPdf(file.FullName, pageNumber);
            }
            catch (Exception ex)
            {
                log.WriteLine("EXCEPTION: " + ex);
                Results = null;
            }
            string pdfText = Results.Text;

            return pdfText;
        }

        private bool CorrectFileName(string fileFullName)
        {
            bool validFileName = false;
            if (fileFullName.Contains(".pdf"))
            {
                validFileName = true;
            }
            else
            {
                Console.WriteLine("Wrong FileName");
            }
            return validFileName;
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