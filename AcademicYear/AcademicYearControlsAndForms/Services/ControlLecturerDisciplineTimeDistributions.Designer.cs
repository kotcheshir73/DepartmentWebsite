namespace AcademicYearControlsAndForms.Services
{
    partial class ControlLecturerDisciplineTimeDistributions
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.panelTop = new System.Windows.Forms.Panel();
            this.comboBoxAcademicYear = new System.Windows.Forms.ComboBox();
            this.labelSelectAcademicYear = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 34);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1163, 536);
            this.tabControl.TabIndex = 1;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.comboBoxAcademicYear);
            this.panelTop.Controls.Add(this.labelSelectAcademicYear);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1163, 34);
            this.panelTop.TabIndex = 0;
            // 
            // comboBoxAcademicYear
            // 
            this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicYear.FormattingEnabled = true;
            this.comboBoxAcademicYear.Location = new System.Drawing.Point(137, 6);
            this.comboBoxAcademicYear.Name = "comboBoxAcademicYear";
            this.comboBoxAcademicYear.Size = new System.Drawing.Size(188, 21);
            this.comboBoxAcademicYear.TabIndex = 1;
            this.comboBoxAcademicYear.SelectedIndexChanged += new System.EventHandler(this.ComboBoxAcademicYear_SelectedIndexChanged);
            // 
            // labelSelectAcademicYear
            // 
            this.labelSelectAcademicYear.AutoSize = true;
            this.labelSelectAcademicYear.Location = new System.Drawing.Point(12, 9);
            this.labelSelectAcademicYear.Name = "labelSelectAcademicYear";
            this.labelSelectAcademicYear.Size = new System.Drawing.Size(119, 13);
            this.labelSelectAcademicYear.TabIndex = 0;
            this.labelSelectAcademicYear.Text = "Выбрать учебный год:";
            // 
            // ControlLecturerDisciplineTimeDistributions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelTop);
            this.Name = "ControlLecturerDisciplineTimeDistributions";
            this.Size = new System.Drawing.Size(1163, 570);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.ComboBox comboBoxAcademicYear;
        private System.Windows.Forms.Label labelSelectAcademicYear;
    }
}
