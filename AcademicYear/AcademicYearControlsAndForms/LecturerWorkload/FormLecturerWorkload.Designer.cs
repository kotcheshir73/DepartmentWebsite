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
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(125, 96);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(272, 96);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(44, 96);
            // 
            // comboBoxAcademicYear
            // 
            this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicYear.Enabled = false;
            this.comboBoxAcademicYear.FormattingEnabled = true;
            this.comboBoxAcademicYear.Location = new System.Drawing.Point(111, 6);
            this.comboBoxAcademicYear.Name = "comboBoxAcademicYear";
            this.comboBoxAcademicYear.Size = new System.Drawing.Size(220, 21);
            this.comboBoxAcademicYear.TabIndex = 1;
            // 
            // labelAcademicYear
            // 
            this.labelAcademicYear.AutoSize = true;
            this.labelAcademicYear.Location = new System.Drawing.Point(12, 9);
            this.labelAcademicYear.Name = "labelAcademicYear";
            this.labelAcademicYear.Size = new System.Drawing.Size(79, 13);
            this.labelAcademicYear.TabIndex = 0;
            this.labelAcademicYear.Text = "Учебный год*:";
            // 
            // comboBoxLecturer
            // 
            this.comboBoxLecturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLecturer.FormattingEnabled = true;
            this.comboBoxLecturer.Location = new System.Drawing.Point(111, 33);
            this.comboBoxLecturer.Name = "comboBoxLecturer";
            this.comboBoxLecturer.Size = new System.Drawing.Size(220, 21);
            this.comboBoxLecturer.TabIndex = 3;
            // 
            // labelLecturer
            // 
            this.labelLecturer.AutoSize = true;
            this.labelLecturer.Location = new System.Drawing.Point(12, 36);
            this.labelLecturer.Name = "labelLecturer";
            this.labelLecturer.Size = new System.Drawing.Size(93, 13);
            this.labelLecturer.TabIndex = 2;
            this.labelLecturer.Text = "Преподаватель*:";
            // 
            // textBoxWorkload
            // 
            this.textBoxWorkload.Location = new System.Drawing.Point(111, 60);
            this.textBoxWorkload.Name = "textBoxWorkload";
            this.textBoxWorkload.Size = new System.Drawing.Size(80, 20);
            this.textBoxWorkload.TabIndex = 5;
            // 
            // labelWorkload
            // 
            this.labelWorkload.AutoSize = true;
            this.labelWorkload.Location = new System.Drawing.Point(12, 63);
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
            this.Controls.Add(this.textBoxWorkload);
            this.Controls.Add(this.labelWorkload);
            this.Controls.Add(this.comboBoxLecturer);
            this.Controls.Add(this.labelLecturer);
            this.Controls.Add(this.comboBoxAcademicYear);
            this.Controls.Add(this.labelAcademicYear);
            this.Name = "FormLecturerWorkload";
            this.Text = "Нагрузка преподавателю";
            this.Load += new System.EventHandler(this.FormLecturerWorkload_Load);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.buttonSaveAndClose, 0);
            this.Controls.SetChildIndex(this.labelAcademicYear, 0);
            this.Controls.SetChildIndex(this.comboBoxAcademicYear, 0);
            this.Controls.SetChildIndex(this.labelLecturer, 0);
            this.Controls.SetChildIndex(this.comboBoxLecturer, 0);
            this.Controls.SetChildIndex(this.labelWorkload, 0);
            this.Controls.SetChildIndex(this.textBoxWorkload, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

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