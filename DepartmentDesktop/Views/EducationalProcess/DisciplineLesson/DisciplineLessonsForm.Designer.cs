namespace DepartmentDesktop.Views.EducationalProcess.DisciplineLesson
{
    partial class DisciplineLessonsForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageLessonsList = new System.Windows.Forms.TabPage();
            this.tabControlLessons = new System.Windows.Forms.TabControl();
            this.tabPageLectures = new System.Windows.Forms.TabPage();
            this.tabPageLaboratory = new System.Windows.Forms.TabPage();
            this.tabPageStudents = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tabPageLessonsList.SuspendLayout();
            this.tabControlLessons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageLessonsList);
            this.tabControl.Controls.Add(this.tabPageStudents);
            this.tabControl.Location = new System.Drawing.Point(1, 2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(701, 398);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageLessonsList
            // 
            this.tabPageLessonsList.Controls.Add(this.tabControlLessons);
            this.tabPageLessonsList.Location = new System.Drawing.Point(4, 22);
            this.tabPageLessonsList.Name = "tabPageLessonsList";
            this.tabPageLessonsList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLessonsList.Size = new System.Drawing.Size(693, 372);
            this.tabPageLessonsList.TabIndex = 0;
            this.tabPageLessonsList.Text = "Список занятий";
            this.tabPageLessonsList.UseVisualStyleBackColor = true;
            // 
            // tabControlLessons
            // 
            this.tabControlLessons.Controls.Add(this.tabPageLectures);
            this.tabControlLessons.Controls.Add(this.tabPageLaboratory);
            this.tabControlLessons.Location = new System.Drawing.Point(8, 7);
            this.tabControlLessons.Name = "tabControlLessons";
            this.tabControlLessons.SelectedIndex = 0;
            this.tabControlLessons.Size = new System.Drawing.Size(679, 359);
            this.tabControlLessons.TabIndex = 0;
            // 
            // tabPageLectures
            // 
            this.tabPageLectures.Location = new System.Drawing.Point(4, 22);
            this.tabPageLectures.Name = "tabPageLectures";
            this.tabPageLectures.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLectures.Size = new System.Drawing.Size(671, 333);
            this.tabPageLectures.TabIndex = 0;
            this.tabPageLectures.Text = "Лекции";
            this.tabPageLectures.UseVisualStyleBackColor = true;
            // 
            // tabPageLaboratory
            // 
            this.tabPageLaboratory.Location = new System.Drawing.Point(4, 22);
            this.tabPageLaboratory.Name = "tabPageLaboratory";
            this.tabPageLaboratory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLaboratory.Size = new System.Drawing.Size(671, 333);
            this.tabPageLaboratory.TabIndex = 1;
            this.tabPageLaboratory.Text = "Лабораторные";
            this.tabPageLaboratory.UseVisualStyleBackColor = true;
            // 
            // tabPageStudents
            // 
            this.tabPageStudents.Location = new System.Drawing.Point(4, 22);
            this.tabPageStudents.Name = "tabPageStudents";
            this.tabPageStudents.Size = new System.Drawing.Size(693, 372);
            this.tabPageStudents.TabIndex = 1;
            this.tabPageStudents.Text = "Студенты";
            this.tabPageStudents.UseVisualStyleBackColor = true;
            // 
            // DisciplineLessonsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 400);
            this.Controls.Add(this.tabControl);
            this.Name = "DisciplineLessonsForm";
            this.Text = "Занятие";
            this.Load += new System.EventHandler(this.DisciplineLessonForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageLessonsList.ResumeLayout(false);
            this.tabControlLessons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageLessonsList;
        private System.Windows.Forms.TabControl tabControlLessons;
        private System.Windows.Forms.TabPage tabPageLectures;
        private System.Windows.Forms.TabPage tabPageLaboratory;
        private System.Windows.Forms.TabPage tabPageStudents;
    }
}