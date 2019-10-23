namespace LaboratoryHeadControlsAndForms.SoftwareRecord
{
    partial class FormSoftwareRecord
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
            this.labelSoftware = new System.Windows.Forms.Label();
            this.comboBoxMaterialTechnicalValue = new System.Windows.Forms.ComboBox();
            this.labelMaterialTechnicalValue = new System.Windows.Forms.Label();
            this.textBoxClaimNumber = new System.Windows.Forms.TextBox();
            this.labelClaimNumber = new System.Windows.Forms.Label();
            this.labelDateSetup = new System.Windows.Forms.Label();
            this.dateTimePickerDateSetup = new System.Windows.Forms.DateTimePicker();
            this.labelSetupDescription = new System.Windows.Forms.Label();
            this.textBoxSetupDescription = new System.Windows.Forms.TextBox();
            this.comboBoxSoftware = new System.Windows.Forms.ComboBox();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.comboBoxSoftware);
            this.panelMain.Controls.Add(this.textBoxSetupDescription);
            this.panelMain.Controls.Add(this.labelSetupDescription);
            this.panelMain.Controls.Add(this.dateTimePickerDateSetup);
            this.panelMain.Controls.Add(this.labelDateSetup);
            this.panelMain.Controls.Add(this.textBoxClaimNumber);
            this.panelMain.Controls.Add(this.labelClaimNumber);
            this.panelMain.Controls.Add(this.labelSoftware);
            this.panelMain.Controls.Add(this.comboBoxMaterialTechnicalValue);
            this.panelMain.Controls.Add(this.labelMaterialTechnicalValue);
            this.panelMain.Size = new System.Drawing.Size(364, 65);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(364, 36);
            // 
            // labelSoftware
            // 
            this.labelSoftware.AutoSize = true;
            this.labelSoftware.Location = new System.Drawing.Point(12, 36);
            this.labelSoftware.Name = "labelSoftware";
            this.labelSoftware.Size = new System.Drawing.Size(30, 13);
            this.labelSoftware.TabIndex = 2;
            this.labelSoftware.Text = "ПО*:";
            // 
            // comboBoxMaterialTechnicalValue
            // 
            this.comboBoxMaterialTechnicalValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMaterialTechnicalValue.FormattingEnabled = true;
            this.comboBoxMaterialTechnicalValue.Location = new System.Drawing.Point(136, 6);
            this.comboBoxMaterialTechnicalValue.Name = "comboBoxMaterialTechnicalValue";
            this.comboBoxMaterialTechnicalValue.Size = new System.Drawing.Size(210, 21);
            this.comboBoxMaterialTechnicalValue.TabIndex = 1;
            // 
            // labelMaterialTechnicalValue
            // 
            this.labelMaterialTechnicalValue.AutoSize = true;
            this.labelMaterialTechnicalValue.Location = new System.Drawing.Point(12, 9);
            this.labelMaterialTechnicalValue.Name = "labelMaterialTechnicalValue";
            this.labelMaterialTechnicalValue.Size = new System.Drawing.Size(90, 13);
            this.labelMaterialTechnicalValue.TabIndex = 0;
            this.labelMaterialTechnicalValue.Text = "Инв. номер ПК*:";
            // 
            // textBoxClaimNumber
            // 
            this.textBoxClaimNumber.Location = new System.Drawing.Point(136, 132);
            this.textBoxClaimNumber.Name = "textBoxClaimNumber";
            this.textBoxClaimNumber.Size = new System.Drawing.Size(210, 20);
            this.textBoxClaimNumber.TabIndex = 9;
            // 
            // labelClaimNumber
            // 
            this.labelClaimNumber.AutoSize = true;
            this.labelClaimNumber.Location = new System.Drawing.Point(12, 135);
            this.labelClaimNumber.Name = "labelClaimNumber";
            this.labelClaimNumber.Size = new System.Drawing.Size(83, 13);
            this.labelClaimNumber.TabIndex = 8;
            this.labelClaimNumber.Text = "Номер заявки:";
            // 
            // labelDateSetup
            // 
            this.labelDateSetup.AutoSize = true;
            this.labelDateSetup.Location = new System.Drawing.Point(12, 64);
            this.labelDateSetup.Name = "labelDateSetup";
            this.labelDateSetup.Size = new System.Drawing.Size(91, 13);
            this.labelDateSetup.TabIndex = 4;
            this.labelDateSetup.Text = "Дата установки:";
            // 
            // dateTimePickerDateSetup
            // 
            this.dateTimePickerDateSetup.Location = new System.Drawing.Point(136, 60);
            this.dateTimePickerDateSetup.Name = "dateTimePickerDateSetup";
            this.dateTimePickerDateSetup.Size = new System.Drawing.Size(210, 20);
            this.dateTimePickerDateSetup.TabIndex = 5;
            // 
            // labelSetupDescription
            // 
            this.labelSetupDescription.AutoSize = true;
            this.labelSetupDescription.Location = new System.Drawing.Point(12, 89);
            this.labelSetupDescription.Name = "labelSetupDescription";
            this.labelSetupDescription.Size = new System.Drawing.Size(77, 26);
            this.labelSetupDescription.TabIndex = 6;
            this.labelSetupDescription.Text = "Особенности:\r\nустановки";
            // 
            // textBoxSetupDescription
            // 
            this.textBoxSetupDescription.Location = new System.Drawing.Point(136, 86);
            this.textBoxSetupDescription.Multiline = true;
            this.textBoxSetupDescription.Name = "textBoxSetupDescription";
            this.textBoxSetupDescription.Size = new System.Drawing.Size(210, 40);
            this.textBoxSetupDescription.TabIndex = 7;
            // 
            // comboBoxSoftware
            // 
            this.comboBoxSoftware.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSoftware.FormattingEnabled = true;
            this.comboBoxSoftware.Location = new System.Drawing.Point(136, 33);
            this.comboBoxSoftware.Name = "comboBoxSoftware";
            this.comboBoxSoftware.Size = new System.Drawing.Size(210, 21);
            this.comboBoxSoftware.TabIndex = 3;
            // 
            // SoftwareRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 202);
            this.Name = "SoftwareRecordForm";
            this.Text = "ПО";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelSoftware;
        private System.Windows.Forms.ComboBox comboBoxMaterialTechnicalValue;
        private System.Windows.Forms.Label labelMaterialTechnicalValue;
        private System.Windows.Forms.TextBox textBoxClaimNumber;
        private System.Windows.Forms.Label labelClaimNumber;
        private System.Windows.Forms.Label labelDateSetup;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateSetup;
        private System.Windows.Forms.Label labelSetupDescription;
        private System.Windows.Forms.TextBox textBoxSetupDescription;
        private System.Windows.Forms.ComboBox comboBoxSoftware;
    }
}