namespace DepartmentDesktop.Views.LearningProgress
{
    partial class StudentsDistributionControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxGroups = new System.Windows.Forms.ComboBox();
            this.comboBoxDisciplines = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxEducationDirection = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxAcademicYear = new System.Windows.Forms.ComboBox();
            this.standartControl = new DepartmentDesktop.Controllers.StandartControl();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.comboBoxAcademicYear);
            this.panelTop.Controls.Add(this.comboBoxEducationDirection);
            this.panelTop.Controls.Add(this.label3);
            this.panelTop.Controls.Add(this.label4);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.comboBoxDisciplines);
            this.panelTop.Controls.Add(this.comboBoxGroups);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(832, 39);
            this.panelTop.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(579, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Группа:";
            // 
            // comboBoxGroups
            // 
            this.comboBoxGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGroups.FormattingEnabled = true;
            this.comboBoxGroups.Location = new System.Drawing.Point(630, 7);
            this.comboBoxGroups.Name = "comboBoxGroups";
            this.comboBoxGroups.Size = new System.Drawing.Size(185, 21);
            this.comboBoxGroups.TabIndex = 5;
            // 
            // comboBoxDisciplines
            // 
            this.comboBoxDisciplines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisciplines.FormattingEnabled = true;
            this.comboBoxDisciplines.Location = new System.Drawing.Point(449, 7);
            this.comboBoxDisciplines.Name = "comboBoxDisciplines";
            this.comboBoxDisciplines.Size = new System.Drawing.Size(113, 21);
            this.comboBoxDisciplines.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(373, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Дисциплина:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(194, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Направление:";
            // 
            // comboBoxEducationDirection
            // 
            this.comboBoxEducationDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEducationDirection.FormattingEnabled = true;
            this.comboBoxEducationDirection.Location = new System.Drawing.Point(278, 7);
            this.comboBoxEducationDirection.Name = "comboBoxEducationDirection";
            this.comboBoxEducationDirection.Size = new System.Drawing.Size(86, 21);
            this.comboBoxEducationDirection.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Учебный год:";
            // 
            // comboBoxAcademicYear
            // 
            this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicYear.FormattingEnabled = true;
            this.comboBoxAcademicYear.Location = new System.Drawing.Point(93, 7);
            this.comboBoxAcademicYear.Name = "comboBoxAcademicYear";
            this.comboBoxAcademicYear.Size = new System.Drawing.Size(95, 21);
            this.comboBoxAcademicYear.TabIndex = 9;
            // 
            // standartControl
            // 
            this.standartControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.standartControl.Location = new System.Drawing.Point(0, 39);
            this.standartControl.Name = "standartControl";
            this.standartControl.Size = new System.Drawing.Size(832, 334);
            this.standartControl.TabIndex = 8;
            // 
            // StudentsDistributionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.standartControl);
            this.Controls.Add(this.panelTop);
            this.Name = "StudentsDistributionControl";
            this.Size = new System.Drawing.Size(832, 373);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.ComboBox comboBoxAcademicYear;
        private System.Windows.Forms.ComboBox comboBoxEducationDirection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxDisciplines;
        private System.Windows.Forms.ComboBox comboBoxGroups;
        private Controllers.StandartControl standartControl;
    }
}
