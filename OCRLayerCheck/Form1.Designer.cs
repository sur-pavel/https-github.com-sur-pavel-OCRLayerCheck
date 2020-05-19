namespace OCRLayerCheck
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.nameForFileLabel = new System.Windows.Forms.Label();
            this.InputPath = new System.Windows.Forms.TextBox();
            this.OpenWithOCRDirectory = new System.Windows.Forms.Button();
            this.OpenWithoutOCRDirectory = new System.Windows.Forms.Button();
            this.CreateFileNameButton = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.TitleInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.AutorInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.JTitleInput = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.YearInput = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TownInput = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.JNumberInput = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.JTownInput = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.ChooseInputPath = new System.Windows.Forms.Button();
            this.ChooseOutputPath = new System.Windows.Forms.Button();
            this.OutputPath = new System.Windows.Forms.TextBox();
            this.NextFileButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameForFileLabel
            // 
            this.nameForFileLabel.AutoSize = true;
            this.nameForFileLabel.Location = new System.Drawing.Point(183, 412);
            this.nameForFileLabel.Name = "nameForFileLabel";
            this.nameForFileLabel.Size = new System.Drawing.Size(64, 13);
            this.nameForFileLabel.TabIndex = 5;
            this.nameForFileLabel.Text = "Имя файла";
            // 
            // InputPath
            // 
            this.InputPath.Location = new System.Drawing.Point(71, 341);
            this.InputPath.Name = "InputPath";
            this.InputPath.Size = new System.Drawing.Size(283, 20);
            this.InputPath.TabIndex = 6;
            this.InputPath.TextChanged += new System.EventHandler(this.PathWithoutOCR_TextChanged);
            // 
            // OpenWithOCRDirectory
            // 
            this.OpenWithOCRDirectory.Location = new System.Drawing.Point(628, 559);
            this.OpenWithOCRDirectory.Name = "OpenWithOCRDirectory";
            this.OpenWithOCRDirectory.Size = new System.Drawing.Size(75, 23);
            this.OpenWithOCRDirectory.TabIndex = 18;
            this.OpenWithOCRDirectory.Text = "Назад";
            this.OpenWithOCRDirectory.Click += new System.EventHandler(this.OpenWithOCRDirectory_Click);
            // 
            // OpenWithoutOCRDirectory
            // 
            this.OpenWithoutOCRDirectory.Location = new System.Drawing.Point(856, 559);
            this.OpenWithoutOCRDirectory.Name = "OpenWithoutOCRDirectory";
            this.OpenWithoutOCRDirectory.Size = new System.Drawing.Size(75, 23);
            this.OpenWithoutOCRDirectory.TabIndex = 9;
            this.OpenWithoutOCRDirectory.Text = "Вперед";
            this.OpenWithoutOCRDirectory.UseVisualStyleBackColor = true;
            this.OpenWithoutOCRDirectory.Click += new System.EventHandler(this.NextPageButton);
            // 
            // CreateFileNameButton
            // 
            this.CreateFileNameButton.Location = new System.Drawing.Point(229, 304);
            this.CreateFileNameButton.Name = "CreateFileNameButton";
            this.CreateFileNameButton.Size = new System.Drawing.Size(75, 23);
            this.CreateFileNameButton.TabIndex = 7;
            this.CreateFileNameButton.Text = "Выбрать";
            this.CreateFileNameButton.UseVisualStyleBackColor = true;
            this.CreateFileNameButton.Click += new System.EventHandler(this.OpenFromDirectory_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(496, 12);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(550, 498);
            this.webBrowser1.TabIndex = 10;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // TitleInput
            // 
            this.TitleInput.Location = new System.Drawing.Point(132, 88);
            this.TitleInput.Name = "TitleInput";
            this.TitleInput.Size = new System.Drawing.Size(283, 20);
            this.TitleInput.TabIndex = 4;
            this.TitleInput.TextChanged += new System.EventHandler(this.PathWithOCR_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(721, 528);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Выберите страницу";
            // 
            // AutorInput
            // 
            this.AutorInput.Location = new System.Drawing.Point(132, 48);
            this.AutorInput.Name = "AutorInput";
            this.AutorInput.Size = new System.Drawing.Size(283, 20);
            this.AutorInput.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Автор";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Заглавие";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 203);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Заглавие журнала";
            // 
            // JTitleInput
            // 
            this.JTitleInput.Location = new System.Drawing.Point(132, 200);
            this.JTitleInput.Name = "JTitleInput";
            this.JTitleInput.Size = new System.Drawing.Size(283, 20);
            this.JTitleInput.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Год";
            // 
            // YearInput
            // 
            this.YearInput.Location = new System.Drawing.Point(132, 168);
            this.YearInput.Name = "YearInput";
            this.YearInput.Size = new System.Drawing.Size(283, 20);
            this.YearInput.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Место";
            // 
            // TownInput
            // 
            this.TownInput.Location = new System.Drawing.Point(132, 128);
            this.TownInput.Name = "TownInput";
            this.TownInput.Size = new System.Drawing.Size(283, 20);
            this.TownInput.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 281);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "№ / Год журнала";
            // 
            // JNumberInput
            // 
            this.JNumberInput.Location = new System.Drawing.Point(132, 278);
            this.JNumberInput.Name = "JNumberInput";
            this.JNumberInput.Size = new System.Drawing.Size(283, 20);
            this.JNumberInput.TabIndex = 19;
            this.JNumberInput.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 238);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Место журнала";
            // 
            // JTownInput
            // 
            this.JTownInput.Location = new System.Drawing.Point(132, 235);
            this.JTownInput.Name = "JTownInput";
            this.JTownInput.Size = new System.Drawing.Size(283, 20);
            this.JTownInput.TabIndex = 21;
            // 
            // comboBox1
            // 
            this.comboBox1.DisplayMember = "0";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Журналы",
            "Книги"});
            this.comboBox1.Location = new System.Drawing.Point(217, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 24;
            this.comboBox1.ValueMember = "0";
            // 
            // ChooseInputPath
            // 
            this.ChooseInputPath.Location = new System.Drawing.Point(372, 341);
            this.ChooseInputPath.Name = "ChooseInputPath";
            this.ChooseInputPath.Size = new System.Drawing.Size(75, 23);
            this.ChooseInputPath.TabIndex = 25;
            this.ChooseInputPath.Text = "Открыть";
            this.ChooseInputPath.UseVisualStyleBackColor = true;
            this.ChooseInputPath.Click += new System.EventHandler(this.button2_Click);
            // 
            // ChooseOutputPath
            // 
            this.ChooseOutputPath.Location = new System.Drawing.Point(372, 376);
            this.ChooseOutputPath.Name = "ChooseOutputPath";
            this.ChooseOutputPath.Size = new System.Drawing.Size(75, 23);
            this.ChooseOutputPath.TabIndex = 27;
            this.ChooseOutputPath.Text = "Открыть";
            this.ChooseOutputPath.UseVisualStyleBackColor = true;
            this.ChooseOutputPath.Click += new System.EventHandler(this.ChooseOutputPath_Click);
            // 
            // OutputPath
            // 
            this.OutputPath.Location = new System.Drawing.Point(71, 376);
            this.OutputPath.Name = "OutputPath";
            this.OutputPath.Size = new System.Drawing.Size(283, 20);
            this.OutputPath.TabIndex = 26;
            this.OutputPath.TextChanged += new System.EventHandler(this.OutputPath_TextChanged);
            // 
            // NextFileButton
            // 
            this.NextFileButton.Location = new System.Drawing.Point(148, 453);
            this.NextFileButton.Name = "NextFileButton";
            this.NextFileButton.Size = new System.Drawing.Size(136, 23);
            this.NextFileButton.TabIndex = 28;
            this.NextFileButton.Text = "След. файл";
            this.NextFileButton.UseVisualStyleBackColor = true;
            this.NextFileButton.Click += new System.EventHandler(this.NextFileButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 610);
            this.Controls.Add(this.NextFileButton);
            this.Controls.Add(this.ChooseOutputPath);
            this.Controls.Add(this.OutputPath);
            this.Controls.Add(this.ChooseInputPath);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.JTownInput);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.JNumberInput);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TownInput);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.YearInput);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.JTitleInput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.OpenWithoutOCRDirectory);
            this.Controls.Add(this.OpenWithOCRDirectory);
            this.Controls.Add(this.CreateFileNameButton);
            this.Controls.Add(this.InputPath);
            this.Controls.Add(this.nameForFileLabel);
            this.Controls.Add(this.TitleInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AutorInput);
            this.Name = "Form1";
            this.Text = "Выберите страницу";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label nameForFileLabel;
        private System.Windows.Forms.TextBox InputPath;
        private System.Windows.Forms.Button OpenWithOCRDirectory;
        private System.Windows.Forms.Button OpenWithoutOCRDirectory;
        private System.Windows.Forms.Button CreateFileNameButton;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox TitleInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox AutorInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox JTitleInput;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox YearInput;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TownInput;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox JNumberInput;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox JTownInput;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button ChooseInputPath;
        private System.Windows.Forms.Button ChooseOutputPath;
        private System.Windows.Forms.TextBox OutputPath;
        private System.Windows.Forms.Button NextFileButton;
    }
}

