namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson.DisciplineLessonTask
{
    partial class DuplicateDisciplineLessonTaskForm
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
            this.labelSelectDLT = new System.Windows.Forms.Label();
            this.comboBoxDisciplineLessonTask = new System.Windows.Forms.ComboBox();
            this.buttonDuplicate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelSelectDLT
            // 
            this.labelSelectDLT.AutoSize = true;
            this.labelSelectDLT.Location = new System.Drawing.Point(12, 9);
            this.labelSelectDLT.Name = "labelSelectDLT";
            this.labelSelectDLT.Size = new System.Drawing.Size(148, 13);
            this.labelSelectDLT.TabIndex = 0;
            this.labelSelectDLT.Text = "Выбрать задание-дубликат:";
            // 
            // comboBoxDisciplineLessonTask
            // 
            this.comboBoxDisciplineLessonTask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisciplineLessonTask.FormattingEnabled = true;
            this.comboBoxDisciplineLessonTask.Location = new System.Drawing.Point(166, 6);
            this.comboBoxDisciplineLessonTask.Name = "comboBoxDisciplineLessonTask";
            this.comboBoxDisciplineLessonTask.Size = new System.Drawing.Size(210, 21);
            this.comboBoxDisciplineLessonTask.TabIndex = 1;
            // 
            // buttonDuplicate
            // 
            this.buttonDuplicate.Location = new System.Drawing.Point(115, 42);
            this.buttonDuplicate.Name = "buttonDuplicate";
            this.buttonDuplicate.Size = new System.Drawing.Size(141, 23);
            this.buttonDuplicate.TabIndex = 2;
            this.buttonDuplicate.Text = "Дублировать";
            this.buttonDuplicate.UseVisualStyleBackColor = true;
            this.buttonDuplicate.Click += new System.EventHandler(this.buttonDuplicate_Click);
            // 
            // DuplicateDisciplineLessonTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 81);
            this.Controls.Add(this.buttonDuplicate);
            this.Controls.Add(this.comboBoxDisciplineLessonTask);
            this.Controls.Add(this.labelSelectDLT);
            this.Name = "DuplicateDisciplineLessonTaskForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Дублирование вариантов";
            this.Load += new System.EventHandler(this.DuplicateDisciplineLessonTaskForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSelectDLT;
        private System.Windows.Forms.ComboBox comboBoxDisciplineLessonTask;
        private System.Windows.Forms.Button buttonDuplicate;
    }
}