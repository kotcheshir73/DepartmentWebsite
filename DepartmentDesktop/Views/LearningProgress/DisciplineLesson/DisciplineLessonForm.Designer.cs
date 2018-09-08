namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.textBoxCountOfPairs = new System.Windows.Forms.TextBox();
            this.labelCountOfPairs = new System.Windows.Forms.Label();
            this.textBoxOrder = new System.Windows.Forms.TextBox();
            this.labelOrder = new System.Windows.Forms.Label();
            this.comboBoxLessonType = new System.Windows.Forms.ComboBox();
            this.labelLessonType = new System.Windows.Forms.Label();
            this.buttonGetFile = new System.Windows.Forms.Button();
            this.tabPageRecords = new System.Windows.Forms.TabPage();
            this.comboBoxAcademicYear = new System.Windows.Forms.ComboBox();
            this.labelAcademicYear = new System.Windows.Forms.Label();
            this.comboBoxSemester = new System.Windows.Forms.ComboBox();
            this.labelSemester = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(103, 338);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 19;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(250, 338);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 20;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(22, 338);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 18;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxPostTitle
            // 
            this.textBoxPostTitle.Location = new System.Drawing.Point(127, 117);
            this.textBoxPostTitle.Name = "textBoxPostTitle";
            this.textBoxPostTitle.Size = new System.Drawing.Size(210, 20);
            this.textBoxPostTitle.TabIndex = 9;
            // 
            // labelPostTitle
            // 
            this.labelPostTitle.AutoSize = true;
            this.labelPostTitle.Location = new System.Drawing.Point(10, 120);
            this.labelPostTitle.Name = "labelPostTitle";
            this.labelPostTitle.Size = new System.Drawing.Size(68, 13);
            this.labelPostTitle.TabIndex = 8;
            this.labelPostTitle.Text = "Заголовок*:";
            // 
            // buttonAddFile
            // 
            this.buttonAddFile.Enabled = false;
            this.buttonAddFile.Location = new System.Drawing.Point(46, 304);
            this.buttonAddFile.Name = "buttonAddFile";
            this.buttonAddFile.Size = new System.Drawing.Size(113, 23);
            this.buttonAddFile.TabIndex = 16;
            this.buttonAddFile.Text = "Добавить файл";
            this.buttonAddFile.Click += new System.EventHandler(this.buttonAddFile_Click);
            // 
            // labelDiscription
            // 
            this.labelDiscription.AutoSize = true;
            this.labelDiscription.Location = new System.Drawing.Point(10, 146);
            this.labelDiscription.Name = "labelDiscription";
            this.labelDiscription.Size = new System.Drawing.Size(64, 13);
            this.labelDiscription.TabIndex = 10;
            this.labelDiscription.Text = "Описание*:";
            // 
            // textBoxDiscription
            // 
            this.textBoxDiscription.Location = new System.Drawing.Point(127, 143);
            this.textBoxDiscription.Multiline = true;
            this.textBoxDiscription.Name = "textBoxDiscription";
            this.textBoxDiscription.Size = new System.Drawing.Size(210, 85);
            this.textBoxDiscription.TabIndex = 11;
            // 
            // comboBoxDiscipline
            // 
            this.comboBoxDiscipline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiscipline.Enabled = false;
            this.comboBoxDiscipline.FormattingEnabled = true;
            this.comboBoxDiscipline.Location = new System.Drawing.Point(127, 36);
            this.comboBoxDiscipline.Name = "comboBoxDiscipline";
            this.comboBoxDiscipline.Size = new System.Drawing.Size(210, 21);
            this.comboBoxDiscipline.TabIndex = 3;
            // 
            // labelDiscipline
            // 
            this.labelDiscipline.AutoSize = true;
            this.labelDiscipline.Location = new System.Drawing.Point(8, 39);
            this.labelDiscipline.Name = "labelDiscipline";
            this.labelDiscipline.Size = new System.Drawing.Size(77, 13);
            this.labelDiscipline.TabIndex = 2;
            this.labelDiscipline.Text = "Дисциплина*:";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageRecords);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(934, 441);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.comboBoxSemester);
            this.tabPageConfig.Controls.Add(this.labelSemester);
            this.tabPageConfig.Controls.Add(this.comboBoxAcademicYear);
            this.tabPageConfig.Controls.Add(this.labelAcademicYear);
            this.tabPageConfig.Controls.Add(this.textBoxCountOfPairs);
            this.tabPageConfig.Controls.Add(this.labelCountOfPairs);
            this.tabPageConfig.Controls.Add(this.textBoxOrder);
            this.tabPageConfig.Controls.Add(this.labelOrder);
            this.tabPageConfig.Controls.Add(this.comboBoxLessonType);
            this.tabPageConfig.Controls.Add(this.labelLessonType);
            this.tabPageConfig.Controls.Add(this.buttonGetFile);
            this.tabPageConfig.Controls.Add(this.labelPostTitle);
            this.tabPageConfig.Controls.Add(this.textBoxPostTitle);
            this.tabPageConfig.Controls.Add(this.comboBoxDiscipline);
            this.tabPageConfig.Controls.Add(this.buttonSave);
            this.tabPageConfig.Controls.Add(this.labelDiscipline);
            this.tabPageConfig.Controls.Add(this.buttonClose);
            this.tabPageConfig.Controls.Add(this.textBoxDiscription);
            this.tabPageConfig.Controls.Add(this.buttonSaveAndClose);
            this.tabPageConfig.Controls.Add(this.labelDiscription);
            this.tabPageConfig.Controls.Add(this.buttonAddFile);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(926, 415);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Занятие";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // textBoxCountOfPairs
            // 
            this.textBoxCountOfPairs.Location = new System.Drawing.Point(127, 260);
            this.textBoxCountOfPairs.Name = "textBoxCountOfPairs";
            this.textBoxCountOfPairs.Size = new System.Drawing.Size(210, 20);
            this.textBoxCountOfPairs.TabIndex = 15;
            // 
            // labelCountOfPairs
            // 
            this.labelCountOfPairs.AutoSize = true;
            this.labelCountOfPairs.Location = new System.Drawing.Point(8, 263);
            this.labelCountOfPairs.Name = "labelCountOfPairs";
            this.labelCountOfPairs.Size = new System.Drawing.Size(94, 13);
            this.labelCountOfPairs.TabIndex = 14;
            this.labelCountOfPairs.Text = "Количество пар*:";
            // 
            // textBoxOrder
            // 
            this.textBoxOrder.Location = new System.Drawing.Point(127, 234);
            this.textBoxOrder.Name = "textBoxOrder";
            this.textBoxOrder.Size = new System.Drawing.Size(210, 20);
            this.textBoxOrder.TabIndex = 13;
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.Location = new System.Drawing.Point(8, 237);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(113, 13);
            this.labelOrder.TabIndex = 12;
            this.labelOrder.Text = "Порядковый номер*:";
            // 
            // comboBoxLessonType
            // 
            this.comboBoxLessonType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLessonType.Enabled = false;
            this.comboBoxLessonType.FormattingEnabled = true;
            this.comboBoxLessonType.Location = new System.Drawing.Point(127, 90);
            this.comboBoxLessonType.Name = "comboBoxLessonType";
            this.comboBoxLessonType.Size = new System.Drawing.Size(210, 21);
            this.comboBoxLessonType.TabIndex = 7;
            // 
            // labelLessonType
            // 
            this.labelLessonType.AutoSize = true;
            this.labelLessonType.Location = new System.Drawing.Point(8, 93);
            this.labelLessonType.Name = "labelLessonType";
            this.labelLessonType.Size = new System.Drawing.Size(77, 13);
            this.labelLessonType.TabIndex = 6;
            this.labelLessonType.Text = "Тип занятия*:";
            // 
            // buttonGetFile
            // 
            this.buttonGetFile.Enabled = false;
            this.buttonGetFile.Location = new System.Drawing.Point(196, 304);
            this.buttonGetFile.Name = "buttonGetFile";
            this.buttonGetFile.Size = new System.Drawing.Size(113, 23);
            this.buttonGetFile.TabIndex = 17;
            this.buttonGetFile.Text = "Получить файл";
            // 
            // tabPageRecords
            // 
            this.tabPageRecords.Location = new System.Drawing.Point(4, 22);
            this.tabPageRecords.Name = "tabPageRecords";
            this.tabPageRecords.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRecords.Size = new System.Drawing.Size(926, 415);
            this.tabPageRecords.TabIndex = 1;
            this.tabPageRecords.Text = "Задания";
            this.tabPageRecords.UseVisualStyleBackColor = true;
            // 
            // comboBoxAcademicYear
            // 
            this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicYear.Enabled = false;
            this.comboBoxAcademicYear.FormattingEnabled = true;
            this.comboBoxAcademicYear.Location = new System.Drawing.Point(127, 9);
            this.comboBoxAcademicYear.Name = "comboBoxAcademicYear";
            this.comboBoxAcademicYear.Size = new System.Drawing.Size(210, 21);
            this.comboBoxAcademicYear.TabIndex = 1;
            // 
            // labelAcademicYear
            // 
            this.labelAcademicYear.AutoSize = true;
            this.labelAcademicYear.Location = new System.Drawing.Point(8, 12);
            this.labelAcademicYear.Name = "labelAcademicYear";
            this.labelAcademicYear.Size = new System.Drawing.Size(79, 13);
            this.labelAcademicYear.TabIndex = 0;
            this.labelAcademicYear.Text = "Учебный год*:";
            // 
            // comboBoxSemester
            // 
            this.comboBoxSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSemester.FormattingEnabled = true;
            this.comboBoxSemester.Location = new System.Drawing.Point(127, 63);
            this.comboBoxSemester.Name = "comboBoxSemester";
            this.comboBoxSemester.Size = new System.Drawing.Size(210, 21);
            this.comboBoxSemester.TabIndex = 5;
            // 
            // labelSemester
            // 
            this.labelSemester.AutoSize = true;
            this.labelSemester.Location = new System.Drawing.Point(8, 66);
            this.labelSemester.Name = "labelSemester";
            this.labelSemester.Size = new System.Drawing.Size(58, 13);
            this.labelSemester.TabIndex = 4;
            this.labelSemester.Text = "Семестр*:";
            // 
            // DisciplineLessonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 441);
            this.Controls.Add(this.tabControl);
            this.Name = "DisciplineLessonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Занятие";
            this.Load += new System.EventHandler(this.DisciplineLessonForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TabPage tabPageRecords;
        private System.Windows.Forms.Button buttonGetFile;
        private System.Windows.Forms.ComboBox comboBoxLessonType;
        private System.Windows.Forms.Label labelLessonType;
        private System.Windows.Forms.TextBox textBoxOrder;
        private System.Windows.Forms.Label labelOrder;
        private System.Windows.Forms.TextBox textBoxCountOfPairs;
        private System.Windows.Forms.Label labelCountOfPairs;
        private System.Windows.Forms.ComboBox comboBoxAcademicYear;
        private System.Windows.Forms.Label labelAcademicYear;
        private System.Windows.Forms.ComboBox comboBoxSemester;
        private System.Windows.Forms.Label labelSemester;
    }
}