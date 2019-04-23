namespace LearningProgressControlsAndForms.DisciplineStudentRecord
{
    partial class FormDisciplineStudentRecord
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
            this.comboBoxStudentGroup = new System.Windows.Forms.ComboBox();
            this.labelStudentGroup = new System.Windows.Forms.Label();
            this.comboBoxStudent = new System.Windows.Forms.ComboBox();
            this.labelStudent = new System.Windows.Forms.Label();
            this.comboBoxDiscipline = new System.Windows.Forms.ComboBox();
            this.labelDiscipline = new System.Windows.Forms.Label();
            this.comboBoxSemester = new System.Windows.Forms.ComboBox();
            this.labelSemester = new System.Windows.Forms.Label();
            this.labelVariant = new System.Windows.Forms.Label();
            this.textBoxVariant = new System.Windows.Forms.TextBox();
            this.labelSubgroup = new System.Windows.Forms.Label();
            this.textBoxSubgroup = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboBoxStudentGroup
            // 
            this.comboBoxStudentGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudentGroup.Enabled = false;
            this.comboBoxStudentGroup.FormattingEnabled = true;
            this.comboBoxStudentGroup.Location = new System.Drawing.Point(131, 6);
            this.comboBoxStudentGroup.Name = "comboBoxStudentGroup";
            this.comboBoxStudentGroup.Size = new System.Drawing.Size(210, 21);
            this.comboBoxStudentGroup.TabIndex = 1;
            // 
            // labelStudentGroup
            // 
            this.labelStudentGroup.AutoSize = true;
            this.labelStudentGroup.Location = new System.Drawing.Point(12, 9);
            this.labelStudentGroup.Name = "labelStudentGroup";
            this.labelStudentGroup.Size = new System.Drawing.Size(94, 13);
            this.labelStudentGroup.TabIndex = 0;
            this.labelStudentGroup.Text = "Учебная группа*:";
            // 
            // comboBoxStudent
            // 
            this.comboBoxStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudent.FormattingEnabled = true;
            this.comboBoxStudent.Location = new System.Drawing.Point(131, 87);
            this.comboBoxStudent.Name = "comboBoxStudent";
            this.comboBoxStudent.Size = new System.Drawing.Size(210, 21);
            this.comboBoxStudent.TabIndex = 7;
            // 
            // labelStudent
            // 
            this.labelStudent.AutoSize = true;
            this.labelStudent.Location = new System.Drawing.Point(12, 90);
            this.labelStudent.Name = "labelStudent";
            this.labelStudent.Size = new System.Drawing.Size(54, 13);
            this.labelStudent.TabIndex = 6;
            this.labelStudent.Text = "Студент*:";
            // 
            // comboBoxDiscipline
            // 
            this.comboBoxDiscipline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiscipline.Enabled = false;
            this.comboBoxDiscipline.FormattingEnabled = true;
            this.comboBoxDiscipline.Location = new System.Drawing.Point(131, 33);
            this.comboBoxDiscipline.Name = "comboBoxDiscipline";
            this.comboBoxDiscipline.Size = new System.Drawing.Size(210, 21);
            this.comboBoxDiscipline.TabIndex = 3;
            // 
            // labelDiscipline
            // 
            this.labelDiscipline.AutoSize = true;
            this.labelDiscipline.Location = new System.Drawing.Point(12, 36);
            this.labelDiscipline.Name = "labelDiscipline";
            this.labelDiscipline.Size = new System.Drawing.Size(77, 13);
            this.labelDiscipline.TabIndex = 2;
            this.labelDiscipline.Text = "Дисциплина*:";
            // 
            // comboBoxSemester
            // 
            this.comboBoxSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSemester.Enabled = false;
            this.comboBoxSemester.FormattingEnabled = true;
            this.comboBoxSemester.Location = new System.Drawing.Point(131, 60);
            this.comboBoxSemester.Name = "comboBoxSemester";
            this.comboBoxSemester.Size = new System.Drawing.Size(210, 21);
            this.comboBoxSemester.TabIndex = 5;
            // 
            // labelSemester
            // 
            this.labelSemester.AutoSize = true;
            this.labelSemester.Location = new System.Drawing.Point(12, 63);
            this.labelSemester.Name = "labelSemester";
            this.labelSemester.Size = new System.Drawing.Size(58, 13);
            this.labelSemester.TabIndex = 4;
            this.labelSemester.Text = "Семестр*:";
            // 
            // labelVariant
            // 
            this.labelVariant.AutoSize = true;
            this.labelVariant.Location = new System.Drawing.Point(14, 117);
            this.labelVariant.Name = "labelVariant";
            this.labelVariant.Size = new System.Drawing.Size(56, 13);
            this.labelVariant.TabIndex = 8;
            this.labelVariant.Text = "Вариант*:";
            // 
            // textBoxVariant
            // 
            this.textBoxVariant.Location = new System.Drawing.Point(131, 114);
            this.textBoxVariant.Name = "textBoxVariant";
            this.textBoxVariant.Size = new System.Drawing.Size(210, 20);
            this.textBoxVariant.TabIndex = 9;
            // 
            // labelSubgroup
            // 
            this.labelSubgroup.AutoSize = true;
            this.labelSubgroup.Location = new System.Drawing.Point(14, 143);
            this.labelSubgroup.Name = "labelSubgroup";
            this.labelSubgroup.Size = new System.Drawing.Size(68, 13);
            this.labelSubgroup.TabIndex = 10;
            this.labelSubgroup.Text = "Подгруппа*:";
            // 
            // textBoxSubgroup
            // 
            this.textBoxSubgroup.Location = new System.Drawing.Point(131, 140);
            this.textBoxSubgroup.Name = "textBoxSubgroup";
            this.textBoxSubgroup.Size = new System.Drawing.Size(210, 20);
            this.textBoxSubgroup.TabIndex = 11;
            // 
            // DisciplineStudentRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 221);
            this.Controls.Add(this.labelSubgroup);
            this.Controls.Add(this.textBoxSubgroup);
            this.Controls.Add(this.labelVariant);
            this.Controls.Add(this.textBoxVariant);
            this.Controls.Add(this.comboBoxSemester);
            this.Controls.Add(this.labelSemester);
            this.Controls.Add(this.comboBoxStudentGroup);
            this.Controls.Add(this.labelStudentGroup);
            this.Controls.Add(this.comboBoxStudent);
            this.Controls.Add(this.labelStudent);
            this.Controls.Add(this.comboBoxDiscipline);
            this.Controls.Add(this.labelDiscipline);
            this.Name = "DisciplineStudentRecordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Связь студента с дисциплиной";
            this.Load += new System.EventHandler(this.FormDisciplineStudentRecord_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxStudentGroup;
        private System.Windows.Forms.Label labelStudentGroup;
        private System.Windows.Forms.ComboBox comboBoxStudent;
        private System.Windows.Forms.Label labelStudent;
        private System.Windows.Forms.ComboBox comboBoxDiscipline;
        private System.Windows.Forms.Label labelDiscipline;
        private System.Windows.Forms.ComboBox comboBoxSemester;
        private System.Windows.Forms.Label labelSemester;
        private System.Windows.Forms.Label labelVariant;
        private System.Windows.Forms.TextBox textBoxVariant;
        private System.Windows.Forms.Label labelSubgroup;
        private System.Windows.Forms.TextBox textBoxSubgroup;
    }
}