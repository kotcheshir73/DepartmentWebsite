﻿namespace TicketViews.Views.ExaminationTemplateTicket
{
    partial class ExaminationTemplateTicketControl
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
            this.standartListControl = new TicketViews.Controls.StandartListControl();
            this.SuspendLayout();
            // 
            // standartListControl
            // 
            this.standartListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.standartListControl.Location = new System.Drawing.Point(0, 0);
            this.standartListControl.Name = "standartListControl";
            this.standartListControl.Size = new System.Drawing.Size(800, 500);
            this.standartListControl.TabIndex = 0;
            // 
            // ExaminationTemplateTicketControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.standartListControl);
            this.Name = "ExaminationTemplateTicketControl";
            this.Size = new System.Drawing.Size(800, 500);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.StandartListControl standartListControl;
    }
}
