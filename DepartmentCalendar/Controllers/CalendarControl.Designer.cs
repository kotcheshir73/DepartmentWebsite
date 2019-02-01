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
            this.panel = new System.Windows.Forms.Panel();
            this.panelDB = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(453, 20);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(881, 632);
            this.panel.TabIndex = 1;
            // 
            // panelDB
            // 
            this.panelDB.Location = new System.Drawing.Point(12, 20);
            this.panelDB.Name = "panelDB";
            this.panelDB.Size = new System.Drawing.Size(435, 632);
            this.panelDB.TabIndex = 0;
            // 
            // CalendarControl
            // 
            this.Controls.Add(this.panelDB);
            this.Controls.Add(this.panel);
            this.Name = "CalendarControl";
            this.Size = new System.Drawing.Size(1348, 668);
            this.Load += new System.EventHandler(this.CalendarControl_Load);
            this.ResumeLayout(false);

        }

        #endregion
        
        private DepartmentWebsite.Controllers.DescriptionBox descriptionBox3;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Panel panelDB;
    }
}
