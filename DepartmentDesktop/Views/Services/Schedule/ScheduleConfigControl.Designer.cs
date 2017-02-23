﻿namespace DepartmentDesktop.Views.Services.Schedule
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
            this.groupBoxLoadHTMLScheduleForClassrooms = new System.Windows.Forms.GroupBox();
            this.checkBoxCheckIfNotComplite = new System.Windows.Forms.CheckBox();
            this.textBoxLinkToHtml = new System.Windows.Forms.TextBox();
            this.labelLinkToHtml = new System.Windows.Forms.Label();
            this.buttonMakeLoadHTMLScheduleForClassrooms = new System.Windows.Forms.Button();
            this.checkBoxClearSchedule = new System.Windows.Forms.CheckBox();
            this.checkedListBoxClassrooms = new System.Windows.Forms.CheckedListBox();
            this.groupBoxClassrooms = new System.Windows.Forms.GroupBox();
            this.groupBoxClearClassrooms = new System.Windows.Forms.GroupBox();
            this.buttonClearSemesterRecordClassrooms = new System.Windows.Forms.Button();
            this.groupBoxExport = new System.Windows.Forms.GroupBox();
            this.buttonExportExcel = new System.Windows.Forms.Button();
            this.buttonExportHTML = new System.Windows.Forms.Button();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.buttonSeasonDatesSave = new System.Windows.Forms.Button();
            this.comboBoxSeasonDates = new System.Windows.Forms.ComboBox();
            this.labelSeasonDates = new System.Windows.Forms.Label();
            this.groupBoxCheckRecordsIfNotComplite = new System.Windows.Forms.GroupBox();
            this.buttonCheckRecordsIfNotComplite = new System.Windows.Forms.Button();
            this.groupBoxImport = new System.Windows.Forms.GroupBox();
            this.buttonImportOffsetFromExcel = new System.Windows.Forms.Button();
            this.buttonClearOffsetRecordClassrooms = new System.Windows.Forms.Button();
            this.buttonClearExaminationRecordClassrooms = new System.Windows.Forms.Button();
            this.buttonClearConsultationRecordClassrooms = new System.Windows.Forms.Button();
            this.buttonImportExaminationFromExcel = new System.Windows.Forms.Button();
            this.groupBoxLoadHTMLScheduleForClassrooms.SuspendLayout();
            this.groupBoxClassrooms.SuspendLayout();
            this.groupBoxClearClassrooms.SuspendLayout();
            this.groupBoxExport.SuspendLayout();
            this.groupBoxSettings.SuspendLayout();
            this.groupBoxCheckRecordsIfNotComplite.SuspendLayout();
            this.groupBoxImport.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxLoadHTMLScheduleForClassrooms
            // 
            this.groupBoxLoadHTMLScheduleForClassrooms.Controls.Add(this.checkBoxCheckIfNotComplite);
            this.groupBoxLoadHTMLScheduleForClassrooms.Controls.Add(this.textBoxLinkToHtml);
            this.groupBoxLoadHTMLScheduleForClassrooms.Controls.Add(this.labelLinkToHtml);
            this.groupBoxLoadHTMLScheduleForClassrooms.Controls.Add(this.buttonMakeLoadHTMLScheduleForClassrooms);
            this.groupBoxLoadHTMLScheduleForClassrooms.Controls.Add(this.checkBoxClearSchedule);
            this.groupBoxLoadHTMLScheduleForClassrooms.Location = new System.Drawing.Point(111, 0);
            this.groupBoxLoadHTMLScheduleForClassrooms.Name = "groupBoxLoadHTMLScheduleForClassrooms";
            this.groupBoxLoadHTMLScheduleForClassrooms.Size = new System.Drawing.Size(360, 165);
            this.groupBoxLoadHTMLScheduleForClassrooms.TabIndex = 1;
            this.groupBoxLoadHTMLScheduleForClassrooms.TabStop = false;
            this.groupBoxLoadHTMLScheduleForClassrooms.Text = "Обновление расписания по аудиториям";
            // 
            // checkBoxCheckIfNotComplite
            // 
            this.checkBoxCheckIfNotComplite.AutoSize = true;
            this.checkBoxCheckIfNotComplite.Location = new System.Drawing.Point(39, 95);
            this.checkBoxCheckIfNotComplite.Name = "checkBoxCheckIfNotComplite";
            this.checkBoxCheckIfNotComplite.Size = new System.Drawing.Size(296, 17);
            this.checkBoxCheckIfNotComplite.TabIndex = 6;
            this.checkBoxCheckIfNotComplite.Text = "Выполнить проверку записей с неполными данными";
            this.checkBoxCheckIfNotComplite.UseVisualStyleBackColor = true;
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
            this.buttonMakeLoadHTMLScheduleForClassrooms.Location = new System.Drawing.Point(275, 127);
            this.buttonMakeLoadHTMLScheduleForClassrooms.Name = "buttonMakeLoadHTMLScheduleForClassrooms";
            this.buttonMakeLoadHTMLScheduleForClassrooms.Size = new System.Drawing.Size(75, 30);
            this.buttonMakeLoadHTMLScheduleForClassrooms.TabIndex = 2;
            this.buttonMakeLoadHTMLScheduleForClassrooms.Text = "Обновить";
            this.buttonMakeLoadHTMLScheduleForClassrooms.UseVisualStyleBackColor = true;
            this.buttonMakeLoadHTMLScheduleForClassrooms.Click += new System.EventHandler(this.buttonMakeLoadHTMLScheduleForClassrooms_Click);
            // 
            // checkBoxClearSchedule
            // 
            this.checkBoxClearSchedule.AutoSize = true;
            this.checkBoxClearSchedule.Location = new System.Drawing.Point(39, 63);
            this.checkBoxClearSchedule.Name = "checkBoxClearSchedule";
            this.checkBoxClearSchedule.Size = new System.Drawing.Size(141, 17);
            this.checkBoxClearSchedule.TabIndex = 1;
            this.checkBoxClearSchedule.Text = "Отчистить расписание";
            this.checkBoxClearSchedule.UseVisualStyleBackColor = true;
            // 
            // checkedListBoxClassrooms
            // 
            this.checkedListBoxClassrooms.FormattingEnabled = true;
            this.checkedListBoxClassrooms.Location = new System.Drawing.Point(5, 20);
            this.checkedListBoxClassrooms.Name = "checkedListBoxClassrooms";
            this.checkedListBoxClassrooms.Size = new System.Drawing.Size(100, 139);
            this.checkedListBoxClassrooms.TabIndex = 0;
            // 
            // groupBoxClassrooms
            // 
            this.groupBoxClassrooms.Controls.Add(this.checkedListBoxClassrooms);
            this.groupBoxClassrooms.Location = new System.Drawing.Point(0, 0);
            this.groupBoxClassrooms.Name = "groupBoxClassrooms";
            this.groupBoxClassrooms.Size = new System.Drawing.Size(110, 165);
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
            this.groupBoxClearClassrooms.Location = new System.Drawing.Point(477, 106);
            this.groupBoxClearClassrooms.Name = "groupBoxClearClassrooms";
            this.groupBoxClearClassrooms.Size = new System.Drawing.Size(200, 204);
            this.groupBoxClearClassrooms.TabIndex = 2;
            this.groupBoxClearClassrooms.TabStop = false;
            this.groupBoxClearClassrooms.Text = "Отчистка аудиторий";
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
            this.groupBoxExport.Controls.Add(this.buttonExportExcel);
            this.groupBoxExport.Controls.Add(this.buttonExportHTML);
            this.groupBoxExport.Location = new System.Drawing.Point(538, 343);
            this.groupBoxExport.Name = "groupBoxExport";
            this.groupBoxExport.Size = new System.Drawing.Size(130, 123);
            this.groupBoxExport.TabIndex = 3;
            this.groupBoxExport.TabStop = false;
            this.groupBoxExport.Text = "Экспорт";
            // 
            // buttonExportExcel
            // 
            this.buttonExportExcel.Location = new System.Drawing.Point(27, 77);
            this.buttonExportExcel.Name = "buttonExportExcel";
            this.buttonExportExcel.Size = new System.Drawing.Size(75, 23);
            this.buttonExportExcel.TabIndex = 1;
            this.buttonExportExcel.Text = "Excel";
            this.buttonExportExcel.UseVisualStyleBackColor = true;
            this.buttonExportExcel.Click += new System.EventHandler(this.buttonExportExcel_Click);
            // 
            // buttonExportHTML
            // 
            this.buttonExportHTML.Location = new System.Drawing.Point(27, 36);
            this.buttonExportHTML.Name = "buttonExportHTML";
            this.buttonExportHTML.Size = new System.Drawing.Size(75, 23);
            this.buttonExportHTML.TabIndex = 0;
            this.buttonExportHTML.Text = "HTML";
            this.buttonExportHTML.UseVisualStyleBackColor = true;
            this.buttonExportHTML.Click += new System.EventHandler(this.buttonExportHTML_Click);
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.buttonSeasonDatesSave);
            this.groupBoxSettings.Controls.Add(this.comboBoxSeasonDates);
            this.groupBoxSettings.Controls.Add(this.labelSeasonDates);
            this.groupBoxSettings.Location = new System.Drawing.Point(111, 171);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(360, 70);
            this.groupBoxSettings.TabIndex = 4;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Общие настройки";
            // 
            // buttonSeasonDatesSave
            // 
            this.buttonSeasonDatesSave.Location = new System.Drawing.Point(275, 22);
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
            this.comboBoxSeasonDates.Size = new System.Drawing.Size(160, 21);
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
            this.groupBoxCheckRecordsIfNotComplite.Location = new System.Drawing.Point(477, 0);
            this.groupBoxCheckRecordsIfNotComplite.Name = "groupBoxCheckRecordsIfNotComplite";
            this.groupBoxCheckRecordsIfNotComplite.Size = new System.Drawing.Size(200, 100);
            this.groupBoxCheckRecordsIfNotComplite.TabIndex = 5;
            this.groupBoxCheckRecordsIfNotComplite.TabStop = false;
            this.groupBoxCheckRecordsIfNotComplite.Text = "Проверка на записи семестра с неполными данными";
            // 
            // buttonCheckRecordsIfNotComplite
            // 
            this.buttonCheckRecordsIfNotComplite.Location = new System.Drawing.Point(25, 50);
            this.buttonCheckRecordsIfNotComplite.Name = "buttonCheckRecordsIfNotComplite";
            this.buttonCheckRecordsIfNotComplite.Size = new System.Drawing.Size(150, 30);
            this.buttonCheckRecordsIfNotComplite.TabIndex = 4;
            this.buttonCheckRecordsIfNotComplite.Text = "Выполнить проверку";
            this.buttonCheckRecordsIfNotComplite.UseVisualStyleBackColor = true;
            this.buttonCheckRecordsIfNotComplite.Click += new System.EventHandler(this.buttonCheckRecordsIfNotComplite_Click);
            // 
            // groupBoxImport
            // 
            this.groupBoxImport.Controls.Add(this.buttonImportExaminationFromExcel);
            this.groupBoxImport.Controls.Add(this.buttonImportOffsetFromExcel);
            this.groupBoxImport.Location = new System.Drawing.Point(683, 0);
            this.groupBoxImport.Name = "groupBoxImport";
            this.groupBoxImport.Size = new System.Drawing.Size(200, 150);
            this.groupBoxImport.TabIndex = 6;
            this.groupBoxImport.TabStop = false;
            this.groupBoxImport.Text = "Импорт";
            // 
            // buttonImportOffsetFromExcel
            // 
            this.buttonImportOffsetFromExcel.Location = new System.Drawing.Point(20, 30);
            this.buttonImportOffsetFromExcel.Name = "buttonImportOffsetFromExcel";
            this.buttonImportOffsetFromExcel.Size = new System.Drawing.Size(150, 30);
            this.buttonImportOffsetFromExcel.TabIndex = 0;
            this.buttonImportOffsetFromExcel.Text = "Зачеты из Excel";
            this.buttonImportOffsetFromExcel.UseVisualStyleBackColor = true;
            this.buttonImportOffsetFromExcel.Click += new System.EventHandler(this.buttonImportOffsetFromExcel_Click);
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
            // buttonImportExaminationFromExcel
            // 
            this.buttonImportExaminationFromExcel.Location = new System.Drawing.Point(20, 70);
            this.buttonImportExaminationFromExcel.Name = "buttonImportExaminationFromExcel";
            this.buttonImportExaminationFromExcel.Size = new System.Drawing.Size(150, 30);
            this.buttonImportExaminationFromExcel.TabIndex = 1;
            this.buttonImportExaminationFromExcel.Text = "Экзамены из Excel";
            this.buttonImportExaminationFromExcel.UseVisualStyleBackColor = true;
            this.buttonImportExaminationFromExcel.Click += new System.EventHandler(this.buttonImportExaminationFromExcel_Click);
            // 
            // ScheduleConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxImport);
            this.Controls.Add(this.groupBoxCheckRecordsIfNotComplite);
            this.Controls.Add(this.groupBoxSettings);
            this.Controls.Add(this.groupBoxExport);
            this.Controls.Add(this.groupBoxClearClassrooms);
            this.Controls.Add(this.groupBoxClassrooms);
            this.Controls.Add(this.groupBoxLoadHTMLScheduleForClassrooms);
            this.Name = "ScheduleConfigControl";
            this.Size = new System.Drawing.Size(993, 500);
            this.groupBoxLoadHTMLScheduleForClassrooms.ResumeLayout(false);
            this.groupBoxLoadHTMLScheduleForClassrooms.PerformLayout();
            this.groupBoxClassrooms.ResumeLayout(false);
            this.groupBoxClearClassrooms.ResumeLayout(false);
            this.groupBoxExport.ResumeLayout(false);
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.groupBoxCheckRecordsIfNotComplite.ResumeLayout(false);
            this.groupBoxImport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxLoadHTMLScheduleForClassrooms;
        private System.Windows.Forms.CheckBox checkBoxClearSchedule;
        private System.Windows.Forms.CheckedListBox checkedListBoxClassrooms;
        private System.Windows.Forms.Button buttonMakeLoadHTMLScheduleForClassrooms;
        private System.Windows.Forms.GroupBox groupBoxClassrooms;
        private System.Windows.Forms.GroupBox groupBoxClearClassrooms;
        private System.Windows.Forms.Button buttonClearSemesterRecordClassrooms;
        private System.Windows.Forms.GroupBox groupBoxExport;
        private System.Windows.Forms.Button buttonExportExcel;
        private System.Windows.Forms.Button buttonExportHTML;
        private System.Windows.Forms.Label labelLinkToHtml;
        private System.Windows.Forms.TextBox textBoxLinkToHtml;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.Label labelSeasonDates;
        private System.Windows.Forms.Button buttonSeasonDatesSave;
        private System.Windows.Forms.ComboBox comboBoxSeasonDates;
        private System.Windows.Forms.CheckBox checkBoxCheckIfNotComplite;
        private System.Windows.Forms.GroupBox groupBoxCheckRecordsIfNotComplite;
        private System.Windows.Forms.Button buttonCheckRecordsIfNotComplite;
        private System.Windows.Forms.GroupBox groupBoxImport;
        private System.Windows.Forms.Button buttonImportOffsetFromExcel;
        private System.Windows.Forms.Button buttonClearOffsetRecordClassrooms;
        private System.Windows.Forms.Button buttonClearExaminationRecordClassrooms;
        private System.Windows.Forms.Button buttonClearConsultationRecordClassrooms;
        private System.Windows.Forms.Button buttonImportExaminationFromExcel;
    }
}
