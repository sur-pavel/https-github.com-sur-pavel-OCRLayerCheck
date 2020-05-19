using System;
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
        private ArticleParser articleParser;

        private PDFHandler pdfHandler;
        private int pageNumber = 1;
        private string pdfText = string.Empty;
        private string inputPath = @"d:\на переименование\";
        private string outputPath = @"d:\переим\";
        private string nameForFile = string.Empty;
        private TaskCompletionSource<bool> clickWaitTask;
        private Article article = new Article();
        private FileInfo currentFileInfo;
        private CancellationTokenSource tokenSource;
        private ManualResetEvent firstResetEvent = new ManualResetEvent(true);
        private ManualResetEvent secondResetEvent = new ManualResetEvent(true);

        public Form1()
        {
            fileHandler = new FileHandler();
            excelHandler = new ExcelHandler(log);

            excelHandler.CreatExcelObject();
            articleParser = new ArticleParser(log, pdfHandler);
            pdfHandler = new PDFHandler(pageNumber, log, patterns);
            pdfHandler.log = this.log;
            fileHandler.log = this.log;
            log.CreateLogFile();

            FullScreen();
            InitializeComponent();
            InputPath.Text = inputPath;
            OutputPath.Text = outputPath;

            log.WriteLine("All tasks ended");
        }

        private void GetNameForPdf()
        {
            try
            {
                if (Regex.IsMatch(InputPath.Text, patterns.DirectoryPath) &&
                                Regex.IsMatch(OutputPath.Text, patterns.DirectoryPath))
                {
                    FileInfo[] files = fileHandler.GetFileNames(InputPath.Text);

                    foreach (FileInfo fileInfo in files)
                    {
                        Thread.Sleep(500);
                        currentFileInfo = fileInfo;
                        firstResetEvent.Reset();
                        Invoke((Action)delegate
                        {
                            log.WriteLine("File: " + fileInfo.FullName);
                            log.WriteLine("PageNumber = " + pageNumber);
                            NavigatePages(fileInfo);

                            webBrowser1.DocumentText = pdfText;
                            article.Autor = AutorInput.Text;
                            article.Title = TitleInput.Text;
                            article.Edition.Town = TownInput.Text;
                            article.Edition.Year = int.Parse(YearInput.Text);

                            nameForFileLabel.Text = nameForFile;
                            article.FileName = nameForFile;

                            excelHandler.AddRow(article);
                            log.WriteLine("Name for File:" + nameForFile);
                            log.WriteLine(article.ToString());
                        });
                        firstResetEvent.WaitOne();
                    }

                    excelHandler.SaveFile();
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

        private void NavigatePages(FileInfo fileInfo)
        {
            int prevPageNumber = pageNumber;
            do
            {
                secondResetEvent.Reset();
                pdfText = pdfHandler.ParsePage(fileInfo, pageNumber).PdfText;
                secondResetEvent.WaitOne();
            } while (pageNumber != prevPageNumber);
        }

        private void FullScreen()
        {
            WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
        }

        private void OpenFromDirectory_Click(object sender, EventArgs e)
        {
            nameForFile = articleParser.GetFileName(article);
            nameForFileLabel.Text = nameForFile;
        }

        private void PreviousPageButton(object sender, EventArgs e)
        {
            secondResetEvent.Set();
            if (pageNumber > 0 && pageNumber < 10)
            {
                pageNumber--;
            }
        }

        private void NextPageButton(object sender, EventArgs e)
        {
            secondResetEvent.Set();
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

        private void PathWithoutOCR_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(InputPath.Text))
            {
                tokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = tokenSource.Token;
                Task.Factory.StartNew(() => GetNameForPdf(), cancellationToken);
            }
        }

        private void PathWithOCR_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //using (OpenFileDialog openFileDialog = new OpenFileDialog())
            //{
            //    openFileDialog.InitialDirectory = "c:\\";
            //    openFileDialog.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            //    openFileDialog.FilterIndex = 1;
            //    openFileDialog.RestoreDirectory = true;

            //    if (openFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        AutorInput.Text = openFileDialog.FileName;
            //    }
            //}
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
            firstResetEvent.Set();
            fileHandler.Save(currentFileInfo, nameForFile, OutputPath.Text);
        }

        private void OpenWithOCRDirectory_Click(object sender, EventArgs e)
        {
            secondResetEvent.Set();
            if (pageNumber > 0 && pageNumber < 10)
            {
                pageNumber--;
            }
        }

        private void OutputPath_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(OutputPath.Text))
            {
                tokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = tokenSource.Token;
                Task.Factory.StartNew(() => GetNameForPdf(), cancellationToken);
            }
        }
    }
}