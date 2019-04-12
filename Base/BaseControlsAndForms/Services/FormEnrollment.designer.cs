namespace BaseControlsAndForms.Services
{
	partial class FormEnrollment
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
            this.labelEnrollmentOrderNumber = new System.Windows.Forms.Label();
            this.textBoxEnrollmentOrderNumber = new System.Windows.Forms.TextBox();
            this.labelFrom = new System.Windows.Forms.Label();
            this.dateTimePickerEnrollmentDate = new System.Windows.Forms.DateTimePicker();
            this.dataGridViewStudents = new System.Windows.Forms.DataGridView();
            this.ColumnNumberOfBook = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPatronymic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFormOfTraninig = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).BeginInit();
            this.SuspendLayout();
            // 
            // labelEnrollmentOrderNumber
            // 
            this.labelEnrollmentOrderNumber.AutoSize = true;
            this.labelEnrollmentOrderNumber.Location = new System.Drawing.Point(12, 9);
            this.labelEnrollmentOrderNumber.Name = "labelEnrollmentOrderNumber";
            this.labelEnrollmentOrderNumber.Size = new System.Drawing.Size(130, 13);
            this.labelEnrollmentOrderNumber.TabIndex = 0;
            this.labelEnrollmentOrderNumber.Text = "Приказ о зачислении №";
            // 
            // textBoxEnrollmentOrderNumber
            // 
            this.textBoxEnrollmentOrderNumber.Location = new System.Drawing.Point(212, 6);
            this.textBoxEnrollmentOrderNumber.Name = "textBoxEnrollmentOrderNumber";
            this.textBoxEnrollmentOrderNumber.Size = new System.Drawing.Size(100, 20);
            this.textBoxEnrollmentOrderNumber.TabIndex = 1;
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(318, 9);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(18, 13);
            this.labelFrom.TabIndex = 2;
            this.labelFrom.Text = "от";
            // 
            // dateTimePickerEnrollmentDate
            // 
            this.dateTimePickerEnrollmentDate.Location = new System.Drawing.Point(342, 6);
            this.dateTimePickerEnrollmentDate.Name = "dateTimePickerEnrollmentDate";
            this.dateTimePickerEnrollmentDate.Size = new System.Drawing.Size(141, 20);
            this.dateTimePickerEnrollmentDate.TabIndex = 3;
            // 
            // dataGridViewStudents
            // 
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
            this.ColumnFormOfTraninig,
            this.ColumnDescription});
            this.dataGridViewStudents.Location = new System.Drawing.Point(0, 32);
            this.dataGridViewStudents.Name = "dataGridViewStudents";
            this.dataGridViewStudents.Size = new System.Drawing.Size(734, 412);
            this.dataGridViewStudents.TabIndex = 4;
            this.dataGridViewStudents.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridViewStudents_KeyDown);
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
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(646, 453);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 25);
            this.buttonClose.TabIndex = 6;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(565, 453);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 25);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // FormEnrollment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 486);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.dataGridViewStudents);
            this.Controls.Add(this.dateTimePickerEnrollmentDate);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.textBoxEnrollmentOrderNumber);
            this.Controls.Add(this.labelEnrollmentOrderNumber);
            this.Name = "FormEnrollment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Зачисление студентов";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelEnrollmentOrderNumber;
		private System.Windows.Forms.TextBox textBoxEnrollmentOrderNumber;
		private System.Windows.Forms.Label labelFrom;
		private System.Windows.Forms.DateTimePicker dateTimePickerEnrollmentDate;
		private System.Windows.Forms.DataGridView dataGridViewStudents;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumberOfBook;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLastName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFirstName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPatronymic;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFormOfTraninig;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescription;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
    }
}