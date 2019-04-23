namespace BaseControlsAndForms.Services
{
    partial class FormFinishEducation
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
            this.labelFrom = new System.Windows.Forms.Label();
            this.textBoxFinishEducationOrderNumber = new System.Windows.Forms.TextBox();
            this.labelFinishEducationOrderNumber = new System.Windows.Forms.Label();
            this.checkBoxSelectAll = new System.Windows.Forms.CheckBox();
            this.dataGridViewStudents = new System.Windows.Forms.DataGridView();
            this.ColumnSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNumberOfBook = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPatronymic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.dateTimePickerFinishEducationOrderDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).BeginInit();
            this.SuspendLayout();
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
            // textBoxFinishEducationOrderNumber
            // 
            this.textBoxFinishEducationOrderNumber.Location = new System.Drawing.Point(77, 6);
            this.textBoxFinishEducationOrderNumber.Name = "textBoxFinishEducationOrderNumber";
            this.textBoxFinishEducationOrderNumber.Size = new System.Drawing.Size(100, 20);
            this.textBoxFinishEducationOrderNumber.TabIndex = 1;
            // 
            // labelFinishEducationOrderNumber
            // 
            this.labelFinishEducationOrderNumber.AutoSize = true;
            this.labelFinishEducationOrderNumber.Location = new System.Drawing.Point(12, 9);
            this.labelFinishEducationOrderNumber.Name = "labelFinishEducationOrderNumber";
            this.labelFinishEducationOrderNumber.Size = new System.Drawing.Size(59, 13);
            this.labelFinishEducationOrderNumber.TabIndex = 0;
            this.labelFinishEducationOrderNumber.Text = "Приказ №";
            // 
            // checkBoxSelectAll
            // 
            this.checkBoxSelectAll.AutoSize = true;
            this.checkBoxSelectAll.Location = new System.Drawing.Point(12, 35);
            this.checkBoxSelectAll.Name = "checkBoxSelectAll";
            this.checkBoxSelectAll.Size = new System.Drawing.Size(91, 17);
            this.checkBoxSelectAll.TabIndex = 4;
            this.checkBoxSelectAll.Text = "Выбрать все";
            this.checkBoxSelectAll.UseVisualStyleBackColor = true;
            this.checkBoxSelectAll.CheckedChanged += new System.EventHandler(this.CheckBoxSelectAll_CheckedChanged);
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
            this.Id,
            this.ColumnNumberOfBook,
            this.ColumnLastName,
            this.ColumnFirstName,
            this.ColumnPatronymic});
            this.dataGridViewStudents.Location = new System.Drawing.Point(0, 60);
            this.dataGridViewStudents.Name = "dataGridViewStudents";
            this.dataGridViewStudents.RowHeadersVisible = false;
            this.dataGridViewStudents.Size = new System.Drawing.Size(734, 330);
            this.dataGridViewStudents.TabIndex = 5;
            // 
            // ColumnSelect
            // 
            this.ColumnSelect.HeaderText = "Выбрать";
            this.ColumnSelect.Name = "ColumnSelect";
            this.ColumnSelect.Width = 60;
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
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(646, 400);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 25);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(565, 400);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 25);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // dateTimePickerFinishEducationOrderDate
            // 
            this.dateTimePickerFinishEducationOrderDate.Location = new System.Drawing.Point(207, 6);
            this.dateTimePickerFinishEducationOrderDate.Name = "dateTimePickerFinishEducationOrderDate";
            this.dateTimePickerFinishEducationOrderDate.Size = new System.Drawing.Size(141, 20);
            this.dateTimePickerFinishEducationOrderDate.TabIndex = 3;
            // 
            // FormFinishEducation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 431);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.textBoxFinishEducationOrderNumber);
            this.Controls.Add(this.labelFinishEducationOrderNumber);
            this.Controls.Add(this.checkBoxSelectAll);
            this.Controls.Add(this.dataGridViewStudents);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.dateTimePickerFinishEducationOrderDate);
            this.Name = "FormFinishEducation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Завершение обучения";
            this.Load += new System.EventHandler(this.FormFinishEducation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.TextBox textBoxFinishEducationOrderNumber;
        private System.Windows.Forms.Label labelFinishEducationOrderNumber;
        private System.Windows.Forms.CheckBox checkBoxSelectAll;
        private System.Windows.Forms.DataGridView dataGridViewStudents;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.DateTimePicker dateTimePickerFinishEducationOrderDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumberOfBook;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPatronymic;
    }
}