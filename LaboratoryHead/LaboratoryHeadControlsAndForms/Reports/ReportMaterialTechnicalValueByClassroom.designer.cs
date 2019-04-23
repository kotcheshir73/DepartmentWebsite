namespace LaboratoryHeadControlsAndForms.Reports
{
    partial class ReportMaterialTechnicalValueByClassroom
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
            this.comboBoxClassroom = new System.Windows.Forms.ComboBox();
            this.labelClassroom = new System.Windows.Forms.Label();
            this.reportViewerReport = new Microsoft.Reporting.WinForms.ReportViewer();
            this.buttonGet = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.buttonGet);
            this.panelTop.Controls.Add(this.comboBoxClassroom);
            this.panelTop.Controls.Add(this.labelClassroom);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(734, 37);
            this.panelTop.TabIndex = 0;
            // 
            // comboBoxClassroom
            // 
            this.comboBoxClassroom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClassroom.FormattingEnabled = true;
            this.comboBoxClassroom.Location = new System.Drawing.Point(81, 6);
            this.comboBoxClassroom.Name = "comboBoxClassroom";
            this.comboBoxClassroom.Size = new System.Drawing.Size(210, 21);
            this.comboBoxClassroom.TabIndex = 2;
            // 
            // labelClassroom
            // 
            this.labelClassroom.AutoSize = true;
            this.labelClassroom.Location = new System.Drawing.Point(12, 9);
            this.labelClassroom.Name = "labelClassroom";
            this.labelClassroom.Size = new System.Drawing.Size(63, 13);
            this.labelClassroom.TabIndex = 0;
            this.labelClassroom.Text = "Аудитория:";
            // 
            // reportViewerReport
            // 
            this.reportViewerReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerReport.LocalReport.ReportEmbeddedResource = "LaboratoryHeadControlsAndForms.Reports.ReportMaterialTechnicalValueByClassroom.rdlc";
            this.reportViewerReport.Location = new System.Drawing.Point(0, 37);
            this.reportViewerReport.Name = "reportViewerReport";
            this.reportViewerReport.Size = new System.Drawing.Size(734, 724);
            this.reportViewerReport.TabIndex = 1;
            // 
            // buttonGet
            // 
            this.buttonGet.Location = new System.Drawing.Point(315, 4);
            this.buttonGet.Name = "buttonGet";
            this.buttonGet.Size = new System.Drawing.Size(75, 23);
            this.buttonGet.TabIndex = 3;
            this.buttonGet.Text = "Получить";
            this.buttonGet.UseVisualStyleBackColor = true;
            this.buttonGet.Click += new System.EventHandler(this.ButtonGet_Click);
            // 
            // MaterialTechnicalValueReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 761);
            this.Controls.Add(this.reportViewerReport);
            this.Controls.Add(this.panelTop);
            this.Name = "MaterialTechnicalValueReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сводка по аудитории";
            this.Load += new System.EventHandler(this.ReportMaterialTechnicalValueByClassroom_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelClassroom;
        private System.Windows.Forms.ComboBox comboBoxClassroom;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewerReport;
        private System.Windows.Forms.Button buttonGet;
    }
}