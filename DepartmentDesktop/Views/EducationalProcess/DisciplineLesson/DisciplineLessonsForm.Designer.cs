namespace DepartmentDesktop.Views.EducationalProcess.DisciplineLesson
{
    partial class DisciplineLessonsForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageLessonsList = new System.Windows.Forms.TabPage();
            this.tabControlLessons = new System.Windows.Forms.TabControl();
            this.tabPageLectures = new System.Windows.Forms.TabPage();
            this.tabPageStudentRecords = new System.Windows.Forms.TabPage();
            this.dataGridViewDL = new System.Windows.Forms.DataGridView();
            this.ColumnStudents = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDisciplineLesson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewDLSR = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRecords = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl.SuspendLayout();
            this.tabPageLessonsList.SuspendLayout();
            this.tabControlLessons.SuspendLayout();
            this.tabPageLectures.SuspendLayout();
            this.tabPageStudentRecords.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDLSR)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageLessonsList);
            this.tabControl.Controls.Add(this.tabPageStudentRecords);
            this.tabControl.Location = new System.Drawing.Point(1, 2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(701, 398);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageLessonsList
            // 
            this.tabPageLessonsList.Controls.Add(this.tabControlLessons);
            this.tabPageLessonsList.Location = new System.Drawing.Point(4, 22);
            this.tabPageLessonsList.Name = "tabPageLessonsList";
            this.tabPageLessonsList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLessonsList.Size = new System.Drawing.Size(693, 372);
            this.tabPageLessonsList.TabIndex = 0;
            this.tabPageLessonsList.Text = "Список занятий";
            this.tabPageLessonsList.UseVisualStyleBackColor = true;
            // 
            // tabControlLessons
            // 
            this.tabControlLessons.Controls.Add(this.tabPageLectures);
            this.tabControlLessons.Location = new System.Drawing.Point(8, 7);
            this.tabControlLessons.Name = "tabControlLessons";
            this.tabControlLessons.SelectedIndex = 0;
            this.tabControlLessons.Size = new System.Drawing.Size(679, 359);
            this.tabControlLessons.TabIndex = 0;
            // 
            // tabPageLectures
            // 
            this.tabPageLectures.Controls.Add(this.dataGridViewDL);
            this.tabPageLectures.Location = new System.Drawing.Point(4, 22);
            this.tabPageLectures.Name = "tabPageLectures";
            this.tabPageLectures.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLectures.Size = new System.Drawing.Size(671, 333);
            this.tabPageLectures.TabIndex = 0;
            this.tabPageLectures.Text = "Лекции";
            this.tabPageLectures.UseVisualStyleBackColor = true;
            // 
            // tabPageStudentRecords
            // 
            this.tabPageStudentRecords.Controls.Add(this.dataGridViewDLSR);
            this.tabPageStudentRecords.Location = new System.Drawing.Point(4, 22);
            this.tabPageStudentRecords.Name = "tabPageStudentRecords";
            this.tabPageStudentRecords.Size = new System.Drawing.Size(693, 372);
            this.tabPageStudentRecords.TabIndex = 1;
            this.tabPageStudentRecords.Text = "Оценки";
            this.tabPageStudentRecords.UseVisualStyleBackColor = true;
            // 
            // dataGridViewDL
            // 
            this.dataGridViewDL.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewDL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDL.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnStudents,
            this.ColumnDisciplineLesson});
            this.dataGridViewDL.Location = new System.Drawing.Point(7, 7);
            this.dataGridViewDL.Name = "dataGridViewDL";
            this.dataGridViewDL.Size = new System.Drawing.Size(658, 320);
            this.dataGridViewDL.TabIndex = 0;
            // 
            // ColumnStudents
            // 
            this.ColumnStudents.HeaderText = "Студенты";
            this.ColumnStudents.Name = "ColumnStudents";
            this.ColumnStudents.Width = 300;
            // 
            // ColumnDisciplineLesson
            // 
            this.ColumnDisciplineLesson.HeaderText = "Занятие";
            this.ColumnDisciplineLesson.Name = "ColumnDisciplineLesson";
            this.ColumnDisciplineLesson.Width = 300;
            // 
            // dataGridViewDLSR
            // 
            this.dataGridViewDLSR.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewDLSR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDLSR.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.ColumnRecords});
            this.dataGridViewDLSR.Location = new System.Drawing.Point(8, 4);
            this.dataGridViewDLSR.Name = "dataGridViewDLSR";
            this.dataGridViewDLSR.Size = new System.Drawing.Size(682, 365);
            this.dataGridViewDLSR.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Студенты";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 300;
            // 
            // ColumnRecords
            // 
            this.ColumnRecords.HeaderText = "Оценки";
            this.ColumnRecords.Name = "ColumnRecords";
            this.ColumnRecords.Width = 300;
            // 
            // DisciplineLessonsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 400);
            this.Controls.Add(this.tabControl);
            this.Name = "DisciplineLessonsForm";
            this.Text = "Занятие";
            this.Load += new System.EventHandler(this.DisciplineLessonForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageLessonsList.ResumeLayout(false);
            this.tabControlLessons.ResumeLayout(false);
            this.tabPageLectures.ResumeLayout(false);
            this.tabPageStudentRecords.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDLSR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageLessonsList;
        private System.Windows.Forms.TabControl tabControlLessons;
        private System.Windows.Forms.TabPage tabPageLectures;
        private System.Windows.Forms.TabPage tabPageStudentRecords;
        private System.Windows.Forms.DataGridView dataGridViewDL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStudents;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDisciplineLesson;
        private System.Windows.Forms.DataGridView dataGridViewDLSR;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRecords;
    }
}