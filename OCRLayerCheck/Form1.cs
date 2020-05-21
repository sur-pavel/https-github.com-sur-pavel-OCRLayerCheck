using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
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
        private Article article = new Article();
        private ArticleParser articleParser;

        private PDFHandler pdfHandler;
        private int pageNumber = 1;
        private string inputPath = @"c:\Users\sur-p\Downloads\на переименование\";
        private string outputPath = @"c:\Users\sur-p\Downloads\переим\";
        private string nameForFile = string.Empty;
        private IEnumerator<FileInfo> files;
        private CancellationTokenSource tokenSource;

        public Form1()
        {
            fileHandler = new FileHandler();
            excelHandler = new ExcelHandler(log);

            excelHandler.CreatExcelObject();
            articleParser = new ArticleParser(log, patterns);
            pdfHandler = new PDFHandler(log, patterns);
            pdfHandler.log = log;
            fileHandler.log = log;
            log.CreateLogFile();

            InitializeComponent();
            comboBox1.Hide();
            FullScreen();

            //InputPath.Text = inputPath;
            //OutputPath.Text = outputPath;
        }

        private void GetNameForPdf()
        {
            try
            {
                if (Regex.IsMatch(InputPath.Text, patterns.DirectoryPath) &&
                                Regex.IsMatch(OutputPath.Text, patterns.DirectoryPath))
                {
                    FileInfo fileInfo = files.Current;
                    log.WriteLine("File: " + fileInfo.FullName);
                    log.WriteLine("PageNumber = " + pageNumber);
                }
            }
            catch (NullReferenceException nullex)
            {
                log.WriteLine(nullex.Message, nullex.StackTrace);
                MessageBox.Show($"{nullex.Message}\n{nullex.StackTrace}");
            }
            catch (Exception ex)
            {
                log.WriteLine(ex.Message, ex.StackTrace);
                MessageBox.Show($"{ex.Message}\n{ex.StackTrace}");
            }
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

        private void PreviousPageButton(object sender, EventArgs e)
        {
            if (pageNumber > 0 && pageNumber < 10)
            {
                pageNumber--;
            }
        }

        private void NextPageButton(object sender, EventArgs e)
        {
            if (pageNumber > 0 && pageNumber < 10)
            {
                pageNumber++;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
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

        private void NextFileButton_Click(object sender, EventArgs e)
        {
            article.FileName = nameForFile;

            if (nameForFile.Contains(".pdf"))
            {
                excelHandler.AddRow(article);
                fileHandler.Save(files.Current, nameForFile, OutputPath.Text);
                files.MoveNext();
                webBrowser1.Navigate(files.Current.FullName);
                oldFileName.Text = files.Current.Name;
                excelHandler.SaveFile();
                InfoLabel.Text = "Info";
            }
            else
            {
                InfoLabel.Text = nameForFile;
            }
        }

        private void OpenWithOCRDirectory_Click(object sender, EventArgs e)
        {
            if (pageNumber > 0 && pageNumber < 10)
            {
                pageNumber--;
            }
        }

        private void InputPath_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(InputPath.Text, patterns.DirectoryPath) &&
                                Regex.IsMatch(OutputPath.Text, patterns.DirectoryPath))
            {
                files = fileHandler.GetFileNames(InputPath.Text);
                files.MoveNext();
                webBrowser1.Navigate(files.Current.FullName);
                oldFileName.Text = files.Current.Name;
                FileInfo fileInfo = files.Current;
                log.WriteLine("File: " + fileInfo.FullName);
                log.WriteLine("PageNumber = " + pageNumber);
            }
            else
            {
                InfoLabel.Text = "Выберите папку с pdf-файлами и папку назначения";
            }
        }

        private void OutputPath_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(InputPath.Text, patterns.DirectoryPath) &&
                                Regex.IsMatch(OutputPath.Text, patterns.DirectoryPath))
            {
                files = fileHandler.GetFileNames(InputPath.Text);
                files.MoveNext();
                webBrowser1.Navigate(files.Current.FullName);
                oldFileName.Text = files.Current.Name;
                FileInfo fileInfo = files.Current;
                log.WriteLine("File: " + fileInfo.FullName);
                log.WriteLine("PageNumber = " + pageNumber);
            }
            else
            {
                InfoLabel.Text = "Выберите папку с pdf-файлами и папку назначения";
            }
        }

        private void CreateNameForFile(object sender, EventArgs e)
        {
            article.Autor = AutorInput.Text;
            article.Title = TitleInput.Text;
            article.Town = TownInput.Text;
            article.Year = YearInput.Text;
            article.Pages = PagesInput.Text;

            article.Journal.Title = JTitleInput.Text;
            article.Journal.Number = JNumberInput.Text;
            article.Journal.Volume = JVolumeInput.Text;

            nameForFile = articleParser.GetFileName(article);
            nameForFile = articleParser.CheckFileName(nameForFile);
            if (!nameForFile.Contains(".pdf"))
            {
                InfoLabel.Text = nameForFile;
            }
            else
            {
                NewFileNameInput.Text = nameForFile;
                log.WriteLine("Name for File:" + nameForFile);
                log.WriteLine(article.ToString());
            }
        }

        private void NextFileButton_Click_1(object sender, EventArgs e)
        {
            article.FileName = nameForFile;

            if (nameForFile.Contains(".pdf"))
            {
                excelHandler.AddRow(article);
                fileHandler.Save(files.Current, nameForFile, OutputPath.Text);
                files.MoveNext();
                webBrowser1.Navigate(files.Current.FullName);
                oldFileName.Text = files.Current.Name;
                excelHandler.SaveFile();
                InfoLabel.Text = "Info";
                NewFileNameInput.Text = "";
            }
            else
            {
                InfoLabel.Text = nameForFile;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
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
        }
    }
}