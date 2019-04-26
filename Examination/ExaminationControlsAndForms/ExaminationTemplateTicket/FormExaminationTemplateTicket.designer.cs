namespace ExaminationControlsAndForms.ExaminationTemplateTicket
{
    partial class FormExaminationTemplateTicket
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
            this.numericUpDownTicketNumber = new System.Windows.Forms.NumericUpDown();
            this.examinationTemplateElement = new ExaminationControlsAndForms.ExaminationTemplate.ControlExaminationTemplateSearch();
            this.labelTicketNumber = new System.Windows.Forms.Label();
            this.labelExaminationTemplate = new System.Windows.Forms.Label();
            this.tabPageRecords = new System.Windows.Forms.TabPage();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTicketNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tabControl);
            this.panelMain.Size = new System.Drawing.Size(834, 465);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(834, 36);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageRecords);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(834, 465);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.numericUpDownTicketNumber);
            this.tabPageConfig.Controls.Add(this.examinationTemplateElement);
            this.tabPageConfig.Controls.Add(this.labelTicketNumber);
            this.tabPageConfig.Controls.Add(this.labelExaminationTemplate);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(826, 439);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Билет";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // numericUpDownTicketNumber
            // 
            this.numericUpDownTicketNumber.Location = new System.Drawing.Point(100, 37);
            this.numericUpDownTicketNumber.Name = "numericUpDownTicketNumber";
            this.numericUpDownTicketNumber.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownTicketNumber.TabIndex = 3;
            // 
            // examinationTemplateElement
            // 
            this.examinationTemplateElement.DisciplineId = null;
            this.examinationTemplateElement.Id = null;
            this.examinationTemplateElement.Location = new System.Drawing.Point(100, 11);
            this.examinationTemplateElement.Name = "examinationTemplateElement";
            this.examinationTemplateElement.Size = new System.Drawing.Size(300, 20);
            this.examinationTemplateElement.TabIndex = 1;
            // 
            // labelTicketNumber
            // 
            this.labelTicketNumber.AutoSize = true;
            this.labelTicketNumber.Location = new System.Drawing.Point(12, 40);
            this.labelTicketNumber.Name = "labelTicketNumber";
            this.labelTicketNumber.Size = new System.Drawing.Size(82, 13);
            this.labelTicketNumber.TabIndex = 2;
            this.labelTicketNumber.Text = "Номер билета:";
            // 
            // labelExaminationTemplate
            // 
            this.labelExaminationTemplate.AutoSize = true;
            this.labelExaminationTemplate.Location = new System.Drawing.Point(12, 13);
            this.labelExaminationTemplate.Name = "labelExaminationTemplate";
            this.labelExaminationTemplate.Size = new System.Drawing.Size(59, 13);
            this.labelExaminationTemplate.TabIndex = 0;
            this.labelExaminationTemplate.Text = "Экзамен*:";
            // 
            // tabPageRecords
            // 
            this.tabPageRecords.Location = new System.Drawing.Point(4, 22);
            this.tabPageRecords.Name = "tabPageRecords";
            this.tabPageRecords.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRecords.Size = new System.Drawing.Size(826, 445);
            this.tabPageRecords.TabIndex = 1;
            this.tabPageRecords.Text = "Вопросы";
            this.tabPageRecords.UseVisualStyleBackColor = true;
            // 
            // FormExaminationTemplateTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 501);
            this.Name = "FormExaminationTemplateTicket";
            this.Text = "Билет экзамена";
            this.Load += new System.EventHandler(this.FormExaminationTemplateTicket_Load);
            this.panelMain.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTicketNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.Label labelTicketNumber;
        private System.Windows.Forms.Label labelExaminationTemplate;
        private System.Windows.Forms.TabPage tabPageRecords;
        private ExaminationTemplate.ControlExaminationTemplateSearch examinationTemplateElement;
        private System.Windows.Forms.NumericUpDown numericUpDownTicketNumber;
    }
}