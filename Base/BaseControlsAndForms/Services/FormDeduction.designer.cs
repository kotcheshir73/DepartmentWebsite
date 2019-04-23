namespace BaseControlsAndForms.Services
{
	partial class FormDeduction
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
            this.dataGridViewStudents = new System.Windows.Forms.DataGridView();
            this.dateTimePickerDeductionDate = new System.Windows.Forms.DateTimePicker();
            this.labelDeductionReason = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelDeductionOrderNumber = new System.Windows.Forms.Label();
            this.textBoxDeductionOrderNumber = new System.Windows.Forms.TextBox();
            this.labelFrom = new System.Windows.Forms.Label();
            this.comboBoxStudentOrderType = new System.Windows.Forms.ComboBox();
            this.ColumnNumberOfBook = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPatronymic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewStudents
            // 
            this.dataGridViewStudents.AllowUserToAddRows = false;
            this.dataGridViewStudents.AllowUserToDeleteRows = false;
            this.dataGridViewStudents.AllowUserToResizeRows = false;
            this.dataGridViewStudents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStudents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnNumberOfBook,
            this.ColumnLastName,
            this.ColumnFirstName,
            this.ColumnPatronymic,
            this.ColumnInfo});
            this.dataGridViewStudents.Location = new System.Drawing.Point(0, 33);
            this.dataGridViewStudents.Name = "dataGridViewStudents";
            this.dataGridViewStudents.RowHeadersVisible = false;
            this.dataGridViewStudents.Size = new System.Drawing.Size(734, 180);
            this.dataGridViewStudents.TabIndex = 6;
            // 
            // dateTimePickerDeductionDate
            // 
            this.dateTimePickerDeductionDate.Location = new System.Drawing.Point(207, 6);
            this.dateTimePickerDeductionDate.Name = "dateTimePickerDeductionDate";
            this.dateTimePickerDeductionDate.Size = new System.Drawing.Size(141, 20);
            this.dateTimePickerDeductionDate.TabIndex = 3;
            // 
            // labelDeductionReason
            // 
            this.labelDeductionReason.AutoSize = true;
            this.labelDeductionReason.Location = new System.Drawing.Point(354, 9);
            this.labelDeductionReason.Name = "labelDeductionReason";
            this.labelDeductionReason.Size = new System.Drawing.Size(63, 13);
            this.labelDeductionReason.TabIndex = 4;
            this.labelDeductionReason.Text = "Основание";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(646, 219);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 25);
            this.buttonClose.TabIndex = 8;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(565, 219);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 25);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // labelDeductionOrderNumber
            // 
            this.labelDeductionOrderNumber.AutoSize = true;
            this.labelDeductionOrderNumber.Location = new System.Drawing.Point(12, 9);
            this.labelDeductionOrderNumber.Name = "labelDeductionOrderNumber";
            this.labelDeductionOrderNumber.Size = new System.Drawing.Size(59, 13);
            this.labelDeductionOrderNumber.TabIndex = 0;
            this.labelDeductionOrderNumber.Text = "Приказ №";
            // 
            // textBoxDeductionOrderNumber
            // 
            this.textBoxDeductionOrderNumber.Location = new System.Drawing.Point(77, 6);
            this.textBoxDeductionOrderNumber.Name = "textBoxDeductionOrderNumber";
            this.textBoxDeductionOrderNumber.Size = new System.Drawing.Size(100, 20);
            this.textBoxDeductionOrderNumber.TabIndex = 1;
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(183, 9);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(18, 13);
            this.labelFrom.TabIndex = 2;
            this.labelFrom.Text = "от";
            // 
            // comboBoxStudentOrderType
            // 
            this.comboBoxStudentOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudentOrderType.FormattingEnabled = true;
            this.comboBoxStudentOrderType.Items.AddRange(new object[] {
            "Отчислить за неуспевамость",
            "Отчислить в связи с переводом",
            "Отчислить по собственному"});
            this.comboBoxStudentOrderType.Location = new System.Drawing.Point(423, 6);
            this.comboBoxStudentOrderType.Name = "comboBoxStudentOrderType";
            this.comboBoxStudentOrderType.Size = new System.Drawing.Size(311, 21);
            this.comboBoxStudentOrderType.TabIndex = 5;
            // 
            // ColumnNumberOfBook
            // 
            this.ColumnNumberOfBook.HeaderText = "Номер зачетки";
            this.ColumnNumberOfBook.Name = "ColumnNumberOfBook";
            this.ColumnNumberOfBook.ReadOnly = true;
            this.ColumnNumberOfBook.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnNumberOfBook.Width = 110;
            // 
            // ColumnLastName
            // 
            this.ColumnLastName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnLastName.HeaderText = "Фамилия";
            this.ColumnLastName.Name = "ColumnLastName";
            this.ColumnLastName.ReadOnly = true;
            this.ColumnLastName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnFirstName
            // 
            this.ColumnFirstName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnFirstName.HeaderText = "Имя";
            this.ColumnFirstName.Name = "ColumnFirstName";
            this.ColumnFirstName.ReadOnly = true;
            this.ColumnFirstName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnPatronymic
            // 
            this.ColumnPatronymic.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnPatronymic.HeaderText = "Отчество";
            this.ColumnPatronymic.Name = "ColumnPatronymic";
            this.ColumnPatronymic.ReadOnly = true;
            this.ColumnPatronymic.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnInfo
            // 
            this.ColumnInfo.HeaderText = "Информация";
            this.ColumnInfo.Name = "ColumnInfo";
            this.ColumnInfo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnInfo.Width = 150;
            // 
            // FormDeduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 251);
            this.Controls.Add(this.comboBoxStudentOrderType);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.textBoxDeductionOrderNumber);
            this.Controls.Add(this.labelDeductionOrderNumber);
            this.Controls.Add(this.dataGridViewStudents);
            this.Controls.Add(this.dateTimePickerDeductionDate);
            this.Controls.Add(this.labelDeductionReason);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Name = "FormDeduction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Отчисление студентов";
            this.Load += new System.EventHandler(this.FormDeduction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewStudents;
		private System.Windows.Forms.DateTimePicker dateTimePickerDeductionDate;
		private System.Windows.Forms.Label labelDeductionReason;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Label labelDeductionOrderNumber;
		private System.Windows.Forms.TextBox textBoxDeductionOrderNumber;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.ComboBox comboBoxStudentOrderType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumberOfBook;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPatronymic;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnInfo;
    }
}