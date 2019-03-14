namespace TicketViews.Views.ExaminationTemplateTicket
{
    partial class ExaminationTemplateTicketForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.numericUpDownTicketNumber = new System.Windows.Forms.NumericUpDown();
            this.examinationTemplateElement = new TicketViews.Views.ExaminationTemplate.ExaminationTemplateElement();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.labelTicketNumber = new System.Windows.Forms.Label();
            this.labelExaminationTemplate = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.tabPageRecords = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTicketNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageRecords);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(834, 501);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.numericUpDownTicketNumber);
            this.tabPageConfig.Controls.Add(this.examinationTemplateElement);
            this.tabPageConfig.Controls.Add(this.buttonSaveAndClose);
            this.tabPageConfig.Controls.Add(this.labelTicketNumber);
            this.tabPageConfig.Controls.Add(this.labelExaminationTemplate);
            this.tabPageConfig.Controls.Add(this.buttonClose);
            this.tabPageConfig.Controls.Add(this.buttonSave);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(826, 475);
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
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(135, 79);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 5;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.ButtonSaveAndClose_Click);
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
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(282, 79);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 6;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(54, 79);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // tabPageRecords
            // 
            this.tabPageRecords.Location = new System.Drawing.Point(4, 22);
            this.tabPageRecords.Name = "tabPageRecords";
            this.tabPageRecords.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRecords.Size = new System.Drawing.Size(826, 475);
            this.tabPageRecords.TabIndex = 1;
            this.tabPageRecords.Text = "Вопросы";
            this.tabPageRecords.UseVisualStyleBackColor = true;
            // 
            // ExaminationTemplateTicketForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 501);
            this.Controls.Add(this.tabControl);
            this.Name = "ExaminationTemplateTicketForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Билет экзамена";
            this.Load += new System.EventHandler(this.ExaminationTemplateTicketForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTicketNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Label labelTicketNumber;
        private System.Windows.Forms.Label labelExaminationTemplate;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TabPage tabPageRecords;
        private ExaminationTemplate.ExaminationTemplateElement examinationTemplateElement;
        private System.Windows.Forms.NumericUpDown numericUpDownTicketNumber;
    }
}