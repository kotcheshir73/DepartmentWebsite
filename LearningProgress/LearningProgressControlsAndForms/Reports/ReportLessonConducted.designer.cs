namespace LearningProgressControlsAndForms.Reports
{
    partial class ReportLessonConducted
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
            this.reportViewerReport = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panelConfig = new System.Windows.Forms.Panel();
            this.labelStudentGroup = new System.Windows.Forms.Label();
            this.comboBoxStudentGroups = new System.Windows.Forms.ComboBox();
            this.panelConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportViewerReport
            // 
            this.reportViewerReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerReport.LocalReport.ReportEmbeddedResource = "LearningProgressControlsAndForms.Reports.ReportLessonConducted.rdlc";
            this.reportViewerReport.Location = new System.Drawing.Point(0, 31);
            this.reportViewerReport.Name = "reportViewerReport";
            this.reportViewerReport.Size = new System.Drawing.Size(784, 680);
            this.reportViewerReport.TabIndex = 0;
            // 
            // panelConfig
            // 
            this.panelConfig.Controls.Add(this.comboBoxStudentGroups);
            this.panelConfig.Controls.Add(this.labelStudentGroup);
            this.panelConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelConfig.Location = new System.Drawing.Point(0, 0);
            this.panelConfig.Name = "panelConfig";
            this.panelConfig.Size = new System.Drawing.Size(784, 31);
            this.panelConfig.TabIndex = 1;
            // 
            // labelStudentGroup
            // 
            this.labelStudentGroup.AutoSize = true;
            this.labelStudentGroup.Location = new System.Drawing.Point(12, 9);
            this.labelStudentGroup.Name = "labelStudentGroup";
            this.labelStudentGroup.Size = new System.Drawing.Size(45, 13);
            this.labelStudentGroup.TabIndex = 0;
            this.labelStudentGroup.Text = "Группа:";
            // 
            // comboBoxStudentGroups
            // 
            this.comboBoxStudentGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudentGroups.FormattingEnabled = true;
            this.comboBoxStudentGroups.Location = new System.Drawing.Point(63, 6);
            this.comboBoxStudentGroups.Name = "comboBoxStudentGroups";
            this.comboBoxStudentGroups.Size = new System.Drawing.Size(109, 21);
            this.comboBoxStudentGroups.TabIndex = 1;
            this.comboBoxStudentGroups.SelectedIndexChanged += new System.EventHandler(this.ComboBoxStudentGroups_SelectedIndexChanged);
            // 
            // LessonConductedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 711);
            this.Controls.Add(this.reportViewerReport);
            this.Controls.Add(this.panelConfig);
            this.Name = "LessonConductedForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Посещаемость студентов";
            this.Load += new System.EventHandler(this.ReportLessonConducted_Load);
            this.panelConfig.ResumeLayout(false);
            this.panelConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerReport;
        private System.Windows.Forms.Panel panelConfig;
        private System.Windows.Forms.Label labelStudentGroup;
        private System.Windows.Forms.ComboBox comboBoxStudentGroups;
    }
}