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
            this.labelAcademicLevel = new System.Windows.Forms.Label();
            this.comboBoxAcademicLevel = new System.Windows.Forms.ComboBox();
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
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(322, 470);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(469, 470);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(241, 470);
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
            // labelAcademicLevel
            // 
            this.labelAcademicLevel.AutoSize = true;
            this.labelAcademicLevel.Location = new System.Drawing.Point(12, 67);
            this.labelAcademicLevel.Name = "labelAcademicLevel";
            this.labelAcademicLevel.Size = new System.Drawing.Size(58, 13);
            this.labelAcademicLevel.TabIndex = 4;
            this.labelAcademicLevel.Text = "Уровень*:";
            // 
            // comboBoxAcademicLevel
            // 
            this.comboBoxAcademicLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicLevel.FormattingEnabled = true;
            this.comboBoxAcademicLevel.Location = new System.Drawing.Point(100, 64);
            this.comboBoxAcademicLevel.Name = "comboBoxAcademicLevel";
            this.comboBoxAcademicLevel.Size = new System.Drawing.Size(222, 21);
            this.comboBoxAcademicLevel.TabIndex = 5;
            // 
            // labelAcademicCourse
            // 
            this.labelAcademicCourse.AutoSize = true;
            this.labelAcademicCourse.Location = new System.Drawing.Point(12, 95);
            this.labelAcademicCourse.Name = "labelAcademicCourse";
            this.labelAcademicCourse.Size = new System.Drawing.Size(42, 13);
            this.labelAcademicCourse.TabIndex = 6;
            this.labelAcademicCourse.Text = "Курсы:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(100, 94);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(32, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(138, 94);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(32, 17);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(176, 94);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(32, 17);
            this.checkBox3.TabIndex = 9;
            this.checkBox3.Text = "3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(214, 94);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(32, 17);
            this.checkBox4.TabIndex = 10;
            this.checkBox4.Text = "4";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageRecords);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(834, 464);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.comboBoxAcademicYear);
            this.tabPageConfig.Controls.Add(this.comboBoxEducationDirection);
            this.tabPageConfig.Controls.Add(this.labelEducationDirection);
            this.tabPageConfig.Controls.Add(this.labelAcademicYear);
            this.tabPageConfig.Controls.Add(this.labelAcademicLevel);
            this.tabPageConfig.Controls.Add(this.comboBoxAcademicLevel);
            this.tabPageConfig.Controls.Add(this.labelAcademicCourse);
            this.tabPageConfig.Controls.Add(this.checkBox4);
            this.tabPageConfig.Controls.Add(this.checkBox1);
            this.tabPageConfig.Controls.Add(this.checkBox3);
            this.tabPageConfig.Controls.Add(this.checkBox2);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(826, 438);
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
            this.tabPageRecords.Size = new System.Drawing.Size(826, 438);
            this.tabPageRecords.TabIndex = 1;
            this.tabPageRecords.Text = "Записи";
            this.tabPageRecords.UseVisualStyleBackColor = true;
            // 
            // FormAcademicPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 501);
            this.Controls.Add(this.tabControl);
            this.Name = "FormAcademicPlan";
            this.Text = "Учебный план";
            this.Load += new System.EventHandler(this.FormAcademicPlan_Load);
            this.Controls.SetChildIndex(this.tabControl, 0);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.buttonSaveAndClose, 0);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelAcademicYear;
		private System.Windows.Forms.Label labelAcademicLevel;
		private System.Windows.Forms.ComboBox comboBoxAcademicLevel;
		private System.Windows.Forms.Label labelAcademicCourse;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageConfig;
		private System.Windows.Forms.TabPage tabPageRecords;
		private System.Windows.Forms.ComboBox comboBoxEducationDirection;
		private System.Windows.Forms.Label labelEducationDirection;
		private System.Windows.Forms.ComboBox comboBoxAcademicYear;
	}
}