namespace DepartmentDesktop.Views.LaboratoryHead.SoftwareRecord
{
    partial class ReportSoftwareRecordsByClaim
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.textBoxClaim = new System.Windows.Forms.TextBox();
            this.buttonGet = new System.Windows.Forms.Button();
            this.labelClaim = new System.Windows.Forms.Label();
            this.reportViewerReport = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.textBoxClaim);
            this.panelTop.Controls.Add(this.buttonGet);
            this.panelTop.Controls.Add(this.labelClaim);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(734, 37);
            this.panelTop.TabIndex = 0;
            // 
            // textBoxClaim
            // 
            this.textBoxClaim.Location = new System.Drawing.Point(65, 6);
            this.textBoxClaim.Name = "textBoxClaim";
            this.textBoxClaim.Size = new System.Drawing.Size(210, 20);
            this.textBoxClaim.TabIndex = 1;
            // 
            // buttonGet
            // 
            this.buttonGet.Location = new System.Drawing.Point(315, 4);
            this.buttonGet.Name = "buttonGet";
            this.buttonGet.Size = new System.Drawing.Size(75, 23);
            this.buttonGet.TabIndex = 3;
            this.buttonGet.Text = "Получить";
            this.buttonGet.UseVisualStyleBackColor = true;
            this.buttonGet.Click += new System.EventHandler(this.buttonGet_Click);
            // 
            // labelClaim
            // 
            this.labelClaim.AutoSize = true;
            this.labelClaim.Location = new System.Drawing.Point(12, 9);
            this.labelClaim.Name = "labelClaim";
            this.labelClaim.Size = new System.Drawing.Size(47, 13);
            this.labelClaim.TabIndex = 0;
            this.labelClaim.Text = "Заявка:";
            // 
            // reportViewerReport
            // 
            this.reportViewerReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerReport.LocalReport.ReportEmbeddedResource = "DepartmentDesktop.Views.LaboratoryHead.SoftwareRecord.ReportSoftwareRecordsByClai" +
    "m.rdlc";
            this.reportViewerReport.Location = new System.Drawing.Point(0, 37);
            this.reportViewerReport.Name = "reportViewerReport";
            this.reportViewerReport.ServerReport.BearerToken = null;
            this.reportViewerReport.Size = new System.Drawing.Size(734, 724);
            this.reportViewerReport.TabIndex = 1;
            // 
            // ReportSoftwareRecordsByClaim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 761);
            this.Controls.Add(this.reportViewerReport);
            this.Controls.Add(this.panelTop);
            this.Name = "ReportSoftwareRecordsByClaim";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список ПО по заявке";
            this.Load += new System.EventHandler(this.ReportSoftwareRecordsByClaim_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button buttonGet;
        private System.Windows.Forms.Label labelClaim;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewerReport;
        private System.Windows.Forms.TextBox textBoxClaim;
    }
}