namespace ScheduleControlsAndForms.Consultation
{
    partial class ScheduleConsultationRecordForm
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
            this.comboBoxLecturer = new System.Windows.Forms.ComboBox();
            this.comboBoxClassroom = new System.Windows.Forms.ComboBox();
            this.textBoxLessonDiscipline = new System.Windows.Forms.TextBox();
            this.textBoxLessonLecturer = new System.Windows.Forms.TextBox();
            this.textBoxLessonStudentGroup = new System.Windows.Forms.TextBox();
            this.textBoxLessonClassroom = new System.Windows.Forms.TextBox();
            this.labelLessonClassroom = new System.Windows.Forms.Label();
            this.labelLessonStudentGroup = new System.Windows.Forms.Label();
            this.labelLessonLecturer = new System.Windows.Forms.Label();
            this.labelLessonDiscipline = new System.Windows.Forms.Label();
            this.labelDateConsultation = new System.Windows.Forms.Label();
            this.dateTimePickerDateConsultation = new System.Windows.Forms.DateTimePicker();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panelConsultation = new System.Windows.Forms.Panel();
            this.textBoxTimeSpan = new System.Windows.Forms.TextBox();
            this.labelTimeSpan = new System.Windows.Forms.Label();
            this.comboBoxDiscipline = new System.Windows.Forms.ComboBox();
            this.panelRecord = new System.Windows.Forms.Panel();
            this.comboBoxStudentGroup = new System.Windows.Forms.ComboBox();
            this.panelConsultation.SuspendLayout();
            this.panelRecord.SuspendLayout();
            this.SuspendLayout();
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
            // textBoxLessonDiscipline
            // 
            this.textBoxLessonDiscipline.Location = new System.Drawing.Point(120, 32);
            this.textBoxLessonDiscipline.Name = "textBoxLessonDiscipline";
            this.textBoxLessonDiscipline.Size = new System.Drawing.Size(180, 20);
            this.textBoxLessonDiscipline.TabIndex = 4;
            // 
            // textBoxLessonLecturer
            // 
            this.textBoxLessonLecturer.Location = new System.Drawing.Point(132, 71);
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
            // labelLessonLecturer
            // 
            this.labelLessonLecturer.AutoSize = true;
            this.labelLessonLecturer.Location = new System.Drawing.Point(15, 74);
            this.labelLessonLecturer.Name = "labelLessonLecturer";
            this.labelLessonLecturer.Size = new System.Drawing.Size(86, 13);
            this.labelLessonLecturer.TabIndex = 6;
            this.labelLessonLecturer.Text = "Преподаватель";
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
            // labelDateConsultation
            // 
            this.labelDateConsultation.AutoSize = true;
            this.labelDateConsultation.Location = new System.Drawing.Point(3, 6);
            this.labelDateConsultation.Name = "labelDateConsultation";
            this.labelDateConsultation.Size = new System.Drawing.Size(109, 13);
            this.labelDateConsultation.TabIndex = 0;
            this.labelDateConsultation.Text = "Дата консультации:";
            // 
            // dateTimePickerDateConsultation
            // 
            this.dateTimePickerDateConsultation.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dateTimePickerDateConsultation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDateConsultation.Location = new System.Drawing.Point(120, 3);
            this.dateTimePickerDateConsultation.Name = "dateTimePickerDateConsultation";
            this.dateTimePickerDateConsultation.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerDateConsultation.TabIndex = 1;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(432, 171);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(351, 171);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // panelConsultation
            // 
            this.panelConsultation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelConsultation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelConsultation.Controls.Add(this.textBoxTimeSpan);
            this.panelConsultation.Controls.Add(this.labelTimeSpan);
            this.panelConsultation.Controls.Add(this.labelDateConsultation);
            this.panelConsultation.Controls.Add(this.dateTimePickerDateConsultation);
            this.panelConsultation.Location = new System.Drawing.Point(10, 131);
            this.panelConsultation.Name = "panelConsultation";
            this.panelConsultation.Size = new System.Drawing.Size(500, 30);
            this.panelConsultation.TabIndex = 1;
            // 
            // textBoxTimeSpan
            // 
            this.textBoxTimeSpan.Location = new System.Drawing.Point(426, 3);
            this.textBoxTimeSpan.Name = "textBoxTimeSpan";
            this.textBoxTimeSpan.Size = new System.Drawing.Size(60, 20);
            this.textBoxTimeSpan.TabIndex = 3;
            // 
            // labelTimeSpan
            // 
            this.labelTimeSpan.AutoSize = true;
            this.labelTimeSpan.Location = new System.Drawing.Point(303, 6);
            this.labelTimeSpan.Name = "labelTimeSpan";
            this.labelTimeSpan.Size = new System.Drawing.Size(114, 13);
            this.labelTimeSpan.TabIndex = 2;
            this.labelTimeSpan.Text = "Продолжительность:";
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
            // panelRecord
            // 
            this.panelRecord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRecord.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelRecord.Controls.Add(this.comboBoxLecturer);
            this.panelRecord.Controls.Add(this.comboBoxStudentGroup);
            this.panelRecord.Controls.Add(this.textBoxLessonStudentGroup);
            this.panelRecord.Controls.Add(this.comboBoxDiscipline);
            this.panelRecord.Controls.Add(this.labelLessonStudentGroup);
            this.panelRecord.Controls.Add(this.comboBoxClassroom);
            this.panelRecord.Controls.Add(this.textBoxLessonDiscipline);
            this.panelRecord.Controls.Add(this.labelLessonDiscipline);
            this.panelRecord.Controls.Add(this.labelLessonClassroom);
            this.panelRecord.Controls.Add(this.textBoxLessonClassroom);
            this.panelRecord.Location = new System.Drawing.Point(10, 10);
            this.panelRecord.Name = "panelRecord";
            this.panelRecord.Size = new System.Drawing.Size(500, 115);
            this.panelRecord.TabIndex = 0;
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
            // ScheduleConsultationRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 206);
            this.Controls.Add(this.textBoxLessonLecturer);
            this.Controls.Add(this.labelLessonLecturer);
            this.Controls.Add(this.panelRecord);
            this.Controls.Add(this.panelConsultation);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Name = "ScheduleConsultationRecordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Консультация";
            this.Load += new System.EventHandler(this.ScheduleConsultationRecordForm_Load);
            this.panelConsultation.ResumeLayout(false);
            this.panelConsultation.PerformLayout();
            this.panelRecord.ResumeLayout(false);
            this.panelRecord.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxLecturer;
        private System.Windows.Forms.ComboBox comboBoxClassroom;
        private System.Windows.Forms.TextBox textBoxLessonDiscipline;
        private System.Windows.Forms.TextBox textBoxLessonLecturer;
        private System.Windows.Forms.TextBox textBoxLessonStudentGroup;
        private System.Windows.Forms.TextBox textBoxLessonClassroom;
        private System.Windows.Forms.Label labelLessonClassroom;
        private System.Windows.Forms.Label labelLessonStudentGroup;
        private System.Windows.Forms.Label labelLessonLecturer;
        private System.Windows.Forms.Label labelLessonDiscipline;
        private System.Windows.Forms.Label labelDateConsultation;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateConsultation;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panelConsultation;
        private System.Windows.Forms.Panel panelRecord;
        private System.Windows.Forms.ComboBox comboBoxStudentGroup;
        private System.Windows.Forms.ComboBox comboBoxDiscipline;
        private System.Windows.Forms.Label labelTimeSpan;
        private System.Windows.Forms.TextBox textBoxTimeSpan;
    }
}