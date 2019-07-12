namespace AcademicYearControlsAndForms.StreamLessonRecord
{
    partial class FormStreamLessonRecord
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
            this.comboBoxAcademicPlanRecord = new System.Windows.Forms.ComboBox();
            this.labelAcademicPlanRecord = new System.Windows.Forms.Label();
            this.comboBoxAcademicPlan = new System.Windows.Forms.ComboBox();
            this.labelAcademicPlan = new System.Windows.Forms.Label();
            this.comboBoxStreamLesson = new System.Windows.Forms.ComboBox();
            this.labelStreamLesson = new System.Windows.Forms.Label();
            this.comboBoxAcademicPlanRecordElement = new System.Windows.Forms.ComboBox();
            this.labelAcademicPlanRecordElement = new System.Windows.Forms.Label();
            this.checkBoxIsMain = new System.Windows.Forms.CheckBox();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.checkBoxIsMain);
            this.panelMain.Controls.Add(this.labelStreamLesson);
            this.panelMain.Controls.Add(this.comboBoxAcademicPlanRecordElement);
            this.panelMain.Controls.Add(this.labelAcademicPlanRecord);
            this.panelMain.Controls.Add(this.labelAcademicPlanRecordElement);
            this.panelMain.Controls.Add(this.comboBoxAcademicPlanRecord);
            this.panelMain.Controls.Add(this.comboBoxStreamLesson);
            this.panelMain.Controls.Add(this.labelAcademicPlan);
            this.panelMain.Controls.Add(this.comboBoxAcademicPlan);
            this.panelMain.Size = new System.Drawing.Size(444, 145);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(444, 36);
            // 
            // comboBoxAcademicPlanRecord
            // 
            this.comboBoxAcademicPlanRecord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicPlanRecord.FormattingEnabled = true;
            this.comboBoxAcademicPlanRecord.Location = new System.Drawing.Point(215, 63);
            this.comboBoxAcademicPlanRecord.Name = "comboBoxAcademicPlanRecord";
            this.comboBoxAcademicPlanRecord.Size = new System.Drawing.Size(220, 21);
            this.comboBoxAcademicPlanRecord.TabIndex = 5;
            this.comboBoxAcademicPlanRecord.SelectedIndexChanged += new System.EventHandler(this.ComboBoxAcademicPlanRecord_SelectedIndexChanged);
            // 
            // labelAcademicPlanRecord
            // 
            this.labelAcademicPlanRecord.AutoSize = true;
            this.labelAcademicPlanRecord.Location = new System.Drawing.Point(12, 66);
            this.labelAcademicPlanRecord.Name = "labelAcademicPlanRecord";
            this.labelAcademicPlanRecord.Size = new System.Drawing.Size(128, 13);
            this.labelAcademicPlanRecord.TabIndex = 4;
            this.labelAcademicPlanRecord.Text = "Запись учебного плана:";
            // 
            // comboBoxAcademicPlan
            // 
            this.comboBoxAcademicPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicPlan.FormattingEnabled = true;
            this.comboBoxAcademicPlan.Location = new System.Drawing.Point(215, 36);
            this.comboBoxAcademicPlan.Name = "comboBoxAcademicPlan";
            this.comboBoxAcademicPlan.Size = new System.Drawing.Size(220, 21);
            this.comboBoxAcademicPlan.TabIndex = 3;
            this.comboBoxAcademicPlan.SelectedIndexChanged += new System.EventHandler(this.ComboBoxAcademicPlan_SelectedIndexChanged);
            // 
            // labelAcademicPlan
            // 
            this.labelAcademicPlan.AutoSize = true;
            this.labelAcademicPlan.Location = new System.Drawing.Point(12, 39);
            this.labelAcademicPlan.Name = "labelAcademicPlan";
            this.labelAcademicPlan.Size = new System.Drawing.Size(82, 13);
            this.labelAcademicPlan.TabIndex = 2;
            this.labelAcademicPlan.Text = "Учебный план:";
            // 
            // comboBoxStreamLesson
            // 
            this.comboBoxStreamLesson.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStreamLesson.Enabled = false;
            this.comboBoxStreamLesson.FormattingEnabled = true;
            this.comboBoxStreamLesson.Location = new System.Drawing.Point(215, 9);
            this.comboBoxStreamLesson.Name = "comboBoxStreamLesson";
            this.comboBoxStreamLesson.Size = new System.Drawing.Size(220, 21);
            this.comboBoxStreamLesson.TabIndex = 1;
            // 
            // labelStreamLesson
            // 
            this.labelStreamLesson.AutoSize = true;
            this.labelStreamLesson.Location = new System.Drawing.Point(12, 12);
            this.labelStreamLesson.Name = "labelStreamLesson";
            this.labelStreamLesson.Size = new System.Drawing.Size(45, 13);
            this.labelStreamLesson.TabIndex = 0;
            this.labelStreamLesson.Text = "Поток*:";
            // 
            // comboBoxAcademicPlanRecordElement
            // 
            this.comboBoxAcademicPlanRecordElement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicPlanRecordElement.FormattingEnabled = true;
            this.comboBoxAcademicPlanRecordElement.Location = new System.Drawing.Point(215, 90);
            this.comboBoxAcademicPlanRecordElement.Name = "comboBoxAcademicPlanRecordElement";
            this.comboBoxAcademicPlanRecordElement.Size = new System.Drawing.Size(220, 21);
            this.comboBoxAcademicPlanRecordElement.TabIndex = 7;
            // 
            // labelAcademicPlanRecordElement
            // 
            this.labelAcademicPlanRecordElement.AutoSize = true;
            this.labelAcademicPlanRecordElement.Location = new System.Drawing.Point(12, 93);
            this.labelAcademicPlanRecordElement.Name = "labelAcademicPlanRecordElement";
            this.labelAcademicPlanRecordElement.Size = new System.Drawing.Size(197, 13);
            this.labelAcademicPlanRecordElement.TabIndex = 6;
            this.labelAcademicPlanRecordElement.Text = "Нагрузка по записи учебного плана*:";
            // 
            // checkBoxIsMain
            // 
            this.checkBoxIsMain.AutoSize = true;
            this.checkBoxIsMain.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxIsMain.Location = new System.Drawing.Point(215, 119);
            this.checkBoxIsMain.Name = "checkBoxIsMain";
            this.checkBoxIsMain.Size = new System.Drawing.Size(177, 17);
            this.checkBoxIsMain.TabIndex = 8;
            this.checkBoxIsMain.Text = "Считать по этой записи часы:";
            this.checkBoxIsMain.UseVisualStyleBackColor = true;
            // 
            // FormStreamLessonRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 181);
            this.Name = "FormStreamLessonRecord";
            this.Text = "Запись потока";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxAcademicPlanRecord;
        private System.Windows.Forms.Label labelAcademicPlanRecord;
        private System.Windows.Forms.ComboBox comboBoxAcademicPlan;
        private System.Windows.Forms.Label labelAcademicPlan;
        private System.Windows.Forms.ComboBox comboBoxStreamLesson;
        private System.Windows.Forms.Label labelStreamLesson;
        private System.Windows.Forms.ComboBox comboBoxAcademicPlanRecordElement;
        private System.Windows.Forms.Label labelAcademicPlanRecordElement;
        private System.Windows.Forms.CheckBox checkBoxIsMain;
    }
}