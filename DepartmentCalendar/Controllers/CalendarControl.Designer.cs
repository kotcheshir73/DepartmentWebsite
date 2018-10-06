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
            this.descriptionBox1 = new DepartmentWebsite.Controllers.DescriptionBox();
            this.monthView1 = new DepartmentWebsite.MonthView();
            this.SuspendLayout();
            // 
            // descriptionBox1
            // 
            this.descriptionBox1.Location = new System.Drawing.Point(981, 53);
            this.descriptionBox1.Name = "descriptionBox1";
            this.descriptionBox1.Size = new System.Drawing.Size(345, 384);
            this.descriptionBox1.TabIndex = 1;
            // 
            // monthView1
            // 
            this.monthView1.DayBackgroundColor = System.Drawing.Color.Empty;
            this.monthView1.ItemPadding = new System.Windows.Forms.Padding(2);
            this.monthView1.Location = new System.Drawing.Point(14, 53);
            this.monthView1.Name = "monthView1";
            this.monthView1.Size = new System.Drawing.Size(942, 590);
            this.monthView1.TabIndex = 0;
            this.monthView1.Text = "monthView1";
            // 
            // CalendarControl
            // 
            this.Controls.Add(this.descriptionBox1);
            this.Controls.Add(this.monthView1);
            this.Name = "CalendarControl";
            this.Size = new System.Drawing.Size(1344, 707);
            this.Load += new System.EventHandler(this.CalendarControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DepartmentWebsite.MonthView monthView1;
        private DepartmentWebsite.Controllers.DescriptionBox descriptionBox1;
    }
}
