namespace DepartmentDesktop
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.действияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MakeTicketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.данныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.educationalProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.educationDirectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.действияToolStripMenuItem,
            this.данныеToolStripMenuItem,
            this.educationalProcessToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(784, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // действияToolStripMenuItem
            // 
            this.действияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MakeTicketsToolStripMenuItem,
            this.ScheduleToolStripMenuItem});
            this.действияToolStripMenuItem.Name = "действияToolStripMenuItem";
            this.действияToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.действияToolStripMenuItem.Text = "Сервис";
            // 
            // MakeTicketsToolStripMenuItem
            // 
            this.MakeTicketsToolStripMenuItem.Name = "MakeTicketsToolStripMenuItem";
            this.MakeTicketsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.MakeTicketsToolStripMenuItem.Text = "Генерация билетов";
            this.MakeTicketsToolStripMenuItem.Click += new System.EventHandler(this.MakeTicketsToolStripMenuItem_Click);
            // 
            // ScheduleToolStripMenuItem
            // 
            this.ScheduleToolStripMenuItem.Name = "ScheduleToolStripMenuItem";
            this.ScheduleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ScheduleToolStripMenuItem.Text = "Расписание";
            this.ScheduleToolStripMenuItem.Click += new System.EventHandler(this.ScheduleToolStripMenuItem_Click);
            // 
            // данныеToolStripMenuItem
            // 
            this.данныеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UsersToolStripMenuItem});
            this.данныеToolStripMenuItem.Name = "данныеToolStripMenuItem";
            this.данныеToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.данныеToolStripMenuItem.Text = "Данные";
            // 
            // UsersToolStripMenuItem
            // 
            this.UsersToolStripMenuItem.Name = "UsersToolStripMenuItem";
            this.UsersToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.UsersToolStripMenuItem.Text = "Пользователи";
            this.UsersToolStripMenuItem.Click += new System.EventHandler(this.UsersToolStripMenuItem_Click);
            // 
            // educationalProcessToolStripMenuItem
            // 
            this.educationalProcessToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.educationDirectionToolStripMenuItem});
            this.educationalProcessToolStripMenuItem.Name = "educationalProcessToolStripMenuItem";
            this.educationalProcessToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.educationalProcessToolStripMenuItem.Text = "Учебный процесс";
            // 
            // educationDirectionToolStripMenuItem
            // 
            this.educationDirectionToolStripMenuItem.Name = "educationDirectionToolStripMenuItem";
            this.educationDirectionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.educationDirectionToolStripMenuItem.Text = "Направления";
            this.educationDirectionToolStripMenuItem.Click += new System.EventHandler(this.educationDirectionToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 412);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главная форма";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem действияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MakeTicketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem данныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ScheduleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem educationalProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem educationDirectionToolStripMenuItem;
    }
}

