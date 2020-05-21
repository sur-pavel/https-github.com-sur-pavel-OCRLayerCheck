using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OCRLayerCheck
{
    internal class FileHandler
    {
        internal Log log;

        internal IEnumerator<FileInfo> GetFileNames(string path)
        {
            FileInfo[] files = null;
            IEnumerator<FileInfo> outfiles = null;
            try
            {
                files = new DirectoryInfo(path).GetFiles("*.pdf");
                outfiles = files.Where(x => x != null && x.Name.Contains(".pdf") &&
                x.Name != null && x.FullName != null && x.Exists).GetEnumerator();
            }
            catch (Exception ex)
            {
                log.WriteLine(ex.Message, ex.StackTrace);
            }
            return outfiles;
        }

        internal void Save(FileInfo currentFileInfo, string nameForFile, string outputPath)
        {
            string outputFile = outputPath + nameForFile;

            try
            {
                File.Copy(currentFileInfo.FullName, outputFile);
            }
            catch (Exception ex)
            {
                log.WriteLine(ex.Message, ex.StackTrace);
                MessageBox.Show(ex.Message, ex.StackTrace);
            }
        }
    }
}