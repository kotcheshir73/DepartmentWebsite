namespace AcademicYearControlsAndForms.AcademicPlanRecordMission
{
    partial class FormAcademicPlanRecordMission
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
            this.comboBoxLecturer = new System.Windows.Forms.ComboBox();
            this.labelLecturer = new System.Windows.Forms.Label();
            this.comboBoxAcademicPlanRecordElement = new System.Windows.Forms.ComboBox();
            this.labelAcademicPlanRecordElement = new System.Windows.Forms.Label();
            this.textBoxHours = new System.Windows.Forms.TextBox();
            this.labelHours = new System.Windows.Forms.Label();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxLecturer
            // 
            this.comboBoxLecturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLecturer.FormattingEnabled = true;
            this.comboBoxLecturer.Location = new System.Drawing.Point(118, 33);
            this.comboBoxLecturer.Name = "comboBoxLecturer";
            this.comboBoxLecturer.Size = new System.Drawing.Size(220, 21);
            this.comboBoxLecturer.TabIndex = 3;
            // 
            // labelLecturer
            // 
            this.labelLecturer.AutoSize = true;
            this.labelLecturer.Location = new System.Drawing.Point(12, 36);
            this.labelLecturer.Name = "labelLecturer";
            this.labelLecturer.Size = new System.Drawing.Size(93, 13);
            this.labelLecturer.TabIndex = 2;
            this.labelLecturer.Text = "Преподаватель*:";
            // 
            // comboBoxAcademicPlanRecordElement
            // 
            this.comboBoxAcademicPlanRecordElement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicPlanRecordElement.Enabled = false;
            this.comboBoxAcademicPlanRecordElement.FormattingEnabled = true;
            this.comboBoxAcademicPlanRecordElement.Location = new System.Drawing.Point(118, 6);
            this.comboBoxAcademicPlanRecordElement.Name = "comboBoxAcademicPlanRecordElement";
            this.comboBoxAcademicPlanRecordElement.Size = new System.Drawing.Size(220, 21);
            this.comboBoxAcademicPlanRecordElement.TabIndex = 1;
            // 
            // labelAcademicPlanRecordElement
            // 
            this.labelAcademicPlanRecordElement.AutoSize = true;
            this.labelAcademicPlanRecordElement.Location = new System.Drawing.Point(12, 9);
            this.labelAcademicPlanRecordElement.Name = "labelAcademicPlanRecordElement";
            this.labelAcademicPlanRecordElement.Size = new System.Drawing.Size(100, 13);
            this.labelAcademicPlanRecordElement.TabIndex = 0;
            this.labelAcademicPlanRecordElement.Text = "Запись нагрузки*:";
            // 
            // textBoxHours
            // 
            this.textBoxHours.Location = new System.Drawing.Point(118, 60);
            this.textBoxHours.Name = "textBoxHours";
            this.textBoxHours.Size = new System.Drawing.Size(80, 20);
            this.textBoxHours.TabIndex = 5;
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
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(107, 86);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 7;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(254, 86);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 8;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(26, 86);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FormAcademicPlanRecordMission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 121);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxHours);
            this.Controls.Add(this.labelHours);
            this.Controls.Add(this.comboBoxLecturer);
            this.Controls.Add(this.labelLecturer);
            this.Controls.Add(this.comboBoxAcademicPlanRecordElement);
            this.Controls.Add(this.labelAcademicPlanRecordElement);
            this.Name = "FormAcademicPlanRecordMission";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Распределение нагрузки";
            this.Load += new System.EventHandler(this.FormAcademicPlanRecordMission_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxLecturer;
        private System.Windows.Forms.Label labelLecturer;
        private System.Windows.Forms.ComboBox comboBoxAcademicPlanRecordElement;
        private System.Windows.Forms.Label labelAcademicPlanRecordElement;
        private System.Windows.Forms.TextBox textBoxHours;
        private System.Windows.Forms.Label labelHours;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
    }
}