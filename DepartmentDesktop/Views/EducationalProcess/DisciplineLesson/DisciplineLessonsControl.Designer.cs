﻿namespace DepartmentDesktop.Views.EducationalProcess.DisciplineLesson
{
    partial class DisciplineLessonsControl
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
            this.standartControl.Location = new System.Drawing.Point(3, 3);
            this.standartControl.Name = "standartControl";
            this.standartControl.Size = new System.Drawing.Size(723, 435);
            this.standartControl.TabIndex = 0;
            // 
            // DisciplineLessonControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.standartControl);
            this.Name = "DisciplineLessonControl";
            this.Size = new System.Drawing.Size(729, 441);
            this.ResumeLayout(false);

        }

        #endregion

        private Controllers.StandartControl standartControl;
    }
}