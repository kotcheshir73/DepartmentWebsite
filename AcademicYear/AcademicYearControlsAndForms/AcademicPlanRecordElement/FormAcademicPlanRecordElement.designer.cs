namespace AcademicYearControlsAndForms.AcademicPlanRecordElement
{
    partial class FormAcademicPlanRecordElement
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
            this.labelAcademicPlanRecord = new System.Windows.Forms.Label();
            this.comboBoxAcademicPlanRecord = new System.Windows.Forms.ComboBox();
            this.comboBoxTimeNorm = new System.Windows.Forms.ComboBox();
            this.labelTimeNorm = new System.Windows.Forms.Label();
            this.labelPlanHours = new System.Windows.Forms.Label();
            this.textBoxPlanHours = new System.Windows.Forms.TextBox();
            this.textBoxFactHours = new System.Windows.Forms.TextBox();
            this.labelFactHours = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.tabPageRecords = new System.Windows.Forms.TabPage();
            this.tabPageDisciplineTimeDistribution = new System.Windows.Forms.TabPage();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tabControl);
            this.panelMain.Size = new System.Drawing.Size(705, 373);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(705, 36);
            // 
            // labelAcademicPlanRecord
            // 
            this.labelAcademicPlanRecord.AutoSize = true;
            this.labelAcademicPlanRecord.Location = new System.Drawing.Point(6, 14);
            this.labelAcademicPlanRecord.Name = "labelAcademicPlanRecord";
            this.labelAcademicPlanRecord.Size = new System.Drawing.Size(132, 13);
            this.labelAcademicPlanRecord.TabIndex = 0;
            this.labelAcademicPlanRecord.Text = "Запись учебного плана*:";
            // 
            // comboBoxAcademicPlanRecord
            // 
            this.comboBoxAcademicPlanRecord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicPlanRecord.Enabled = false;
            this.comboBoxAcademicPlanRecord.FormattingEnabled = true;
            this.comboBoxAcademicPlanRecord.Location = new System.Drawing.Point(144, 11);
            this.comboBoxAcademicPlanRecord.Name = "comboBoxAcademicPlanRecord";
            this.comboBoxAcademicPlanRecord.Size = new System.Drawing.Size(220, 21);
            this.comboBoxAcademicPlanRecord.TabIndex = 1;
            // 
            // comboBoxTimeNorm
            // 
            this.comboBoxTimeNorm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTimeNorm.FormattingEnabled = true;
            this.comboBoxTimeNorm.Location = new System.Drawing.Point(144, 38);
            this.comboBoxTimeNorm.Name = "comboBoxTimeNorm";
            this.comboBoxTimeNorm.Size = new System.Drawing.Size(220, 21);
            this.comboBoxTimeNorm.TabIndex = 3;
            // 
            // labelTimeNorm
            // 
            this.labelTimeNorm.AutoSize = true;
            this.labelTimeNorm.Location = new System.Drawing.Point(6, 41);
            this.labelTimeNorm.Name = "labelTimeNorm";
            this.labelTimeNorm.Size = new System.Drawing.Size(95, 13);
            this.labelTimeNorm.TabIndex = 2;
            this.labelTimeNorm.Text = "Норма времени*:";
            // 
            // labelPlanHours
            // 
            this.labelPlanHours.AutoSize = true;
            this.labelPlanHours.Location = new System.Drawing.Point(6, 68);
            this.labelPlanHours.Name = "labelPlanHours";
            this.labelPlanHours.Size = new System.Drawing.Size(71, 13);
            this.labelPlanHours.TabIndex = 4;
            this.labelPlanHours.Text = "План. часы*:";
            // 
            // textBoxPlanHours
            // 
            this.textBoxPlanHours.Location = new System.Drawing.Point(83, 65);
            this.textBoxPlanHours.Name = "textBoxPlanHours";
            this.textBoxPlanHours.Size = new System.Drawing.Size(80, 20);
            this.textBoxPlanHours.TabIndex = 5;
            // 
            // textBoxFactHours
            // 
            this.textBoxFactHours.Location = new System.Drawing.Point(284, 65);
            this.textBoxFactHours.Name = "textBoxFactHours";
            this.textBoxFactHours.Size = new System.Drawing.Size(80, 20);
            this.textBoxFactHours.TabIndex = 7;
            // 
            // labelFactHours
            // 
            this.labelFactHours.AutoSize = true;
            this.labelFactHours.Location = new System.Drawing.Point(207, 68);
            this.labelFactHours.Name = "labelFactHours";
            this.labelFactHours.Size = new System.Drawing.Size(73, 13);
            this.labelFactHours.TabIndex = 6;
            this.labelFactHours.Text = "Факт. часы*:";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageRecords);
            this.tabControl.Controls.Add(this.tabPageDisciplineTimeDistribution);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(705, 373);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.labelAcademicPlanRecord);
            this.tabPageConfig.Controls.Add(this.textBoxFactHours);
            this.tabPageConfig.Controls.Add(this.comboBoxAcademicPlanRecord);
            this.tabPageConfig.Controls.Add(this.labelFactHours);
            this.tabPageConfig.Controls.Add(this.labelTimeNorm);
            this.tabPageConfig.Controls.Add(this.comboBoxTimeNorm);
            this.tabPageConfig.Controls.Add(this.labelPlanHours);
            this.tabPageConfig.Controls.Add(this.textBoxPlanHours);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(697, 347);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Нагрузка по виду нагрузок";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // tabPageRecords
            // 
            this.tabPageRecords.Location = new System.Drawing.Point(4, 22);
            this.tabPageRecords.Name = "tabPageRecords";
            this.tabPageRecords.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRecords.Size = new System.Drawing.Size(311, 148);
            this.tabPageRecords.TabIndex = 1;
            this.tabPageRecords.Text = "Распределение нагрузки";
            this.tabPageRecords.UseVisualStyleBackColor = true;
            // 
            // tabPageDisciplineTimeDistribution
            // 
            this.tabPageDisciplineTimeDistribution.Location = new System.Drawing.Point(4, 22);
            this.tabPageDisciplineTimeDistribution.Name = "tabPageDisciplineTimeDistribution";
            this.tabPageDisciplineTimeDistribution.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDisciplineTimeDistribution.Size = new System.Drawing.Size(311, 148);
            this.tabPageDisciplineTimeDistribution.TabIndex = 2;
            this.tabPageDisciplineTimeDistribution.Text = "Расчасовки";
            this.tabPageDisciplineTimeDistribution.UseVisualStyleBackColor = true;
            // 
            // FormAcademicPlanRecordElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 409);
            this.Name = "FormAcademicPlanRecordElement";
            this.Text = "Нагрузка по виду нагрузок";
            this.panelMain.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelAcademicPlanRecord;
        private System.Windows.Forms.ComboBox comboBoxAcademicPlanRecord;
        private System.Windows.Forms.ComboBox comboBoxTimeNorm;
        private System.Windows.Forms.Label labelTimeNorm;
        private System.Windows.Forms.Label labelPlanHours;
        private System.Windows.Forms.TextBox textBoxPlanHours;
        private System.Windows.Forms.TextBox textBoxFactHours;
        private System.Windows.Forms.Label labelFactHours;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TabPage tabPageRecords;
        private System.Windows.Forms.TabPage tabPageDisciplineTimeDistribution;
    }
}