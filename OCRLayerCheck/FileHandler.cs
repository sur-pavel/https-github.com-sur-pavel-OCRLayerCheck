using System;
using System.IO;
using System.Windows.Forms;

namespace OCRLayerCheck
{
    internal class FileHandler
    {
        internal Log log;

        internal FileInfo[] GetFileNames(string path)
        {
            FileInfo[] files = null;
            try
            {
                DirectoryInfo info = new DirectoryInfo(path);
                files = info.GetFiles("*.pdf");
            }
            catch (Exception ex)
            {
                log.WriteLine(ex.Message, ex.StackTrace);
            }
            return files;
        }

        internal void Move(FileInfo file, string path)
        {
            try
            {
                File.Move(file.FullName, path + file.Name);
            }
            catch (Exception ex)
            {
                log.WriteLine("EXCEPTION: " + ex);
            }
        }

        internal void Save(FileInfo currentFileInfo, string nameForFile, string outputPath)
        {
            string outputFile = currentFileInfo.DirectoryName + nameForFile;

            try
            {
                File.Copy(outputFile, outputPath);
            }
            catch (Exception ex)
            {
                log.WriteLine(ex.Message, ex.StackTrace);
                MessageBox.Show(ex.Message, ex.StackTrace);
            }
        }
    }
}