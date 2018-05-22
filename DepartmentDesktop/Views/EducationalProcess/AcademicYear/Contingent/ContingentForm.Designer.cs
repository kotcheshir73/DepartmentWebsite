namespace DepartmentDesktop.Views.EducationalProcess.Contingent
{
	partial class ContingentForm
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
            this.comboBoxAcademicYear = new System.Windows.Forms.ComboBox();
            this.labelAcademicYear = new System.Windows.Forms.Label();
            this.comboBoxEducationDirection = new System.Windows.Forms.ComboBox();
            this.labelEducationDirection = new System.Windows.Forms.Label();
            this.textBoxCountSubgroups = new System.Windows.Forms.TextBox();
            this.labelCountSubgroups = new System.Windows.Forms.Label();
            this.labelCountStudents = new System.Windows.Forms.Label();
            this.textBoxCountStudents = new System.Windows.Forms.TextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.labelCourse = new System.Windows.Forms.Label();
            this.textBoxCourse = new System.Windows.Forms.TextBox();
            this.labelCountGroups = new System.Windows.Forms.Label();
            this.textBoxCountGroups = new System.Windows.Forms.TextBox();
            this.textBoxContingentName = new System.Windows.Forms.TextBox();
            this.labelContingentName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxAcademicYear
            // 
            this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicYear.Enabled = false;
            this.comboBoxAcademicYear.FormattingEnabled = true;
            this.comboBoxAcademicYear.Location = new System.Drawing.Point(108, 6);
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
            // comboBoxEducationDirection
            // 
            this.comboBoxEducationDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEducationDirection.FormattingEnabled = true;
            this.comboBoxEducationDirection.Location = new System.Drawing.Point(108, 33);
            this.comboBoxEducationDirection.Name = "comboBoxEducationDirection";
            this.comboBoxEducationDirection.Size = new System.Drawing.Size(220, 21);
            this.comboBoxEducationDirection.TabIndex = 3;
            // 
            // labelEducationDirection
            // 
            this.labelEducationDirection.AutoSize = true;
            this.labelEducationDirection.Location = new System.Drawing.Point(12, 36);
            this.labelEducationDirection.Name = "labelEducationDirection";
            this.labelEducationDirection.Size = new System.Drawing.Size(82, 13);
            this.labelEducationDirection.TabIndex = 2;
            this.labelEducationDirection.Text = "Направление*:";
            // 
            // textBoxCountSubgroups
            // 
            this.textBoxCountSubgroups.Location = new System.Drawing.Point(278, 145);
            this.textBoxCountSubgroups.MaxLength = 2;
            this.textBoxCountSubgroups.Name = "textBoxCountSubgroups";
            this.textBoxCountSubgroups.Size = new System.Drawing.Size(50, 20);
            this.textBoxCountSubgroups.TabIndex = 13;
            this.textBoxCountSubgroups.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelCountSubgroups
            // 
            this.labelCountSubgroups.AutoSize = true;
            this.labelCountSubgroups.Location = new System.Drawing.Point(150, 148);
            this.labelCountSubgroups.Name = "labelCountSubgroups";
            this.labelCountSubgroups.Size = new System.Drawing.Size(122, 13);
            this.labelCountSubgroups.TabIndex = 12;
            this.labelCountSubgroups.Text = "Количество подгрупп*:";
            // 
            // labelCountStudents
            // 
            this.labelCountStudents.AutoSize = true;
            this.labelCountStudents.Location = new System.Drawing.Point(145, 122);
            this.labelCountStudents.Name = "labelCountStudents";
            this.labelCountStudents.Size = new System.Drawing.Size(127, 13);
            this.labelCountStudents.TabIndex = 10;
            this.labelCountStudents.Text = "Количество студентов*:";
            // 
            // textBoxCountStudents
            // 
            this.textBoxCountStudents.Location = new System.Drawing.Point(278, 119);
            this.textBoxCountStudents.MaxLength = 3;
            this.textBoxCountStudents.Name = "textBoxCountStudents";
            this.textBoxCountStudents.Size = new System.Drawing.Size(50, 20);
            this.textBoxCountStudents.TabIndex = 11;
            this.textBoxCountStudents.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(247, 171);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 16;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(19, 171);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 14;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(100, 171);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 15;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // labelCourse
            // 
            this.labelCourse.AutoSize = true;
            this.labelCourse.Location = new System.Drawing.Point(12, 96);
            this.labelCourse.Name = "labelCourse";
            this.labelCourse.Size = new System.Drawing.Size(38, 13);
            this.labelCourse.TabIndex = 6;
            this.labelCourse.Text = "Курс*:";
            // 
            // textBoxCourse
            // 
            this.textBoxCourse.Location = new System.Drawing.Point(56, 93);
            this.textBoxCourse.MaxLength = 3;
            this.textBoxCourse.Name = "textBoxCourse";
            this.textBoxCourse.Size = new System.Drawing.Size(50, 20);
            this.textBoxCourse.TabIndex = 7;
            this.textBoxCourse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelCountGroups
            // 
            this.labelCountGroups.AutoSize = true;
            this.labelCountGroups.Location = new System.Drawing.Point(168, 96);
            this.labelCountGroups.Name = "labelCountGroups";
            this.labelCountGroups.Size = new System.Drawing.Size(104, 13);
            this.labelCountGroups.TabIndex = 8;
            this.labelCountGroups.Text = "Количество групп*:";
            // 
            // textBoxCountGroups
            // 
            this.textBoxCountGroups.Location = new System.Drawing.Point(278, 93);
            this.textBoxCountGroups.MaxLength = 3;
            this.textBoxCountGroups.Name = "textBoxCountGroups";
            this.textBoxCountGroups.Size = new System.Drawing.Size(50, 20);
            this.textBoxCountGroups.TabIndex = 9;
            this.textBoxCountGroups.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxContingentName
            // 
            this.textBoxContingentName.Location = new System.Drawing.Point(108, 60);
            this.textBoxContingentName.MaxLength = 100;
            this.textBoxContingentName.Name = "textBoxContingentName";
            this.textBoxContingentName.Size = new System.Drawing.Size(220, 20);
            this.textBoxContingentName.TabIndex = 5;
            // 
            // labelContingentName
            // 
            this.labelContingentName.AutoSize = true;
            this.labelContingentName.Location = new System.Drawing.Point(12, 63);
            this.labelContingentName.Name = "labelContingentName";
            this.labelContingentName.Size = new System.Drawing.Size(90, 13);
            this.labelContingentName.TabIndex = 4;
            this.labelContingentName.Text = "Наименование*:";
            // 
            // ContingentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 202);
            this.Controls.Add(this.textBoxContingentName);
            this.Controls.Add(this.labelContingentName);
            this.Controls.Add(this.labelCountGroups);
            this.Controls.Add(this.textBoxCountGroups);
            this.Controls.Add(this.textBoxCourse);
            this.Controls.Add(this.labelCourse);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxCountSubgroups);
            this.Controls.Add(this.labelCountSubgroups);
            this.Controls.Add(this.labelCountStudents);
            this.Controls.Add(this.textBoxCountStudents);
            this.Controls.Add(this.comboBoxEducationDirection);
            this.Controls.Add(this.labelEducationDirection);
            this.Controls.Add(this.comboBoxAcademicYear);
            this.Controls.Add(this.labelAcademicYear);
            this.Name = "ContingentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Контингент";
            this.Load += new System.EventHandler(this.ContingentForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxAcademicYear;
		private System.Windows.Forms.Label labelAcademicYear;
		private System.Windows.Forms.ComboBox comboBoxEducationDirection;
		private System.Windows.Forms.Label labelEducationDirection;
		private System.Windows.Forms.TextBox textBoxCountSubgroups;
		private System.Windows.Forms.Label labelCountSubgroups;
		private System.Windows.Forms.Label labelCountStudents;
		private System.Windows.Forms.TextBox textBoxCountStudents;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonSaveAndClose;
		private System.Windows.Forms.Label labelCourse;
		private System.Windows.Forms.TextBox textBoxCourse;
        private System.Windows.Forms.Label labelCountGroups;
        private System.Windows.Forms.TextBox textBoxCountGroups;
        private System.Windows.Forms.TextBox textBoxContingentName;
        private System.Windows.Forms.Label labelContingentName;
    }
}