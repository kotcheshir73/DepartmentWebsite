namespace DepartmentDesktop.Views.EducationalProcess.LecturerCabinet
{
    partial class LecturerCabinetForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageAboutLecturer = new System.Windows.Forms.TabPage();
            this.tabPageDisciplines = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageAboutLecturer);
            this.tabControl1.Controls.Add(this.tabPageDisciplines);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(592, 278);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageAboutLecturer
            // 
            this.tabPageAboutLecturer.Location = new System.Drawing.Point(4, 22);
            this.tabPageAboutLecturer.Name = "tabPageAboutLecturer";
            this.tabPageAboutLecturer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAboutLecturer.Size = new System.Drawing.Size(584, 252);
            this.tabPageAboutLecturer.TabIndex = 0;
            this.tabPageAboutLecturer.Text = "О преподавателе";
            this.tabPageAboutLecturer.UseVisualStyleBackColor = true;
            // 
            // tabPageDisciplines
            // 
            this.tabPageDisciplines.Location = new System.Drawing.Point(4, 22);
            this.tabPageDisciplines.Name = "tabPageDisciplines";
            this.tabPageDisciplines.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDisciplines.Size = new System.Drawing.Size(584, 252);
            this.tabPageDisciplines.TabIndex = 1;
            this.tabPageDisciplines.Text = "Дисциплины";
            this.tabPageDisciplines.UseVisualStyleBackColor = true;
            // 
            // LecturerCabinetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 302);
            this.Controls.Add(this.tabControl1);
            this.Name = "LecturerCabinetForm";
            this.Text = "Личный кабинет преподавателя";
            this.Load += new System.EventHandler(this.LecturerCabinetForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageAboutLecturer;
        private System.Windows.Forms.TabPage tabPageDisciplines;
    }
}