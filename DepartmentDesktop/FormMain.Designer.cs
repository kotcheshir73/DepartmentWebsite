﻿namespace DepartmentDesktop
{
    partial class FormMain
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.действияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MakeTicketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AdminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.educationalProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.educationDirectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.studentGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.classroomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seasonDatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.streamingLessonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleStudentGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleStudentGroupSemesterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleClassroomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleClassroomSemesterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleClassroomOffsetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleClassroomExaminationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleClassroomConsultationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.scheduleConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleStopWordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleLessonTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleStudentGroupOffsetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.действияToolStripMenuItem,
            this.AdminToolStripMenuItem,
            this.educationalProcessToolStripMenuItem,
            this.scheduleToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(784, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // действияToolStripMenuItem
            // 
            this.действияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MakeTicketsToolStripMenuItem});
            this.действияToolStripMenuItem.Name = "действияToolStripMenuItem";
            this.действияToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.действияToolStripMenuItem.Text = "Сервис";
            // 
            // MakeTicketsToolStripMenuItem
            // 
            this.MakeTicketsToolStripMenuItem.Name = "MakeTicketsToolStripMenuItem";
            this.MakeTicketsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.MakeTicketsToolStripMenuItem.Text = "Генерация билетов";
            this.MakeTicketsToolStripMenuItem.Click += new System.EventHandler(this.MakeTicketsToolStripMenuItem_Click);
            // 
            // AdminToolStripMenuItem
            // 
            this.AdminToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UsersToolStripMenuItem});
            this.AdminToolStripMenuItem.Name = "AdminToolStripMenuItem";
            this.AdminToolStripMenuItem.Size = new System.Drawing.Size(134, 20);
            this.AdminToolStripMenuItem.Text = "Администрирование";
            // 
            // UsersToolStripMenuItem
            // 
            this.UsersToolStripMenuItem.Name = "UsersToolStripMenuItem";
            this.UsersToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.UsersToolStripMenuItem.Text = "Пользователи";
            this.UsersToolStripMenuItem.Click += new System.EventHandler(this.UsersToolStripMenuItem_Click);
            // 
            // educationalProcessToolStripMenuItem
            // 
            this.educationalProcessToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.educationDirectionToolStripMenuItem,
            this.studentGroupToolStripMenuItem,
            this.classroomToolStripMenuItem,
            this.seasonDatesToolStripMenuItem,
            this.streamingLessonsToolStripMenuItem});
            this.educationalProcessToolStripMenuItem.Name = "educationalProcessToolStripMenuItem";
            this.educationalProcessToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.educationalProcessToolStripMenuItem.Text = "Учебный процесс";
            // 
            // educationDirectionToolStripMenuItem
            // 
            this.educationDirectionToolStripMenuItem.Name = "educationDirectionToolStripMenuItem";
            this.educationDirectionToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.educationDirectionToolStripMenuItem.Text = "Направления";
            this.educationDirectionToolStripMenuItem.Click += new System.EventHandler(this.educationDirectionToolStripMenuItem_Click);
            // 
            // studentGroupToolStripMenuItem
            // 
            this.studentGroupToolStripMenuItem.Name = "studentGroupToolStripMenuItem";
            this.studentGroupToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.studentGroupToolStripMenuItem.Text = "Группы";
            this.studentGroupToolStripMenuItem.Click += new System.EventHandler(this.studentGroupToolStripMenuItem_Click);
            // 
            // classroomToolStripMenuItem
            // 
            this.classroomToolStripMenuItem.Name = "classroomToolStripMenuItem";
            this.classroomToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.classroomToolStripMenuItem.Text = "Аудитории";
            this.classroomToolStripMenuItem.Click += new System.EventHandler(this.classroomToolStripMenuItem_Click);
            // 
            // seasonDatesToolStripMenuItem
            // 
            this.seasonDatesToolStripMenuItem.Name = "seasonDatesToolStripMenuItem";
            this.seasonDatesToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.seasonDatesToolStripMenuItem.Text = "Даты семестра";
            this.seasonDatesToolStripMenuItem.Click += new System.EventHandler(this.seasonDatesToolStripMenuItem_Click);
            // 
            // streamingLessonsToolStripMenuItem
            // 
            this.streamingLessonsToolStripMenuItem.Name = "streamingLessonsToolStripMenuItem";
            this.streamingLessonsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.streamingLessonsToolStripMenuItem.Text = "Потоки";
            this.streamingLessonsToolStripMenuItem.Click += new System.EventHandler(this.streamingLessonsToolStripMenuItem_Click);
            // 
            // scheduleToolStripMenuItem
            // 
            this.scheduleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scheduleStudentGroupToolStripMenuItem,
            this.scheduleClassroomToolStripMenuItem,
            this.toolStripSeparator1,
            this.scheduleConfigToolStripMenuItem,
            this.scheduleStopWordsToolStripMenuItem,
            this.scheduleLessonTimeToolStripMenuItem});
            this.scheduleToolStripMenuItem.Name = "scheduleToolStripMenuItem";
            this.scheduleToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.scheduleToolStripMenuItem.Text = "Расписание";
            // 
            // scheduleStudentGroupToolStripMenuItem
            // 
            this.scheduleStudentGroupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scheduleStudentGroupSemesterToolStripMenuItem,
            this.scheduleStudentGroupOffsetToolStripMenuItem});
            this.scheduleStudentGroupToolStripMenuItem.Name = "scheduleStudentGroupToolStripMenuItem";
            this.scheduleStudentGroupToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.scheduleStudentGroupToolStripMenuItem.Text = "Группы";
            // 
            // scheduleStudentGroupSemesterToolStripMenuItem
            // 
            this.scheduleStudentGroupSemesterToolStripMenuItem.Name = "scheduleStudentGroupSemesterToolStripMenuItem";
            this.scheduleStudentGroupSemesterToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.scheduleStudentGroupSemesterToolStripMenuItem.Text = "Семестр";
            this.scheduleStudentGroupSemesterToolStripMenuItem.Click += new System.EventHandler(this.scheduleStudentGroupSemesterToolStripMenuItem_Click);
            // 
            // scheduleClassroomToolStripMenuItem
            // 
            this.scheduleClassroomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scheduleClassroomSemesterToolStripMenuItem,
            this.scheduleClassroomOffsetToolStripMenuItem,
            this.scheduleClassroomExaminationToolStripMenuItem,
            this.scheduleClassroomConsultationToolStripMenuItem});
            this.scheduleClassroomToolStripMenuItem.Name = "scheduleClassroomToolStripMenuItem";
            this.scheduleClassroomToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.scheduleClassroomToolStripMenuItem.Text = "Аудитории";
            // 
            // scheduleClassroomSemesterToolStripMenuItem
            // 
            this.scheduleClassroomSemesterToolStripMenuItem.Name = "scheduleClassroomSemesterToolStripMenuItem";
            this.scheduleClassroomSemesterToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.scheduleClassroomSemesterToolStripMenuItem.Text = "Семестр";
            this.scheduleClassroomSemesterToolStripMenuItem.Click += new System.EventHandler(this.scheduleClassroomSemesterToolStripMenuItem_Click);
            // 
            // scheduleClassroomOffsetToolStripMenuItem
            // 
            this.scheduleClassroomOffsetToolStripMenuItem.Name = "scheduleClassroomOffsetToolStripMenuItem";
            this.scheduleClassroomOffsetToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.scheduleClassroomOffsetToolStripMenuItem.Text = "Зачетная неделя";
            this.scheduleClassroomOffsetToolStripMenuItem.Click += new System.EventHandler(this.scheduleClassroomOffsetToolStripMenuItem_Click);
            // 
            // scheduleClassroomExaminationToolStripMenuItem
            // 
            this.scheduleClassroomExaminationToolStripMenuItem.Name = "scheduleClassroomExaminationToolStripMenuItem";
            this.scheduleClassroomExaminationToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.scheduleClassroomExaminationToolStripMenuItem.Text = "Экзамены";
            this.scheduleClassroomExaminationToolStripMenuItem.Click += new System.EventHandler(this.scheduleClassroomExaminationToolStripMenuItem_Click);
            // 
            // scheduleClassroomConsultationToolStripMenuItem
            // 
            this.scheduleClassroomConsultationToolStripMenuItem.Name = "scheduleClassroomConsultationToolStripMenuItem";
            this.scheduleClassroomConsultationToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.scheduleClassroomConsultationToolStripMenuItem.Text = "Консультации";
            this.scheduleClassroomConsultationToolStripMenuItem.Click += new System.EventHandler(this.scheduleClassroomConsultationToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(156, 6);
            // 
            // scheduleConfigToolStripMenuItem
            // 
            this.scheduleConfigToolStripMenuItem.Name = "scheduleConfigToolStripMenuItem";
            this.scheduleConfigToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.scheduleConfigToolStripMenuItem.Text = "Настройки";
            this.scheduleConfigToolStripMenuItem.Click += new System.EventHandler(this.scheduleConfigToolStripMenuItem_Click);
            // 
            // scheduleStopWordsToolStripMenuItem
            // 
            this.scheduleStopWordsToolStripMenuItem.Name = "scheduleStopWordsToolStripMenuItem";
            this.scheduleStopWordsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.scheduleStopWordsToolStripMenuItem.Text = "Стоп-слова";
            this.scheduleStopWordsToolStripMenuItem.Click += new System.EventHandler(this.scheduleStopWordsToolStripMenuItem_Click);
            // 
            // scheduleLessonTimeToolStripMenuItem
            // 
            this.scheduleLessonTimeToolStripMenuItem.Name = "scheduleLessonTimeToolStripMenuItem";
            this.scheduleLessonTimeToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.scheduleLessonTimeToolStripMenuItem.Text = "Интервалы пар";
            this.scheduleLessonTimeToolStripMenuItem.Click += new System.EventHandler(this.scheduleLessonTimeToolStripMenuItem_Click);
            // 
            // scheduleStudentGroupOffsetToolStripMenuItem
            // 
            this.scheduleStudentGroupOffsetToolStripMenuItem.Name = "scheduleStudentGroupOffsetToolStripMenuItem";
            this.scheduleStudentGroupOffsetToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.scheduleStudentGroupOffsetToolStripMenuItem.Text = "Зачетная неделя";
            this.scheduleStudentGroupOffsetToolStripMenuItem.Click += new System.EventHandler(this.scheduleStudentGroupOffsetToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 412);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главная форма";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem действияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MakeTicketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AdminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem educationalProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem educationDirectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem studentGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem classroomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleClassroomSemesterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seasonDatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem streamingLessonsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleStopWordsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleClassroomConsultationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleClassroomOffsetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleClassroomExaminationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleLessonTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem scheduleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleClassroomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleStudentGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleStudentGroupSemesterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleStudentGroupOffsetToolStripMenuItem;
    }
}

