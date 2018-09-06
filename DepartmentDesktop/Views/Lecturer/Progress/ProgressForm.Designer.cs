namespace DepartmentDesktop.Views.EducationalProcess.Progress
{
    partial class ProgressForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDisciplines = new System.Windows.Forms.ComboBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageLectures = new System.Windows.Forms.TabPage();
            this.tabPageLabs = new System.Windows.Forms.TabPage();
            this.tabPagePractices = new System.Windows.Forms.TabPage();
            this.tabPageCourseWorks = new System.Windows.Forms.TabPage();
            this.buttonClose = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Дисциплина";
            // 
            // comboBoxDisciplines
            // 
            this.comboBoxDisciplines.FormattingEnabled = true;
            this.comboBoxDisciplines.Location = new System.Drawing.Point(98, 13);
            this.comboBoxDisciplines.Name = "comboBoxDisciplines";
            this.comboBoxDisciplines.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDisciplines.TabIndex = 2;
            this.comboBoxDisciplines.SelectedIndexChanged += new System.EventHandler(this.comboBoxDisciplines_SelectedIndexChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageLectures);
            this.tabControl.Controls.Add(this.tabPageLabs);
            this.tabControl.Controls.Add(this.tabPagePractices);
            this.tabControl.Controls.Add(this.tabPageCourseWorks);
            this.tabControl.Location = new System.Drawing.Point(12, 40);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(816, 312);
            this.tabControl.TabIndex = 4;
            // 
            // tabPageLectures
            // 
            this.tabPageLectures.Location = new System.Drawing.Point(4, 22);
            this.tabPageLectures.Name = "tabPageLectures";
            this.tabPageLectures.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLectures.Size = new System.Drawing.Size(808, 286);
            this.tabPageLectures.TabIndex = 0;
            this.tabPageLectures.Text = "Лекции";
            this.tabPageLectures.UseVisualStyleBackColor = true;
            // 
            // tabPageLabs
            // 
            this.tabPageLabs.Location = new System.Drawing.Point(4, 22);
            this.tabPageLabs.Name = "tabPageLabs";
            this.tabPageLabs.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLabs.Size = new System.Drawing.Size(808, 286);
            this.tabPageLabs.TabIndex = 1;
            this.tabPageLabs.Text = "Лабораторные";
            this.tabPageLabs.UseVisualStyleBackColor = true;
            // 
            // tabPagePractices
            // 
            this.tabPagePractices.Location = new System.Drawing.Point(4, 22);
            this.tabPagePractices.Name = "tabPagePractices";
            this.tabPagePractices.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePractices.Size = new System.Drawing.Size(808, 286);
            this.tabPagePractices.TabIndex = 2;
            this.tabPagePractices.Text = "Практики";
            this.tabPagePractices.UseVisualStyleBackColor = true;
            // 
            // tabPageCourseWorks
            // 
            this.tabPageCourseWorks.Location = new System.Drawing.Point(4, 22);
            this.tabPageCourseWorks.Name = "tabPageCourseWorks";
            this.tabPageCourseWorks.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCourseWorks.Size = new System.Drawing.Size(808, 286);
            this.tabPageCourseWorks.TabIndex = 3;
            this.tabPageCourseWorks.Text = "Курсовые р/п";
            this.tabPageCourseWorks.UseVisualStyleBackColor = true;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(749, 358);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 34;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 391);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.comboBoxDisciplines);
            this.Controls.Add(this.label1);
            this.Name = "ProgressForm";
            this.Text = "Успеваемость";
            this.Load += new System.EventHandler(this.ProgressForm_Load);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxDisciplines;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageLectures;
        private System.Windows.Forms.TabPage tabPageLabs;
        private System.Windows.Forms.TabPage tabPagePractices;
        private System.Windows.Forms.TabPage tabPageCourseWorks;
        private System.Windows.Forms.Button buttonClose;
    }
}