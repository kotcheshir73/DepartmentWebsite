﻿namespace DepartmentDesktop.Views.Lecturer.Progress
{
    partial class ProgressCursControl
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
            this.standartControl = new DepartmentDesktop.Controllers.StandartControl();
            this.SuspendLayout();
            // 
            // standartControl
            // 
            this.standartControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.standartControl.Location = new System.Drawing.Point(0, 0);
            this.standartControl.Name = "standartControl";
            this.standartControl.Size = new System.Drawing.Size(589, 380);
            this.standartControl.TabIndex = 0;
            // 
            // ProgressCursControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.standartControl);
            this.Name = "ProgressCursControl";
            this.Size = new System.Drawing.Size(589, 380);
            this.ResumeLayout(false);

        }

        #endregion

        private Controllers.StandartControl standartControl;
    }
}