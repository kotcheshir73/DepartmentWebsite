namespace ExaminationControlsAndForms.ExaminationTemplateBlockQuestion
{
    partial class FormExaminationTemplateBlockQuestion
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
            this.numericUpDownQuestionNumber = new System.Windows.Forms.NumericUpDown();
            this.labelExaminationTemplateTicket = new System.Windows.Forms.Label();
            this.labelQuestionNumber = new System.Windows.Forms.Label();
            this.examinationTemplateBlockElement = new ExaminationControlsAndForms.ExaminationTemplateBlock.ControlExaminationTemplateBlockSearch();
            this.labelQuestionText = new System.Windows.Forms.Label();
            this.textBoxQuestionText = new System.Windows.Forms.TextBox();
            this.labelQuestionImage = new System.Windows.Forms.Label();
            this.pictureBoxQuestionImage = new System.Windows.Forms.PictureBox();
            this.buttonLoadImage = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuestionNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQuestionImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.buttonLoadImage);
            this.panelMain.Controls.Add(this.labelExaminationTemplateTicket);
            this.panelMain.Controls.Add(this.pictureBoxQuestionImage);
            this.panelMain.Controls.Add(this.labelQuestionNumber);
            this.panelMain.Controls.Add(this.labelQuestionImage);
            this.panelMain.Controls.Add(this.numericUpDownQuestionNumber);
            this.panelMain.Controls.Add(this.textBoxQuestionText);
            this.panelMain.Controls.Add(this.examinationTemplateBlockElement);
            this.panelMain.Controls.Add(this.labelQuestionText);
            this.panelMain.Size = new System.Drawing.Size(444, 255);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(444, 36);
            // 
            // numericUpDownQuestionNumber
            // 
            this.numericUpDownQuestionNumber.Location = new System.Drawing.Point(131, 39);
            this.numericUpDownQuestionNumber.Name = "numericUpDownQuestionNumber";
            this.numericUpDownQuestionNumber.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownQuestionNumber.TabIndex = 3;
            // 
            // labelExaminationTemplateTicket
            // 
            this.labelExaminationTemplateTicket.AutoSize = true;
            this.labelExaminationTemplateTicket.Location = new System.Drawing.Point(12, 15);
            this.labelExaminationTemplateTicket.Name = "labelExaminationTemplateTicket";
            this.labelExaminationTemplateTicket.Size = new System.Drawing.Size(39, 13);
            this.labelExaminationTemplateTicket.TabIndex = 0;
            this.labelExaminationTemplateTicket.Text = "Блок*:";
            // 
            // labelQuestionNumber
            // 
            this.labelQuestionNumber.AutoSize = true;
            this.labelQuestionNumber.Location = new System.Drawing.Point(12, 41);
            this.labelQuestionNumber.Name = "labelQuestionNumber";
            this.labelQuestionNumber.Size = new System.Drawing.Size(93, 13);
            this.labelQuestionNumber.TabIndex = 2;
            this.labelQuestionNumber.Text = "Номер вопроса*:";
            // 
            // examinationTemplateBlockElement
            // 
            this.examinationTemplateBlockElement.ExaminationTemplateId = null;
            this.examinationTemplateBlockElement.Id = null;
            this.examinationTemplateBlockElement.Location = new System.Drawing.Point(131, 13);
            this.examinationTemplateBlockElement.Name = "examinationTemplateBlockElement";
            this.examinationTemplateBlockElement.Service = null;
            this.examinationTemplateBlockElement.Size = new System.Drawing.Size(300, 20);
            this.examinationTemplateBlockElement.TabIndex = 1;
            // 
            // labelQuestionText
            // 
            this.labelQuestionText.AutoSize = true;
            this.labelQuestionText.Location = new System.Drawing.Point(12, 68);
            this.labelQuestionText.Name = "labelQuestionText";
            this.labelQuestionText.Size = new System.Drawing.Size(85, 13);
            this.labelQuestionText.TabIndex = 4;
            this.labelQuestionText.Text = "Текст вопроса:";
            // 
            // textBoxQuestionText
            // 
            this.textBoxQuestionText.Location = new System.Drawing.Point(131, 65);
            this.textBoxQuestionText.Multiline = true;
            this.textBoxQuestionText.Name = "textBoxQuestionText";
            this.textBoxQuestionText.Size = new System.Drawing.Size(300, 40);
            this.textBoxQuestionText.TabIndex = 5;
            // 
            // labelQuestionImage
            // 
            this.labelQuestionImage.AutoSize = true;
            this.labelQuestionImage.Location = new System.Drawing.Point(12, 114);
            this.labelQuestionImage.Name = "labelQuestionImage";
            this.labelQuestionImage.Size = new System.Drawing.Size(80, 13);
            this.labelQuestionImage.TabIndex = 6;
            this.labelQuestionImage.Text = "Изображение:";
            // 
            // pictureBoxQuestionImage
            // 
            this.pictureBoxQuestionImage.Location = new System.Drawing.Point(131, 111);
            this.pictureBoxQuestionImage.Name = "pictureBoxQuestionImage";
            this.pictureBoxQuestionImage.Size = new System.Drawing.Size(300, 131);
            this.pictureBoxQuestionImage.TabIndex = 7;
            this.pictureBoxQuestionImage.TabStop = false;
            // 
            // buttonLoadImage
            // 
            this.buttonLoadImage.Location = new System.Drawing.Point(22, 163);
            this.buttonLoadImage.Name = "buttonLoadImage";
            this.buttonLoadImage.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadImage.TabIndex = 8;
            this.buttonLoadImage.Text = "Загрузить";
            this.buttonLoadImage.UseVisualStyleBackColor = true;
            this.buttonLoadImage.Click += new System.EventHandler(this.ButtonLoadImage_Click);
            // 
            // FormExaminationTemplateBlockQuestion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 291);
            this.Name = "FormExaminationTemplateBlockQuestion";
            this.Text = "Вопрос блока";
            this.Load += new System.EventHandler(this.FormExaminationTemplateBlockQuestion_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuestionNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQuestionImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownQuestionNumber;
        private System.Windows.Forms.Label labelExaminationTemplateTicket;
        private System.Windows.Forms.Label labelQuestionNumber;
        private ExaminationTemplateBlock.ControlExaminationTemplateBlockSearch examinationTemplateBlockElement;
        private System.Windows.Forms.Label labelQuestionText;
        private System.Windows.Forms.TextBox textBoxQuestionText;
        private System.Windows.Forms.Label labelQuestionImage;
        private System.Windows.Forms.PictureBox pictureBoxQuestionImage;
        private System.Windows.Forms.Button buttonLoadImage;
    }
}