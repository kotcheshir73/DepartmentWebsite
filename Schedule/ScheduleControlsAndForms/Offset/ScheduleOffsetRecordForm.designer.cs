namespace ScheduleControlsAndForms.Offset
{
    partial class ScheduleOffsetRecordForm
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
            this.panelDateTime = new System.Windows.Forms.Panel();
            this.labelDateOffset = new System.Windows.Forms.Label();
            this.labelLesson = new System.Windows.Forms.Label();
            this.comboBoxLesson = new System.Windows.Forms.ComboBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panelRecord = new System.Windows.Forms.Panel();
            this.comboBoxDiscipline = new System.Windows.Forms.ComboBox();
            this.comboBoxLecturer = new System.Windows.Forms.ComboBox();
            this.textBoxLessonDiscipline = new System.Windows.Forms.TextBox();
            this.comboBoxClassroom = new System.Windows.Forms.ComboBox();
            this.textBoxLessonLecturer = new System.Windows.Forms.TextBox();
            this.comboBoxStudentGroup = new System.Windows.Forms.ComboBox();
            this.textBoxLessonStudentGroup = new System.Windows.Forms.TextBox();
            this.textBoxLessonClassroom = new System.Windows.Forms.TextBox();
            this.labelLessonDiscipline = new System.Windows.Forms.Label();
            this.labelLessonLecturer = new System.Windows.Forms.Label();
            this.labelLessonClassroom = new System.Windows.Forms.Label();
            this.labelLessonStudentGroup = new System.Windows.Forms.Label();
            this.dateTimePickerDateOffset = new System.Windows.Forms.DateTimePicker();
            this.panelDateTime.SuspendLayout();
            this.panelRecord.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDateTime
            // 
            this.panelDateTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelDateTime.Controls.Add(this.dateTimePickerDateOffset);
            this.panelDateTime.Controls.Add(this.labelDateOffset);
            this.panelDateTime.Controls.Add(this.labelLesson);
            this.panelDateTime.Controls.Add(this.comboBoxLesson);
            this.panelDateTime.Location = new System.Drawing.Point(10, 131);
            this.panelDateTime.Name = "panelDateTime";
            this.panelDateTime.Size = new System.Drawing.Size(500, 38);
            this.panelDateTime.TabIndex = 1;
            // 
            // labelDateOffset
            // 
            this.labelDateOffset.AutoSize = true;
            this.labelDateOffset.Location = new System.Drawing.Point(9, 11);
            this.labelDateOffset.Name = "labelDateOffset";
            this.labelDateOffset.Size = new System.Drawing.Size(73, 13);
            this.labelDateOffset.TabIndex = 0;
            this.labelDateOffset.Text = "Дата зачета:";
            // 
            // labelLesson
            // 
            this.labelLesson.AutoSize = true;
            this.labelLesson.Location = new System.Drawing.Point(303, 11);
            this.labelLesson.Name = "labelLesson";
            this.labelLesson.Size = new System.Drawing.Size(33, 13);
            this.labelLesson.TabIndex = 2;
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
            this.comboBoxLesson.Location = new System.Drawing.Point(342, 8);
            this.comboBoxLesson.Name = "comboBoxLesson";
            this.comboBoxLesson.Size = new System.Drawing.Size(45, 21);
            this.comboBoxLesson.TabIndex = 3;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(435, 175);
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
            this.buttonSave.Location = new System.Drawing.Point(354, 175);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // panelRecord
            // 
            this.panelRecord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRecord.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelRecord.Controls.Add(this.comboBoxDiscipline);
            this.panelRecord.Controls.Add(this.comboBoxLecturer);
            this.panelRecord.Controls.Add(this.textBoxLessonDiscipline);
            this.panelRecord.Controls.Add(this.comboBoxClassroom);
            this.panelRecord.Controls.Add(this.textBoxLessonLecturer);
            this.panelRecord.Controls.Add(this.comboBoxStudentGroup);
            this.panelRecord.Controls.Add(this.textBoxLessonStudentGroup);
            this.panelRecord.Controls.Add(this.textBoxLessonClassroom);
            this.panelRecord.Controls.Add(this.labelLessonDiscipline);
            this.panelRecord.Controls.Add(this.labelLessonLecturer);
            this.panelRecord.Controls.Add(this.labelLessonClassroom);
            this.panelRecord.Controls.Add(this.labelLessonStudentGroup);
            this.panelRecord.Location = new System.Drawing.Point(10, 10);
            this.panelRecord.Name = "panelRecord";
            this.panelRecord.Size = new System.Drawing.Size(500, 115);
            this.panelRecord.TabIndex = 0;
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
            // textBoxLessonDiscipline
            // 
            this.textBoxLessonDiscipline.Location = new System.Drawing.Point(120, 32);
            this.textBoxLessonDiscipline.Name = "textBoxLessonDiscipline";
            this.textBoxLessonDiscipline.Size = new System.Drawing.Size(180, 20);
            this.textBoxLessonDiscipline.TabIndex = 4;
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
            // textBoxLessonLecturer
            // 
            this.textBoxLessonLecturer.Location = new System.Drawing.Point(120, 59);
            this.textBoxLessonLecturer.Name = "textBoxLessonLecturer";
            this.textBoxLessonLecturer.Size = new System.Drawing.Size(180, 20);
            this.textBoxLessonLecturer.TabIndex = 7;
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
            this.labelLessonDiscipline.Location = new System.Drawing.Point(2, 35);
            this.labelLessonDiscipline.Name = "labelLessonDiscipline";
            this.labelLessonDiscipline.Size = new System.Drawing.Size(52, 13);
            this.labelLessonDiscipline.TabIndex = 3;
            this.labelLessonDiscipline.Text = "Предмет";
            // 
            // labelLessonLecturer
            // 
            this.labelLessonLecturer.AutoSize = true;
            this.labelLessonLecturer.Location = new System.Drawing.Point(2, 62);
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
            this.labelLessonStudentGroup.Location = new System.Drawing.Point(2, 89);
            this.labelLessonStudentGroup.Name = "labelLessonStudentGroup";
            this.labelLessonStudentGroup.Size = new System.Drawing.Size(42, 13);
            this.labelLessonStudentGroup.TabIndex = 9;
            this.labelLessonStudentGroup.Text = "Группа";
            // 
            // dateTimePickerDateOffset
            // 
            this.dateTimePickerDateOffset.Location = new System.Drawing.Point(120, 8);
            this.dateTimePickerDateOffset.Name = "dateTimePickerDateOffset";
            this.dateTimePickerDateOffset.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerDateOffset.TabIndex = 1;
            // 
            // ScheduleOffsetRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 206);
            this.Controls.Add(this.panelRecord);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.panelDateTime);
            this.Name = "ScheduleOffsetRecordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Зачет";
            this.Load += new System.EventHandler(this.ScheduleOffsetRecordForm_Load);
            this.panelDateTime.ResumeLayout(false);
            this.panelDateTime.PerformLayout();
            this.panelRecord.ResumeLayout(false);
            this.panelRecord.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelDateTime;
        private System.Windows.Forms.Label labelDateOffset;
        private System.Windows.Forms.Label labelLesson;
        private System.Windows.Forms.ComboBox comboBoxLesson;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panelRecord;
        private System.Windows.Forms.ComboBox comboBoxDiscipline;
        private System.Windows.Forms.ComboBox comboBoxLecturer;
        private System.Windows.Forms.TextBox textBoxLessonDiscipline;
        private System.Windows.Forms.ComboBox comboBoxClassroom;
        private System.Windows.Forms.TextBox textBoxLessonLecturer;
        private System.Windows.Forms.ComboBox comboBoxStudentGroup;
        private System.Windows.Forms.TextBox textBoxLessonStudentGroup;
        private System.Windows.Forms.TextBox textBoxLessonClassroom;
        private System.Windows.Forms.Label labelLessonDiscipline;
        private System.Windows.Forms.Label labelLessonLecturer;
        private System.Windows.Forms.Label labelLessonClassroom;
        private System.Windows.Forms.Label labelLessonStudentGroup;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateOffset;
    }
}