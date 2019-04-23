namespace LearningProgressControlsAndForms.Services
{
    partial class FormCreateAssignTasks
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.buttonForm = new System.Windows.Forms.Button();
            this.dateTimePickerDateAccept = new System.Windows.Forms.DateTimePicker();
            this.labelDateAccept = new System.Windows.Forms.Label();
            this.comboBoxDisciplineLessonTask = new System.Windows.Forms.ComboBox();
            this.labelDisciplineLessonTask = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.buttonApply = new System.Windows.Forms.Button();
            this.dataGridViewAccepts = new System.Windows.Forms.DataGridView();
            this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStudent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccepts)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.buttonForm);
            this.panelTop.Controls.Add(this.dateTimePickerDateAccept);
            this.panelTop.Controls.Add(this.labelDateAccept);
            this.panelTop.Controls.Add(this.comboBoxDisciplineLessonTask);
            this.panelTop.Controls.Add(this.labelDisciplineLessonTask);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(884, 34);
            this.panelTop.TabIndex = 0;
            // 
            // buttonForm
            // 
            this.buttonForm.Location = new System.Drawing.Point(558, 4);
            this.buttonForm.Name = "buttonForm";
            this.buttonForm.Size = new System.Drawing.Size(115, 23);
            this.buttonForm.TabIndex = 2;
            this.buttonForm.Text = "Сформировать";
            this.buttonForm.UseVisualStyleBackColor = true;
            this.buttonForm.Click += new System.EventHandler(this.ButtonForm_Click);
            // 
            // dateTimePickerDateAccept
            // 
            this.dateTimePickerDateAccept.Location = new System.Drawing.Point(385, 6);
            this.dateTimePickerDateAccept.Name = "dateTimePickerDateAccept";
            this.dateTimePickerDateAccept.Size = new System.Drawing.Size(149, 20);
            this.dateTimePickerDateAccept.TabIndex = 1;
            // 
            // labelDateAccept
            // 
            this.labelDateAccept.AutoSize = true;
            this.labelDateAccept.Location = new System.Drawing.Point(303, 9);
            this.labelDateAccept.Name = "labelDateAccept";
            this.labelDateAccept.Size = new System.Drawing.Size(76, 13);
            this.labelDateAccept.TabIndex = 0;
            this.labelDateAccept.Text = "Дата выдачи:";
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
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.buttonApply);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 481);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(884, 30);
            this.panelBottom.TabIndex = 1;
            // 
            // buttonApply
            // 
            this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApply.Location = new System.Drawing.Point(797, 3);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 0;
            this.buttonApply.Text = "Применть";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.ButtonApply_Click);
            // 
            // dataGridViewAccepts
            // 
            this.dataGridViewAccepts.AllowUserToAddRows = false;
            this.dataGridViewAccepts.AllowUserToDeleteRows = false;
            this.dataGridViewAccepts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAccepts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnId,
            this.ColumnStudent,
            this.ColumnTask,
            this.ColumnComment});
            this.dataGridViewAccepts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewAccepts.Location = new System.Drawing.Point(0, 34);
            this.dataGridViewAccepts.Name = "dataGridViewAccepts";
            this.dataGridViewAccepts.RowHeadersVisible = false;
            this.dataGridViewAccepts.Size = new System.Drawing.Size(884, 447);
            this.dataGridViewAccepts.TabIndex = 2;
            // 
            // ColumnId
            // 
            this.ColumnId.HeaderText = "ColumnId";
            this.ColumnId.Name = "ColumnId";
            this.ColumnId.Visible = false;
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
            this.ColumnTask.Width = 400;
            // 
            // ColumnComment
            // 
            this.ColumnComment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnComment.HeaderText = "Комментарий";
            this.ColumnComment.Name = "ColumnComment";
            // 
            // CreateAssignTasksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 511);
            this.Controls.Add(this.dataGridViewAccepts);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "CreateAssignTasksForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выдача заданий";
            this.Load += new System.EventHandler(this.FormCreateAssignTasks_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccepts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelDateAccept;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateAccept;
        private System.Windows.Forms.Button buttonForm;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.ComboBox comboBoxDisciplineLessonTask;
        private System.Windows.Forms.Label labelDisciplineLessonTask;
        private System.Windows.Forms.DataGridView dataGridViewAccepts;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStudent;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTask;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnComment;
    }
}