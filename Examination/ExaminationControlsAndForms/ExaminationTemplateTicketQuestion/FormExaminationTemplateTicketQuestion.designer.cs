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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(152, 96);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(299, 96);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(71, 96);
            // 
            // labelExaminationTemplateTicket
            // 
            this.labelExaminationTemplateTicket.AutoSize = true;
            this.labelExaminationTemplateTicket.Location = new System.Drawing.Point(12, 9);
            this.labelExaminationTemplateTicket.Name = "labelExaminationTemplateTicket";
            this.labelExaminationTemplateTicket.Size = new System.Drawing.Size(44, 13);
            this.labelExaminationTemplateTicket.TabIndex = 0;
            this.labelExaminationTemplateTicket.Text = "Билет*:";
            // 
            // labelExaminationTemplateBlockQuestion
            // 
            this.labelExaminationTemplateBlockQuestion.AutoSize = true;
            this.labelExaminationTemplateBlockQuestion.Location = new System.Drawing.Point(12, 36);
            this.labelExaminationTemplateBlockQuestion.Name = "labelExaminationTemplateBlockQuestion";
            this.labelExaminationTemplateBlockQuestion.Size = new System.Drawing.Size(51, 13);
            this.labelExaminationTemplateBlockQuestion.TabIndex = 2;
            this.labelExaminationTemplateBlockQuestion.Text = "Вопрос*:";
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.Location = new System.Drawing.Point(12, 62);
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
            this.examinationTemplateTicketElement.Location = new System.Drawing.Point(131, 7);
            this.examinationTemplateTicketElement.Name = "examinationTemplateTicketElement";
            this.examinationTemplateTicketElement.Size = new System.Drawing.Size(300, 20);
            this.examinationTemplateTicketElement.TabIndex = 1;
            // 
            // examinationTemplateBlockQuestionElement
            // 
            this.examinationTemplateBlockQuestionElement.ExaminationTemplateBlockId = null;
            this.examinationTemplateBlockQuestionElement.ExaminationTemplateId = null;
            this.examinationTemplateBlockQuestionElement.Id = null;
            this.examinationTemplateBlockQuestionElement.Location = new System.Drawing.Point(131, 33);
            this.examinationTemplateBlockQuestionElement.Name = "examinationTemplateBlockQuestionElement";
            this.examinationTemplateBlockQuestionElement.Size = new System.Drawing.Size(300, 20);
            this.examinationTemplateBlockQuestionElement.TabIndex = 3;
            // 
            // numericUpDownOrder
            // 
            this.numericUpDownOrder.Location = new System.Drawing.Point(131, 60);
            this.numericUpDownOrder.Name = "numericUpDownOrder";
            this.numericUpDownOrder.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownOrder.TabIndex = 5;
            // 
            // ExaminationTemplateTicketQuestionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 131);
            this.Controls.Add(this.numericUpDownOrder);
            this.Controls.Add(this.examinationTemplateBlockQuestionElement);
            this.Controls.Add(this.examinationTemplateTicketElement);
            this.Controls.Add(this.labelExaminationTemplateTicket);
            this.Controls.Add(this.labelExaminationTemplateBlockQuestion);
            this.Controls.Add(this.labelOrder);
            this.Name = "ExaminationTemplateTicketQuestionForm";
            this.Text = "Вопрос билета";
            this.Load += new System.EventHandler(this.FormExaminationTemplateTicketQuestion_Load);
            this.Controls.SetChildIndex(this.labelOrder, 0);
            this.Controls.SetChildIndex(this.labelExaminationTemplateBlockQuestion, 0);
            this.Controls.SetChildIndex(this.labelExaminationTemplateTicket, 0);
            this.Controls.SetChildIndex(this.examinationTemplateTicketElement, 0);
            this.Controls.SetChildIndex(this.examinationTemplateBlockQuestionElement, 0);
            this.Controls.SetChildIndex(this.numericUpDownOrder, 0);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.buttonSaveAndClose, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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