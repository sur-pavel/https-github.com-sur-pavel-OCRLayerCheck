using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OCRLayerCheck
{
    public class Article
    {
        public string Autor { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;

        public string Town { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Pages { get; set; } = string.Empty;

        public DocType DocumentType { get; set; }
        public Journal Journal = new Journal();
        public string FileName { get; set; } = string.Empty;
        public StringBuilder PdfText { get; set; } = new StringBuilder();

        public StringBuilder OddPdfText { get; set; } = new StringBuilder();

        private PropertyInfo[] _PropertyInfos = null;

        public enum DocType
        {
            Book = 1,
            Article = 2,
            Journal = 3,
            WrongBook = 4
        }

        public override string ToString()
        {
            if (_PropertyInfos == null)
                _PropertyInfos = GetType().GetProperties();

            var builder = new StringBuilder();

            foreach (var info in _PropertyInfos)
            {
                var value = info.GetValue(this, null) ?? "(null)";
                builder.AppendLine(info.Name + ": " + value.ToString());
            }
            builder.AppendLine("Journal Data: \n" + Journal.ToString());
            return builder.ToString();
        }

        public IEnumerator<PropertyInfo> GetEnumerator()
        {
            foreach (var property in typeof(Article).GetProperties())
            {
                yield return property;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Article article &&
                   Autor == article.Autor &&
                   Title == article.Title &&
                   Town == article.Town &&
                   Year == article.Year &&
                   Pages == article.Pages &&
                   EqualityComparer<Journal>.Default.Equals(Journal, article.Journal) &&
                   FileName == article.FileName &&
                   EqualityComparer<StringBuilder>.Default.Equals(PdfText, article.PdfText);
        }

        public override int GetHashCode()
        {
            throw new System.NotImplementedException();
        }
    }
}