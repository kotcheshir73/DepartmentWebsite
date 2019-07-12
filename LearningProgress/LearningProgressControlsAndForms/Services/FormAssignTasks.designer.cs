namespace LearningProgressControlsAndForms.Services
{
    partial class FormAssignTasks
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
            this.dataGridViewAccepts = new System.Windows.Forms.DataGridView();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.buttonApply = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.buttonGet = new System.Windows.Forms.Button();
            this.comboBoxDisciplineLessonTask = new System.Windows.Forms.ComboBox();
            this.labelDisciplineLessonTask = new System.Windows.Forms.Label();
            this.dateTimePickerDateAccept = new System.Windows.Forms.DateTimePicker();
            this.labelDateAccept = new System.Windows.Forms.Label();
            this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDLTId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStudentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStudent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnResult = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnBall = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccepts)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewAccepts
            // 
            this.dataGridViewAccepts.AllowUserToAddRows = false;
            this.dataGridViewAccepts.AllowUserToDeleteRows = false;
            this.dataGridViewAccepts.AllowUserToResizeRows = false;
            this.dataGridViewAccepts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAccepts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnId,
            this.ColumnDLTId,
            this.ColumnStudentId,
            this.ColumnStudent,
            this.ColumnTask,
            this.ColumnResult,
            this.ColumnBall,
            this.ColumnComment});
            this.dataGridViewAccepts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewAccepts.Location = new System.Drawing.Point(0, 34);
            this.dataGridViewAccepts.Name = "dataGridViewAccepts";
            this.dataGridViewAccepts.RowHeadersVisible = false;
            this.dataGridViewAccepts.Size = new System.Drawing.Size(984, 427);
            this.dataGridViewAccepts.TabIndex = 1;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.buttonApply);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 461);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(984, 30);
            this.panelBottom.TabIndex = 2;
            // 
            // buttonApply
            // 
            this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApply.Location = new System.Drawing.Point(897, 3);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 0;
            this.buttonApply.Text = "Применть";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.ButtonApply_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.dateTimePickerDateAccept);
            this.panelTop.Controls.Add(this.labelDateAccept);
            this.panelTop.Controls.Add(this.buttonGet);
            this.panelTop.Controls.Add(this.comboBoxDisciplineLessonTask);
            this.panelTop.Controls.Add(this.labelDisciplineLessonTask);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(984, 34);
            this.panelTop.TabIndex = 3;
            // 
            // buttonGet
            // 
            this.buttonGet.Location = new System.Drawing.Point(306, 4);
            this.buttonGet.Name = "buttonGet";
            this.buttonGet.Size = new System.Drawing.Size(115, 23);
            this.buttonGet.TabIndex = 2;
            this.buttonGet.Text = "Получить";
            this.buttonGet.UseVisualStyleBackColor = true;
            this.buttonGet.Click += new System.EventHandler(this.ButtonGet_Click);
            // 
            // comboBoxDisciplineLessonTask
            // 
            this.comboBoxDisciplineLessonTask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisciplineLessonTask.FormattingEnabled = true;
            this.comboBoxDisciplineLessonTask.Location = new System.Drawing.Point(75, 6);
            this.comboBoxDisciplineLessonTask.Name = "comboBoxDisciplineLessonTask";
            this.comboBoxDisciplineLessonTask.Size = new System.Drawing.Size(210, 21);
            this.comboBoxDisciplineLessonTask.TabIndex = 5;
            // 
            // labelDisciplineLessonTask
            // 
            this.labelDisciplineLessonTask.AutoSize = true;
            this.labelDisciplineLessonTask.Location = new System.Drawing.Point(12, 9);
            this.labelDisciplineLessonTask.Name = "labelDisciplineLessonTask";
            this.labelDisciplineLessonTask.Size = new System.Drawing.Size(57, 13);
            this.labelDisciplineLessonTask.TabIndex = 4;
            this.labelDisciplineLessonTask.Text = "Задание*:";
            // 
            // dateTimePickerDateAccept
            // 
            this.dateTimePickerDateAccept.Location = new System.Drawing.Point(527, 6);
            this.dateTimePickerDateAccept.Name = "dateTimePickerDateAccept";
            this.dateTimePickerDateAccept.Size = new System.Drawing.Size(149, 20);
            this.dateTimePickerDateAccept.TabIndex = 7;
            // 
            // labelDateAccept
            // 
            this.labelDateAccept.AutoSize = true;
            this.labelDateAccept.Location = new System.Drawing.Point(445, 9);
            this.labelDateAccept.Name = "labelDateAccept";
            this.labelDateAccept.Size = new System.Drawing.Size(76, 13);
            this.labelDateAccept.TabIndex = 6;
            this.labelDateAccept.Text = "Дата выдачи:";
            // 
            // ColumnId
            // 
            this.ColumnId.HeaderText = "ColumnId";
            this.ColumnId.Name = "ColumnId";
            this.ColumnId.Visible = false;
            // 
            // ColumnDLTId
            // 
            this.ColumnDLTId.HeaderText = "ColumnDLTId";
            this.ColumnDLTId.Name = "ColumnDLTId";
            this.ColumnDLTId.Visible = false;
            // 
            // ColumnStudentId
            // 
            this.ColumnStudentId.HeaderText = "ColumnStudentId";
            this.ColumnStudentId.Name = "ColumnStudentId";
            this.ColumnStudentId.Visible = false;
            // 
            // ColumnStudent
            // 
            this.ColumnStudent.HeaderText = "Студент";
            this.ColumnStudent.Name = "ColumnStudent";
            this.ColumnStudent.ReadOnly = true;
            this.ColumnStudent.Width = 200;
            // 
            // ColumnTask
            // 
            this.ColumnTask.HeaderText = "Задание";
            this.ColumnTask.Name = "ColumnTask";
            this.ColumnTask.Width = 300;
            // 
            // ColumnResult
            // 
            this.ColumnResult.HeaderText = "Результат";
            this.ColumnResult.Name = "ColumnResult";
            // 
            // ColumnBall
            // 
            this.ColumnBall.HeaderText = "Балл";
            this.ColumnBall.Name = "ColumnBall";
            this.ColumnBall.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnBall.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnComment
            // 
            this.ColumnComment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnComment.HeaderText = "Комментарий";
            this.ColumnComment.Name = "ColumnComment";
            // 
            // AssignTasksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 491);
            this.Controls.Add(this.dataGridViewAccepts);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "AssignTasksForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Прием заданий";
            this.Load += new System.EventHandler(this.FormAssignTasks_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccepts)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewAccepts;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button buttonGet;
        private System.Windows.Forms.ComboBox comboBoxDisciplineLessonTask;
        private System.Windows.Forms.Label labelDisciplineLessonTask;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateAccept;
        private System.Windows.Forms.Label labelDateAccept;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDLTId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStudentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStudent;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTask;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBall;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnComment;
    }
}