namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson
{
    partial class DuplicateDisciplineLessonForm
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
            this.buttonDuplicate = new System.Windows.Forms.Button();
            this.comboBoxDisciplineLesson = new System.Windows.Forms.ComboBox();
            this.labelSelectDL = new System.Windows.Forms.Label();
            this.checkBoxDuplicateTaskVariants = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonDuplicate
            // 
            this.buttonDuplicate.Location = new System.Drawing.Point(117, 73);
            this.buttonDuplicate.Name = "buttonDuplicate";
            this.buttonDuplicate.Size = new System.Drawing.Size(141, 23);
            this.buttonDuplicate.TabIndex = 2;
            this.buttonDuplicate.Text = "Дублировать";
            this.buttonDuplicate.UseVisualStyleBackColor = true;
            this.buttonDuplicate.Click += new System.EventHandler(this.buttonDuplicate_Click);
            // 
            // comboBoxDisciplineLesson
            // 
            this.comboBoxDisciplineLesson.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisciplineLesson.FormattingEnabled = true;
            this.comboBoxDisciplineLesson.Location = new System.Drawing.Point(169, 11);
            this.comboBoxDisciplineLesson.Name = "comboBoxDisciplineLesson";
            this.comboBoxDisciplineLesson.Size = new System.Drawing.Size(210, 21);
            this.comboBoxDisciplineLesson.TabIndex = 1;
            // 
            // labelSelectDL
            // 
            this.labelSelectDL.AutoSize = true;
            this.labelSelectDL.Location = new System.Drawing.Point(15, 14);
            this.labelSelectDL.Name = "labelSelectDL";
            this.labelSelectDL.Size = new System.Drawing.Size(147, 13);
            this.labelSelectDL.TabIndex = 0;
            this.labelSelectDL.Text = "Выбрать занятие-дубликат:";
            // 
            // checkBoxDuplicateTaskVariants
            // 
            this.checkBoxDuplicateTaskVariants.AutoSize = true;
            this.checkBoxDuplicateTaskVariants.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxDuplicateTaskVariants.Location = new System.Drawing.Point(18, 45);
            this.checkBoxDuplicateTaskVariants.Name = "checkBoxDuplicateTaskVariants";
            this.checkBoxDuplicateTaskVariants.Size = new System.Drawing.Size(145, 17);
            this.checkBoxDuplicateTaskVariants.TabIndex = 3;
            this.checkBoxDuplicateTaskVariants.Text = "Дублировать варианты";
            this.checkBoxDuplicateTaskVariants.UseVisualStyleBackColor = true;
            // 
            // DuplicateDisciplineLessonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 108);
            this.Controls.Add(this.checkBoxDuplicateTaskVariants);
            this.Controls.Add(this.buttonDuplicate);
            this.Controls.Add(this.comboBoxDisciplineLesson);
            this.Controls.Add(this.labelSelectDL);
            this.Name = "DuplicateDisciplineLessonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Дублирование заданий";
            this.Load += new System.EventHandler(this.DuplicateDisciplineLessonForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDuplicate;
        private System.Windows.Forms.ComboBox comboBoxDisciplineLesson;
        private System.Windows.Forms.Label labelSelectDL;
        private System.Windows.Forms.CheckBox checkBoxDuplicateTaskVariants;
    }
}