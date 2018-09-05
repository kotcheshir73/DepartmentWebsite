namespace DepartmentDesktop.Views.LaboratoryHead.SoftwareRecord
{
    partial class SoftwareRecordDelSoftwareForm
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
            this.labelSelectSoftware = new System.Windows.Forms.Label();
            this.comboBoxSelectSoftware = new System.Windows.Forms.ComboBox();
            this.dataGridViewSelectedInventoryNumbers = new System.Windows.Forms.DataGridView();
            this.ColumnSelectedInventoryNumbersId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSelectedInventoryNumbers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonAddInvenotyNumbers = new System.Windows.Forms.Button();
            this.dataGridViewFindInventoryNumbers = new System.Windows.Forms.DataGridView();
            this.ColumnFindInventoryNumberId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFindInventoryNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonInventoryNumberSearch = new System.Windows.Forms.Button();
            this.textBoxInventoryNumberSearch = new System.Windows.Forms.TextBox();
            this.labelInventoryNumberSearch = new System.Windows.Forms.Label();
            this.buttonApply = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelectedInventoryNumbers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFindInventoryNumbers)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSelectSoftware
            // 
            this.labelSelectSoftware.AutoSize = true;
            this.labelSelectSoftware.Location = new System.Drawing.Point(12, 9);
            this.labelSelectSoftware.Name = "labelSelectSoftware";
            this.labelSelectSoftware.Size = new System.Drawing.Size(73, 13);
            this.labelSelectSoftware.TabIndex = 0;
            this.labelSelectSoftware.Text = "Выбрать ПО:";
            // 
            // comboBoxSelectSoftware
            // 
            this.comboBoxSelectSoftware.FormattingEnabled = true;
            this.comboBoxSelectSoftware.Location = new System.Drawing.Point(91, 6);
            this.comboBoxSelectSoftware.Name = "comboBoxSelectSoftware";
            this.comboBoxSelectSoftware.Size = new System.Drawing.Size(516, 21);
            this.comboBoxSelectSoftware.TabIndex = 1;
            // 
            // dataGridViewSelectedInventoryNumbers
            // 
            this.dataGridViewSelectedInventoryNumbers.AllowUserToAddRows = false;
            this.dataGridViewSelectedInventoryNumbers.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dataGridViewSelectedInventoryNumbers.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewSelectedInventoryNumbers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSelectedInventoryNumbers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSelectedInventoryNumbersId,
            this.ColumnSelectedInventoryNumbers});
            this.dataGridViewSelectedInventoryNumbers.Location = new System.Drawing.Point(380, 63);
            this.dataGridViewSelectedInventoryNumbers.Name = "dataGridViewSelectedInventoryNumbers";
            this.dataGridViewSelectedInventoryNumbers.ReadOnly = true;
            this.dataGridViewSelectedInventoryNumbers.RowHeadersVisible = false;
            this.dataGridViewSelectedInventoryNumbers.Size = new System.Drawing.Size(278, 397);
            this.dataGridViewSelectedInventoryNumbers.TabIndex = 7;
            // 
            // ColumnSelectedInventoryNumbersId
            // 
            this.ColumnSelectedInventoryNumbersId.HeaderText = "Id";
            this.ColumnSelectedInventoryNumbersId.Name = "ColumnSelectedInventoryNumbersId";
            this.ColumnSelectedInventoryNumbersId.ReadOnly = true;
            this.ColumnSelectedInventoryNumbersId.Visible = false;
            // 
            // ColumnSelectedInventoryNumbers
            // 
            this.ColumnSelectedInventoryNumbers.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnSelectedInventoryNumbers.HeaderText = "Инв. номер";
            this.ColumnSelectedInventoryNumbers.Name = "ColumnSelectedInventoryNumbers";
            this.ColumnSelectedInventoryNumbers.ReadOnly = true;
            // 
            // buttonAddInvenotyNumbers
            // 
            this.buttonAddInvenotyNumbers.Location = new System.Drawing.Point(308, 116);
            this.buttonAddInvenotyNumbers.Name = "buttonAddInvenotyNumbers";
            this.buttonAddInvenotyNumbers.Size = new System.Drawing.Size(56, 43);
            this.buttonAddInvenotyNumbers.TabIndex = 6;
            this.buttonAddInvenotyNumbers.Text = ">>";
            this.buttonAddInvenotyNumbers.UseVisualStyleBackColor = true;
            this.buttonAddInvenotyNumbers.Click += new System.EventHandler(this.buttonAddInvenotyNumbers_Click);
            // 
            // dataGridViewFindInventoryNumbers
            // 
            this.dataGridViewFindInventoryNumbers.AllowUserToAddRows = false;
            this.dataGridViewFindInventoryNumbers.AllowUserToDeleteRows = false;
            this.dataGridViewFindInventoryNumbers.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewFindInventoryNumbers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFindInventoryNumbers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnFindInventoryNumberId,
            this.ColumnFindInventoryNumber});
            this.dataGridViewFindInventoryNumbers.Location = new System.Drawing.Point(12, 63);
            this.dataGridViewFindInventoryNumbers.Name = "dataGridViewFindInventoryNumbers";
            this.dataGridViewFindInventoryNumbers.ReadOnly = true;
            this.dataGridViewFindInventoryNumbers.RowHeadersVisible = false;
            this.dataGridViewFindInventoryNumbers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFindInventoryNumbers.Size = new System.Drawing.Size(278, 397);
            this.dataGridViewFindInventoryNumbers.TabIndex = 5;
            // 
            // ColumnFindInventoryNumberId
            // 
            this.ColumnFindInventoryNumberId.HeaderText = "Id";
            this.ColumnFindInventoryNumberId.Name = "ColumnFindInventoryNumberId";
            this.ColumnFindInventoryNumberId.ReadOnly = true;
            this.ColumnFindInventoryNumberId.Visible = false;
            // 
            // ColumnFindInventoryNumber
            // 
            this.ColumnFindInventoryNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnFindInventoryNumber.HeaderText = "Инв. номер";
            this.ColumnFindInventoryNumber.Name = "ColumnFindInventoryNumber";
            this.ColumnFindInventoryNumber.ReadOnly = true;
            // 
            // buttonInventoryNumberSearch
            // 
            this.buttonInventoryNumberSearch.Location = new System.Drawing.Point(368, 34);
            this.buttonInventoryNumberSearch.Name = "buttonInventoryNumberSearch";
            this.buttonInventoryNumberSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonInventoryNumberSearch.TabIndex = 4;
            this.buttonInventoryNumberSearch.Text = "Найти";
            this.buttonInventoryNumberSearch.UseVisualStyleBackColor = true;
            this.buttonInventoryNumberSearch.Click += new System.EventHandler(this.buttonInventoryNumberSearch_Click);
            // 
            // textBoxInventoryNumberSearch
            // 
            this.textBoxInventoryNumberSearch.Location = new System.Drawing.Point(142, 36);
            this.textBoxInventoryNumberSearch.Name = "textBoxInventoryNumberSearch";
            this.textBoxInventoryNumberSearch.Size = new System.Drawing.Size(200, 20);
            this.textBoxInventoryNumberSearch.TabIndex = 3;
            // 
            // labelInventoryNumberSearch
            // 
            this.labelInventoryNumberSearch.AutoSize = true;
            this.labelInventoryNumberSearch.Location = new System.Drawing.Point(22, 39);
            this.labelInventoryNumberSearch.Name = "labelInventoryNumberSearch";
            this.labelInventoryNumberSearch.Size = new System.Drawing.Size(114, 13);
            this.labelInventoryNumberSearch.TabIndex = 2;
            this.labelInventoryNumberSearch.Text = "Инвентарный номер:";
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(488, 470);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(170, 23);
            this.buttonApply.TabIndex = 8;
            this.buttonApply.Text = "Удалить ПО";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // SoftwareRecordDelSoftwareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 505);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.dataGridViewSelectedInventoryNumbers);
            this.Controls.Add(this.buttonAddInvenotyNumbers);
            this.Controls.Add(this.dataGridViewFindInventoryNumbers);
            this.Controls.Add(this.buttonInventoryNumberSearch);
            this.Controls.Add(this.textBoxInventoryNumberSearch);
            this.Controls.Add(this.labelInventoryNumberSearch);
            this.Controls.Add(this.comboBoxSelectSoftware);
            this.Controls.Add(this.labelSelectSoftware);
            this.Name = "SoftwareRecordDelSoftwareForm";
            this.Text = "Удаление ПО с компьютеров по инв. номерам";
            this.Load += new System.EventHandler(this.SoftwareRecordDelSoftwareForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelectedInventoryNumbers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFindInventoryNumbers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSelectSoftware;
        private System.Windows.Forms.ComboBox comboBoxSelectSoftware;
        private System.Windows.Forms.DataGridView dataGridViewSelectedInventoryNumbers;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSelectedInventoryNumbersId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSelectedInventoryNumbers;
        private System.Windows.Forms.Button buttonAddInvenotyNumbers;
        private System.Windows.Forms.DataGridView dataGridViewFindInventoryNumbers;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFindInventoryNumberId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFindInventoryNumber;
        private System.Windows.Forms.Button buttonInventoryNumberSearch;
        private System.Windows.Forms.TextBox textBoxInventoryNumberSearch;
        private System.Windows.Forms.Label labelInventoryNumberSearch;
        private System.Windows.Forms.Button buttonApply;
    }
}