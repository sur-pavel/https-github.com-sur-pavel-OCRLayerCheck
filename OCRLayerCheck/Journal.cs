namespace OCRLayerCheck
{
    using System.Reflection;
    using System.Text;

    public class Journal
    {
        public string Title { get; set; }
        public int Number { get; set; }

        public Journal()
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