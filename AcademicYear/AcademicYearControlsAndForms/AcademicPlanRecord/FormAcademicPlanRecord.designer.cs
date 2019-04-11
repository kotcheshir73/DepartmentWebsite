﻿namespace AcademicYearControlsAndForms.AcademicPlanRecord
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
            this.labelContingent = new System.Windows.Forms.Label();
            this.comboBoxContingent = new System.Windows.Forms.ComboBox();
            this.tabPageRecords = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(270, 336);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(417, 336);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(189, 336);
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
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageRecords);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(691, 330);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
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
            this.tabPageConfig.Size = new System.Drawing.Size(683, 304);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Запись учебного плана";
            this.tabPageConfig.UseVisualStyleBackColor = true;
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
            this.tabPageRecords.Size = new System.Drawing.Size(683, 304);
            this.tabPageRecords.TabIndex = 1;
            this.tabPageRecords.Text = "Распределение часов";
            this.tabPageRecords.UseVisualStyleBackColor = true;
            // 
            // FormAcademicPlanRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 367);
            this.Controls.Add(this.tabControl);
            this.Name = "FormAcademicPlanRecord";
            this.Text = "Запись учебного плана";
            this.Load += new System.EventHandler(this.FormAcademicPlanRecord_Load);
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
    }
}