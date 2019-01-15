using DepartmentProcessAccountingService.IServices;
using DepartmentWebsite;

namespace DepartmentCalendar.Controllers
{
    partial class CalendarControl
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
            this.descriptionBox2 = new DepartmentWebsite.Controllers.DescriptionBox();
            this.SuspendLayout();
            // 
            // descriptionBox2
            // 
            this.descriptionBox2.Location = new System.Drawing.Point(17, 13);
            this.descriptionBox2.Name = "descriptionBox2";
            this.descriptionBox2.Size = new System.Drawing.Size(364, 455);
            this.descriptionBox2.TabIndex = 0;
            // 
            // CalendarControl
            // 
            this.Controls.Add(this.descriptionBox2);
            this.Name = "CalendarControl";
            this.Size = new System.Drawing.Size(893, 563);
            this.ResumeLayout(false);

        }

        #endregion

        private DepartmentWebsite.MonthView monthView1;
        private DepartmentWebsite.Controllers.DescriptionBox descriptionBox1;
        private DepartmentWebsite.Controllers.DescriptionBox descriptionBox2;
    }
}
