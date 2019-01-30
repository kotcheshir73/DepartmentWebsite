namespace DepartmentDesktop.Views.LearningProgress.DisciplineLessonConducted
{
    partial class DisciplineLessonConductedForm
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
            this.comboBoxDisciplineLesson = new System.Windows.Forms.ComboBox();
            this.labelDisciplineLesson = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.labelSubgroup = new System.Windows.Forms.Label();
            this.comboBoxSubgroup = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.comboBoxStudentGroups = new System.Windows.Forms.ComboBox();
            this.labelStudentGroup = new System.Windows.Forms.Label();
            this.tabPageRecords = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxDisciplineLesson
            // 
            this.comboBoxDisciplineLesson.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisciplineLesson.FormattingEnabled = true;
            this.comboBoxDisciplineLesson.Location = new System.Drawing.Point(137, 40);
            this.comboBoxDisciplineLesson.Name = "comboBoxDisciplineLesson";
            this.comboBoxDisciplineLesson.Size = new System.Drawing.Size(210, 21);
            this.comboBoxDisciplineLesson.TabIndex = 3;
            // 
            // labelDisciplineLesson
            // 
            this.labelDisciplineLesson.AutoSize = true;
            this.labelDisciplineLesson.Location = new System.Drawing.Point(18, 43);
            this.labelDisciplineLesson.Name = "labelDisciplineLesson";
            this.labelDisciplineLesson.Size = new System.Drawing.Size(56, 13);
            this.labelDisciplineLesson.TabIndex = 2;
            this.labelDisciplineLesson.Text = "Занятие*:";
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(18, 70);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(103, 13);
            this.labelDate.TabIndex = 4;
            this.labelDate.Text = "Дата проведения*:";
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.Location = new System.Drawing.Point(137, 67);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(210, 20);
            this.dateTimePickerDate.TabIndex = 5;
            // 
            // labelSubgroup
            // 
            this.labelSubgroup.AutoSize = true;
            this.labelSubgroup.Location = new System.Drawing.Point(18, 96);
            this.labelSubgroup.Name = "labelSubgroup";
            this.labelSubgroup.Size = new System.Drawing.Size(64, 13);
            this.labelSubgroup.TabIndex = 6;
            this.labelSubgroup.Text = "Подгруппа:";
            // 
            // comboBoxSubgroup
            // 
            this.comboBoxSubgroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSubgroup.FormattingEnabled = true;
            this.comboBoxSubgroup.Location = new System.Drawing.Point(137, 93);
            this.comboBoxSubgroup.Name = "comboBoxSubgroup";
            this.comboBoxSubgroup.Size = new System.Drawing.Size(210, 21);
            this.comboBoxSubgroup.TabIndex = 7;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(34, 131);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(262, 131);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 10;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(115, 131);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 9;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageRecords);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1143, 497);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.comboBoxStudentGroups);
            this.tabPageConfig.Controls.Add(this.labelStudentGroup);
            this.tabPageConfig.Controls.Add(this.labelDisciplineLesson);
            this.tabPageConfig.Controls.Add(this.buttonSave);
            this.tabPageConfig.Controls.Add(this.comboBoxDisciplineLesson);
            this.tabPageConfig.Controls.Add(this.buttonClose);
            this.tabPageConfig.Controls.Add(this.labelDate);
            this.tabPageConfig.Controls.Add(this.buttonSaveAndClose);
            this.tabPageConfig.Controls.Add(this.dateTimePickerDate);
            this.tabPageConfig.Controls.Add(this.comboBoxSubgroup);
            this.tabPageConfig.Controls.Add(this.labelSubgroup);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(1135, 471);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Проводимое занятие";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // comboBoxStudentGroups
            // 
            this.comboBoxStudentGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudentGroups.FormattingEnabled = true;
            this.comboBoxStudentGroups.Location = new System.Drawing.Point(137, 13);
            this.comboBoxStudentGroups.Name = "comboBoxStudentGroups";
            this.comboBoxStudentGroups.Size = new System.Drawing.Size(210, 21);
            this.comboBoxStudentGroups.TabIndex = 11;
            this.comboBoxStudentGroups.SelectedIndexChanged += new System.EventHandler(this.comboBoxStudentGroups_SelectedIndexChanged);
            // 
            // labelStudentGroup
            // 
            this.labelStudentGroup.AutoSize = true;
            this.labelStudentGroup.Location = new System.Drawing.Point(18, 16);
            this.labelStudentGroup.Name = "labelStudentGroup";
            this.labelStudentGroup.Size = new System.Drawing.Size(49, 13);
            this.labelStudentGroup.TabIndex = 0;
            this.labelStudentGroup.Text = "Группа*:";
            // 
            // tabPageRecords
            // 
            this.tabPageRecords.Location = new System.Drawing.Point(4, 22);
            this.tabPageRecords.Name = "tabPageRecords";
            this.tabPageRecords.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRecords.Size = new System.Drawing.Size(1135, 471);
            this.tabPageRecords.TabIndex = 1;
            this.tabPageRecords.Text = "Студенты";
            this.tabPageRecords.UseVisualStyleBackColor = true;
            // 
            // DisciplineLessonConductedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 497);
            this.Controls.Add(this.tabControl);
            this.Name = "DisciplineLessonConductedForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Проведение занятия";
            this.Load += new System.EventHandler(this.DisciplineLessonConductedForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxDisciplineLesson;
        private System.Windows.Forms.Label labelDisciplineLesson;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.Label labelSubgroup;
        private System.Windows.Forms.ComboBox comboBoxSubgroup;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TabPage tabPageRecords;
        private System.Windows.Forms.Label labelStudentGroup;
        private System.Windows.Forms.ComboBox comboBoxStudentGroups;
    }
}