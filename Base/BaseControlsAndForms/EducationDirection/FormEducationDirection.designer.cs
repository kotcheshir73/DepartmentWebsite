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
            this.labelQualification = new System.Windows.Forms.Label();
            this.comboBoxQualification = new System.Windows.Forms.ComboBox();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.comboBoxQualification);
            this.panelMain.Controls.Add(this.labelQualification);
            this.panelMain.Controls.Add(this.textBoxShortName);
            this.panelMain.Controls.Add(this.textBoxCipher);
            this.panelMain.Controls.Add(this.labelShortName);
            this.panelMain.Controls.Add(this.labelCipher);
            this.panelMain.Controls.Add(this.textBoxDescription);
            this.panelMain.Controls.Add(this.labelTitle);
            this.panelMain.Controls.Add(this.labelDescription);
            this.panelMain.Controls.Add(this.textBoxTitle);
            this.panelMain.Size = new System.Drawing.Size(404, 185);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(404, 36);
            // 
            // labelCipher
            // 
            this.labelCipher.AutoSize = true;
            this.labelCipher.Location = new System.Drawing.Point(11, 9);
            this.labelCipher.Name = "labelCipher";
            this.labelCipher.Size = new System.Drawing.Size(43, 13);
            this.labelCipher.TabIndex = 0;
            this.labelCipher.Text = "Шифр*:";
            // 
            // textBoxCipher
            // 
            this.textBoxCipher.Location = new System.Drawing.Point(102, 6);
            this.textBoxCipher.MaxLength = 10;
            this.textBoxCipher.Name = "textBoxCipher";
            this.textBoxCipher.Size = new System.Drawing.Size(100, 20);
            this.textBoxCipher.TabIndex = 1;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(102, 32);
            this.textBoxTitle.MaxLength = 100;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(290, 20);
            this.textBoxTitle.TabIndex = 5;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(11, 35);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(64, 13);
            this.labelTitle.TabIndex = 4;
            this.labelTitle.Text = "Название*:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(102, 85);
            this.textBoxDescription.MaxLength = 10000;
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(290, 91);
            this.textBoxDescription.TabIndex = 9;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(11, 88);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(60, 13);
            this.labelDescription.TabIndex = 8;
            this.labelDescription.Text = "Описание:";
            // 
            // textBoxShortName
            // 
            this.textBoxShortName.Location = new System.Drawing.Point(292, 6);
            this.textBoxShortName.MaxLength = 10;
            this.textBoxShortName.Name = "textBoxShortName";
            this.textBoxShortName.Size = new System.Drawing.Size(100, 20);
            this.textBoxShortName.TabIndex = 3;
            // 
            // labelShortName
            // 
            this.labelShortName.AutoSize = true;
            this.labelShortName.Location = new System.Drawing.Point(228, 9);
            this.labelShortName.Name = "labelShortName";
            this.labelShortName.Size = new System.Drawing.Size(50, 13);
            this.labelShortName.TabIndex = 2;
            this.labelShortName.Text = "Кратко*:";
            // 
            // labelQualification
            // 
            this.labelQualification.AutoSize = true;
            this.labelQualification.Location = new System.Drawing.Point(11, 61);
            this.labelQualification.Name = "labelQualification";
            this.labelQualification.Size = new System.Drawing.Size(85, 13);
            this.labelQualification.TabIndex = 6;
            this.labelQualification.Text = "Квалификация:";
            // 
            // comboBoxQualification
            // 
            this.comboBoxQualification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxQualification.FormattingEnabled = true;
            this.comboBoxQualification.Location = new System.Drawing.Point(102, 58);
            this.comboBoxQualification.Name = "comboBoxQualification";
            this.comboBoxQualification.Size = new System.Drawing.Size(290, 21);
            this.comboBoxQualification.TabIndex = 7;
            // 
            // FormEducationDirection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 221);
            this.Name = "FormEducationDirection";
            this.Text = "Направление обучения";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.ComboBox comboBoxQualification;
        private System.Windows.Forms.Label labelQualification;
    }
}