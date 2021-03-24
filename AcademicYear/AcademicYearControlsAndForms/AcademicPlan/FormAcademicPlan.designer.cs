namespace AcademicYearControlsAndForms.AcademicPlan
{
	partial class FormAcademicPlan
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
			this.labelAcademicYear = new System.Windows.Forms.Label();
			this.labelAcademicCourse = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageConfig = new System.Windows.Forms.TabPage();
			this.comboBoxAcademicYear = new System.Windows.Forms.ComboBox();
			this.comboBoxEducationDirection = new System.Windows.Forms.ComboBox();
			this.labelEducationDirection = new System.Windows.Forms.Label();
			this.tabPageRecords = new System.Windows.Forms.TabPage();
			this.panelMain.SuspendLayout();
			this.panelTop.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPageConfig.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelMain
			// 
			this.panelMain.Controls.Add(this.tabControl);
			this.panelMain.Size = new System.Drawing.Size(1284, 725);
			// 
			// panelTop
			// 
			this.panelTop.Size = new System.Drawing.Size(1284, 36);
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
			// labelAcademicCourse
			// 
			this.labelAcademicCourse.AutoSize = true;
			this.labelAcademicCourse.Location = new System.Drawing.Point(12, 65);
			this.labelAcademicCourse.Name = "labelAcademicCourse";
			this.labelAcademicCourse.Size = new System.Drawing.Size(42, 13);
			this.labelAcademicCourse.TabIndex = 6;
			this.labelAcademicCourse.Text = "Курсы:";
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(100, 64);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(32, 17);
			this.checkBox1.TabIndex = 7;
			this.checkBox1.Text = "1";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(138, 64);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(32, 17);
			this.checkBox2.TabIndex = 8;
			this.checkBox2.Text = "2";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.Location = new System.Drawing.Point(176, 64);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(32, 17);
			this.checkBox3.TabIndex = 9;
			this.checkBox3.Text = "3";
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// checkBox4
			// 
			this.checkBox4.AutoSize = true;
			this.checkBox4.Location = new System.Drawing.Point(214, 64);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(32, 17);
			this.checkBox4.TabIndex = 10;
			this.checkBox4.Text = "4";
			this.checkBox4.UseVisualStyleBackColor = true;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageConfig);
			this.tabControl.Controls.Add(this.tabPageRecords);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(1284, 725);
			this.tabControl.TabIndex = 0;
			// 
			// tabPageConfig
			// 
			this.tabPageConfig.Controls.Add(this.comboBoxAcademicYear);
			this.tabPageConfig.Controls.Add(this.comboBoxEducationDirection);
			this.tabPageConfig.Controls.Add(this.labelEducationDirection);
			this.tabPageConfig.Controls.Add(this.labelAcademicYear);
			this.tabPageConfig.Controls.Add(this.labelAcademicCourse);
			this.tabPageConfig.Controls.Add(this.checkBox4);
			this.tabPageConfig.Controls.Add(this.checkBox1);
			this.tabPageConfig.Controls.Add(this.checkBox3);
			this.tabPageConfig.Controls.Add(this.checkBox2);
			this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
			this.tabPageConfig.Name = "tabPageConfig";
			this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageConfig.Size = new System.Drawing.Size(1276, 699);
			this.tabPageConfig.TabIndex = 0;
			this.tabPageConfig.Text = "Учебный план";
			this.tabPageConfig.UseVisualStyleBackColor = true;
			// 
			// comboBoxAcademicYear
			// 
			this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAcademicYear.Enabled = false;
			this.comboBoxAcademicYear.FormattingEnabled = true;
			this.comboBoxAcademicYear.Location = new System.Drawing.Point(100, 10);
			this.comboBoxAcademicYear.Name = "comboBoxAcademicYear";
			this.comboBoxAcademicYear.Size = new System.Drawing.Size(222, 21);
			this.comboBoxAcademicYear.TabIndex = 1;
			// 
			// comboBoxEducationDirection
			// 
			this.comboBoxEducationDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEducationDirection.FormattingEnabled = true;
			this.comboBoxEducationDirection.Location = new System.Drawing.Point(100, 37);
			this.comboBoxEducationDirection.Name = "comboBoxEducationDirection";
			this.comboBoxEducationDirection.Size = new System.Drawing.Size(222, 21);
			this.comboBoxEducationDirection.TabIndex = 3;
			// 
			// labelEducationDirection
			// 
			this.labelEducationDirection.AutoSize = true;
			this.labelEducationDirection.Location = new System.Drawing.Point(12, 40);
			this.labelEducationDirection.Name = "labelEducationDirection";
			this.labelEducationDirection.Size = new System.Drawing.Size(78, 13);
			this.labelEducationDirection.TabIndex = 2;
			this.labelEducationDirection.Text = "Направление:";
			// 
			// tabPageRecords
			// 
			this.tabPageRecords.Location = new System.Drawing.Point(4, 22);
			this.tabPageRecords.Name = "tabPageRecords";
			this.tabPageRecords.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageRecords.Size = new System.Drawing.Size(311, 148);
			this.tabPageRecords.TabIndex = 1;
			this.tabPageRecords.Text = "Записи";
			this.tabPageRecords.UseVisualStyleBackColor = true;
			// 
			// FormAcademicPlan
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1284, 761);
			this.Name = "FormAcademicPlan";
			this.Text = "Учебный план";
			this.panelMain.ResumeLayout(false);
			this.panelTop.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.tabPageConfig.ResumeLayout(false);
			this.tabPageConfig.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelAcademicYear;
		private System.Windows.Forms.Label labelAcademicCourse;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageConfig;
		private System.Windows.Forms.TabPage tabPageRecords;
		private System.Windows.Forms.ComboBox comboBoxAcademicYear;
        private System.Windows.Forms.ComboBox comboBoxEducationDirection;
        private System.Windows.Forms.Label labelEducationDirection;
    }
}