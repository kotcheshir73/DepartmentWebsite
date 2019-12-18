namespace ScheduleControlsAndForms.Semester
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
            this.panelPeriod = new System.Windows.Forms.Panel();
            this.labelPeriod = new System.Windows.Forms.Label();
            this.radioButtonSecondPeriod = new System.Windows.Forms.RadioButton();
            this.radioButtonFirstPeriod = new System.Windows.Forms.RadioButton();
            this.textBoxNotParseRecord = new System.Windows.Forms.TextBox();
            this.labelNotParseRecord = new System.Windows.Forms.Label();
            this.panelSemester = new System.Windows.Forms.Panel();
            this.labelSemester = new System.Windows.Forms.Label();
            this.radioButtonSpring = new System.Windows.Forms.RadioButton();
            this.radioButtonAutumn = new System.Windows.Forms.RadioButton();
            this.labelWeek = new System.Windows.Forms.Label();
            this.comboBoxWeek = new System.Windows.Forms.ComboBox();
            this.comboBoxLessonType = new System.Windows.Forms.ComboBox();
            this.labelDay = new System.Windows.Forms.Label();
            this.labelLesson = new System.Windows.Forms.Label();
            this.comboBoxLesson = new System.Windows.Forms.ComboBox();
            this.comboBoxDay = new System.Windows.Forms.ComboBox();
            this.comboBoxDiscipline = new System.Windows.Forms.ComboBox();
            this.comboBoxLecturer = new System.Windows.Forms.ComboBox();
            this.comboBoxClassroom = new System.Windows.Forms.ComboBox();
            this.comboBoxStudentGroup = new System.Windows.Forms.ComboBox();
            this.panelTextData = new System.Windows.Forms.Panel();
            this.textBoxLessonDiscipline = new System.Windows.Forms.TextBox();
            this.textBoxLessonLecturer = new System.Windows.Forms.TextBox();
            this.textBoxLessonStudentGroup = new System.Windows.Forms.TextBox();
            this.textBoxLessonClassroom = new System.Windows.Forms.TextBox();
            this.labelLessonDiscipline = new System.Windows.Forms.Label();
            this.labelLessonLecturer = new System.Windows.Forms.Label();
            this.labelLessonClassroom = new System.Windows.Forms.Label();
            this.labelLessonStudentGroup = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panelDateTime.SuspendLayout();
            this.panelPeriod.SuspendLayout();
            this.panelSemester.SuspendLayout();
            this.panelTextData.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelLessonType
            // 
            this.labelLessonType.AutoSize = true;
            this.labelLessonType.Location = new System.Drawing.Point(303, 9);
            this.labelLessonType.Name = "labelLessonType";
            this.labelLessonType.Size = new System.Drawing.Size(70, 13);
            this.labelLessonType.TabIndex = 4;
            this.labelLessonType.Text = "Тип занятия";
            // 
            // panelDateTime
            // 
            this.panelDateTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelDateTime.Controls.Add(this.panelPeriod);
            this.panelDateTime.Controls.Add(this.textBoxNotParseRecord);
            this.panelDateTime.Controls.Add(this.labelNotParseRecord);
            this.panelDateTime.Controls.Add(this.panelSemester);
            this.panelDateTime.Controls.Add(this.labelWeek);
            this.panelDateTime.Controls.Add(this.comboBoxWeek);
            this.panelDateTime.Controls.Add(this.comboBoxLessonType);
            this.panelDateTime.Controls.Add(this.labelDay);
            this.panelDateTime.Controls.Add(this.labelLesson);
            this.panelDateTime.Controls.Add(this.comboBoxLesson);
            this.panelDateTime.Controls.Add(this.labelLessonType);
            this.panelDateTime.Controls.Add(this.comboBoxDay);
            this.panelDateTime.Location = new System.Drawing.Point(10, 131);
            this.panelDateTime.Name = "panelDateTime";
            this.panelDateTime.Size = new System.Drawing.Size(500, 105);
            this.panelDateTime.TabIndex = 7;
            // 
            // panelPeriod
            // 
            this.panelPeriod.Controls.Add(this.labelPeriod);
            this.panelPeriod.Controls.Add(this.radioButtonSecondPeriod);
            this.panelPeriod.Controls.Add(this.radioButtonFirstPeriod);
            this.panelPeriod.Location = new System.Drawing.Point(306, 35);
            this.panelPeriod.Name = "panelPeriod";
            this.panelPeriod.Size = new System.Drawing.Size(180, 31);
            this.panelPeriod.TabIndex = 7;
            // 
            // labelPeriod
            // 
            this.labelPeriod.AutoSize = true;
            this.labelPeriod.Location = new System.Drawing.Point(3, 7);
            this.labelPeriod.Name = "labelPeriod";
            this.labelPeriod.Size = new System.Drawing.Size(45, 13);
            this.labelPeriod.TabIndex = 0;
            this.labelPeriod.Text = "Период";
            // 
            // radioButtonSecondPeriod
            // 
            this.radioButtonSecondPeriod.AutoSize = true;
            this.radioButtonSecondPeriod.Location = new System.Drawing.Point(119, 5);
            this.radioButtonSecondPeriod.Name = "radioButtonSecondPeriod";
            this.radioButtonSecondPeriod.Size = new System.Drawing.Size(61, 17);
            this.radioButtonSecondPeriod.TabIndex = 2;
            this.radioButtonSecondPeriod.Text = "Второй";
            this.radioButtonSecondPeriod.UseVisualStyleBackColor = true;
            // 
            // radioButtonFirstPeriod
            // 
            this.radioButtonFirstPeriod.AutoSize = true;
            this.radioButtonFirstPeriod.Checked = true;
            this.radioButtonFirstPeriod.Location = new System.Drawing.Point(54, 5);
            this.radioButtonFirstPeriod.Name = "radioButtonFirstPeriod";
            this.radioButtonFirstPeriod.Size = new System.Drawing.Size(65, 17);
            this.radioButtonFirstPeriod.TabIndex = 1;
            this.radioButtonFirstPeriod.TabStop = true;
            this.radioButtonFirstPeriod.Text = "Первый";
            this.radioButtonFirstPeriod.UseVisualStyleBackColor = true;
            // 
            // textBoxNotParseRecord
            // 
            this.textBoxNotParseRecord.Location = new System.Drawing.Point(120, 6);
            this.textBoxNotParseRecord.Multiline = true;
            this.textBoxNotParseRecord.Name = "textBoxNotParseRecord";
            this.textBoxNotParseRecord.ReadOnly = true;
            this.textBoxNotParseRecord.Size = new System.Drawing.Size(180, 20);
            this.textBoxNotParseRecord.TabIndex = 9;
            // 
            // labelNotParseRecord
            // 
            this.labelNotParseRecord.AutoSize = true;
            this.labelNotParseRecord.Location = new System.Drawing.Point(3, 9);
            this.labelNotParseRecord.Name = "labelNotParseRecord";
            this.labelNotParseRecord.Size = new System.Drawing.Size(46, 13);
            this.labelNotParseRecord.TabIndex = 8;
            this.labelNotParseRecord.Text = "Строка:";
            // 
            // panelSemester
            // 
            this.panelSemester.Controls.Add(this.labelSemester);
            this.panelSemester.Controls.Add(this.radioButtonSpring);
            this.panelSemester.Controls.Add(this.radioButtonAutumn);
            this.panelSemester.Location = new System.Drawing.Point(0, 35);
            this.panelSemester.Name = "panelSemester";
            this.panelSemester.Size = new System.Drawing.Size(300, 31);
            this.panelSemester.TabIndex = 0;
            // 
            // labelSemester
            // 
            this.labelSemester.AutoSize = true;
            this.labelSemester.Location = new System.Drawing.Point(3, 7);
            this.labelSemester.Name = "labelSemester";
            this.labelSemester.Size = new System.Drawing.Size(51, 13);
            this.labelSemester.TabIndex = 0;
            this.labelSemester.Text = "Семестр";
            // 
            // radioButtonSpring
            // 
            this.radioButtonSpring.AutoSize = true;
            this.radioButtonSpring.Location = new System.Drawing.Point(226, 5);
            this.radioButtonSpring.Name = "radioButtonSpring";
            this.radioButtonSpring.Size = new System.Drawing.Size(74, 17);
            this.radioButtonSpring.TabIndex = 2;
            this.radioButtonSpring.Text = "Весенний";
            this.radioButtonSpring.UseVisualStyleBackColor = true;
            // 
            // radioButtonAutumn
            // 
            this.radioButtonAutumn.AutoSize = true;
            this.radioButtonAutumn.Checked = true;
            this.radioButtonAutumn.Location = new System.Drawing.Point(120, 5);
            this.radioButtonAutumn.Name = "radioButtonAutumn";
            this.radioButtonAutumn.Size = new System.Drawing.Size(69, 17);
            this.radioButtonAutumn.TabIndex = 1;
            this.radioButtonAutumn.TabStop = true;
            this.radioButtonAutumn.Text = "Осенний";
            this.radioButtonAutumn.UseVisualStyleBackColor = true;
            // 
            // labelWeek
            // 
            this.labelWeek.AutoSize = true;
            this.labelWeek.Location = new System.Drawing.Point(3, 75);
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
            this.comboBoxWeek.Location = new System.Drawing.Point(120, 72);
            this.comboBoxWeek.Name = "comboBoxWeek";
            this.comboBoxWeek.Size = new System.Drawing.Size(45, 21);
            this.comboBoxWeek.TabIndex = 1;
            // 
            // comboBoxLessonType
            // 
            this.comboBoxLessonType.FormattingEnabled = true;
            this.comboBoxLessonType.Location = new System.Drawing.Point(387, 6);
            this.comboBoxLessonType.Name = "comboBoxLessonType";
            this.comboBoxLessonType.Size = new System.Drawing.Size(94, 21);
            this.comboBoxLessonType.TabIndex = 4;
            // 
            // labelDay
            // 
            this.labelDay.AutoSize = true;
            this.labelDay.Location = new System.Drawing.Point(202, 75);
            this.labelDay.Name = "labelDay";
            this.labelDay.Size = new System.Drawing.Size(34, 13);
            this.labelDay.TabIndex = 2;
            this.labelDay.Text = "День";
            // 
            // labelLesson
            // 
            this.labelLesson.AutoSize = true;
            this.labelLesson.Location = new System.Drawing.Point(306, 75);
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
            this.comboBoxLesson.Location = new System.Drawing.Point(345, 72);
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
            this.comboBoxDay.Location = new System.Drawing.Point(242, 72);
            this.comboBoxDay.Name = "comboBoxDay";
            this.comboBoxDay.Size = new System.Drawing.Size(45, 21);
            this.comboBoxDay.TabIndex = 3;
            // 
            // comboBoxDiscipline
            // 
            this.comboBoxDiscipline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiscipline.FormattingEnabled = true;
            this.comboBoxDiscipline.Location = new System.Drawing.Point(306, 32);
            this.comboBoxDiscipline.Name = "comboBoxDiscipline";
            this.comboBoxDiscipline.Size = new System.Drawing.Size(180, 21);
            this.comboBoxDiscipline.TabIndex = 5;
            this.comboBoxDiscipline.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDiscipline_SelectedIndexChanged);
            // 
            // comboBoxLecturer
            // 
            this.comboBoxLecturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLecturer.FormattingEnabled = true;
            this.comboBoxLecturer.Location = new System.Drawing.Point(306, 59);
            this.comboBoxLecturer.Name = "comboBoxLecturer";
            this.comboBoxLecturer.Size = new System.Drawing.Size(180, 21);
            this.comboBoxLecturer.TabIndex = 8;
            this.comboBoxLecturer.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLecturer_SelectedIndexChanged);
            // 
            // comboBoxClassroom
            // 
            this.comboBoxClassroom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClassroom.FormattingEnabled = true;
            this.comboBoxClassroom.Location = new System.Drawing.Point(306, 5);
            this.comboBoxClassroom.Name = "comboBoxClassroom";
            this.comboBoxClassroom.Size = new System.Drawing.Size(180, 21);
            this.comboBoxClassroom.TabIndex = 2;
            this.comboBoxClassroom.SelectedIndexChanged += new System.EventHandler(this.ComboBoxClassroom_SelectedIndexChanged);
            // 
            // comboBoxStudentGroup
            // 
            this.comboBoxStudentGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudentGroup.FormattingEnabled = true;
            this.comboBoxStudentGroup.Location = new System.Drawing.Point(306, 86);
            this.comboBoxStudentGroup.Name = "comboBoxStudentGroup";
            this.comboBoxStudentGroup.Size = new System.Drawing.Size(180, 21);
            this.comboBoxStudentGroup.TabIndex = 11;
            this.comboBoxStudentGroup.SelectedIndexChanged += new System.EventHandler(this.ComboBoxStudentGroup_SelectedIndexChanged);
            // 
            // panelTextData
            // 
            this.panelTextData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelTextData.Controls.Add(this.comboBoxDiscipline);
            this.panelTextData.Controls.Add(this.comboBoxLecturer);
            this.panelTextData.Controls.Add(this.textBoxLessonDiscipline);
            this.panelTextData.Controls.Add(this.comboBoxClassroom);
            this.panelTextData.Controls.Add(this.textBoxLessonLecturer);
            this.panelTextData.Controls.Add(this.comboBoxStudentGroup);
            this.panelTextData.Controls.Add(this.textBoxLessonStudentGroup);
            this.panelTextData.Controls.Add(this.textBoxLessonClassroom);
            this.panelTextData.Controls.Add(this.labelLessonDiscipline);
            this.panelTextData.Controls.Add(this.labelLessonLecturer);
            this.panelTextData.Controls.Add(this.labelLessonClassroom);
            this.panelTextData.Controls.Add(this.labelLessonStudentGroup);
            this.panelTextData.Location = new System.Drawing.Point(10, 10);
            this.panelTextData.Name = "panelTextData";
            this.panelTextData.Size = new System.Drawing.Size(500, 115);
            this.panelTextData.TabIndex = 5;
            // 
            // textBoxLessonDiscipline
            // 
            this.textBoxLessonDiscipline.Location = new System.Drawing.Point(120, 32);
            this.textBoxLessonDiscipline.Name = "textBoxLessonDiscipline";
            this.textBoxLessonDiscipline.Size = new System.Drawing.Size(180, 20);
            this.textBoxLessonDiscipline.TabIndex = 4;
            // 
            // textBoxLessonLecturer
            // 
            this.textBoxLessonLecturer.Location = new System.Drawing.Point(120, 59);
            this.textBoxLessonLecturer.Name = "textBoxLessonLecturer";
            this.textBoxLessonLecturer.Size = new System.Drawing.Size(180, 20);
            this.textBoxLessonLecturer.TabIndex = 7;
            // 
            // textBoxLessonStudentGroup
            // 
            this.textBoxLessonStudentGroup.Location = new System.Drawing.Point(120, 86);
            this.textBoxLessonStudentGroup.Name = "textBoxLessonStudentGroup";
            this.textBoxLessonStudentGroup.Size = new System.Drawing.Size(180, 20);
            this.textBoxLessonStudentGroup.TabIndex = 10;
            // 
            // textBoxLessonClassroom
            // 
            this.textBoxLessonClassroom.Location = new System.Drawing.Point(120, 5);
            this.textBoxLessonClassroom.Name = "textBoxLessonClassroom";
            this.textBoxLessonClassroom.Size = new System.Drawing.Size(180, 20);
            this.textBoxLessonClassroom.TabIndex = 1;
            // 
            // labelLessonDiscipline
            // 
            this.labelLessonDiscipline.AutoSize = true;
            this.labelLessonDiscipline.Location = new System.Drawing.Point(3, 35);
            this.labelLessonDiscipline.Name = "labelLessonDiscipline";
            this.labelLessonDiscipline.Size = new System.Drawing.Size(52, 13);
            this.labelLessonDiscipline.TabIndex = 3;
            this.labelLessonDiscipline.Text = "Предмет";
            // 
            // labelLessonLecturer
            // 
            this.labelLessonLecturer.AutoSize = true;
            this.labelLessonLecturer.Location = new System.Drawing.Point(3, 62);
            this.labelLessonLecturer.Name = "labelLessonLecturer";
            this.labelLessonLecturer.Size = new System.Drawing.Size(86, 13);
            this.labelLessonLecturer.TabIndex = 6;
            this.labelLessonLecturer.Text = "Преподаватель";
            // 
            // labelLessonClassroom
            // 
            this.labelLessonClassroom.AutoSize = true;
            this.labelLessonClassroom.Location = new System.Drawing.Point(3, 8);
            this.labelLessonClassroom.Name = "labelLessonClassroom";
            this.labelLessonClassroom.Size = new System.Drawing.Size(60, 13);
            this.labelLessonClassroom.TabIndex = 0;
            this.labelLessonClassroom.Text = "Аудитория";
            // 
            // labelLessonStudentGroup
            // 
            this.labelLessonStudentGroup.AutoSize = true;
            this.labelLessonStudentGroup.Location = new System.Drawing.Point(3, 89);
            this.labelLessonStudentGroup.Name = "labelLessonStudentGroup";
            this.labelLessonStudentGroup.Size = new System.Drawing.Size(42, 13);
            this.labelLessonStudentGroup.TabIndex = 9;
            this.labelLessonStudentGroup.Text = "Группа";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(432, 242);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(296, 242);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(130, 23);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Сохранить и закрыть";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ScheduleSemesterRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 276);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.panelDateTime);
            this.Controls.Add(this.panelTextData);
            this.Name = "ScheduleSemesterRecordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Запись семестра";
            this.Load += new System.EventHandler(this.ScheduleSemesterRecordForm_Load);
            this.panelDateTime.ResumeLayout(false);
            this.panelDateTime.PerformLayout();
            this.panelPeriod.ResumeLayout(false);
            this.panelPeriod.PerformLayout();
            this.panelSemester.ResumeLayout(false);
            this.panelSemester.PerformLayout();
            this.panelTextData.ResumeLayout(false);
            this.panelTextData.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.ComboBox comboBoxDiscipline;
        private System.Windows.Forms.ComboBox comboBoxLecturer;
        private System.Windows.Forms.ComboBox comboBoxClassroom;
        private System.Windows.Forms.ComboBox comboBoxStudentGroup;
        private System.Windows.Forms.Panel panelTextData;
        private System.Windows.Forms.TextBox textBoxLessonDiscipline;
        private System.Windows.Forms.TextBox textBoxLessonLecturer;
        private System.Windows.Forms.ComboBox comboBoxLessonType;
        private System.Windows.Forms.TextBox textBoxLessonStudentGroup;
        private System.Windows.Forms.TextBox textBoxLessonClassroom;
        private System.Windows.Forms.Label labelLessonClassroom;
        private System.Windows.Forms.Label labelLessonStudentGroup;
        private System.Windows.Forms.Label labelLessonLecturer;
        private System.Windows.Forms.Label labelLessonDiscipline;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxNotParseRecord;
        private System.Windows.Forms.Label labelNotParseRecord;
        private System.Windows.Forms.Panel panelSemester;
        private System.Windows.Forms.RadioButton radioButtonSpring;
        private System.Windows.Forms.RadioButton radioButtonAutumn;
        private System.Windows.Forms.Label labelSemester;
        private System.Windows.Forms.Panel panelPeriod;
        private System.Windows.Forms.Label labelPeriod;
        private System.Windows.Forms.RadioButton radioButtonSecondPeriod;
        private System.Windows.Forms.RadioButton radioButtonFirstPeriod;
    }
}