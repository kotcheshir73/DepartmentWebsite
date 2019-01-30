namespace DepartmentDesktop.Views.LearningProgress
{
    partial class ConfiguringDisciplinesControl
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
            this.labelDiscipline = new System.Windows.Forms.Label();
            this.comboBoxDisciplines = new System.Windows.Forms.ComboBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.panelTop = new System.Windows.Forms.Panel();
            this.comboBoxAcademicYear = new System.Windows.Forms.ComboBox();
            this.labelAcademicYear = new System.Windows.Forms.Label();
            this.comboBoxEducationDirection = new System.Windows.Forms.ComboBox();
            this.labelEducationDirection = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelDiscipline
            // 
            this.labelDiscipline.AutoSize = true;
            this.labelDiscipline.Location = new System.Drawing.Point(490, 11);
            this.labelDiscipline.Name = "labelDiscipline";
            this.labelDiscipline.Size = new System.Drawing.Size(70, 13);
            this.labelDiscipline.TabIndex = 4;
            this.labelDiscipline.Text = "Дисциплина";
            // 
            // comboBoxDisciplines
            // 
            this.comboBoxDisciplines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisciplines.FormattingEnabled = true;
            this.comboBoxDisciplines.Location = new System.Drawing.Point(575, 8);
            this.comboBoxDisciplines.Name = "comboBoxDisciplines";
            this.comboBoxDisciplines.Size = new System.Drawing.Size(427, 21);
            this.comboBoxDisciplines.TabIndex = 5;
            this.comboBoxDisciplines.SelectedIndexChanged += new System.EventHandler(this.comboBoxDisciplines_SelectedIndexChanged);
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 67);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1000, 433);
            this.tabControl.TabIndex = 1;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelInfo);
            this.panelTop.Controls.Add(this.comboBoxEducationDirection);
            this.panelTop.Controls.Add(this.labelEducationDirection);
            this.panelTop.Controls.Add(this.comboBoxAcademicYear);
            this.panelTop.Controls.Add(this.labelAcademicYear);
            this.panelTop.Controls.Add(this.labelDiscipline);
            this.panelTop.Controls.Add(this.comboBoxDisciplines);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1000, 67);
            this.panelTop.TabIndex = 0;
            // 
            // comboBoxAcademicYear
            // 
            this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicYear.FormattingEnabled = true;
            this.comboBoxAcademicYear.Location = new System.Drawing.Point(97, 8);
            this.comboBoxAcademicYear.Name = "comboBoxAcademicYear";
            this.comboBoxAcademicYear.Size = new System.Drawing.Size(163, 21);
            this.comboBoxAcademicYear.TabIndex = 1;
            this.comboBoxAcademicYear.SelectedIndexChanged += new System.EventHandler(this.comboBoxAcademicYear_SelectedIndexChanged);
            // 
            // labelAcademicYear
            // 
            this.labelAcademicYear.AutoSize = true;
            this.labelAcademicYear.Location = new System.Drawing.Point(16, 11);
            this.labelAcademicYear.Name = "labelAcademicYear";
            this.labelAcademicYear.Size = new System.Drawing.Size(75, 13);
            this.labelAcademicYear.TabIndex = 0;
            this.labelAcademicYear.Text = "Учебный год:";
            // 
            // comboBoxEducationDirection
            // 
            this.comboBoxEducationDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEducationDirection.FormattingEnabled = true;
            this.comboBoxEducationDirection.Location = new System.Drawing.Point(366, 8);
            this.comboBoxEducationDirection.Name = "comboBoxEducationDirection";
            this.comboBoxEducationDirection.Size = new System.Drawing.Size(95, 21);
            this.comboBoxEducationDirection.TabIndex = 3;
            this.comboBoxEducationDirection.SelectedIndexChanged += new System.EventHandler(this.comboBoxEducationDirection_SelectedIndexChanged);
            // 
            // labelEducationDirection
            // 
            this.labelEducationDirection.AutoSize = true;
            this.labelEducationDirection.Location = new System.Drawing.Point(282, 11);
            this.labelEducationDirection.Name = "labelEducationDirection";
            this.labelEducationDirection.Size = new System.Drawing.Size(78, 13);
            this.labelEducationDirection.TabIndex = 2;
            this.labelEducationDirection.Text = "Направление:";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(16, 41);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(76, 13);
            this.labelInfo.TabIndex = 6;
            this.labelInfo.Text = "Информация:";
            // 
            // LearningProgressControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelTop);
            this.Name = "LearningProgressControl";
            this.Size = new System.Drawing.Size(1000, 500);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelDiscipline;
        private System.Windows.Forms.ComboBox comboBoxDisciplines;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.ComboBox comboBoxAcademicYear;
        private System.Windows.Forms.Label labelAcademicYear;
        private System.Windows.Forms.ComboBox comboBoxEducationDirection;
        private System.Windows.Forms.Label labelEducationDirection;
        private System.Windows.Forms.Label labelInfo;
    }
}
