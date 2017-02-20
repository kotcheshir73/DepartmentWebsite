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
            this.labelClassroom = new System.Windows.Forms.Label();
            this.comboBoxWeek = new System.Windows.Forms.ComboBox();
            this.labelWeek = new System.Windows.Forms.Label();
            this.comboBoxDay = new System.Windows.Forms.ComboBox();
            this.comboBoxLesson = new System.Windows.Forms.ComboBox();
            this.labelLesson = new System.Windows.Forms.Label();
            this.labelDay = new System.Windows.Forms.Label();
            this.labelLessonType = new System.Windows.Forms.Label();
            this.labelLessonGroup = new System.Windows.Forms.Label();
            this.labelLessonLecturer = new System.Windows.Forms.Label();
            this.labelLessonDiscipline = new System.Windows.Forms.Label();
            this.textBoxLessonDiscipline = new System.Windows.Forms.TextBox();
            this.textBoxLessonLecturer = new System.Windows.Forms.TextBox();
            this.textBoxLessonGroup = new System.Windows.Forms.TextBox();
            this.textBoxClassroom = new System.Windows.Forms.TextBox();
            this.comboBoxLessonType = new System.Windows.Forms.ComboBox();
            this.checkBoxApplyToAnalogRecordsByDisipline = new System.Windows.Forms.CheckBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboBoxLecturer = new System.Windows.Forms.ComboBox();
            this.comboBoxGroup = new System.Windows.Forms.ComboBox();
            this.comboBoxClassroom = new System.Windows.Forms.ComboBox();
            this.radioButtonApplyToTextData = new System.Windows.Forms.RadioButton();
            this.radioButtonApplyToBaseData = new System.Windows.Forms.RadioButton();
            this.panelTextData = new System.Windows.Forms.Panel();
            this.panelBaseData = new System.Windows.Forms.Panel();
            this.panelDateTime = new System.Windows.Forms.Panel();
            this.panelApply = new System.Windows.Forms.Panel();
            this.checkBoxApplyToAnalogRecordsByLessonType = new System.Windows.Forms.CheckBox();
            this.checkBoxApplyToAnalogRecordsByClassroom = new System.Windows.Forms.CheckBox();
            this.checkBoxApplyToAnalogRecordsByGroup = new System.Windows.Forms.CheckBox();
            this.checkBoxApplyToAnalogRecordsByLecturer = new System.Windows.Forms.CheckBox();
            this.panelTextData.SuspendLayout();
            this.panelBaseData.SuspendLayout();
            this.panelDateTime.SuspendLayout();
            this.panelApply.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelClassroom
            // 
            this.labelClassroom.AutoSize = true;
            this.labelClassroom.Location = new System.Drawing.Point(12, 137);
            this.labelClassroom.Name = "labelClassroom";
            this.labelClassroom.Size = new System.Drawing.Size(60, 13);
            this.labelClassroom.TabIndex = 5;
            this.labelClassroom.Text = "Аудитория";
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
            this.comboBoxWeek.TabIndex = 11;
            // 
            // labelWeek
            // 
            this.labelWeek.AutoSize = true;
            this.labelWeek.Location = new System.Drawing.Point(9, 11);
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
            this.comboBoxDay.Location = new System.Drawing.Point(167, 8);
            this.comboBoxDay.Name = "comboBoxDay";
            this.comboBoxDay.Size = new System.Drawing.Size(45, 21);
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
            this.comboBoxLesson.Location = new System.Drawing.Point(283, 8);
            this.comboBoxLesson.Name = "comboBoxLesson";
            this.comboBoxLesson.Size = new System.Drawing.Size(45, 21);
            this.comboBoxLesson.TabIndex = 15;
            // 
            // labelLesson
            // 
            this.labelLesson.AutoSize = true;
            this.labelLesson.Location = new System.Drawing.Point(244, 11);
            this.labelLesson.Name = "labelLesson";
            this.labelLesson.Size = new System.Drawing.Size(33, 13);
            this.labelLesson.TabIndex = 14;
            this.labelLesson.Text = "Пара";
            // 
            // labelDay
            // 
            this.labelDay.AutoSize = true;
            this.labelDay.Location = new System.Drawing.Point(127, 11);
            this.labelDay.Name = "labelDay";
            this.labelDay.Size = new System.Drawing.Size(34, 13);
            this.labelDay.TabIndex = 12;
            this.labelDay.Text = "День";
            // 
            // labelLessonType
            // 
            this.labelLessonType.AutoSize = true;
            this.labelLessonType.Location = new System.Drawing.Point(12, 163);
            this.labelLessonType.Name = "labelLessonType";
            this.labelLessonType.Size = new System.Drawing.Size(70, 13);
            this.labelLessonType.TabIndex = 6;
            this.labelLessonType.Text = "Тип занятия";
            // 
            // labelLessonGroup
            // 
            this.labelLessonGroup.AutoSize = true;
            this.labelLessonGroup.Location = new System.Drawing.Point(12, 111);
            this.labelLessonGroup.Name = "labelLessonGroup";
            this.labelLessonGroup.Size = new System.Drawing.Size(42, 13);
            this.labelLessonGroup.TabIndex = 4;
            this.labelLessonGroup.Text = "Группа";
            // 
            // labelLessonLecturer
            // 
            this.labelLessonLecturer.AutoSize = true;
            this.labelLessonLecturer.Location = new System.Drawing.Point(12, 85);
            this.labelLessonLecturer.Name = "labelLessonLecturer";
            this.labelLessonLecturer.Size = new System.Drawing.Size(86, 13);
            this.labelLessonLecturer.TabIndex = 3;
            this.labelLessonLecturer.Text = "Преподаватель";
            // 
            // labelLessonDiscipline
            // 
            this.labelLessonDiscipline.AutoSize = true;
            this.labelLessonDiscipline.Location = new System.Drawing.Point(12, 59);
            this.labelLessonDiscipline.Name = "labelLessonDiscipline";
            this.labelLessonDiscipline.Size = new System.Drawing.Size(52, 13);
            this.labelLessonDiscipline.TabIndex = 2;
            this.labelLessonDiscipline.Text = "Предмет";
            // 
            // textBoxLessonDiscipline
            // 
            this.textBoxLessonDiscipline.Location = new System.Drawing.Point(3, 6);
            this.textBoxLessonDiscipline.Name = "textBoxLessonDiscipline";
            this.textBoxLessonDiscipline.Size = new System.Drawing.Size(180, 20);
            this.textBoxLessonDiscipline.TabIndex = 1;
            // 
            // textBoxLessonLecturer
            // 
            this.textBoxLessonLecturer.Location = new System.Drawing.Point(3, 32);
            this.textBoxLessonLecturer.Name = "textBoxLessonLecturer";
            this.textBoxLessonLecturer.Size = new System.Drawing.Size(180, 20);
            this.textBoxLessonLecturer.TabIndex = 2;
            // 
            // textBoxLessonGroup
            // 
            this.textBoxLessonGroup.Location = new System.Drawing.Point(3, 58);
            this.textBoxLessonGroup.Name = "textBoxLessonGroup";
            this.textBoxLessonGroup.Size = new System.Drawing.Size(180, 20);
            this.textBoxLessonGroup.TabIndex = 3;
            // 
            // textBoxClassroom
            // 
            this.textBoxClassroom.Location = new System.Drawing.Point(3, 84);
            this.textBoxClassroom.Name = "textBoxClassroom";
            this.textBoxClassroom.Size = new System.Drawing.Size(180, 20);
            this.textBoxClassroom.TabIndex = 4;
            // 
            // comboBoxLessonType
            // 
            this.comboBoxLessonType.FormattingEnabled = true;
            this.comboBoxLessonType.Location = new System.Drawing.Point(3, 110);
            this.comboBoxLessonType.Name = "comboBoxLessonType";
            this.comboBoxLessonType.Size = new System.Drawing.Size(94, 21);
            this.comboBoxLessonType.TabIndex = 9;
            // 
            // checkBoxApplyToAnalogRecordsByDisipline
            // 
            this.checkBoxApplyToAnalogRecordsByDisipline.AutoSize = true;
            this.checkBoxApplyToAnalogRecordsByDisipline.Location = new System.Drawing.Point(12, 7);
            this.checkBoxApplyToAnalogRecordsByDisipline.Name = "checkBoxApplyToAnalogRecordsByDisipline";
            this.checkBoxApplyToAnalogRecordsByDisipline.Size = new System.Drawing.Size(296, 17);
            this.checkBoxApplyToAnalogRecordsByDisipline.TabIndex = 0;
            this.checkBoxApplyToAnalogRecordsByDisipline.Text = "Применить к аналогичным записям по дисциплинам";
            this.checkBoxApplyToAnalogRecordsByDisipline.UseVisualStyleBackColor = true;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(415, 366);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 12;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(334, 366);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 11;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboBoxLecturer
            // 
            this.comboBoxLecturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLecturer.FormattingEnabled = true;
            this.comboBoxLecturer.Location = new System.Drawing.Point(3, 34);
            this.comboBoxLecturer.Name = "comboBoxLecturer";
            this.comboBoxLecturer.Size = new System.Drawing.Size(180, 21);
            this.comboBoxLecturer.TabIndex = 2;
            this.comboBoxLecturer.SelectedIndexChanged += new System.EventHandler(this.comboBoxLecturer_SelectedIndexChanged);
            // 
            // comboBoxGroup
            // 
            this.comboBoxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGroup.FormattingEnabled = true;
            this.comboBoxGroup.Location = new System.Drawing.Point(3, 60);
            this.comboBoxGroup.Name = "comboBoxGroup";
            this.comboBoxGroup.Size = new System.Drawing.Size(180, 21);
            this.comboBoxGroup.TabIndex = 3;
            this.comboBoxGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxGroup_SelectedIndexChanged);
            // 
            // comboBoxClassroom
            // 
            this.comboBoxClassroom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClassroom.FormattingEnabled = true;
            this.comboBoxClassroom.Location = new System.Drawing.Point(3, 86);
            this.comboBoxClassroom.Name = "comboBoxClassroom";
            this.comboBoxClassroom.Size = new System.Drawing.Size(180, 21);
            this.comboBoxClassroom.TabIndex = 4;
            this.comboBoxClassroom.SelectedIndexChanged += new System.EventHandler(this.comboBoxClassroom_SelectedIndexChanged);
            // 
            // radioButtonApplyToTextData
            // 
            this.radioButtonApplyToTextData.AutoSize = true;
            this.radioButtonApplyToTextData.Checked = true;
            this.radioButtonApplyToTextData.Location = new System.Drawing.Point(104, 12);
            this.radioButtonApplyToTextData.Name = "radioButtonApplyToTextData";
            this.radioButtonApplyToTextData.Size = new System.Drawing.Size(153, 30);
            this.radioButtonApplyToTextData.TabIndex = 0;
            this.radioButtonApplyToTextData.TabStop = true;
            this.radioButtonApplyToTextData.Text = "Применить изменений к \r\nтекстовым данным";
            this.radioButtonApplyToTextData.UseVisualStyleBackColor = true;
            // 
            // radioButtonApplyToBaseData
            // 
            this.radioButtonApplyToBaseData.AutoSize = true;
            this.radioButtonApplyToBaseData.Location = new System.Drawing.Point(301, 12);
            this.radioButtonApplyToBaseData.Name = "radioButtonApplyToBaseData";
            this.radioButtonApplyToBaseData.Size = new System.Drawing.Size(153, 30);
            this.radioButtonApplyToBaseData.TabIndex = 1;
            this.radioButtonApplyToBaseData.Text = "Применить изменений к \r\nбазовым данным";
            this.radioButtonApplyToBaseData.UseVisualStyleBackColor = true;
            // 
            // panelTextData
            // 
            this.panelTextData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelTextData.Controls.Add(this.textBoxLessonDiscipline);
            this.panelTextData.Controls.Add(this.textBoxLessonLecturer);
            this.panelTextData.Controls.Add(this.comboBoxLessonType);
            this.panelTextData.Controls.Add(this.textBoxLessonGroup);
            this.panelTextData.Controls.Add(this.textBoxClassroom);
            this.panelTextData.Location = new System.Drawing.Point(104, 48);
            this.panelTextData.Name = "panelTextData";
            this.panelTextData.Size = new System.Drawing.Size(190, 136);
            this.panelTextData.TabIndex = 7;
            // 
            // panelBaseData
            // 
            this.panelBaseData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelBaseData.Controls.Add(this.comboBoxLecturer);
            this.panelBaseData.Controls.Add(this.comboBoxClassroom);
            this.panelBaseData.Controls.Add(this.comboBoxGroup);
            this.panelBaseData.Location = new System.Drawing.Point(300, 48);
            this.panelBaseData.Name = "panelBaseData";
            this.panelBaseData.Size = new System.Drawing.Size(190, 136);
            this.panelBaseData.TabIndex = 8;
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
            this.panelDateTime.Location = new System.Drawing.Point(104, 190);
            this.panelDateTime.Name = "panelDateTime";
            this.panelDateTime.Size = new System.Drawing.Size(386, 38);
            this.panelDateTime.TabIndex = 9;
            // 
            // panelApply
            // 
            this.panelApply.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelApply.Controls.Add(this.checkBoxApplyToAnalogRecordsByLessonType);
            this.panelApply.Controls.Add(this.checkBoxApplyToAnalogRecordsByClassroom);
            this.panelApply.Controls.Add(this.checkBoxApplyToAnalogRecordsByGroup);
            this.panelApply.Controls.Add(this.checkBoxApplyToAnalogRecordsByLecturer);
            this.panelApply.Controls.Add(this.checkBoxApplyToAnalogRecordsByDisipline);
            this.panelApply.Location = new System.Drawing.Point(104, 234);
            this.panelApply.Name = "panelApply";
            this.panelApply.Size = new System.Drawing.Size(386, 126);
            this.panelApply.TabIndex = 10;
            // 
            // checkBoxApplyToAnalogRecordsByLessonType
            // 
            this.checkBoxApplyToAnalogRecordsByLessonType.AutoSize = true;
            this.checkBoxApplyToAnalogRecordsByLessonType.Location = new System.Drawing.Point(12, 99);
            this.checkBoxApplyToAnalogRecordsByLessonType.Name = "checkBoxApplyToAnalogRecordsByLessonType";
            this.checkBoxApplyToAnalogRecordsByLessonType.Size = new System.Drawing.Size(303, 17);
            this.checkBoxApplyToAnalogRecordsByLessonType.TabIndex = 4;
            this.checkBoxApplyToAnalogRecordsByLessonType.Text = "Применить к аналогичным записям по типам занятий";
            this.checkBoxApplyToAnalogRecordsByLessonType.UseVisualStyleBackColor = true;
            // 
            // checkBoxApplyToAnalogRecordsByClassroom
            // 
            this.checkBoxApplyToAnalogRecordsByClassroom.AutoSize = true;
            this.checkBoxApplyToAnalogRecordsByClassroom.Location = new System.Drawing.Point(12, 76);
            this.checkBoxApplyToAnalogRecordsByClassroom.Name = "checkBoxApplyToAnalogRecordsByClassroom";
            this.checkBoxApplyToAnalogRecordsByClassroom.Size = new System.Drawing.Size(288, 17);
            this.checkBoxApplyToAnalogRecordsByClassroom.TabIndex = 3;
            this.checkBoxApplyToAnalogRecordsByClassroom.Text = "Применить к аналогичным записям по аудиториям";
            this.checkBoxApplyToAnalogRecordsByClassroom.UseVisualStyleBackColor = true;
            // 
            // checkBoxApplyToAnalogRecordsByGroup
            // 
            this.checkBoxApplyToAnalogRecordsByGroup.AutoSize = true;
            this.checkBoxApplyToAnalogRecordsByGroup.Location = new System.Drawing.Point(12, 53);
            this.checkBoxApplyToAnalogRecordsByGroup.Name = "checkBoxApplyToAnalogRecordsByGroup";
            this.checkBoxApplyToAnalogRecordsByGroup.Size = new System.Drawing.Size(270, 17);
            this.checkBoxApplyToAnalogRecordsByGroup.TabIndex = 2;
            this.checkBoxApplyToAnalogRecordsByGroup.Text = "Применить к аналогичным записям по группам";
            this.checkBoxApplyToAnalogRecordsByGroup.UseVisualStyleBackColor = true;
            // 
            // checkBoxApplyToAnalogRecordsByLecturer
            // 
            this.checkBoxApplyToAnalogRecordsByLecturer.AutoSize = true;
            this.checkBoxApplyToAnalogRecordsByLecturer.Location = new System.Drawing.Point(12, 30);
            this.checkBoxApplyToAnalogRecordsByLecturer.Name = "checkBoxApplyToAnalogRecordsByLecturer";
            this.checkBoxApplyToAnalogRecordsByLecturer.Size = new System.Drawing.Size(313, 17);
            this.checkBoxApplyToAnalogRecordsByLecturer.TabIndex = 1;
            this.checkBoxApplyToAnalogRecordsByLecturer.Text = "Применить к аналогичным записям по преподавателям";
            this.checkBoxApplyToAnalogRecordsByLecturer.UseVisualStyleBackColor = true;
            // 
            // ScheduleSemesterRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 392);
            this.Controls.Add(this.panelApply);
            this.Controls.Add(this.labelLessonType);
            this.Controls.Add(this.panelDateTime);
            this.Controls.Add(this.radioButtonApplyToBaseData);
            this.Controls.Add(this.panelBaseData);
            this.Controls.Add(this.panelTextData);
            this.Controls.Add(this.radioButtonApplyToTextData);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelClassroom);
            this.Controls.Add(this.labelLessonGroup);
            this.Controls.Add(this.labelLessonLecturer);
            this.Controls.Add(this.labelLessonDiscipline);
            this.Name = "ScheduleSemesterRecordForm";
            this.Text = "Запись семестра";
            this.Load += new System.EventHandler(this.ScheduleSemesterRecordForm_Load);
            this.panelTextData.ResumeLayout(false);
            this.panelTextData.PerformLayout();
            this.panelBaseData.ResumeLayout(false);
            this.panelDateTime.ResumeLayout(false);
            this.panelDateTime.PerformLayout();
            this.panelApply.ResumeLayout(false);
            this.panelApply.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelClassroom;
        private System.Windows.Forms.ComboBox comboBoxWeek;
        private System.Windows.Forms.Label labelWeek;
        private System.Windows.Forms.ComboBox comboBoxDay;
        private System.Windows.Forms.ComboBox comboBoxLesson;
        private System.Windows.Forms.Label labelLesson;
        private System.Windows.Forms.Label labelDay;
        private System.Windows.Forms.Label labelLessonType;
        private System.Windows.Forms.Label labelLessonGroup;
        private System.Windows.Forms.Label labelLessonLecturer;
        private System.Windows.Forms.Label labelLessonDiscipline;
        private System.Windows.Forms.TextBox textBoxLessonDiscipline;
        private System.Windows.Forms.TextBox textBoxLessonLecturer;
        private System.Windows.Forms.TextBox textBoxLessonGroup;
        private System.Windows.Forms.TextBox textBoxClassroom;
        private System.Windows.Forms.ComboBox comboBoxLessonType;
        private System.Windows.Forms.CheckBox checkBoxApplyToAnalogRecordsByDisipline;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ComboBox comboBoxLecturer;
        private System.Windows.Forms.ComboBox comboBoxGroup;
        private System.Windows.Forms.ComboBox comboBoxClassroom;
        private System.Windows.Forms.RadioButton radioButtonApplyToTextData;
        private System.Windows.Forms.RadioButton radioButtonApplyToBaseData;
        private System.Windows.Forms.Panel panelTextData;
        private System.Windows.Forms.Panel panelBaseData;
        private System.Windows.Forms.Panel panelDateTime;
        private System.Windows.Forms.Panel panelApply;
        private System.Windows.Forms.CheckBox checkBoxApplyToAnalogRecordsByGroup;
        private System.Windows.Forms.CheckBox checkBoxApplyToAnalogRecordsByLecturer;
        private System.Windows.Forms.CheckBox checkBoxApplyToAnalogRecordsByClassroom;
        private System.Windows.Forms.CheckBox checkBoxApplyToAnalogRecordsByLessonType;
    }
}