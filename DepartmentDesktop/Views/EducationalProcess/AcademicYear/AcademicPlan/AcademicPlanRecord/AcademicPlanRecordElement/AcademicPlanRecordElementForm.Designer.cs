namespace DepartmentDesktop.Views.EducationalProcess.AcademicYear.AcademicPlan.AcademicPlanRecord.AcademicPlanRecordElement
{
    partial class AcademicPlanRecordElementForm
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
            this.labelAcademicPlanRecord = new System.Windows.Forms.Label();
            this.comboBoxAcademicPlanRecord = new System.Windows.Forms.ComboBox();
            this.comboBoxKindOfLoad = new System.Windows.Forms.ComboBox();
            this.labelKindOfLoad = new System.Windows.Forms.Label();
            this.labelHours = new System.Windows.Forms.Label();
            this.textBoxHours = new System.Windows.Forms.TextBox();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelAcademicPlanRecord
            // 
            this.labelAcademicPlanRecord.AutoSize = true;
            this.labelAcademicPlanRecord.Location = new System.Drawing.Point(12, 9);
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
            this.comboBoxAcademicPlanRecord.Location = new System.Drawing.Point(150, 6);
            this.comboBoxAcademicPlanRecord.Name = "comboBoxAcademicPlanRecord";
            this.comboBoxAcademicPlanRecord.Size = new System.Drawing.Size(220, 21);
            this.comboBoxAcademicPlanRecord.TabIndex = 1;
            // 
            // comboBoxKindOfLoad
            // 
            this.comboBoxKindOfLoad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKindOfLoad.FormattingEnabled = true;
            this.comboBoxKindOfLoad.Location = new System.Drawing.Point(150, 33);
            this.comboBoxKindOfLoad.Name = "comboBoxKindOfLoad";
            this.comboBoxKindOfLoad.Size = new System.Drawing.Size(220, 21);
            this.comboBoxKindOfLoad.TabIndex = 3;
            // 
            // labelKindOfLoad
            // 
            this.labelKindOfLoad.AutoSize = true;
            this.labelKindOfLoad.Location = new System.Drawing.Point(12, 36);
            this.labelKindOfLoad.Name = "labelKindOfLoad";
            this.labelKindOfLoad.Size = new System.Drawing.Size(82, 13);
            this.labelKindOfLoad.TabIndex = 2;
            this.labelKindOfLoad.Text = "Вид нагрузки*:";
            // 
            // labelHours
            // 
            this.labelHours.AutoSize = true;
            this.labelHours.Location = new System.Drawing.Point(12, 63);
            this.labelHours.Name = "labelHours";
            this.labelHours.Size = new System.Drawing.Size(42, 13);
            this.labelHours.TabIndex = 4;
            this.labelHours.Text = "Часы*:";
            // 
            // textBoxHours
            // 
            this.textBoxHours.Location = new System.Drawing.Point(150, 60);
            this.textBoxHours.Name = "textBoxHours";
            this.textBoxHours.Size = new System.Drawing.Size(220, 20);
            this.textBoxHours.TabIndex = 5;
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(123, 96);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 7;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(270, 96);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 8;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(42, 96);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // AcademicPlanRecordElementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 131);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxHours);
            this.Controls.Add(this.labelHours);
            this.Controls.Add(this.comboBoxKindOfLoad);
            this.Controls.Add(this.labelKindOfLoad);
            this.Controls.Add(this.comboBoxAcademicPlanRecord);
            this.Controls.Add(this.labelAcademicPlanRecord);
            this.Name = "AcademicPlanRecordElementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Нагрузка по виду нагрузок";
            this.Load += new System.EventHandler(this.AcademicPlanRecordElementForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAcademicPlanRecord;
        private System.Windows.Forms.ComboBox comboBoxAcademicPlanRecord;
        private System.Windows.Forms.ComboBox comboBoxKindOfLoad;
        private System.Windows.Forms.Label labelKindOfLoad;
        private System.Windows.Forms.Label labelHours;
        private System.Windows.Forms.TextBox textBoxHours;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
    }
}