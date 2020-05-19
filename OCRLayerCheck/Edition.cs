using System.Reflection;
using System.Text;

namespace OCRLayerCheck
{
    public class Edition
    {
        public string Town { get; set; }
        public int Year { get; set; }

        public Edition()
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