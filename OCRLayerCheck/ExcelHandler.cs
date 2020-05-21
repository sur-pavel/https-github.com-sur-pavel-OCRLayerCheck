using System.Reflection;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace OCRLayerCheck
{
    internal class ExcelHandler
    {
        private Excel.Application xlApp;
        private Excel.Workbook xlWorkBook;
        private Excel.Worksheet xlWorkSheet;
        private object misValue = Missing.Value;
        private int row = 2;
        private Log log;

        public ExcelHandler(Log log)
        {
            this.log = log;
        }

        internal void CreatExcelObject()
        {
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlApp.DisplayAlerts = false;
            xlWorkBook = xlApp.Workbooks.Add();
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Cells[1, 1] = "Автор";
            xlWorkSheet.Cells[1, 2] = "Заглавие";
            xlWorkSheet.Cells[1, 3] = "Место";
            xlWorkSheet.Cells[1, 4] = "Год";
            xlWorkSheet.Cells[1, 5] = "Год";
            xlWorkSheet.Cells[1, 6] = "Кол-во страниц";
            xlWorkSheet.Cells[1, 7] = "Номер журнала";
            xlWorkSheet.Cells[1, 8] = "Том журнала";
            xlWorkSheet.Cells[1, 9] = "Имя файла";

            xlWorkSheet.get_Range("A1", "G1").Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
        }

        internal void AddRow(Article article)
        {
            xlWorkSheet.Cells[row, 1] = article.Autor;
            xlWorkSheet.Cells[row, 2] = article.Title;
            xlWorkSheet.Cells[row, 3] = article.Town;
            xlWorkSheet.Cells[row, 4] = article.Year;
            xlWorkSheet.Cells[row, 5] = article.Pages;

            xlWorkSheet.Cells[row, 6] = article.Journal.Title;
            xlWorkSheet.Cells[row, 7] = article.Journal.Number;
            xlWorkSheet.Cells[row, 8] = article.Journal.Volume;
            xlWorkSheet.Cells[row, 9] = article.FileName;

            row++;
        }

        internal void SaveFile()
        {
            xlWorkBook.SaveAs("List_Of_Records", AccessMode: Excel.XlSaveAsAccessMode.xlExclusive,
               ConflictResolution: Excel.XlSaveConflictResolution.xlLocalSessionChanges);
        }

        internal void Quit()
        {
            xlWorkBook.Close(SaveChanges: true);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
            log.WriteLine("Excel file created");
            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
        }
    }
}