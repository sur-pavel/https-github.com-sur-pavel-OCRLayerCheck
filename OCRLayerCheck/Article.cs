using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OCRLayerCheck
{
    internal class Article
    {
        public string Autor { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;

        public string Town { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Pages { get; set; } = string.Empty;

        public Journal Journal = new Journal();

        private PropertyInfo[] _PropertyInfos = null;

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
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}