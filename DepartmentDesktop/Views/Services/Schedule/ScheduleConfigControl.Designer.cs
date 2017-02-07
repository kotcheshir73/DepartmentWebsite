namespace DepartmentDesktop.Views.Services.Schedule
{
    partial class ScheduleConfigControl
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
            this.groupBoxLoadHTMLScheduleForClassrooms = new System.Windows.Forms.GroupBox();
            this.checkedListBoxClassrooms = new System.Windows.Forms.CheckedListBox();
            this.checkBoxClearSchedule = new System.Windows.Forms.CheckBox();
            this.buttonMakeLoadHTMLScheduleForClassrooms = new System.Windows.Forms.Button();
            this.groupBoxLoadHTMLScheduleForClassrooms.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxLoadHTMLScheduleForClassrooms
            // 
            this.groupBoxLoadHTMLScheduleForClassrooms.Controls.Add(this.buttonMakeLoadHTMLScheduleForClassrooms);
            this.groupBoxLoadHTMLScheduleForClassrooms.Controls.Add(this.checkBoxClearSchedule);
            this.groupBoxLoadHTMLScheduleForClassrooms.Controls.Add(this.checkedListBoxClassrooms);
            this.groupBoxLoadHTMLScheduleForClassrooms.Location = new System.Drawing.Point(3, 3);
            this.groupBoxLoadHTMLScheduleForClassrooms.Name = "groupBoxLoadHTMLScheduleForClassrooms";
            this.groupBoxLoadHTMLScheduleForClassrooms.Size = new System.Drawing.Size(318, 270);
            this.groupBoxLoadHTMLScheduleForClassrooms.TabIndex = 0;
            this.groupBoxLoadHTMLScheduleForClassrooms.TabStop = false;
            this.groupBoxLoadHTMLScheduleForClassrooms.Text = "Обновление расписания по аудиториям";
            // 
            // checkedListBoxClassrooms
            // 
            this.checkedListBoxClassrooms.FormattingEnabled = true;
            this.checkedListBoxClassrooms.Location = new System.Drawing.Point(6, 19);
            this.checkedListBoxClassrooms.Name = "checkedListBoxClassrooms";
            this.checkedListBoxClassrooms.Size = new System.Drawing.Size(142, 244);
            this.checkedListBoxClassrooms.TabIndex = 0;
            // 
            // checkBoxClearSchedule
            // 
            this.checkBoxClearSchedule.AutoSize = true;
            this.checkBoxClearSchedule.Location = new System.Drawing.Point(167, 32);
            this.checkBoxClearSchedule.Name = "checkBoxClearSchedule";
            this.checkBoxClearSchedule.Size = new System.Drawing.Size(141, 17);
            this.checkBoxClearSchedule.TabIndex = 1;
            this.checkBoxClearSchedule.Text = "Отчистить расписание";
            this.checkBoxClearSchedule.UseVisualStyleBackColor = true;
            // 
            // buttonMakeLoadHTMLScheduleForClassrooms
            // 
            this.buttonMakeLoadHTMLScheduleForClassrooms.Location = new System.Drawing.Point(167, 231);
            this.buttonMakeLoadHTMLScheduleForClassrooms.Name = "buttonMakeLoadHTMLScheduleForClassrooms";
            this.buttonMakeLoadHTMLScheduleForClassrooms.Size = new System.Drawing.Size(75, 23);
            this.buttonMakeLoadHTMLScheduleForClassrooms.TabIndex = 2;
            this.buttonMakeLoadHTMLScheduleForClassrooms.Text = "Выполнить";
            this.buttonMakeLoadHTMLScheduleForClassrooms.UseVisualStyleBackColor = true;
            this.buttonMakeLoadHTMLScheduleForClassrooms.Click += new System.EventHandler(this.buttonMakeLoadHTMLScheduleForClassrooms_Click);
            // 
            // ScheduleConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxLoadHTMLScheduleForClassrooms);
            this.Name = "ScheduleConfigControl";
            this.Size = new System.Drawing.Size(800, 500);
            this.groupBoxLoadHTMLScheduleForClassrooms.ResumeLayout(false);
            this.groupBoxLoadHTMLScheduleForClassrooms.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxLoadHTMLScheduleForClassrooms;
        private System.Windows.Forms.CheckBox checkBoxClearSchedule;
        private System.Windows.Forms.CheckedListBox checkedListBoxClassrooms;
        private System.Windows.Forms.Button buttonMakeLoadHTMLScheduleForClassrooms;
    }
}
