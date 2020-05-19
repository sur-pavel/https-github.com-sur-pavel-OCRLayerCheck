using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace OCRLayerCheck
{
    internal class Log
    {
        private string _logFile;
        private static readonly object sync = new object();

        internal void CreateLogFile()
        {
            string fileName = "OCRLayerCheck_" + DateTime.Now.ToString().Replace(":", "-") + ".log";
            string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            Directory.CreateDirectory(appPath + @"\logs\");
            string logFile = appPath + @"\logs\" + fileName;
            _logFile = $"{logFile}";
            FileStream fs = File.Create(logFile);
            fs.Close();
        }

        public void WriteLine(params string[] log)
        {
            try
            {
                lock (sync)
                {
                    File.AppendAllText(_logFile, log + "\n", Encoding.GetEncoding("Windows-1251"));
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