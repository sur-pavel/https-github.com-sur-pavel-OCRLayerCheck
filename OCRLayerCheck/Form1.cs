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
        private Article article;
        private ArticleParser articleParser;

        private PDFHandler pdfHandler;
        private List<FileInfo> filesInfoList;
        private HashSet<string> filesToDelete;
        private string nameForFile = string.Empty;
        private string tempFileFullName = string.Empty;
        private int infoListIndex;

        public Form1()
        {
            fileHandler = new FileHandler();
            excelHandler = new ExcelHandler(log);
            articleParser = new ArticleParser(log, patterns);
            pdfHandler = new PDFHandler(log, patterns);
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

        private void Button1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void PathWithOCR_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
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
            var stackTraceFrame = new StackTrace().GetFrame(0);
            log.WriteLine($"{stackTraceFrame.GetMethod()} Old fileName: {filesInfoList[infoListIndex].Name}:");
            article = pdfHandler.GetPdfPageText(filesInfoList[infoListIndex], new Article());
            article = articleParser.ParsePdfText(article);
            log.WriteLine(stackTraceFrame.GetMethod() + article.ToString());
            ClearControls();
            FillInputs(article);
            CreateNameForFile();
            ManageNewFileName();

            tempFileFullName = fileHandler.CreateTempFile(filesInfoList[infoListIndex]);
            webBrowser1.Navigate(tempFileFullName);

            oldFileName.Text = filesInfoList[infoListIndex].Name;
            InfoLabel.Text = string.Empty;
        }

        private void FillInputs(Article article)
        {
            AutorInput.Text = article.Autor;
            TitleInput.Text = article.Title;
            TownInput.Text = article.Town;
            YearInput.Text = article.Year;

            JTitleInput.Text = article.Journal.Title;
            JNumberInput.Text = article.Journal.Number;
            JVolumeInput.Text = article.Journal.Volume;
        }

        private void CreateNameForFile()
        {
            if (!nameForFile.Equals(NewFileNameInput.Text))
            {
                article.Title = string.IsNullOrEmpty(TitleInput.Text) ? article.Title : TitleInput.Text;
                article.Town = string.IsNullOrEmpty(TownInput.Text) ? article.Town : TownInput.Text;
                article.Year = string.IsNullOrEmpty(YearInput.Text) ? article.Year : YearInput.Text;
                article.Pages = string.IsNullOrEmpty(PagesInput.Text) ? article.Pages : PagesInput.Text;

                article.Journal.Title = string.IsNullOrEmpty(JTitleInput.Text) ? article.Journal.Title : JTitleInput.Text;
                article.Journal.Number = string.IsNullOrEmpty(JNumberInput.Text) ? article.Journal.Number : JNumberInput.Text;
                article.Journal.Volume = string.IsNullOrEmpty(JVolumeInput.Text) ? article.Journal.Volume : JVolumeInput.Text;

                ManageNewFileName();
            }
        }

        private void ManageNewFileName()
        {
            nameForFile = articleParser.GetFileName(article);
            nameForFile = articleParser.CheckFileName(nameForFile);
            if (!nameForFile.Contains(".pdf"))
            {
                InfoLabel.Text = nameForFile;
            }
            else
            {
                NewFileNameInput.Text = nameForFile;
                var stackTraceFrame = new StackTrace().GetFrame(0);
                log.WriteLine(stackTraceFrame.GetMethod() + " New fileName:" + nameForFile);
                log.WriteLine(stackTraceFrame.GetMethod() + article.ToString());
            }
        }

        private void NextFileButton_Click_1(object sender, EventArgs e)
        {
            article.FileName = NewFileNameInput.Text;

            if (article.FileName.Contains(".pdf"))
            {
                bool moved = fileHandler.Move(filesInfoList[infoListIndex], OutputPath.Text + article.FileName);
                if (moved)
                {
                    infoListIndex++;
                    if (infoListIndex < filesInfoList.Count)
                    {
                        ShowPDF();
                        if (!string.IsNullOrEmpty(tempFileFullName))
                        {
                            filesToDelete.Add(tempFileFullName);
                        }
                        excelHandler.AddRow(article);
                        excelHandler.SaveFile();
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

        private void pdfViewer1_Load(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CreateNameForFile();
        }

        private void AutorInput_TextChanged(object sender, EventArgs e)
        {
            CreateNameForFile();
        }

        private void TownInput_TextChanged(object sender, EventArgs e)
        {
            CreateNameForFile();
        }

        private void YearInput_TextChanged(object sender, EventArgs e)
        {
            CreateNameForFile();
        }

        private void JTitleInput_TextChanged(object sender, EventArgs e)
        {
            CreateNameForFile();
        }

        private void JVolumeInput_TextChanged(object sender, EventArgs e)
        {
            CreateNameForFile();
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
    }
}