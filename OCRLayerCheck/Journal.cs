namespace OCRLayerCheck
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    public class Journal
    {
        public string Title { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Volume { get; set; } = string.Empty;

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

        public override bool Equals(object obj)
        {
            return obj is Journal journal &&
                   Title == journal.Title &&
                   Number == journal.Number &&
                   Volume == journal.Volume;
        }

        public override int GetHashCode()
        {
            int hashCode = -6655960;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Number);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Volume);
            return hashCode;
        }
    }
}