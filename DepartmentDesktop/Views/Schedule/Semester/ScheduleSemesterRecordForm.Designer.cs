namespace DepartmentDesktop.Views.Schedule.Semester
{
    partial class ScheduleSemesterRecordForm
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
            this.labelLessonType = new System.Windows.Forms.Label();
            this.panelDateTime = new System.Windows.Forms.Panel();
            this.labelWeek = new System.Windows.Forms.Label();
            this.comboBoxWeek = new System.Windows.Forms.ComboBox();
            this.labelDay = new System.Windows.Forms.Label();
            this.labelLesson = new System.Windows.Forms.Label();
            this.comboBoxLesson = new System.Windows.Forms.ComboBox();
            this.comboBoxDay = new System.Windows.Forms.ComboBox();
            this.panelBaseData = new System.Windows.Forms.Panel();
            this.comboBoxDiscipline = new System.Windows.Forms.ComboBox();
            this.comboBoxLecturer = new System.Windows.Forms.ComboBox();
            this.comboBoxClassroom = new System.Windows.Forms.ComboBox();
            this.comboBoxStudentGroup = new System.Windows.Forms.ComboBox();
            this.panelTextData = new System.Windows.Forms.Panel();
            this.textBoxLessonDiscipline = new System.Windows.Forms.TextBox();
            this.textBoxLessonLecturer = new System.Windows.Forms.TextBox();
            this.comboBoxLessonType = new System.Windows.Forms.ComboBox();
            this.textBoxLessonGroup = new System.Windows.Forms.TextBox();
            this.textBoxClassroom = new System.Windows.Forms.TextBox();
            this.labelClassroom = new System.Windows.Forms.Label();
            this.labelLessonGroup = new System.Windows.Forms.Label();
            this.labelLessonLecturer = new System.Windows.Forms.Label();
            this.labelLessonDiscipline = new System.Windows.Forms.Label();
            this.dataGridViewRecords = new System.Windows.Forms.DataGridView();
            this.groupBoxSearchBy = new System.Windows.Forms.GroupBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.checkBoxClassroom = new System.Windows.Forms.CheckBox();
            this.checkBoxGroupName = new System.Windows.Forms.CheckBox();
            this.checkBoxLecturer = new System.Windows.Forms.CheckBox();
            this.checkBoxDiscipline = new System.Windows.Forms.CheckBox();
            this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnRecord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonSaveOther = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxNotParseRecord = new System.Windows.Forms.TextBox();
            this.labelNotParseRecord = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.panelDateTime.SuspendLayout();
            this.panelBaseData.SuspendLayout();
            this.panelTextData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecords)).BeginInit();
            this.groupBoxSearchBy.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelLessonType
            // 
            this.labelLessonType.AutoSize = true;
            this.labelLessonType.Location = new System.Drawing.Point(12, 125);
            this.labelLessonType.Name = "labelLessonType";
            this.labelLessonType.Size = new System.Drawing.Size(70, 13);
            this.labelLessonType.TabIndex = 4;
            this.labelLessonType.Text = "Тип занятия";
            // 
            // panelDateTime
            // 
            this.panelDateTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelDateTime.Controls.Add(this.labelWeek);
            this.panelDateTime.Controls.Add(this.comboBoxWeek);
            this.panelDateTime.Controls.Add(this.labelDay);
            this.panelDateTime.Controls.Add(this.labelLesson);
            this.panelDateTime.Controls.Add(this.comboBoxLesson);
            this.panelDateTime.Controls.Add(this.comboBoxDay);
            this.panelDateTime.Location = new System.Drawing.Point(104, 152);
            this.panelDateTime.Name = "panelDateTime";
            this.panelDateTime.Size = new System.Drawing.Size(386, 38);
            this.panelDateTime.TabIndex = 7;
            // 
            // labelWeek
            // 
            this.labelWeek.AutoSize = true;
            this.labelWeek.Location = new System.Drawing.Point(9, 11);
            this.labelWeek.Name = "labelWeek";
            this.labelWeek.Size = new System.Drawing.Size(45, 13);
            this.labelWeek.TabIndex = 0;
            this.labelWeek.Text = "Неделя";
            // 
            // comboBoxWeek
            // 
            this.comboBoxWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWeek.FormattingEnabled = true;
            this.comboBoxWeek.Items.AddRange(new object[] {
            "1",
            "2"});
            this.comboBoxWeek.Location = new System.Drawing.Point(60, 8);
            this.comboBoxWeek.Name = "comboBoxWeek";
            this.comboBoxWeek.Size = new System.Drawing.Size(45, 21);
            this.comboBoxWeek.TabIndex = 1;
            // 
            // labelDay
            // 
            this.labelDay.AutoSize = true;
            this.labelDay.Location = new System.Drawing.Point(127, 11);
            this.labelDay.Name = "labelDay";
            this.labelDay.Size = new System.Drawing.Size(34, 13);
            this.labelDay.TabIndex = 2;
            this.labelDay.Text = "День";
            // 
            // labelLesson
            // 
            this.labelLesson.AutoSize = true;
            this.labelLesson.Location = new System.Drawing.Point(244, 11);
            this.labelLesson.Name = "labelLesson";
            this.labelLesson.Size = new System.Drawing.Size(33, 13);
            this.labelLesson.TabIndex = 4;
            this.labelLesson.Text = "Пара";
            // 
            // comboBoxLesson
            // 
            this.comboBoxLesson.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLesson.FormattingEnabled = true;
            this.comboBoxLesson.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.comboBoxLesson.Location = new System.Drawing.Point(283, 8);
            this.comboBoxLesson.Name = "comboBoxLesson";
            this.comboBoxLesson.Size = new System.Drawing.Size(45, 21);
            this.comboBoxLesson.TabIndex = 5;
            // 
            // comboBoxDay
            // 
            this.comboBoxDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDay.FormattingEnabled = true;
            this.comboBoxDay.Items.AddRange(new object[] {
            "Пн",
            "Вт",
            "Ср",
            "Чт",
            "Пт",
            "Сб",
            "Вс"});
            this.comboBoxDay.Location = new System.Drawing.Point(167, 8);
            this.comboBoxDay.Name = "comboBoxDay";
            this.comboBoxDay.Size = new System.Drawing.Size(45, 21);
            this.comboBoxDay.TabIndex = 3;
            // 
            // panelBaseData
            // 
            this.panelBaseData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelBaseData.Controls.Add(this.comboBoxDiscipline);
            this.panelBaseData.Controls.Add(this.comboBoxLecturer);
            this.panelBaseData.Controls.Add(this.comboBoxClassroom);
            this.panelBaseData.Controls.Add(this.comboBoxStudentGroup);
            this.panelBaseData.Location = new System.Drawing.Point(300, 10);
            this.panelBaseData.Name = "panelBaseData";
            this.panelBaseData.Size = new System.Drawing.Size(190, 136);
            this.panelBaseData.TabIndex = 6;
            // 
            // comboBoxDiscipline
            // 
            this.comboBoxDiscipline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiscipline.FormattingEnabled = true;
            this.comboBoxDiscipline.Location = new System.Drawing.Point(3, 6);
            this.comboBoxDiscipline.Name = "comboBoxDiscipline";
            this.comboBoxDiscipline.Size = new System.Drawing.Size(180, 21);
            this.comboBoxDiscipline.TabIndex = 0;
            this.comboBoxDiscipline.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDiscipline_SelectedIndexChanged);
            // 
            // comboBoxLecturer
            // 
            this.comboBoxLecturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLecturer.FormattingEnabled = true;
            this.comboBoxLecturer.Location = new System.Drawing.Point(3, 34);
            this.comboBoxLecturer.Name = "comboBoxLecturer";
            this.comboBoxLecturer.Size = new System.Drawing.Size(180, 21);
            this.comboBoxLecturer.TabIndex = 1;
            this.comboBoxLecturer.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLecturer_SelectedIndexChanged);
            // 
            // comboBoxClassroom
            // 
            this.comboBoxClassroom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClassroom.FormattingEnabled = true;
            this.comboBoxClassroom.Location = new System.Drawing.Point(3, 86);
            this.comboBoxClassroom.Name = "comboBoxClassroom";
            this.comboBoxClassroom.Size = new System.Drawing.Size(180, 21);
            this.comboBoxClassroom.TabIndex = 3;
            this.comboBoxClassroom.SelectedIndexChanged += new System.EventHandler(this.ComboBoxClassroom_SelectedIndexChanged);
            // 
            // comboBoxStudentGroup
            // 
            this.comboBoxStudentGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudentGroup.FormattingEnabled = true;
            this.comboBoxStudentGroup.Location = new System.Drawing.Point(3, 60);
            this.comboBoxStudentGroup.Name = "comboBoxStudentGroup";
            this.comboBoxStudentGroup.Size = new System.Drawing.Size(180, 21);
            this.comboBoxStudentGroup.TabIndex = 2;
            this.comboBoxStudentGroup.SelectedIndexChanged += new System.EventHandler(this.ComboBoxStudentGroup_SelectedIndexChanged);
            // 
            // panelTextData
            // 
            this.panelTextData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelTextData.Controls.Add(this.textBoxLessonDiscipline);
            this.panelTextData.Controls.Add(this.textBoxLessonLecturer);
            this.panelTextData.Controls.Add(this.comboBoxLessonType);
            this.panelTextData.Controls.Add(this.textBoxLessonGroup);
            this.panelTextData.Controls.Add(this.textBoxClassroom);
            this.panelTextData.Location = new System.Drawing.Point(104, 10);
            this.panelTextData.Name = "panelTextData";
            this.panelTextData.Size = new System.Drawing.Size(190, 136);
            this.panelTextData.TabIndex = 5;
            // 
            // textBoxLessonDiscipline
            // 
            this.textBoxLessonDiscipline.Location = new System.Drawing.Point(3, 6);
            this.textBoxLessonDiscipline.Name = "textBoxLessonDiscipline";
            this.textBoxLessonDiscipline.Size = new System.Drawing.Size(180, 20);
            this.textBoxLessonDiscipline.TabIndex = 0;
            // 
            // textBoxLessonLecturer
            // 
            this.textBoxLessonLecturer.Location = new System.Drawing.Point(3, 32);
            this.textBoxLessonLecturer.Name = "textBoxLessonLecturer";
            this.textBoxLessonLecturer.Size = new System.Drawing.Size(180, 20);
            this.textBoxLessonLecturer.TabIndex = 1;
            // 
            // comboBoxLessonType
            // 
            this.comboBoxLessonType.FormattingEnabled = true;
            this.comboBoxLessonType.Location = new System.Drawing.Point(3, 110);
            this.comboBoxLessonType.Name = "comboBoxLessonType";
            this.comboBoxLessonType.Size = new System.Drawing.Size(94, 21);
            this.comboBoxLessonType.TabIndex = 4;
            // 
            // textBoxLessonGroup
            // 
            this.textBoxLessonGroup.Location = new System.Drawing.Point(3, 58);
            this.textBoxLessonGroup.Name = "textBoxLessonGroup";
            this.textBoxLessonGroup.Size = new System.Drawing.Size(180, 20);
            this.textBoxLessonGroup.TabIndex = 2;
            // 
            // textBoxClassroom
            // 
            this.textBoxClassroom.Location = new System.Drawing.Point(3, 84);
            this.textBoxClassroom.Name = "textBoxClassroom";
            this.textBoxClassroom.Size = new System.Drawing.Size(180, 20);
            this.textBoxClassroom.TabIndex = 3;
            // 
            // labelClassroom
            // 
            this.labelClassroom.AutoSize = true;
            this.labelClassroom.Location = new System.Drawing.Point(12, 99);
            this.labelClassroom.Name = "labelClassroom";
            this.labelClassroom.Size = new System.Drawing.Size(60, 13);
            this.labelClassroom.TabIndex = 3;
            this.labelClassroom.Text = "Аудитория";
            // 
            // labelLessonGroup
            // 
            this.labelLessonGroup.AutoSize = true;
            this.labelLessonGroup.Location = new System.Drawing.Point(12, 73);
            this.labelLessonGroup.Name = "labelLessonGroup";
            this.labelLessonGroup.Size = new System.Drawing.Size(42, 13);
            this.labelLessonGroup.TabIndex = 2;
            this.labelLessonGroup.Text = "Группа";
            // 
            // labelLessonLecturer
            // 
            this.labelLessonLecturer.AutoSize = true;
            this.labelLessonLecturer.Location = new System.Drawing.Point(12, 47);
            this.labelLessonLecturer.Name = "labelLessonLecturer";
            this.labelLessonLecturer.Size = new System.Drawing.Size(86, 13);
            this.labelLessonLecturer.TabIndex = 1;
            this.labelLessonLecturer.Text = "Преподаватель";
            // 
            // labelLessonDiscipline
            // 
            this.labelLessonDiscipline.AutoSize = true;
            this.labelLessonDiscipline.Location = new System.Drawing.Point(12, 21);
            this.labelLessonDiscipline.Name = "labelLessonDiscipline";
            this.labelLessonDiscipline.Size = new System.Drawing.Size(52, 13);
            this.labelLessonDiscipline.TabIndex = 0;
            this.labelLessonDiscipline.Text = "Предмет";
            // 
            // dataGridViewRecords
            // 
            this.dataGridViewRecords.AllowUserToAddRows = false;
            this.dataGridViewRecords.AllowUserToDeleteRows = false;
            this.dataGridViewRecords.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRecords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnId,
            this.ColumnSelect,
            this.ColumnRecord});
            this.dataGridViewRecords.Location = new System.Drawing.Point(496, 116);
            this.dataGridViewRecords.Name = "dataGridViewRecords";
            this.dataGridViewRecords.RowHeadersVisible = false;
            this.dataGridViewRecords.Size = new System.Drawing.Size(350, 129);
            this.dataGridViewRecords.TabIndex = 8;
            // 
            // groupBoxSearchBy
            // 
            this.groupBoxSearchBy.Controls.Add(this.buttonSaveOther);
            this.groupBoxSearchBy.Controls.Add(this.buttonSearch);
            this.groupBoxSearchBy.Controls.Add(this.checkBoxClassroom);
            this.groupBoxSearchBy.Controls.Add(this.checkBoxGroupName);
            this.groupBoxSearchBy.Controls.Add(this.checkBoxLecturer);
            this.groupBoxSearchBy.Controls.Add(this.checkBoxDiscipline);
            this.groupBoxSearchBy.Location = new System.Drawing.Point(496, 10);
            this.groupBoxSearchBy.Name = "groupBoxSearchBy";
            this.groupBoxSearchBy.Size = new System.Drawing.Size(350, 100);
            this.groupBoxSearchBy.TabIndex = 9;
            this.groupBoxSearchBy.TabStop = false;
            this.groupBoxSearchBy.Text = "Найти пары по";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(257, 25);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 4;
            this.buttonSearch.Text = "Найти";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // checkBoxClassroom
            // 
            this.checkBoxClassroom.AutoSize = true;
            this.checkBoxClassroom.Location = new System.Drawing.Point(132, 63);
            this.checkBoxClassroom.Name = "checkBoxClassroom";
            this.checkBoxClassroom.Size = new System.Drawing.Size(79, 17);
            this.checkBoxClassroom.TabIndex = 3;
            this.checkBoxClassroom.Text = "Аудитория";
            this.checkBoxClassroom.UseVisualStyleBackColor = true;
            // 
            // checkBoxGroupName
            // 
            this.checkBoxGroupName.AutoSize = true;
            this.checkBoxGroupName.Location = new System.Drawing.Point(19, 63);
            this.checkBoxGroupName.Name = "checkBoxGroupName";
            this.checkBoxGroupName.Size = new System.Drawing.Size(61, 17);
            this.checkBoxGroupName.TabIndex = 2;
            this.checkBoxGroupName.Text = "Группа";
            this.checkBoxGroupName.UseVisualStyleBackColor = true;
            // 
            // checkBoxLecturer
            // 
            this.checkBoxLecturer.AutoSize = true;
            this.checkBoxLecturer.Location = new System.Drawing.Point(132, 29);
            this.checkBoxLecturer.Name = "checkBoxLecturer";
            this.checkBoxLecturer.Size = new System.Drawing.Size(105, 17);
            this.checkBoxLecturer.TabIndex = 1;
            this.checkBoxLecturer.Text = "Преподаватель";
            this.checkBoxLecturer.UseVisualStyleBackColor = true;
            // 
            // checkBoxDiscipline
            // 
            this.checkBoxDiscipline.AutoSize = true;
            this.checkBoxDiscipline.Location = new System.Drawing.Point(19, 29);
            this.checkBoxDiscipline.Name = "checkBoxDiscipline";
            this.checkBoxDiscipline.Size = new System.Drawing.Size(89, 17);
            this.checkBoxDiscipline.TabIndex = 0;
            this.checkBoxDiscipline.Text = "Дисциплина";
            this.checkBoxDiscipline.UseVisualStyleBackColor = true;
            // 
            // ColumnId
            // 
            this.ColumnId.HeaderText = "Id";
            this.ColumnId.Name = "ColumnId";
            this.ColumnId.Visible = false;
            // 
            // ColumnSelect
            // 
            this.ColumnSelect.HeaderText = "Выбрать";
            this.ColumnSelect.Name = "ColumnSelect";
            this.ColumnSelect.Width = 60;
            // 
            // ColumnRecord
            // 
            this.ColumnRecord.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnRecord.HeaderText = "Запись";
            this.ColumnRecord.Name = "ColumnRecord";
            this.ColumnRecord.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnRecord.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // buttonSaveOther
            // 
            this.buttonSaveOther.Location = new System.Drawing.Point(257, 59);
            this.buttonSaveOther.Name = "buttonSaveOther";
            this.buttonSaveOther.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveOther.TabIndex = 5;
            this.buttonSaveOther.Text = "Сохранить";
            this.buttonSaveOther.UseVisualStyleBackColor = true;
            this.buttonSaveOther.Click += new System.EventHandler(this.ButtonSaveOther_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(415, 222);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(279, 222);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(130, 23);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Сохранить и закрыть";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // textBoxNotParseRecord
            // 
            this.textBoxNotParseRecord.Location = new System.Drawing.Point(104, 196);
            this.textBoxNotParseRecord.Name = "textBoxNotParseRecord";
            this.textBoxNotParseRecord.ReadOnly = true;
            this.textBoxNotParseRecord.Size = new System.Drawing.Size(386, 20);
            this.textBoxNotParseRecord.TabIndex = 9;
            // 
            // labelNotParseRecord
            // 
            this.labelNotParseRecord.AutoSize = true;
            this.labelNotParseRecord.Location = new System.Drawing.Point(12, 199);
            this.labelNotParseRecord.Name = "labelNotParseRecord";
            this.labelNotParseRecord.Size = new System.Drawing.Size(46, 13);
            this.labelNotParseRecord.TabIndex = 8;
            this.labelNotParseRecord.Text = "Строка:";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(198, 222);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 12;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // ScheduleSemesterRecordForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 256);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.labelNotParseRecord);
            this.Controls.Add(this.textBoxNotParseRecord);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBoxSearchBy);
            this.Controls.Add(this.dataGridViewRecords);
            this.Controls.Add(this.labelLessonType);
            this.Controls.Add(this.panelDateTime);
            this.Controls.Add(this.panelBaseData);
            this.Controls.Add(this.panelTextData);
            this.Controls.Add(this.labelClassroom);
            this.Controls.Add(this.labelLessonGroup);
            this.Controls.Add(this.labelLessonLecturer);
            this.Controls.Add(this.labelLessonDiscipline);
            this.Name = "ScheduleSemesterRecordForm2";
            this.Text = "ScheduleSemesterRecordForm2";
            this.Load += new System.EventHandler(this.ScheduleSemesterRecordForm_Load);
            this.panelDateTime.ResumeLayout(false);
            this.panelDateTime.PerformLayout();
            this.panelBaseData.ResumeLayout(false);
            this.panelTextData.ResumeLayout(false);
            this.panelTextData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecords)).EndInit();
            this.groupBoxSearchBy.ResumeLayout(false);
            this.groupBoxSearchBy.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLessonType;
        private System.Windows.Forms.Panel panelDateTime;
        private System.Windows.Forms.Label labelWeek;
        private System.Windows.Forms.ComboBox comboBoxWeek;
        private System.Windows.Forms.Label labelDay;
        private System.Windows.Forms.Label labelLesson;
        private System.Windows.Forms.ComboBox comboBoxLesson;
        private System.Windows.Forms.ComboBox comboBoxDay;
        private System.Windows.Forms.Panel panelBaseData;
        private System.Windows.Forms.ComboBox comboBoxDiscipline;
        private System.Windows.Forms.ComboBox comboBoxLecturer;
        private System.Windows.Forms.ComboBox comboBoxClassroom;
        private System.Windows.Forms.ComboBox comboBoxStudentGroup;
        private System.Windows.Forms.Panel panelTextData;
        private System.Windows.Forms.TextBox textBoxLessonDiscipline;
        private System.Windows.Forms.TextBox textBoxLessonLecturer;
        private System.Windows.Forms.ComboBox comboBoxLessonType;
        private System.Windows.Forms.TextBox textBoxLessonGroup;
        private System.Windows.Forms.TextBox textBoxClassroom;
        private System.Windows.Forms.Label labelClassroom;
        private System.Windows.Forms.Label labelLessonGroup;
        private System.Windows.Forms.Label labelLessonLecturer;
        private System.Windows.Forms.Label labelLessonDiscipline;
        private System.Windows.Forms.DataGridView dataGridViewRecords;
        private System.Windows.Forms.GroupBox groupBoxSearchBy;
        private System.Windows.Forms.CheckBox checkBoxClassroom;
        private System.Windows.Forms.CheckBox checkBoxGroupName;
        private System.Windows.Forms.CheckBox checkBoxLecturer;
        private System.Windows.Forms.CheckBox checkBoxDiscipline;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRecord;
        private System.Windows.Forms.Button buttonSaveOther;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxNotParseRecord;
        private System.Windows.Forms.Label labelNotParseRecord;
        private System.Windows.Forms.Button buttonAdd;
    }
}