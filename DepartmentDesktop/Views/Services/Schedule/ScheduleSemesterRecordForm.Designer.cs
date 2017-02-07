namespace DepartmentDesktop.Views.Services.Schedule
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
            this.labelClassroomId = new System.Windows.Forms.Label();
            this.comboBoxWeek = new System.Windows.Forms.ComboBox();
            this.labelWeek = new System.Windows.Forms.Label();
            this.comboBoxDay = new System.Windows.Forms.ComboBox();
            this.comboBoxLesson = new System.Windows.Forms.ComboBox();
            this.labelLesson = new System.Windows.Forms.Label();
            this.labelDay = new System.Windows.Forms.Label();
            this.labelLessonType = new System.Windows.Forms.Label();
            this.labelLessonGroupName = new System.Windows.Forms.Label();
            this.labelLessonTeacher = new System.Windows.Forms.Label();
            this.labelLessonDiscipline = new System.Windows.Forms.Label();
            this.textBoxLessonDiscipline = new System.Windows.Forms.TextBox();
            this.textBoxLessonTeacher = new System.Windows.Forms.TextBox();
            this.textBoxLessonGroupName = new System.Windows.Forms.TextBox();
            this.textBoxClassroomId = new System.Windows.Forms.TextBox();
            this.comboBoxLessonType = new System.Windows.Forms.ComboBox();
            this.checkBoxApplyToAnalogRecords = new System.Windows.Forms.CheckBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelClassroomId
            // 
            this.labelClassroomId.AutoSize = true;
            this.labelClassroomId.Location = new System.Drawing.Point(12, 87);
            this.labelClassroomId.Name = "labelClassroomId";
            this.labelClassroomId.Size = new System.Drawing.Size(60, 13);
            this.labelClassroomId.TabIndex = 6;
            this.labelClassroomId.Text = "Аудитория";
            // 
            // comboBoxWeek
            // 
            this.comboBoxWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWeek.FormattingEnabled = true;
            this.comboBoxWeek.Items.AddRange(new object[] {
            "1",
            "2"});
            this.comboBoxWeek.Location = new System.Drawing.Point(63, 137);
            this.comboBoxWeek.Name = "comboBoxWeek";
            this.comboBoxWeek.Size = new System.Drawing.Size(38, 21);
            this.comboBoxWeek.TabIndex = 11;
            // 
            // labelWeek
            // 
            this.labelWeek.AutoSize = true;
            this.labelWeek.Location = new System.Drawing.Point(12, 140);
            this.labelWeek.Name = "labelWeek";
            this.labelWeek.Size = new System.Drawing.Size(45, 13);
            this.labelWeek.TabIndex = 10;
            this.labelWeek.Text = "Неделя";
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
            this.comboBoxDay.Location = new System.Drawing.Point(154, 137);
            this.comboBoxDay.Name = "comboBoxDay";
            this.comboBoxDay.Size = new System.Drawing.Size(38, 21);
            this.comboBoxDay.TabIndex = 13;
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
            this.comboBoxLesson.Location = new System.Drawing.Point(247, 137);
            this.comboBoxLesson.Name = "comboBoxLesson";
            this.comboBoxLesson.Size = new System.Drawing.Size(38, 21);
            this.comboBoxLesson.TabIndex = 15;
            // 
            // labelLesson
            // 
            this.labelLesson.AutoSize = true;
            this.labelLesson.Location = new System.Drawing.Point(208, 140);
            this.labelLesson.Name = "labelLesson";
            this.labelLesson.Size = new System.Drawing.Size(33, 13);
            this.labelLesson.TabIndex = 14;
            this.labelLesson.Text = "Пара";
            // 
            // labelDay
            // 
            this.labelDay.AutoSize = true;
            this.labelDay.Location = new System.Drawing.Point(114, 140);
            this.labelDay.Name = "labelDay";
            this.labelDay.Size = new System.Drawing.Size(34, 13);
            this.labelDay.TabIndex = 12;
            this.labelDay.Text = "День";
            // 
            // labelLessonType
            // 
            this.labelLessonType.AutoSize = true;
            this.labelLessonType.Location = new System.Drawing.Point(12, 113);
            this.labelLessonType.Name = "labelLessonType";
            this.labelLessonType.Size = new System.Drawing.Size(70, 13);
            this.labelLessonType.TabIndex = 8;
            this.labelLessonType.Text = "Тип занятия";
            // 
            // labelLessonGroupName
            // 
            this.labelLessonGroupName.AutoSize = true;
            this.labelLessonGroupName.Location = new System.Drawing.Point(12, 61);
            this.labelLessonGroupName.Name = "labelLessonGroupName";
            this.labelLessonGroupName.Size = new System.Drawing.Size(42, 13);
            this.labelLessonGroupName.TabIndex = 4;
            this.labelLessonGroupName.Text = "Группа";
            // 
            // labelLessonTeacher
            // 
            this.labelLessonTeacher.AutoSize = true;
            this.labelLessonTeacher.Location = new System.Drawing.Point(12, 35);
            this.labelLessonTeacher.Name = "labelLessonTeacher";
            this.labelLessonTeacher.Size = new System.Drawing.Size(86, 13);
            this.labelLessonTeacher.TabIndex = 2;
            this.labelLessonTeacher.Text = "Преподаватель";
            // 
            // labelLessonDiscipline
            // 
            this.labelLessonDiscipline.AutoSize = true;
            this.labelLessonDiscipline.Location = new System.Drawing.Point(12, 9);
            this.labelLessonDiscipline.Name = "labelLessonDiscipline";
            this.labelLessonDiscipline.Size = new System.Drawing.Size(52, 13);
            this.labelLessonDiscipline.TabIndex = 0;
            this.labelLessonDiscipline.Text = "Предмет";
            // 
            // textBoxLessonDiscipline
            // 
            this.textBoxLessonDiscipline.Location = new System.Drawing.Point(104, 6);
            this.textBoxLessonDiscipline.Name = "textBoxLessonDiscipline";
            this.textBoxLessonDiscipline.Size = new System.Drawing.Size(181, 20);
            this.textBoxLessonDiscipline.TabIndex = 1;
            // 
            // textBoxLessonTeacher
            // 
            this.textBoxLessonTeacher.Location = new System.Drawing.Point(104, 32);
            this.textBoxLessonTeacher.Name = "textBoxLessonTeacher";
            this.textBoxLessonTeacher.Size = new System.Drawing.Size(181, 20);
            this.textBoxLessonTeacher.TabIndex = 3;
            // 
            // textBoxLessonGroupName
            // 
            this.textBoxLessonGroupName.Location = new System.Drawing.Point(104, 58);
            this.textBoxLessonGroupName.Name = "textBoxLessonGroupName";
            this.textBoxLessonGroupName.Size = new System.Drawing.Size(181, 20);
            this.textBoxLessonGroupName.TabIndex = 5;
            // 
            // textBoxClassroomId
            // 
            this.textBoxClassroomId.Location = new System.Drawing.Point(104, 84);
            this.textBoxClassroomId.Name = "textBoxClassroomId";
            this.textBoxClassroomId.Size = new System.Drawing.Size(181, 20);
            this.textBoxClassroomId.TabIndex = 7;
            // 
            // comboBoxLessonType
            // 
            this.comboBoxLessonType.FormattingEnabled = true;
            this.comboBoxLessonType.Location = new System.Drawing.Point(104, 110);
            this.comboBoxLessonType.Name = "comboBoxLessonType";
            this.comboBoxLessonType.Size = new System.Drawing.Size(181, 21);
            this.comboBoxLessonType.TabIndex = 9;
            // 
            // checkBoxApplyToAnalogRecords
            // 
            this.checkBoxApplyToAnalogRecords.AutoSize = true;
            this.checkBoxApplyToAnalogRecords.Location = new System.Drawing.Point(15, 174);
            this.checkBoxApplyToAnalogRecords.Name = "checkBoxApplyToAnalogRecords";
            this.checkBoxApplyToAnalogRecords.Size = new System.Drawing.Size(210, 17);
            this.checkBoxApplyToAnalogRecords.TabIndex = 16;
            this.checkBoxApplyToAnalogRecords.Text = "Применить к аналоигчным записям";
            this.checkBoxApplyToAnalogRecords.UseVisualStyleBackColor = true;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(210, 197);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 18;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(129, 197);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 17;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // ScheduleSemesterRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 231);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.checkBoxApplyToAnalogRecords);
            this.Controls.Add(this.comboBoxLessonType);
            this.Controls.Add(this.textBoxClassroomId);
            this.Controls.Add(this.textBoxLessonGroupName);
            this.Controls.Add(this.textBoxLessonTeacher);
            this.Controls.Add(this.textBoxLessonDiscipline);
            this.Controls.Add(this.labelClassroomId);
            this.Controls.Add(this.comboBoxWeek);
            this.Controls.Add(this.labelWeek);
            this.Controls.Add(this.comboBoxDay);
            this.Controls.Add(this.comboBoxLesson);
            this.Controls.Add(this.labelLesson);
            this.Controls.Add(this.labelDay);
            this.Controls.Add(this.labelLessonType);
            this.Controls.Add(this.labelLessonGroupName);
            this.Controls.Add(this.labelLessonTeacher);
            this.Controls.Add(this.labelLessonDiscipline);
            this.Name = "ScheduleSemesterRecordForm";
            this.Text = "Запись семестра";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelClassroomId;
        private System.Windows.Forms.ComboBox comboBoxWeek;
        private System.Windows.Forms.Label labelWeek;
        private System.Windows.Forms.ComboBox comboBoxDay;
        private System.Windows.Forms.ComboBox comboBoxLesson;
        private System.Windows.Forms.Label labelLesson;
        private System.Windows.Forms.Label labelDay;
        private System.Windows.Forms.Label labelLessonType;
        private System.Windows.Forms.Label labelLessonGroupName;
        private System.Windows.Forms.Label labelLessonTeacher;
        private System.Windows.Forms.Label labelLessonDiscipline;
        private System.Windows.Forms.TextBox textBoxLessonDiscipline;
        private System.Windows.Forms.TextBox textBoxLessonTeacher;
        private System.Windows.Forms.TextBox textBoxLessonGroupName;
        private System.Windows.Forms.TextBox textBoxClassroomId;
        private System.Windows.Forms.ComboBox comboBoxLessonType;
        private System.Windows.Forms.CheckBox checkBoxApplyToAnalogRecords;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
    }
}