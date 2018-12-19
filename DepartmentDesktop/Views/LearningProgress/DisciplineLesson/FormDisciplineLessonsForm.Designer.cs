namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson
{
    partial class FormDisciplineLessonsForm
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
            this.comboBoxSemester = new System.Windows.Forms.ComboBox();
            this.labelSemester = new System.Windows.Forms.Label();
            this.textBoxCountLessons = new System.Windows.Forms.TextBox();
            this.labelCountLessons = new System.Windows.Forms.Label();
            this.buttonForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxSemester
            // 
            this.comboBoxSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSemester.FormattingEnabled = true;
            this.comboBoxSemester.Location = new System.Drawing.Point(135, 6);
            this.comboBoxSemester.Name = "comboBoxSemester";
            this.comboBoxSemester.Size = new System.Drawing.Size(210, 21);
            this.comboBoxSemester.TabIndex = 1;
            // 
            // labelSemester
            // 
            this.labelSemester.AutoSize = true;
            this.labelSemester.Location = new System.Drawing.Point(12, 9);
            this.labelSemester.Name = "labelSemester";
            this.labelSemester.Size = new System.Drawing.Size(58, 13);
            this.labelSemester.TabIndex = 0;
            this.labelSemester.Text = "Семестр*:";
            // 
            // textBoxCountLessons
            // 
            this.textBoxCountLessons.Location = new System.Drawing.Point(135, 33);
            this.textBoxCountLessons.Name = "textBoxCountLessons";
            this.textBoxCountLessons.Size = new System.Drawing.Size(210, 20);
            this.textBoxCountLessons.TabIndex = 3;
            // 
            // labelCountLessons
            // 
            this.labelCountLessons.AutoSize = true;
            this.labelCountLessons.Location = new System.Drawing.Point(12, 36);
            this.labelCountLessons.Name = "labelCountLessons";
            this.labelCountLessons.Size = new System.Drawing.Size(117, 13);
            this.labelCountLessons.TabIndex = 2;
            this.labelCountLessons.Text = "Количество занятий*:";
            // 
            // buttonForm
            // 
            this.buttonForm.Location = new System.Drawing.Point(120, 70);
            this.buttonForm.Name = "buttonForm";
            this.buttonForm.Size = new System.Drawing.Size(141, 23);
            this.buttonForm.TabIndex = 4;
            this.buttonForm.Text = "Сформировать";
            this.buttonForm.UseVisualStyleBackColor = true;
            this.buttonForm.Click += new System.EventHandler(this.buttonForm_Click);
            // 
            // FormDisciplineLessonsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 101);
            this.Controls.Add(this.buttonForm);
            this.Controls.Add(this.textBoxCountLessons);
            this.Controls.Add(this.labelCountLessons);
            this.Controls.Add(this.comboBoxSemester);
            this.Controls.Add(this.labelSemester);
            this.Name = "FormDisciplineLessonsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Формирование занятий";
            this.Load += new System.EventHandler(this.FormDisciplineLessonsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxSemester;
        private System.Windows.Forms.Label labelSemester;
        private System.Windows.Forms.TextBox textBoxCountLessons;
        private System.Windows.Forms.Label labelCountLessons;
        private System.Windows.Forms.Button buttonForm;
    }
}