namespace BaseControlsAndForms.Services
{
    partial class FormStudentOrdersShow
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
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnOrderNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnOrderData = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnOrderType = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnBlockType = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnStudentGroupFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnStudentGroupTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnId,
            this.ColumnOrderNumber,
            this.ColumnOrderData,
            this.ColumnOrderType,
            this.ColumnBlockType,
            this.ColumnStudentGroupFrom,
            this.ColumnStudentGroupTo});
			this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView.Location = new System.Drawing.Point(0, 0);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size(884, 461);
			this.dataGridView.TabIndex = 0;
			this.dataGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_CellMouseDoubleClick);
			// 
			// ColumnId
			// 
			this.ColumnId.HeaderText = "Id";
			this.ColumnId.Name = "ColumnId";
			this.ColumnId.ReadOnly = true;
			this.ColumnId.Visible = false;
			// 
			// ColumnOrderNumber
			// 
			this.ColumnOrderNumber.HeaderText = "Номер приказа";
			this.ColumnOrderNumber.Name = "ColumnOrderNumber";
			this.ColumnOrderNumber.ReadOnly = true;
			this.ColumnOrderNumber.Width = 150;
			// 
			// ColumnOrderData
			// 
			this.ColumnOrderData.HeaderText = "Дата приказа";
			this.ColumnOrderData.Name = "ColumnOrderData";
			this.ColumnOrderData.ReadOnly = true;
			this.ColumnOrderData.Width = 150;
			// 
			// ColumnOrderType
			// 
			this.ColumnOrderType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColumnOrderType.HeaderText = "Тип приказа";
			this.ColumnOrderType.Name = "ColumnOrderType";
			this.ColumnOrderType.ReadOnly = true;
			// 
			// ColumnBlockType
			// 
			this.ColumnBlockType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColumnBlockType.HeaderText = "Тип приказа";
			this.ColumnBlockType.Name = "ColumnBlockType";
			this.ColumnBlockType.ReadOnly = true;
			// 
			// ColumnStudentGroupFrom
			// 
			this.ColumnStudentGroupFrom.HeaderText = "С группы";
			this.ColumnStudentGroupFrom.Name = "ColumnStudentGroupFrom";
			this.ColumnStudentGroupFrom.ReadOnly = true;
			// 
			// ColumnStudentGroupTo
			// 
			this.ColumnStudentGroupTo.HeaderText = "В группу";
			this.ColumnStudentGroupTo.Name = "ColumnStudentGroupTo";
			this.ColumnStudentGroupTo.ReadOnly = true;
			// 
			// FormStudentOrdersShow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(884, 461);
			this.Controls.Add(this.dataGridView);
			this.Name = "FormStudentOrdersShow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Приказы по студенту";
			this.Load += new System.EventHandler(this.FormStudentOrdersShow_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOrderNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOrderData;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOrderType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBlockType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStudentGroupFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStudentGroupTo;
    }
}