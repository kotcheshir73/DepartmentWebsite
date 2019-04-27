namespace ExaminationControlsAndForms.ExaminationTemplateTicketQuestion
{
    partial class FormExaminationTemplateTicketQuestion
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
            this.labelExaminationTemplateTicket = new System.Windows.Forms.Label();
            this.labelExaminationTemplateBlockQuestion = new System.Windows.Forms.Label();
            this.labelOrder = new System.Windows.Forms.Label();
            this.examinationTemplateTicketElement = new ExaminationControlsAndForms.ExaminationTemplateTicket.ControlExaminationTemplateTicketSearch();
            this.examinationTemplateBlockQuestionElement = new ExaminationControlsAndForms.ExaminationTemplateBlockQuestion.ControlExaminationTemplateBlockQuestionSearch();
            this.numericUpDownOrder = new System.Windows.Forms.NumericUpDown();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.numericUpDownOrder);
            this.panelMain.Controls.Add(this.labelExaminationTemplateTicket);
            this.panelMain.Controls.Add(this.examinationTemplateBlockQuestionElement);
            this.panelMain.Controls.Add(this.labelOrder);
            this.panelMain.Controls.Add(this.examinationTemplateTicketElement);
            this.panelMain.Controls.Add(this.labelExaminationTemplateBlockQuestion);
            this.panelMain.Size = new System.Drawing.Size(444, 95);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(444, 36);
            // 
            // labelExaminationTemplateTicket
            // 
            this.labelExaminationTemplateTicket.AutoSize = true;
            this.labelExaminationTemplateTicket.Location = new System.Drawing.Point(12, 12);
            this.labelExaminationTemplateTicket.Name = "labelExaminationTemplateTicket";
            this.labelExaminationTemplateTicket.Size = new System.Drawing.Size(44, 13);
            this.labelExaminationTemplateTicket.TabIndex = 0;
            this.labelExaminationTemplateTicket.Text = "Билет*:";
            // 
            // labelExaminationTemplateBlockQuestion
            // 
            this.labelExaminationTemplateBlockQuestion.AutoSize = true;
            this.labelExaminationTemplateBlockQuestion.Location = new System.Drawing.Point(12, 39);
            this.labelExaminationTemplateBlockQuestion.Name = "labelExaminationTemplateBlockQuestion";
            this.labelExaminationTemplateBlockQuestion.Size = new System.Drawing.Size(51, 13);
            this.labelExaminationTemplateBlockQuestion.TabIndex = 2;
            this.labelExaminationTemplateBlockQuestion.Text = "Вопрос*:";
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.Location = new System.Drawing.Point(12, 65);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(113, 13);
            this.labelOrder.TabIndex = 4;
            this.labelOrder.Text = "Порядковый номер*:";
            // 
            // examinationTemplateTicketElement
            // 
            this.examinationTemplateTicketElement.Enabled = false;
            this.examinationTemplateTicketElement.ExaminationTemplateId = null;
            this.examinationTemplateTicketElement.Id = null;
            this.examinationTemplateTicketElement.Location = new System.Drawing.Point(131, 10);
            this.examinationTemplateTicketElement.Name = "examinationTemplateTicketElement";
            this.examinationTemplateTicketElement.Size = new System.Drawing.Size(300, 20);
            this.examinationTemplateTicketElement.TabIndex = 1;
            // 
            // examinationTemplateBlockQuestionElement
            // 
            this.examinationTemplateBlockQuestionElement.ExaminationTemplateBlockId = null;
            this.examinationTemplateBlockQuestionElement.ExaminationTemplateId = null;
            this.examinationTemplateBlockQuestionElement.Id = null;
            this.examinationTemplateBlockQuestionElement.Location = new System.Drawing.Point(131, 36);
            this.examinationTemplateBlockQuestionElement.Name = "examinationTemplateBlockQuestionElement";
            this.examinationTemplateBlockQuestionElement.Size = new System.Drawing.Size(300, 20);
            this.examinationTemplateBlockQuestionElement.TabIndex = 3;
            // 
            // numericUpDownOrder
            // 
            this.numericUpDownOrder.Location = new System.Drawing.Point(131, 63);
            this.numericUpDownOrder.Name = "numericUpDownOrder";
            this.numericUpDownOrder.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownOrder.TabIndex = 5;
            // 
            // FormExaminationTemplateTicketQuestion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 131);
            this.Name = "FormExaminationTemplateTicketQuestion";
            this.Text = "Вопрос билета";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelExaminationTemplateTicket;
        private System.Windows.Forms.Label labelExaminationTemplateBlockQuestion;
        private System.Windows.Forms.Label labelOrder;
        private ExaminationTemplateTicket.ControlExaminationTemplateTicketSearch examinationTemplateTicketElement;
        private ExaminationTemplateBlockQuestion.ControlExaminationTemplateBlockQuestionSearch examinationTemplateBlockQuestionElement;
        private System.Windows.Forms.NumericUpDown numericUpDownOrder;
    }
}