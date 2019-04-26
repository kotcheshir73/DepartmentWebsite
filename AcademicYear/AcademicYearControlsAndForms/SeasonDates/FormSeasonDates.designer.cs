namespace AcademicYearControlsAndForms.SeasonDates
{
    partial class FormSeasonDates
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.dateTimePickerDateEndPractic = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerDateBeginPractic = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerDateEndExamination = new System.Windows.Forms.DateTimePicker();
            this.labelDateEndExamination = new System.Windows.Forms.Label();
            this.dateTimePickerDateBeginExamination = new System.Windows.Forms.DateTimePicker();
            this.labelDateBeginExamination = new System.Windows.Forms.Label();
            this.dateTimePickerDateEndOffset = new System.Windows.Forms.DateTimePicker();
            this.labelDateEndOffset = new System.Windows.Forms.Label();
            this.dateTimePickerDateBeginOffset = new System.Windows.Forms.DateTimePicker();
            this.labelDateBeginOffset = new System.Windows.Forms.Label();
            this.dateTimePickerDateEndFirstHalfSemester = new System.Windows.Forms.DateTimePicker();
            this.labelDateEndFirstHalfSemester = new System.Windows.Forms.Label();
            this.dateTimePickerDateBeginFirstHalfSemester = new System.Windows.Forms.DateTimePicker();
            this.labelDateBeginFirstHalfSemester = new System.Windows.Forms.Label();
            this.checkBoxDateBeginPractic = new System.Windows.Forms.CheckBox();
            this.checkBoxDateEndPractic = new System.Windows.Forms.CheckBox();
            this.dateTimePickerDateEndSecondHalfSemester = new System.Windows.Forms.DateTimePicker();
            this.labelDateEndSecondHalfSemester = new System.Windows.Forms.Label();
            this.dateTimePickerDateBeginSecondHalfSemester = new System.Windows.Forms.DateTimePicker();
            this.labelDateBeginSecondHalfSemester = new System.Windows.Forms.Label();
            this.comboBoxAcademicYear = new System.Windows.Forms.ComboBox();
            this.labelAcademicYear = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.comboBoxAcademicYear);
            this.panelMain.Controls.Add(this.labelAcademicYear);
            this.panelMain.Controls.Add(this.labelTitle);
            this.panelMain.Controls.Add(this.dateTimePickerDateEndSecondHalfSemester);
            this.panelMain.Controls.Add(this.textBoxTitle);
            this.panelMain.Controls.Add(this.labelDateEndSecondHalfSemester);
            this.panelMain.Controls.Add(this.labelDateBeginFirstHalfSemester);
            this.panelMain.Controls.Add(this.dateTimePickerDateBeginSecondHalfSemester);
            this.panelMain.Controls.Add(this.dateTimePickerDateBeginFirstHalfSemester);
            this.panelMain.Controls.Add(this.labelDateBeginSecondHalfSemester);
            this.panelMain.Controls.Add(this.labelDateEndFirstHalfSemester);
            this.panelMain.Controls.Add(this.checkBoxDateEndPractic);
            this.panelMain.Controls.Add(this.dateTimePickerDateEndFirstHalfSemester);
            this.panelMain.Controls.Add(this.checkBoxDateBeginPractic);
            this.panelMain.Controls.Add(this.labelDateBeginOffset);
            this.panelMain.Controls.Add(this.dateTimePickerDateEndPractic);
            this.panelMain.Controls.Add(this.dateTimePickerDateBeginOffset);
            this.panelMain.Controls.Add(this.dateTimePickerDateBeginPractic);
            this.panelMain.Controls.Add(this.labelDateEndOffset);
            this.panelMain.Controls.Add(this.dateTimePickerDateEndExamination);
            this.panelMain.Controls.Add(this.dateTimePickerDateEndOffset);
            this.panelMain.Controls.Add(this.labelDateEndExamination);
            this.panelMain.Controls.Add(this.labelDateBeginExamination);
            this.panelMain.Controls.Add(this.dateTimePickerDateBeginExamination);
            this.panelMain.Size = new System.Drawing.Size(374, 412);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(374, 36);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(12, 41);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(60, 13);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "Название:";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(100, 38);
            this.textBoxTitle.MaxLength = 150;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(222, 20);
            this.textBoxTitle.TabIndex = 3;
            // 
            // dateTimePickerDateEndPractic
            // 
            this.dateTimePickerDateEndPractic.Location = new System.Drawing.Point(218, 387);
            this.dateTimePickerDateEndPractic.Name = "dateTimePickerDateEndPractic";
            this.dateTimePickerDateEndPractic.Size = new System.Drawing.Size(140, 20);
            this.dateTimePickerDateEndPractic.TabIndex = 23;
            this.dateTimePickerDateEndPractic.Tag = "0_6";
            // 
            // dateTimePickerDateBeginPractic
            // 
            this.dateTimePickerDateBeginPractic.Location = new System.Drawing.Point(218, 352);
            this.dateTimePickerDateBeginPractic.Name = "dateTimePickerDateBeginPractic";
            this.dateTimePickerDateBeginPractic.Size = new System.Drawing.Size(140, 20);
            this.dateTimePickerDateBeginPractic.TabIndex = 21;
            this.dateTimePickerDateBeginPractic.Tag = "0_6";
            // 
            // dateTimePickerDateEndExamination
            // 
            this.dateTimePickerDateEndExamination.Location = new System.Drawing.Point(218, 317);
            this.dateTimePickerDateEndExamination.Name = "dateTimePickerDateEndExamination";
            this.dateTimePickerDateEndExamination.Size = new System.Drawing.Size(140, 20);
            this.dateTimePickerDateEndExamination.TabIndex = 19;
            this.dateTimePickerDateEndExamination.Tag = "0_6";
            // 
            // labelDateEndExamination
            // 
            this.labelDateEndExamination.AutoSize = true;
            this.labelDateEndExamination.Location = new System.Drawing.Point(12, 322);
            this.labelDateEndExamination.Name = "labelDateEndExamination";
            this.labelDateEndExamination.Size = new System.Drawing.Size(148, 13);
            this.labelDateEndExamination.TabIndex = 18;
            this.labelDateEndExamination.Text = "Дата окончания экзаменов";
            // 
            // dateTimePickerDateBeginExamination
            // 
            this.dateTimePickerDateBeginExamination.Location = new System.Drawing.Point(218, 282);
            this.dateTimePickerDateBeginExamination.Name = "dateTimePickerDateBeginExamination";
            this.dateTimePickerDateBeginExamination.Size = new System.Drawing.Size(140, 20);
            this.dateTimePickerDateBeginExamination.TabIndex = 17;
            this.dateTimePickerDateBeginExamination.Tag = "0_5";
            // 
            // labelDateBeginExamination
            // 
            this.labelDateBeginExamination.AutoSize = true;
            this.labelDateBeginExamination.Location = new System.Drawing.Point(12, 288);
            this.labelDateBeginExamination.Name = "labelDateBeginExamination";
            this.labelDateBeginExamination.Size = new System.Drawing.Size(130, 13);
            this.labelDateBeginExamination.TabIndex = 16;
            this.labelDateBeginExamination.Text = "Дата начала экзаменов";
            // 
            // dateTimePickerDateEndOffset
            // 
            this.dateTimePickerDateEndOffset.Location = new System.Drawing.Point(218, 247);
            this.dateTimePickerDateEndOffset.Name = "dateTimePickerDateEndOffset";
            this.dateTimePickerDateEndOffset.Size = new System.Drawing.Size(140, 20);
            this.dateTimePickerDateEndOffset.TabIndex = 15;
            this.dateTimePickerDateEndOffset.Tag = "0_4";
            // 
            // labelDateEndOffset
            // 
            this.labelDateEndOffset.AutoSize = true;
            this.labelDateEndOffset.Location = new System.Drawing.Point(12, 251);
            this.labelDateEndOffset.Name = "labelDateEndOffset";
            this.labelDateEndOffset.Size = new System.Drawing.Size(132, 13);
            this.labelDateEndOffset.TabIndex = 14;
            this.labelDateEndOffset.Text = "Дата окончания зачетов";
            // 
            // dateTimePickerDateBeginOffset
            // 
            this.dateTimePickerDateBeginOffset.Location = new System.Drawing.Point(218, 212);
            this.dateTimePickerDateBeginOffset.Name = "dateTimePickerDateBeginOffset";
            this.dateTimePickerDateBeginOffset.Size = new System.Drawing.Size(140, 20);
            this.dateTimePickerDateBeginOffset.TabIndex = 13;
            this.dateTimePickerDateBeginOffset.Tag = "0_3";
            // 
            // labelDateBeginOffset
            // 
            this.labelDateBeginOffset.AutoSize = true;
            this.labelDateBeginOffset.Location = new System.Drawing.Point(12, 217);
            this.labelDateBeginOffset.Name = "labelDateBeginOffset";
            this.labelDateBeginOffset.Size = new System.Drawing.Size(114, 13);
            this.labelDateBeginOffset.TabIndex = 12;
            this.labelDateBeginOffset.Text = "Дата начала зачетов";
            // 
            // dateTimePickerDateEndFirstHalfSemester
            // 
            this.dateTimePickerDateEndFirstHalfSemester.Location = new System.Drawing.Point(218, 107);
            this.dateTimePickerDateEndFirstHalfSemester.Name = "dateTimePickerDateEndFirstHalfSemester";
            this.dateTimePickerDateEndFirstHalfSemester.Size = new System.Drawing.Size(140, 20);
            this.dateTimePickerDateEndFirstHalfSemester.TabIndex = 7;
            this.dateTimePickerDateEndFirstHalfSemester.Tag = "0_2";
            // 
            // labelDateEndFirstHalfSemester
            // 
            this.labelDateEndFirstHalfSemester.AutoSize = true;
            this.labelDateEndFirstHalfSemester.Location = new System.Drawing.Point(12, 110);
            this.labelDateEndFirstHalfSemester.Name = "labelDateEndFirstHalfSemester";
            this.labelDateEndFirstHalfSemester.Size = new System.Drawing.Size(201, 13);
            this.labelDateEndFirstHalfSemester.TabIndex = 6;
            this.labelDateEndFirstHalfSemester.Text = "Дата окончания первого полупериода";
            // 
            // dateTimePickerDateBeginFirstHalfSemester
            // 
            this.dateTimePickerDateBeginFirstHalfSemester.Location = new System.Drawing.Point(218, 72);
            this.dateTimePickerDateBeginFirstHalfSemester.Name = "dateTimePickerDateBeginFirstHalfSemester";
            this.dateTimePickerDateBeginFirstHalfSemester.Size = new System.Drawing.Size(140, 20);
            this.dateTimePickerDateBeginFirstHalfSemester.TabIndex = 5;
            this.dateTimePickerDateBeginFirstHalfSemester.Tag = "0_1";
            // 
            // labelDateBeginFirstHalfSemester
            // 
            this.labelDateBeginFirstHalfSemester.AutoSize = true;
            this.labelDateBeginFirstHalfSemester.Location = new System.Drawing.Point(12, 76);
            this.labelDateBeginFirstHalfSemester.Name = "labelDateBeginFirstHalfSemester";
            this.labelDateBeginFirstHalfSemester.Size = new System.Drawing.Size(183, 13);
            this.labelDateBeginFirstHalfSemester.TabIndex = 4;
            this.labelDateBeginFirstHalfSemester.Text = "Дата начала первого полупериода";
            // 
            // checkBoxDateBeginPractic
            // 
            this.checkBoxDateBeginPractic.AutoSize = true;
            this.checkBoxDateBeginPractic.Location = new System.Drawing.Point(15, 353);
            this.checkBoxDateBeginPractic.Name = "checkBoxDateBeginPractic";
            this.checkBoxDateBeginPractic.Size = new System.Drawing.Size(140, 17);
            this.checkBoxDateBeginPractic.TabIndex = 20;
            this.checkBoxDateBeginPractic.Text = "Дата начала практики";
            this.checkBoxDateBeginPractic.UseVisualStyleBackColor = true;
            this.checkBoxDateBeginPractic.CheckedChanged += new System.EventHandler(this.CheckBoxDateBeginPractic_CheckedChanged);
            // 
            // checkBoxDateEndPractic
            // 
            this.checkBoxDateEndPractic.AutoSize = true;
            this.checkBoxDateEndPractic.Location = new System.Drawing.Point(15, 388);
            this.checkBoxDateEndPractic.Name = "checkBoxDateEndPractic";
            this.checkBoxDateEndPractic.Size = new System.Drawing.Size(158, 17);
            this.checkBoxDateEndPractic.TabIndex = 22;
            this.checkBoxDateEndPractic.Text = "Дата окончания практики";
            this.checkBoxDateEndPractic.UseVisualStyleBackColor = true;
            this.checkBoxDateEndPractic.CheckedChanged += new System.EventHandler(this.CheckBoxDateEndPractic_CheckedChanged);
            // 
            // dateTimePickerDateEndSecondHalfSemester
            // 
            this.dateTimePickerDateEndSecondHalfSemester.Location = new System.Drawing.Point(218, 177);
            this.dateTimePickerDateEndSecondHalfSemester.Name = "dateTimePickerDateEndSecondHalfSemester";
            this.dateTimePickerDateEndSecondHalfSemester.Size = new System.Drawing.Size(140, 20);
            this.dateTimePickerDateEndSecondHalfSemester.TabIndex = 11;
            this.dateTimePickerDateEndSecondHalfSemester.Tag = "0_2";
            // 
            // labelDateEndSecondHalfSemester
            // 
            this.labelDateEndSecondHalfSemester.AutoSize = true;
            this.labelDateEndSecondHalfSemester.Location = new System.Drawing.Point(12, 180);
            this.labelDateEndSecondHalfSemester.Name = "labelDateEndSecondHalfSemester";
            this.labelDateEndSecondHalfSemester.Size = new System.Drawing.Size(200, 13);
            this.labelDateEndSecondHalfSemester.TabIndex = 10;
            this.labelDateEndSecondHalfSemester.Text = "Дата окончания второго полупериода";
            // 
            // dateTimePickerDateBeginSecondHalfSemester
            // 
            this.dateTimePickerDateBeginSecondHalfSemester.Location = new System.Drawing.Point(218, 142);
            this.dateTimePickerDateBeginSecondHalfSemester.Name = "dateTimePickerDateBeginSecondHalfSemester";
            this.dateTimePickerDateBeginSecondHalfSemester.Size = new System.Drawing.Size(140, 20);
            this.dateTimePickerDateBeginSecondHalfSemester.TabIndex = 9;
            this.dateTimePickerDateBeginSecondHalfSemester.Tag = "0_1";
            // 
            // labelDateBeginSecondHalfSemester
            // 
            this.labelDateBeginSecondHalfSemester.AutoSize = true;
            this.labelDateBeginSecondHalfSemester.Location = new System.Drawing.Point(12, 146);
            this.labelDateBeginSecondHalfSemester.Name = "labelDateBeginSecondHalfSemester";
            this.labelDateBeginSecondHalfSemester.Size = new System.Drawing.Size(182, 13);
            this.labelDateBeginSecondHalfSemester.TabIndex = 8;
            this.labelDateBeginSecondHalfSemester.Text = "Дата начала второго полупериода";
            // 
            // comboBoxAcademicYear
            // 
            this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicYear.Enabled = false;
            this.comboBoxAcademicYear.FormattingEnabled = true;
            this.comboBoxAcademicYear.Location = new System.Drawing.Point(100, 11);
            this.comboBoxAcademicYear.Name = "comboBoxAcademicYear";
            this.comboBoxAcademicYear.Size = new System.Drawing.Size(222, 21);
            this.comboBoxAcademicYear.TabIndex = 1;
            // 
            // labelAcademicYear
            // 
            this.labelAcademicYear.AutoSize = true;
            this.labelAcademicYear.Location = new System.Drawing.Point(12, 14);
            this.labelAcademicYear.Name = "labelAcademicYear";
            this.labelAcademicYear.Size = new System.Drawing.Size(79, 13);
            this.labelAcademicYear.TabIndex = 0;
            this.labelAcademicYear.Text = "Учебный год*:";
            // 
            // FormSeasonDates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 448);
            this.Name = "FormSeasonDates";
            this.Text = "Даты семестра";
            this.Load += new System.EventHandler(this.FormSeasonDates_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateEndPractic;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateBeginPractic;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateEndExamination;
        private System.Windows.Forms.Label labelDateEndExamination;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateBeginExamination;
        private System.Windows.Forms.Label labelDateBeginExamination;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateEndOffset;
        private System.Windows.Forms.Label labelDateEndOffset;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateBeginOffset;
        private System.Windows.Forms.Label labelDateBeginOffset;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateEndFirstHalfSemester;
        private System.Windows.Forms.Label labelDateEndFirstHalfSemester;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateBeginFirstHalfSemester;
        private System.Windows.Forms.Label labelDateBeginFirstHalfSemester;
        private System.Windows.Forms.CheckBox checkBoxDateBeginPractic;
        private System.Windows.Forms.CheckBox checkBoxDateEndPractic;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateEndSecondHalfSemester;
        private System.Windows.Forms.Label labelDateEndSecondHalfSemester;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateBeginSecondHalfSemester;
        private System.Windows.Forms.Label labelDateBeginSecondHalfSemester;
        private System.Windows.Forms.ComboBox comboBoxAcademicYear;
        private System.Windows.Forms.Label labelAcademicYear;
    }
}