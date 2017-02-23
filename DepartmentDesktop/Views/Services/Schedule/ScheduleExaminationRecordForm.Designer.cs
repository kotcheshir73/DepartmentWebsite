namespace DepartmentDesktop.Views.Services.Schedule
{
    partial class ScheduleExaminationRecordForm
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
            this.panelBaseData = new System.Windows.Forms.Panel();
            this.comboBoxLecturer = new System.Windows.Forms.ComboBox();
            this.comboBoxClassroom = new System.Windows.Forms.ComboBox();
            this.comboBoxStudentGroup = new System.Windows.Forms.ComboBox();
            this.panelTextData = new System.Windows.Forms.Panel();
            this.textBoxLessonDiscipline = new System.Windows.Forms.TextBox();
            this.textBoxLessonLecturer = new System.Windows.Forms.TextBox();
            this.textBoxLessonGroup = new System.Windows.Forms.TextBox();
            this.textBoxClassroom = new System.Windows.Forms.TextBox();
            this.labelClassroom = new System.Windows.Forms.Label();
            this.labelLessonGroup = new System.Windows.Forms.Label();
            this.labelLessonLecturer = new System.Windows.Forms.Label();
            this.labelLessonDiscipline = new System.Windows.Forms.Label();
            this.panelDateTime = new System.Windows.Forms.Panel();
            this.labelDateConsultation = new System.Windows.Forms.Label();
            this.dateTimePickerDateConsultation = new System.Windows.Forms.DateTimePicker();
            this.labelDateExamination = new System.Windows.Forms.Label();
            this.dateTimePickerDateExamination = new System.Windows.Forms.DateTimePicker();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panelBaseData.SuspendLayout();
            this.panelTextData.SuspendLayout();
            this.panelDateTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBaseData
            // 
            this.panelBaseData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelBaseData.Controls.Add(this.comboBoxLecturer);
            this.panelBaseData.Controls.Add(this.comboBoxClassroom);
            this.panelBaseData.Controls.Add(this.comboBoxStudentGroup);
            this.panelBaseData.Location = new System.Drawing.Point(300, 12);
            this.panelBaseData.Name = "panelBaseData";
            this.panelBaseData.Size = new System.Drawing.Size(190, 120);
            this.panelBaseData.TabIndex = 5;
            // 
            // comboBoxLecturer
            // 
            this.comboBoxLecturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLecturer.FormattingEnabled = true;
            this.comboBoxLecturer.Location = new System.Drawing.Point(3, 34);
            this.comboBoxLecturer.Name = "comboBoxLecturer";
            this.comboBoxLecturer.Size = new System.Drawing.Size(180, 21);
            this.comboBoxLecturer.TabIndex = 1;
            this.comboBoxLecturer.SelectedIndexChanged += new System.EventHandler(this.comboBoxLecturer_SelectedIndexChanged);
            // 
            // comboBoxClassroom
            // 
            this.comboBoxClassroom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClassroom.FormattingEnabled = true;
            this.comboBoxClassroom.Location = new System.Drawing.Point(3, 86);
            this.comboBoxClassroom.Name = "comboBoxClassroom";
            this.comboBoxClassroom.Size = new System.Drawing.Size(180, 21);
            this.comboBoxClassroom.TabIndex = 3;
            this.comboBoxClassroom.SelectedIndexChanged += new System.EventHandler(this.comboBoxClassroom_SelectedIndexChanged);
            // 
            // comboBoxStudentGroup
            // 
            this.comboBoxStudentGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudentGroup.FormattingEnabled = true;
            this.comboBoxStudentGroup.Location = new System.Drawing.Point(3, 60);
            this.comboBoxStudentGroup.Name = "comboBoxStudentGroup";
            this.comboBoxStudentGroup.Size = new System.Drawing.Size(180, 21);
            this.comboBoxStudentGroup.TabIndex = 2;
            this.comboBoxStudentGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxStudentGroup_SelectedIndexChanged);
            // 
            // panelTextData
            // 
            this.panelTextData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelTextData.Controls.Add(this.textBoxLessonDiscipline);
            this.panelTextData.Controls.Add(this.textBoxLessonLecturer);
            this.panelTextData.Controls.Add(this.textBoxLessonGroup);
            this.panelTextData.Controls.Add(this.textBoxClassroom);
            this.panelTextData.Location = new System.Drawing.Point(104, 12);
            this.panelTextData.Name = "panelTextData";
            this.panelTextData.Size = new System.Drawing.Size(190, 120);
            this.panelTextData.TabIndex = 4;
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
            this.labelClassroom.Location = new System.Drawing.Point(12, 101);
            this.labelClassroom.Name = "labelClassroom";
            this.labelClassroom.Size = new System.Drawing.Size(60, 13);
            this.labelClassroom.TabIndex = 3;
            this.labelClassroom.Text = "Аудитория";
            // 
            // labelLessonGroup
            // 
            this.labelLessonGroup.AutoSize = true;
            this.labelLessonGroup.Location = new System.Drawing.Point(12, 75);
            this.labelLessonGroup.Name = "labelLessonGroup";
            this.labelLessonGroup.Size = new System.Drawing.Size(42, 13);
            this.labelLessonGroup.TabIndex = 2;
            this.labelLessonGroup.Text = "Группа";
            // 
            // labelLessonLecturer
            // 
            this.labelLessonLecturer.AutoSize = true;
            this.labelLessonLecturer.Location = new System.Drawing.Point(12, 49);
            this.labelLessonLecturer.Name = "labelLessonLecturer";
            this.labelLessonLecturer.Size = new System.Drawing.Size(86, 13);
            this.labelLessonLecturer.TabIndex = 1;
            this.labelLessonLecturer.Text = "Преподаватель";
            // 
            // labelLessonDiscipline
            // 
            this.labelLessonDiscipline.AutoSize = true;
            this.labelLessonDiscipline.Location = new System.Drawing.Point(12, 23);
            this.labelLessonDiscipline.Name = "labelLessonDiscipline";
            this.labelLessonDiscipline.Size = new System.Drawing.Size(52, 13);
            this.labelLessonDiscipline.TabIndex = 0;
            this.labelLessonDiscipline.Text = "Предмет";
            // 
            // panelDateTime
            // 
            this.panelDateTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelDateTime.Controls.Add(this.labelDateExamination);
            this.panelDateTime.Controls.Add(this.dateTimePickerDateExamination);
            this.panelDateTime.Controls.Add(this.labelDateConsultation);
            this.panelDateTime.Controls.Add(this.dateTimePickerDateConsultation);
            this.panelDateTime.Location = new System.Drawing.Point(104, 138);
            this.panelDateTime.Name = "panelDateTime";
            this.panelDateTime.Size = new System.Drawing.Size(386, 62);
            this.panelDateTime.TabIndex = 7;
            // 
            // labelDateConsultation
            // 
            this.labelDateConsultation.AutoSize = true;
            this.labelDateConsultation.Location = new System.Drawing.Point(9, 10);
            this.labelDateConsultation.Name = "labelDateConsultation";
            this.labelDateConsultation.Size = new System.Drawing.Size(109, 13);
            this.labelDateConsultation.TabIndex = 0;
            this.labelDateConsultation.Text = "Дата консультации:";
            // 
            // dateTimePickerDateConsultation
            // 
            this.dateTimePickerDateConsultation.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dateTimePickerDateConsultation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDateConsultation.Location = new System.Drawing.Point(138, 6);
            this.dateTimePickerDateConsultation.Name = "dateTimePickerDateConsultation";
            this.dateTimePickerDateConsultation.Size = new System.Drawing.Size(148, 20);
            this.dateTimePickerDateConsultation.TabIndex = 1;
            // 
            // labelDateExamination
            // 
            this.labelDateExamination.AutoSize = true;
            this.labelDateExamination.Location = new System.Drawing.Point(9, 36);
            this.labelDateExamination.Name = "labelDateExamination";
            this.labelDateExamination.Size = new System.Drawing.Size(89, 13);
            this.labelDateExamination.TabIndex = 2;
            this.labelDateExamination.Text = "Дата экзамена:";
            // 
            // dateTimePickerDateExamination
            // 
            this.dateTimePickerDateExamination.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dateTimePickerDateExamination.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDateExamination.Location = new System.Drawing.Point(138, 32);
            this.dateTimePickerDateExamination.Name = "dateTimePickerDateExamination";
            this.dateTimePickerDateExamination.Size = new System.Drawing.Size(148, 20);
            this.dateTimePickerDateExamination.TabIndex = 3;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(415, 206);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 8;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(334, 206);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // ScheduleExaminationRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 232);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.panelDateTime);
            this.Controls.Add(this.panelBaseData);
            this.Controls.Add(this.panelTextData);
            this.Controls.Add(this.labelClassroom);
            this.Controls.Add(this.labelLessonGroup);
            this.Controls.Add(this.labelLessonLecturer);
            this.Controls.Add(this.labelLessonDiscipline);
            this.Name = "ScheduleExaminationRecordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Экзамен";
            this.Load += new System.EventHandler(this.ScheduleExaminationRecordForm_Load);
            this.panelBaseData.ResumeLayout(false);
            this.panelTextData.ResumeLayout(false);
            this.panelTextData.PerformLayout();
            this.panelDateTime.ResumeLayout(false);
            this.panelDateTime.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelBaseData;
        private System.Windows.Forms.ComboBox comboBoxLecturer;
        private System.Windows.Forms.ComboBox comboBoxClassroom;
        private System.Windows.Forms.ComboBox comboBoxStudentGroup;
        private System.Windows.Forms.Panel panelTextData;
        private System.Windows.Forms.TextBox textBoxLessonDiscipline;
        private System.Windows.Forms.TextBox textBoxLessonLecturer;
        private System.Windows.Forms.TextBox textBoxLessonGroup;
        private System.Windows.Forms.TextBox textBoxClassroom;
        private System.Windows.Forms.Label labelClassroom;
        private System.Windows.Forms.Label labelLessonGroup;
        private System.Windows.Forms.Label labelLessonLecturer;
        private System.Windows.Forms.Label labelLessonDiscipline;
        private System.Windows.Forms.Panel panelDateTime;
        private System.Windows.Forms.Label labelDateConsultation;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateConsultation;
        private System.Windows.Forms.Label labelDateExamination;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateExamination;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
    }
}