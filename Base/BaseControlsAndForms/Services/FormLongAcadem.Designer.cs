namespace BaseControlsAndForms.Services
{
    partial class FormLongAcadem
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
			this.labelFrom = new System.Windows.Forms.Label();
			this.textBoxToAcademOrderNumber = new System.Windows.Forms.TextBox();
			this.labelToAcademOrderNumber = new System.Windows.Forms.Label();
			this.dateTimePickerToAcademDate = new System.Windows.Forms.DateTimePicker();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
			this.labelTo = new System.Windows.Forms.Label();
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
			this.dataGridViewStudents.Location = new System.Drawing.Point(0, 32);
			this.dataGridViewStudents.Name = "dataGridViewStudents";
			this.dataGridViewStudents.ReadOnly = true;
			this.dataGridViewStudents.RowHeadersVisible = false;
			this.dataGridViewStudents.Size = new System.Drawing.Size(734, 178);
			this.dataGridViewStudents.TabIndex = 4;
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
			// labelFrom
			// 
			this.labelFrom.AutoSize = true;
			this.labelFrom.Location = new System.Drawing.Point(183, 9);
			this.labelFrom.Name = "labelFrom";
			this.labelFrom.Size = new System.Drawing.Size(18, 13);
			this.labelFrom.TabIndex = 2;
			this.labelFrom.Text = "от";
			// 
			// textBoxToAcademOrderNumber
			// 
			this.textBoxToAcademOrderNumber.Location = new System.Drawing.Point(77, 6);
			this.textBoxToAcademOrderNumber.Name = "textBoxToAcademOrderNumber";
			this.textBoxToAcademOrderNumber.Size = new System.Drawing.Size(100, 20);
			this.textBoxToAcademOrderNumber.TabIndex = 1;
			// 
			// labelToAcademOrderNumber
			// 
			this.labelToAcademOrderNumber.AutoSize = true;
			this.labelToAcademOrderNumber.Location = new System.Drawing.Point(12, 9);
			this.labelToAcademOrderNumber.Name = "labelToAcademOrderNumber";
			this.labelToAcademOrderNumber.Size = new System.Drawing.Size(59, 13);
			this.labelToAcademOrderNumber.TabIndex = 0;
			this.labelToAcademOrderNumber.Text = "Приказ №";
			// 
			// dateTimePickerToAcademDate
			// 
			this.dateTimePickerToAcademDate.Location = new System.Drawing.Point(207, 6);
			this.dateTimePickerToAcademDate.Name = "dateTimePickerToAcademDate";
			this.dateTimePickerToAcademDate.Size = new System.Drawing.Size(141, 20);
			this.dateTimePickerToAcademDate.TabIndex = 3;
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.Location = new System.Drawing.Point(646, 219);
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
			this.buttonSave.Location = new System.Drawing.Point(565, 219);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 25);
			this.buttonSave.TabIndex = 5;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
			// 
			// dateTimePickerTo
			// 
			this.dateTimePickerTo.Location = new System.Drawing.Point(404, 6);
			this.dateTimePickerTo.Name = "dateTimePickerTo";
			this.dateTimePickerTo.Size = new System.Drawing.Size(141, 20);
			this.dateTimePickerTo.TabIndex = 8;
			// 
			// labelTo
			// 
			this.labelTo.AutoSize = true;
			this.labelTo.Location = new System.Drawing.Point(368, 9);
			this.labelTo.Name = "labelTo";
			this.labelTo.Size = new System.Drawing.Size(19, 13);
			this.labelTo.TabIndex = 7;
			this.labelTo.Text = "до";
			// 
			// FormLongAcadem
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(734, 251);
			this.Controls.Add(this.dateTimePickerTo);
			this.Controls.Add(this.labelTo);
			this.Controls.Add(this.dataGridViewStudents);
			this.Controls.Add(this.labelFrom);
			this.Controls.Add(this.textBoxToAcademOrderNumber);
			this.Controls.Add(this.labelToAcademOrderNumber);
			this.Controls.Add(this.dateTimePickerToAcademDate);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonSave);
			this.Name = "FormLongAcadem";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Продление академа студентов";
			this.Load += new System.EventHandler(this.FormLongAcadem_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewStudents;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumberOfBook;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPatronymic;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.TextBox textBoxToAcademOrderNumber;
        private System.Windows.Forms.Label labelToAcademOrderNumber;
        private System.Windows.Forms.DateTimePicker dateTimePickerToAcademDate;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.DateTimePicker dateTimePickerTo;
		private System.Windows.Forms.Label labelTo;
	}
}