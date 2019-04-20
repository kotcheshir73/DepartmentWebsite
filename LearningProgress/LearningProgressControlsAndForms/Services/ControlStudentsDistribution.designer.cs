namespace LearningProgressControlsAndForms.Services
{
    partial class ControlStudentsDistribution
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
            this.comboBoxSemester = new System.Windows.Forms.ComboBox();
            this.labelSemester = new System.Windows.Forms.Label();
            this.comboBoxAcademicYear = new System.Windows.Forms.ComboBox();
            this.comboBoxEducationDirection = new System.Windows.Forms.ComboBox();
            this.labelEducationDirection = new System.Windows.Forms.Label();
            this.labelAcademicYear = new System.Windows.Forms.Label();
            this.labelDisciplines = new System.Windows.Forms.Label();
            this.s = new System.Windows.Forms.Label();
            this.comboBoxDisciplines = new System.Windows.Forms.ComboBox();
            this.comboBoxStudentGroups = new System.Windows.Forms.ComboBox();
            this.panelControl = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.comboBoxSemester);
            this.panelTop.Controls.Add(this.labelSemester);
            this.panelTop.Controls.Add(this.comboBoxAcademicYear);
            this.panelTop.Controls.Add(this.comboBoxEducationDirection);
            this.panelTop.Controls.Add(this.labelEducationDirection);
            this.panelTop.Controls.Add(this.labelAcademicYear);
            this.panelTop.Controls.Add(this.labelDisciplines);
            this.panelTop.Controls.Add(this.s);
            this.panelTop.Controls.Add(this.comboBoxDisciplines);
            this.panelTop.Controls.Add(this.comboBoxStudentGroups);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1000, 39);
            this.panelTop.TabIndex = 0;
            // 
            // comboBoxSemester
            // 
            this.comboBoxSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSemester.FormattingEnabled = true;
            this.comboBoxSemester.Location = new System.Drawing.Point(900, 7);
            this.comboBoxSemester.Name = "comboBoxSemester";
            this.comboBoxSemester.Size = new System.Drawing.Size(114, 21);
            this.comboBoxSemester.TabIndex = 9;
            this.comboBoxSemester.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSemester_SelectedIndexChanged);
            // 
            // labelSemester
            // 
            this.labelSemester.AutoSize = true;
            this.labelSemester.Location = new System.Drawing.Point(836, 10);
            this.labelSemester.Name = "labelSemester";
            this.labelSemester.Size = new System.Drawing.Size(54, 13);
            this.labelSemester.TabIndex = 8;
            this.labelSemester.Text = "Семестр:";
            // 
            // comboBoxAcademicYear
            // 
            this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicYear.FormattingEnabled = true;
            this.comboBoxAcademicYear.Location = new System.Drawing.Point(93, 7);
            this.comboBoxAcademicYear.Name = "comboBoxAcademicYear";
            this.comboBoxAcademicYear.Size = new System.Drawing.Size(95, 21);
            this.comboBoxAcademicYear.TabIndex = 1;
            this.comboBoxAcademicYear.SelectedIndexChanged += new System.EventHandler(this.ComboBoxAcademicYear_SelectedIndexChanged);
            // 
            // comboBoxEducationDirection
            // 
            this.comboBoxEducationDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEducationDirection.FormattingEnabled = true;
            this.comboBoxEducationDirection.Location = new System.Drawing.Point(278, 7);
            this.comboBoxEducationDirection.Name = "comboBoxEducationDirection";
            this.comboBoxEducationDirection.Size = new System.Drawing.Size(86, 21);
            this.comboBoxEducationDirection.TabIndex = 3;
            this.comboBoxEducationDirection.SelectedIndexChanged += new System.EventHandler(this.ComboBoxEducationDirection_SelectedIndexChanged);
            // 
            // labelEducationDirection
            // 
            this.labelEducationDirection.AutoSize = true;
            this.labelEducationDirection.Location = new System.Drawing.Point(194, 10);
            this.labelEducationDirection.Name = "labelEducationDirection";
            this.labelEducationDirection.Size = new System.Drawing.Size(78, 13);
            this.labelEducationDirection.TabIndex = 2;
            this.labelEducationDirection.Text = "Направление:";
            // 
            // labelAcademicYear
            // 
            this.labelAcademicYear.AutoSize = true;
            this.labelAcademicYear.Location = new System.Drawing.Point(12, 10);
            this.labelAcademicYear.Name = "labelAcademicYear";
            this.labelAcademicYear.Size = new System.Drawing.Size(75, 13);
            this.labelAcademicYear.TabIndex = 0;
            this.labelAcademicYear.Text = "Учебный год:";
            // 
            // labelDisciplines
            // 
            this.labelDisciplines.AutoSize = true;
            this.labelDisciplines.Location = new System.Drawing.Point(373, 10);
            this.labelDisciplines.Name = "labelDisciplines";
            this.labelDisciplines.Size = new System.Drawing.Size(73, 13);
            this.labelDisciplines.TabIndex = 4;
            this.labelDisciplines.Text = "Дисциплина:";
            // 
            // s
            // 
            this.s.AutoSize = true;
            this.s.Location = new System.Drawing.Point(658, 10);
            this.s.Name = "s";
            this.s.Size = new System.Drawing.Size(45, 13);
            this.s.TabIndex = 6;
            this.s.Text = "Группа:";
            // 
            // comboBoxDisciplines
            // 
            this.comboBoxDisciplines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisciplines.FormattingEnabled = true;
            this.comboBoxDisciplines.Location = new System.Drawing.Point(449, 7);
            this.comboBoxDisciplines.Name = "comboBoxDisciplines";
            this.comboBoxDisciplines.Size = new System.Drawing.Size(203, 21);
            this.comboBoxDisciplines.TabIndex = 5;
            this.comboBoxDisciplines.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDisciplines_SelectedIndexChanged);
            // 
            // comboBoxStudentGroups
            // 
            this.comboBoxStudentGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudentGroups.FormattingEnabled = true;
            this.comboBoxStudentGroups.Location = new System.Drawing.Point(709, 7);
            this.comboBoxStudentGroups.Name = "comboBoxStudentGroups";
            this.comboBoxStudentGroups.Size = new System.Drawing.Size(109, 21);
            this.comboBoxStudentGroups.TabIndex = 7;
            this.comboBoxStudentGroups.SelectedIndexChanged += new System.EventHandler(this.ComboBoxStudentGroups_SelectedIndexChanged);
            // 
            // panelControl
            // 
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl.Location = new System.Drawing.Point(0, 39);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(1000, 461);
            this.panelControl.TabIndex = 1;
            // 
            // StudentsDistributionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.panelTop);
            this.Name = "StudentsDistributionControl";
            this.Size = new System.Drawing.Size(1000, 500);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.ComboBox comboBoxAcademicYear;
        private System.Windows.Forms.ComboBox comboBoxEducationDirection;
        private System.Windows.Forms.Label labelEducationDirection;
        private System.Windows.Forms.Label labelAcademicYear;
        private System.Windows.Forms.Label labelDisciplines;
        private System.Windows.Forms.Label s;
        private System.Windows.Forms.ComboBox comboBoxDisciplines;
        private System.Windows.Forms.ComboBox comboBoxStudentGroups;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.ComboBox comboBoxSemester;
        private System.Windows.Forms.Label labelSemester;
    }
}
