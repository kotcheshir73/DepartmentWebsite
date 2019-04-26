namespace AcademicYearControlsAndForms.LecturerWorkload
{
    partial class FormLecturerWorkload
    {
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
            this.comboBoxAcademicYear = new System.Windows.Forms.ComboBox();
            this.labelAcademicYear = new System.Windows.Forms.Label();
            this.comboBoxLecturer = new System.Windows.Forms.ComboBox();
            this.labelLecturer = new System.Windows.Forms.Label();
            this.textBoxWorkload = new System.Windows.Forms.TextBox();
            this.labelWorkload = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.textBoxWorkload);
            this.panelMain.Controls.Add(this.labelAcademicYear);
            this.panelMain.Controls.Add(this.labelWorkload);
            this.panelMain.Controls.Add(this.comboBoxAcademicYear);
            this.panelMain.Controls.Add(this.comboBoxLecturer);
            this.panelMain.Controls.Add(this.labelLecturer);
            this.panelMain.Size = new System.Drawing.Size(384, 95);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(384, 36);
            // 
            // comboBoxAcademicYear
            // 
            this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicYear.Enabled = false;
            this.comboBoxAcademicYear.FormattingEnabled = true;
            this.comboBoxAcademicYear.Location = new System.Drawing.Point(111, 10);
            this.comboBoxAcademicYear.Name = "comboBoxAcademicYear";
            this.comboBoxAcademicYear.Size = new System.Drawing.Size(220, 21);
            this.comboBoxAcademicYear.TabIndex = 1;
            // 
            // labelAcademicYear
            // 
            this.labelAcademicYear.AutoSize = true;
            this.labelAcademicYear.Location = new System.Drawing.Point(12, 13);
            this.labelAcademicYear.Name = "labelAcademicYear";
            this.labelAcademicYear.Size = new System.Drawing.Size(79, 13);
            this.labelAcademicYear.TabIndex = 0;
            this.labelAcademicYear.Text = "Учебный год*:";
            // 
            // comboBoxLecturer
            // 
            this.comboBoxLecturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLecturer.FormattingEnabled = true;
            this.comboBoxLecturer.Location = new System.Drawing.Point(111, 37);
            this.comboBoxLecturer.Name = "comboBoxLecturer";
            this.comboBoxLecturer.Size = new System.Drawing.Size(220, 21);
            this.comboBoxLecturer.TabIndex = 3;
            // 
            // labelLecturer
            // 
            this.labelLecturer.AutoSize = true;
            this.labelLecturer.Location = new System.Drawing.Point(12, 40);
            this.labelLecturer.Name = "labelLecturer";
            this.labelLecturer.Size = new System.Drawing.Size(93, 13);
            this.labelLecturer.TabIndex = 2;
            this.labelLecturer.Text = "Преподаватель*:";
            // 
            // textBoxWorkload
            // 
            this.textBoxWorkload.Location = new System.Drawing.Point(111, 64);
            this.textBoxWorkload.Name = "textBoxWorkload";
            this.textBoxWorkload.Size = new System.Drawing.Size(80, 20);
            this.textBoxWorkload.TabIndex = 5;
            // 
            // labelWorkload
            // 
            this.labelWorkload.AutoSize = true;
            this.labelWorkload.Location = new System.Drawing.Point(12, 67);
            this.labelWorkload.Name = "labelWorkload";
            this.labelWorkload.Size = new System.Drawing.Size(62, 13);
            this.labelWorkload.TabIndex = 4;
            this.labelWorkload.Text = "Нагрузка*:";
            // 
            // FormLecturerWorkload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 131);
            this.Name = "FormLecturerWorkload";
            this.Text = "Нагрузка преподавателю";
            this.Load += new System.EventHandler(this.FormLecturerWorkload_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxAcademicYear;
        private System.Windows.Forms.Label labelAcademicYear;
        private System.Windows.Forms.ComboBox comboBoxLecturer;
        private System.Windows.Forms.Label labelLecturer;
        private System.Windows.Forms.TextBox textBoxWorkload;
        private System.Windows.Forms.Label labelWorkload;
    }
}