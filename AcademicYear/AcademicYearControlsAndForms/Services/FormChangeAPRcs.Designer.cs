namespace AcademicYearControlsAndForms.Services
{
    partial class FormChangeAPRcs
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
            this.buttonChange = new System.Windows.Forms.Button();
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
            this.comboBoxAcademicPlanRecord.FormattingEnabled = true;
            this.comboBoxAcademicPlanRecord.Location = new System.Drawing.Point(150, 6);
            this.comboBoxAcademicPlanRecord.Name = "comboBoxAcademicPlanRecord";
            this.comboBoxAcademicPlanRecord.Size = new System.Drawing.Size(600, 21);
            this.comboBoxAcademicPlanRecord.TabIndex = 1;
            // 
            // buttonChange
            // 
            this.buttonChange.Location = new System.Drawing.Point(648, 33);
            this.buttonChange.Name = "buttonChange";
            this.buttonChange.Size = new System.Drawing.Size(75, 23);
            this.buttonChange.TabIndex = 2;
            this.buttonChange.Text = "Сменить";
            this.buttonChange.UseVisualStyleBackColor = true;
            this.buttonChange.Click += new System.EventHandler(this.ButtonChange_Click);
            // 
            // FormChangeAPRcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 68);
            this.Controls.Add(this.buttonChange);
            this.Controls.Add(this.labelAcademicPlanRecord);
            this.Controls.Add(this.comboBoxAcademicPlanRecord);
            this.Name = "FormChangeAPRcs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Смена записи учебного плана";
            this.Load += new System.EventHandler(this.FormChangeAPRcs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAcademicPlanRecord;
        private System.Windows.Forms.ComboBox comboBoxAcademicPlanRecord;
        private System.Windows.Forms.Button buttonChange;
    }
}