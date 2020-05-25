using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OCRLayerCheck
{
    internal class Book
    {
        public string Autor { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;

        public string Town { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Pages { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public StringBuilder PdfText { get; set; } = new StringBuilder();

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
    }
}