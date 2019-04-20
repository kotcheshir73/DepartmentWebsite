namespace LearningProgressControlsAndForms.DisciplineLessonTaskStudentAccept
{
    partial class FormDisciplineLessonTaskStudentAccept
    {
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
            this.comboBoxDisciplineLesson = new System.Windows.Forms.ComboBox();
            this.labelDisciplineLesson = new System.Windows.Forms.Label();
            this.comboBoxDisciplineLessonTask = new System.Windows.Forms.ComboBox();
            this.labelDisciplineLessonTask = new System.Windows.Forms.Label();
            this.comboBoxStudent = new System.Windows.Forms.ComboBox();
            this.labelStudent = new System.Windows.Forms.Label();
            this.comboBoxResult = new System.Windows.Forms.ComboBox();
            this.labelResult = new System.Windows.Forms.Label();
            this.labelDateAccept = new System.Windows.Forms.Label();
            this.dateTimePickerDateAccept = new System.Windows.Forms.DateTimePicker();
            this.labelScore = new System.Windows.Forms.Label();
            this.textBoxScore = new System.Windows.Forms.TextBox();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.labelComment = new System.Windows.Forms.Label();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.textBoxTask = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(102, 192);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(249, 192);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(21, 192);
            // 
            // comboBoxDisciplineLesson
            // 
            this.comboBoxDisciplineLesson.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisciplineLesson.Enabled = false;
            this.comboBoxDisciplineLesson.FormattingEnabled = true;
            this.comboBoxDisciplineLesson.Location = new System.Drawing.Point(117, 6);
            this.comboBoxDisciplineLesson.Name = "comboBoxDisciplineLesson";
            this.comboBoxDisciplineLesson.Size = new System.Drawing.Size(210, 21);
            this.comboBoxDisciplineLesson.TabIndex = 1;
            // 
            // labelDisciplineLesson
            // 
            this.labelDisciplineLesson.AutoSize = true;
            this.labelDisciplineLesson.Location = new System.Drawing.Point(12, 9);
            this.labelDisciplineLesson.Name = "labelDisciplineLesson";
            this.labelDisciplineLesson.Size = new System.Drawing.Size(56, 13);
            this.labelDisciplineLesson.TabIndex = 0;
            this.labelDisciplineLesson.Text = "Занятие*:";
            // 
            // comboBoxDisciplineLessonTask
            // 
            this.comboBoxDisciplineLessonTask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisciplineLessonTask.FormattingEnabled = true;
            this.comboBoxDisciplineLessonTask.Location = new System.Drawing.Point(117, 33);
            this.comboBoxDisciplineLessonTask.Name = "comboBoxDisciplineLessonTask";
            this.comboBoxDisciplineLessonTask.Size = new System.Drawing.Size(210, 21);
            this.comboBoxDisciplineLessonTask.TabIndex = 3;
            // 
            // labelDisciplineLessonTask
            // 
            this.labelDisciplineLessonTask.AutoSize = true;
            this.labelDisciplineLessonTask.Location = new System.Drawing.Point(12, 36);
            this.labelDisciplineLessonTask.Name = "labelDisciplineLessonTask";
            this.labelDisciplineLessonTask.Size = new System.Drawing.Size(57, 13);
            this.labelDisciplineLessonTask.TabIndex = 2;
            this.labelDisciplineLessonTask.Text = "Задание*:";
            // 
            // comboBoxStudent
            // 
            this.comboBoxStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudent.FormattingEnabled = true;
            this.comboBoxStudent.Location = new System.Drawing.Point(117, 60);
            this.comboBoxStudent.Name = "comboBoxStudent";
            this.comboBoxStudent.Size = new System.Drawing.Size(210, 21);
            this.comboBoxStudent.TabIndex = 5;
            // 
            // labelStudent
            // 
            this.labelStudent.AutoSize = true;
            this.labelStudent.Location = new System.Drawing.Point(12, 63);
            this.labelStudent.Name = "labelStudent";
            this.labelStudent.Size = new System.Drawing.Size(54, 13);
            this.labelStudent.TabIndex = 4;
            this.labelStudent.Text = "Студент*:";
            // 
            // comboBoxResult
            // 
            this.comboBoxResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxResult.FormattingEnabled = true;
            this.comboBoxResult.Location = new System.Drawing.Point(117, 87);
            this.comboBoxResult.Name = "comboBoxResult";
            this.comboBoxResult.Size = new System.Drawing.Size(210, 21);
            this.comboBoxResult.TabIndex = 7;
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Location = new System.Drawing.Point(12, 90);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(66, 13);
            this.labelResult.TabIndex = 6;
            this.labelResult.Text = "Результат*:";
            // 
            // labelDateAccept
            // 
            this.labelDateAccept.AutoSize = true;
            this.labelDateAccept.Location = new System.Drawing.Point(12, 118);
            this.labelDateAccept.Name = "labelDateAccept";
            this.labelDateAccept.Size = new System.Drawing.Size(68, 13);
            this.labelDateAccept.TabIndex = 8;
            this.labelDateAccept.Text = "Дата сдачи:";
            // 
            // dateTimePickerDateAccept
            // 
            this.dateTimePickerDateAccept.Location = new System.Drawing.Point(117, 114);
            this.dateTimePickerDateAccept.Name = "dateTimePickerDateAccept";
            this.dateTimePickerDateAccept.Size = new System.Drawing.Size(210, 20);
            this.dateTimePickerDateAccept.TabIndex = 9;
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.Location = new System.Drawing.Point(12, 143);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(99, 13);
            this.labelScore.TabIndex = 10;
            this.labelScore.Text = "Полученный балл:";
            // 
            // textBoxScore
            // 
            this.textBoxScore.Location = new System.Drawing.Point(117, 140);
            this.textBoxScore.Name = "textBoxScore";
            this.textBoxScore.Size = new System.Drawing.Size(210, 20);
            this.textBoxScore.TabIndex = 11;
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(117, 166);
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(210, 20);
            this.textBoxComment.TabIndex = 13;
            // 
            // labelComment
            // 
            this.labelComment.AutoSize = true;
            this.labelComment.Location = new System.Drawing.Point(12, 169);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(80, 13);
            this.labelComment.TabIndex = 12;
            this.labelComment.Text = "Комментарий:";
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Enabled = false;
            this.richTextBoxLog.Location = new System.Drawing.Point(333, 114);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(270, 72);
            this.richTextBoxLog.TabIndex = 18;
            this.richTextBoxLog.Text = "";
            // 
            // textBoxTask
            // 
            this.textBoxTask.Location = new System.Drawing.Point(333, 6);
            this.textBoxTask.Multiline = true;
            this.textBoxTask.Name = "textBoxTask";
            this.textBoxTask.Size = new System.Drawing.Size(270, 102);
            this.textBoxTask.TabIndex = 19;
            // 
            // DisciplineLessonTaskStudentAcceptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 221);
            this.Controls.Add(this.textBoxTask);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.textBoxComment);
            this.Controls.Add(this.labelComment);
            this.Controls.Add(this.textBoxScore);
            this.Controls.Add(this.labelScore);
            this.Controls.Add(this.dateTimePickerDateAccept);
            this.Controls.Add(this.labelDateAccept);
            this.Controls.Add(this.comboBoxResult);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.comboBoxStudent);
            this.Controls.Add(this.labelStudent);
            this.Controls.Add(this.comboBoxDisciplineLessonTask);
            this.Controls.Add(this.labelDisciplineLessonTask);
            this.Controls.Add(this.comboBoxDisciplineLesson);
            this.Controls.Add(this.labelDisciplineLesson);
            this.Name = "DisciplineLessonTaskStudentAcceptForm";
            this.Text = "Успеваемость студента";
            this.Load += new System.EventHandler(this.FormDisciplineLessonTaskStudentAccept_Load);
            this.Controls.SetChildIndex(this.labelDisciplineLesson, 0);
            this.Controls.SetChildIndex(this.comboBoxDisciplineLesson, 0);
            this.Controls.SetChildIndex(this.labelDisciplineLessonTask, 0);
            this.Controls.SetChildIndex(this.comboBoxDisciplineLessonTask, 0);
            this.Controls.SetChildIndex(this.labelStudent, 0);
            this.Controls.SetChildIndex(this.comboBoxStudent, 0);
            this.Controls.SetChildIndex(this.labelResult, 0);
            this.Controls.SetChildIndex(this.comboBoxResult, 0);
            this.Controls.SetChildIndex(this.labelDateAccept, 0);
            this.Controls.SetChildIndex(this.dateTimePickerDateAccept, 0);
            this.Controls.SetChildIndex(this.labelScore, 0);
            this.Controls.SetChildIndex(this.textBoxScore, 0);
            this.Controls.SetChildIndex(this.labelComment, 0);
            this.Controls.SetChildIndex(this.textBoxComment, 0);
            this.Controls.SetChildIndex(this.richTextBoxLog, 0);
            this.Controls.SetChildIndex(this.textBoxTask, 0);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.buttonSaveAndClose, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxDisciplineLesson;
        private System.Windows.Forms.Label labelDisciplineLesson;
        private System.Windows.Forms.ComboBox comboBoxDisciplineLessonTask;
        private System.Windows.Forms.Label labelDisciplineLessonTask;
        private System.Windows.Forms.ComboBox comboBoxStudent;
        private System.Windows.Forms.Label labelStudent;
        private System.Windows.Forms.ComboBox comboBoxResult;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Label labelDateAccept;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateAccept;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.TextBox textBoxScore;
        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.Label labelComment;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.TextBox textBoxTask;
    }
}