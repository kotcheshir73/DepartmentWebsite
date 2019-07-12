namespace BaseControlsAndForms.Services
{
	partial class FormTransferCourse
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
            this.dateTimePickerTransferDate = new System.Windows.Forms.DateTimePicker();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelNewStudentGroup = new System.Windows.Forms.Label();
            this.comboBoxNewStudentGroup = new System.Windows.Forms.ComboBox();
            this.checkBoxSelectAll = new System.Windows.Forms.CheckBox();
            this.textBoxTransferOrderNumber = new System.Windows.Forms.TextBox();
            this.labelTransferOrderNumber = new System.Windows.Forms.Label();
            this.labelFrom = new System.Windows.Forms.Label();
            this.ColumnSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnCondition = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNumberOfBook = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPatronymic = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.ColumnCondition,
            this.Id,
            this.ColumnNumberOfBook,
            this.ColumnLastName,
            this.ColumnFirstName,
            this.ColumnPatronymic});
            this.dataGridViewStudents.Location = new System.Drawing.Point(0, 60);
            this.dataGridViewStudents.Name = "dataGridViewStudents";
            this.dataGridViewStudents.RowHeadersVisible = false;
            this.dataGridViewStudents.Size = new System.Drawing.Size(734, 330);
            this.dataGridViewStudents.TabIndex = 8;
            // 
            // dateTimePickerTransferDate
            // 
            this.dateTimePickerTransferDate.Location = new System.Drawing.Point(207, 6);
            this.dateTimePickerTransferDate.Name = "dateTimePickerTransferDate";
            this.dateTimePickerTransferDate.Size = new System.Drawing.Size(141, 20);
            this.dateTimePickerTransferDate.TabIndex = 3;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(646, 400);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 25);
            this.buttonClose.TabIndex = 10;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(565, 400);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 25);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // labelNewStudentGroup
            // 
            this.labelNewStudentGroup.AutoSize = true;
            this.labelNewStudentGroup.Location = new System.Drawing.Point(365, 9);
            this.labelNewStudentGroup.Name = "labelNewStudentGroup";
            this.labelNewStudentGroup.Size = new System.Drawing.Size(189, 13);
            this.labelNewStudentGroup.TabIndex = 4;
            this.labelNewStudentGroup.Text = "Группа, куда переводятся студенты";
            // 
            // comboBoxNewStudentGroup
            // 
            this.comboBoxNewStudentGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNewStudentGroup.FormattingEnabled = true;
            this.comboBoxNewStudentGroup.Location = new System.Drawing.Point(560, 6);
            this.comboBoxNewStudentGroup.Name = "comboBoxNewStudentGroup";
            this.comboBoxNewStudentGroup.Size = new System.Drawing.Size(130, 21);
            this.comboBoxNewStudentGroup.TabIndex = 5;
            // 
            // checkBoxSelectAll
            // 
            this.checkBoxSelectAll.AutoSize = true;
            this.checkBoxSelectAll.Location = new System.Drawing.Point(12, 35);
            this.checkBoxSelectAll.Name = "checkBoxSelectAll";
            this.checkBoxSelectAll.Size = new System.Drawing.Size(91, 17);
            this.checkBoxSelectAll.TabIndex = 6;
            this.checkBoxSelectAll.Text = "Выбрать все";
            this.checkBoxSelectAll.UseVisualStyleBackColor = true;
            this.checkBoxSelectAll.CheckedChanged += new System.EventHandler(this.CheckBoxSelectAll_CheckedChanged);
            // 
            // textBoxTransferOrderNumber
            // 
            this.textBoxTransferOrderNumber.Location = new System.Drawing.Point(77, 6);
            this.textBoxTransferOrderNumber.Name = "textBoxTransferOrderNumber";
            this.textBoxTransferOrderNumber.Size = new System.Drawing.Size(100, 20);
            this.textBoxTransferOrderNumber.TabIndex = 1;
            // 
            // labelTransferOrderNumber
            // 
            this.labelTransferOrderNumber.AutoSize = true;
            this.labelTransferOrderNumber.Location = new System.Drawing.Point(12, 9);
            this.labelTransferOrderNumber.Name = "labelTransferOrderNumber";
            this.labelTransferOrderNumber.Size = new System.Drawing.Size(59, 13);
            this.labelTransferOrderNumber.TabIndex = 0;
            this.labelTransferOrderNumber.Text = "Приказ №";
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
            // ColumnSelect
            // 
            this.ColumnSelect.HeaderText = "Выбрать";
            this.ColumnSelect.Name = "ColumnSelect";
            this.ColumnSelect.Width = 60;
            // 
            // ColumnCondition
            // 
            this.ColumnCondition.HeaderText = "Условно";
            this.ColumnCondition.Name = "ColumnCondition";
            this.ColumnCondition.Width = 60;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
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
            // FormTransferCourse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 431);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.textBoxTransferOrderNumber);
            this.Controls.Add(this.labelTransferOrderNumber);
            this.Controls.Add(this.checkBoxSelectAll);
            this.Controls.Add(this.comboBoxNewStudentGroup);
            this.Controls.Add(this.labelNewStudentGroup);
            this.Controls.Add(this.dataGridViewStudents);
            this.Controls.Add(this.dateTimePickerTransferDate);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Name = "FormTransferCourse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Перевод студентов";
            this.Load += new System.EventHandler(this.FormTransferCourse_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewStudents;
		private System.Windows.Forms.DateTimePicker dateTimePickerTransferDate;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Label labelNewStudentGroup;
		private System.Windows.Forms.ComboBox comboBoxNewStudentGroup;
		private System.Windows.Forms.CheckBox checkBoxSelectAll;
        private System.Windows.Forms.TextBox textBoxTransferOrderNumber;
        private System.Windows.Forms.Label labelTransferOrderNumber;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSelect;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnCondition;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumberOfBook;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPatronymic;
    }
}