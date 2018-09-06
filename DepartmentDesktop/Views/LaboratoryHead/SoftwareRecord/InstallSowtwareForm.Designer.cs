namespace DepartmentDesktop.Views.LaboratoryHead.SoftwareRecord
{
    partial class InstallSowtwareForm
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
            this.buttonInventoryNumberSearch = new System.Windows.Forms.Button();
            this.textBoxInventoryNumberSearch = new System.Windows.Forms.TextBox();
            this.labelInventoryNumberSearch = new System.Windows.Forms.Label();
            this.checkedListBoxInvNumbers = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxSoftwares = new System.Windows.Forms.CheckedListBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.textBoxSetupDescription = new System.Windows.Forms.TextBox();
            this.labelSetupDescription = new System.Windows.Forms.Label();
            this.dateTimePickerDateSetup = new System.Windows.Forms.DateTimePicker();
            this.labelDateSetup = new System.Windows.Forms.Label();
            this.textBoxClaimNumber = new System.Windows.Forms.TextBox();
            this.labelClaimNumber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonInventoryNumberSearch
            // 
            this.buttonInventoryNumberSearch.Location = new System.Drawing.Point(176, 32);
            this.buttonInventoryNumberSearch.Name = "buttonInventoryNumberSearch";
            this.buttonInventoryNumberSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonInventoryNumberSearch.TabIndex = 2;
            this.buttonInventoryNumberSearch.Text = "Найти";
            this.buttonInventoryNumberSearch.UseVisualStyleBackColor = true;
            this.buttonInventoryNumberSearch.Click += new System.EventHandler(this.buttonInventoryNumberSearch_Click);
            // 
            // textBoxInventoryNumberSearch
            // 
            this.textBoxInventoryNumberSearch.Location = new System.Drawing.Point(132, 6);
            this.textBoxInventoryNumberSearch.Name = "textBoxInventoryNumberSearch";
            this.textBoxInventoryNumberSearch.Size = new System.Drawing.Size(145, 20);
            this.textBoxInventoryNumberSearch.TabIndex = 1;
            // 
            // labelInventoryNumberSearch
            // 
            this.labelInventoryNumberSearch.AutoSize = true;
            this.labelInventoryNumberSearch.Location = new System.Drawing.Point(12, 9);
            this.labelInventoryNumberSearch.Name = "labelInventoryNumberSearch";
            this.labelInventoryNumberSearch.Size = new System.Drawing.Size(114, 13);
            this.labelInventoryNumberSearch.TabIndex = 0;
            this.labelInventoryNumberSearch.Text = "Инвентарный номер:";
            // 
            // checkedListBoxInvNumbers
            // 
            this.checkedListBoxInvNumbers.FormattingEnabled = true;
            this.checkedListBoxInvNumbers.Location = new System.Drawing.Point(15, 61);
            this.checkedListBoxInvNumbers.Name = "checkedListBoxInvNumbers";
            this.checkedListBoxInvNumbers.Size = new System.Drawing.Size(262, 454);
            this.checkedListBoxInvNumbers.TabIndex = 3;
            // 
            // checkedListBoxSoftwares
            // 
            this.checkedListBoxSoftwares.FormattingEnabled = true;
            this.checkedListBoxSoftwares.Location = new System.Drawing.Point(302, 3);
            this.checkedListBoxSoftwares.Name = "checkedListBoxSoftwares";
            this.checkedListBoxSoftwares.Size = new System.Drawing.Size(467, 514);
            this.checkedListBoxSoftwares.TabIndex = 4;
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(810, 301);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(170, 23);
            this.buttonApply.TabIndex = 11;
            this.buttonApply.Text = "Зафиксировать";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // textBoxSetupDescription
            // 
            this.textBoxSetupDescription.Location = new System.Drawing.Point(787, 90);
            this.textBoxSetupDescription.Multiline = true;
            this.textBoxSetupDescription.Name = "textBoxSetupDescription";
            this.textBoxSetupDescription.Size = new System.Drawing.Size(210, 104);
            this.textBoxSetupDescription.TabIndex = 8;
            // 
            // labelSetupDescription
            // 
            this.labelSetupDescription.AutoSize = true;
            this.labelSetupDescription.Location = new System.Drawing.Point(784, 74);
            this.labelSetupDescription.Name = "labelSetupDescription";
            this.labelSetupDescription.Size = new System.Drawing.Size(132, 13);
            this.labelSetupDescription.TabIndex = 7;
            this.labelSetupDescription.Text = "Особенности установки:\r\n";
            // 
            // dateTimePickerDateSetup
            // 
            this.dateTimePickerDateSetup.Location = new System.Drawing.Point(787, 32);
            this.dateTimePickerDateSetup.Name = "dateTimePickerDateSetup";
            this.dateTimePickerDateSetup.Size = new System.Drawing.Size(210, 20);
            this.dateTimePickerDateSetup.TabIndex = 6;
            // 
            // labelDateSetup
            // 
            this.labelDateSetup.AutoSize = true;
            this.labelDateSetup.Location = new System.Drawing.Point(784, 9);
            this.labelDateSetup.Name = "labelDateSetup";
            this.labelDateSetup.Size = new System.Drawing.Size(91, 13);
            this.labelDateSetup.TabIndex = 5;
            this.labelDateSetup.Text = "Дата установки:";
            // 
            // textBoxClaimNumber
            // 
            this.textBoxClaimNumber.Location = new System.Drawing.Point(788, 226);
            this.textBoxClaimNumber.Name = "textBoxClaimNumber";
            this.textBoxClaimNumber.Size = new System.Drawing.Size(210, 20);
            this.textBoxClaimNumber.TabIndex = 10;
            // 
            // labelClaimNumber
            // 
            this.labelClaimNumber.AutoSize = true;
            this.labelClaimNumber.Location = new System.Drawing.Point(793, 210);
            this.labelClaimNumber.Name = "labelClaimNumber";
            this.labelClaimNumber.Size = new System.Drawing.Size(83, 13);
            this.labelClaimNumber.TabIndex = 9;
            this.labelClaimNumber.Text = "Номер заявки:";
            // 
            // InstallSowtwareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 521);
            this.Controls.Add(this.textBoxSetupDescription);
            this.Controls.Add(this.labelSetupDescription);
            this.Controls.Add(this.dateTimePickerDateSetup);
            this.Controls.Add(this.labelDateSetup);
            this.Controls.Add(this.textBoxClaimNumber);
            this.Controls.Add(this.labelClaimNumber);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.checkedListBoxSoftwares);
            this.Controls.Add(this.checkedListBoxInvNumbers);
            this.Controls.Add(this.buttonInventoryNumberSearch);
            this.Controls.Add(this.textBoxInventoryNumberSearch);
            this.Controls.Add(this.labelInventoryNumberSearch);
            this.Name = "InstallSowtwareForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Установка ПО";
            this.Load += new System.EventHandler(this.InstallSowtwareForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonInventoryNumberSearch;
        private System.Windows.Forms.TextBox textBoxInventoryNumberSearch;
        private System.Windows.Forms.Label labelInventoryNumberSearch;
        private System.Windows.Forms.CheckedListBox checkedListBoxInvNumbers;
        private System.Windows.Forms.CheckedListBox checkedListBoxSoftwares;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.TextBox textBoxSetupDescription;
        private System.Windows.Forms.Label labelSetupDescription;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateSetup;
        private System.Windows.Forms.Label labelDateSetup;
        private System.Windows.Forms.TextBox textBoxClaimNumber;
        private System.Windows.Forms.Label labelClaimNumber;
    }
}