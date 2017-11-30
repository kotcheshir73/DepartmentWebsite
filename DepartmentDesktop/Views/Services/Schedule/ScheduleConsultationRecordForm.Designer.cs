namespace DepartmentDesktop.Views.Services.Schedule
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
            this.textBoxLessonGroup = new System.Windows.Forms.TextBox();
            this.textBoxClassroom = new System.Windows.Forms.TextBox();
            this.labelClassroom = new System.Windows.Forms.Label();
            this.labelLessonGroup = new System.Windows.Forms.Label();
            this.labelLessonLecturer = new System.Windows.Forms.Label();
            this.labelLessonDiscipline = new System.Windows.Forms.Label();
            this.labelDateConsultation = new System.Windows.Forms.Label();
            this.dateTimePickerDateConsultation = new System.Windows.Forms.DateTimePicker();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panelDateTime = new System.Windows.Forms.Panel();
            this.panelDiscipline = new System.Windows.Forms.Panel();
            this.panelLecturer = new System.Windows.Forms.Panel();
            this.panelClassroom = new System.Windows.Forms.Panel();
            this.panelStudentGroup = new System.Windows.Forms.Panel();
            this.comboBoxStudentGroup = new System.Windows.Forms.ComboBox();
            this.comboBoxDiscipline = new System.Windows.Forms.ComboBox();
            this.panelDateTime.SuspendLayout();
            this.panelDiscipline.SuspendLayout();
            this.panelLecturer.SuspendLayout();
            this.panelClassroom.SuspendLayout();
            this.panelStudentGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxLecturer
            // 
            this.comboBoxLecturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLecturer.FormattingEnabled = true;
            this.comboBoxLecturer.Location = new System.Drawing.Point(292, 5);
            this.comboBoxLecturer.Name = "comboBoxLecturer";
            this.comboBoxLecturer.Size = new System.Drawing.Size(180, 21);
            this.comboBoxLecturer.TabIndex = 1;
            this.comboBoxLecturer.SelectedIndexChanged += new System.EventHandler(this.comboBoxLecturer_SelectedIndexChanged);
            // 
            // comboBoxClassroom
            // 
            this.comboBoxClassroom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClassroom.FormattingEnabled = true;
            this.comboBoxClassroom.Location = new System.Drawing.Point(292, 5);
            this.comboBoxClassroom.Name = "comboBoxClassroom";
            this.comboBoxClassroom.Size = new System.Drawing.Size(180, 21);
            this.comboBoxClassroom.TabIndex = 3;
            this.comboBoxClassroom.SelectedIndexChanged += new System.EventHandler(this.comboBoxClassroom_SelectedIndexChanged);
            // 
            // textBoxLessonDiscipline
            // 
            this.textBoxLessonDiscipline.Location = new System.Drawing.Point(102, 5);
            this.textBoxLessonDiscipline.Name = "textBoxLessonDiscipline";
            this.textBoxLessonDiscipline.Size = new System.Drawing.Size(180, 20);
            this.textBoxLessonDiscipline.TabIndex = 0;
            // 
            // textBoxLessonLecturer
            // 
            this.textBoxLessonLecturer.Location = new System.Drawing.Point(102, 5);
            this.textBoxLessonLecturer.Name = "textBoxLessonLecturer";
            this.textBoxLessonLecturer.Size = new System.Drawing.Size(180, 20);
            this.textBoxLessonLecturer.TabIndex = 1;
            // 
            // textBoxLessonGroup
            // 
            this.textBoxLessonGroup.Location = new System.Drawing.Point(102, 5);
            this.textBoxLessonGroup.Name = "textBoxLessonGroup";
            this.textBoxLessonGroup.Size = new System.Drawing.Size(180, 20);
            this.textBoxLessonGroup.TabIndex = 2;
            // 
            // textBoxClassroom
            // 
            this.textBoxClassroom.Location = new System.Drawing.Point(102, 5);
            this.textBoxClassroom.Name = "textBoxClassroom";
            this.textBoxClassroom.Size = new System.Drawing.Size(180, 20);
            this.textBoxClassroom.TabIndex = 3;
            // 
            // labelClassroom
            // 
            this.labelClassroom.AutoSize = true;
            this.labelClassroom.Location = new System.Drawing.Point(3, 8);
            this.labelClassroom.Name = "labelClassroom";
            this.labelClassroom.Size = new System.Drawing.Size(60, 13);
            this.labelClassroom.TabIndex = 3;
            this.labelClassroom.Text = "Аудитория";
            // 
            // labelLessonGroup
            // 
            this.labelLessonGroup.AutoSize = true;
            this.labelLessonGroup.Location = new System.Drawing.Point(3, 8);
            this.labelLessonGroup.Name = "labelLessonGroup";
            this.labelLessonGroup.Size = new System.Drawing.Size(42, 13);
            this.labelLessonGroup.TabIndex = 2;
            this.labelLessonGroup.Text = "Группа";
            // 
            // labelLessonLecturer
            // 
            this.labelLessonLecturer.AutoSize = true;
            this.labelLessonLecturer.Location = new System.Drawing.Point(3, 8);
            this.labelLessonLecturer.Name = "labelLessonLecturer";
            this.labelLessonLecturer.Size = new System.Drawing.Size(86, 13);
            this.labelLessonLecturer.TabIndex = 1;
            this.labelLessonLecturer.Text = "Преподаватель";
            // 
            // labelLessonDiscipline
            // 
            this.labelLessonDiscipline.AutoSize = true;
            this.labelLessonDiscipline.Location = new System.Drawing.Point(3, 8);
            this.labelLessonDiscipline.Name = "labelLessonDiscipline";
            this.labelLessonDiscipline.Size = new System.Drawing.Size(52, 13);
            this.labelLessonDiscipline.TabIndex = 0;
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
            this.dateTimePickerDateConsultation.Location = new System.Drawing.Point(118, 3);
            this.dateTimePickerDateConsultation.Name = "dateTimePickerDateConsultation";
            this.dateTimePickerDateConsultation.Size = new System.Drawing.Size(148, 20);
            this.dateTimePickerDateConsultation.TabIndex = 1;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(417, 192);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 8;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(336, 192);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // panelDateTime
            // 
            this.panelDateTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelDateTime.Controls.Add(this.labelDateConsultation);
            this.panelDateTime.Controls.Add(this.dateTimePickerDateConsultation);
            this.panelDateTime.Location = new System.Drawing.Point(12, 12);
            this.panelDateTime.Name = "panelDateTime";
            this.panelDateTime.Size = new System.Drawing.Size(480, 30);
            this.panelDateTime.TabIndex = 0;
            // 
            // panelDiscipline
            // 
            this.panelDiscipline.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelDiscipline.Controls.Add(this.comboBoxDiscipline);
            this.panelDiscipline.Controls.Add(this.textBoxLessonDiscipline);
            this.panelDiscipline.Controls.Add(this.labelLessonDiscipline);
            this.panelDiscipline.Location = new System.Drawing.Point(12, 120);
            this.panelDiscipline.Name = "panelDiscipline";
            this.panelDiscipline.Size = new System.Drawing.Size(480, 30);
            this.panelDiscipline.TabIndex = 3;
            // 
            // panelLecturer
            // 
            this.panelLecturer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelLecturer.Controls.Add(this.comboBoxLecturer);
            this.panelLecturer.Controls.Add(this.textBoxLessonLecturer);
            this.panelLecturer.Controls.Add(this.labelLessonLecturer);
            this.panelLecturer.Location = new System.Drawing.Point(12, 84);
            this.panelLecturer.Name = "panelLecturer";
            this.panelLecturer.Size = new System.Drawing.Size(480, 30);
            this.panelLecturer.TabIndex = 2;
            // 
            // panelClassroom
            // 
            this.panelClassroom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelClassroom.Controls.Add(this.comboBoxClassroom);
            this.panelClassroom.Controls.Add(this.labelClassroom);
            this.panelClassroom.Controls.Add(this.textBoxClassroom);
            this.panelClassroom.Location = new System.Drawing.Point(12, 48);
            this.panelClassroom.Name = "panelClassroom";
            this.panelClassroom.Size = new System.Drawing.Size(480, 30);
            this.panelClassroom.TabIndex = 1;
            // 
            // panelStudentGroup
            // 
            this.panelStudentGroup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelStudentGroup.Controls.Add(this.comboBoxStudentGroup);
            this.panelStudentGroup.Controls.Add(this.textBoxLessonGroup);
            this.panelStudentGroup.Controls.Add(this.labelLessonGroup);
            this.panelStudentGroup.Location = new System.Drawing.Point(12, 156);
            this.panelStudentGroup.Name = "panelStudentGroup";
            this.panelStudentGroup.Size = new System.Drawing.Size(480, 30);
            this.panelStudentGroup.TabIndex = 4;
            // 
            // comboBoxStudentGroup
            // 
            this.comboBoxStudentGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudentGroup.FormattingEnabled = true;
            this.comboBoxStudentGroup.Location = new System.Drawing.Point(292, 5);
            this.comboBoxStudentGroup.Name = "comboBoxStudentGroup";
            this.comboBoxStudentGroup.Size = new System.Drawing.Size(180, 21);
            this.comboBoxStudentGroup.TabIndex = 3;
            this.comboBoxStudentGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxStudentGroup_SelectedIndexChanged);
            // 
            // comboBoxDiscipline
            // 
            this.comboBoxDiscipline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiscipline.FormattingEnabled = true;
            this.comboBoxDiscipline.Location = new System.Drawing.Point(292, 5);
            this.comboBoxDiscipline.Name = "comboBoxDiscipline";
            this.comboBoxDiscipline.Size = new System.Drawing.Size(180, 21);
            this.comboBoxDiscipline.TabIndex = 1;
            this.comboBoxDiscipline.SelectedIndexChanged += new System.EventHandler(this.comboBoxDiscipline_SelectedIndexChanged);
            // 
            // ScheduleConsultationRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 227);
            this.Controls.Add(this.panelStudentGroup);
            this.Controls.Add(this.panelClassroom);
            this.Controls.Add(this.panelLecturer);
            this.Controls.Add(this.panelDiscipline);
            this.Controls.Add(this.panelDateTime);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Name = "ScheduleConsultationRecordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Консультация";
            this.Load += new System.EventHandler(this.ScheduleConsultationRecordForm_Load);
            this.panelDateTime.ResumeLayout(false);
            this.panelDateTime.PerformLayout();
            this.panelDiscipline.ResumeLayout(false);
            this.panelDiscipline.PerformLayout();
            this.panelLecturer.ResumeLayout(false);
            this.panelLecturer.PerformLayout();
            this.panelClassroom.ResumeLayout(false);
            this.panelClassroom.PerformLayout();
            this.panelStudentGroup.ResumeLayout(false);
            this.panelStudentGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxLecturer;
        private System.Windows.Forms.ComboBox comboBoxClassroom;
        private System.Windows.Forms.TextBox textBoxLessonDiscipline;
        private System.Windows.Forms.TextBox textBoxLessonLecturer;
        private System.Windows.Forms.TextBox textBoxLessonGroup;
        private System.Windows.Forms.TextBox textBoxClassroom;
        private System.Windows.Forms.Label labelClassroom;
        private System.Windows.Forms.Label labelLessonGroup;
        private System.Windows.Forms.Label labelLessonLecturer;
        private System.Windows.Forms.Label labelLessonDiscipline;
        private System.Windows.Forms.Label labelDateConsultation;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateConsultation;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panelDateTime;
        private System.Windows.Forms.Panel panelDiscipline;
        private System.Windows.Forms.Panel panelLecturer;
        private System.Windows.Forms.Panel panelClassroom;
        private System.Windows.Forms.Panel panelStudentGroup;
        private System.Windows.Forms.ComboBox comboBoxStudentGroup;
        private System.Windows.Forms.ComboBox comboBoxDiscipline;
    }
}