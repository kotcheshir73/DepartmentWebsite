namespace DepartmentTablet
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
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.LearningProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConductedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AcceptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.GripMargin = new System.Windows.Forms.Padding(2);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LearningProcessToolStripMenuItem});
            this.menuStripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(984, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "Главное меню";
            // 
            // LearningProcessToolStripMenuItem
            // 
            this.LearningProcessToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConductedToolStripMenuItem,
            this.AcceptToolStripMenuItem});
            this.LearningProcessToolStripMenuItem.Name = "LearningProcessToolStripMenuItem";
            this.LearningProcessToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.LearningProcessToolStripMenuItem.Text = "Обучение";
            // 
            // ConductedToolStripMenuItem
            // 
            this.ConductedToolStripMenuItem.Name = "ConductedToolStripMenuItem";
            this.ConductedToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.ConductedToolStripMenuItem.Size = new System.Drawing.Size(158, 30);
            this.ConductedToolStripMenuItem.Text = "Посещаемость";
            this.ConductedToolStripMenuItem.Click += new System.EventHandler(this.ConductedToolStripMenuItem_Click);
            // 
            // AcceptToolStripMenuItem
            // 
            this.AcceptToolStripMenuItem.Name = "AcceptToolStripMenuItem";
            this.AcceptToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.AcceptToolStripMenuItem.Size = new System.Drawing.Size(158, 30);
            this.AcceptToolStripMenuItem.Text = "Успеваемость";
            this.AcceptToolStripMenuItem.Click += new System.EventHandler(this.AcceptToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.menuStripMain);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Кафедральный портал";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem LearningProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ConductedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AcceptToolStripMenuItem;
    }
}