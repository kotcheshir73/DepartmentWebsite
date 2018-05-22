namespace DepartmentDesktop.Views.EducationalProcess.Discipline
{
	partial class DisciplineForm
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
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelDisciplineBlock = new System.Windows.Forms.Label();
            this.comboBoxDisciplineBlock = new System.Windows.Forms.ComboBox();
            this.textBoxDisciplineShortName = new System.Windows.Forms.TextBox();
            this.labelDisciplineShortName = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.tabPageLoadDistributions = new System.Windows.Forms.TabPage();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.standartControlAcademicPlanRecords = new DepartmentDesktop.Controllers.StandartControl();
            this.panelTop = new System.Windows.Forms.Panel();
            this.comboBoxAcademicYear = new System.Windows.Forms.ComboBox();
            this.labelAcademicYear = new System.Windows.Forms.Label();
            this.tabPageSchedule = new System.Windows.Forms.TabPage();
            this.standartControlSchedule = new DepartmentDesktop.Controllers.StandartControl();
            this.panelScheduleTop = new System.Windows.Forms.Panel();
            this.comboBoxSeasonDate = new System.Windows.Forms.ComboBox();
            this.labelSeasonDate = new System.Windows.Forms.Label();
            this.textBoxDisciplineBlueAsteriskName = new System.Windows.Forms.TextBox();
            this.labelDisciplineBlueAsteriskName = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.tabPageLoadDistributions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.tabPageSchedule.SuspendLayout();
            this.panelScheduleTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(153, 125);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 9;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(300, 125);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 10;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(72, 125);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(187, 9);
            this.textBoxTitle.MaxLength = 100;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(250, 20);
            this.textBoxTitle.TabIndex = 1;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(6, 12);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(64, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Название*:";
            // 
            // labelDisciplineBlock
            // 
            this.labelDisciplineBlock.AutoSize = true;
            this.labelDisciplineBlock.Location = new System.Drawing.Point(6, 64);
            this.labelDisciplineBlock.Name = "labelDisciplineBlock";
            this.labelDisciplineBlock.Size = new System.Drawing.Size(39, 13);
            this.labelDisciplineBlock.TabIndex = 4;
            this.labelDisciplineBlock.Text = "Блок*:";
            // 
            // comboBoxDisciplineBlock
            // 
            this.comboBoxDisciplineBlock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisciplineBlock.FormattingEnabled = true;
            this.comboBoxDisciplineBlock.Location = new System.Drawing.Point(187, 61);
            this.comboBoxDisciplineBlock.Name = "comboBoxDisciplineBlock";
            this.comboBoxDisciplineBlock.Size = new System.Drawing.Size(250, 21);
            this.comboBoxDisciplineBlock.TabIndex = 5;
            // 
            // textBoxDisciplineShortName
            // 
            this.textBoxDisciplineShortName.Location = new System.Drawing.Point(187, 35);
            this.textBoxDisciplineShortName.MaxLength = 100;
            this.textBoxDisciplineShortName.Name = "textBoxDisciplineShortName";
            this.textBoxDisciplineShortName.Size = new System.Drawing.Size(250, 20);
            this.textBoxDisciplineShortName.TabIndex = 2;
            // 
            // labelDisciplineShortName
            // 
            this.labelDisciplineShortName.AutoSize = true;
            this.labelDisciplineShortName.Location = new System.Drawing.Point(6, 38);
            this.labelDisciplineShortName.Name = "labelDisciplineShortName";
            this.labelDisciplineShortName.Size = new System.Drawing.Size(56, 13);
            this.labelDisciplineShortName.TabIndex = 2;
            this.labelDisciplineShortName.Text = "Краткое*:";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageLoadDistributions);
            this.tabControl.Controls.Add(this.tabPageSchedule);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(821, 503);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.textBoxDisciplineBlueAsteriskName);
            this.tabPageConfig.Controls.Add(this.labelDisciplineBlueAsteriskName);
            this.tabPageConfig.Controls.Add(this.labelTitle);
            this.tabPageConfig.Controls.Add(this.textBoxDisciplineShortName);
            this.tabPageConfig.Controls.Add(this.textBoxTitle);
            this.tabPageConfig.Controls.Add(this.labelDisciplineShortName);
            this.tabPageConfig.Controls.Add(this.buttonSave);
            this.tabPageConfig.Controls.Add(this.comboBoxDisciplineBlock);
            this.tabPageConfig.Controls.Add(this.buttonClose);
            this.tabPageConfig.Controls.Add(this.labelDisciplineBlock);
            this.tabPageConfig.Controls.Add(this.buttonSaveAndClose);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(813, 477);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Основные настройки";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // tabPageLoadDistributions
            // 
            this.tabPageLoadDistributions.Controls.Add(this.splitContainer);
            this.tabPageLoadDistributions.Controls.Add(this.panelTop);
            this.tabPageLoadDistributions.Location = new System.Drawing.Point(4, 22);
            this.tabPageLoadDistributions.Name = "tabPageLoadDistributions";
            this.tabPageLoadDistributions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLoadDistributions.Size = new System.Drawing.Size(813, 477);
            this.tabPageLoadDistributions.TabIndex = 1;
            this.tabPageLoadDistributions.Text = "Нагрузка по дисциплине";
            this.tabPageLoadDistributions.UseVisualStyleBackColor = true;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(3, 43);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.standartControlAcademicPlanRecords);
            this.splitContainer.Size = new System.Drawing.Size(807, 431);
            this.splitContainer.SplitterDistance = 211;
            this.splitContainer.TabIndex = 1;
            // 
            // standartControlAcademicPlanRecords
            // 
            this.standartControlAcademicPlanRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.standartControlAcademicPlanRecords.Location = new System.Drawing.Point(0, 0);
            this.standartControlAcademicPlanRecords.Name = "standartControlAcademicPlanRecords";
            this.standartControlAcademicPlanRecords.Size = new System.Drawing.Size(807, 211);
            this.standartControlAcademicPlanRecords.TabIndex = 0;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.comboBoxAcademicYear);
            this.panelTop.Controls.Add(this.labelAcademicYear);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(3, 3);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(807, 40);
            this.panelTop.TabIndex = 0;
            // 
            // comboBoxAcademicYear
            // 
            this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicYear.FormattingEnabled = true;
            this.comboBoxAcademicYear.Location = new System.Drawing.Point(102, 9);
            this.comboBoxAcademicYear.Name = "comboBoxAcademicYear";
            this.comboBoxAcademicYear.Size = new System.Drawing.Size(222, 21);
            this.comboBoxAcademicYear.TabIndex = 1;
            this.comboBoxAcademicYear.SelectedIndexChanged += new System.EventHandler(this.comboBoxAcademicYear_SelectedIndexChanged);
            // 
            // labelAcademicYear
            // 
            this.labelAcademicYear.AutoSize = true;
            this.labelAcademicYear.Location = new System.Drawing.Point(14, 12);
            this.labelAcademicYear.Name = "labelAcademicYear";
            this.labelAcademicYear.Size = new System.Drawing.Size(79, 13);
            this.labelAcademicYear.TabIndex = 0;
            this.labelAcademicYear.Text = "Учебный год*:";
            // 
            // tabPageSchedule
            // 
            this.tabPageSchedule.Controls.Add(this.standartControlSchedule);
            this.tabPageSchedule.Controls.Add(this.panelScheduleTop);
            this.tabPageSchedule.Location = new System.Drawing.Point(4, 22);
            this.tabPageSchedule.Name = "tabPageSchedule";
            this.tabPageSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSchedule.Size = new System.Drawing.Size(813, 477);
            this.tabPageSchedule.TabIndex = 2;
            this.tabPageSchedule.Text = "Расписание";
            this.tabPageSchedule.UseVisualStyleBackColor = true;
            // 
            // standartControlSchedule
            // 
            this.standartControlSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.standartControlSchedule.Location = new System.Drawing.Point(3, 43);
            this.standartControlSchedule.Name = "standartControlSchedule";
            this.standartControlSchedule.Size = new System.Drawing.Size(807, 431);
            this.standartControlSchedule.TabIndex = 1;
            // 
            // panelScheduleTop
            // 
            this.panelScheduleTop.Controls.Add(this.comboBoxSeasonDate);
            this.panelScheduleTop.Controls.Add(this.labelSeasonDate);
            this.panelScheduleTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelScheduleTop.Location = new System.Drawing.Point(3, 3);
            this.panelScheduleTop.Name = "panelScheduleTop";
            this.panelScheduleTop.Size = new System.Drawing.Size(807, 40);
            this.panelScheduleTop.TabIndex = 0;
            // 
            // comboBoxSeasonDate
            // 
            this.comboBoxSeasonDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSeasonDate.FormattingEnabled = true;
            this.comboBoxSeasonDate.Location = new System.Drawing.Point(102, 9);
            this.comboBoxSeasonDate.Name = "comboBoxSeasonDate";
            this.comboBoxSeasonDate.Size = new System.Drawing.Size(222, 21);
            this.comboBoxSeasonDate.TabIndex = 1;
            this.comboBoxSeasonDate.SelectedIndexChanged += new System.EventHandler(this.comboBoxSeasonDate_SelectedIndexChanged);
            // 
            // labelSeasonDate
            // 
            this.labelSeasonDate.AutoSize = true;
            this.labelSeasonDate.Location = new System.Drawing.Point(14, 12);
            this.labelSeasonDate.Name = "labelSeasonDate";
            this.labelSeasonDate.Size = new System.Drawing.Size(58, 13);
            this.labelSeasonDate.TabIndex = 0;
            this.labelSeasonDate.Text = "Семестр*:";
            // 
            // textBoxDisciplineBlueAsteriskName
            // 
            this.textBoxDisciplineBlueAsteriskName.Location = new System.Drawing.Point(187, 88);
            this.textBoxDisciplineBlueAsteriskName.MaxLength = 100;
            this.textBoxDisciplineBlueAsteriskName.Name = "textBoxDisciplineBlueAsteriskName";
            this.textBoxDisciplineBlueAsteriskName.Size = new System.Drawing.Size(250, 20);
            this.textBoxDisciplineBlueAsteriskName.TabIndex = 7;
            // 
            // labelDisciplineBlueAsteriskName
            // 
            this.labelDisciplineBlueAsteriskName.AutoSize = true;
            this.labelDisciplineBlueAsteriskName.Location = new System.Drawing.Point(6, 91);
            this.labelDisciplineBlueAsteriskName.Name = "labelDisciplineBlueAsteriskName";
            this.labelDisciplineBlueAsteriskName.Size = new System.Drawing.Size(175, 13);
            this.labelDisciplineBlueAsteriskName.TabIndex = 6;
            this.labelDisciplineBlueAsteriskName.Text = "Синоноим для синей звездочки*:";
            // 
            // DisciplineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 503);
            this.Controls.Add(this.tabControl);
            this.Name = "DisciplineForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Дисциплина";
            this.Load += new System.EventHandler(this.DisciplineForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.tabPageLoadDistributions.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.tabPageSchedule.ResumeLayout(false);
            this.panelScheduleTop.ResumeLayout(false);
            this.panelScheduleTop.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonSaveAndClose;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.TextBox textBoxTitle;
		private System.Windows.Forms.Label labelTitle;
		private System.Windows.Forms.Label labelDisciplineBlock;
		private System.Windows.Forms.ComboBox comboBoxDisciplineBlock;
        private System.Windows.Forms.TextBox textBoxDisciplineShortName;
        private System.Windows.Forms.Label labelDisciplineShortName;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TabPage tabPageLoadDistributions;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.ComboBox comboBoxAcademicYear;
        private System.Windows.Forms.Label labelAcademicYear;
        private System.Windows.Forms.SplitContainer splitContainer;
        private Controllers.StandartControl standartControlAcademicPlanRecords;
        private System.Windows.Forms.TabPage tabPageSchedule;
        private System.Windows.Forms.Panel panelScheduleTop;
        private System.Windows.Forms.ComboBox comboBoxSeasonDate;
        private System.Windows.Forms.Label labelSeasonDate;
        private Controllers.StandartControl standartControlSchedule;
        private System.Windows.Forms.TextBox textBoxDisciplineBlueAsteriskName;
        private System.Windows.Forms.Label labelDisciplineBlueAsteriskName;
    }
}