namespace BaseControlsAndForms.Services
{
    partial class FormRecovery
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
			this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnNumberOfBook = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnPatronymic = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.textBoxRecoveryOrderNumber = new System.Windows.Forms.TextBox();
			this.labelRecoveryOrderNumber = new System.Windows.Forms.Label();
			this.dateTimePickerRecoveryDate = new System.Windows.Forms.DateTimePicker();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.comboBoxNewStudentGroup = new System.Windows.Forms.ComboBox();
			this.labelStudentGroup = new System.Windows.Forms.Label();
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
            this.ColumnId,
            this.ColumnNumberOfBook,
            this.ColumnLastName,
            this.ColumnFirstName,
            this.ColumnPatronymic});
			this.dataGridViewStudents.Location = new System.Drawing.Point(0, 33);
			this.dataGridViewStudents.Name = "dataGridViewStudents";
			this.dataGridViewStudents.RowHeadersVisible = false;
			this.dataGridViewStudents.Size = new System.Drawing.Size(734, 180);
			this.dataGridViewStudents.TabIndex = 3;
			// 
			// ColumnSelect
			// 
			this.ColumnSelect.HeaderText = "Выбрать";
			this.ColumnSelect.Name = "ColumnSelect";
			this.ColumnSelect.Width = 70;
			// 
			// ColumnId
			// 
			this.ColumnId.HeaderText = "ColumnId";
			this.ColumnId.Name = "ColumnId";
			this.ColumnId.Visible = false;
			// 
			// ColumnNumberOfBook
			// 
			this.ColumnNumberOfBook.HeaderText = "Номер зачетки";
			this.ColumnNumberOfBook.Name = "ColumnNumberOfBook";
			this.ColumnNumberOfBook.Width = 110;
			// 
			// ColumnLastName
			// 
			this.ColumnLastName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColumnLastName.HeaderText = "Фамилия";
			this.ColumnLastName.Name = "ColumnLastName";
			// 
			// ColumnFirstName
			// 
			this.ColumnFirstName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColumnFirstName.HeaderText = "Имя";
			this.ColumnFirstName.Name = "ColumnFirstName";
			// 
			// ColumnPatronymic
			// 
			this.ColumnPatronymic.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColumnPatronymic.HeaderText = "Отчество";
			this.ColumnPatronymic.Name = "ColumnPatronymic";
			// 
			// textBoxRecoveryOrderNumber
			// 
			this.textBoxRecoveryOrderNumber.Location = new System.Drawing.Point(77, 6);
			this.textBoxRecoveryOrderNumber.Name = "textBoxRecoveryOrderNumber";
			this.textBoxRecoveryOrderNumber.Size = new System.Drawing.Size(100, 20);
			this.textBoxRecoveryOrderNumber.TabIndex = 1;
			// 
			// labelRecoveryOrderNumber
			// 
			this.labelRecoveryOrderNumber.AutoSize = true;
			this.labelRecoveryOrderNumber.Location = new System.Drawing.Point(12, 9);
			this.labelRecoveryOrderNumber.Name = "labelRecoveryOrderNumber";
			this.labelRecoveryOrderNumber.Size = new System.Drawing.Size(59, 13);
			this.labelRecoveryOrderNumber.TabIndex = 0;
			this.labelRecoveryOrderNumber.Text = "Приказ №";
			// 
			// dateTimePickerRecoveryDate
			// 
			this.dateTimePickerRecoveryDate.Location = new System.Drawing.Point(183, 6);
			this.dateTimePickerRecoveryDate.Name = "dateTimePickerRecoveryDate";
			this.dateTimePickerRecoveryDate.Size = new System.Drawing.Size(141, 20);
			this.dateTimePickerRecoveryDate.TabIndex = 2;
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.Location = new System.Drawing.Point(646, 219);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 25);
			this.buttonClose.TabIndex = 5;
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
			this.buttonSave.TabIndex = 4;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
			// 
			// comboBoxNewStudentGroup
			// 
			this.comboBoxNewStudentGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxNewStudentGroup.FormattingEnabled = true;
			this.comboBoxNewStudentGroup.Location = new System.Drawing.Point(565, 6);
			this.comboBoxNewStudentGroup.Name = "comboBoxNewStudentGroup";
			this.comboBoxNewStudentGroup.Size = new System.Drawing.Size(130, 21);
			this.comboBoxNewStudentGroup.TabIndex = 7;
			// 
			// labelStudentGroup
			// 
			this.labelStudentGroup.AutoSize = true;
			this.labelStudentGroup.Location = new System.Drawing.Point(335, 9);
			this.labelStudentGroup.Name = "labelStudentGroup";
			this.labelStudentGroup.Size = new System.Drawing.Size(224, 13);
			this.labelStudentGroup.TabIndex = 6;
			this.labelStudentGroup.Text = "Группа, куда восстанавливается студенты";
			// 
			// FormRecovery
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(734, 251);
			this.Controls.Add(this.comboBoxNewStudentGroup);
			this.Controls.Add(this.labelStudentGroup);
			this.Controls.Add(this.dataGridViewStudents);
			this.Controls.Add(this.textBoxRecoveryOrderNumber);
			this.Controls.Add(this.labelRecoveryOrderNumber);
			this.Controls.Add(this.dateTimePickerRecoveryDate);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonSave);
			this.Name = "FormRecovery";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Восстановление студентов";
			this.Load += new System.EventHandler(this.FormRecovery_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewStudents;
        private System.Windows.Forms.TextBox textBoxRecoveryOrderNumber;
        private System.Windows.Forms.Label labelRecoveryOrderNumber;
        private System.Windows.Forms.DateTimePicker dateTimePickerRecoveryDate;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumberOfBook;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPatronymic;
        private System.Windows.Forms.ComboBox comboBoxNewStudentGroup;
        private System.Windows.Forms.Label labelStudentGroup;
    }
}