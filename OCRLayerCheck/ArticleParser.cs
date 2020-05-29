using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
            if (article.DocumentType == Article.DocType.Book)
            {
                return $"{article.Autor}_{article.Title}_{article.Town}_" +
                    $"{article.Year}_{article.Pages}" +
                    $"{article.Journal.Volume}.pdf";
            }
            else
            {
                return $"{article.Autor}_{article.Title}_{article.Town}_" +
                    $"{article.Year}_{article.Pages}={article.Journal.Title}_{article.Journal.Number}_" +
                    $"{article.Journal.Volume}.pdf";
            }
        }

        internal string CheckFileName(string fileName)
        {
            if (patterns.MatchEscapedSymbols(fileName).Success)
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

            if (article.DocumentType == Article.DocType.Book || article.DocumentType == Article.DocType.WrongBook)
            {
                article = ParseBook(article, data);
            }
            else
            {
                article = ParseJournalArticle(article, data);
            }

            return article;
        }

        private Article ParseBook(Article article, string data)
        {
            article.Journal.Number = patterns.MatchYear(article.OddPdfText.ToString()).Value;
            article.Journal.Volume = patterns.MatchJournalVolume(article.OddPdfText.ToString()).Value.Replace("-", "");
            article.Autor = patterns.MatchBookAutor(data).Value.Trim();

            if (!string.IsNullOrEmpty(article.Autor))
            {
                data = data.Replace(article.Autor, "");
                article.Autor = FirstCharToUpper(article.Autor);
                article.Autor = CleanUpString(article.Autor);
            }
            article.Title = patterns.MatchTitle(data).Value;
            if (!string.IsNullOrEmpty(article.Title))
            {
                data = data.Replace(article.Title, "");
                article.Title = OptimizeTitle(article.Title);
            }
            string bookEdition = patterns.MatchBookEdition(data).Value;
            article.Town = patterns.MatchBookTown(bookEdition).Value.Replace(":", "").Trim();
            article.Year = patterns.MatchYear(bookEdition).Value.Trim();

            return article;
        }

        private string OptimizeTitle(string str)
        {
            str = Regex.Replace(str, @"\.$", "");
            str = Regex.Replace(str, @"\s?\!$", "!");
            str = Regex.Replace(str, @"\?$", "");
            str = Regex.Replace(str, @"\:$", "");
            str = Regex.Replace(str, @"\s?\:", ".");
            return str.Trim();
        }

        private Article ParseJournalArticle(Article article, string data)
        {
            article.Autor = data.Split(',')[0];
            article.Autor = CleanUpString(article.Autor);

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
                //article.Journal.Title = GetJournalTitle(data, jVolumeNumber);
                article.Journal.Title = "BCHmc";
            }

            return article;
        }

        private string GetJournalTitle(string data, string jVolumeNumber)
        {
            string jTitle = data.Split(new string[] { jVolumeNumber }, StringSplitOptions.None)[0];
            jTitle = CleanUpString(jTitle);
            jTitle = Regex.Replace(jTitle, @"\[.+\]", "");
            return GetAbbreviation(jTitle.Trim());
        }

        private string GetAbbreviation(string jTitle)
        {
            return new string(jTitle.Split()
          .Where(s => s.Length > 0 && char.IsLetter(s[0]) && char.IsUpper(s[0]) &&
          !s.Equals("de") && !s.Equals("et"))
          .Take(jTitle.Split().Length)
          .Select(s => s[0])
          .ToArray());
        }

        private string CleanUpString(string str)
        {
            str = Regex.Replace(str, patterns.cleanUpPattern, "");
            str = Regex.Replace(str, @"\s\;\s", ", ");
            str = Regex.Replace(str, @"\;$", "");
            return str.Trim();
        }

        public string FirstCharToUpper(string input)
        {
            input = input.ToLower();
            if (input.Split(' ').Length > 1)
            {
                List<string> list = new List<string>();
                List<string> substrs = new List<string>();
                foreach (string str in input.Split(' '))
                {
                    if (str.Contains("-"))
                    {
                        foreach (string substr in str.Split('-'))
                        {
                            substrs.Add(substr[0].ToString().ToUpper() + substr.Substring(1));
                        }
                        list.Add(string.Join("-", substrs));
                    }
                    else
                    {
                        list.Add(str[0].ToString().ToUpper() + str.Substring(1));
                    }
                }
                return string.Join(" ", list);
            }
            else
            {
                return input[0].ToString().ToUpper() + input.Substring(1);
            }
        }
    }
}