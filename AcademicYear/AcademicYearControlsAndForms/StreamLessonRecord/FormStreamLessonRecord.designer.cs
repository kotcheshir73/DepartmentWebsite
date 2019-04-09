namespace AcademicYearControlsAndForms.StreamLessonRecord
{
    partial class FormStreamLessonRecord
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
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboBoxAcademicPlanRecord = new System.Windows.Forms.ComboBox();
            this.labelAcademicPlanRecord = new System.Windows.Forms.Label();
            this.comboBoxAcademicPlan = new System.Windows.Forms.ComboBox();
            this.labelAcademicPlan = new System.Windows.Forms.Label();
            this.comboBoxStreamLesson = new System.Windows.Forms.ComboBox();
            this.labelStreamLesson = new System.Windows.Forms.Label();
            this.comboBoxAcademicPlanRecordElement = new System.Windows.Forms.ComboBox();
            this.labelAcademicPlanRecordElement = new System.Windows.Forms.Label();
            this.checkBoxIsMain = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(155, 149);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 10;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(302, 149);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(74, 149);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboBoxAcademicPlanRecord
            // 
            this.comboBoxAcademicPlanRecord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicPlanRecord.FormattingEnabled = true;
            this.comboBoxAcademicPlanRecord.Location = new System.Drawing.Point(215, 60);
            this.comboBoxAcademicPlanRecord.Name = "comboBoxAcademicPlanRecord";
            this.comboBoxAcademicPlanRecord.Size = new System.Drawing.Size(220, 21);
            this.comboBoxAcademicPlanRecord.TabIndex = 5;
            this.comboBoxAcademicPlanRecord.SelectedIndexChanged += new System.EventHandler(this.comboBoxAcademicPlanRecord_SelectedIndexChanged);
            // 
            // labelAcademicPlanRecord
            // 
            this.labelAcademicPlanRecord.AutoSize = true;
            this.labelAcademicPlanRecord.Location = new System.Drawing.Point(12, 63);
            this.labelAcademicPlanRecord.Name = "labelAcademicPlanRecord";
            this.labelAcademicPlanRecord.Size = new System.Drawing.Size(128, 13);
            this.labelAcademicPlanRecord.TabIndex = 4;
            this.labelAcademicPlanRecord.Text = "Запись учебного плана:";
            // 
            // comboBoxAcademicPlan
            // 
            this.comboBoxAcademicPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicPlan.FormattingEnabled = true;
            this.comboBoxAcademicPlan.Location = new System.Drawing.Point(215, 33);
            this.comboBoxAcademicPlan.Name = "comboBoxAcademicPlan";
            this.comboBoxAcademicPlan.Size = new System.Drawing.Size(220, 21);
            this.comboBoxAcademicPlan.TabIndex = 3;
            this.comboBoxAcademicPlan.SelectedIndexChanged += new System.EventHandler(this.comboBoxAcademicPlan_SelectedIndexChanged);
            // 
            // labelAcademicPlan
            // 
            this.labelAcademicPlan.AutoSize = true;
            this.labelAcademicPlan.Location = new System.Drawing.Point(12, 36);
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
            this.comboBoxStreamLesson.Location = new System.Drawing.Point(215, 6);
            this.comboBoxStreamLesson.Name = "comboBoxStreamLesson";
            this.comboBoxStreamLesson.Size = new System.Drawing.Size(220, 21);
            this.comboBoxStreamLesson.TabIndex = 1;
            // 
            // labelStreamLesson
            // 
            this.labelStreamLesson.AutoSize = true;
            this.labelStreamLesson.Location = new System.Drawing.Point(12, 9);
            this.labelStreamLesson.Name = "labelStreamLesson";
            this.labelStreamLesson.Size = new System.Drawing.Size(45, 13);
            this.labelStreamLesson.TabIndex = 0;
            this.labelStreamLesson.Text = "Поток*:";
            // 
            // comboBoxAcademicPlanRecordElement
            // 
            this.comboBoxAcademicPlanRecordElement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicPlanRecordElement.FormattingEnabled = true;
            this.comboBoxAcademicPlanRecordElement.Location = new System.Drawing.Point(215, 87);
            this.comboBoxAcademicPlanRecordElement.Name = "comboBoxAcademicPlanRecordElement";
            this.comboBoxAcademicPlanRecordElement.Size = new System.Drawing.Size(220, 21);
            this.comboBoxAcademicPlanRecordElement.TabIndex = 7;
            // 
            // labelAcademicPlanRecordElement
            // 
            this.labelAcademicPlanRecordElement.AutoSize = true;
            this.labelAcademicPlanRecordElement.Location = new System.Drawing.Point(12, 90);
            this.labelAcademicPlanRecordElement.Name = "labelAcademicPlanRecordElement";
            this.labelAcademicPlanRecordElement.Size = new System.Drawing.Size(197, 13);
            this.labelAcademicPlanRecordElement.TabIndex = 6;
            this.labelAcademicPlanRecordElement.Text = "Нагрузка по записи учебного плана*:";
            // 
            // checkBoxIsMain
            // 
            this.checkBoxIsMain.AutoSize = true;
            this.checkBoxIsMain.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxIsMain.Location = new System.Drawing.Point(215, 116);
            this.checkBoxIsMain.Name = "checkBoxIsMain";
            this.checkBoxIsMain.Size = new System.Drawing.Size(177, 17);
            this.checkBoxIsMain.TabIndex = 8;
            this.checkBoxIsMain.Text = "Считать по этой записи часы:";
            this.checkBoxIsMain.UseVisualStyleBackColor = true;
            // 
            // StreamLessonRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 181);
            this.Controls.Add(this.checkBoxIsMain);
            this.Controls.Add(this.comboBoxAcademicPlanRecordElement);
            this.Controls.Add(this.labelAcademicPlanRecordElement);
            this.Controls.Add(this.comboBoxStreamLesson);
            this.Controls.Add(this.labelStreamLesson);
            this.Controls.Add(this.comboBoxAcademicPlan);
            this.Controls.Add(this.labelAcademicPlan);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.comboBoxAcademicPlanRecord);
            this.Controls.Add(this.labelAcademicPlanRecord);
            this.Name = "StreamLessonRecordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Запись потока";
            this.Load += new System.EventHandler(this.FormStreamLessonRecord_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
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