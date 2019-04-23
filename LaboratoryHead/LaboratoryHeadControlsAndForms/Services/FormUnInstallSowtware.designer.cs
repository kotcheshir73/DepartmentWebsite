namespace LaboratoryHeadControlsAndForms.Services
{
    partial class FormUnInstallSowtware
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
            this.checkedListBoxSoftwares = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxInvNumbers = new System.Windows.Forms.CheckedListBox();
            this.buttonInventoryNumberSearch = new System.Windows.Forms.Button();
            this.textBoxInventoryNumberSearch = new System.Windows.Forms.TextBox();
            this.labelInventoryNumberSearch = new System.Windows.Forms.Label();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonSoftwareSearch = new System.Windows.Forms.Button();
            this.textBoxDeleteReason = new System.Windows.Forms.TextBox();
            this.labelDeleteReason = new System.Windows.Forms.Label();
            this.dateTimePickerDateDelete = new System.Windows.Forms.DateTimePicker();
            this.labelDateDelete = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkedListBoxSoftwares
            // 
            this.checkedListBoxSoftwares.FormattingEnabled = true;
            this.checkedListBoxSoftwares.Location = new System.Drawing.Point(302, 3);
            this.checkedListBoxSoftwares.Name = "checkedListBoxSoftwares";
            this.checkedListBoxSoftwares.Size = new System.Drawing.Size(467, 514);
            this.checkedListBoxSoftwares.TabIndex = 5;
            // 
            // checkedListBoxInvNumbers
            // 
            this.checkedListBoxInvNumbers.FormattingEnabled = true;
            this.checkedListBoxInvNumbers.Location = new System.Drawing.Point(15, 61);
            this.checkedListBoxInvNumbers.Name = "checkedListBoxInvNumbers";
            this.checkedListBoxInvNumbers.Size = new System.Drawing.Size(262, 424);
            this.checkedListBoxInvNumbers.TabIndex = 3;
            // 
            // buttonInventoryNumberSearch
            // 
            this.buttonInventoryNumberSearch.Location = new System.Drawing.Point(176, 32);
            this.buttonInventoryNumberSearch.Name = "buttonInventoryNumberSearch";
            this.buttonInventoryNumberSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonInventoryNumberSearch.TabIndex = 2;
            this.buttonInventoryNumberSearch.Text = "Найти";
            this.buttonInventoryNumberSearch.UseVisualStyleBackColor = true;
            this.buttonInventoryNumberSearch.Click += new System.EventHandler(this.ButtonInventoryNumberSearch_Click);
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
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(810, 301);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(170, 23);
            this.buttonApply.TabIndex = 10;
            this.buttonApply.Text = "Зафиксировать";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.ButtonApply_Click);
            // 
            // buttonSoftwareSearch
            // 
            this.buttonSoftwareSearch.Location = new System.Drawing.Point(70, 491);
            this.buttonSoftwareSearch.Name = "buttonSoftwareSearch";
            this.buttonSoftwareSearch.Size = new System.Drawing.Size(170, 23);
            this.buttonSoftwareSearch.TabIndex = 4;
            this.buttonSoftwareSearch.Text = "Найто ПО";
            this.buttonSoftwareSearch.UseVisualStyleBackColor = true;
            this.buttonSoftwareSearch.Click += new System.EventHandler(this.ButtonSoftwareSearch_Click);
            // 
            // textBoxDeleteReason
            // 
            this.textBoxDeleteReason.Location = new System.Drawing.Point(787, 90);
            this.textBoxDeleteReason.Multiline = true;
            this.textBoxDeleteReason.Name = "textBoxDeleteReason";
            this.textBoxDeleteReason.Size = new System.Drawing.Size(210, 104);
            this.textBoxDeleteReason.TabIndex = 9;
            // 
            // labelDeleteReason
            // 
            this.labelDeleteReason.AutoSize = true;
            this.labelDeleteReason.Location = new System.Drawing.Point(784, 74);
            this.labelDeleteReason.Name = "labelDeleteReason";
            this.labelDeleteReason.Size = new System.Drawing.Size(103, 13);
            this.labelDeleteReason.TabIndex = 8;
            this.labelDeleteReason.Text = "Причина удаления:\r\n";
            // 
            // dateTimePickerDateDelete
            // 
            this.dateTimePickerDateDelete.Location = new System.Drawing.Point(787, 32);
            this.dateTimePickerDateDelete.Name = "dateTimePickerDateDelete";
            this.dateTimePickerDateDelete.Size = new System.Drawing.Size(210, 20);
            this.dateTimePickerDateDelete.TabIndex = 7;
            // 
            // labelDateDelete
            // 
            this.labelDateDelete.AutoSize = true;
            this.labelDateDelete.Location = new System.Drawing.Point(784, 9);
            this.labelDateDelete.Name = "labelDateDelete";
            this.labelDateDelete.Size = new System.Drawing.Size(86, 13);
            this.labelDateDelete.TabIndex = 6;
            this.labelDateDelete.Text = "Дата удаления:";
            // 
            // UnInstallSowtwareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 521);
            this.Controls.Add(this.textBoxDeleteReason);
            this.Controls.Add(this.labelDeleteReason);
            this.Controls.Add(this.dateTimePickerDateDelete);
            this.Controls.Add(this.labelDateDelete);
            this.Controls.Add(this.buttonSoftwareSearch);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.checkedListBoxSoftwares);
            this.Controls.Add(this.checkedListBoxInvNumbers);
            this.Controls.Add(this.buttonInventoryNumberSearch);
            this.Controls.Add(this.textBoxInventoryNumberSearch);
            this.Controls.Add(this.labelInventoryNumberSearch);
            this.Name = "UnInstallSowtwareForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Удаление ПО";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxSoftwares;
        private System.Windows.Forms.CheckedListBox checkedListBoxInvNumbers;
        private System.Windows.Forms.Button buttonInventoryNumberSearch;
        private System.Windows.Forms.TextBox textBoxInventoryNumberSearch;
        private System.Windows.Forms.Label labelInventoryNumberSearch;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonSoftwareSearch;
        private System.Windows.Forms.TextBox textBoxDeleteReason;
        private System.Windows.Forms.Label labelDeleteReason;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateDelete;
        private System.Windows.Forms.Label labelDateDelete;
    }
}