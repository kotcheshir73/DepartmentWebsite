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
            this.buttonClearConsultationRecordClassrooms = new System.Windows.Forms.Button();
            this.buttonClearExaminationRecordClassrooms = new System.Windows.Forms.Button();
            this.buttonClearOffsetRecordClassrooms = new System.Windows.Forms.Button();
            this.buttonClearSemesterRecordClassrooms = new System.Windows.Forms.Button();
            this.groupBoxImport = new System.Windows.Forms.GroupBox();
            this.buttonImportSemesterFromSite = new System.Windows.Forms.Button();
            this.comboBoxStartPeriodDate = new System.Windows.Forms.ComboBox();
            this.labelStartSemesterPeriodDate = new System.Windows.Forms.Label();
            this.buttonImportExaminationFromExcel = new System.Windows.Forms.Button();
            this.buttonImportOffsetFromExcel = new System.Windows.Forms.Button();
            this.groupBoxExportInfo = new System.Windows.Forms.GroupBox();
            this.labelOffsetExport = new System.Windows.Forms.Label();
            this.labelStartOffsetsDate = new System.Windows.Forms.Label();
            this.dateTimePickerOffsetStart = new System.Windows.Forms.DateTimePicker();
            this.groupBoxClearClassrooms.SuspendLayout();
            this.groupBoxImport.SuspendLayout();
            this.groupBoxExportInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxClearClassrooms
            // 
            this.groupBoxClearClassrooms.Controls.Add(this.buttonClearConsultationRecordClassrooms);
            this.groupBoxClearClassrooms.Controls.Add(this.buttonClearExaminationRecordClassrooms);
            this.groupBoxClearClassrooms.Controls.Add(this.buttonClearOffsetRecordClassrooms);
            this.groupBoxClearClassrooms.Controls.Add(this.buttonClearSemesterRecordClassrooms);
            this.groupBoxClearClassrooms.Location = new System.Drawing.Point(209, 5);
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
            this.buttonClearConsultationRecordClassrooms.Click += new System.EventHandler(this.ButtonClearConsultationRecordClassrooms_Click);
            // 
            // buttonClearExaminationRecordClassrooms
            // 
            this.buttonClearExaminationRecordClassrooms.Location = new System.Drawing.Point(25, 110);
            this.buttonClearExaminationRecordClassrooms.Name = "buttonClearExaminationRecordClassrooms";
            this.buttonClearExaminationRecordClassrooms.Size = new System.Drawing.Size(150, 30);
            this.buttonClearExaminationRecordClassrooms.TabIndex = 2;
            this.buttonClearExaminationRecordClassrooms.Text = "Отчистить экзамены";
            this.buttonClearExaminationRecordClassrooms.UseVisualStyleBackColor = true;
            this.buttonClearExaminationRecordClassrooms.Click += new System.EventHandler(this.ButtonClearExaminationRecordClassrooms_Click);
            // 
            // buttonClearOffsetRecordClassrooms
            // 
            this.buttonClearOffsetRecordClassrooms.Location = new System.Drawing.Point(25, 70);
            this.buttonClearOffsetRecordClassrooms.Name = "buttonClearOffsetRecordClassrooms";
            this.buttonClearOffsetRecordClassrooms.Size = new System.Drawing.Size(150, 30);
            this.buttonClearOffsetRecordClassrooms.TabIndex = 1;
            this.buttonClearOffsetRecordClassrooms.Text = "Отчистить зачеты";
            this.buttonClearOffsetRecordClassrooms.UseVisualStyleBackColor = true;
            this.buttonClearOffsetRecordClassrooms.Click += new System.EventHandler(this.ButtonClearOffsetRecordClassrooms_Click);
            // 
            // buttonClearSemesterRecordClassrooms
            // 
            this.buttonClearSemesterRecordClassrooms.Location = new System.Drawing.Point(25, 30);
            this.buttonClearSemesterRecordClassrooms.Name = "buttonClearSemesterRecordClassrooms";
            this.buttonClearSemesterRecordClassrooms.Size = new System.Drawing.Size(150, 30);
            this.buttonClearSemesterRecordClassrooms.TabIndex = 0;
            this.buttonClearSemesterRecordClassrooms.Text = "Отчистить семестр";
            this.buttonClearSemesterRecordClassrooms.UseVisualStyleBackColor = true;
            this.buttonClearSemesterRecordClassrooms.Click += new System.EventHandler(this.ButtonClearSemesterRecordClassrooms_Click);
            // 
            // groupBoxImport
            // 
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
            this.groupBoxImport.TabIndex = 6;
            this.groupBoxImport.TabStop = false;
            this.groupBoxImport.Text = "Импорт";
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
            this.buttonImportExaminationFromExcel.Location = new System.Drawing.Point(35, 206);
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
            // labelStartOffsetsDate
            // 
            this.labelStartOffsetsDate.AutoSize = true;
            this.labelStartOffsetsDate.Location = new System.Drawing.Point(18, 112);
            this.labelStartOffsetsDate.Name = "labelStartOffsetsDate";
            this.labelStartOffsetsDate.Size = new System.Drawing.Size(117, 13);
            this.labelStartOffsetsDate.TabIndex = 3;
            this.labelStartOffsetsDate.Text = "Дата начала зачетов:";
            // 
            // dateTimePickerOffsetStart
            // 
            this.dateTimePickerOffsetStart.Location = new System.Drawing.Point(35, 128);
            this.dateTimePickerOffsetStart.Name = "dateTimePickerOffsetStart";
            this.dateTimePickerOffsetStart.Size = new System.Drawing.Size(150, 20);
            this.dateTimePickerOffsetStart.TabIndex = 4;
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
            this.groupBoxImport.ResumeLayout(false);
            this.groupBoxImport.PerformLayout();
            this.groupBoxExportInfo.ResumeLayout(false);
            this.groupBoxExportInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxClearClassrooms;
        private System.Windows.Forms.Button buttonClearSemesterRecordClassrooms;
        private System.Windows.Forms.GroupBox groupBoxImport;
        private System.Windows.Forms.Button buttonImportOffsetFromExcel;
        private System.Windows.Forms.Button buttonClearOffsetRecordClassrooms;
        private System.Windows.Forms.Button buttonClearExaminationRecordClassrooms;
        private System.Windows.Forms.Button buttonClearConsultationRecordClassrooms;
        private System.Windows.Forms.Button buttonImportExaminationFromExcel;
        private System.Windows.Forms.GroupBox groupBoxExportInfo;
        private System.Windows.Forms.Label labelOffsetExport;
        private System.Windows.Forms.Button buttonImportSemesterFromSite;
        private System.Windows.Forms.ComboBox comboBoxStartPeriodDate;
        private System.Windows.Forms.Label labelStartSemesterPeriodDate;
        private System.Windows.Forms.Label labelStartOffsetsDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerOffsetStart;
    }
}
