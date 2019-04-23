namespace AcademicYearControlsAndForms.AcademicYear
{
	partial class FormAcademicYear
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageAcademicPlans = new System.Windows.Forms.TabPage();
            this.tabPageStreamLessons = new System.Windows.Forms.TabPage();
            this.tabPageTimeNorms = new System.Windows.Forms.TabPage();
            this.tabPageContingents = new System.Windows.Forms.TabPage();
            this.tabPageSeasonDates = new System.Windows.Forms.TabPage();
            this.tabPageLecturerWorkload = new System.Windows.Forms.TabPage();
            this.panelTop = new System.Windows.Forms.Panel();
            this.tabPageDisciplineTimeDistribution = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(444, 11);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(591, 11);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(363, 11);
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
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageAcademicPlans);
            this.tabControl.Controls.Add(this.tabPageStreamLessons);
            this.tabControl.Controls.Add(this.tabPageTimeNorms);
            this.tabControl.Controls.Add(this.tabPageContingents);
            this.tabControl.Controls.Add(this.tabPageSeasonDates);
            this.tabControl.Controls.Add(this.tabPageLecturerWorkload);
            this.tabControl.Controls.Add(this.tabPageDisciplineTimeDistribution);
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
            // tabPageStreamLessons
            // 
            this.tabPageStreamLessons.Location = new System.Drawing.Point(4, 22);
            this.tabPageStreamLessons.Name = "tabPageStreamLessons";
            this.tabPageStreamLessons.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStreamLessons.Size = new System.Drawing.Size(1081, 553);
            this.tabPageStreamLessons.TabIndex = 4;
            this.tabPageStreamLessons.Text = "Потоки";
            this.tabPageStreamLessons.UseVisualStyleBackColor = true;
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
            // tabPageContingents
            // 
            this.tabPageContingents.Location = new System.Drawing.Point(4, 22);
            this.tabPageContingents.Name = "tabPageContingents";
            this.tabPageContingents.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageContingents.Size = new System.Drawing.Size(1081, 553);
            this.tabPageContingents.TabIndex = 3;
            this.tabPageContingents.Text = "Контингент";
            this.tabPageContingents.UseVisualStyleBackColor = true;
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
            // tabPageLecturerWorkload
            // 
            this.tabPageLecturerWorkload.Location = new System.Drawing.Point(4, 22);
            this.tabPageLecturerWorkload.Name = "tabPageLecturerWorkload";
            this.tabPageLecturerWorkload.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLecturerWorkload.Size = new System.Drawing.Size(1081, 553);
            this.tabPageLecturerWorkload.TabIndex = 5;
            this.tabPageLecturerWorkload.Text = "Преподаватели";
            this.tabPageLecturerWorkload.UseVisualStyleBackColor = true;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelTitle);
            this.panelTop.Controls.Add(this.textBoxTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1089, 42);
            this.panelTop.TabIndex = 0;
            // 
            // tabPageDisciplineTimeDistribution
            // 
            this.tabPageDisciplineTimeDistribution.Location = new System.Drawing.Point(4, 22);
            this.tabPageDisciplineTimeDistribution.Name = "tabPageDisciplineTimeDistribution";
            this.tabPageDisciplineTimeDistribution.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDisciplineTimeDistribution.Size = new System.Drawing.Size(1081, 553);
            this.tabPageDisciplineTimeDistribution.TabIndex = 6;
            this.tabPageDisciplineTimeDistribution.Text = "Расчасовки";
            this.tabPageDisciplineTimeDistribution.UseVisualStyleBackColor = true;
            // 
            // FormAcademicYear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 621);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelTop);
            this.Name = "FormAcademicYear";
            this.Text = "Учебный год";
            this.Load += new System.EventHandler(this.FormAcademicYear_Load);
            this.Controls.SetChildIndex(this.panelTop, 0);
            this.Controls.SetChildIndex(this.tabControl, 0);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.buttonSaveAndClose, 0);
            this.tabControl.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelTitle;
		private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageAcademicPlans;
        private System.Windows.Forms.TabPage tabPageTimeNorms;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.TabPage tabPageSeasonDates;
        private System.Windows.Forms.TabPage tabPageContingents;
        private System.Windows.Forms.TabPage tabPageStreamLessons;
        private System.Windows.Forms.TabPage tabPageLecturerWorkload;
        private System.Windows.Forms.TabPage tabPageDisciplineTimeDistribution;
    }
}