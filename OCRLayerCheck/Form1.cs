using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
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
        private FileUtil fileUtil = new FileUtil();
        private ArticleParser articleParser;

        private PDFHandler pdfHandler;
        private List<FileInfo> filesInfoList;
        private List<FileInfo> filesToDelete;
        private string nameForFile = string.Empty;
        private string pdfText = string.Empty;
        private int infoListIndex;
        private bool firstBrowserDown;
        private CancellationToken cancellationToken = new CancellationTokenSource().Token;

        public Form1()
        {
            fileHandler = new FileHandler();
            fileHandler.log = log;
            fileUtil.KillProcesses("excel");
            excelHandler = new ExcelHandler(log);

            excelHandler.CreatExcelObject();
            articleParser = new ArticleParser(log, patterns);
            pdfHandler = new PDFHandler(log, patterns);

            log.CreateLogFile();

            InitializeComponent();

            webBrowser2.Hide();

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
            ShowPDF();
        }

        private void OutputPath_TextChanged(object sender, EventArgs e)
        {
        }

        private void ShowPDF()
        {
            if (patterns.MatchDirectoryPath(InputPath.Text).Success)
            {
                filesInfoList = fileHandler.GetFileNames(InputPath.Text);

                var stackTraceFrame = new StackTrace().GetFrame(0);
                log.WriteLine($"{stackTraceFrame.GetMethod()} Old fileName: {filesInfoList[infoListIndex].Name}:");
                article = pdfHandler.GetPdfPageText(filesInfoList[infoListIndex], new Article());
                article = articleParser.ParsePdfText(article);
                log.WriteLine(article.ToString());
                FillInputs(article);

                if (firstBrowserDown)
                {
                    HideNavigate(webBrowser1);
                    webBrowser2.Show();
                    webBrowser2.Navigate(filesInfoList[infoListIndex].FullName);
                    firstBrowserDown = false;
                }
                else
                {
                    HideNavigate(webBrowser2);
                    webBrowser1.Show();
                    webBrowser1.Navigate(filesInfoList[infoListIndex].FullName);
                    firstBrowserDown = true;
                }

                oldFileName.Text = filesInfoList[infoListIndex].Name;

                log.WriteLine(stackTraceFrame.GetMethod() + ":\n File: " + filesInfoList[infoListIndex].FullName);
                InfoLabel.Text = string.Empty;
            }
            else
            {
                InfoLabel.Text = "Выберите папку с pdf-файлами и папку назначения";
            }
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

            CreateNameForFile();
        }

        public void HideNavigate(WebBrowser webBrowser)
        {
            webBrowser.Hide();
            string browserName = webBrowser.Name;
            int tabIndex = webBrowser.TabIndex;
            webBrowser.Dispose();
            webBrowser = new WebBrowser();
            webBrowser.Location = new System.Drawing.Point(466, 27);
            webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            webBrowser.Name = browserName;
            webBrowser.Size = new System.Drawing.Size(773, 516);
            webBrowser.TabIndex = tabIndex;
            if (infoListIndex + 1 < filesInfoList.Count)
            {
                webBrowser.Navigate(filesInfoList[infoListIndex + 1].FullName);
            }
            else
            {
                MessageBox.Show("Следующего файла нет.\nВсе pdf-файлы в папке обработаны", "Информация!");
            }
        }

        private void CreateNameForFile()
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
                log.WriteLine(stackTraceFrame.GetMethod() + "\nName for File:" + nameForFile);
                log.WriteLine(article.ToString());
            }
        }

        private void NextFileButton_Click_1(object sender, EventArgs e)
        {
            article.FileName = NewFileNameInput.Text;

            if (article.FileName.Contains(".pdf"))
            {
                if (++infoListIndex < filesInfoList.Count)
                {
                    MessageBox.Show("index =" + infoListIndex);
                    ShowPDF();
                    bool moved = fileHandler.Move(filesInfoList[infoListIndex - 1], OutputPath.Text + article.FileName);
                    if (moved)
                    {
                        filesToDelete.Add(filesInfoList[infoListIndex - 1]);
                        excelHandler.AddRow(article);
                        excelHandler.SaveFile();
                        InfoLabel.Text = string.Empty;
                        NewFileNameInput.Text = "";
                        ClearInputs();
                    }
                }
            }
            else
            {
                InfoLabel.Text = nameForFile;
            }
        }

        private void ClearInputs()
        {
            AutorInput.Text = string.Empty;
            TitleInput.Text = string.Empty;
            TownInput.Text = string.Empty;
            YearInput.Text = string.Empty;
            PagesInput.Text = string.Empty;

            JTitleInput.Text = string.Empty;
            JNumberInput.Text = string.Empty;
            JVolumeInput.Text = string.Empty;
        }

        protected override void OnClosed(EventArgs e)
        {
            webBrowser1.Dispose();
            if (filesToDelete != null)
            {
                foreach (FileInfo fileInfo in filesToDelete)
                {
                    fileHandler.Delete(fileInfo);
                }
            }
            excelHandler.SaveFile();
            excelHandler.Quit();
            base.OnClosed(e);
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
    }
}