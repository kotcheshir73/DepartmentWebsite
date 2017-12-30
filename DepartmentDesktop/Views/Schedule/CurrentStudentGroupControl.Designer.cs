namespace DepartmentDesktop.Views.Schedule
{
    partial class CurrentStudentGroupControl
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
            this.tabControlStudentGroup = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // tabControlStudentGroup
            // 
            this.tabControlStudentGroup.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControlStudentGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlStudentGroup.Location = new System.Drawing.Point(0, 0);
            this.tabControlStudentGroup.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlStudentGroup.Multiline = true;
            this.tabControlStudentGroup.Name = "tabControlStudentGroup";
            this.tabControlStudentGroup.Padding = new System.Drawing.Point(0, 0);
            this.tabControlStudentGroup.SelectedIndex = 0;
            this.tabControlStudentGroup.Size = new System.Drawing.Size(800, 500);
            this.tabControlStudentGroup.TabIndex = 0;
            // 
            // CurrentStudentGroupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlStudentGroup);
            this.Name = "CurrentStudentGroupControl";
            this.Size = new System.Drawing.Size(800, 500);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlStudentGroup;
    }
}
