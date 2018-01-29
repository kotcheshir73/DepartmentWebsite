namespace DepartmentDesktop.Views.EducationalProcess.AcademicYear
{
	partial class AcademicYearForm
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageAcademicPlans = new System.Windows.Forms.TabPage();
            this.tabPageTimeNorms = new System.Windows.Forms.TabPage();
            this.tabPageSeasonDates = new System.Windows.Forms.TabPage();
            this.panelTop = new System.Windows.Forms.Panel();
            this.tabPageContingent = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(19, 14);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(64, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Название*:";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(89, 11);
            this.textBoxTitle.MaxLength = 10;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(240, 20);
            this.textBoxTitle.TabIndex = 1;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(585, 9);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(357, 9);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(438, 9);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 3;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageAcademicPlans);
            this.tabControl.Controls.Add(this.tabPageTimeNorms);
            this.tabControl.Controls.Add(this.tabPageContingent);
            this.tabControl.Controls.Add(this.tabPageSeasonDates);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 42);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1089, 579);
            this.tabControl.TabIndex = 1;
            // 
            // tabPageAcademicPlans
            // 
            this.tabPageAcademicPlans.Location = new System.Drawing.Point(4, 22);
            this.tabPageAcademicPlans.Name = "tabPageAcademicPlans";
            this.tabPageAcademicPlans.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAcademicPlans.Size = new System.Drawing.Size(1081, 553);
            this.tabPageAcademicPlans.TabIndex = 0;
            this.tabPageAcademicPlans.Text = "Академические планы";
            this.tabPageAcademicPlans.UseVisualStyleBackColor = true;
            // 
            // tabPageTimeNorms
            // 
            this.tabPageTimeNorms.Location = new System.Drawing.Point(4, 22);
            this.tabPageTimeNorms.Name = "tabPageTimeNorms";
            this.tabPageTimeNorms.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTimeNorms.Size = new System.Drawing.Size(1081, 553);
            this.tabPageTimeNorms.TabIndex = 1;
            this.tabPageTimeNorms.Text = "Нормы времени";
            this.tabPageTimeNorms.UseVisualStyleBackColor = true;
            // 
            // tabPageSeasonDates
            // 
            this.tabPageSeasonDates.Location = new System.Drawing.Point(4, 22);
            this.tabPageSeasonDates.Name = "tabPageSeasonDates";
            this.tabPageSeasonDates.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSeasonDates.Size = new System.Drawing.Size(1081, 553);
            this.tabPageSeasonDates.TabIndex = 2;
            this.tabPageSeasonDates.Text = "Даты семестров";
            this.tabPageSeasonDates.UseVisualStyleBackColor = true;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelTitle);
            this.panelTop.Controls.Add(this.textBoxTitle);
            this.panelTop.Controls.Add(this.buttonSaveAndClose);
            this.panelTop.Controls.Add(this.buttonSave);
            this.panelTop.Controls.Add(this.buttonClose);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1089, 42);
            this.panelTop.TabIndex = 0;
            // 
            // tabPageContingent
            // 
            this.tabPageContingent.Location = new System.Drawing.Point(4, 22);
            this.tabPageContingent.Name = "tabPageContingent";
            this.tabPageContingent.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageContingent.Size = new System.Drawing.Size(1081, 553);
            this.tabPageContingent.TabIndex = 3;
            this.tabPageContingent.Text = "Контингент";
            this.tabPageContingent.UseVisualStyleBackColor = true;
            // 
            // AcademicYearForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 621);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelTop);
            this.Name = "AcademicYearForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Учебный год";
            this.Load += new System.EventHandler(this.AcademicYearForm_Load);
            this.tabControl.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelTitle;
		private System.Windows.Forms.TextBox textBoxTitle;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageAcademicPlans;
        private System.Windows.Forms.TabPage tabPageTimeNorms;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.TabPage tabPageSeasonDates;
        private System.Windows.Forms.TabPage tabPageContingent;
    }
}