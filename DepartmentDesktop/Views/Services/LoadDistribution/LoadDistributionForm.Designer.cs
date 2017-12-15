namespace DepartmentDesktop.Views.EducationalProcess.LoadDistribution
{
	partial class LoadDistributionForm
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
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageConfig = new System.Windows.Forms.TabPage();
			this.buttonSaveAndClose = new System.Windows.Forms.Button();
			this.tabPageRecords = new System.Windows.Forms.TabPage();
			this.tabControl.SuspendLayout();
			this.tabPageConfig.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBoxAcademicYear
			// 
			this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAcademicYear.FormattingEnabled = true;
			this.comboBoxAcademicYear.Location = new System.Drawing.Point(104, 14);
			this.comboBoxAcademicYear.Name = "comboBoxAcademicYear";
			this.comboBoxAcademicYear.Size = new System.Drawing.Size(220, 21);
			this.comboBoxAcademicYear.TabIndex = 1;
			// 
			// labelAcademicYear
			// 
			this.labelAcademicYear.AutoSize = true;
			this.labelAcademicYear.Location = new System.Drawing.Point(16, 17);
			this.labelAcademicYear.Name = "labelAcademicYear";
			this.labelAcademicYear.Size = new System.Drawing.Size(79, 13);
			this.labelAcademicYear.TabIndex = 0;
			this.labelAcademicYear.Text = "Учебный год*:";
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(249, 41);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 4;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(21, 41);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 2;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageConfig);
			this.tabControl.Controls.Add(this.tabPageRecords);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(1187, 461);
			this.tabControl.TabIndex = 0;
			// 
			// tabPageConfig
			// 
			this.tabPageConfig.Controls.Add(this.buttonSaveAndClose);
			this.tabPageConfig.Controls.Add(this.labelAcademicYear);
			this.tabPageConfig.Controls.Add(this.buttonClose);
			this.tabPageConfig.Controls.Add(this.comboBoxAcademicYear);
			this.tabPageConfig.Controls.Add(this.buttonSave);
			this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
			this.tabPageConfig.Name = "tabPageConfig";
			this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageConfig.Size = new System.Drawing.Size(1179, 435);
			this.tabPageConfig.TabIndex = 0;
			this.tabPageConfig.Text = "Распределение нагрузок";
			this.tabPageConfig.UseVisualStyleBackColor = true;
			// 
			// buttonSaveAndClose
			// 
			this.buttonSaveAndClose.Location = new System.Drawing.Point(102, 41);
			this.buttonSaveAndClose.Name = "buttonSaveAndClose";
			this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
			this.buttonSaveAndClose.TabIndex = 3;
			this.buttonSaveAndClose.Text = "Сохранить и закрыть";
			this.buttonSaveAndClose.UseVisualStyleBackColor = true;
			this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
			// 
			// tabPageRecords
			// 
			this.tabPageRecords.Location = new System.Drawing.Point(4, 22);
			this.tabPageRecords.Name = "tabPageRecords";
			this.tabPageRecords.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageRecords.Size = new System.Drawing.Size(1179, 435);
			this.tabPageRecords.TabIndex = 1;
			this.tabPageRecords.Text = "Записи";
			this.tabPageRecords.UseVisualStyleBackColor = true;
			// 
			// LoadDistributionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1187, 461);
			this.Controls.Add(this.tabControl);
			this.Name = "LoadDistributionForm";
			this.Text = "Распределение нагрузки";
			this.Load += new System.EventHandler(this.LoadDistributionForm_Load);
			this.tabControl.ResumeLayout(false);
			this.tabPageConfig.ResumeLayout(false);
			this.tabPageConfig.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxAcademicYear;
		private System.Windows.Forms.Label labelAcademicYear;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageConfig;
		private System.Windows.Forms.TabPage tabPageRecords;
		private System.Windows.Forms.Button buttonSaveAndClose;
	}
}