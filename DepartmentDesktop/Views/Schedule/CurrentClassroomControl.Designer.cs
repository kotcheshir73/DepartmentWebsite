namespace DepartmentDesktop.Views.Schedule
{
    partial class CurrentClassroomControl
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
            this.tabControlClassroom = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // tabControlClassroom
            // 
            this.tabControlClassroom.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControlClassroom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlClassroom.Location = new System.Drawing.Point(0, 0);
            this.tabControlClassroom.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlClassroom.Multiline = true;
            this.tabControlClassroom.Name = "tabControlClassroom";
            this.tabControlClassroom.Padding = new System.Drawing.Point(0, 0);
            this.tabControlClassroom.SelectedIndex = 0;
            this.tabControlClassroom.Size = new System.Drawing.Size(800, 500);
            this.tabControlClassroom.TabIndex = 0;
            // 
            // CurrentClassroomControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlClassroom);
            this.Name = "CurrentClassroomControl";
            this.Size = new System.Drawing.Size(800, 500);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlClassroom;
    }
}
