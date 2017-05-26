namespace DepartmentDesktop.Views.EducationalProcess.AcademicPlan
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
			this.SuspendLayout();
			// 
			// comboBoxAcademicYear
			// 
			this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAcademicYear.FormattingEnabled = true;
			this.comboBoxAcademicYear.Location = new System.Drawing.Point(102, 6);
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
			this.comboBoxEducationDirection.Location = new System.Drawing.Point(102, 33);
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
			this.textBoxCountSubgroups.Location = new System.Drawing.Point(272, 86);
			this.textBoxCountSubgroups.MaxLength = 2;
			this.textBoxCountSubgroups.Name = "textBoxCountSubgroups";
			this.textBoxCountSubgroups.Size = new System.Drawing.Size(50, 20);
			this.textBoxCountSubgroups.TabIndex = 9;
			this.textBoxCountSubgroups.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// labelCountSubgroups
			// 
			this.labelCountSubgroups.AutoSize = true;
			this.labelCountSubgroups.Location = new System.Drawing.Point(144, 89);
			this.labelCountSubgroups.Name = "labelCountSubgroups";
			this.labelCountSubgroups.Size = new System.Drawing.Size(122, 13);
			this.labelCountSubgroups.TabIndex = 8;
			this.labelCountSubgroups.Text = "Количество подгрупп*:";
			// 
			// labelCountStudents
			// 
			this.labelCountStudents.AutoSize = true;
			this.labelCountStudents.Location = new System.Drawing.Point(139, 63);
			this.labelCountStudents.Name = "labelCountStudents";
			this.labelCountStudents.Size = new System.Drawing.Size(127, 13);
			this.labelCountStudents.TabIndex = 6;
			this.labelCountStudents.Text = "Количество студентов*:";
			// 
			// textBoxCountStudents
			// 
			this.textBoxCountStudents.Location = new System.Drawing.Point(272, 60);
			this.textBoxCountStudents.MaxLength = 3;
			this.textBoxCountStudents.Name = "textBoxCountStudents";
			this.textBoxCountStudents.Size = new System.Drawing.Size(50, 20);
			this.textBoxCountStudents.TabIndex = 7;
			this.textBoxCountStudents.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(247, 112);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 12;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(19, 112);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 10;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonSaveAndClose
			// 
			this.buttonSaveAndClose.Location = new System.Drawing.Point(100, 112);
			this.buttonSaveAndClose.Name = "buttonSaveAndClose";
			this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
			this.buttonSaveAndClose.TabIndex = 11;
			this.buttonSaveAndClose.Text = "Сохранить и закрыть";
			this.buttonSaveAndClose.UseVisualStyleBackColor = true;
			this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
			// 
			// labelCourse
			// 
			this.labelCourse.AutoSize = true;
			this.labelCourse.Location = new System.Drawing.Point(12, 63);
			this.labelCourse.Name = "labelCourse";
			this.labelCourse.Size = new System.Drawing.Size(38, 13);
			this.labelCourse.TabIndex = 4;
			this.labelCourse.Text = "Курс*:";
			// 
			// textBoxCourse
			// 
			this.textBoxCourse.Location = new System.Drawing.Point(56, 60);
			this.textBoxCourse.MaxLength = 3;
			this.textBoxCourse.Name = "textBoxCourse";
			this.textBoxCourse.Size = new System.Drawing.Size(50, 20);
			this.textBoxCourse.TabIndex = 5;
			this.textBoxCourse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// ContingentForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(334, 142);
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
	}
}