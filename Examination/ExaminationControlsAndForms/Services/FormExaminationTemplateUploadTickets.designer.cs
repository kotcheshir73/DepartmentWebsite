namespace ExaminationControlsAndForms.Services
{
    partial class FormExaminationTemplateUploadTickets
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
            this.labelTicketTemplate = new System.Windows.Forms.Label();
            this.ticketTemplateElement = new ExaminationControlsAndForms.TicketTemplate.ControlTicketTemplateSearch();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.buttonFileName = new System.Windows.Forms.Button();
            this.buttonUploadTickets = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTicketTemplate
            // 
            this.labelTicketTemplate.AutoSize = true;
            this.labelTicketTemplate.Location = new System.Drawing.Point(12, 9);
            this.labelTicketTemplate.Name = "labelTicketTemplate";
            this.labelTicketTemplate.Size = new System.Drawing.Size(97, 13);
            this.labelTicketTemplate.TabIndex = 0;
            this.labelTicketTemplate.Text = "Шаблон билетов*:";
            // 
            // ticketTemplateElement
            // 
            this.ticketTemplateElement.ExaminationTemplateId = null;
            this.ticketTemplateElement.Id = null;
            this.ticketTemplateElement.Location = new System.Drawing.Point(115, 6);
            this.ticketTemplateElement.Name = "ticketTemplateElement";
            this.ticketTemplateElement.Size = new System.Drawing.Size(300, 20);
            this.ticketTemplateElement.TabIndex = 1;
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Enabled = false;
            this.textBoxFileName.Location = new System.Drawing.Point(115, 32);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(300, 20);
            this.textBoxFileName.TabIndex = 3;
            // 
            // buttonFileName
            // 
            this.buttonFileName.Location = new System.Drawing.Point(12, 30);
            this.buttonFileName.Name = "buttonFileName";
            this.buttonFileName.Size = new System.Drawing.Size(97, 23);
            this.buttonFileName.TabIndex = 2;
            this.buttonFileName.Text = "Путь до файла";
            this.buttonFileName.UseVisualStyleBackColor = true;
            this.buttonFileName.Click += new System.EventHandler(this.ButtonFileName_Click);
            // 
            // buttonUploadTickets
            // 
            this.buttonUploadTickets.Location = new System.Drawing.Point(306, 69);
            this.buttonUploadTickets.Name = "buttonUploadTickets";
            this.buttonUploadTickets.Size = new System.Drawing.Size(75, 23);
            this.buttonUploadTickets.TabIndex = 4;
            this.buttonUploadTickets.Text = "Выгрузить";
            this.buttonUploadTickets.UseVisualStyleBackColor = true;
            this.buttonUploadTickets.Click += new System.EventHandler(this.ButtonUploadTickets_Click);
            // 
            // ExaminationTemplateUploadTicketsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 101);
            this.Controls.Add(this.buttonUploadTickets);
            this.Controls.Add(this.buttonFileName);
            this.Controls.Add(this.textBoxFileName);
            this.Controls.Add(this.ticketTemplateElement);
            this.Controls.Add(this.labelTicketTemplate);
            this.Name = "ExaminationTemplateUploadTicketsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выгрузка билетов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTicketTemplate;
        private TicketTemplate.ControlTicketTemplateSearch ticketTemplateElement;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Button buttonFileName;
        private System.Windows.Forms.Button buttonUploadTickets;
    }
}