using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace OCRLayerCheck
{
    public class ArticleParser
    {
        private Patterns patterns = new Patterns();
        private Log log;

        public ArticleParser(Log log, Patterns patterns)
        {
            this.patterns = patterns;
            this.log = log;
        }

        internal string GetFileName(Article article)
        {
            if (Regex.IsMatch(article.PdfText.ToString(), patterns.BookEditionPattern))
            {
                return $"{article.Autor}_{article.Title}_{article.Town}_" +
                    $"{article.Year}_{article.Pages}" +
                    $"{article.Journal.Volume}.pdf";
            }
            else
            {
                return $"{article.Autor}_{article.Title}_{article.Town}_" +
                    $"{article.Year}_{article.Pages}={article.Journal.Title}_{article.Journal.Number}_" +
                    $"{article.Journal.Volume}_.pdf";
            }
        }

        internal string CheckFileName(string fileName)
        {
            Match wrongSimbols = Regex.Match(fileName, patterns.EscapedSymbols);
            if (wrongSimbols.Success)
            {
                fileName = @"Имя файл содержит один символов  \ / ? : *  > < | ";
            }

            return fileName;
        }

        public Article ParsePdfText(Article article)
        {
            string pdfText = article.PdfText.ToString();
            string[] referenceStrings = { "Electronic reference" ,
           "Electronic reference",
           "Referencia electrónica",
           "Source gallica",
           "Notizia bibliografica digitale",
           "Reference electronique",
           "Référence électronique",
           "Référence électronique"
            };
            foreach (string refStr in referenceStrings)
            {
                if (pdfText.Contains(refStr))
                {
                    article = FillArticle(article, pdfText, refStr);
                }
            }

            return article;
        }

        private Article FillArticle(Article article, string pdfText, string referenceString)
        {
            string data = pdfText;

            if (!string.IsNullOrEmpty(referenceString))
            {
                data = pdfText.Split(new string[] { referenceString }, StringSplitOptions.None)[1];
            }
            data = data.Replace("\n", " ").Replace("  ", " ").Trim();
            //var stackTraceFrame = new StackTrace().GetFrame(0);
            //log.WriteLine($"\n{stackTraceFrame.GetMethod()}\n------------------------" + data + "------------------------\n\n");

            if (patterns.MatchBookEdition(data).Success)
            {
                article.Autor = patterns.MatchBookAutor(data).Value.Trim();

                if (!string.IsNullOrEmpty(article.Autor))
                {
                    data = data.Replace(article.Autor, "");
                }
                article.Autor = CleanUpString(article.Autor);
                article.Town = Regex.Match(patterns.MatchBookEdition(data).Value, patterns.TownPattern).Value.
                    Replace(":", "").Trim();
                article.Year = Regex.Match(patterns.MatchBookEdition(data).Value, patterns.YearPattern).Value.Trim();
            }
            else
            {
                article = ParseJournalArticle(article, data);
            }

            return article;
        }

        private Article ParseJournalArticle(Article article, string data)
        {
            article.Autor = data.Split(',')[0];
            data = data.Replace(article.Autor, "");
            if (patterns.MatchArticleTitle(data).Success)
            {
                article.Title = patterns.MatchArticleTitle(data).Value;
                data = data.Replace(article.Title, "");
                article.Title = article.Title.Substring(1, article.Title.Length - 2).Trim();
            }
            if (patterns.MatchJVolumeYear(data).Success)
            {
                string jVolumeNumber = patterns.MatchJVolumeYear(data).Value;
                article.Year = patterns.MatchYear(jVolumeNumber).Value.Trim();
                article.Journal.Number = article.Year;
                article.Journal.Volume = jVolumeNumber.Split('|')[0].Trim();
            }
            if (data.Contains("Bulletin de correspondance hellenique moderne et contemporain"))
            {
                article.Journal.Title = "BCHMC";
            }
            return article;
        }

        private string CleanUpString(string str)
        {
            return Regex.Replace(str, patterns.cleanUpPattern, "").Trim();
        }
    }
}