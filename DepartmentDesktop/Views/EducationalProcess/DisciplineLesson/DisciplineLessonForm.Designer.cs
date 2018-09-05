namespace DepartmentDesktop.Views.EducationalProcess.DisciplineLesson
{
    partial class DisciplineLessonForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxPostTitle = new System.Windows.Forms.TextBox();
            this.labelPostTitle = new System.Windows.Forms.Label();
            this.buttonAddFile = new System.Windows.Forms.Button();
            this.labelDiscription = new System.Windows.Forms.Label();
            this.textBoxDiscription = new System.Windows.Forms.TextBox();
            this.comboBoxDiscipline = new System.Windows.Forms.ComboBox();
            this.labelDiscipline = new System.Windows.Forms.Label();
            this.labelAddFile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(184, 130);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 12;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(331, 130);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 13;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(103, 130);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 11;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxPostTitle
            // 
            this.textBoxPostTitle.Location = new System.Drawing.Point(93, 12);
            this.textBoxPostTitle.Name = "textBoxPostTitle";
            this.textBoxPostTitle.Size = new System.Drawing.Size(313, 20);
            this.textBoxPostTitle.TabIndex = 8;
            // 
            // labelPostTitle
            // 
            this.labelPostTitle.AutoSize = true;
            this.labelPostTitle.Location = new System.Drawing.Point(23, 15);
            this.labelPostTitle.Name = "labelPostTitle";
            this.labelPostTitle.Size = new System.Drawing.Size(68, 13);
            this.labelPostTitle.TabIndex = 7;
            this.labelPostTitle.Text = "Заголовок*:";
            // 
            // buttonAddFile
            // 
            this.buttonAddFile.Location = new System.Drawing.Point(131, 101);
            this.buttonAddFile.Name = "buttonAddFile";
            this.buttonAddFile.Size = new System.Drawing.Size(275, 23);
            this.buttonAddFile.TabIndex = 17;
            this.buttonAddFile.Text = "Добавить файл";
            this.buttonAddFile.Click += new System.EventHandler(this.buttonAddFile_Click);
            // 
            // labelDiscription
            // 
            this.labelDiscription.AutoSize = true;
            this.labelDiscription.Location = new System.Drawing.Point(23, 48);
            this.labelDiscription.Name = "labelDiscription";
            this.labelDiscription.Size = new System.Drawing.Size(64, 13);
            this.labelDiscription.TabIndex = 15;
            this.labelDiscription.Text = "Описание*:";
            // 
            // textBoxDiscription
            // 
            this.textBoxDiscription.Location = new System.Drawing.Point(95, 48);
            this.textBoxDiscription.Name = "textBoxDiscription";
            this.textBoxDiscription.Size = new System.Drawing.Size(311, 20);
            this.textBoxDiscription.TabIndex = 16;
            // 
            // comboBoxDiscipline
            // 
            this.comboBoxDiscipline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiscipline.FormattingEnabled = true;
            this.comboBoxDiscipline.Location = new System.Drawing.Point(131, 74);
            this.comboBoxDiscipline.Name = "comboBoxDiscipline";
            this.comboBoxDiscipline.Size = new System.Drawing.Size(275, 21);
            this.comboBoxDiscipline.TabIndex = 19;
            // 
            // labelDiscipline
            // 
            this.labelDiscipline.AutoSize = true;
            this.labelDiscipline.Location = new System.Drawing.Point(23, 77);
            this.labelDiscipline.Name = "labelDiscipline";
            this.labelDiscipline.Size = new System.Drawing.Size(77, 13);
            this.labelDiscipline.TabIndex = 18;
            this.labelDiscipline.Text = "Дисциплина*:";
            // 
            // labelAddFile
            // 
            this.labelAddFile.AutoSize = true;
            this.labelAddFile.Location = new System.Drawing.Point(23, 106);
            this.labelAddFile.Name = "labelAddFile";
            this.labelAddFile.Size = new System.Drawing.Size(102, 13);
            this.labelAddFile.TabIndex = 20;
            this.labelAddFile.Text = "Файл не добавлен";
            // 
            // DisciplineLessonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 165);
            this.Controls.Add(this.labelAddFile);
            this.Controls.Add(this.comboBoxDiscipline);
            this.Controls.Add(this.labelDiscipline);
            this.Controls.Add(this.textBoxDiscription);
            this.Controls.Add(this.labelDiscription);
            this.Controls.Add(this.buttonAddFile);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxPostTitle);
            this.Controls.Add(this.labelPostTitle);
            this.Name = "DisciplineLessonForm";
            this.Text = "Добавить занятие";
            this.Load += new System.EventHandler(this.DisciplineLessonForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxPostTitle;
        private System.Windows.Forms.Label labelPostTitle;
        private System.Windows.Forms.Button buttonAddFile;
        private System.Windows.Forms.Label labelDiscription;
        private System.Windows.Forms.TextBox textBoxDiscription;
        private System.Windows.Forms.ComboBox comboBoxDiscipline;
        private System.Windows.Forms.Label labelDiscipline;
        private System.Windows.Forms.Label labelAddFile;
    }
}