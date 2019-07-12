namespace LearningProgressControlsAndForms.DisciplineLessonConductedStudent
{
    partial class FormDisciplineLessonConductedStudent
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
            this.labelDisciplineLesson = new System.Windows.Forms.Label();
            this.comboBoxDisciplineLesson = new System.Windows.Forms.ComboBox();
            this.labelStudent = new System.Windows.Forms.Label();
            this.comboBoxStudent = new System.Windows.Forms.ComboBox();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.checkBoxBall = new System.Windows.Forms.CheckBox();
            this.textBoxBall = new System.Windows.Forms.TextBox();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.labelComment = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.textBoxComment);
            this.panelMain.Controls.Add(this.labelDisciplineLesson);
            this.panelMain.Controls.Add(this.labelComment);
            this.panelMain.Controls.Add(this.comboBoxDisciplineLesson);
            this.panelMain.Controls.Add(this.checkBoxBall);
            this.panelMain.Controls.Add(this.labelStudent);
            this.panelMain.Controls.Add(this.textBoxBall);
            this.panelMain.Controls.Add(this.comboBoxStudent);
            this.panelMain.Controls.Add(this.comboBoxStatus);
            this.panelMain.Controls.Add(this.labelStatus);
            this.panelMain.Size = new System.Drawing.Size(364, 185);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(364, 36);
            // 
            // labelDisciplineLesson
            // 
            this.labelDisciplineLesson.AutoSize = true;
            this.labelDisciplineLesson.Location = new System.Drawing.Point(12, 15);
            this.labelDisciplineLesson.Name = "labelDisciplineLesson";
            this.labelDisciplineLesson.Size = new System.Drawing.Size(56, 13);
            this.labelDisciplineLesson.TabIndex = 0;
            this.labelDisciplineLesson.Text = "Занятие*:";
            // 
            // comboBoxDisciplineLesson
            // 
            this.comboBoxDisciplineLesson.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisciplineLesson.FormattingEnabled = true;
            this.comboBoxDisciplineLesson.Location = new System.Drawing.Point(131, 12);
            this.comboBoxDisciplineLesson.Name = "comboBoxDisciplineLesson";
            this.comboBoxDisciplineLesson.Size = new System.Drawing.Size(210, 21);
            this.comboBoxDisciplineLesson.TabIndex = 1;
            // 
            // labelStudent
            // 
            this.labelStudent.AutoSize = true;
            this.labelStudent.Location = new System.Drawing.Point(12, 42);
            this.labelStudent.Name = "labelStudent";
            this.labelStudent.Size = new System.Drawing.Size(54, 13);
            this.labelStudent.TabIndex = 2;
            this.labelStudent.Text = "Студент*:";
            // 
            // comboBoxStudent
            // 
            this.comboBoxStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudent.FormattingEnabled = true;
            this.comboBoxStudent.Location = new System.Drawing.Point(131, 39);
            this.comboBoxStudent.Name = "comboBoxStudent";
            this.comboBoxStudent.Size = new System.Drawing.Size(210, 21);
            this.comboBoxStudent.TabIndex = 3;
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(131, 66);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(210, 21);
            this.comboBoxStatus.TabIndex = 5;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(12, 69);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(48, 13);
            this.labelStatus.TabIndex = 4;
            this.labelStatus.Text = "Статус*:";
            // 
            // checkBoxBall
            // 
            this.checkBoxBall.AutoSize = true;
            this.checkBoxBall.Location = new System.Drawing.Point(15, 96);
            this.checkBoxBall.Name = "checkBoxBall";
            this.checkBoxBall.Size = new System.Drawing.Size(54, 17);
            this.checkBoxBall.TabIndex = 6;
            this.checkBoxBall.Text = "Балл:";
            this.checkBoxBall.UseVisualStyleBackColor = true;
            this.checkBoxBall.CheckedChanged += new System.EventHandler(this.CheckBoxBall_CheckedChanged);
            // 
            // textBoxBall
            // 
            this.textBoxBall.Enabled = false;
            this.textBoxBall.Location = new System.Drawing.Point(131, 93);
            this.textBoxBall.Name = "textBoxBall";
            this.textBoxBall.Size = new System.Drawing.Size(72, 20);
            this.textBoxBall.TabIndex = 7;
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(131, 119);
            this.textBoxComment.Multiline = true;
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(210, 54);
            this.textBoxComment.TabIndex = 9;
            // 
            // labelComment
            // 
            this.labelComment.AutoSize = true;
            this.labelComment.Location = new System.Drawing.Point(12, 122);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(80, 13);
            this.labelComment.TabIndex = 8;
            this.labelComment.Text = "Комментарий:";
            // 
            // FormDisciplineLessonConductedStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 221);
            this.Name = "FormDisciplineLessonConductedStudent";
            this.Text = "Отметка о посещении студента";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelDisciplineLesson;
        private System.Windows.Forms.ComboBox comboBoxDisciplineLesson;
        private System.Windows.Forms.Label labelStudent;
        private System.Windows.Forms.ComboBox comboBoxStudent;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.CheckBox checkBoxBall;
        private System.Windows.Forms.TextBox textBoxBall;
        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.Label labelComment;
    }
}