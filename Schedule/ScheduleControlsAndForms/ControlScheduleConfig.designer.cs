namespace ScheduleControlsAndForms
{
    partial class ControlScheduleConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlScheduleConfig));
            this.groupBoxClearClassrooms = new System.Windows.Forms.GroupBox();
            this.buttonClearConsultationRecord = new System.Windows.Forms.Button();
            this.buttonClearExaminationRecord = new System.Windows.Forms.Button();
            this.buttonClearOffsetRecord = new System.Windows.Forms.Button();
            this.buttonClearSemesterRecord = new System.Windows.Forms.Button();
            this.groupBoxImport = new System.Windows.Forms.GroupBox();
            this.dateTimePickerExamStart = new System.Windows.Forms.DateTimePicker();
            this.labelStartExamsDate = new System.Windows.Forms.Label();
            this.dateTimePickerOffsetStart = new System.Windows.Forms.DateTimePicker();
            this.labelStartOffsetsDate = new System.Windows.Forms.Label();
            this.buttonImportSemesterFromSite = new System.Windows.Forms.Button();
            this.comboBoxStartPeriodDate = new System.Windows.Forms.ComboBox();
            this.labelStartSemesterPeriodDate = new System.Windows.Forms.Label();
            this.buttonImportExaminationFromExcel = new System.Windows.Forms.Button();
            this.buttonImportOffsetFromExcel = new System.Windows.Forms.Button();
            this.groupBoxExportInfo = new System.Windows.Forms.GroupBox();
            this.labelExamImport = new System.Windows.Forms.Label();
            this.labelOffsetImport = new System.Windows.Forms.Label();
            this.labelPeriod = new System.Windows.Forms.Label();
            this.dateTimePickerFromClear = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerToClear = new System.Windows.Forms.DateTimePicker();
            this.groupBoxClearClassrooms.SuspendLayout();
            this.groupBoxImport.SuspendLayout();
            this.groupBoxExportInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxClearClassrooms
            // 
            this.groupBoxClearClassrooms.Controls.Add(this.dateTimePickerToClear);
            this.groupBoxClearClassrooms.Controls.Add(this.dateTimePickerFromClear);
            this.groupBoxClearClassrooms.Controls.Add(this.labelPeriod);
            this.groupBoxClearClassrooms.Controls.Add(this.buttonClearConsultationRecord);
            this.groupBoxClearClassrooms.Controls.Add(this.buttonClearExaminationRecord);
            this.groupBoxClearClassrooms.Controls.Add(this.buttonClearOffsetRecord);
            this.groupBoxClearClassrooms.Controls.Add(this.buttonClearSemesterRecord);
            this.groupBoxClearClassrooms.Location = new System.Drawing.Point(650, 3);
            this.groupBoxClearClassrooms.Name = "groupBoxClearClassrooms";
            this.groupBoxClearClassrooms.Size = new System.Drawing.Size(200, 265);
            this.groupBoxClearClassrooms.TabIndex = 2;
            this.groupBoxClearClassrooms.TabStop = false;
            this.groupBoxClearClassrooms.Text = "Отчистка аудиторий";
            // 
            // buttonClearConsultationRecord
            // 
            this.buttonClearConsultationRecord.Location = new System.Drawing.Point(27, 223);
            this.buttonClearConsultationRecord.Name = "buttonClearConsultationRecord";
            this.buttonClearConsultationRecord.Size = new System.Drawing.Size(150, 30);
            this.buttonClearConsultationRecord.TabIndex = 6;
            this.buttonClearConsultationRecord.Text = "Отчистить консультации";
            this.buttonClearConsultationRecord.UseVisualStyleBackColor = true;
            this.buttonClearConsultationRecord.Click += new System.EventHandler(this.ButtonClearConsultationRecord_Click);
            // 
            // buttonClearExaminationRecord
            // 
            this.buttonClearExaminationRecord.Location = new System.Drawing.Point(27, 183);
            this.buttonClearExaminationRecord.Name = "buttonClearExaminationRecord";
            this.buttonClearExaminationRecord.Size = new System.Drawing.Size(150, 30);
            this.buttonClearExaminationRecord.TabIndex = 5;
            this.buttonClearExaminationRecord.Text = "Отчистить экзамены";
            this.buttonClearExaminationRecord.UseVisualStyleBackColor = true;
            this.buttonClearExaminationRecord.Click += new System.EventHandler(this.ButtonClearExaminationRecord_Click);
            // 
            // buttonClearOffsetRecord
            // 
            this.buttonClearOffsetRecord.Location = new System.Drawing.Point(27, 143);
            this.buttonClearOffsetRecord.Name = "buttonClearOffsetRecord";
            this.buttonClearOffsetRecord.Size = new System.Drawing.Size(150, 30);
            this.buttonClearOffsetRecord.TabIndex = 4;
            this.buttonClearOffsetRecord.Text = "Отчистить зачеты";
            this.buttonClearOffsetRecord.UseVisualStyleBackColor = true;
            this.buttonClearOffsetRecord.Click += new System.EventHandler(this.ButtonClearOffsetRecord_Click);
            // 
            // buttonClearSemesterRecord
            // 
            this.buttonClearSemesterRecord.Location = new System.Drawing.Point(27, 103);
            this.buttonClearSemesterRecord.Name = "buttonClearSemesterRecord";
            this.buttonClearSemesterRecord.Size = new System.Drawing.Size(150, 30);
            this.buttonClearSemesterRecord.TabIndex = 3;
            this.buttonClearSemesterRecord.Text = "Отчистить семестр";
            this.buttonClearSemesterRecord.UseVisualStyleBackColor = true;
            this.buttonClearSemesterRecord.Click += new System.EventHandler(this.ButtonClearSemesterRecord_Click);
            // 
            // groupBoxImport
            // 
            this.groupBoxImport.Controls.Add(this.dateTimePickerExamStart);
            this.groupBoxImport.Controls.Add(this.labelStartExamsDate);
            this.groupBoxImport.Controls.Add(this.dateTimePickerOffsetStart);
            this.groupBoxImport.Controls.Add(this.labelStartOffsetsDate);
            this.groupBoxImport.Controls.Add(this.buttonImportSemesterFromSite);
            this.groupBoxImport.Controls.Add(this.comboBoxStartPeriodDate);
            this.groupBoxImport.Controls.Add(this.labelStartSemesterPeriodDate);
            this.groupBoxImport.Controls.Add(this.buttonImportExaminationFromExcel);
            this.groupBoxImport.Controls.Add(this.buttonImportOffsetFromExcel);
            this.groupBoxImport.Location = new System.Drawing.Point(3, 3);
            this.groupBoxImport.Name = "groupBoxImport";
            this.groupBoxImport.Size = new System.Drawing.Size(200, 282);
            this.groupBoxImport.TabIndex = 0;
            this.groupBoxImport.TabStop = false;
            this.groupBoxImport.Text = "Импорт";
            // 
            // dateTimePickerExamStart
            // 
            this.dateTimePickerExamStart.Location = new System.Drawing.Point(35, 209);
            this.dateTimePickerExamStart.Name = "dateTimePickerExamStart";
            this.dateTimePickerExamStart.Size = new System.Drawing.Size(150, 20);
            this.dateTimePickerExamStart.TabIndex = 7;
            // 
            // labelStartExamsDate
            // 
            this.labelStartExamsDate.AutoSize = true;
            this.labelStartExamsDate.Location = new System.Drawing.Point(18, 193);
            this.labelStartExamsDate.Name = "labelStartExamsDate";
            this.labelStartExamsDate.Size = new System.Drawing.Size(117, 13);
            this.labelStartExamsDate.TabIndex = 6;
            this.labelStartExamsDate.Text = "Дата начала зачетов:";
            // 
            // dateTimePickerOffsetStart
            // 
            this.dateTimePickerOffsetStart.Location = new System.Drawing.Point(35, 128);
            this.dateTimePickerOffsetStart.Name = "dateTimePickerOffsetStart";
            this.dateTimePickerOffsetStart.Size = new System.Drawing.Size(150, 20);
            this.dateTimePickerOffsetStart.TabIndex = 4;
            // 
            // labelStartOffsetsDate
            // 
            this.labelStartOffsetsDate.AutoSize = true;
            this.labelStartOffsetsDate.Location = new System.Drawing.Point(18, 112);
            this.labelStartOffsetsDate.Name = "labelStartOffsetsDate";
            this.labelStartOffsetsDate.Size = new System.Drawing.Size(117, 13);
            this.labelStartOffsetsDate.TabIndex = 3;
            this.labelStartOffsetsDate.Text = "Дата начала зачетов:";
            // 
            // buttonImportSemesterFromSite
            // 
            this.buttonImportSemesterFromSite.Location = new System.Drawing.Point(35, 71);
            this.buttonImportSemesterFromSite.Name = "buttonImportSemesterFromSite";
            this.buttonImportSemesterFromSite.Size = new System.Drawing.Size(150, 30);
            this.buttonImportSemesterFromSite.TabIndex = 2;
            this.buttonImportSemesterFromSite.Text = "Семестр с сайта";
            this.buttonImportSemesterFromSite.UseVisualStyleBackColor = true;
            this.buttonImportSemesterFromSite.Click += new System.EventHandler(this.ButtonImportSemesterFromSite_Click);
            // 
            // comboBoxStartPeriodDate
            // 
            this.comboBoxStartPeriodDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartPeriodDate.FormattingEnabled = true;
            this.comboBoxStartPeriodDate.Location = new System.Drawing.Point(35, 44);
            this.comboBoxStartPeriodDate.Name = "comboBoxStartPeriodDate";
            this.comboBoxStartPeriodDate.Size = new System.Drawing.Size(150, 21);
            this.comboBoxStartPeriodDate.TabIndex = 1;
            // 
            // labelStartSemesterPeriodDate
            // 
            this.labelStartSemesterPeriodDate.AutoSize = true;
            this.labelStartSemesterPeriodDate.Location = new System.Drawing.Point(18, 28);
            this.labelStartSemesterPeriodDate.Name = "labelStartSemesterPeriodDate";
            this.labelStartSemesterPeriodDate.Size = new System.Drawing.Size(119, 13);
            this.labelStartSemesterPeriodDate.TabIndex = 0;
            this.labelStartSemesterPeriodDate.Text = "Дата начала периода:";
            // 
            // buttonImportExaminationFromExcel
            // 
            this.buttonImportExaminationFromExcel.Location = new System.Drawing.Point(35, 235);
            this.buttonImportExaminationFromExcel.Name = "buttonImportExaminationFromExcel";
            this.buttonImportExaminationFromExcel.Size = new System.Drawing.Size(150, 30);
            this.buttonImportExaminationFromExcel.TabIndex = 4;
            this.buttonImportExaminationFromExcel.Text = "Экзамены из Excel";
            this.buttonImportExaminationFromExcel.UseVisualStyleBackColor = true;
            this.buttonImportExaminationFromExcel.Click += new System.EventHandler(this.ButtonImportExaminationFromExcel_Click);
            // 
            // buttonImportOffsetFromExcel
            // 
            this.buttonImportOffsetFromExcel.Location = new System.Drawing.Point(35, 154);
            this.buttonImportOffsetFromExcel.Name = "buttonImportOffsetFromExcel";
            this.buttonImportOffsetFromExcel.Size = new System.Drawing.Size(150, 30);
            this.buttonImportOffsetFromExcel.TabIndex = 5;
            this.buttonImportOffsetFromExcel.Text = "Зачеты из Excel";
            this.buttonImportOffsetFromExcel.UseVisualStyleBackColor = true;
            this.buttonImportOffsetFromExcel.Click += new System.EventHandler(this.ButtonImportOffsetFromExcel_Click);
            // 
            // groupBoxExportInfo
            // 
            this.groupBoxExportInfo.Controls.Add(this.labelExamImport);
            this.groupBoxExportInfo.Controls.Add(this.labelOffsetImport);
            this.groupBoxExportInfo.Location = new System.Drawing.Point(209, 3);
            this.groupBoxExportInfo.Name = "groupBoxExportInfo";
            this.groupBoxExportInfo.Size = new System.Drawing.Size(435, 165);
            this.groupBoxExportInfo.TabIndex = 1;
            this.groupBoxExportInfo.TabStop = false;
            this.groupBoxExportInfo.Text = "Информация по экспорту";
            // 
            // labelExamImport
            // 
            this.labelExamImport.AutoSize = true;
            this.labelExamImport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelExamImport.Location = new System.Drawing.Point(6, 92);
            this.labelExamImport.Name = "labelExamImport";
            this.labelExamImport.Size = new System.Drawing.Size(423, 67);
            this.labelExamImport.TabIndex = 1;
            this.labelExamImport.Text = resources.GetString("labelExamImport.Text");
            // 
            // labelOffsetImport
            // 
            this.labelOffsetImport.AutoSize = true;
            this.labelOffsetImport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelOffsetImport.Location = new System.Drawing.Point(6, 16);
            this.labelOffsetImport.Name = "labelOffsetImport";
            this.labelOffsetImport.Size = new System.Drawing.Size(361, 67);
            this.labelOffsetImport.TabIndex = 0;
            this.labelOffsetImport.Text = resources.GetString("labelOffsetImport.Text");
            // 
            // labelPeriod
            // 
            this.labelPeriod.AutoSize = true;
            this.labelPeriod.Location = new System.Drawing.Point(24, 23);
            this.labelPeriod.Name = "labelPeriod";
            this.labelPeriod.Size = new System.Drawing.Size(48, 13);
            this.labelPeriod.TabIndex = 0;
            this.labelPeriod.Text = "Период:";
            // 
            // dateTimePickerFromClear
            // 
            this.dateTimePickerFromClear.Location = new System.Drawing.Point(27, 41);
            this.dateTimePickerFromClear.Name = "dateTimePickerFromClear";
            this.dateTimePickerFromClear.Size = new System.Drawing.Size(150, 20);
            this.dateTimePickerFromClear.TabIndex = 1;
            // 
            // dateTimePickerToClear
            // 
            this.dateTimePickerToClear.Location = new System.Drawing.Point(27, 67);
            this.dateTimePickerToClear.Name = "dateTimePickerToClear";
            this.dateTimePickerToClear.Size = new System.Drawing.Size(150, 20);
            this.dateTimePickerToClear.TabIndex = 2;
            // 
            // ControlScheduleConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxExportInfo);
            this.Controls.Add(this.groupBoxImport);
            this.Controls.Add(this.groupBoxClearClassrooms);
            this.Name = "ControlScheduleConfig";
            this.Size = new System.Drawing.Size(1386, 500);
            this.groupBoxClearClassrooms.ResumeLayout(false);
            this.groupBoxClearClassrooms.PerformLayout();
            this.groupBoxImport.ResumeLayout(false);
            this.groupBoxImport.PerformLayout();
            this.groupBoxExportInfo.ResumeLayout(false);
            this.groupBoxExportInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxClearClassrooms;
        private System.Windows.Forms.Button buttonClearSemesterRecord;
        private System.Windows.Forms.GroupBox groupBoxImport;
        private System.Windows.Forms.Button buttonImportOffsetFromExcel;
        private System.Windows.Forms.Button buttonClearOffsetRecord;
        private System.Windows.Forms.Button buttonClearExaminationRecord;
        private System.Windows.Forms.Button buttonClearConsultationRecord;
        private System.Windows.Forms.Button buttonImportExaminationFromExcel;
        private System.Windows.Forms.GroupBox groupBoxExportInfo;
        private System.Windows.Forms.Label labelOffsetImport;
        private System.Windows.Forms.Button buttonImportSemesterFromSite;
        private System.Windows.Forms.ComboBox comboBoxStartPeriodDate;
        private System.Windows.Forms.Label labelStartSemesterPeriodDate;
        private System.Windows.Forms.Label labelStartOffsetsDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerOffsetStart;
        private System.Windows.Forms.DateTimePicker dateTimePickerExamStart;
        private System.Windows.Forms.Label labelStartExamsDate;
        private System.Windows.Forms.Label labelExamImport;
        private System.Windows.Forms.DateTimePicker dateTimePickerToClear;
        private System.Windows.Forms.DateTimePicker dateTimePickerFromClear;
        private System.Windows.Forms.Label labelPeriod;
    }
}
