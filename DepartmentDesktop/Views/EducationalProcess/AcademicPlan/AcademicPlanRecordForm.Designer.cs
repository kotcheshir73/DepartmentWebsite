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
			this.labelKindOfLoad = new System.Windows.Forms.Label();
			this.comboBoxKindOfLoad = new System.Windows.Forms.ComboBox();
			this.labelSemester = new System.Windows.Forms.Label();
			this.comboBoxSemester = new System.Windows.Forms.ComboBox();
			this.labelHours = new System.Windows.Forms.Label();
			this.textBoxHours = new System.Windows.Forms.TextBox();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.labelDiscipline = new System.Windows.Forms.Label();
			this.comboBoxDiscipline = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// labelAcademicPlan
			// 
			this.labelAcademicPlan.AutoSize = true;
			this.labelAcademicPlan.Location = new System.Drawing.Point(12, 9);
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
			this.comboBoxAcademicPlan.Location = new System.Drawing.Point(104, 6);
			this.comboBoxAcademicPlan.Name = "comboBoxAcademicPlan";
			this.comboBoxAcademicPlan.Size = new System.Drawing.Size(200, 21);
			this.comboBoxAcademicPlan.TabIndex = 1;
			// 
			// labelKindOfLoad
			// 
			this.labelKindOfLoad.AutoSize = true;
			this.labelKindOfLoad.Location = new System.Drawing.Point(12, 63);
			this.labelKindOfLoad.Name = "labelKindOfLoad";
			this.labelKindOfLoad.Size = new System.Drawing.Size(82, 13);
			this.labelKindOfLoad.TabIndex = 4;
			this.labelKindOfLoad.Text = "Вид нагрузки*:";
			// 
			// comboBoxKindOfLoad
			// 
			this.comboBoxKindOfLoad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxKindOfLoad.FormattingEnabled = true;
			this.comboBoxKindOfLoad.Location = new System.Drawing.Point(104, 60);
			this.comboBoxKindOfLoad.Name = "comboBoxKindOfLoad";
			this.comboBoxKindOfLoad.Size = new System.Drawing.Size(200, 21);
			this.comboBoxKindOfLoad.TabIndex = 5;
			// 
			// labelSemester
			// 
			this.labelSemester.AutoSize = true;
			this.labelSemester.Location = new System.Drawing.Point(12, 90);
			this.labelSemester.Name = "labelSemester";
			this.labelSemester.Size = new System.Drawing.Size(58, 13);
			this.labelSemester.TabIndex = 6;
			this.labelSemester.Text = "Семестр*:";
			// 
			// comboBoxSemester
			// 
			this.comboBoxSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSemester.FormattingEnabled = true;
			this.comboBoxSemester.Location = new System.Drawing.Point(104, 87);
			this.comboBoxSemester.Name = "comboBoxSemester";
			this.comboBoxSemester.Size = new System.Drawing.Size(200, 21);
			this.comboBoxSemester.TabIndex = 7;
			// 
			// labelHours
			// 
			this.labelHours.AutoSize = true;
			this.labelHours.Location = new System.Drawing.Point(12, 117);
			this.labelHours.Name = "labelHours";
			this.labelHours.Size = new System.Drawing.Size(42, 13);
			this.labelHours.TabIndex = 8;
			this.labelHours.Text = "Часы*:";
			// 
			// textBoxHours
			// 
			this.textBoxHours.Location = new System.Drawing.Point(104, 114);
			this.textBoxHours.MaxLength = 3;
			this.textBoxHours.Name = "textBoxHours";
			this.textBoxHours.Size = new System.Drawing.Size(100, 20);
			this.textBoxHours.TabIndex = 9;
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(229, 147);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 11;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(148, 147);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 10;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
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
			// comboBoxDiscipline
			// 
			this.comboBoxDiscipline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDiscipline.FormattingEnabled = true;
			this.comboBoxDiscipline.Location = new System.Drawing.Point(104, 33);
			this.comboBoxDiscipline.Name = "comboBoxDiscipline";
			this.comboBoxDiscipline.Size = new System.Drawing.Size(200, 21);
			this.comboBoxDiscipline.TabIndex = 3;
			// 
			// AcademicPlanRecordForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(314, 182);
			this.Controls.Add(this.comboBoxDiscipline);
			this.Controls.Add(this.labelDiscipline);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.textBoxHours);
			this.Controls.Add(this.labelHours);
			this.Controls.Add(this.comboBoxSemester);
			this.Controls.Add(this.labelSemester);
			this.Controls.Add(this.comboBoxKindOfLoad);
			this.Controls.Add(this.labelKindOfLoad);
			this.Controls.Add(this.comboBoxAcademicPlan);
			this.Controls.Add(this.labelAcademicPlan);
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
		private System.Windows.Forms.Label labelKindOfLoad;
		private System.Windows.Forms.ComboBox comboBoxKindOfLoad;
		private System.Windows.Forms.Label labelSemester;
		private System.Windows.Forms.ComboBox comboBoxSemester;
		private System.Windows.Forms.Label labelHours;
		private System.Windows.Forms.TextBox textBoxHours;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Label labelDiscipline;
		private System.Windows.Forms.ComboBox comboBoxDiscipline;
	}
}