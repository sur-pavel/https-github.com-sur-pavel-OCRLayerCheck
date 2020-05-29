using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCRLayerCheck
{
    public partial class Form1 : Form
    {
        internal Log log = new Log();
        private FileHandler fileHandler;
        private IrbisHandler irbisHandler = new IrbisHandler();
        private ExcelHandler excelHandler;

        private Patterns patterns = new Patterns();
        private Article currentArticle;
        private ArticleParser articleParser;

        private PDFHandler pdfHandler;
        private List<FileInfo> filesInfoList;
        private HashSet<string> filesToDelete;
        private string nameForFile = string.Empty;
        private string tempFileFullName = string.Empty;
        private int infoListIndex;
        private bool fromShowMethod = true;

        public Form1()
        {
            fileHandler = new FileHandler();
            excelHandler = new ExcelHandler(log);
            articleParser = new ArticleParser(log, patterns);
            pdfHandler = new PDFHandler(log, patterns);
            //FileUtil fileUtil = new FileUtil();
            //fileUtil.KillProcesses("excel");
            filesToDelete = new HashSet<string>();

            fileHandler.log = log;
            excelHandler.CreatExcelObject();
            log.CreateLogFile();

            InitializeComponent();

            infoListIndex = 0;
            FullScreen();
            InputPath.Text = @"c:\Users\sur-p\Downloads\на переименование\";
            OutputPath.Text = @"c:\Users\sur-p\Downloads\переим\";
        }

        private void FullScreen()
        {
            WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void ChooseInputPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                InputPath.Text = FBD.SelectedPath + @"\";
            }
        }

        private void ChooseOutputPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                OutputPath.Text = FBD.SelectedPath + @"\";
            }
        }

        private void InputPath_TextChanged(object sender, EventArgs e)
        {
            GetFiles();
            ShowPDF();
        }

        private void OutputPath_TextChanged(object sender, EventArgs e)
        {
        }

        private void GetFiles()
        {
            if (patterns.MatchDirectoryPath(InputPath.Text).Success)
            {
                filesInfoList = fileHandler.GetFileNames(InputPath.Text);
            }
            else
            {
                InfoLabel.Text = "Выберите папку с pdf-файлами и папку назначения";
            }
        }

        private void ShowPDF()
        {
            fromShowMethod = true;
            var stackTraceFrame = new StackTrace().GetFrame(0);
            log.WriteLine($"{stackTraceFrame.GetMethod()} Old fileName: {filesInfoList[infoListIndex].Name}:");
            currentArticle = new Article();
            currentArticle = pdfHandler.GetPdfPageText(filesInfoList[infoListIndex], currentArticle);
            currentArticle = articleParser.ParsePdfText(currentArticle);
            ClearControls();
            FillInputs(currentArticle);
            ManageNewFileName();

            tempFileFullName = fileHandler.CreateTempFile(filesInfoList[infoListIndex]);
            webBrowser1.Navigate(tempFileFullName);

            oldFileName.Text = filesInfoList[infoListIndex].Name;
            InfoLabel.Text = string.Empty;
            fromShowMethod = false;
        }

        private void FillInputs(Article article)
        {
            AutorInput.Text = article.Autor;
            TitleInput.Text = article.Title;
            TownInput.Text = article.Town;
            YearInput.Text = article.Year;
            PagesInput.Text = article.Pages;

            JTitleInput.Text = article.Journal.Title;
            JNumberInput.Text = article.Journal.Number;
            JVolumeInput.Text = article.Journal.Volume;
        }

        private void CreateNameForFile()
        {
            currentArticle.Autor = string.IsNullOrEmpty(AutorInput.Text) ? currentArticle.Autor : AutorInput.Text;
            currentArticle.Title = string.IsNullOrEmpty(TitleInput.Text) ? currentArticle.Title : TitleInput.Text;
            currentArticle.Town = string.IsNullOrEmpty(TownInput.Text) ? currentArticle.Town : TownInput.Text;
            currentArticle.Year = string.IsNullOrEmpty(YearInput.Text) ? currentArticle.Year : YearInput.Text;
            currentArticle.Pages = string.IsNullOrEmpty(PagesInput.Text) ? currentArticle.Pages : PagesInput.Text;

            currentArticle.Journal.Title = string.IsNullOrEmpty(JTitleInput.Text) ? currentArticle.Journal.Title : JTitleInput.Text;
            currentArticle.Journal.Number = string.IsNullOrEmpty(JNumberInput.Text) ? currentArticle.Journal.Number : JNumberInput.Text;
            currentArticle.Journal.Volume = string.IsNullOrEmpty(JVolumeInput.Text) ? currentArticle.Journal.Volume : JVolumeInput.Text;

            ManageNewFileName();
        }

        private void ManageNewFileName()
        {
            nameForFile = articleParser.GetFileName(currentArticle);
            nameForFile = articleParser.CheckFileName(nameForFile);
            if (!nameForFile.Contains(".pdf"))
            {
                InfoLabel.Text = nameForFile;
            }
            else
            {
                NewFileNameInput.Text = nameForFile;
            }
        }

        private void NextFileButton_Click_1(object sender, EventArgs e)
        {
            currentArticle.FileName = NewFileNameInput.Text;

            if (currentArticle.FileName.Contains(".pdf"))
            {
                var stackTraceFrame = new StackTrace().GetFrame(0);
                log.WriteLine(stackTraceFrame.GetMethod() + " New fileName:" + nameForFile);
                log.WriteLine(stackTraceFrame.GetMethod() + currentArticle.ToString());
                bool moved = fileHandler.Move(filesInfoList[infoListIndex], OutputPath.Text + currentArticle.FileName);
                if (moved)
                {
                    infoListIndex++;
                    if (infoListIndex < filesInfoList.Count)
                    {
                        if (!string.IsNullOrEmpty(tempFileFullName))
                        {
                            filesToDelete.Add(tempFileFullName);
                        }
                        Article articleForExcel = currentArticle;
                        Task.Factory.StartNew(() =>
                        {
                            excelHandler.AddRow(articleForExcel);
                            excelHandler.SaveFile();
                        });
                        ShowPDF();
                    }
                    else
                    {
                        MessageBox.Show("Все файлы обработаны");
                    }
                }
            }
            else
            {
                InfoLabel.Text = nameForFile;
            }
        }

        private void ClearControls()
        {
            AutorInput.Text = string.Empty;
            TitleInput.Text = string.Empty;
            TownInput.Text = string.Empty;
            YearInput.Text = string.Empty;
            PagesInput.Text = string.Empty;

            JTitleInput.Text = string.Empty;
            JNumberInput.Text = string.Empty;
            JVolumeInput.Text = string.Empty;
            NewFileNameInput.Text = string.Empty;
            InfoLabel.Text = string.Empty;
        }

        private void AutorInput_TextChanged(object sender, EventArgs e)
        {
            if (!fromShowMethod)
            {
                CreateNameForFile();
            }
        }

        private void TitleInput_TextChanged(object sender, EventArgs e)
        {
            if (!fromShowMethod)
            {
                CreateNameForFile();
            }
        }

        private void TownInput_TextChanged(object sender, EventArgs e)
        {
            if (!fromShowMethod)
            {
                CreateNameForFile();
            }
        }

        private void YearInput_TextChanged(object sender, EventArgs e)
        {
            if (!fromShowMethod)
            {
                CreateNameForFile();
            }
        }

        private void PagesInput_TextChanged(object sender, EventArgs e)
        {
            if (!fromShowMethod)
            {
                CreateNameForFile();
            }
        }

        private void JTitleInput_TextChanged(object sender, EventArgs e)
        {
            if (!fromShowMethod)
            {
                CreateNameForFile();
            }
        }

        private void JNumberInput_TextChanged(object sender, EventArgs e)
        {
            if (!fromShowMethod)
            {
                CreateNameForFile();
            }
        }

        private void JVolumeInput_TextChanged(object sender, EventArgs e)
        {
            if (!fromShowMethod)
            {
                CreateNameForFile();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (!webBrowser1.IsDisposed)
            {
                webBrowser1.Dispose();
            }
            Task exitTask = Task.Factory.StartNew(() =>
            {
                if (filesToDelete != null)
                {
                    foreach (string tempFileFullName in filesToDelete)
                    {
                        fileHandler.Delete(tempFileFullName);
                    }
                }
                excelHandler.SaveFile();
                excelHandler.Quit();
            });
            exitTask.Wait();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }
    }
}