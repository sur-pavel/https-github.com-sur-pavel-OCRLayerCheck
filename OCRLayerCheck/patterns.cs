using System.Text.RegularExpressions;

namespace OCRLayerCheck
{
    internal class Patterns
    {
        public string NumberPattern = @"\d+";

        public string AutorPattern = @"-([А-я]|\w|((?!__))+|\s)*-" +
            @"([А-я]|\w|((?!__))+|\s)+\,\s" +
            @"(([А-я]|\w|((?!__))+|-|\(|\))+\s?){1,3}";

        public string WordPattern = @"(\s|^)([\D]|((?!__))+)+(\s|$)";

        public string FileName = @"((\w|[А-я])+\s(\w|[А-я])\.)?_.+_" +
            @"\[?(\w|[А-я])*\]?_\[?(\d|-){4}\]?_\d+?(p|с)?=.+\d+_.*";

        public string TownPattern = @"\.\s([А-я]|\w|((?!__))+)+\s\:";
        public string YearPattern = @"\d{4}";

        public string BookEditionPattern = @"\.\s([А-я]|\w|((?!__))+)+\s\:\s([\D]|((?!__))+)+\,\s\d{4}";

        public string DirectoryPath = @"[a-zA-Z]:\\((?:.*?\\)*)";

        public string EscapedSymbols = @"(\||\\|\;|\:|\/|\?|\*|\>|\<)";

        private string fileNameExample = "Иванов П._Название статьи_М_19--_12с=Название журнала_номер 193_Т.1";

        internal Match MatchBookEdition(string str)
        {
            return Regex.Match(str, @"\.\s([А-я]|\w|((?!__))+)+\s\:\s([\D]|((?!__))+)+\,\s\d{4}");
        }

        internal Match MatchBookAutor(string str)
        {
            return Regex.Match(str, @"^([А-я]|\w|((?!__))+|\s)*-([А-я]|\w|((?!__))+|\s)+\,\s(([А-я]|\w|((?!__))+|-|\(|\))+\s?){1,3}");
        }

        internal Match MatchBookTown(string str)
        {
            return Regex.Match(str, @"\.\s([А-я]|\w|((?!__))+)+\s\:");
        }

        internal Match MatchYear(string str)
        {
            return Regex.Match(str, @"\d+");
        }

        internal Match MatchDirectoryPath(string str)
        {
            return Regex.Match(str, @"[a-zA-Z]:\\((?:.*?\\)*)");
        }

        internal Match MatchEscapedSymbols(string str)
        {
            return Regex.Match(str, @"(\||\\|\;|\:|\/|\?|\*|\>|\<)");
        }

        internal Match MatchJVolumeYear(string str)
        {
            return Regex.Match(str, @"\s\d\s\|\s\d{4}");
        }
    }
}