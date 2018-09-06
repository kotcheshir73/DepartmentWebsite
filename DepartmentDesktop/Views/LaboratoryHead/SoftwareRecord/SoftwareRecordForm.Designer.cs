namespace DepartmentDesktop.Views.LaboratoryHead.SoftwareRecord
{
    partial class SoftwareRecordForm
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
            this.labelSoftware = new System.Windows.Forms.Label();
            this.comboBoxMaterialTechnicalValue = new System.Windows.Forms.ComboBox();
            this.labelMaterialTechnicalValue = new System.Windows.Forms.Label();
            this.textBoxClaimNumber = new System.Windows.Forms.TextBox();
            this.labelClaimNumber = new System.Windows.Forms.Label();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelDateSetup = new System.Windows.Forms.Label();
            this.dateTimePickerDateSetup = new System.Windows.Forms.DateTimePicker();
            this.labelSetupDescription = new System.Windows.Forms.Label();
            this.textBoxSetupDescription = new System.Windows.Forms.TextBox();
            this.comboBoxSoftware = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
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
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(106, 168);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 11;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(253, 168);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 12;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(25, 168);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
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
            this.Controls.Add(this.comboBoxSoftware);
            this.Controls.Add(this.textBoxSetupDescription);
            this.Controls.Add(this.labelSetupDescription);
            this.Controls.Add(this.dateTimePickerDateSetup);
            this.Controls.Add(this.labelDateSetup);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxClaimNumber);
            this.Controls.Add(this.labelClaimNumber);
            this.Controls.Add(this.labelSoftware);
            this.Controls.Add(this.comboBoxMaterialTechnicalValue);
            this.Controls.Add(this.labelMaterialTechnicalValue);
            this.Name = "SoftwareRecordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ПО";
            this.Load += new System.EventHandler(this.SoftwareRecordForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelSoftware;
        private System.Windows.Forms.ComboBox comboBoxMaterialTechnicalValue;
        private System.Windows.Forms.Label labelMaterialTechnicalValue;
        private System.Windows.Forms.TextBox textBoxClaimNumber;
        private System.Windows.Forms.Label labelClaimNumber;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelDateSetup;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateSetup;
        private System.Windows.Forms.Label labelSetupDescription;
        private System.Windows.Forms.TextBox textBoxSetupDescription;
        private System.Windows.Forms.ComboBox comboBoxSoftware;
    }
}