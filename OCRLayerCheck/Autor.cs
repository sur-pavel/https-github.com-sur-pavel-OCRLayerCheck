using System.Reflection;
using System.Text;

namespace OCRLayerCheck
{
    internal class Autor
    {
        public string LastName { get; set; }
        public string Initial { get; set; }

        public Autor()
        {
        }

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