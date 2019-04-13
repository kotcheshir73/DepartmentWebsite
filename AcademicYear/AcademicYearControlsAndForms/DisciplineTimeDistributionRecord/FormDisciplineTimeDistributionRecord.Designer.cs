namespace AcademicYearControlsAndForms.DisciplineTimeDistributionRecord
{
    partial class FormDisciplineTimeDistributionRecord
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
            this.labelWeekNumber = new System.Windows.Forms.Label();
            this.textBoxWeekNumber = new System.Windows.Forms.TextBox();
            this.labelDisciplineTimeDistribution = new System.Windows.Forms.Label();
            this.comboBoxDisciplineTimeDistribution = new System.Windows.Forms.ComboBox();
            this.labelTimeNorm = new System.Windows.Forms.Label();
            this.comboBoxTimeNorm = new System.Windows.Forms.ComboBox();
            this.labelHours = new System.Windows.Forms.Label();
            this.textBoxHours = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(105, 96);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(252, 96);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(24, 96);
            // 
            // labelWeekNumber
            // 
            this.labelWeekNumber.AutoSize = true;
            this.labelWeekNumber.Location = new System.Drawing.Point(12, 63);
            this.labelWeekNumber.Name = "labelWeekNumber";
            this.labelWeekNumber.Size = new System.Drawing.Size(87, 13);
            this.labelWeekNumber.TabIndex = 4;
            this.labelWeekNumber.Text = "Номер недели*:";
            // 
            // textBoxWeekNumber
            // 
            this.textBoxWeekNumber.Location = new System.Drawing.Point(113, 60);
            this.textBoxWeekNumber.Name = "textBoxWeekNumber";
            this.textBoxWeekNumber.Size = new System.Drawing.Size(61, 20);
            this.textBoxWeekNumber.TabIndex = 5;
            // 
            // labelDisciplineTimeDistribution
            // 
            this.labelDisciplineTimeDistribution.AutoSize = true;
            this.labelDisciplineTimeDistribution.Location = new System.Drawing.Point(12, 9);
            this.labelDisciplineTimeDistribution.Name = "labelDisciplineTimeDistribution";
            this.labelDisciplineTimeDistribution.Size = new System.Drawing.Size(74, 13);
            this.labelDisciplineTimeDistribution.TabIndex = 0;
            this.labelDisciplineTimeDistribution.Text = "Расчасовка*:";
            // 
            // comboBoxDisciplineTimeDistribution
            // 
            this.comboBoxDisciplineTimeDistribution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisciplineTimeDistribution.Enabled = false;
            this.comboBoxDisciplineTimeDistribution.FormattingEnabled = true;
            this.comboBoxDisciplineTimeDistribution.Location = new System.Drawing.Point(113, 6);
            this.comboBoxDisciplineTimeDistribution.Name = "comboBoxDisciplineTimeDistribution";
            this.comboBoxDisciplineTimeDistribution.Size = new System.Drawing.Size(220, 21);
            this.comboBoxDisciplineTimeDistribution.TabIndex = 1;
            // 
            // labelTimeNorm
            // 
            this.labelTimeNorm.AutoSize = true;
            this.labelTimeNorm.Location = new System.Drawing.Point(12, 36);
            this.labelTimeNorm.Name = "labelTimeNorm";
            this.labelTimeNorm.Size = new System.Drawing.Size(95, 13);
            this.labelTimeNorm.TabIndex = 2;
            this.labelTimeNorm.Text = "Норма времени*:";
            // 
            // comboBoxTimeNorm
            // 
            this.comboBoxTimeNorm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTimeNorm.FormattingEnabled = true;
            this.comboBoxTimeNorm.Location = new System.Drawing.Point(113, 33);
            this.comboBoxTimeNorm.Name = "comboBoxTimeNorm";
            this.comboBoxTimeNorm.Size = new System.Drawing.Size(220, 21);
            this.comboBoxTimeNorm.TabIndex = 3;
            // 
            // labelHours
            // 
            this.labelHours.AutoSize = true;
            this.labelHours.Location = new System.Drawing.Point(204, 63);
            this.labelHours.Name = "labelHours";
            this.labelHours.Size = new System.Drawing.Size(42, 13);
            this.labelHours.TabIndex = 6;
            this.labelHours.Text = "Часы*:";
            // 
            // textBoxHours
            // 
            this.textBoxHours.Location = new System.Drawing.Point(272, 60);
            this.textBoxHours.Name = "textBoxHours";
            this.textBoxHours.Size = new System.Drawing.Size(61, 20);
            this.textBoxHours.TabIndex = 7;
            // 
            // FormDisciplineTimeDistributionRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 131);
            this.Controls.Add(this.labelHours);
            this.Controls.Add(this.textBoxHours);
            this.Controls.Add(this.labelWeekNumber);
            this.Controls.Add(this.textBoxWeekNumber);
            this.Controls.Add(this.labelDisciplineTimeDistribution);
            this.Controls.Add(this.comboBoxDisciplineTimeDistribution);
            this.Controls.Add(this.labelTimeNorm);
            this.Controls.Add(this.comboBoxTimeNorm);
            this.Name = "FormDisciplineTimeDistributionRecord";
            this.Text = "Часы расчасовки";
            this.Load += new System.EventHandler(this.FormDisciplineTimeDistributionRecord_Load);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.buttonSaveAndClose, 0);
            this.Controls.SetChildIndex(this.comboBoxTimeNorm, 0);
            this.Controls.SetChildIndex(this.labelTimeNorm, 0);
            this.Controls.SetChildIndex(this.comboBoxDisciplineTimeDistribution, 0);
            this.Controls.SetChildIndex(this.labelDisciplineTimeDistribution, 0);
            this.Controls.SetChildIndex(this.textBoxWeekNumber, 0);
            this.Controls.SetChildIndex(this.labelWeekNumber, 0);
            this.Controls.SetChildIndex(this.textBoxHours, 0);
            this.Controls.SetChildIndex(this.labelHours, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelWeekNumber;
        private System.Windows.Forms.TextBox textBoxWeekNumber;
        private System.Windows.Forms.Label labelDisciplineTimeDistribution;
        private System.Windows.Forms.ComboBox comboBoxDisciplineTimeDistribution;
        private System.Windows.Forms.Label labelTimeNorm;
        private System.Windows.Forms.ComboBox comboBoxTimeNorm;
        private System.Windows.Forms.Label labelHours;
        private System.Windows.Forms.TextBox textBoxHours;
    }
}