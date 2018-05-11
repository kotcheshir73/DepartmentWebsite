namespace DepartmentDesktop.Views.LaboratoryHead.SoftwareRecord
{
    partial class SoftwareRecordAddClaimForm
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
            this.labelClaimNumber = new System.Windows.Forms.Label();
            this.textBoxClaimNumber = new System.Windows.Forms.TextBox();
            this.labelDateSetup = new System.Windows.Forms.Label();
            this.dateTimePickerDateSetup = new System.Windows.Forms.DateTimePicker();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageInventoryNumbers = new System.Windows.Forms.TabPage();
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
            this.tabPageSoftwareList = new System.Windows.Forms.TabPage();
            this.dataGridViewSoftware = new System.Windows.Forms.DataGridView();
            this.ColumnSoftwareName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSoftwareKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSoftwareK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonApply = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPageInventoryNumbers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelectedInventoryNumbers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFindInventoryNumbers)).BeginInit();
            this.tabPageSoftwareList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSoftware)).BeginInit();
            this.SuspendLayout();
            // 
            // labelClaimNumber
            // 
            this.labelClaimNumber.AutoSize = true;
            this.labelClaimNumber.Location = new System.Drawing.Point(12, 9);
            this.labelClaimNumber.Name = "labelClaimNumber";
            this.labelClaimNumber.Size = new System.Drawing.Size(83, 13);
            this.labelClaimNumber.TabIndex = 0;
            this.labelClaimNumber.Text = "Номер заявки:";
            // 
            // textBoxClaimNumber
            // 
            this.textBoxClaimNumber.Location = new System.Drawing.Point(101, 6);
            this.textBoxClaimNumber.Name = "textBoxClaimNumber";
            this.textBoxClaimNumber.Size = new System.Drawing.Size(250, 20);
            this.textBoxClaimNumber.TabIndex = 1;
            // 
            // labelDateSetup
            // 
            this.labelDateSetup.AutoSize = true;
            this.labelDateSetup.Location = new System.Drawing.Point(387, 9);
            this.labelDateSetup.Name = "labelDateSetup";
            this.labelDateSetup.Size = new System.Drawing.Size(75, 13);
            this.labelDateSetup.TabIndex = 2;
            this.labelDateSetup.Text = "Дата заявки:";
            // 
            // dateTimePickerDateSetup
            // 
            this.dateTimePickerDateSetup.Location = new System.Drawing.Point(468, 6);
            this.dateTimePickerDateSetup.Name = "dateTimePickerDateSetup";
            this.dateTimePickerDateSetup.Size = new System.Drawing.Size(150, 20);
            this.dateTimePickerDateSetup.TabIndex = 3;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageInventoryNumbers);
            this.tabControl.Controls.Add(this.tabPageSoftwareList);
            this.tabControl.Location = new System.Drawing.Point(1, 32);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(674, 474);
            this.tabControl.TabIndex = 4;
            // 
            // tabPageInventoryNumbers
            // 
            this.tabPageInventoryNumbers.Controls.Add(this.dataGridViewSelectedInventoryNumbers);
            this.tabPageInventoryNumbers.Controls.Add(this.buttonAddInvenotyNumbers);
            this.tabPageInventoryNumbers.Controls.Add(this.dataGridViewFindInventoryNumbers);
            this.tabPageInventoryNumbers.Controls.Add(this.buttonInventoryNumberSearch);
            this.tabPageInventoryNumbers.Controls.Add(this.textBoxInventoryNumberSearch);
            this.tabPageInventoryNumbers.Controls.Add(this.labelInventoryNumberSearch);
            this.tabPageInventoryNumbers.Location = new System.Drawing.Point(4, 22);
            this.tabPageInventoryNumbers.Name = "tabPageInventoryNumbers";
            this.tabPageInventoryNumbers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInventoryNumbers.Size = new System.Drawing.Size(666, 448);
            this.tabPageInventoryNumbers.TabIndex = 0;
            this.tabPageInventoryNumbers.Text = "Инв. номера";
            this.tabPageInventoryNumbers.UseVisualStyleBackColor = true;
            // 
            // dataGridViewSelectedInventoryNumbers
            // 
            this.dataGridViewSelectedInventoryNumbers.AllowUserToAddRows = false;
            this.dataGridViewSelectedInventoryNumbers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSelectedInventoryNumbers.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewSelectedInventoryNumbers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSelectedInventoryNumbers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSelectedInventoryNumbersId,
            this.ColumnSelectedInventoryNumbers});
            this.dataGridViewSelectedInventoryNumbers.Location = new System.Drawing.Point(380, 40);
            this.dataGridViewSelectedInventoryNumbers.Name = "dataGridViewSelectedInventoryNumbers";
            this.dataGridViewSelectedInventoryNumbers.ReadOnly = true;
            this.dataGridViewSelectedInventoryNumbers.RowHeadersVisible = false;
            this.dataGridViewSelectedInventoryNumbers.Size = new System.Drawing.Size(278, 403);
            this.dataGridViewSelectedInventoryNumbers.TabIndex = 5;
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
            this.buttonAddInvenotyNumbers.Location = new System.Drawing.Point(302, 93);
            this.buttonAddInvenotyNumbers.Name = "buttonAddInvenotyNumbers";
            this.buttonAddInvenotyNumbers.Size = new System.Drawing.Size(56, 43);
            this.buttonAddInvenotyNumbers.TabIndex = 4;
            this.buttonAddInvenotyNumbers.Text = ">>";
            this.buttonAddInvenotyNumbers.UseVisualStyleBackColor = true;
            this.buttonAddInvenotyNumbers.Click += new System.EventHandler(this.buttonAddInvenotyNumbers_Click);
            // 
            // dataGridViewFindInventoryNumbers
            // 
            this.dataGridViewFindInventoryNumbers.AllowUserToAddRows = false;
            this.dataGridViewFindInventoryNumbers.AllowUserToDeleteRows = false;
            this.dataGridViewFindInventoryNumbers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewFindInventoryNumbers.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewFindInventoryNumbers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFindInventoryNumbers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnFindInventoryNumberId,
            this.ColumnFindInventoryNumber});
            this.dataGridViewFindInventoryNumbers.Location = new System.Drawing.Point(6, 40);
            this.dataGridViewFindInventoryNumbers.Name = "dataGridViewFindInventoryNumbers";
            this.dataGridViewFindInventoryNumbers.ReadOnly = true;
            this.dataGridViewFindInventoryNumbers.RowHeadersVisible = false;
            this.dataGridViewFindInventoryNumbers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFindInventoryNumbers.Size = new System.Drawing.Size(278, 403);
            this.dataGridViewFindInventoryNumbers.TabIndex = 3;
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
            this.buttonInventoryNumberSearch.Location = new System.Drawing.Point(362, 11);
            this.buttonInventoryNumberSearch.Name = "buttonInventoryNumberSearch";
            this.buttonInventoryNumberSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonInventoryNumberSearch.TabIndex = 2;
            this.buttonInventoryNumberSearch.Text = "Найти";
            this.buttonInventoryNumberSearch.UseVisualStyleBackColor = true;
            this.buttonInventoryNumberSearch.Click += new System.EventHandler(this.buttonInventoryNumberSearch_Click);
            // 
            // textBoxInventoryNumberSearch
            // 
            this.textBoxInventoryNumberSearch.Location = new System.Drawing.Point(136, 13);
            this.textBoxInventoryNumberSearch.Name = "textBoxInventoryNumberSearch";
            this.textBoxInventoryNumberSearch.Size = new System.Drawing.Size(200, 20);
            this.textBoxInventoryNumberSearch.TabIndex = 1;
            // 
            // labelInventoryNumberSearch
            // 
            this.labelInventoryNumberSearch.AutoSize = true;
            this.labelInventoryNumberSearch.Location = new System.Drawing.Point(16, 16);
            this.labelInventoryNumberSearch.Name = "labelInventoryNumberSearch";
            this.labelInventoryNumberSearch.Size = new System.Drawing.Size(114, 13);
            this.labelInventoryNumberSearch.TabIndex = 0;
            this.labelInventoryNumberSearch.Text = "Инвентарный номер:";
            // 
            // tabPageSoftwareList
            // 
            this.tabPageSoftwareList.Controls.Add(this.dataGridViewSoftware);
            this.tabPageSoftwareList.Location = new System.Drawing.Point(4, 22);
            this.tabPageSoftwareList.Name = "tabPageSoftwareList";
            this.tabPageSoftwareList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSoftwareList.Size = new System.Drawing.Size(666, 448);
            this.tabPageSoftwareList.TabIndex = 1;
            this.tabPageSoftwareList.Text = "Список ПО";
            this.tabPageSoftwareList.UseVisualStyleBackColor = true;
            // 
            // dataGridViewSoftware
            // 
            this.dataGridViewSoftware.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewSoftware.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSoftware.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSoftwareName,
            this.ColumnSoftwareKey,
            this.ColumnSoftwareK});
            this.dataGridViewSoftware.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSoftware.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewSoftware.Name = "dataGridViewSoftware";
            this.dataGridViewSoftware.RowHeadersVisible = false;
            this.dataGridViewSoftware.Size = new System.Drawing.Size(660, 442);
            this.dataGridViewSoftware.TabIndex = 0;
            // 
            // ColumnSoftwareName
            // 
            this.ColumnSoftwareName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnSoftwareName.HeaderText = "Название";
            this.ColumnSoftwareName.Name = "ColumnSoftwareName";
            // 
            // ColumnSoftwareKey
            // 
            this.ColumnSoftwareKey.HeaderText = "Ключ";
            this.ColumnSoftwareKey.Name = "ColumnSoftwareKey";
            this.ColumnSoftwareKey.Width = 250;
            // 
            // ColumnSoftwareK
            // 
            this.ColumnSoftwareK.HeaderText = "K";
            this.ColumnSoftwareK.Name = "ColumnSoftwareK";
            this.ColumnSoftwareK.Width = 50;
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(493, 508);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(170, 23);
            this.buttonApply.TabIndex = 5;
            this.buttonApply.Text = "Оформить заявку";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // SoftwareRecordAddClaimForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 536);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.dateTimePickerDateSetup);
            this.Controls.Add(this.labelDateSetup);
            this.Controls.Add(this.textBoxClaimNumber);
            this.Controls.Add(this.labelClaimNumber);
            this.Name = "SoftwareRecordAddClaimForm";
            this.Text = "Заявка на ПО";
            this.tabControl.ResumeLayout(false);
            this.tabPageInventoryNumbers.ResumeLayout(false);
            this.tabPageInventoryNumbers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelectedInventoryNumbers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFindInventoryNumbers)).EndInit();
            this.tabPageSoftwareList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSoftware)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelClaimNumber;
        private System.Windows.Forms.TextBox textBoxClaimNumber;
        private System.Windows.Forms.Label labelDateSetup;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateSetup;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageInventoryNumbers;
        private System.Windows.Forms.TabPage tabPageSoftwareList;
        private System.Windows.Forms.Label labelInventoryNumberSearch;
        private System.Windows.Forms.TextBox textBoxInventoryNumberSearch;
        private System.Windows.Forms.Button buttonInventoryNumberSearch;
        private System.Windows.Forms.DataGridView dataGridViewFindInventoryNumbers;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFindInventoryNumberId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFindInventoryNumber;
        private System.Windows.Forms.Button buttonAddInvenotyNumbers;
        private System.Windows.Forms.DataGridView dataGridViewSelectedInventoryNumbers;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSelectedInventoryNumbersId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSelectedInventoryNumbers;
        private System.Windows.Forms.DataGridView dataGridViewSoftware;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSoftwareName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSoftwareKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSoftwareK;
        private System.Windows.Forms.Button buttonApply;
    }
}