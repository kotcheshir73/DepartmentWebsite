namespace AcademicYearControlsAndForms.AcademicPlanRecord
{
	partial class FormAcademicPlanRecord
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
			this.labelAcademicPlan = new System.Windows.Forms.Label();
			this.comboBoxAcademicPlan = new System.Windows.Forms.ComboBox();
			this.labelSemester = new System.Windows.Forms.Label();
			this.comboBoxSemester = new System.Windows.Forms.ComboBox();
			this.labelZet = new System.Windows.Forms.Label();
			this.textBoxZet = new System.Windows.Forms.TextBox();
			this.labelDiscipline = new System.Windows.Forms.Label();
			this.comboBoxDiscipline = new System.Windows.Forms.ComboBox();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageConfig = new System.Windows.Forms.TabPage();
			this.checkBoxIsActiveSemester = new System.Windows.Forms.CheckBox();
			this.checkBoxInDepartment = new System.Windows.Forms.CheckBox();
			this.checkBoxIsChild = new System.Windows.Forms.CheckBox();
			this.checkBoxIsUseInWorkload = new System.Windows.Forms.CheckBox();
			this.labelContingent = new System.Windows.Forms.Label();
			this.comboBoxContingent = new System.Windows.Forms.ComboBox();
			this.tabPageRecords = new System.Windows.Forms.TabPage();
			this.checkBoxIsParent = new System.Windows.Forms.CheckBox();
			this.panelMain.SuspendLayout();
			this.panelTop.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPageConfig.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelMain
			// 
			this.panelMain.Controls.Add(this.tabControl);
			this.panelMain.Size = new System.Drawing.Size(691, 331);
			// 
			// panelTop
			// 
			this.panelTop.Size = new System.Drawing.Size(691, 36);
			// 
			// labelAcademicPlan
			// 
			this.labelAcademicPlan.AutoSize = true;
			this.labelAcademicPlan.Location = new System.Drawing.Point(17, 14);
			this.labelAcademicPlan.Name = "labelAcademicPlan";
			this.labelAcademicPlan.Size = new System.Drawing.Size(86, 13);
			this.labelAcademicPlan.TabIndex = 0;
			this.labelAcademicPlan.Text = "Учебный план*:";
			// 
			// comboBoxAcademicPlan
			// 
			this.comboBoxAcademicPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAcademicPlan.Enabled = false;
			this.comboBoxAcademicPlan.FormattingEnabled = true;
			this.comboBoxAcademicPlan.Location = new System.Drawing.Point(109, 11);
			this.comboBoxAcademicPlan.Name = "comboBoxAcademicPlan";
			this.comboBoxAcademicPlan.Size = new System.Drawing.Size(220, 21);
			this.comboBoxAcademicPlan.TabIndex = 1;
			// 
			// labelSemester
			// 
			this.labelSemester.AutoSize = true;
			this.labelSemester.Location = new System.Drawing.Point(17, 94);
			this.labelSemester.Name = "labelSemester";
			this.labelSemester.Size = new System.Drawing.Size(54, 13);
			this.labelSemester.TabIndex = 6;
			this.labelSemester.Text = "Семестр:";
			// 
			// comboBoxSemester
			// 
			this.comboBoxSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSemester.FormattingEnabled = true;
			this.comboBoxSemester.Location = new System.Drawing.Point(109, 92);
			this.comboBoxSemester.Name = "comboBoxSemester";
			this.comboBoxSemester.Size = new System.Drawing.Size(220, 21);
			this.comboBoxSemester.TabIndex = 7;
			// 
			// labelZet
			// 
			this.labelZet.AutoSize = true;
			this.labelZet.Location = new System.Drawing.Point(17, 123);
			this.labelZet.Name = "labelZet";
			this.labelZet.Size = new System.Drawing.Size(32, 13);
			this.labelZet.TabIndex = 8;
			this.labelZet.Text = "Зет*:";
			// 
			// textBoxZet
			// 
			this.textBoxZet.Location = new System.Drawing.Point(109, 119);
			this.textBoxZet.MaxLength = 3;
			this.textBoxZet.Name = "textBoxZet";
			this.textBoxZet.Size = new System.Drawing.Size(100, 20);
			this.textBoxZet.TabIndex = 9;
			// 
			// labelDiscipline
			// 
			this.labelDiscipline.AutoSize = true;
			this.labelDiscipline.Location = new System.Drawing.Point(17, 41);
			this.labelDiscipline.Name = "labelDiscipline";
			this.labelDiscipline.Size = new System.Drawing.Size(77, 13);
			this.labelDiscipline.TabIndex = 2;
			this.labelDiscipline.Text = "Дисциплина*:";
			// 
			// comboBoxDiscipline
			// 
			this.comboBoxDiscipline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDiscipline.FormattingEnabled = true;
			this.comboBoxDiscipline.Location = new System.Drawing.Point(109, 38);
			this.comboBoxDiscipline.Name = "comboBoxDiscipline";
			this.comboBoxDiscipline.Size = new System.Drawing.Size(220, 21);
			this.comboBoxDiscipline.TabIndex = 3;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageConfig);
			this.tabControl.Controls.Add(this.tabPageRecords);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(691, 331);
			this.tabControl.TabIndex = 0;
			// 
			// tabPageConfig
			// 
			this.tabPageConfig.Controls.Add(this.checkBoxIsParent);
			this.tabPageConfig.Controls.Add(this.checkBoxIsActiveSemester);
			this.tabPageConfig.Controls.Add(this.checkBoxInDepartment);
			this.tabPageConfig.Controls.Add(this.checkBoxIsChild);
			this.tabPageConfig.Controls.Add(this.checkBoxIsUseInWorkload);
			this.tabPageConfig.Controls.Add(this.labelContingent);
			this.tabPageConfig.Controls.Add(this.comboBoxContingent);
			this.tabPageConfig.Controls.Add(this.labelAcademicPlan);
			this.tabPageConfig.Controls.Add(this.comboBoxAcademicPlan);
			this.tabPageConfig.Controls.Add(this.comboBoxDiscipline);
			this.tabPageConfig.Controls.Add(this.labelSemester);
			this.tabPageConfig.Controls.Add(this.labelDiscipline);
			this.tabPageConfig.Controls.Add(this.comboBoxSemester);
			this.tabPageConfig.Controls.Add(this.labelZet);
			this.tabPageConfig.Controls.Add(this.textBoxZet);
			this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
			this.tabPageConfig.Name = "tabPageConfig";
			this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageConfig.Size = new System.Drawing.Size(683, 305);
			this.tabPageConfig.TabIndex = 0;
			this.tabPageConfig.Text = "Запись учебного плана";
			this.tabPageConfig.UseVisualStyleBackColor = true;
			// 
			// checkBoxIsActiveSemester
			// 
			this.checkBoxIsActiveSemester.AutoSize = true;
			this.checkBoxIsActiveSemester.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBoxIsActiveSemester.Location = new System.Drawing.Point(109, 259);
			this.checkBoxIsActiveSemester.Name = "checkBoxIsActiveSemester";
			this.checkBoxIsActiveSemester.Size = new System.Drawing.Size(77, 17);
			this.checkBoxIsActiveSemester.TabIndex = 14;
			this.checkBoxIsActiveSemester.Text = "Активная:";
			this.checkBoxIsActiveSemester.UseVisualStyleBackColor = true;
			// 
			// checkBoxInDepartment
			// 
			this.checkBoxInDepartment.AutoSize = true;
			this.checkBoxInDepartment.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBoxInDepartment.Location = new System.Drawing.Point(109, 223);
			this.checkBoxInDepartment.Name = "checkBoxInDepartment";
			this.checkBoxInDepartment.Size = new System.Drawing.Size(104, 17);
			this.checkBoxInDepartment.TabIndex = 13;
			this.checkBoxInDepartment.Text = "Кафедральная:";
			this.checkBoxInDepartment.UseVisualStyleBackColor = true;
			// 
			// checkBoxIsChild
			// 
			this.checkBoxIsChild.AutoSize = true;
			this.checkBoxIsChild.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBoxIsChild.Enabled = false;
			this.checkBoxIsChild.Location = new System.Drawing.Point(109, 155);
			this.checkBoxIsChild.Name = "checkBoxIsChild";
			this.checkBoxIsChild.Size = new System.Drawing.Size(147, 17);
			this.checkBoxIsChild.TabIndex = 10;
			this.checkBoxIsChild.Text = "Дисциплина по выбору:";
			this.checkBoxIsChild.UseVisualStyleBackColor = true;
			// 
			// checkBoxIsUseInWorkload
			// 
			this.checkBoxIsUseInWorkload.AutoSize = true;
			this.checkBoxIsUseInWorkload.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBoxIsUseInWorkload.Location = new System.Drawing.Point(109, 188);
			this.checkBoxIsUseInWorkload.Name = "checkBoxIsUseInWorkload";
			this.checkBoxIsUseInWorkload.Size = new System.Drawing.Size(133, 17);
			this.checkBoxIsUseInWorkload.TabIndex = 12;
			this.checkBoxIsUseInWorkload.Text = "Участвует в расчете:";
			this.checkBoxIsUseInWorkload.UseVisualStyleBackColor = true;
			// 
			// labelContingent
			// 
			this.labelContingent.AutoSize = true;
			this.labelContingent.Location = new System.Drawing.Point(17, 67);
			this.labelContingent.Name = "labelContingent";
			this.labelContingent.Size = new System.Drawing.Size(68, 13);
			this.labelContingent.TabIndex = 4;
			this.labelContingent.Text = "Контингент:";
			// 
			// comboBoxContingent
			// 
			this.comboBoxContingent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxContingent.FormattingEnabled = true;
			this.comboBoxContingent.Location = new System.Drawing.Point(109, 65);
			this.comboBoxContingent.Name = "comboBoxContingent";
			this.comboBoxContingent.Size = new System.Drawing.Size(220, 21);
			this.comboBoxContingent.TabIndex = 5;
			// 
			// tabPageRecords
			// 
			this.tabPageRecords.Location = new System.Drawing.Point(4, 22);
			this.tabPageRecords.Name = "tabPageRecords";
			this.tabPageRecords.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageRecords.Size = new System.Drawing.Size(311, 148);
			this.tabPageRecords.TabIndex = 1;
			this.tabPageRecords.Text = "Распределение часов";
			this.tabPageRecords.UseVisualStyleBackColor = true;
			// 
			// checkBoxIsParent
			// 
			this.checkBoxIsParent.AutoSize = true;
			this.checkBoxIsParent.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBoxIsParent.Enabled = false;
			this.checkBoxIsParent.Location = new System.Drawing.Point(284, 155);
			this.checkBoxIsParent.Name = "checkBoxIsParent";
			this.checkBoxIsParent.Size = new System.Drawing.Size(226, 17);
			this.checkBoxIsParent.TabIndex = 11;
			this.checkBoxIsParent.Text = "Родительская дисциплина для выбора:";
			this.checkBoxIsParent.UseVisualStyleBackColor = true;
			// 
			// FormAcademicPlanRecord
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(691, 367);
			this.Name = "FormAcademicPlanRecord";
			this.Text = "Запись учебного плана";
			this.panelMain.ResumeLayout(false);
			this.panelTop.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.tabPageConfig.ResumeLayout(false);
			this.tabPageConfig.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelAcademicPlan;
		private System.Windows.Forms.ComboBox comboBoxAcademicPlan;
		private System.Windows.Forms.Label labelSemester;
		private System.Windows.Forms.ComboBox comboBoxSemester;
		private System.Windows.Forms.Label labelZet;
		private System.Windows.Forms.TextBox textBoxZet;
		private System.Windows.Forms.Label labelDiscipline;
		private System.Windows.Forms.ComboBox comboBoxDiscipline;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TabPage tabPageRecords;
        private System.Windows.Forms.Label labelContingent;
        private System.Windows.Forms.ComboBox comboBoxContingent;
        private System.Windows.Forms.CheckBox checkBoxIsUseInWorkload;
        private System.Windows.Forms.CheckBox checkBoxIsChild;
		private System.Windows.Forms.CheckBox checkBoxInDepartment;
		private System.Windows.Forms.CheckBox checkBoxIsActiveSemester;
		private System.Windows.Forms.CheckBox checkBoxIsParent;
	}
}