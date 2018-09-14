namespace DepartmentDesktop.Views.LearningProgress.DisciplineStudentRecord
{
    partial class DisciplineStudentRecordByGroupForm
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
            this.SuspendLayout();
            // 
            // reportViewerReport
            // 
            this.reportViewerReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerReport.LocalReport.ReportEmbeddedResource = "DepartmentDesktop.Views.LearningProgress.DisciplineStudentRecord.ReportDiscipline" +
    "StudentRecordByGroup.rdlc";
            this.reportViewerReport.Location = new System.Drawing.Point(0, 0);
            this.reportViewerReport.Name = "reportViewerReport";
            this.reportViewerReport.ServerReport.BearerToken = null;
            this.reportViewerReport.Size = new System.Drawing.Size(734, 761);
            this.reportViewerReport.TabIndex = 0;
            // 
            // DisciplineStudentRecordByGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 761);
            this.Controls.Add(this.reportViewerReport);
            this.Name = "DisciplineStudentRecordByGroupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Варианты студентов";
            this.Load += new System.EventHandler(this.DisciplineStudentRecordByGroupForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerReport;
    }
}