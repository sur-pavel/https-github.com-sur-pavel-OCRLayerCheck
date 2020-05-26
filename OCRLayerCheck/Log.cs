using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace OCRLayerCheck
{
    public class Log
    {
        private string _logFile;
        private static readonly object sync = new object();

        public void CreateLogFile()
        {
            string fileName = "OCRLayerCheck_" + DateTime.Now.ToString().Replace(":", "-") + ".log";
            string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            Directory.CreateDirectory(appPath + @"\logs\");
            string logFile = appPath + @"\logs\" + fileName;
            _logFile = $"{logFile}";
            FileStream fs = File.Create(logFile);
            fs.Close();
        }

        public void WriteLine(params string[] logs)
        {
            try
            {
                foreach (string log in logs)
                {
                    lock (sync)
                    {
                        File.AppendAllText(_logFile, "\n___" + DateTime.Now.ToString("h:mm:ss tt") + "____\n");
                        File.AppendAllText(_logFile, log + "\n", Encoding.GetEncoding("Windows-1251"));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
        }
    }
}