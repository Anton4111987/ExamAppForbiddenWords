namespace ExamAppForbiddenWords
{
    partial class FormSearchForbiddenWords
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonStart = new Button();
            label = new Label();
            buttonLoadForb = new Button();
            buttonPath = new Button();
            progressBarSearchAllFiles = new ProgressBar();
            labelSearchAllFiles = new Label();
            progressBarCopyFiles = new ProgressBar();
            labelCopyFiles = new Label();
            progressBarContainsForbidden = new ProgressBar();
            labelContainsForbidden = new Label();
            textBoxForbiddenWords = new TextBox();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(457, 299);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(162, 44);
            buttonStart.TabIndex = 0;
            buttonStart.Text = "Start";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // label
            // 
            label.AutoSize = true;
            label.Font = new Font("Showcard Gothic", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label.ForeColor = Color.Green;
            label.Location = new Point(30, 9);
            label.Name = "label";
            label.Size = new Size(568, 33);
            label.TabIndex = 2;
            label.Text = "Приложение поиска запрещенных слов";
            // 
            // buttonLoadForb
            // 
            buttonLoadForb.Location = new Point(343, 69);
            buttonLoadForb.Name = "buttonLoadForb";
            buttonLoadForb.Size = new Size(276, 47);
            buttonLoadForb.TabIndex = 5;
            buttonLoadForb.Text = "Загрузить список запрещенных слов";
            buttonLoadForb.UseVisualStyleBackColor = true;
            buttonLoadForb.Click += buttonLoadForb_Click;
            // 
            // buttonPath
            // 
            buttonPath.Location = new Point(343, 146);
            buttonPath.Margin = new Padding(3, 2, 3, 2);
            buttonPath.Name = "buttonPath";
            buttonPath.Size = new Size(276, 38);
            buttonPath.TabIndex = 6;
            buttonPath.Text = "Выбрать путь для сохранения файлов";
            buttonPath.UseVisualStyleBackColor = true;
            buttonPath.Click += buttonPath_Click;
            // 
            // progressBarSearchAllFiles
            // 
            progressBarSearchAllFiles.Location = new Point(30, 404);
            progressBarSearchAllFiles.Name = "progressBarSearchAllFiles";
            progressBarSearchAllFiles.Size = new Size(589, 23);
            progressBarSearchAllFiles.TabIndex = 7;
            // 
            // labelSearchAllFiles
            // 
            labelSearchAllFiles.AutoSize = true;
            labelSearchAllFiles.Location = new Point(34, 375);
            labelSearchAllFiles.Name = "labelSearchAllFiles";
            labelSearchAllFiles.Size = new Size(114, 15);
            labelSearchAllFiles.TabIndex = 8;
            labelSearchAllFiles.Text = "Поиск всех файлов";
            // 
            // progressBarCopyFiles
            // 
            progressBarCopyFiles.Location = new Point(30, 561);
            progressBarCopyFiles.Name = "progressBarCopyFiles";
            progressBarCopyFiles.Size = new Size(589, 23);
            progressBarCopyFiles.TabIndex = 7;
            // 
            // labelCopyFiles
            // 
            labelCopyFiles.AutoSize = true;
            labelCopyFiles.Location = new Point(33, 525);
            labelCopyFiles.Name = "labelCopyFiles";
            labelCopyFiles.Size = new Size(220, 15);
            labelCopyFiles.TabIndex = 8;
            labelCopyFiles.Text = "Прогресс замены слов и копирования";
            // 
            // progressBarContainsForbidden
            // 
            progressBarContainsForbidden.Location = new Point(30, 483);
            progressBarContainsForbidden.Name = "progressBarContainsForbidden";
            progressBarContainsForbidden.Size = new Size(589, 23);
            progressBarContainsForbidden.TabIndex = 7;
            // 
            // labelContainsForbidden
            // 
            labelContainsForbidden.AutoSize = true;
            labelContainsForbidden.Location = new Point(33, 446);
            labelContainsForbidden.Name = "labelContainsForbidden";
            labelContainsForbidden.Size = new Size(336, 15);
            labelContainsForbidden.TabIndex = 8;
            labelContainsForbidden.Text = "Прогресс отбора файлов содержащих запрещенные слова";
            // 
            // textBoxForbiddenWords
            // 
            textBoxForbiddenWords.Location = new Point(12, 69);
            textBoxForbiddenWords.Multiline = true;
            textBoxForbiddenWords.Name = "textBoxForbiddenWords";
            textBoxForbiddenWords.Size = new Size(265, 274);
            textBoxForbiddenWords.TabIndex = 9;
            // 
            // FormSearchForbiddenWords
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(645, 631);
            Controls.Add(textBoxForbiddenWords);
            Controls.Add(labelContainsForbidden);
            Controls.Add(labelCopyFiles);
            Controls.Add(labelSearchAllFiles);
            Controls.Add(progressBarCopyFiles);
            Controls.Add(progressBarContainsForbidden);
            Controls.Add(progressBarSearchAllFiles);
            Controls.Add(buttonPath);
            Controls.Add(buttonLoadForb);
            Controls.Add(label);
            Controls.Add(buttonStart);
            Name = "FormSearchForbiddenWords";
            Text = "Поиск запрещенных слов";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonStart;
        private Label label;
        private Button buttonLoadForb;
        private Button buttonPath;
        private ProgressBar progressBarSearchAllFiles;
        private Label labelSearchAllFiles;
        private ProgressBar progressBarCopyFiles;
        private Label labelCopyFiles;
        private ProgressBar progressBarContainsForbidden;
        private Label labelContainsForbidden;
        private TextBox textBoxForbiddenWords;
    }
}