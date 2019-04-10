namespace BaseControlsAndForms.EducationDirection
{
    partial class FormEducationDirection
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
            this.labelCipher = new System.Windows.Forms.Label();
            this.textBoxCipher = new System.Windows.Forms.TextBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxShortName = new System.Windows.Forms.TextBox();
            this.labelShortName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(131, 155);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(278, 155);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(50, 155);
            // 
            // labelCipher
            // 
            this.labelCipher.AutoSize = true;
            this.labelCipher.Location = new System.Drawing.Point(12, 9);
            this.labelCipher.Name = "labelCipher";
            this.labelCipher.Size = new System.Drawing.Size(43, 13);
            this.labelCipher.TabIndex = 0;
            this.labelCipher.Text = "Шифр*:";
            // 
            // textBoxCipher
            // 
            this.textBoxCipher.Location = new System.Drawing.Point(99, 6);
            this.textBoxCipher.MaxLength = 10;
            this.textBoxCipher.Name = "textBoxCipher";
            this.textBoxCipher.Size = new System.Drawing.Size(100, 20);
            this.textBoxCipher.TabIndex = 1;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(99, 32);
            this.textBoxTitle.MaxLength = 100;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(290, 20);
            this.textBoxTitle.TabIndex = 5;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(12, 35);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(64, 13);
            this.labelTitle.TabIndex = 4;
            this.labelTitle.Text = "Название*:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(99, 58);
            this.textBoxDescription.MaxLength = 10000;
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(290, 91);
            this.textBoxDescription.TabIndex = 7;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(12, 61);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(60, 13);
            this.labelDescription.TabIndex = 6;
            this.labelDescription.Text = "Описание:";
            // 
            // textBoxShortName
            // 
            this.textBoxShortName.Location = new System.Drawing.Point(289, 6);
            this.textBoxShortName.MaxLength = 10;
            this.textBoxShortName.Name = "textBoxShortName";
            this.textBoxShortName.Size = new System.Drawing.Size(100, 20);
            this.textBoxShortName.TabIndex = 3;
            // 
            // labelShortName
            // 
            this.labelShortName.AutoSize = true;
            this.labelShortName.Location = new System.Drawing.Point(225, 9);
            this.labelShortName.Name = "labelShortName";
            this.labelShortName.Size = new System.Drawing.Size(50, 13);
            this.labelShortName.TabIndex = 2;
            this.labelShortName.Text = "Кратко*:";
            // 
            // FormEducationDirection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 182);
            this.Controls.Add(this.textBoxShortName);
            this.Controls.Add(this.labelShortName);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.textBoxCipher);
            this.Controls.Add(this.labelCipher);
            this.Name = "FormEducationDirection";
            this.Text = "Направление обучения";
            this.Load += new System.EventHandler(this.FormEducationDirection_Load);
            this.Controls.SetChildIndex(this.labelCipher, 0);
            this.Controls.SetChildIndex(this.textBoxCipher, 0);
            this.Controls.SetChildIndex(this.labelTitle, 0);
            this.Controls.SetChildIndex(this.textBoxTitle, 0);
            this.Controls.SetChildIndex(this.labelDescription, 0);
            this.Controls.SetChildIndex(this.textBoxDescription, 0);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.buttonSaveAndClose, 0);
            this.Controls.SetChildIndex(this.labelShortName, 0);
            this.Controls.SetChildIndex(this.textBoxShortName, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCipher;
        private System.Windows.Forms.TextBox textBoxCipher;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxShortName;
        private System.Windows.Forms.Label labelShortName;
    }
}