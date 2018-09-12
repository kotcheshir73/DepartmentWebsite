namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson.DisciplineLessonTask
{
    partial class FormDisciplineLessonTasksForm
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
            this.checkBoxMaxBall = new System.Windows.Forms.CheckBox();
            this.labelTaskTemplate = new System.Windows.Forms.Label();
            this.checkBoxIsNecessarily = new System.Windows.Forms.CheckBox();
            this.textBoxTaskTemplate = new System.Windows.Forms.TextBox();
            this.textBoxMaxBall = new System.Windows.Forms.TextBox();
            this.labelMaxBall = new System.Windows.Forms.Label();
            this.groupBoxTasks = new System.Windows.Forms.GroupBox();
            this.buttonForm = new System.Windows.Forms.Button();
            this.labelCountTasks = new System.Windows.Forms.Label();
            this.textBoxCountTasks = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // checkBoxMaxBall
            // 
            this.checkBoxMaxBall.AutoSize = true;
            this.checkBoxMaxBall.Location = new System.Drawing.Point(411, 118);
            this.checkBoxMaxBall.Name = "checkBoxMaxBall";
            this.checkBoxMaxBall.Size = new System.Drawing.Size(15, 14);
            this.checkBoxMaxBall.TabIndex = 6;
            this.checkBoxMaxBall.UseVisualStyleBackColor = true;
            this.checkBoxMaxBall.CheckedChanged += new System.EventHandler(this.checkBoxMaxBall_CheckedChanged);
            // 
            // labelTaskTemplate
            // 
            this.labelTaskTemplate.AutoSize = true;
            this.labelTaskTemplate.Location = new System.Drawing.Point(12, 9);
            this.labelTaskTemplate.Name = "labelTaskTemplate";
            this.labelTaskTemplate.Size = new System.Drawing.Size(98, 13);
            this.labelTaskTemplate.TabIndex = 0;
            this.labelTaskTemplate.Text = "Шаблон задания*:";
            // 
            // checkBoxIsNecessarily
            // 
            this.checkBoxIsNecessarily.AutoSize = true;
            this.checkBoxIsNecessarily.Location = new System.Drawing.Point(363, 71);
            this.checkBoxIsNecessarily.Name = "checkBoxIsNecessarily";
            this.checkBoxIsNecessarily.Size = new System.Drawing.Size(144, 17);
            this.checkBoxIsNecessarily.TabIndex = 4;
            this.checkBoxIsNecessarily.Text = "Обязательное задание";
            this.checkBoxIsNecessarily.UseVisualStyleBackColor = true;
            // 
            // textBoxTaskTemplate
            // 
            this.textBoxTaskTemplate.Location = new System.Drawing.Point(136, 6);
            this.textBoxTaskTemplate.Name = "textBoxTaskTemplate";
            this.textBoxTaskTemplate.Size = new System.Drawing.Size(210, 20);
            this.textBoxTaskTemplate.TabIndex = 1;
            this.textBoxTaskTemplate.Text = "[N]";
            // 
            // textBoxMaxBall
            // 
            this.textBoxMaxBall.Enabled = false;
            this.textBoxMaxBall.Location = new System.Drawing.Point(432, 116);
            this.textBoxMaxBall.Name = "textBoxMaxBall";
            this.textBoxMaxBall.Size = new System.Drawing.Size(72, 20);
            this.textBoxMaxBall.TabIndex = 7;
            // 
            // labelMaxBall
            // 
            this.labelMaxBall.AutoSize = true;
            this.labelMaxBall.Location = new System.Drawing.Point(363, 100);
            this.labelMaxBall.Name = "labelMaxBall";
            this.labelMaxBall.Size = new System.Drawing.Size(116, 13);
            this.labelMaxBall.TabIndex = 5;
            this.labelMaxBall.Text = "Максимальный балл:";
            // 
            // groupBoxTasks
            // 
            this.groupBoxTasks.Location = new System.Drawing.Point(12, 58);
            this.groupBoxTasks.Name = "groupBoxTasks";
            this.groupBoxTasks.Size = new System.Drawing.Size(345, 375);
            this.groupBoxTasks.TabIndex = 8;
            this.groupBoxTasks.TabStop = false;
            this.groupBoxTasks.Text = "Описания заданий";
            // 
            // buttonForm
            // 
            this.buttonForm.Location = new System.Drawing.Point(366, 176);
            this.buttonForm.Name = "buttonForm";
            this.buttonForm.Size = new System.Drawing.Size(141, 23);
            this.buttonForm.TabIndex = 9;
            this.buttonForm.Text = "Сформировать";
            this.buttonForm.UseVisualStyleBackColor = true;
            this.buttonForm.Click += new System.EventHandler(this.buttonForm_Click);
            // 
            // labelCountTasks
            // 
            this.labelCountTasks.AutoSize = true;
            this.labelCountTasks.Location = new System.Drawing.Point(12, 35);
            this.labelCountTasks.Name = "labelCountTasks";
            this.labelCountTasks.Size = new System.Drawing.Size(118, 13);
            this.labelCountTasks.TabIndex = 2;
            this.labelCountTasks.Text = "Количество заданий*:";
            // 
            // textBoxCountTasks
            // 
            this.textBoxCountTasks.Location = new System.Drawing.Point(136, 32);
            this.textBoxCountTasks.Name = "textBoxCountTasks";
            this.textBoxCountTasks.Size = new System.Drawing.Size(210, 20);
            this.textBoxCountTasks.TabIndex = 3;
            this.textBoxCountTasks.TextChanged += new System.EventHandler(this.textBoxCountTasks_TextChanged);
            // 
            // FormDisciplineLessonTasksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 441);
            this.Controls.Add(this.labelCountTasks);
            this.Controls.Add(this.textBoxCountTasks);
            this.Controls.Add(this.buttonForm);
            this.Controls.Add(this.groupBoxTasks);
            this.Controls.Add(this.checkBoxMaxBall);
            this.Controls.Add(this.labelTaskTemplate);
            this.Controls.Add(this.checkBoxIsNecessarily);
            this.Controls.Add(this.textBoxTaskTemplate);
            this.Controls.Add(this.textBoxMaxBall);
            this.Controls.Add(this.labelMaxBall);
            this.Name = "FormDisciplineLessonTasksForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Формирование заданий";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxMaxBall;
        private System.Windows.Forms.Label labelTaskTemplate;
        private System.Windows.Forms.CheckBox checkBoxIsNecessarily;
        private System.Windows.Forms.TextBox textBoxTaskTemplate;
        private System.Windows.Forms.TextBox textBoxMaxBall;
        private System.Windows.Forms.Label labelMaxBall;
        private System.Windows.Forms.GroupBox groupBoxTasks;
        private System.Windows.Forms.Button buttonForm;
        private System.Windows.Forms.Label labelCountTasks;
        private System.Windows.Forms.TextBox textBoxCountTasks;
    }
}