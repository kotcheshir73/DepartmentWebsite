namespace TicketViews.Views.ExaminationTemplateBlockQuestion
{
    partial class ExaminationTemplateBlockQuestionForm
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
            this.numericUpDownQuestionNumber = new System.Windows.Forms.NumericUpDown();
            this.labelExaminationTemplateTicket = new System.Windows.Forms.Label();
            this.labelQuestionNumber = new System.Windows.Forms.Label();
            this.examinationTemplateBlockElement = new TicketViews.Views.ExaminationTemplateBlock.ExaminationTemplateBlockElement();
            this.labelQuestionText = new System.Windows.Forms.Label();
            this.textBoxQuestionText = new System.Windows.Forms.TextBox();
            this.labelQuestionImage = new System.Windows.Forms.Label();
            this.pictureBoxQuestionImage = new System.Windows.Forms.PictureBox();
            this.buttonLoadImage = new System.Windows.Forms.Button();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuestionNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQuestionImage)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownQuestionNumber
            // 
            this.numericUpDownQuestionNumber.Location = new System.Drawing.Point(131, 33);
            this.numericUpDownQuestionNumber.Name = "numericUpDownQuestionNumber";
            this.numericUpDownQuestionNumber.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownQuestionNumber.TabIndex = 3;
            // 
            // labelExaminationTemplateTicket
            // 
            this.labelExaminationTemplateTicket.AutoSize = true;
            this.labelExaminationTemplateTicket.Location = new System.Drawing.Point(12, 9);
            this.labelExaminationTemplateTicket.Name = "labelExaminationTemplateTicket";
            this.labelExaminationTemplateTicket.Size = new System.Drawing.Size(39, 13);
            this.labelExaminationTemplateTicket.TabIndex = 0;
            this.labelExaminationTemplateTicket.Text = "Блок*:";
            // 
            // labelQuestionNumber
            // 
            this.labelQuestionNumber.AutoSize = true;
            this.labelQuestionNumber.Location = new System.Drawing.Point(12, 35);
            this.labelQuestionNumber.Name = "labelQuestionNumber";
            this.labelQuestionNumber.Size = new System.Drawing.Size(93, 13);
            this.labelQuestionNumber.TabIndex = 2;
            this.labelQuestionNumber.Text = "Номер вопроса*:";
            // 
            // examinationTemplateBlockElement
            // 
            this.examinationTemplateBlockElement.ExaminationTemplateId = null;
            this.examinationTemplateBlockElement.Id = null;
            this.examinationTemplateBlockElement.Location = new System.Drawing.Point(131, 7);
            this.examinationTemplateBlockElement.Name = "examinationTemplateBlockElement";
            this.examinationTemplateBlockElement.Size = new System.Drawing.Size(300, 20);
            this.examinationTemplateBlockElement.TabIndex = 1;
            // 
            // labelQuestionText
            // 
            this.labelQuestionText.AutoSize = true;
            this.labelQuestionText.Location = new System.Drawing.Point(12, 62);
            this.labelQuestionText.Name = "labelQuestionText";
            this.labelQuestionText.Size = new System.Drawing.Size(85, 13);
            this.labelQuestionText.TabIndex = 4;
            this.labelQuestionText.Text = "Текст вопроса:";
            // 
            // textBoxQuestionText
            // 
            this.textBoxQuestionText.Location = new System.Drawing.Point(131, 59);
            this.textBoxQuestionText.Multiline = true;
            this.textBoxQuestionText.Name = "textBoxQuestionText";
            this.textBoxQuestionText.Size = new System.Drawing.Size(300, 40);
            this.textBoxQuestionText.TabIndex = 5;
            // 
            // labelQuestionImage
            // 
            this.labelQuestionImage.AutoSize = true;
            this.labelQuestionImage.Location = new System.Drawing.Point(12, 108);
            this.labelQuestionImage.Name = "labelQuestionImage";
            this.labelQuestionImage.Size = new System.Drawing.Size(80, 13);
            this.labelQuestionImage.TabIndex = 6;
            this.labelQuestionImage.Text = "Изображение:";
            // 
            // pictureBoxQuestionImage
            // 
            this.pictureBoxQuestionImage.Location = new System.Drawing.Point(131, 105);
            this.pictureBoxQuestionImage.Name = "pictureBoxQuestionImage";
            this.pictureBoxQuestionImage.Size = new System.Drawing.Size(300, 131);
            this.pictureBoxQuestionImage.TabIndex = 7;
            this.pictureBoxQuestionImage.TabStop = false;
            // 
            // buttonLoadImage
            // 
            this.buttonLoadImage.Location = new System.Drawing.Point(22, 157);
            this.buttonLoadImage.Name = "buttonLoadImage";
            this.buttonLoadImage.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadImage.TabIndex = 8;
            this.buttonLoadImage.Text = "Загрузить";
            this.buttonLoadImage.UseVisualStyleBackColor = true;
            this.buttonLoadImage.Click += new System.EventHandler(this.ButtonLoadImage_Click);
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(155, 256);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 10;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.ButtonSaveAndClose_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(302, 256);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(74, 256);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ExaminationTemplateBlockQuestionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 291);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonLoadImage);
            this.Controls.Add(this.pictureBoxQuestionImage);
            this.Controls.Add(this.labelQuestionImage);
            this.Controls.Add(this.textBoxQuestionText);
            this.Controls.Add(this.labelQuestionText);
            this.Controls.Add(this.examinationTemplateBlockElement);
            this.Controls.Add(this.numericUpDownQuestionNumber);
            this.Controls.Add(this.labelExaminationTemplateTicket);
            this.Controls.Add(this.labelQuestionNumber);
            this.Name = "ExaminationTemplateBlockQuestionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вопрос блока";
            this.Load += new System.EventHandler(this.ExaminationTemplateBlockQuestionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuestionNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQuestionImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownQuestionNumber;
        private System.Windows.Forms.Label labelExaminationTemplateTicket;
        private System.Windows.Forms.Label labelQuestionNumber;
        private ExaminationTemplateBlock.ExaminationTemplateBlockElement examinationTemplateBlockElement;
        private System.Windows.Forms.Label labelQuestionText;
        private System.Windows.Forms.TextBox textBoxQuestionText;
        private System.Windows.Forms.Label labelQuestionImage;
        private System.Windows.Forms.PictureBox pictureBoxQuestionImage;
        private System.Windows.Forms.Button buttonLoadImage;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
    }
}