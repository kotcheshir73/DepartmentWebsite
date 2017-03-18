namespace DepartmentDesktop.Views.EducationalProcess.StudentGroup
{
	partial class StudentGroupEnrollmentForm
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
			this.labelOrderNumber = new System.Windows.Forms.Label();
			this.textBoxOrderNumber = new System.Windows.Forms.TextBox();
			this.labelFrom = new System.Windows.Forms.Label();
			this.dateTimePickerEnrollmentDate = new System.Windows.Forms.DateTimePicker();
			this.dataGridViewStudents = new System.Windows.Forms.DataGridView();
			this.ColumnNumberOfBook = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnPatronymic = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnFormOfTraninig = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.buttonLoadFromFile = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).BeginInit();
			this.SuspendLayout();
			// 
			// labelOrderNumber
			// 
			this.labelOrderNumber.AutoSize = true;
			this.labelOrderNumber.Location = new System.Drawing.Point(12, 9);
			this.labelOrderNumber.Name = "labelOrderNumber";
			this.labelOrderNumber.Size = new System.Drawing.Size(59, 13);
			this.labelOrderNumber.TabIndex = 0;
			this.labelOrderNumber.Text = "Приказ №";
			// 
			// textBoxOrderNumber
			// 
			this.textBoxOrderNumber.Location = new System.Drawing.Point(77, 6);
			this.textBoxOrderNumber.Name = "textBoxOrderNumber";
			this.textBoxOrderNumber.Size = new System.Drawing.Size(100, 20);
			this.textBoxOrderNumber.TabIndex = 1;
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
			// dateTimePickerEnrollmentDate
			// 
			this.dateTimePickerEnrollmentDate.Location = new System.Drawing.Point(207, 6);
			this.dateTimePickerEnrollmentDate.Name = "dateTimePickerEnrollmentDate";
			this.dateTimePickerEnrollmentDate.Size = new System.Drawing.Size(141, 20);
			this.dateTimePickerEnrollmentDate.TabIndex = 3;
			// 
			// dataGridViewStudents
			// 
			this.dataGridViewStudents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewStudents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnNumberOfBook,
            this.ColumnLastName,
            this.ColumnFirstName,
            this.ColumnPatronymic,
            this.ColumnFormOfTraninig,
            this.ColumnDescription});
			this.dataGridViewStudents.Location = new System.Drawing.Point(0, 32);
			this.dataGridViewStudents.Name = "dataGridViewStudents";
			this.dataGridViewStudents.Size = new System.Drawing.Size(734, 358);
			this.dataGridViewStudents.TabIndex = 4;
			// 
			// ColumnNumberOfBook
			// 
			this.ColumnNumberOfBook.HeaderText = "Номер зачетки";
			this.ColumnNumberOfBook.Name = "ColumnNumberOfBook";
			this.ColumnNumberOfBook.Width = 110;
			// 
			// ColumnLastName
			// 
			this.ColumnLastName.HeaderText = "Фамилия";
			this.ColumnLastName.Name = "ColumnLastName";
			// 
			// ColumnFirstName
			// 
			this.ColumnFirstName.HeaderText = "Имя";
			this.ColumnFirstName.Name = "ColumnFirstName";
			// 
			// ColumnPatronymic
			// 
			this.ColumnPatronymic.HeaderText = "Отчество";
			this.ColumnPatronymic.Name = "ColumnPatronymic";
			// 
			// ColumnFormOfTraninig
			// 
			this.ColumnFormOfTraninig.HeaderText = "Форма обучения";
			this.ColumnFormOfTraninig.Name = "ColumnFormOfTraninig";
			this.ColumnFormOfTraninig.Width = 120;
			// 
			// ColumnDescription
			// 
			this.ColumnDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColumnDescription.HeaderText = "Описание";
			this.ColumnDescription.Name = "ColumnDescription";
			// 
			// buttonLoadFromFile
			// 
			this.buttonLoadFromFile.Location = new System.Drawing.Point(12, 399);
			this.buttonLoadFromFile.Name = "buttonLoadFromFile";
			this.buttonLoadFromFile.Size = new System.Drawing.Size(120, 25);
			this.buttonLoadFromFile.TabIndex = 5;
			this.buttonLoadFromFile.Text = "Загрузить из файла";
			this.buttonLoadFromFile.UseVisualStyleBackColor = true;
			this.buttonLoadFromFile.Click += new System.EventHandler(this.buttonLoadFromFile_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(646, 399);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 25);
			this.buttonClose.TabIndex = 7;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(565, 399);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 25);
			this.buttonSave.TabIndex = 6;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// StudentGroupEnrollmentForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(734, 432);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.buttonLoadFromFile);
			this.Controls.Add(this.dataGridViewStudents);
			this.Controls.Add(this.dateTimePickerEnrollmentDate);
			this.Controls.Add(this.labelFrom);
			this.Controls.Add(this.textBoxOrderNumber);
			this.Controls.Add(this.labelOrderNumber);
			this.Name = "StudentGroupEnrollmentForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Зачисление студентов";
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelOrderNumber;
		private System.Windows.Forms.TextBox textBoxOrderNumber;
		private System.Windows.Forms.Label labelFrom;
		private System.Windows.Forms.DateTimePicker dateTimePickerEnrollmentDate;
		private System.Windows.Forms.DataGridView dataGridViewStudents;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumberOfBook;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLastName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFirstName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPatronymic;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFormOfTraninig;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescription;
		private System.Windows.Forms.Button buttonLoadFromFile;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
	}
}