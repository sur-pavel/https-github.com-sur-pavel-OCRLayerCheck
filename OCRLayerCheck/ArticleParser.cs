using System;
using System.IO;
using System.Text.RegularExpressions;

namespace OCRLayerCheck
{
    internal class ArticleParser
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
                    $"{article.Year}_{article.Pages}={article.Journal.Title}_{article.Journal.Number}" +
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

        internal Article ParsePdfText(Article article)
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
                    FillArticle(article, pdfText, refStr);
                }
            }

            return article;
        }

        private void FillArticle(Article article, string pdfText, string referenceString)
        {
            string data = pdfText.Split(new string[] { referenceString }, StringSplitOptions.None)[1];
            data = data.Replace("\n", " ").Replace("  ", " ").Trim();
            log.WriteLine("\n\n------------------------" + data + "------------------------\n\n");

            if (patterns.MatchBookEdition(data).Success)
            {
                article.Autor = patterns.MatchBookAutor(data).Value.Trim();

                if (!string.IsNullOrEmpty(article.Autor))
                {
                    data = data.Replace(article.Autor, "");
                }

                article.Town = Regex.Match(patterns.MatchBookEdition(data).Value, patterns.TownPattern).Value.
                    Replace(":", "").Trim();
                article.Year = Regex.Match(patterns.MatchBookEdition(data).Value, patterns.YearPattern).Value.Trim();
            }
            else
            {
                article.Autor = data.Split(',')[0];
                data = data.Replace(article.Autor, "");
                string jVolumeNumber = patterns.MatchJVolumeYear(data).Value;
                article.Journal.Number = patterns.MatchYear(jVolumeNumber).Value;
                article.Year = Regex.Match(data, patterns.YearPattern).Value.Trim();
            }

            log.WriteLine(article.ToString());
        }
    }
}