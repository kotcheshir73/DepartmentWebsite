namespace LaboratoryHeadControlsAndForms.Services
{
    partial class FormMaterialTechnicalValueApplyTMCRecrods
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
            this.labelSelectedInventoryNumber = new System.Windows.Forms.Label();
            this.textBoxSelectedInventoryNumber = new System.Windows.Forms.TextBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.groupBoxSearchInventoryNumbers = new System.Windows.Forms.GroupBox();
            this.dataGridViewFindInventoryNumbers = new System.Windows.Forms.DataGridView();
            this.buttonInventoryNumberSearch = new System.Windows.Forms.Button();
            this.textBoxInventoryNumberSearch = new System.Windows.Forms.TextBox();
            this.labelInventoryNumberSearch = new System.Windows.Forms.Label();
            this.ColumnFindInventoryNumberId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnFindInventoryNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxSearchInventoryNumbers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFindInventoryNumbers)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSelectedInventoryNumber
            // 
            this.labelSelectedInventoryNumber.AutoSize = true;
            this.labelSelectedInventoryNumber.Location = new System.Drawing.Point(12, 9);
            this.labelSelectedInventoryNumber.Name = "labelSelectedInventoryNumber";
            this.labelSelectedInventoryNumber.Size = new System.Drawing.Size(114, 13);
            this.labelSelectedInventoryNumber.TabIndex = 0;
            this.labelSelectedInventoryNumber.Text = "Инвентарный номер:";
            // 
            // textBoxSelectedInventoryNumber
            // 
            this.textBoxSelectedInventoryNumber.Location = new System.Drawing.Point(136, 6);
            this.textBoxSelectedInventoryNumber.Name = "textBoxSelectedInventoryNumber";
            this.textBoxSelectedInventoryNumber.ReadOnly = true;
            this.textBoxSelectedInventoryNumber.Size = new System.Drawing.Size(210, 20);
            this.textBoxSelectedInventoryNumber.TabIndex = 1;
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(368, 4);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 2;
            this.buttonApply.Text = "Применить";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.ButtonApply_Click);
            // 
            // groupBoxSearchInventoryNumbers
            // 
            this.groupBoxSearchInventoryNumbers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSearchInventoryNumbers.Controls.Add(this.dataGridViewFindInventoryNumbers);
            this.groupBoxSearchInventoryNumbers.Controls.Add(this.buttonInventoryNumberSearch);
            this.groupBoxSearchInventoryNumbers.Controls.Add(this.textBoxInventoryNumberSearch);
            this.groupBoxSearchInventoryNumbers.Controls.Add(this.labelInventoryNumberSearch);
            this.groupBoxSearchInventoryNumbers.Location = new System.Drawing.Point(3, 32);
            this.groupBoxSearchInventoryNumbers.Name = "groupBoxSearchInventoryNumbers";
            this.groupBoxSearchInventoryNumbers.Size = new System.Drawing.Size(449, 420);
            this.groupBoxSearchInventoryNumbers.TabIndex = 3;
            this.groupBoxSearchInventoryNumbers.TabStop = false;
            this.groupBoxSearchInventoryNumbers.Text = "Выбранные инв. номера";
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
            this.ColumnSelected,
            this.ColumnFindInventoryNumber});
            this.dataGridViewFindInventoryNumbers.Location = new System.Drawing.Point(27, 68);
            this.dataGridViewFindInventoryNumbers.Name = "dataGridViewFindInventoryNumbers";
            this.dataGridViewFindInventoryNumbers.RowHeadersVisible = false;
            this.dataGridViewFindInventoryNumbers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFindInventoryNumbers.Size = new System.Drawing.Size(393, 343);
            this.dataGridViewFindInventoryNumbers.TabIndex = 3;
            // 
            // buttonInventoryNumberSearch
            // 
            this.buttonInventoryNumberSearch.Location = new System.Drawing.Point(365, 29);
            this.buttonInventoryNumberSearch.Name = "buttonInventoryNumberSearch";
            this.buttonInventoryNumberSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonInventoryNumberSearch.TabIndex = 2;
            this.buttonInventoryNumberSearch.Text = "Найти";
            this.buttonInventoryNumberSearch.UseVisualStyleBackColor = true;
            this.buttonInventoryNumberSearch.Click += new System.EventHandler(this.ButtonInventoryNumberSearch_Click);
            // 
            // textBoxInventoryNumberSearch
            // 
            this.textBoxInventoryNumberSearch.Location = new System.Drawing.Point(133, 31);
            this.textBoxInventoryNumberSearch.Name = "textBoxInventoryNumberSearch";
            this.textBoxInventoryNumberSearch.Size = new System.Drawing.Size(210, 20);
            this.textBoxInventoryNumberSearch.TabIndex = 1;
            // 
            // labelInventoryNumberSearch
            // 
            this.labelInventoryNumberSearch.AutoSize = true;
            this.labelInventoryNumberSearch.Location = new System.Drawing.Point(13, 34);
            this.labelInventoryNumberSearch.Name = "labelInventoryNumberSearch";
            this.labelInventoryNumberSearch.Size = new System.Drawing.Size(114, 13);
            this.labelInventoryNumberSearch.TabIndex = 0;
            this.labelInventoryNumberSearch.Text = "Инвентарный номер:";
            // 
            // ColumnFindInventoryNumberId
            // 
            this.ColumnFindInventoryNumberId.HeaderText = "Id";
            this.ColumnFindInventoryNumberId.Name = "ColumnFindInventoryNumberId";
            this.ColumnFindInventoryNumberId.Visible = false;
            // 
            // ColumnSelected
            // 
            this.ColumnSelected.HeaderText = "Выбор";
            this.ColumnSelected.Name = "ColumnSelected";
            this.ColumnSelected.Width = 50;
            // 
            // ColumnFindInventoryNumber
            // 
            this.ColumnFindInventoryNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnFindInventoryNumber.HeaderText = "Инв. номер";
            this.ColumnFindInventoryNumber.Name = "ColumnFindInventoryNumber";
            this.ColumnFindInventoryNumber.ReadOnly = true;
            // 
            // MaterialTechnicalValueApplyTMCRecrodsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 455);
            this.Controls.Add(this.groupBoxSearchInventoryNumbers);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.labelSelectedInventoryNumber);
            this.Controls.Add(this.textBoxSelectedInventoryNumber);
            this.Name = "MaterialTechnicalValueApplyTMCRecrodsForm";
            this.Text = "Применение сведений по ТМЦ";
            this.Load += new System.EventHandler(this.FormMaterialTechnicalValueApplyTMCRecrods_Load);
            this.groupBoxSearchInventoryNumbers.ResumeLayout(false);
            this.groupBoxSearchInventoryNumbers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFindInventoryNumbers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSelectedInventoryNumber;
        private System.Windows.Forms.TextBox textBoxSelectedInventoryNumber;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.GroupBox groupBoxSearchInventoryNumbers;
        private System.Windows.Forms.DataGridView dataGridViewFindInventoryNumbers;
        private System.Windows.Forms.Button buttonInventoryNumberSearch;
        private System.Windows.Forms.TextBox textBoxInventoryNumberSearch;
        private System.Windows.Forms.Label labelInventoryNumberSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFindInventoryNumberId;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFindInventoryNumber;
    }
}