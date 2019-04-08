namespace ScheduleControlsAndForms.Current
{
    partial class ControlCurrentLecturer
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
            this.tabControlLecturer = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // tabControlLecturer
            // 
            this.tabControlLecturer.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControlLecturer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlLecturer.Location = new System.Drawing.Point(0, 0);
            this.tabControlLecturer.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlLecturer.Multiline = true;
            this.tabControlLecturer.Name = "tabControlLecturer";
            this.tabControlLecturer.Padding = new System.Drawing.Point(0, 0);
            this.tabControlLecturer.SelectedIndex = 0;
            this.tabControlLecturer.Size = new System.Drawing.Size(800, 500);
            this.tabControlLecturer.TabIndex = 0;
            // 
            // CurrentLecturerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlLecturer);
            this.Name = "CurrentLecturerControl";
            this.Size = new System.Drawing.Size(800, 500);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlLecturer;
    }
}
