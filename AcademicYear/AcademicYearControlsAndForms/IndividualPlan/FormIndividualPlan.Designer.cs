namespace AcademicYearControlsAndForms.IndividualPlan
{
    partial class FormIndividualPlan
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.comboBoxLecturer = new System.Windows.Forms.ComboBox();
            this.labelLecturer = new System.Windows.Forms.Label();
            this.comboBoxAcademicYear = new System.Windows.Forms.ComboBox();
            this.labelAcademicYear = new System.Windows.Forms.Label();
            this.tabPageRecords = new System.Windows.Forms.TabPage();
            this.tabPageNIRContractualWork = new System.Windows.Forms.TabPage();
            this.tabPageNIRScientificArticle = new System.Windows.Forms.TabPage();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tabControl);
            this.panelMain.Size = new System.Drawing.Size(750, 420);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(750, 36);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageRecords);
            this.tabControl.Controls.Add(this.tabPageNIRContractualWork);
            this.tabControl.Controls.Add(this.tabPageNIRScientificArticle);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(750, 420);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.comboBoxLecturer);
            this.tabPageConfig.Controls.Add(this.labelLecturer);
            this.tabPageConfig.Controls.Add(this.comboBoxAcademicYear);
            this.tabPageConfig.Controls.Add(this.labelAcademicYear);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(742, 394);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Индивидуальный план";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // comboBoxLecturer
            // 
            this.comboBoxLecturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLecturer.FormattingEnabled = true;
            this.comboBoxLecturer.Location = new System.Drawing.Point(118, 40);
            this.comboBoxLecturer.Name = "comboBoxLecturer";
            this.comboBoxLecturer.Size = new System.Drawing.Size(222, 21);
            this.comboBoxLecturer.TabIndex = 3;
            // 
            // labelLecturer
            // 
            this.labelLecturer.AutoSize = true;
            this.labelLecturer.Location = new System.Drawing.Point(19, 43);
            this.labelLecturer.Name = "labelLecturer";
            this.labelLecturer.Size = new System.Drawing.Size(93, 13);
            this.labelLecturer.TabIndex = 2;
            this.labelLecturer.Text = "Преподаватель*:";
            // 
            // comboBoxAcademicYear
            // 
            this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicYear.Enabled = false;
            this.comboBoxAcademicYear.FormattingEnabled = true;
            this.comboBoxAcademicYear.Location = new System.Drawing.Point(118, 13);
            this.comboBoxAcademicYear.Name = "comboBoxAcademicYear";
            this.comboBoxAcademicYear.Size = new System.Drawing.Size(222, 21);
            this.comboBoxAcademicYear.TabIndex = 1;
            // 
            // labelAcademicYear
            // 
            this.labelAcademicYear.AutoSize = true;
            this.labelAcademicYear.Location = new System.Drawing.Point(19, 16);
            this.labelAcademicYear.Name = "labelAcademicYear";
            this.labelAcademicYear.Size = new System.Drawing.Size(79, 13);
            this.labelAcademicYear.TabIndex = 0;
            this.labelAcademicYear.Text = "Учебный год*:";
            // 
            // tabPageRecords
            // 
            this.tabPageRecords.Location = new System.Drawing.Point(4, 22);
            this.tabPageRecords.Name = "tabPageRecords";
            this.tabPageRecords.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRecords.Size = new System.Drawing.Size(742, 389);
            this.tabPageRecords.TabIndex = 1;
            this.tabPageRecords.Text = "Записи";
            this.tabPageRecords.UseVisualStyleBackColor = true;
            // 
            // tabPageNIRContractualWork
            // 
            this.tabPageNIRContractualWork.Location = new System.Drawing.Point(4, 22);
            this.tabPageNIRContractualWork.Name = "tabPageNIRContractualWork";
            this.tabPageNIRContractualWork.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNIRContractualWork.Size = new System.Drawing.Size(742, 389);
            this.tabPageNIRContractualWork.TabIndex = 2;
            this.tabPageNIRContractualWork.Text = "Публикации";
            this.tabPageNIRContractualWork.UseVisualStyleBackColor = true;
            // 
            // tabPageNIRScientificArticle
            // 
            this.tabPageNIRScientificArticle.Location = new System.Drawing.Point(4, 22);
            this.tabPageNIRScientificArticle.Name = "tabPageNIRScientificArticle";
            this.tabPageNIRScientificArticle.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNIRScientificArticle.Size = new System.Drawing.Size(742, 389);
            this.tabPageNIRScientificArticle.TabIndex = 3;
            this.tabPageNIRScientificArticle.Text = "Хоздоговорные НИР";
            this.tabPageNIRScientificArticle.UseVisualStyleBackColor = true;
            // 
            // FormIndividualPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 456);
            this.Name = "FormIndividualPlan";
            this.Text = "Индивидуальный план";
            this.Load += new System.EventHandler(this.FormIndividualPlan_Load);
            this.panelMain.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TabPage tabPageRecords;
        private System.Windows.Forms.TabPage tabPageNIRContractualWork;
        private System.Windows.Forms.TabPage tabPageNIRScientificArticle;
        private System.Windows.Forms.ComboBox comboBoxAcademicYear;
        private System.Windows.Forms.Label labelAcademicYear;
        private System.Windows.Forms.ComboBox comboBoxLecturer;
        private System.Windows.Forms.Label labelLecturer;
    }
}