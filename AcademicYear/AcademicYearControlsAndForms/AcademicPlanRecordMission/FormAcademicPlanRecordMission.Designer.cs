namespace AcademicYearControlsAndForms.AcademicPlanRecordMission
{
    partial class FormAcademicPlanRecordMission
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
            this.comboBoxLecturer = new System.Windows.Forms.ComboBox();
            this.labelLecturer = new System.Windows.Forms.Label();
            this.comboBoxAcademicPlanRecordElement = new System.Windows.Forms.ComboBox();
            this.labelAcademicPlanRecordElement = new System.Windows.Forms.Label();
            this.textBoxHours = new System.Windows.Forms.TextBox();
            this.labelHours = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.textBoxHours);
            this.panelMain.Controls.Add(this.labelAcademicPlanRecordElement);
            this.panelMain.Controls.Add(this.labelHours);
            this.panelMain.Controls.Add(this.comboBoxAcademicPlanRecordElement);
            this.panelMain.Controls.Add(this.comboBoxLecturer);
            this.panelMain.Controls.Add(this.labelLecturer);
            this.panelMain.Size = new System.Drawing.Size(354, 85);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(354, 36);
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
            // FormAcademicPlanRecordMission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 121);
            this.Name = "FormAcademicPlanRecordMission";
            this.Text = "Распределение нагрузки";
            this.Load += new System.EventHandler(this.FormAcademicPlanRecordMission_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxLecturer;
        private System.Windows.Forms.Label labelLecturer;
        private System.Windows.Forms.ComboBox comboBoxAcademicPlanRecordElement;
        private System.Windows.Forms.Label labelAcademicPlanRecordElement;
        private System.Windows.Forms.TextBox textBoxHours;
        private System.Windows.Forms.Label labelHours;
    }
}