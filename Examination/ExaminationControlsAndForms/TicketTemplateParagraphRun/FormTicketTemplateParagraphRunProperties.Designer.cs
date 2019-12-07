namespace ExaminationControlsAndForms.TicketTemplateParagraphRun
{
    partial class FormTicketTemplateParagraphRunProperties
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
            this.textBoxSize = new System.Windows.Forms.TextBox();
            this.labelSize = new System.Windows.Forms.Label();
            this.checkBoxUnderline = new System.Windows.Forms.CheckBox();
            this.checkBoxItalic = new System.Windows.Forms.CheckBox();
            this.checkBoxBold = new System.Windows.Forms.CheckBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxSize
            // 
            this.textBoxSize.Location = new System.Drawing.Point(150, 36);
            this.textBoxSize.Name = "textBoxSize";
            this.textBoxSize.Size = new System.Drawing.Size(50, 20);
            this.textBoxSize.TabIndex = 4;
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(95, 39);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(49, 13);
            this.labelSize.TabIndex = 3;
            this.labelSize.Text = "Размер:";
            // 
            // checkBoxUnderline
            // 
            this.checkBoxUnderline.AutoSize = true;
            this.checkBoxUnderline.Location = new System.Drawing.Point(98, 12);
            this.checkBoxUnderline.Name = "checkBoxUnderline";
            this.checkBoxUnderline.Size = new System.Drawing.Size(91, 17);
            this.checkBoxUnderline.TabIndex = 1;
            this.checkBoxUnderline.Text = "Подчеркнуто";
            this.checkBoxUnderline.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxUnderline.UseVisualStyleBackColor = true;
            // 
            // checkBoxItalic
            // 
            this.checkBoxItalic.AutoSize = true;
            this.checkBoxItalic.Location = new System.Drawing.Point(12, 38);
            this.checkBoxItalic.Name = "checkBoxItalic";
            this.checkBoxItalic.Size = new System.Drawing.Size(62, 17);
            this.checkBoxItalic.TabIndex = 2;
            this.checkBoxItalic.Text = "Курсив";
            this.checkBoxItalic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxItalic.UseVisualStyleBackColor = true;
            // 
            // checkBoxBold
            // 
            this.checkBoxBold.AutoSize = true;
            this.checkBoxBold.Location = new System.Drawing.Point(12, 12);
            this.checkBoxBold.Name = "checkBoxBold";
            this.checkBoxBold.Size = new System.Drawing.Size(69, 17);
            this.checkBoxBold.TabIndex = 0;
            this.checkBoxBold.Text = "Жирный";
            this.checkBoxBold.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxBold.UseVisualStyleBackColor = true;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(140, 62);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // FormTicketTemplateParagraphRunProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 97);
            this.ControlBox = false;
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.textBoxSize);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.checkBoxUnderline);
            this.Controls.Add(this.checkBoxItalic);
            this.Controls.Add(this.checkBoxBold);
            this.Name = "FormTicketTemplateParagraphRunProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Свойства текста";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSize;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.CheckBox checkBoxUnderline;
        private System.Windows.Forms.CheckBox checkBoxItalic;
        private System.Windows.Forms.CheckBox checkBoxBold;
        private System.Windows.Forms.Button buttonClose;
    }
}