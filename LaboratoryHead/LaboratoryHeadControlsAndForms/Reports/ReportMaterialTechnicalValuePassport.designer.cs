namespace LaboratoryHeadControlsAndForms.Reports
{
    partial class ReportMaterialTechnicalValuePassport
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
            this.reportViewerReport.LocalReport.ReportEmbeddedResource = "LaboratoryHeadControlsAndForms.Reports.ReportMaterialTechnicalValuePassport.rdlc";
            this.reportViewerReport.Location = new System.Drawing.Point(0, 0);
            this.reportViewerReport.Name = "reportViewerReport";
            this.reportViewerReport.Size = new System.Drawing.Size(784, 761);
            this.reportViewerReport.TabIndex = 3;
            // 
            // MaterialTechnicalValuePassport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.reportViewerReport);
            this.Name = "MaterialTechnicalValuePassport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Технический паспорт";
            this.Load += new System.EventHandler(this.ReportMaterialTechnicalValuePassport_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private Microsoft.Reporting.WinForms.ReportViewer reportViewerReport;
    }
}