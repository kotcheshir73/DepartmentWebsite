namespace BaseControlsAndForms.StudentGroup
{
	partial class StudentGroupDeductionForm
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
            this.ColumnNumberOfBook = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPatronymic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateTimePickerDeductionDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxDeductionReason = new System.Windows.Forms.TextBox();
            this.labelDeductionReason = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelDeductionOrderNumber = new System.Windows.Forms.Label();
            this.textBoxDeductionOrderNumber = new System.Windows.Forms.TextBox();
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
            this.ColumnPatronymic});
            this.dataGridViewStudents.Location = new System.Drawing.Point(0, 33);
            this.dataGridViewStudents.Name = "dataGridViewStudents";
            this.dataGridViewStudents.ReadOnly = true;
            this.dataGridViewStudents.RowHeadersVisible = false;
            this.dataGridViewStudents.Size = new System.Drawing.Size(734, 180);
            this.dataGridViewStudents.TabIndex = 6;
            // 
            // ColumnNumberOfBook
            // 
            this.ColumnNumberOfBook.HeaderText = "Номер зачетки";
            this.ColumnNumberOfBook.Name = "ColumnNumberOfBook";
            this.ColumnNumberOfBook.ReadOnly = true;
            this.ColumnNumberOfBook.Width = 110;
            // 
            // ColumnLastName
            // 
            this.ColumnLastName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnLastName.HeaderText = "Фамилия";
            this.ColumnLastName.Name = "ColumnLastName";
            this.ColumnLastName.ReadOnly = true;
            // 
            // ColumnFirstName
            // 
            this.ColumnFirstName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnFirstName.HeaderText = "Имя";
            this.ColumnFirstName.Name = "ColumnFirstName";
            this.ColumnFirstName.ReadOnly = true;
            // 
            // ColumnPatronymic
            // 
            this.ColumnPatronymic.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnPatronymic.HeaderText = "Отчество";
            this.ColumnPatronymic.Name = "ColumnPatronymic";
            this.ColumnPatronymic.ReadOnly = true;
            // 
            // dateTimePickerDeductionDate
            // 
            this.dateTimePickerDeductionDate.Location = new System.Drawing.Point(581, 7);
            this.dateTimePickerDeductionDate.Name = "dateTimePickerDeductionDate";
            this.dateTimePickerDeductionDate.Size = new System.Drawing.Size(141, 20);
            this.dateTimePickerDeductionDate.TabIndex = 2;
            // 
            // textBoxDeductionReason
            // 
            this.textBoxDeductionReason.Location = new System.Drawing.Point(81, 7);
            this.textBoxDeductionReason.Name = "textBoxDeductionReason";
            this.textBoxDeductionReason.Size = new System.Drawing.Size(323, 20);
            this.textBoxDeductionReason.TabIndex = 1;
            // 
            // labelDeductionReason
            // 
            this.labelDeductionReason.AutoSize = true;
            this.labelDeductionReason.Location = new System.Drawing.Point(12, 10);
            this.labelDeductionReason.Name = "labelDeductionReason";
            this.labelDeductionReason.Size = new System.Drawing.Size(63, 13);
            this.labelDeductionReason.TabIndex = 0;
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
            this.labelDeductionOrderNumber.Location = new System.Drawing.Point(410, 10);
            this.labelDeductionOrderNumber.Name = "labelDeductionOrderNumber";
            this.labelDeductionOrderNumber.Size = new System.Drawing.Size(59, 13);
            this.labelDeductionOrderNumber.TabIndex = 4;
            this.labelDeductionOrderNumber.Text = "Приказ №";
            // 
            // textBoxDeductionOrderNumber
            // 
            this.textBoxDeductionOrderNumber.Location = new System.Drawing.Point(475, 7);
            this.textBoxDeductionOrderNumber.Name = "textBoxDeductionOrderNumber";
            this.textBoxDeductionOrderNumber.Size = new System.Drawing.Size(100, 20);
            this.textBoxDeductionOrderNumber.TabIndex = 5;
            // 
            // StudentGroupDeductionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 251);
            this.Controls.Add(this.textBoxDeductionOrderNumber);
            this.Controls.Add(this.labelDeductionOrderNumber);
            this.Controls.Add(this.dataGridViewStudents);
            this.Controls.Add(this.dateTimePickerDeductionDate);
            this.Controls.Add(this.textBoxDeductionReason);
            this.Controls.Add(this.labelDeductionReason);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Name = "StudentGroupDeductionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Отчисление студентов";
            this.Load += new System.EventHandler(this.StudentGroupDeductionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewStudents;
		private System.Windows.Forms.DateTimePicker dateTimePickerDeductionDate;
		private System.Windows.Forms.TextBox textBoxDeductionReason;
		private System.Windows.Forms.Label labelDeductionReason;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Label labelDeductionOrderNumber;
		private System.Windows.Forms.TextBox textBoxDeductionOrderNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumberOfBook;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPatronymic;
    }
}