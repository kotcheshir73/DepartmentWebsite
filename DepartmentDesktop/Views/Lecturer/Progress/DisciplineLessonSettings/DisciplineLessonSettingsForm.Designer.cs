namespace DepartmentDesktop.Views.Lecturer
{
    partial class DisciplineLessonSettingsForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxOrder = new System.Windows.Forms.TextBox();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxLessonType = new System.Windows.Forms.ComboBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageLesson = new System.Windows.Forms.TabPage();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.tabPageTasks = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tabPageLesson.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Порядок*:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Кол-во пар*:";
            // 
            // textBoxOrder
            // 
            this.textBoxOrder.Location = new System.Drawing.Point(77, 149);
            this.textBoxOrder.Name = "textBoxOrder";
            this.textBoxOrder.Size = new System.Drawing.Size(111, 20);
            this.textBoxOrder.TabIndex = 4;
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(88, 123);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(100, 20);
            this.textBoxCount.TabIndex = 5;
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(317, 250);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 33;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(464, 250);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 34;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(236, 250);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 32;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Описание:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(16, 73);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(523, 47);
            this.textBoxDescription.TabIndex = 36;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "Тип занятия*:";
            // 
            // comboBoxLessonType
            // 
            this.comboBoxLessonType.FormattingEnabled = true;
            this.comboBoxLessonType.Location = new System.Drawing.Point(88, 10);
            this.comboBoxLessonType.Name = "comboBoxLessonType";
            this.comboBoxLessonType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxLessonType.TabIndex = 38;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageLesson);
            this.tabControl.Controls.Add(this.tabPageTasks);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(560, 307);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageLesson
            // 
            this.tabPageLesson.Controls.Add(this.dateTimePicker);
            this.tabPageLesson.Controls.Add(this.label2);
            this.tabPageLesson.Controls.Add(this.label4);
            this.tabPageLesson.Controls.Add(this.textBoxTitle);
            this.tabPageLesson.Controls.Add(this.label1);
            this.tabPageLesson.Controls.Add(this.buttonSaveAndClose);
            this.tabPageLesson.Controls.Add(this.buttonClose);
            this.tabPageLesson.Controls.Add(this.comboBoxLessonType);
            this.tabPageLesson.Controls.Add(this.buttonSave);
            this.tabPageLesson.Controls.Add(this.label6);
            this.tabPageLesson.Controls.Add(this.label3);
            this.tabPageLesson.Controls.Add(this.textBoxDescription);
            this.tabPageLesson.Controls.Add(this.textBoxOrder);
            this.tabPageLesson.Controls.Add(this.label5);
            this.tabPageLesson.Controls.Add(this.textBoxCount);
            this.tabPageLesson.Location = new System.Drawing.Point(4, 22);
            this.tabPageLesson.Name = "tabPageLesson";
            this.tabPageLesson.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLesson.Size = new System.Drawing.Size(552, 281);
            this.tabPageLesson.TabIndex = 0;
            this.tabPageLesson.Text = "Занятие";
            this.tabPageLesson.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(118, 175);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker.TabIndex = 42;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "Дата проведения:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "Тема*:";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(60, 34);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(479, 20);
            this.textBoxTitle.TabIndex = 39;
            // 
            // tabPageTasks
            // 
            this.tabPageTasks.Location = new System.Drawing.Point(4, 22);
            this.tabPageTasks.Name = "tabPageTasks";
            this.tabPageTasks.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTasks.Size = new System.Drawing.Size(552, 281);
            this.tabPageTasks.TabIndex = 1;
            this.tabPageTasks.Text = "Задания";
            this.tabPageTasks.UseVisualStyleBackColor = true;
            // 
            // DisciplineLessonSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 307);
            this.Controls.Add(this.tabControl);
            this.Name = "DisciplineLessonSettingsForm";
            this.Text = "Занятие";
            this.Load += new System.EventHandler(this.DisciplineLessonTaskSettingsForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageLesson.ResumeLayout(false);
            this.tabPageLesson.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxOrder;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxLessonType;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageLesson;
        private System.Windows.Forms.TabPage tabPageTasks;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
    }
}