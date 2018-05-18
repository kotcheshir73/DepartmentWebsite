namespace DepartmentDesktop.Views.EducationalProcess.AcademicYear.StreamLesson
{
    partial class StreamLessonForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.textBoxStreamLessonHours = new System.Windows.Forms.TextBox();
            this.labelStreamLessonHours = new System.Windows.Forms.Label();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxStreamLessonName = new System.Windows.Forms.TextBox();
            this.labelStreamLessonName = new System.Windows.Forms.Label();
            this.comboBoxAcademicYear = new System.Windows.Forms.ComboBox();
            this.labelAcademicYear = new System.Windows.Forms.Label();
            this.tabPageRecords = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageRecords);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(734, 481);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.textBoxStreamLessonHours);
            this.tabPageConfig.Controls.Add(this.labelStreamLessonHours);
            this.tabPageConfig.Controls.Add(this.buttonSaveAndClose);
            this.tabPageConfig.Controls.Add(this.buttonClose);
            this.tabPageConfig.Controls.Add(this.buttonSave);
            this.tabPageConfig.Controls.Add(this.textBoxStreamLessonName);
            this.tabPageConfig.Controls.Add(this.labelStreamLessonName);
            this.tabPageConfig.Controls.Add(this.comboBoxAcademicYear);
            this.tabPageConfig.Controls.Add(this.labelAcademicYear);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(726, 455);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Информация по потоку";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // textBoxStreamLessonHours
            // 
            this.textBoxStreamLessonHours.Location = new System.Drawing.Point(106, 66);
            this.textBoxStreamLessonHours.Name = "textBoxStreamLessonHours";
            this.textBoxStreamLessonHours.Size = new System.Drawing.Size(100, 20);
            this.textBoxStreamLessonHours.TabIndex = 5;
            // 
            // labelStreamLessonHours
            // 
            this.labelStreamLessonHours.AutoSize = true;
            this.labelStreamLessonHours.Location = new System.Drawing.Point(18, 66);
            this.labelStreamLessonHours.Name = "labelStreamLessonHours";
            this.labelStreamLessonHours.Size = new System.Drawing.Size(42, 13);
            this.labelStreamLessonHours.TabIndex = 4;
            this.labelStreamLessonHours.Text = "Часы*:";
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(106, 98);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 7;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(253, 98);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 8;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(25, 98);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxStreamLessonName
            // 
            this.textBoxStreamLessonName.Location = new System.Drawing.Point(106, 40);
            this.textBoxStreamLessonName.Name = "textBoxStreamLessonName";
            this.textBoxStreamLessonName.Size = new System.Drawing.Size(222, 20);
            this.textBoxStreamLessonName.TabIndex = 3;
            // 
            // labelStreamLessonName
            // 
            this.labelStreamLessonName.AutoSize = true;
            this.labelStreamLessonName.Location = new System.Drawing.Point(18, 43);
            this.labelStreamLessonName.Name = "labelStreamLessonName";
            this.labelStreamLessonName.Size = new System.Drawing.Size(64, 13);
            this.labelStreamLessonName.TabIndex = 2;
            this.labelStreamLessonName.Text = "Название:*";
            // 
            // comboBoxAcademicYear
            // 
            this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicYear.Enabled = false;
            this.comboBoxAcademicYear.FormattingEnabled = true;
            this.comboBoxAcademicYear.Location = new System.Drawing.Point(106, 13);
            this.comboBoxAcademicYear.Name = "comboBoxAcademicYear";
            this.comboBoxAcademicYear.Size = new System.Drawing.Size(222, 21);
            this.comboBoxAcademicYear.TabIndex = 1;
            // 
            // labelAcademicYear
            // 
            this.labelAcademicYear.AutoSize = true;
            this.labelAcademicYear.Location = new System.Drawing.Point(18, 16);
            this.labelAcademicYear.Name = "labelAcademicYear";
            this.labelAcademicYear.Size = new System.Drawing.Size(79, 13);
            this.labelAcademicYear.TabIndex = 0;
            this.labelAcademicYear.Text = "Учебный год*:";
            // 
            // tabPageRecords
            // 
            this.tabPageRecords.Location = new System.Drawing.Point(4, 22);
            this.tabPageRecords.Name = "tabPageRecords";
            this.tabPageRecords.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRecords.Size = new System.Drawing.Size(726, 455);
            this.tabPageRecords.TabIndex = 1;
            this.tabPageRecords.Text = "Записи";
            this.tabPageRecords.UseVisualStyleBackColor = true;
            // 
            // StreamLessonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 481);
            this.Controls.Add(this.tabControl);
            this.Name = "StreamLessonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Поток";
            this.Load += new System.EventHandler(this.StreamLessonForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TabPage tabPageRecords;
        private System.Windows.Forms.ComboBox comboBoxAcademicYear;
        private System.Windows.Forms.Label labelAcademicYear;
        private System.Windows.Forms.TextBox textBoxStreamLessonName;
        private System.Windows.Forms.Label labelStreamLessonName;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxStreamLessonHours;
        private System.Windows.Forms.Label labelStreamLessonHours;
    }
}