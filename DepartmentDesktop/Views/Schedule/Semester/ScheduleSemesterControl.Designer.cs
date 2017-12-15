namespace DepartmentDesktop.Views.Schedule.Semester
{
    partial class ScheduleSemesterControl
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
            this.tabControlSemester = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // tabControlSemester
            // 
            this.tabControlSemester.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControlSemester.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSemester.Location = new System.Drawing.Point(0, 0);
            this.tabControlSemester.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlSemester.Multiline = true;
            this.tabControlSemester.Name = "tabControlSemester";
            this.tabControlSemester.Padding = new System.Drawing.Point(0, 0);
            this.tabControlSemester.SelectedIndex = 0;
            this.tabControlSemester.Size = new System.Drawing.Size(800, 500);
            this.tabControlSemester.TabIndex = 0;
            // 
            // ScheduleSemesterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlSemester);
            this.Name = "ScheduleSemesterControl";
            this.Size = new System.Drawing.Size(800, 500);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlSemester;
    }
}
