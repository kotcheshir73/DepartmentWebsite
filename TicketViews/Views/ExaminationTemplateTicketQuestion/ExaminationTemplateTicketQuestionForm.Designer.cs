namespace TicketViews.Views.ExaminationTemplateTicketQuestion
{
    partial class ExaminationTemplateTicketQuestionForm
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
            this.labelExaminationTemplateTicket = new System.Windows.Forms.Label();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.labelExaminationTemplateBlockQuestion = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelOrder = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.examinationTemplateTicketElement = new TicketViews.Views.ExaminationTemplateTicket.ExaminationTemplateTicketElement();
            this.examinationTemplateBlockQuestionElement = new TicketViews.Views.ExaminationTemplateBlockQuestion.ExaminationTemplateBlockQuestionElement();
            this.numericUpDownOrder = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).BeginInit();
            this.SuspendLayout();
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
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(141, 100);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 7;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.ButtonSaveAndClose_Click);
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
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(288, 100);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 8;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
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
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(60, 100);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // examinationTemplateTicketElement
            // 
            this.examinationTemplateTicketElement.Enabled = false;
            this.examinationTemplateTicketElement.ExaminationTemplateId = null;
            this.examinationTemplateTicketElement.Location = new System.Drawing.Point(131, 7);
            this.examinationTemplateTicketElement.Name = "examinationTemplateTicketElement";
            this.examinationTemplateTicketElement.Size = new System.Drawing.Size(300, 20);
            this.examinationTemplateTicketElement.TabIndex = 1;
            // 
            // examinationTemplateBlockQuestionElement
            // 
            this.examinationTemplateBlockQuestionElement.ExaminationTemplateBlockId = null;
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
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.labelExaminationTemplateBlockQuestion);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.labelOrder);
            this.Controls.Add(this.buttonSave);
            this.Name = "ExaminationTemplateTicketQuestionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вопрос билета";
            this.Load += new System.EventHandler(this.ExaminationTemplateTicketQuestionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelExaminationTemplateTicket;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Label labelExaminationTemplateBlockQuestion;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelOrder;
        private System.Windows.Forms.Button buttonSave;
        private ExaminationTemplateTicket.ExaminationTemplateTicketElement examinationTemplateTicketElement;
        private ExaminationTemplateBlockQuestion.ExaminationTemplateBlockQuestionElement examinationTemplateBlockQuestionElement;
        private System.Windows.Forms.NumericUpDown numericUpDownOrder;
    }
}