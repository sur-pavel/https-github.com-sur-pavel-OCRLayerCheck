namespace OCRLayerCheck
{
    internal class Patterns
    {
        public string NumberPattern = @"\d+";

        public string WordPattern = @"(\s|^)([\D]|((?!__))+)+(\s|$)";

        public string FileName = @"((\w|[А-я])+\s(\w|[А-я])\.)?_.+_(\w|[А-я])+_\[?(\d|-){4}\]?_\d+?(p|с)?=.+\s\d+";

        public string YearPattern = @"\d{4}";

        private string fileNameExample = "Иванов П._Название статьи_М_19--_12с=Название журнала, номер 193";

        public string DirectoryPath = @"[a-zA-Z]:\\((?:.*?\\)*)";
    }
}