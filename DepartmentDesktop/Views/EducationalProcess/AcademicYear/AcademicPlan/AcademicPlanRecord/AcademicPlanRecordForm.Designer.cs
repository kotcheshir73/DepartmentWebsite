namespace DepartmentDesktop.Views.EducationalProcess.AcademicPlan
{
	partial class AcademicPlanRecordForm
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
            this.labelAcademicPlan = new System.Windows.Forms.Label();
            this.comboBoxAcademicPlan = new System.Windows.Forms.ComboBox();
            this.labelSemester = new System.Windows.Forms.Label();
            this.comboBoxSemester = new System.Windows.Forms.ComboBox();
            this.labelZet = new System.Windows.Forms.Label();
            this.textBoxZet = new System.Windows.Forms.TextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelDiscipline = new System.Windows.Forms.Label();
            this.comboBoxDiscipline = new System.Windows.Forms.ComboBox();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelAcademicPlan
            // 
            this.labelAcademicPlan.AutoSize = true;
            this.labelAcademicPlan.Location = new System.Drawing.Point(18, 14);
            this.labelAcademicPlan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAcademicPlan.Name = "labelAcademicPlan";
            this.labelAcademicPlan.Size = new System.Drawing.Size(126, 20);
            this.labelAcademicPlan.TabIndex = 0;
            this.labelAcademicPlan.Text = "Учебный план*:";
            // 
            // comboBoxAcademicPlan
            // 
            this.comboBoxAcademicPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicPlan.Enabled = false;
            this.comboBoxAcademicPlan.FormattingEnabled = true;
            this.comboBoxAcademicPlan.Location = new System.Drawing.Point(156, 9);
            this.comboBoxAcademicPlan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxAcademicPlan.Name = "comboBoxAcademicPlan";
            this.comboBoxAcademicPlan.Size = new System.Drawing.Size(328, 28);
            this.comboBoxAcademicPlan.TabIndex = 1;
            // 
            // labelSemester
            // 
            this.labelSemester.AutoSize = true;
            this.labelSemester.Location = new System.Drawing.Point(18, 93);
            this.labelSemester.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSemester.Name = "labelSemester";
            this.labelSemester.Size = new System.Drawing.Size(85, 20);
            this.labelSemester.TabIndex = 6;
            this.labelSemester.Text = "Семестр*:";
            // 
            // comboBoxSemester
            // 
            this.comboBoxSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSemester.FormattingEnabled = true;
            this.comboBoxSemester.Location = new System.Drawing.Point(156, 89);
            this.comboBoxSemester.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxSemester.Name = "comboBoxSemester";
            this.comboBoxSemester.Size = new System.Drawing.Size(328, 28);
            this.comboBoxSemester.TabIndex = 7;
            // 
            // labelZet
            // 
            this.labelZet.AutoSize = true;
            this.labelZet.Location = new System.Drawing.Point(18, 135);
            this.labelZet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelZet.Name = "labelZet";
            this.labelZet.Size = new System.Drawing.Size(48, 20);
            this.labelZet.TabIndex = 8;
            this.labelZet.Text = "Зет*:";
            // 
            // textBoxZet
            // 
            this.textBoxZet.Location = new System.Drawing.Point(156, 130);
            this.textBoxZet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxZet.MaxLength = 3;
            this.textBoxZet.Name = "textBoxZet";
            this.textBoxZet.Size = new System.Drawing.Size(148, 26);
            this.textBoxZet.TabIndex = 9;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(372, 179);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(112, 35);
            this.buttonClose.TabIndex = 12;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(30, 179);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(112, 35);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelDiscipline
            // 
            this.labelDiscipline.AutoSize = true;
            this.labelDiscipline.Location = new System.Drawing.Point(18, 55);
            this.labelDiscipline.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDiscipline.Name = "labelDiscipline";
            this.labelDiscipline.Size = new System.Drawing.Size(112, 20);
            this.labelDiscipline.TabIndex = 2;
            this.labelDiscipline.Text = "Дисциплина*:";
            // 
            // comboBoxDiscipline
            // 
            this.comboBoxDiscipline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiscipline.FormattingEnabled = true;
            this.comboBoxDiscipline.Location = new System.Drawing.Point(156, 51);
            this.comboBoxDiscipline.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxDiscipline.Name = "comboBoxDiscipline";
            this.comboBoxDiscipline.Size = new System.Drawing.Size(328, 28);
            this.comboBoxDiscipline.TabIndex = 3;
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(152, 179);
            this.buttonSaveAndClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(212, 35);
            this.buttonSaveAndClose.TabIndex = 11;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // AcademicPlanRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 226);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.comboBoxDiscipline);
            this.Controls.Add(this.labelDiscipline);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxZet);
            this.Controls.Add(this.labelZet);
            this.Controls.Add(this.comboBoxSemester);
            this.Controls.Add(this.labelSemester);
            this.Controls.Add(this.comboBoxAcademicPlan);
            this.Controls.Add(this.labelAcademicPlan);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AcademicPlanRecordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Запись учебного плана";
            this.Load += new System.EventHandler(this.AcademicPlanRecordForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelAcademicPlan;
		private System.Windows.Forms.ComboBox comboBoxAcademicPlan;
		private System.Windows.Forms.Label labelSemester;
		private System.Windows.Forms.ComboBox comboBoxSemester;
		private System.Windows.Forms.Label labelZet;
		private System.Windows.Forms.TextBox textBoxZet;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Label labelDiscipline;
		private System.Windows.Forms.ComboBox comboBoxDiscipline;
		private System.Windows.Forms.Button buttonSaveAndClose;
	}
}