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
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.labelHours);
            this.panelMain.Controls.Add(this.labelDisciplineTimeDistribution);
            this.panelMain.Controls.Add(this.textBoxHours);
            this.panelMain.Controls.Add(this.comboBoxTimeNorm);
            this.panelMain.Controls.Add(this.labelWeekNumber);
            this.panelMain.Controls.Add(this.labelTimeNorm);
            this.panelMain.Controls.Add(this.textBoxWeekNumber);
            this.panelMain.Controls.Add(this.comboBoxDisciplineTimeDistribution);
            this.panelMain.Size = new System.Drawing.Size(344, 95);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(344, 36);
            // 
            // labelWeekNumber
            // 
            this.labelWeekNumber.AutoSize = true;
            this.labelWeekNumber.Location = new System.Drawing.Point(9, 68);
            this.labelWeekNumber.Name = "labelWeekNumber";
            this.labelWeekNumber.Size = new System.Drawing.Size(87, 13);
            this.labelWeekNumber.TabIndex = 4;
            this.labelWeekNumber.Text = "Номер недели*:";
            // 
            // textBoxWeekNumber
            // 
            this.textBoxWeekNumber.Location = new System.Drawing.Point(110, 65);
            this.textBoxWeekNumber.Name = "textBoxWeekNumber";
            this.textBoxWeekNumber.Size = new System.Drawing.Size(61, 20);
            this.textBoxWeekNumber.TabIndex = 5;
            // 
            // labelDisciplineTimeDistribution
            // 
            this.labelDisciplineTimeDistribution.AutoSize = true;
            this.labelDisciplineTimeDistribution.Location = new System.Drawing.Point(9, 14);
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
            this.comboBoxDisciplineTimeDistribution.Location = new System.Drawing.Point(110, 11);
            this.comboBoxDisciplineTimeDistribution.Name = "comboBoxDisciplineTimeDistribution";
            this.comboBoxDisciplineTimeDistribution.Size = new System.Drawing.Size(220, 21);
            this.comboBoxDisciplineTimeDistribution.TabIndex = 1;
            // 
            // labelTimeNorm
            // 
            this.labelTimeNorm.AutoSize = true;
            this.labelTimeNorm.Location = new System.Drawing.Point(9, 41);
            this.labelTimeNorm.Name = "labelTimeNorm";
            this.labelTimeNorm.Size = new System.Drawing.Size(95, 13);
            this.labelTimeNorm.TabIndex = 2;
            this.labelTimeNorm.Text = "Норма времени*:";
            // 
            // comboBoxTimeNorm
            // 
            this.comboBoxTimeNorm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTimeNorm.FormattingEnabled = true;
            this.comboBoxTimeNorm.Location = new System.Drawing.Point(110, 38);
            this.comboBoxTimeNorm.Name = "comboBoxTimeNorm";
            this.comboBoxTimeNorm.Size = new System.Drawing.Size(220, 21);
            this.comboBoxTimeNorm.TabIndex = 3;
            // 
            // labelHours
            // 
            this.labelHours.AutoSize = true;
            this.labelHours.Location = new System.Drawing.Point(201, 68);
            this.labelHours.Name = "labelHours";
            this.labelHours.Size = new System.Drawing.Size(42, 13);
            this.labelHours.TabIndex = 6;
            this.labelHours.Text = "Часы*:";
            // 
            // textBoxHours
            // 
            this.textBoxHours.Location = new System.Drawing.Point(269, 65);
            this.textBoxHours.Name = "textBoxHours";
            this.textBoxHours.Size = new System.Drawing.Size(61, 20);
            this.textBoxHours.TabIndex = 7;
            // 
            // FormDisciplineTimeDistributionRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 131);
            this.Name = "FormDisciplineTimeDistributionRecord";
            this.Text = "Часы расчасовки";
            this.Load += new System.EventHandler(this.FormDisciplineTimeDistributionRecord_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

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