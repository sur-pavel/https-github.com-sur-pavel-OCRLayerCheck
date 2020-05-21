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
            string fileName = $"{article.Autor}_{article.Title}_{article.Town}_" +
                $"{article.Year}_{article.Pages}={article.Journal.Title}_{article.Journal.Number}" +
                $"{article.Journal.Volume}_.pdf";

            return fileName;
        }

        internal string CheckFileName(string fileName)
        {
            Match fileNameM = Regex.Match(fileName, patterns.FileName);
            Match wrongSimbols = Regex.Match(fileName, patterns.EscapedSymbols);
            if (wrongSimbols.Success)
            {
                fileName = @"Имя файл содержит один символов  \ / ? : *  > < | ";
            }

            return fileName;
        }
    }
}