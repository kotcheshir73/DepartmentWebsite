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
            this.radioButtonApplyToBaseData = new System.Windows.Forms.RadioButton();
            this.panelBaseData = new System.Windows.Forms.Panel();
            this.comboBoxLecturer = new System.Windows.Forms.ComboBox();
            this.comboBoxClassroom = new System.Windows.Forms.ComboBox();
            this.comboBoxGroup = new System.Windows.Forms.ComboBox();
            this.panelTextData = new System.Windows.Forms.Panel();
            this.textBoxLessonDiscipline = new System.Windows.Forms.TextBox();
            this.textBoxLessonLecturer = new System.Windows.Forms.TextBox();
            this.textBoxLessonGroup = new System.Windows.Forms.TextBox();
            this.textBoxClassroom = new System.Windows.Forms.TextBox();
            this.radioButtonApplyToTextData = new System.Windows.Forms.RadioButton();
            this.labelClassroom = new System.Windows.Forms.Label();
            this.labelLessonGroup = new System.Windows.Forms.Label();
            this.labelLessonLecturer = new System.Windows.Forms.Label();
            this.labelLessonDiscipline = new System.Windows.Forms.Label();
            this.labelDateConsultation = new System.Windows.Forms.Label();
            this.dateTimePickerDateConsultation = new System.Windows.Forms.DateTimePicker();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panelBaseData.SuspendLayout();
            this.panelTextData.SuspendLayout();
            this.SuspendLayout();
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
            // panelBaseData
            // 
            this.panelBaseData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelBaseData.Controls.Add(this.comboBoxLecturer);
            this.panelBaseData.Controls.Add(this.comboBoxClassroom);
            this.panelBaseData.Controls.Add(this.comboBoxGroup);
            this.panelBaseData.Location = new System.Drawing.Point(300, 48);
            this.panelBaseData.Name = "panelBaseData";
            this.panelBaseData.Size = new System.Drawing.Size(190, 115);
            this.panelBaseData.TabIndex = 7;
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
            // comboBoxGroup
            // 
            this.comboBoxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGroup.FormattingEnabled = true;
            this.comboBoxGroup.Location = new System.Drawing.Point(3, 60);
            this.comboBoxGroup.Name = "comboBoxGroup";
            this.comboBoxGroup.Size = new System.Drawing.Size(180, 21);
            this.comboBoxGroup.TabIndex = 2;
            this.comboBoxGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxGroup_SelectedIndexChanged);
            // 
            // panelTextData
            // 
            this.panelTextData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelTextData.Controls.Add(this.textBoxLessonDiscipline);
            this.panelTextData.Controls.Add(this.textBoxLessonLecturer);
            this.panelTextData.Controls.Add(this.textBoxLessonGroup);
            this.panelTextData.Controls.Add(this.textBoxClassroom);
            this.panelTextData.Location = new System.Drawing.Point(104, 48);
            this.panelTextData.Name = "panelTextData";
            this.panelTextData.Size = new System.Drawing.Size(190, 115);
            this.panelTextData.TabIndex = 6;
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
            // labelClassroom
            // 
            this.labelClassroom.AutoSize = true;
            this.labelClassroom.Location = new System.Drawing.Point(12, 137);
            this.labelClassroom.Name = "labelClassroom";
            this.labelClassroom.Size = new System.Drawing.Size(60, 13);
            this.labelClassroom.TabIndex = 5;
            this.labelClassroom.Text = "Аудитория";
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
            // labelDateConsultation
            // 
            this.labelDateConsultation.AutoSize = true;
            this.labelDateConsultation.Location = new System.Drawing.Point(12, 175);
            this.labelDateConsultation.Name = "labelDateConsultation";
            this.labelDateConsultation.Size = new System.Drawing.Size(109, 13);
            this.labelDateConsultation.TabIndex = 8;
            this.labelDateConsultation.Text = "Дата консультации:";
            // 
            // dateTimePickerDateConsultation
            // 
            this.dateTimePickerDateConsultation.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dateTimePickerDateConsultation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDateConsultation.Location = new System.Drawing.Point(141, 169);
            this.dateTimePickerDateConsultation.Name = "dateTimePickerDateConsultation";
            this.dateTimePickerDateConsultation.Size = new System.Drawing.Size(148, 20);
            this.dateTimePickerDateConsultation.TabIndex = 9;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(415, 189);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 14;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(334, 189);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 13;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // ScheduleConsultationRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 224);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.dateTimePickerDateConsultation);
            this.Controls.Add(this.labelDateConsultation);
            this.Controls.Add(this.radioButtonApplyToBaseData);
            this.Controls.Add(this.panelBaseData);
            this.Controls.Add(this.panelTextData);
            this.Controls.Add(this.radioButtonApplyToTextData);
            this.Controls.Add(this.labelClassroom);
            this.Controls.Add(this.labelLessonGroup);
            this.Controls.Add(this.labelLessonLecturer);
            this.Controls.Add(this.labelLessonDiscipline);
            this.Name = "ScheduleConsultationRecordForm";
            this.Text = "Консультация";
            this.Load += new System.EventHandler(this.ScheduleConsultationRecordForm_Load);
            this.panelBaseData.ResumeLayout(false);
            this.panelTextData.ResumeLayout(false);
            this.panelTextData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RadioButton radioButtonApplyToBaseData;
        private System.Windows.Forms.Panel panelBaseData;
        private System.Windows.Forms.ComboBox comboBoxLecturer;
        private System.Windows.Forms.ComboBox comboBoxClassroom;
        private System.Windows.Forms.ComboBox comboBoxGroup;
        private System.Windows.Forms.Panel panelTextData;
        private System.Windows.Forms.TextBox textBoxLessonDiscipline;
        private System.Windows.Forms.TextBox textBoxLessonLecturer;
        private System.Windows.Forms.TextBox textBoxLessonGroup;
        private System.Windows.Forms.TextBox textBoxClassroom;
        private System.Windows.Forms.RadioButton radioButtonApplyToTextData;
        private System.Windows.Forms.Label labelClassroom;
        private System.Windows.Forms.Label labelLessonGroup;
        private System.Windows.Forms.Label labelLessonLecturer;
        private System.Windows.Forms.Label labelLessonDiscipline;
        private System.Windows.Forms.Label labelDateConsultation;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateConsultation;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
    }
}