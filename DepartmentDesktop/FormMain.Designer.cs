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
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.lecturerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.disciplineBlockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.disciplineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.studentGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.streamingLessonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.studentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.studentsStudentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.studentsGraduateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.studentsAcademToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.studentsDeductionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.classroomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.seasonDatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.academicPlansAndOtherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.academicPlansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.kindOfLoadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.academicYearsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contingentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.timeNormsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadDistributionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scheduleClassroomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scheduleClassroomSemesterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scheduleClassroomOffsetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scheduleClassroomExaminationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scheduleClassroomConsultationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scheduleStudentGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scheduleStudentGroupSemesterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scheduleStudentGroupOffsetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scheduleStudentGroupExaminationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scheduleStudentGroupConsultationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scheduleLecturerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scheduleLecturerSemesterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scheduleLecturerOffsetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scheduleLecturerExaminationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scheduleLecturerConsultationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.scheduleConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scheduleLessonTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rolesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.UsersToolStripMenuItem,
            this.rolesToolStripMenuItem});
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
            this.toolStripSeparator3,
            this.lecturerToolStripMenuItem,
            this.toolStripSeparator4,
            this.disciplineBlockToolStripMenuItem,
            this.disciplineToolStripMenuItem,
            this.toolStripSeparator6,
            this.studentGroupToolStripMenuItem,
            this.streamingLessonsToolStripMenuItem,
            this.studentsToolStripMenuItem,
            this.toolStripSeparator5,
            this.classroomToolStripMenuItem,
            this.toolStripSeparator7,
            this.seasonDatesToolStripMenuItem,
            this.toolStripSeparator2,
            this.academicPlansAndOtherToolStripMenuItem,
            this.loadDistributionToolStripMenuItem});
			this.educationalProcessToolStripMenuItem.Name = "educationalProcessToolStripMenuItem";
			this.educationalProcessToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
			this.educationalProcessToolStripMenuItem.Text = "Учебный процесс";
			// 
			// educationDirectionToolStripMenuItem
			// 
			this.educationDirectionToolStripMenuItem.Name = "educationDirectionToolStripMenuItem";
			this.educationDirectionToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.educationDirectionToolStripMenuItem.Text = "Направления";
			this.educationDirectionToolStripMenuItem.Click += new System.EventHandler(this.educationDirectionToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(169, 6);
			// 
			// lecturerToolStripMenuItem
			// 
			this.lecturerToolStripMenuItem.Name = "lecturerToolStripMenuItem";
			this.lecturerToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.lecturerToolStripMenuItem.Text = "Преподаватели";
			this.lecturerToolStripMenuItem.Click += new System.EventHandler(this.lecturerToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(169, 6);
			// 
			// disciplineBlockToolStripMenuItem
			// 
			this.disciplineBlockToolStripMenuItem.Name = "disciplineBlockToolStripMenuItem";
			this.disciplineBlockToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.disciplineBlockToolStripMenuItem.Text = "Блоки дисциплин";
			this.disciplineBlockToolStripMenuItem.Click += new System.EventHandler(this.disciplineBlockToolStripMenuItem_Click);
			// 
			// disciplineToolStripMenuItem
			// 
			this.disciplineToolStripMenuItem.Name = "disciplineToolStripMenuItem";
			this.disciplineToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.disciplineToolStripMenuItem.Text = "Дисциплины";
			this.disciplineToolStripMenuItem.Click += new System.EventHandler(this.disciplineToolStripMenuItem_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(169, 6);
			// 
			// studentGroupToolStripMenuItem
			// 
			this.studentGroupToolStripMenuItem.Name = "studentGroupToolStripMenuItem";
			this.studentGroupToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.studentGroupToolStripMenuItem.Text = "Группы";
			this.studentGroupToolStripMenuItem.Click += new System.EventHandler(this.studentGroupToolStripMenuItem_Click);
			// 
			// streamingLessonsToolStripMenuItem
			// 
			this.streamingLessonsToolStripMenuItem.Name = "streamingLessonsToolStripMenuItem";
			this.streamingLessonsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.streamingLessonsToolStripMenuItem.Text = "Потоки";
			this.streamingLessonsToolStripMenuItem.Click += new System.EventHandler(this.streamingLessonsToolStripMenuItem_Click);
			// 
			// studentsToolStripMenuItem
			// 
			this.studentsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.studentsStudentToolStripMenuItem,
            this.studentsGraduateToolStripMenuItem,
            this.studentsAcademToolStripMenuItem,
            this.studentsDeductionToolStripMenuItem});
			this.studentsToolStripMenuItem.Name = "studentsToolStripMenuItem";
			this.studentsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.studentsToolStripMenuItem.Text = "Студенты";
			// 
			// studentsStudentToolStripMenuItem
			// 
			this.studentsStudentToolStripMenuItem.Name = "studentsStudentToolStripMenuItem";
			this.studentsStudentToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
			this.studentsStudentToolStripMenuItem.Text = "Учащиеся";
			this.studentsStudentToolStripMenuItem.Click += new System.EventHandler(this.studentsStudentToolStripMenuItem_Click);
			// 
			// studentsGraduateToolStripMenuItem
			// 
			this.studentsGraduateToolStripMenuItem.Name = "studentsGraduateToolStripMenuItem";
			this.studentsGraduateToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
			this.studentsGraduateToolStripMenuItem.Text = "Завершившие обучение";
			this.studentsGraduateToolStripMenuItem.Click += new System.EventHandler(this.studentsGraduateToolStripMenuItem_Click);
			// 
			// studentsAcademToolStripMenuItem
			// 
			this.studentsAcademToolStripMenuItem.Name = "studentsAcademToolStripMenuItem";
			this.studentsAcademToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
			this.studentsAcademToolStripMenuItem.Text = "В академическом отпуске";
			this.studentsAcademToolStripMenuItem.Click += new System.EventHandler(this.studentsAcademToolStripMenuItem_Click);
			// 
			// studentsDeductionToolStripMenuItem
			// 
			this.studentsDeductionToolStripMenuItem.Name = "studentsDeductionToolStripMenuItem";
			this.studentsDeductionToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
			this.studentsDeductionToolStripMenuItem.Text = "Отчисленные";
			this.studentsDeductionToolStripMenuItem.Click += new System.EventHandler(this.studentsDeductionToolStripMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(169, 6);
			// 
			// classroomToolStripMenuItem
			// 
			this.classroomToolStripMenuItem.Name = "classroomToolStripMenuItem";
			this.classroomToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.classroomToolStripMenuItem.Text = "Аудитории";
			this.classroomToolStripMenuItem.Click += new System.EventHandler(this.classroomToolStripMenuItem_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(169, 6);
			// 
			// seasonDatesToolStripMenuItem
			// 
			this.seasonDatesToolStripMenuItem.Name = "seasonDatesToolStripMenuItem";
			this.seasonDatesToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.seasonDatesToolStripMenuItem.Text = "Даты семестра";
			this.seasonDatesToolStripMenuItem.Click += new System.EventHandler(this.seasonDatesToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(169, 6);
			// 
			// academicPlansAndOtherToolStripMenuItem
			// 
			this.academicPlansAndOtherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.academicPlansToolStripMenuItem,
            this.kindOfLoadsToolStripMenuItem,
            this.academicYearsToolStripMenuItem,
            this.contingentsToolStripMenuItem,
            this.timeNormsToolStripMenuItem});
			this.academicPlansAndOtherToolStripMenuItem.Name = "academicPlansAndOtherToolStripMenuItem";
			this.academicPlansAndOtherToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.academicPlansAndOtherToolStripMenuItem.Text = "Учебные планы";
			// 
			// academicPlansToolStripMenuItem
			// 
			this.academicPlansToolStripMenuItem.Name = "academicPlansToolStripMenuItem";
			this.academicPlansToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.academicPlansToolStripMenuItem.Text = "Учебные планы";
			this.academicPlansToolStripMenuItem.Click += new System.EventHandler(this.academicPlansToolStripMenuItem_Click);
			// 
			// kindOfLoadsToolStripMenuItem
			// 
			this.kindOfLoadsToolStripMenuItem.Name = "kindOfLoadsToolStripMenuItem";
			this.kindOfLoadsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.kindOfLoadsToolStripMenuItem.Text = "Виды нагрузок";
			this.kindOfLoadsToolStripMenuItem.Click += new System.EventHandler(this.kindOfLoadsToolStripMenuItem_Click);
			// 
			// academicYearsToolStripMenuItem
			// 
			this.academicYearsToolStripMenuItem.Name = "academicYearsToolStripMenuItem";
			this.academicYearsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.academicYearsToolStripMenuItem.Text = "Учебные года";
			this.academicYearsToolStripMenuItem.Click += new System.EventHandler(this.academicYearsToolStripMenuItem_Click);
			// 
			// contingentsToolStripMenuItem
			// 
			this.contingentsToolStripMenuItem.Name = "contingentsToolStripMenuItem";
			this.contingentsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.contingentsToolStripMenuItem.Text = "Контингент";
			this.contingentsToolStripMenuItem.Click += new System.EventHandler(this.contingentsToolStripMenuItem_Click);
			// 
			// timeNormsToolStripMenuItem
			// 
			this.timeNormsToolStripMenuItem.Name = "timeNormsToolStripMenuItem";
			this.timeNormsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.timeNormsToolStripMenuItem.Text = "Нормы времени";
			this.timeNormsToolStripMenuItem.Click += new System.EventHandler(this.timeNormsToolStripMenuItem_Click);
			// 
			// loadDistributionToolStripMenuItem
			// 
			this.loadDistributionToolStripMenuItem.Name = "loadDistributionToolStripMenuItem";
			this.loadDistributionToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.loadDistributionToolStripMenuItem.Text = "Расчет штатов";
			this.loadDistributionToolStripMenuItem.Click += new System.EventHandler(this.loadDistributionToolStripMenuItem_Click);
			// 
			// scheduleToolStripMenuItem
			// 
			this.scheduleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scheduleClassroomToolStripMenuItem,
            this.scheduleStudentGroupToolStripMenuItem,
            this.scheduleLecturerToolStripMenuItem,
            this.toolStripSeparator1,
            this.scheduleConfigToolStripMenuItem,
            this.scheduleLessonTimeToolStripMenuItem});
			this.scheduleToolStripMenuItem.Name = "scheduleToolStripMenuItem";
			this.scheduleToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
			this.scheduleToolStripMenuItem.Text = "Расписание";
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
			// scheduleStudentGroupToolStripMenuItem
			// 
			this.scheduleStudentGroupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scheduleStudentGroupSemesterToolStripMenuItem,
            this.scheduleStudentGroupOffsetToolStripMenuItem,
            this.scheduleStudentGroupExaminationToolStripMenuItem,
            this.scheduleStudentGroupConsultationToolStripMenuItem});
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
			// scheduleStudentGroupOffsetToolStripMenuItem
			// 
			this.scheduleStudentGroupOffsetToolStripMenuItem.Name = "scheduleStudentGroupOffsetToolStripMenuItem";
			this.scheduleStudentGroupOffsetToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.scheduleStudentGroupOffsetToolStripMenuItem.Text = "Зачетная неделя";
			this.scheduleStudentGroupOffsetToolStripMenuItem.Click += new System.EventHandler(this.scheduleStudentGroupOffsetToolStripMenuItem_Click);
			// 
			// scheduleStudentGroupExaminationToolStripMenuItem
			// 
			this.scheduleStudentGroupExaminationToolStripMenuItem.Name = "scheduleStudentGroupExaminationToolStripMenuItem";
			this.scheduleStudentGroupExaminationToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.scheduleStudentGroupExaminationToolStripMenuItem.Text = "Экзамены";
			this.scheduleStudentGroupExaminationToolStripMenuItem.Click += new System.EventHandler(this.scheduleStudentGroupExaminationToolStripMenuItem_Click);
			// 
			// scheduleStudentGroupConsultationToolStripMenuItem
			// 
			this.scheduleStudentGroupConsultationToolStripMenuItem.Name = "scheduleStudentGroupConsultationToolStripMenuItem";
			this.scheduleStudentGroupConsultationToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.scheduleStudentGroupConsultationToolStripMenuItem.Text = "Консультации";
			this.scheduleStudentGroupConsultationToolStripMenuItem.Click += new System.EventHandler(this.scheduleStudentGroupConsultationToolStripMenuItem_Click);
			// 
			// scheduleLecturerToolStripMenuItem
			// 
			this.scheduleLecturerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scheduleLecturerSemesterToolStripMenuItem,
            this.scheduleLecturerOffsetToolStripMenuItem,
            this.scheduleLecturerExaminationToolStripMenuItem,
            this.scheduleLecturerConsultationToolStripMenuItem});
			this.scheduleLecturerToolStripMenuItem.Name = "scheduleLecturerToolStripMenuItem";
			this.scheduleLecturerToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.scheduleLecturerToolStripMenuItem.Text = "Преподаватели";
			// 
			// scheduleLecturerSemesterToolStripMenuItem
			// 
			this.scheduleLecturerSemesterToolStripMenuItem.Name = "scheduleLecturerSemesterToolStripMenuItem";
			this.scheduleLecturerSemesterToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.scheduleLecturerSemesterToolStripMenuItem.Text = "Семестр";
			this.scheduleLecturerSemesterToolStripMenuItem.Click += new System.EventHandler(this.scheduleLecturerSemesterToolStripMenuItem_Click);
			// 
			// scheduleLecturerOffsetToolStripMenuItem
			// 
			this.scheduleLecturerOffsetToolStripMenuItem.Name = "scheduleLecturerOffsetToolStripMenuItem";
			this.scheduleLecturerOffsetToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.scheduleLecturerOffsetToolStripMenuItem.Text = "Зачетная неделя";
			this.scheduleLecturerOffsetToolStripMenuItem.Click += new System.EventHandler(this.scheduleLecturerOffsetToolStripMenuItem_Click);
			// 
			// scheduleLecturerExaminationToolStripMenuItem
			// 
			this.scheduleLecturerExaminationToolStripMenuItem.Name = "scheduleLecturerExaminationToolStripMenuItem";
			this.scheduleLecturerExaminationToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.scheduleLecturerExaminationToolStripMenuItem.Text = "Экзамены";
			this.scheduleLecturerExaminationToolStripMenuItem.Click += new System.EventHandler(this.scheduleLecturerExaminationToolStripMenuItem_Click);
			// 
			// scheduleLecturerConsultationToolStripMenuItem
			// 
			this.scheduleLecturerConsultationToolStripMenuItem.Name = "scheduleLecturerConsultationToolStripMenuItem";
			this.scheduleLecturerConsultationToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.scheduleLecturerConsultationToolStripMenuItem.Text = "Консультации";
			this.scheduleLecturerConsultationToolStripMenuItem.Click += new System.EventHandler(this.scheduleLecturerConsultationToolStripMenuItem_Click);
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
			// scheduleLessonTimeToolStripMenuItem
			// 
			this.scheduleLessonTimeToolStripMenuItem.Name = "scheduleLessonTimeToolStripMenuItem";
			this.scheduleLessonTimeToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.scheduleLessonTimeToolStripMenuItem.Text = "Интервалы пар";
			this.scheduleLessonTimeToolStripMenuItem.Click += new System.EventHandler(this.scheduleLessonTimeToolStripMenuItem_Click);
			// 
			// rolesToolStripMenuItem
			// 
			this.rolesToolStripMenuItem.Name = "rolesToolStripMenuItem";
			this.rolesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.rolesToolStripMenuItem.Text = "Роли";
			this.rolesToolStripMenuItem.Click += new System.EventHandler(this.rolesToolStripMenuItem_Click);
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
        private System.Windows.Forms.ToolStripMenuItem scheduleStudentGroupExaminationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleStudentGroupConsultationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem studentsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem studentsStudentToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem studentsGraduateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem studentsAcademToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem studentsDeductionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem academicPlansAndOtherToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem kindOfLoadsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem academicPlansToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem academicYearsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem contingentsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem timeNormsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem lecturerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadDistributionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem disciplineBlockToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem disciplineToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem scheduleLecturerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem scheduleLecturerSemesterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem scheduleLecturerOffsetToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem scheduleLecturerExaminationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem scheduleLecturerConsultationToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem rolesToolStripMenuItem;
	}
}

