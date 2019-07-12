namespace AcademicYearControlsAndForms.Services
{
    partial class FormDuplicate
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
            this.comboBoxFromAcademicYear = new System.Windows.Forms.ComboBox();
            this.labelFromAcademicYear = new System.Windows.Forms.Label();
            this.comboBoxToAcademicYear = new System.Windows.Forms.ComboBox();
            this.labelToAcademicYear = new System.Windows.Forms.Label();
            this.checkBoxAcademicPlan = new System.Windows.Forms.CheckBox();
            this.checkBoxTimeNorm = new System.Windows.Forms.CheckBox();
            this.checkBoxContingent = new System.Windows.Forms.CheckBox();
            this.checkBoxSeasonDate = new System.Windows.Forms.CheckBox();
            this.buttonMake = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxFromAcademicYear
            // 
            this.comboBoxFromAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFromAcademicYear.FormattingEnabled = true;
            this.comboBoxFromAcademicYear.Location = new System.Drawing.Point(151, 15);
            this.comboBoxFromAcademicYear.Name = "comboBoxFromAcademicYear";
            this.comboBoxFromAcademicYear.Size = new System.Drawing.Size(222, 21);
            this.comboBoxFromAcademicYear.TabIndex = 1;
            // 
            // labelFromAcademicYear
            // 
            this.labelFromAcademicYear.AutoSize = true;
            this.labelFromAcademicYear.Location = new System.Drawing.Point(12, 18);
            this.labelFromAcademicYear.Name = "labelFromAcademicYear";
            this.labelFromAcademicYear.Size = new System.Drawing.Size(133, 13);
            this.labelFromAcademicYear.TabIndex = 0;
            this.labelFromAcademicYear.Text = "С какого учебного года*:";
            // 
            // comboBoxToAcademicYear
            // 
            this.comboBoxToAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxToAcademicYear.FormattingEnabled = true;
            this.comboBoxToAcademicYear.Location = new System.Drawing.Point(151, 53);
            this.comboBoxToAcademicYear.Name = "comboBoxToAcademicYear";
            this.comboBoxToAcademicYear.Size = new System.Drawing.Size(222, 21);
            this.comboBoxToAcademicYear.TabIndex = 3;
            // 
            // labelToAcademicYear
            // 
            this.labelToAcademicYear.AutoSize = true;
            this.labelToAcademicYear.Location = new System.Drawing.Point(12, 56);
            this.labelToAcademicYear.Name = "labelToAcademicYear";
            this.labelToAcademicYear.Size = new System.Drawing.Size(119, 13);
            this.labelToAcademicYear.TabIndex = 2;
            this.labelToAcademicYear.Text = "В какой учебный год*:";
            // 
            // checkBoxAcademicPlan
            // 
            this.checkBoxAcademicPlan.AutoSize = true;
            this.checkBoxAcademicPlan.Location = new System.Drawing.Point(37, 102);
            this.checkBoxAcademicPlan.Name = "checkBoxAcademicPlan";
            this.checkBoxAcademicPlan.Size = new System.Drawing.Size(161, 17);
            this.checkBoxAcademicPlan.TabIndex = 4;
            this.checkBoxAcademicPlan.Text = "Перенести учебные планы";
            this.checkBoxAcademicPlan.UseVisualStyleBackColor = true;
            // 
            // checkBoxTimeNorm
            // 
            this.checkBoxTimeNorm.AutoSize = true;
            this.checkBoxTimeNorm.Location = new System.Drawing.Point(37, 138);
            this.checkBoxTimeNorm.Name = "checkBoxTimeNorm";
            this.checkBoxTimeNorm.Size = new System.Drawing.Size(165, 17);
            this.checkBoxTimeNorm.TabIndex = 5;
            this.checkBoxTimeNorm.Text = "Перенести нормы времени";
            this.checkBoxTimeNorm.UseVisualStyleBackColor = true;
            // 
            // checkBoxContingent
            // 
            this.checkBoxContingent.AutoSize = true;
            this.checkBoxContingent.Location = new System.Drawing.Point(37, 179);
            this.checkBoxContingent.Name = "checkBoxContingent";
            this.checkBoxContingent.Size = new System.Drawing.Size(141, 17);
            this.checkBoxContingent.TabIndex = 6;
            this.checkBoxContingent.Text = "Перенести контингент";
            this.checkBoxContingent.UseVisualStyleBackColor = true;
            // 
            // checkBoxSeasonDate
            // 
            this.checkBoxSeasonDate.AutoSize = true;
            this.checkBoxSeasonDate.Location = new System.Drawing.Point(37, 218);
            this.checkBoxSeasonDate.Name = "checkBoxSeasonDate";
            this.checkBoxSeasonDate.Size = new System.Drawing.Size(161, 17);
            this.checkBoxSeasonDate.TabIndex = 7;
            this.checkBoxSeasonDate.Text = "Перенести даты семестра";
            this.checkBoxSeasonDate.UseVisualStyleBackColor = true;
            // 
            // buttonMake
            // 
            this.buttonMake.Location = new System.Drawing.Point(254, 138);
            this.buttonMake.Name = "buttonMake";
            this.buttonMake.Size = new System.Drawing.Size(89, 49);
            this.buttonMake.TabIndex = 8;
            this.buttonMake.Text = "Выполнить";
            this.buttonMake.UseVisualStyleBackColor = true;
            this.buttonMake.Click += new System.EventHandler(this.buttonMake_Click);
            // 
            // DuplicateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 261);
            this.Controls.Add(this.buttonMake);
            this.Controls.Add(this.checkBoxSeasonDate);
            this.Controls.Add(this.checkBoxContingent);
            this.Controls.Add(this.checkBoxTimeNorm);
            this.Controls.Add(this.checkBoxAcademicPlan);
            this.Controls.Add(this.comboBoxToAcademicYear);
            this.Controls.Add(this.labelToAcademicYear);
            this.Controls.Add(this.comboBoxFromAcademicYear);
            this.Controls.Add(this.labelFromAcademicYear);
            this.Name = "DuplicateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Продублировать записи";
            this.Load += new System.EventHandler(this.FormDuplicate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxFromAcademicYear;
        private System.Windows.Forms.Label labelFromAcademicYear;
        private System.Windows.Forms.ComboBox comboBoxToAcademicYear;
        private System.Windows.Forms.Label labelToAcademicYear;
        private System.Windows.Forms.CheckBox checkBoxAcademicPlan;
        private System.Windows.Forms.CheckBox checkBoxTimeNorm;
        private System.Windows.Forms.CheckBox checkBoxContingent;
        private System.Windows.Forms.CheckBox checkBoxSeasonDate;
        private System.Windows.Forms.Button buttonMake;
    }
}