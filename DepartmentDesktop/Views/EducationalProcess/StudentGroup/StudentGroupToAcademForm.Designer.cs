namespace DepartmentDesktop.Views.EducationalProcess.StudentGroup
{
	partial class StudentGroupToAcademForm
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
			this.ColumnSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ColumnNumberOfBook = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnPatronymic = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.textBoxToAcademOrderNumber = new System.Windows.Forms.TextBox();
			this.labelToAcademOrderNumber = new System.Windows.Forms.Label();
			this.checkBoxSelectAll = new System.Windows.Forms.CheckBox();
			this.dateTimePickerToAcademDate = new System.Windows.Forms.DateTimePicker();
			this.textBoxToAcademReason = new System.Windows.Forms.TextBox();
			this.labelToAcademReason = new System.Windows.Forms.Label();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
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
            this.ColumnSelect,
            this.ColumnNumberOfBook,
            this.ColumnLastName,
            this.ColumnFirstName,
            this.ColumnPatronymic});
			this.dataGridViewStudents.Location = new System.Drawing.Point(0, 60);
			this.dataGridViewStudents.Name = "dataGridViewStudents";
			this.dataGridViewStudents.RowHeadersVisible = false;
			this.dataGridViewStudents.Size = new System.Drawing.Size(734, 331);
			this.dataGridViewStudents.TabIndex = 6;
			// 
			// ColumnSelect
			// 
			this.ColumnSelect.HeaderText = "Выбрать";
			this.ColumnSelect.Name = "ColumnSelect";
			this.ColumnSelect.Width = 60;
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
			// textBoxToAcademOrderNumber
			// 
			this.textBoxToAcademOrderNumber.Location = new System.Drawing.Point(237, 33);
			this.textBoxToAcademOrderNumber.Name = "textBoxToAcademOrderNumber";
			this.textBoxToAcademOrderNumber.Size = new System.Drawing.Size(100, 20);
			this.textBoxToAcademOrderNumber.TabIndex = 5;
			// 
			// labelToAcademOrderNumber
			// 
			this.labelToAcademOrderNumber.AutoSize = true;
			this.labelToAcademOrderNumber.Location = new System.Drawing.Point(172, 36);
			this.labelToAcademOrderNumber.Name = "labelToAcademOrderNumber";
			this.labelToAcademOrderNumber.Size = new System.Drawing.Size(59, 13);
			this.labelToAcademOrderNumber.TabIndex = 4;
			this.labelToAcademOrderNumber.Text = "Приказ №";
			// 
			// checkBoxSelectAll
			// 
			this.checkBoxSelectAll.AutoSize = true;
			this.checkBoxSelectAll.Location = new System.Drawing.Point(15, 37);
			this.checkBoxSelectAll.Name = "checkBoxSelectAll";
			this.checkBoxSelectAll.Size = new System.Drawing.Size(91, 17);
			this.checkBoxSelectAll.TabIndex = 3;
			this.checkBoxSelectAll.Text = "Выбрать все";
			this.checkBoxSelectAll.UseVisualStyleBackColor = true;
			this.checkBoxSelectAll.CheckedChanged += new System.EventHandler(this.checkBoxSelectAll_CheckedChanged);
			// 
			// dateTimePickerToAcademDate
			// 
			this.dateTimePickerToAcademDate.Location = new System.Drawing.Point(581, 7);
			this.dateTimePickerToAcademDate.Name = "dateTimePickerToAcademDate";
			this.dateTimePickerToAcademDate.Size = new System.Drawing.Size(141, 20);
			this.dateTimePickerToAcademDate.TabIndex = 2;
			// 
			// textBoxToAcademReason
			// 
			this.textBoxToAcademReason.Location = new System.Drawing.Point(81, 7);
			this.textBoxToAcademReason.Name = "textBoxToAcademReason";
			this.textBoxToAcademReason.Size = new System.Drawing.Size(494, 20);
			this.textBoxToAcademReason.TabIndex = 1;
			// 
			// labelToAcademReason
			// 
			this.labelToAcademReason.AutoSize = true;
			this.labelToAcademReason.Location = new System.Drawing.Point(12, 10);
			this.labelToAcademReason.Name = "labelToAcademReason";
			this.labelToAcademReason.Size = new System.Drawing.Size(63, 13);
			this.labelToAcademReason.TabIndex = 0;
			this.labelToAcademReason.Text = "Основание";
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(646, 400);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 25);
			this.buttonClose.TabIndex = 8;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(565, 400);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 25);
			this.buttonSave.TabIndex = 7;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// StudentGroupToAcademForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(734, 432);
			this.Controls.Add(this.dataGridViewStudents);
			this.Controls.Add(this.textBoxToAcademOrderNumber);
			this.Controls.Add(this.labelToAcademOrderNumber);
			this.Controls.Add(this.checkBoxSelectAll);
			this.Controls.Add(this.dateTimePickerToAcademDate);
			this.Controls.Add(this.textBoxToAcademReason);
			this.Controls.Add(this.labelToAcademReason);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonSave);
			this.Name = "StudentGroupToAcademForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Перевод в академ студентов";
			this.Load += new System.EventHandler(this.StudentGroupToAcademForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewStudents;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSelect;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumberOfBook;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLastName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFirstName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPatronymic;
		private System.Windows.Forms.TextBox textBoxToAcademOrderNumber;
		private System.Windows.Forms.Label labelToAcademOrderNumber;
		private System.Windows.Forms.CheckBox checkBoxSelectAll;
		private System.Windows.Forms.DateTimePicker dateTimePickerToAcademDate;
		private System.Windows.Forms.TextBox textBoxToAcademReason;
		private System.Windows.Forms.Label labelToAcademReason;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
	}
}