namespace DepartmentDesktop.Views.LearningProgress.DisciplineLessonConducted.DisciplineLessonConductedStudent
{
    partial class FillStudentsForm
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
            this.dataGridViewStudents = new System.Windows.Forms.DataGridView();
            this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDLCId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStudentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStatus = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnBall = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonApply = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewStudents
            // 
            this.dataGridViewStudents.AllowUserToAddRows = false;
            this.dataGridViewStudents.AllowUserToDeleteRows = false;
            this.dataGridViewStudents.AllowUserToResizeRows = false;
            this.dataGridViewStudents.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStudents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnId,
            this.ColumnDLCId,
            this.ColumnStudentId,
            this.ColumnName,
            this.ColumnStatus,
            this.ColumnBall,
            this.ColumnComment});
            this.dataGridViewStudents.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridViewStudents.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridViewStudents.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewStudents.Name = "dataGridViewStudents";
            this.dataGridViewStudents.RowHeadersVisible = false;
            this.dataGridViewStudents.Size = new System.Drawing.Size(725, 486);
            this.dataGridViewStudents.TabIndex = 0;
            // 
            // ColumnId
            // 
            this.ColumnId.HeaderText = "ColumnId";
            this.ColumnId.Name = "ColumnId";
            this.ColumnId.Visible = false;
            // 
            // ColumnDLCId
            // 
            this.ColumnDLCId.HeaderText = "ColumnDLCId";
            this.ColumnDLCId.Name = "ColumnDLCId";
            this.ColumnDLCId.Visible = false;
            // 
            // ColumnStudentId
            // 
            this.ColumnStudentId.HeaderText = "ColumnStudentId";
            this.ColumnStudentId.Name = "ColumnStudentId";
            this.ColumnStudentId.Visible = false;
            // 
            // ColumnName
            // 
            this.ColumnName.FillWeight = 300F;
            this.ColumnName.HeaderText = "ФИО";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            this.ColumnName.Width = 200;
            // 
            // ColumnStatus
            // 
            this.ColumnStatus.HeaderText = "Статус";
            this.ColumnStatus.Name = "ColumnStatus";
            this.ColumnStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ColumnBall
            // 
            this.ColumnBall.HeaderText = "Балл";
            this.ColumnBall.Name = "ColumnBall";
            // 
            // ColumnComment
            // 
            this.ColumnComment.FillWeight = 300F;
            this.ColumnComment.HeaderText = "Комментарий";
            this.ColumnComment.Name = "ColumnComment";
            this.ColumnComment.Width = 300;
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(578, 492);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(135, 23);
            this.buttonApply.TabIndex = 1;
            this.buttonApply.Text = "Применть";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // DisciplineLessonConductedStudentFillStudentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 525);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.dataGridViewStudents);
            this.Name = "DisciplineLessonConductedStudentFillStudentsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Заполнение группы";
            this.Load += new System.EventHandler(this.FillStudentsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewStudents;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDLCId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStudentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBall;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnComment;
    }
}