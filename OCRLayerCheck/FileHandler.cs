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

        internal List<FileInfo> GetFileNames(string path)
        {
            FileInfo[] files = null;
            List<FileInfo> outfiles = null;
            try
            {
                files = new DirectoryInfo(path).GetFiles("*.pdf");
                outfiles = files.Where(x => x?.Name.Contains(".pdf") == true &&
                x.Name != null && x.FullName != null && x.Exists).ToList();
            }
            catch (Exception ex)
            {
                log.WriteLine(ex.Message, ex.StackTrace);
            }
            return outfiles;
        }

        internal bool Move(FileInfo currentFileInfo, string outputFile)
        {
            bool moved = false;
            try
            {
                File.Move(currentFileInfo.FullName, outputFile);
                moved = true;
            }
            catch (Exception ex)
            {
                log.WriteLine(ex.Message + "\n" + ex.StackTrace);
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Ошибка");
            }
            return moved;
        }

        internal bool Delete(FileInfo currentFileInfo)
        {
            bool deleted = true;

            try
            {
                File.Delete(currentFileInfo.FullName);
            }
            catch (NullReferenceException nullex)
            {
                log.WriteLine(nullex.Message, nullex.StackTrace);
                MessageBox.Show(nullex.Message, nullex.StackTrace);
                deleted = false;
            }
            catch (Exception ex)
            {
                log.WriteLine(ex.Message, ex.StackTrace);
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                deleted = false;
            }
            return deleted;
        }
    }
}