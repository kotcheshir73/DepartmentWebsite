namespace DepartmentDesktop.Views.Schedule
{
    partial class ScheduleConfigControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScheduleConfigControl));
            this.groupBoxLoadHTMLScheduleForClassrooms = new System.Windows.Forms.GroupBox();
            this.checkBoxIsFirstHalfSemester = new System.Windows.Forms.CheckBox();
            this.textBoxLinkToHtml = new System.Windows.Forms.TextBox();
            this.labelLinkToHtml = new System.Windows.Forms.Label();
            this.buttonMakeLoadHTMLScheduleForClassrooms = new System.Windows.Forms.Button();
            this.checkedListBoxClassrooms = new System.Windows.Forms.CheckedListBox();
            this.groupBoxClassrooms = new System.Windows.Forms.GroupBox();
            this.groupBoxClearClassrooms = new System.Windows.Forms.GroupBox();
            this.buttonClearConsultationRecordClassrooms = new System.Windows.Forms.Button();
            this.buttonClearExaminationRecordClassrooms = new System.Windows.Forms.Button();
            this.buttonClearOffsetRecordClassrooms = new System.Windows.Forms.Button();
            this.buttonClearSemesterRecordClassrooms = new System.Windows.Forms.Button();
            this.groupBoxExport = new System.Windows.Forms.GroupBox();
            this.buttonExportExaminationRecordHTML = new System.Windows.Forms.Button();
            this.buttonExportExaminationRecordExcel = new System.Windows.Forms.Button();
            this.buttonExportOffsetRecordHTML = new System.Windows.Forms.Button();
            this.buttonExportOffsetRecordExcel = new System.Windows.Forms.Button();
            this.buttonExportSemesterRecordExcel = new System.Windows.Forms.Button();
            this.buttonExportSemesterRecordHTML = new System.Windows.Forms.Button();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.buttonSeasonDatesSave = new System.Windows.Forms.Button();
            this.comboBoxSeasonDates = new System.Windows.Forms.ComboBox();
            this.labelSeasonDates = new System.Windows.Forms.Label();
            this.groupBoxCheckRecordsIfNotComplite = new System.Windows.Forms.GroupBox();
            this.buttonCheckRecordsIfNotComplite = new System.Windows.Forms.Button();
            this.groupBoxImport = new System.Windows.Forms.GroupBox();
            this.buttonImportExaminationFromExcel = new System.Windows.Forms.Button();
            this.buttonImportOffsetFromExcel = new System.Windows.Forms.Button();
            this.groupBoxStudentGroups = new System.Windows.Forms.GroupBox();
            this.checkedListBoxStudentGroups = new System.Windows.Forms.CheckedListBox();
            this.groupBoxLecturers = new System.Windows.Forms.GroupBox();
            this.checkedListBoxLecturers = new System.Windows.Forms.CheckedListBox();
            this.groupBoxExportInfo = new System.Windows.Forms.GroupBox();
            this.labelOffsetExport = new System.Windows.Forms.Label();
            this.groupBoxLoadHTMLScheduleForClassrooms.SuspendLayout();
            this.groupBoxClassrooms.SuspendLayout();
            this.groupBoxClearClassrooms.SuspendLayout();
            this.groupBoxExport.SuspendLayout();
            this.groupBoxSettings.SuspendLayout();
            this.groupBoxCheckRecordsIfNotComplite.SuspendLayout();
            this.groupBoxImport.SuspendLayout();
            this.groupBoxStudentGroups.SuspendLayout();
            this.groupBoxLecturers.SuspendLayout();
            this.groupBoxExportInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxLoadHTMLScheduleForClassrooms
            // 
            this.groupBoxLoadHTMLScheduleForClassrooms.Controls.Add(this.checkBoxIsFirstHalfSemester);
            this.groupBoxLoadHTMLScheduleForClassrooms.Controls.Add(this.textBoxLinkToHtml);
            this.groupBoxLoadHTMLScheduleForClassrooms.Controls.Add(this.labelLinkToHtml);
            this.groupBoxLoadHTMLScheduleForClassrooms.Controls.Add(this.buttonMakeLoadHTMLScheduleForClassrooms);
            this.groupBoxLoadHTMLScheduleForClassrooms.Location = new System.Drawing.Point(359, 0);
            this.groupBoxLoadHTMLScheduleForClassrooms.Name = "groupBoxLoadHTMLScheduleForClassrooms";
            this.groupBoxLoadHTMLScheduleForClassrooms.Size = new System.Drawing.Size(360, 100);
            this.groupBoxLoadHTMLScheduleForClassrooms.TabIndex = 1;
            this.groupBoxLoadHTMLScheduleForClassrooms.TabStop = false;
            this.groupBoxLoadHTMLScheduleForClassrooms.Text = "Обновление расписания по аудиториям";
            // 
            // checkBoxIsFirstHalfSemester
            // 
            this.checkBoxIsFirstHalfSemester.AutoSize = true;
            this.checkBoxIsFirstHalfSemester.Location = new System.Drawing.Point(16, 62);
            this.checkBoxIsFirstHalfSemester.Name = "checkBoxIsFirstHalfSemester";
            this.checkBoxIsFirstHalfSemester.Size = new System.Drawing.Size(128, 17);
            this.checkBoxIsFirstHalfSemester.TabIndex = 5;
            this.checkBoxIsFirstHalfSemester.Text = "Первый полупериод";
            this.checkBoxIsFirstHalfSemester.UseVisualStyleBackColor = true;
            // 
            // textBoxLinkToHtml
            // 
            this.textBoxLinkToHtml.Location = new System.Drawing.Point(100, 28);
            this.textBoxLinkToHtml.Name = "textBoxLinkToHtml";
            this.textBoxLinkToHtml.Size = new System.Drawing.Size(250, 20);
            this.textBoxLinkToHtml.TabIndex = 4;
            this.textBoxLinkToHtml.Text = "http://www.ulstu.ru/schedule/students/";
            // 
            // labelLinkToHtml
            // 
            this.labelLinkToHtml.AutoSize = true;
            this.labelLinkToHtml.Location = new System.Drawing.Point(13, 31);
            this.labelLinkToHtml.Name = "labelLinkToHtml";
            this.labelLinkToHtml.Size = new System.Drawing.Size(81, 13);
            this.labelLinkToHtml.TabIndex = 3;
            this.labelLinkToHtml.Text = "Путь до сайта:";
            // 
            // buttonMakeLoadHTMLScheduleForClassrooms
            // 
            this.buttonMakeLoadHTMLScheduleForClassrooms.Location = new System.Drawing.Point(275, 54);
            this.buttonMakeLoadHTMLScheduleForClassrooms.Name = "buttonMakeLoadHTMLScheduleForClassrooms";
            this.buttonMakeLoadHTMLScheduleForClassrooms.Size = new System.Drawing.Size(75, 30);
            this.buttonMakeLoadHTMLScheduleForClassrooms.TabIndex = 2;
            this.buttonMakeLoadHTMLScheduleForClassrooms.Text = "Обновить";
            this.buttonMakeLoadHTMLScheduleForClassrooms.UseVisualStyleBackColor = true;
            this.buttonMakeLoadHTMLScheduleForClassrooms.Click += new System.EventHandler(this.buttonMakeLoadHTMLScheduleForClassrooms_Click);
            // 
            // checkedListBoxClassrooms
            // 
            this.checkedListBoxClassrooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxClassrooms.FormattingEnabled = true;
            this.checkedListBoxClassrooms.Location = new System.Drawing.Point(3, 16);
            this.checkedListBoxClassrooms.Name = "checkedListBoxClassrooms";
            this.checkedListBoxClassrooms.Size = new System.Drawing.Size(104, 131);
            this.checkedListBoxClassrooms.TabIndex = 0;
            // 
            // groupBoxClassrooms
            // 
            this.groupBoxClassrooms.Controls.Add(this.checkedListBoxClassrooms);
            this.groupBoxClassrooms.Location = new System.Drawing.Point(0, 0);
            this.groupBoxClassrooms.Name = "groupBoxClassrooms";
            this.groupBoxClassrooms.Size = new System.Drawing.Size(110, 150);
            this.groupBoxClassrooms.TabIndex = 0;
            this.groupBoxClassrooms.TabStop = false;
            this.groupBoxClassrooms.Text = "Аудитории";
            // 
            // groupBoxClearClassrooms
            // 
            this.groupBoxClearClassrooms.Controls.Add(this.buttonClearConsultationRecordClassrooms);
            this.groupBoxClearClassrooms.Controls.Add(this.buttonClearExaminationRecordClassrooms);
            this.groupBoxClearClassrooms.Controls.Add(this.buttonClearOffsetRecordClassrooms);
            this.groupBoxClearClassrooms.Controls.Add(this.buttonClearSemesterRecordClassrooms);
            this.groupBoxClearClassrooms.Location = new System.Drawing.Point(359, 106);
            this.groupBoxClearClassrooms.Name = "groupBoxClearClassrooms";
            this.groupBoxClearClassrooms.Size = new System.Drawing.Size(200, 204);
            this.groupBoxClearClassrooms.TabIndex = 2;
            this.groupBoxClearClassrooms.TabStop = false;
            this.groupBoxClearClassrooms.Text = "Отчистка аудиторий";
            // 
            // buttonClearConsultationRecordClassrooms
            // 
            this.buttonClearConsultationRecordClassrooms.Location = new System.Drawing.Point(25, 150);
            this.buttonClearConsultationRecordClassrooms.Name = "buttonClearConsultationRecordClassrooms";
            this.buttonClearConsultationRecordClassrooms.Size = new System.Drawing.Size(150, 30);
            this.buttonClearConsultationRecordClassrooms.TabIndex = 3;
            this.buttonClearConsultationRecordClassrooms.Text = "Отчистить консультации";
            this.buttonClearConsultationRecordClassrooms.UseVisualStyleBackColor = true;
            this.buttonClearConsultationRecordClassrooms.Click += new System.EventHandler(this.buttonClearConsultationRecordClassrooms_Click);
            // 
            // buttonClearExaminationRecordClassrooms
            // 
            this.buttonClearExaminationRecordClassrooms.Location = new System.Drawing.Point(25, 110);
            this.buttonClearExaminationRecordClassrooms.Name = "buttonClearExaminationRecordClassrooms";
            this.buttonClearExaminationRecordClassrooms.Size = new System.Drawing.Size(150, 30);
            this.buttonClearExaminationRecordClassrooms.TabIndex = 2;
            this.buttonClearExaminationRecordClassrooms.Text = "Отчистить экзамены";
            this.buttonClearExaminationRecordClassrooms.UseVisualStyleBackColor = true;
            this.buttonClearExaminationRecordClassrooms.Click += new System.EventHandler(this.buttonClearExaminationRecordClassrooms_Click);
            // 
            // buttonClearOffsetRecordClassrooms
            // 
            this.buttonClearOffsetRecordClassrooms.Location = new System.Drawing.Point(25, 70);
            this.buttonClearOffsetRecordClassrooms.Name = "buttonClearOffsetRecordClassrooms";
            this.buttonClearOffsetRecordClassrooms.Size = new System.Drawing.Size(150, 30);
            this.buttonClearOffsetRecordClassrooms.TabIndex = 1;
            this.buttonClearOffsetRecordClassrooms.Text = "Отчистить зачеты";
            this.buttonClearOffsetRecordClassrooms.UseVisualStyleBackColor = true;
            this.buttonClearOffsetRecordClassrooms.Click += new System.EventHandler(this.buttonClearOffsetRecordClassrooms_Click);
            // 
            // buttonClearSemesterRecordClassrooms
            // 
            this.buttonClearSemesterRecordClassrooms.Location = new System.Drawing.Point(25, 30);
            this.buttonClearSemesterRecordClassrooms.Name = "buttonClearSemesterRecordClassrooms";
            this.buttonClearSemesterRecordClassrooms.Size = new System.Drawing.Size(150, 30);
            this.buttonClearSemesterRecordClassrooms.TabIndex = 0;
            this.buttonClearSemesterRecordClassrooms.Text = "Отчистить семестр";
            this.buttonClearSemesterRecordClassrooms.UseVisualStyleBackColor = true;
            this.buttonClearSemesterRecordClassrooms.Click += new System.EventHandler(this.buttonClearSemesterRecordClassrooms_Click);
            // 
            // groupBoxExport
            // 
            this.groupBoxExport.Controls.Add(this.buttonExportExaminationRecordHTML);
            this.groupBoxExport.Controls.Add(this.buttonExportExaminationRecordExcel);
            this.groupBoxExport.Controls.Add(this.buttonExportOffsetRecordHTML);
            this.groupBoxExport.Controls.Add(this.buttonExportOffsetRecordExcel);
            this.groupBoxExport.Controls.Add(this.buttonExportSemesterRecordExcel);
            this.groupBoxExport.Controls.Add(this.buttonExportSemesterRecordHTML);
            this.groupBoxExport.Location = new System.Drawing.Point(725, 126);
            this.groupBoxExport.Name = "groupBoxExport";
            this.groupBoxExport.Size = new System.Drawing.Size(200, 280);
            this.groupBoxExport.TabIndex = 3;
            this.groupBoxExport.TabStop = false;
            this.groupBoxExport.Text = "Экспорт";
            // 
            // buttonExportExaminationRecordHTML
            // 
            this.buttonExportExaminationRecordHTML.Location = new System.Drawing.Point(25, 230);
            this.buttonExportExaminationRecordHTML.Name = "buttonExportExaminationRecordHTML";
            this.buttonExportExaminationRecordHTML.Size = new System.Drawing.Size(150, 30);
            this.buttonExportExaminationRecordHTML.TabIndex = 5;
            this.buttonExportExaminationRecordHTML.Text = "Экзамены в HTML";
            this.buttonExportExaminationRecordHTML.UseVisualStyleBackColor = true;
            this.buttonExportExaminationRecordHTML.Click += new System.EventHandler(this.buttonExportExaminationRecordHTML_Click);
            // 
            // buttonExportExaminationRecordExcel
            // 
            this.buttonExportExaminationRecordExcel.Location = new System.Drawing.Point(25, 111);
            this.buttonExportExaminationRecordExcel.Name = "buttonExportExaminationRecordExcel";
            this.buttonExportExaminationRecordExcel.Size = new System.Drawing.Size(150, 30);
            this.buttonExportExaminationRecordExcel.TabIndex = 2;
            this.buttonExportExaminationRecordExcel.Text = "Экзамены в Excel";
            this.buttonExportExaminationRecordExcel.UseVisualStyleBackColor = true;
            this.buttonExportExaminationRecordExcel.Click += new System.EventHandler(this.buttonExportExaminationRecordExcel_Click);
            // 
            // buttonExportOffsetRecordHTML
            // 
            this.buttonExportOffsetRecordHTML.Location = new System.Drawing.Point(25, 190);
            this.buttonExportOffsetRecordHTML.Name = "buttonExportOffsetRecordHTML";
            this.buttonExportOffsetRecordHTML.Size = new System.Drawing.Size(150, 30);
            this.buttonExportOffsetRecordHTML.TabIndex = 4;
            this.buttonExportOffsetRecordHTML.Text = "Зачеты в HTML";
            this.buttonExportOffsetRecordHTML.UseVisualStyleBackColor = true;
            this.buttonExportOffsetRecordHTML.Click += new System.EventHandler(this.buttonExportOffsetRecordHTML_Click);
            // 
            // buttonExportOffsetRecordExcel
            // 
            this.buttonExportOffsetRecordExcel.Location = new System.Drawing.Point(25, 70);
            this.buttonExportOffsetRecordExcel.Name = "buttonExportOffsetRecordExcel";
            this.buttonExportOffsetRecordExcel.Size = new System.Drawing.Size(150, 30);
            this.buttonExportOffsetRecordExcel.TabIndex = 1;
            this.buttonExportOffsetRecordExcel.Text = "Зачеты в Excel";
            this.buttonExportOffsetRecordExcel.UseVisualStyleBackColor = true;
            this.buttonExportOffsetRecordExcel.Click += new System.EventHandler(this.buttonExportOffsetRecordExcel_Click);
            // 
            // buttonExportSemesterRecordExcel
            // 
            this.buttonExportSemesterRecordExcel.Location = new System.Drawing.Point(25, 30);
            this.buttonExportSemesterRecordExcel.Name = "buttonExportSemesterRecordExcel";
            this.buttonExportSemesterRecordExcel.Size = new System.Drawing.Size(150, 30);
            this.buttonExportSemesterRecordExcel.TabIndex = 0;
            this.buttonExportSemesterRecordExcel.Text = "Семестр в Excel";
            this.buttonExportSemesterRecordExcel.UseVisualStyleBackColor = true;
            this.buttonExportSemesterRecordExcel.Click += new System.EventHandler(this.buttonExportSemesterRecordExcel_Click);
            // 
            // buttonExportSemesterRecordHTML
            // 
            this.buttonExportSemesterRecordHTML.Location = new System.Drawing.Point(25, 150);
            this.buttonExportSemesterRecordHTML.Name = "buttonExportSemesterRecordHTML";
            this.buttonExportSemesterRecordHTML.Size = new System.Drawing.Size(150, 30);
            this.buttonExportSemesterRecordHTML.TabIndex = 3;
            this.buttonExportSemesterRecordHTML.Text = "Семестр в HTML";
            this.buttonExportSemesterRecordHTML.UseVisualStyleBackColor = true;
            this.buttonExportSemesterRecordHTML.Click += new System.EventHandler(this.buttonExportSemesterRecordHTML_Click);
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.buttonSeasonDatesSave);
            this.groupBoxSettings.Controls.Add(this.comboBoxSeasonDates);
            this.groupBoxSettings.Controls.Add(this.labelSeasonDates);
            this.groupBoxSettings.Location = new System.Drawing.Point(3, 253);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(350, 93);
            this.groupBoxSettings.TabIndex = 4;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Общие настройки";
            // 
            // buttonSeasonDatesSave
            // 
            this.buttonSeasonDatesSave.Location = new System.Drawing.Point(269, 55);
            this.buttonSeasonDatesSave.Name = "buttonSeasonDatesSave";
            this.buttonSeasonDatesSave.Size = new System.Drawing.Size(75, 30);
            this.buttonSeasonDatesSave.TabIndex = 2;
            this.buttonSeasonDatesSave.Text = "Сохранить";
            this.buttonSeasonDatesSave.UseVisualStyleBackColor = true;
            this.buttonSeasonDatesSave.Click += new System.EventHandler(this.buttonSeasonDatesSave_Click);
            // 
            // comboBoxSeasonDates
            // 
            this.comboBoxSeasonDates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSeasonDates.FormattingEnabled = true;
            this.comboBoxSeasonDates.Location = new System.Drawing.Point(111, 28);
            this.comboBoxSeasonDates.Name = "comboBoxSeasonDates";
            this.comboBoxSeasonDates.Size = new System.Drawing.Size(233, 21);
            this.comboBoxSeasonDates.TabIndex = 1;
            // 
            // labelSeasonDates
            // 
            this.labelSeasonDates.AutoSize = true;
            this.labelSeasonDates.Location = new System.Drawing.Point(15, 31);
            this.labelSeasonDates.Name = "labelSeasonDates";
            this.labelSeasonDates.Size = new System.Drawing.Size(90, 13);
            this.labelSeasonDates.TabIndex = 0;
            this.labelSeasonDates.Text = "Даты семестра:";
            // 
            // groupBoxCheckRecordsIfNotComplite
            // 
            this.groupBoxCheckRecordsIfNotComplite.Controls.Add(this.buttonCheckRecordsIfNotComplite);
            this.groupBoxCheckRecordsIfNotComplite.Location = new System.Drawing.Point(565, 106);
            this.groupBoxCheckRecordsIfNotComplite.Name = "groupBoxCheckRecordsIfNotComplite";
            this.groupBoxCheckRecordsIfNotComplite.Size = new System.Drawing.Size(154, 100);
            this.groupBoxCheckRecordsIfNotComplite.TabIndex = 5;
            this.groupBoxCheckRecordsIfNotComplite.TabStop = false;
            this.groupBoxCheckRecordsIfNotComplite.Text = "Проверка на записи семестра с неполными данными";
            // 
            // buttonCheckRecordsIfNotComplite
            // 
            this.buttonCheckRecordsIfNotComplite.Location = new System.Drawing.Point(6, 50);
            this.buttonCheckRecordsIfNotComplite.Name = "buttonCheckRecordsIfNotComplite";
            this.buttonCheckRecordsIfNotComplite.Size = new System.Drawing.Size(138, 30);
            this.buttonCheckRecordsIfNotComplite.TabIndex = 4;
            this.buttonCheckRecordsIfNotComplite.Text = "Выполнить проверку";
            this.buttonCheckRecordsIfNotComplite.UseVisualStyleBackColor = true;
            this.buttonCheckRecordsIfNotComplite.Click += new System.EventHandler(this.buttonCheckRecordsIfNotComplite_Click);
            // 
            // groupBoxImport
            // 
            this.groupBoxImport.Controls.Add(this.buttonImportExaminationFromExcel);
            this.groupBoxImport.Controls.Add(this.buttonImportOffsetFromExcel);
            this.groupBoxImport.Location = new System.Drawing.Point(725, 0);
            this.groupBoxImport.Name = "groupBoxImport";
            this.groupBoxImport.Size = new System.Drawing.Size(200, 120);
            this.groupBoxImport.TabIndex = 6;
            this.groupBoxImport.TabStop = false;
            this.groupBoxImport.Text = "Импорт";
            // 
            // buttonImportExaminationFromExcel
            // 
            this.buttonImportExaminationFromExcel.Location = new System.Drawing.Point(25, 70);
            this.buttonImportExaminationFromExcel.Name = "buttonImportExaminationFromExcel";
            this.buttonImportExaminationFromExcel.Size = new System.Drawing.Size(150, 30);
            this.buttonImportExaminationFromExcel.TabIndex = 1;
            this.buttonImportExaminationFromExcel.Text = "Экзамены из Excel";
            this.buttonImportExaminationFromExcel.UseVisualStyleBackColor = true;
            this.buttonImportExaminationFromExcel.Click += new System.EventHandler(this.buttonImportExaminationFromExcel_Click);
            // 
            // buttonImportOffsetFromExcel
            // 
            this.buttonImportOffsetFromExcel.Location = new System.Drawing.Point(25, 30);
            this.buttonImportOffsetFromExcel.Name = "buttonImportOffsetFromExcel";
            this.buttonImportOffsetFromExcel.Size = new System.Drawing.Size(150, 30);
            this.buttonImportOffsetFromExcel.TabIndex = 0;
            this.buttonImportOffsetFromExcel.Text = "Зачеты из Excel";
            this.buttonImportOffsetFromExcel.UseVisualStyleBackColor = true;
            this.buttonImportOffsetFromExcel.Click += new System.EventHandler(this.buttonImportOffsetFromExcel_Click);
            // 
            // groupBoxStudentGroups
            // 
            this.groupBoxStudentGroups.Controls.Add(this.checkedListBoxStudentGroups);
            this.groupBoxStudentGroups.Location = new System.Drawing.Point(111, 0);
            this.groupBoxStudentGroups.Name = "groupBoxStudentGroups";
            this.groupBoxStudentGroups.Size = new System.Drawing.Size(110, 250);
            this.groupBoxStudentGroups.TabIndex = 0;
            this.groupBoxStudentGroups.TabStop = false;
            this.groupBoxStudentGroups.Text = "Группы";
            // 
            // checkedListBoxStudentGroups
            // 
            this.checkedListBoxStudentGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxStudentGroups.FormattingEnabled = true;
            this.checkedListBoxStudentGroups.Location = new System.Drawing.Point(3, 16);
            this.checkedListBoxStudentGroups.Name = "checkedListBoxStudentGroups";
            this.checkedListBoxStudentGroups.Size = new System.Drawing.Size(104, 231);
            this.checkedListBoxStudentGroups.TabIndex = 0;
            // 
            // groupBoxLecturers
            // 
            this.groupBoxLecturers.Controls.Add(this.checkedListBoxLecturers);
            this.groupBoxLecturers.Location = new System.Drawing.Point(227, 0);
            this.groupBoxLecturers.Name = "groupBoxLecturers";
            this.groupBoxLecturers.Size = new System.Drawing.Size(126, 250);
            this.groupBoxLecturers.TabIndex = 0;
            this.groupBoxLecturers.TabStop = false;
            this.groupBoxLecturers.Text = "Преподаватели";
            // 
            // checkedListBoxLecturers
            // 
            this.checkedListBoxLecturers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxLecturers.FormattingEnabled = true;
            this.checkedListBoxLecturers.Location = new System.Drawing.Point(3, 16);
            this.checkedListBoxLecturers.Name = "checkedListBoxLecturers";
            this.checkedListBoxLecturers.Size = new System.Drawing.Size(120, 231);
            this.checkedListBoxLecturers.TabIndex = 0;
            // 
            // groupBoxExportInfo
            // 
            this.groupBoxExportInfo.Controls.Add(this.labelOffsetExport);
            this.groupBoxExportInfo.Location = new System.Drawing.Point(931, 0);
            this.groupBoxExportInfo.Name = "groupBoxExportInfo";
            this.groupBoxExportInfo.Size = new System.Drawing.Size(443, 406);
            this.groupBoxExportInfo.TabIndex = 7;
            this.groupBoxExportInfo.TabStop = false;
            this.groupBoxExportInfo.Text = "Информация по экспорту";
            // 
            // labelOffsetExport
            // 
            this.labelOffsetExport.AutoSize = true;
            this.labelOffsetExport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelOffsetExport.Location = new System.Drawing.Point(6, 16);
            this.labelOffsetExport.Name = "labelOffsetExport";
            this.labelOffsetExport.Size = new System.Drawing.Size(430, 80);
            this.labelOffsetExport.TabIndex = 0;
            this.labelOffsetExport.Text = resources.GetString("labelOffsetExport.Text");
            // 
            // ScheduleConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxExportInfo);
            this.Controls.Add(this.groupBoxLecturers);
            this.Controls.Add(this.groupBoxStudentGroups);
            this.Controls.Add(this.groupBoxImport);
            this.Controls.Add(this.groupBoxCheckRecordsIfNotComplite);
            this.Controls.Add(this.groupBoxSettings);
            this.Controls.Add(this.groupBoxExport);
            this.Controls.Add(this.groupBoxClearClassrooms);
            this.Controls.Add(this.groupBoxClassrooms);
            this.Controls.Add(this.groupBoxLoadHTMLScheduleForClassrooms);
            this.Name = "ScheduleConfigControl";
            this.Size = new System.Drawing.Size(1386, 500);
            this.groupBoxLoadHTMLScheduleForClassrooms.ResumeLayout(false);
            this.groupBoxLoadHTMLScheduleForClassrooms.PerformLayout();
            this.groupBoxClassrooms.ResumeLayout(false);
            this.groupBoxClearClassrooms.ResumeLayout(false);
            this.groupBoxExport.ResumeLayout(false);
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.groupBoxCheckRecordsIfNotComplite.ResumeLayout(false);
            this.groupBoxImport.ResumeLayout(false);
            this.groupBoxStudentGroups.ResumeLayout(false);
            this.groupBoxLecturers.ResumeLayout(false);
            this.groupBoxExportInfo.ResumeLayout(false);
            this.groupBoxExportInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxLoadHTMLScheduleForClassrooms;
        private System.Windows.Forms.CheckedListBox checkedListBoxClassrooms;
        private System.Windows.Forms.Button buttonMakeLoadHTMLScheduleForClassrooms;
        private System.Windows.Forms.GroupBox groupBoxClassrooms;
        private System.Windows.Forms.GroupBox groupBoxClearClassrooms;
        private System.Windows.Forms.Button buttonClearSemesterRecordClassrooms;
        private System.Windows.Forms.GroupBox groupBoxExport;
        private System.Windows.Forms.Button buttonExportSemesterRecordExcel;
        private System.Windows.Forms.Button buttonExportSemesterRecordHTML;
        private System.Windows.Forms.Label labelLinkToHtml;
        private System.Windows.Forms.TextBox textBoxLinkToHtml;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.Label labelSeasonDates;
        private System.Windows.Forms.Button buttonSeasonDatesSave;
        private System.Windows.Forms.ComboBox comboBoxSeasonDates;
        private System.Windows.Forms.GroupBox groupBoxCheckRecordsIfNotComplite;
        private System.Windows.Forms.Button buttonCheckRecordsIfNotComplite;
        private System.Windows.Forms.GroupBox groupBoxImport;
        private System.Windows.Forms.Button buttonImportOffsetFromExcel;
        private System.Windows.Forms.Button buttonClearOffsetRecordClassrooms;
        private System.Windows.Forms.Button buttonClearExaminationRecordClassrooms;
        private System.Windows.Forms.Button buttonClearConsultationRecordClassrooms;
        private System.Windows.Forms.Button buttonImportExaminationFromExcel;
        private System.Windows.Forms.Button buttonExportOffsetRecordExcel;
        private System.Windows.Forms.Button buttonExportOffsetRecordHTML;
        private System.Windows.Forms.Button buttonExportExaminationRecordExcel;
        private System.Windows.Forms.Button buttonExportExaminationRecordHTML;
        private System.Windows.Forms.GroupBox groupBoxStudentGroups;
        private System.Windows.Forms.CheckedListBox checkedListBoxStudentGroups;
        private System.Windows.Forms.GroupBox groupBoxLecturers;
        private System.Windows.Forms.CheckedListBox checkedListBoxLecturers;
        private System.Windows.Forms.CheckBox checkBoxIsFirstHalfSemester;
        private System.Windows.Forms.GroupBox groupBoxExportInfo;
        private System.Windows.Forms.Label labelOffsetExport;
    }
}
