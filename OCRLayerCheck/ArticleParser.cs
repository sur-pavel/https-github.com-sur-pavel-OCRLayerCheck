using AM;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OCRLayerCheck
{
    internal class ArticleParser
    {
        private Patterns patterns = new Patterns();
        private Log log;
        private PDFHandler pdfHandler;

        public ArticleParser(Log log)
        {
            this.log = log;
        }

        public ArticleParser(Log log, PDFHandler pdfHandler) : this(log)
        {
            this.pdfHandler = pdfHandler;
        }

        internal void ParseFileText(string pdfText, Article article)
        {
            article.Edition = GetEdition(pdfText);
            article.FileName = GetFileName(article);
            article.Title = GetTitle(pdfText);
            article.Journal = GetJournal(pdfText);
        }

        private Journal GetJournal(string pdfText)
        {
            Journal journal = new Journal();
            journal.Title = GetJournalTitle(pdfText);
            journal.Number = GetJournalNumber(pdfText);
            return journal;
        }

        internal void ParseFileText(Article article)
        {
            //article.PdfText = pdfHandler
        }

        private int GetJournalNumber(string pdfText)
        {
            int number = ParseJournalNumber(pdfText);

            return number;
        }

        private int ParseJournalNumber(string pdfText)
        {
            int number = 0;
            Match numberMatch = Regex.Match(pdfText, patterns.NumberPattern);

            if (numberMatch.Success)
            {
                number = int.Parse(numberMatch.Value);
            }
            return number;
        }

        private string GetJournalTitle(string pdfText)
        {
            string jTitle = string.Empty;
            Match titlePatternM = Regex.Match(pdfText, patterns.WordPattern);
            if (titlePatternM.Success)
            {
                jTitle = titlePatternM.Value;
            }
            return jTitle;
        }

        private string GetTitle(string pdfText)
        {
            string title = string.Empty;
            Match titlePatternM = Regex.Match(pdfText, patterns.WordPattern);
            if (titlePatternM.Success)
            {
                title = titlePatternM.Value;
            }
            return title;
        }

        internal string GetFileName(Article article)
        {
            string fileName = $"{article.Autor}_{article.Title}_{article.Edition.Town}_" +
                $"{article.Edition.Year}_{article.Journal.Title},{article.Journal.Number}.pdf";

            Match fileNameM = Regex.Match(fileName, patterns.FileName);
            if (fileNameM.Success)
            {
                log.WriteLine("Right file name");
                article.FileName = fileName;
            }
            else
            {
                fileName = "Wrong file name";
            }
            return fileName;
        }

        private Edition GetEdition(string pdfText)
        {
            Edition edition = new Edition();
            Match townM = Regex.Match(pdfText, patterns.WordPattern);
            Match yearM = Regex.Match(pdfText, patterns.YearPattern);
            if (townM.Success)
            {
                log.WriteLine("Town =" + townM.Value);
                edition.Town = townM.Value;
            }

            if (townM.Success)
            {
                log.WriteLine("Town =" + townM.Value);
                edition.Town = yearM.Value;
            }

            return edition;
        }

        private Autor GetAutor(string pdfText)
        {
            Autor autor = new Autor();
            Match wordM = Regex.Match(pdfText, patterns.WordPattern);
            if (wordM.Success)
            {
                log.WriteLine("Town =" + wordM.Value);
                autor.LastName = wordM.Value;
            }
            return autor;
        }

        private string ConsoleSuggestDialog(string word, List<string> suggestList)
        {
            Console.WriteLine($"Wrong word: {word}");
            Console.WriteLine("Choose suggest (print number): ");
            Console.WriteLine("Else you can 'E'dit or e'X'it)");
            for (int inc = 0; inc < suggestList.Count; inc++)
            {
                Console.WriteLine($"{inc} - {suggestList.GetItem(inc)}");
            }
            int choosedItem = -1;
            bool exit = false;
            while (choosedItem == -1 || exit)
            {
                ConsoleKeyInfo UserInput = Console.ReadKey();
                char key = UserInput.KeyChar;
                if (char.IsDigit(key))
                {
                    choosedItem = int.Parse(key.ToString());
                    word = suggestList.GetItem(choosedItem);
                }
                else if (key.OneOf(new char[] { 'e', 'x' }))
                {
                    switch (key)
                    {
                        case 'E':
                        case 'e':
                            Console.Write("\nEdit name:\n");
                            SendKeys.SendWait(word);
                            word = Console.ReadLine();
                            Console.WriteLine($"New string:\n{word}");
                            Console.ReadKey();
                            exit = true;
                            break;

                        case 'X':
                        case 'x':
                            exit = true;
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    choosedItem = -1;
                }
            }
            return word;
        }
    }
}