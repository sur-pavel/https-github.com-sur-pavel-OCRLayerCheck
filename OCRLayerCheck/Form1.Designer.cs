using System.Windows.Forms.VisualStyles;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.AutorInput = new System.Windows.Forms.TextBox();
            this.TitleInput = new System.Windows.Forms.TextBox();
            this.TownInput = new System.Windows.Forms.TextBox();
            this.YearInput = new System.Windows.Forms.TextBox();
            this.PagesInput = new System.Windows.Forms.TextBox();
            this.JTitleInput = new System.Windows.Forms.TextBox();
            this.JNumberInput = new System.Windows.Forms.TextBox();
            this.JVolumeInput = new System.Windows.Forms.TextBox();
            this.InputPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ChooseInputPath = new System.Windows.Forms.Button();
            this.ChooseOutputPath = new System.Windows.Forms.Button();
            this.OutputPath = new System.Windows.Forms.TextBox();
            this.NextFileButton = new System.Windows.Forms.Button();
            this.oldFileName = new System.Windows.Forms.Label();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.PagesLabel = new System.Windows.Forms.Label();
            this.VolumeJLabel = new System.Windows.Forms.Label();
            this.NewFileNameInput = new System.Windows.Forms.TextBox();
            this.ToPathLabel = new System.Windows.Forms.Label();
            this.FromPathLabel = new System.Windows.Forms.Label();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.BackButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.DocType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // AutorInput
            // 
            this.AutorInput.Location = new System.Drawing.Point(132, 48);
            this.AutorInput.Name = "AutorInput";
            this.AutorInput.Size = new System.Drawing.Size(283, 20);
            this.AutorInput.TabIndex = 1;
            this.AutorInput.TextChanged += new System.EventHandler(this.AutorInput_TextChanged);
            // 
            // TitleInput
            // 
            this.TitleInput.Location = new System.Drawing.Point(132, 88);
            this.TitleInput.Name = "TitleInput";
            this.TitleInput.Size = new System.Drawing.Size(283, 20);
            this.TitleInput.TabIndex = 2;
            this.TitleInput.TextChanged += new System.EventHandler(this.TitleInput_TextChanged);
            // 
            // TownInput
            // 
            this.TownInput.Location = new System.Drawing.Point(132, 128);
            this.TownInput.Name = "TownInput";
            this.TownInput.Size = new System.Drawing.Size(283, 20);
            this.TownInput.TabIndex = 3;
            this.TownInput.TextChanged += new System.EventHandler(this.TownInput_TextChanged);
            // 
            // YearInput
            // 
            this.YearInput.Location = new System.Drawing.Point(132, 168);
            this.YearInput.Name = "YearInput";
            this.YearInput.Size = new System.Drawing.Size(283, 20);
            this.YearInput.TabIndex = 4;
            this.YearInput.TextChanged += new System.EventHandler(this.YearInput_TextChanged);
            // 
            // PagesInput
            // 
            this.PagesInput.Location = new System.Drawing.Point(132, 205);
            this.PagesInput.Name = "PagesInput";
            this.PagesInput.Size = new System.Drawing.Size(283, 20);
            this.PagesInput.TabIndex = 5;
            this.PagesInput.TextChanged += new System.EventHandler(this.PagesInput_TextChanged);
            // 
            // JTitleInput
            // 
            this.JTitleInput.Location = new System.Drawing.Point(132, 238);
            this.JTitleInput.Name = "JTitleInput";
            this.JTitleInput.Size = new System.Drawing.Size(283, 20);
            this.JTitleInput.TabIndex = 6;
            this.JTitleInput.TextChanged += new System.EventHandler(this.JTitleInput_TextChanged);
            // 
            // JNumberInput
            // 
            this.JNumberInput.Location = new System.Drawing.Point(132, 277);
            this.JNumberInput.Name = "JNumberInput";
            this.JNumberInput.Size = new System.Drawing.Size(283, 20);
            this.JNumberInput.TabIndex = 7;
            this.JNumberInput.TextChanged += new System.EventHandler(this.JNumberInput_TextChanged);
            // 
            // JVolumeInput
            // 
            this.JVolumeInput.Location = new System.Drawing.Point(132, 316);
            this.JVolumeInput.Name = "JVolumeInput";
            this.JVolumeInput.Size = new System.Drawing.Size(283, 20);
            this.JVolumeInput.TabIndex = 8;
            this.JVolumeInput.TextChanged += new System.EventHandler(this.JVolumeInput_TextChanged);
            // 
            // InputPath
            // 
            this.InputPath.Location = new System.Drawing.Point(68, 396);
            this.InputPath.Name = "InputPath";
            this.InputPath.Size = new System.Drawing.Size(283, 20);
            this.InputPath.TabIndex = 9;
            this.InputPath.TextChanged += new System.EventHandler(this.InputPath_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 10;
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
            this.label5.Location = new System.Drawing.Point(12, 241);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Заглавие журнала";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Год";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Место";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 280);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "№ / Год журнала";
            // 
            // ChooseInputPath
            // 
            this.ChooseInputPath.Location = new System.Drawing.Point(369, 396);
            this.ChooseInputPath.Name = "ChooseInputPath";
            this.ChooseInputPath.Size = new System.Drawing.Size(75, 23);
            this.ChooseInputPath.TabIndex = 16;
            this.ChooseInputPath.Text = "Открыть";
            this.ChooseInputPath.UseVisualStyleBackColor = true;
            this.ChooseInputPath.Click += new System.EventHandler(this.ChooseInputPath_Click);
            // 
            // ChooseOutputPath
            // 
            this.ChooseOutputPath.Location = new System.Drawing.Point(369, 431);
            this.ChooseOutputPath.Name = "ChooseOutputPath";
            this.ChooseOutputPath.Size = new System.Drawing.Size(75, 23);
            this.ChooseOutputPath.TabIndex = 18;
            this.ChooseOutputPath.Text = "Открыть";
            this.ChooseOutputPath.UseVisualStyleBackColor = true;
            this.ChooseOutputPath.Click += new System.EventHandler(this.ChooseOutputPath_Click);
            // 
            // OutputPath
            // 
            this.OutputPath.Location = new System.Drawing.Point(68, 431);
            this.OutputPath.Name = "OutputPath";
            this.OutputPath.Size = new System.Drawing.Size(283, 20);
            this.OutputPath.TabIndex = 17;
            this.OutputPath.TextChanged += new System.EventHandler(this.OutputPath_TextChanged);
            // 
            // NextFileButton
            // 
            this.NextFileButton.Location = new System.Drawing.Point(169, 580);
            this.NextFileButton.Name = "NextFileButton";
            this.NextFileButton.Size = new System.Drawing.Size(136, 23);
            this.NextFileButton.TabIndex = 19;
            this.NextFileButton.Text = "След. файл";
            this.NextFileButton.UseVisualStyleBackColor = true;
            this.NextFileButton.Click += new System.EventHandler(this.NextFileButton_Click_1);
            // 
            // oldFileName
            // 
            this.oldFileName.AutoSize = true;
            this.oldFileName.Location = new System.Drawing.Point(28, 464);
            this.oldFileName.MaximumSize = new System.Drawing.Size(400, 0);
            this.oldFileName.Name = "oldFileName";
            this.oldFileName.Size = new System.Drawing.Size(101, 13);
            this.oldFileName.TabIndex = 20;
            this.oldFileName.Text = "Старое имя файла";
            // 
            // PagesLabel
            // 
            this.PagesLabel.AutoSize = true;
            this.PagesLabel.Location = new System.Drawing.Point(28, 205);
            this.PagesLabel.Name = "PagesLabel";
            this.PagesLabel.Size = new System.Drawing.Size(57, 13);
            this.PagesLabel.TabIndex = 21;
            this.PagesLabel.Text = "Страницы";
            // 
            // VolumeJLabel
            // 
            this.VolumeJLabel.AutoSize = true;
            this.VolumeJLabel.Location = new System.Drawing.Point(12, 319);
            this.VolumeJLabel.Name = "VolumeJLabel";
            this.VolumeJLabel.Size = new System.Drawing.Size(108, 13);
            this.VolumeJLabel.TabIndex = 22;
            this.VolumeJLabel.Text = "Том журнала/книги";
            // 
            // NewFileNameInput
            // 
            this.NewFileNameInput.Location = new System.Drawing.Point(15, 620);
            this.NewFileNameInput.Name = "NewFileNameInput";
            this.NewFileNameInput.Size = new System.Drawing.Size(548, 20);
            this.NewFileNameInput.TabIndex = 23;
            // 
            // ToPathLabel
            // 
            this.ToPathLabel.AutoSize = true;
            this.ToPathLabel.Location = new System.Drawing.Point(28, 436);
            this.ToPathLabel.Name = "ToPathLabel";
            this.ToPathLabel.Size = new System.Drawing.Size(14, 13);
            this.ToPathLabel.TabIndex = 24;
            this.ToPathLabel.Text = "В";
            // 
            // FromPathLabel
            // 
            this.FromPathLabel.AutoSize = true;
            this.FromPathLabel.Location = new System.Drawing.Point(28, 401);
            this.FromPathLabel.Name = "FromPathLabel";
            this.FromPathLabel.Size = new System.Drawing.Size(21, 13);
            this.FromPathLabel.TabIndex = 25;
            this.FromPathLabel.Text = "Из";
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Location = new System.Drawing.Point(105, 530);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(0, 13);
            this.InfoLabel.TabIndex = 26;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(461, 27);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(845, 576);
            this.webBrowser1.TabIndex = 27;
            // 
            // BackButton
            // 
            this.BackButton.Location = new System.Drawing.Point(830, 617);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(75, 23);
            this.BackButton.TabIndex = 28;
            this.BackButton.Text = "Назад";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(961, 620);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(242, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Нажмите, если открылась ссылка в браузере";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "label3";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 358);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Тип документа";
            // 
            // DocType
            // 
            this.DocType.FormattingEnabled = true;
            this.DocType.Items.AddRange(new object[] {
            "Книга",
            "Статья",
            "Книга?"});
            this.DocType.Location = new System.Drawing.Point(132, 355);
            this.DocType.Name = "DocType";
            this.DocType.Size = new System.Drawing.Size(121, 21);
            this.DocType.TabIndex = 32;
            this.DocType.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1318, 656);
            this.Controls.Add(this.DocType);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.FromPathLabel);
            this.Controls.Add(this.ToPathLabel);
            this.Controls.Add(this.NewFileNameInput);
            this.Controls.Add(this.VolumeJLabel);
            this.Controls.Add(this.JVolumeInput);
            this.Controls.Add(this.PagesLabel);
            this.Controls.Add(this.PagesInput);
            this.Controls.Add(this.oldFileName);
            this.Controls.Add(this.NextFileButton);
            this.Controls.Add(this.ChooseOutputPath);
            this.Controls.Add(this.OutputPath);
            this.Controls.Add(this.ChooseInputPath);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.JNumberInput);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TownInput);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.YearInput);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.JTitleInput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.InputPath);
            this.Controls.Add(this.TitleInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AutorInput);
            this.Controls.Add(this.webBrowser1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "OCRLayerCheck";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox InputPath;
        private System.Windows.Forms.TextBox TitleInput;
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
        private System.Windows.Forms.Button ChooseInputPath;
        private System.Windows.Forms.Button ChooseOutputPath;
        private System.Windows.Forms.TextBox OutputPath;
        private System.Windows.Forms.Button NextFileButton;
        private System.Windows.Forms.Label oldFileName;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Label PagesLabel;
        private System.Windows.Forms.TextBox PagesInput;
        private System.Windows.Forms.Label VolumeJLabel;
        private System.Windows.Forms.TextBox JVolumeInput;
        private System.Windows.Forms.TextBox NewFileNameInput;
        private System.Windows.Forms.Label ToPathLabel;
        private System.Windows.Forms.Label FromPathLabel;
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox DocType;
    }
}

