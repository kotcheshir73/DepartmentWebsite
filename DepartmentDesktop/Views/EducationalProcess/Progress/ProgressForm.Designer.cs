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
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxDisciplines = new System.Windows.Forms.ComboBox();
            this.comboBoxStudentGroups = new System.Windows.Forms.ComboBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageLectures = new System.Windows.Forms.TabPage();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ColumnDisciplineLessonTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl.SuspendLayout();
            this.tabPageLectures.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Дисциплина";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Группа";
            // 
            // comboBoxDisciplines
            // 
            this.comboBoxDisciplines.FormattingEnabled = true;
            this.comboBoxDisciplines.Location = new System.Drawing.Point(118, 13);
            this.comboBoxDisciplines.Name = "comboBoxDisciplines";
            this.comboBoxDisciplines.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDisciplines.TabIndex = 2;
            // 
            // comboBoxStudentGroups
            // 
            this.comboBoxStudentGroups.FormattingEnabled = true;
            this.comboBoxStudentGroups.Location = new System.Drawing.Point(317, 12);
            this.comboBoxStudentGroups.Name = "comboBoxStudentGroups";
            this.comboBoxStudentGroups.Size = new System.Drawing.Size(121, 21);
            this.comboBoxStudentGroups.TabIndex = 3;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageLectures);
            this.tabControl.Location = new System.Drawing.Point(32, 50);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(699, 295);
            this.tabControl.TabIndex = 4;
            // 
            // tabPageLectures
            // 
            this.tabPageLectures.Controls.Add(this.dataGridView);
            this.tabPageLectures.Location = new System.Drawing.Point(4, 22);
            this.tabPageLectures.Name = "tabPageLectures";
            this.tabPageLectures.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLectures.Size = new System.Drawing.Size(691, 269);
            this.tabPageLectures.TabIndex = 0;
            this.tabPageLectures.Text = "Лекции";
            this.tabPageLectures.UseVisualStyleBackColor = true;
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDisciplineLessonTitle});
            this.dataGridView.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView.Location = new System.Drawing.Point(7, 7);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(678, 256);
            this.dataGridView.TabIndex = 0;
            // 
            // ColumnDisciplineLessonTitle
            // 
            this.ColumnDisciplineLessonTitle.HeaderText = "Заголовок";
            this.ColumnDisciplineLessonTitle.Name = "ColumnDisciplineLessonTitle";
            this.ColumnDisciplineLessonTitle.Width = 600;
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 357);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.comboBoxStudentGroups);
            this.Controls.Add(this.comboBoxDisciplines);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ProgressForm";
            this.Text = "Успеваемость";
            this.Load += new System.EventHandler(this.ProgressForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageLectures.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxDisciplines;
        private System.Windows.Forms.ComboBox comboBoxStudentGroups;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageLectures;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDisciplineLessonTitle;
    }
}