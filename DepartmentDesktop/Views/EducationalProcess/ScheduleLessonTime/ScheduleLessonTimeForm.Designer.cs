namespace DepartmentDesktop.Views.EducationalProcess.ScheduleLessonTime
{
    partial class ScheduleLessonTimeForm
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.labelDateBeginLesson = new System.Windows.Forms.Label();
            this.dateTimePickerDateBeginLesson = new System.Windows.Forms.DateTimePicker();
            this.labelDateEndLesson = new System.Windows.Forms.Label();
            this.dateTimePickerDateEndLesson = new System.Windows.Forms.DateTimePicker();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(60, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Название:";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(78, 6);
            this.textBoxTitle.MaxLength = 150;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(170, 20);
            this.textBoxTitle.TabIndex = 1;
            // 
            // labelDateBeginLesson
            // 
            this.labelDateBeginLesson.AutoSize = true;
            this.labelDateBeginLesson.Location = new System.Drawing.Point(12, 36);
            this.labelDateBeginLesson.Name = "labelDateBeginLesson";
            this.labelDateBeginLesson.Size = new System.Drawing.Size(125, 13);
            this.labelDateBeginLesson.TabIndex = 2;
            this.labelDateBeginLesson.Text = "Время начала занятия:";
            // 
            // dateTimePickerDateBeginLesson
            // 
            this.dateTimePickerDateBeginLesson.CustomFormat = "HH:mm:ss";
            this.dateTimePickerDateBeginLesson.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDateBeginLesson.Location = new System.Drawing.Point(163, 32);
            this.dateTimePickerDateBeginLesson.Name = "dateTimePickerDateBeginLesson";
            this.dateTimePickerDateBeginLesson.Size = new System.Drawing.Size(85, 20);
            this.dateTimePickerDateBeginLesson.TabIndex = 3;
            // 
            // labelDateEndLesson
            // 
            this.labelDateEndLesson.AutoSize = true;
            this.labelDateEndLesson.Location = new System.Drawing.Point(12, 76);
            this.labelDateEndLesson.Name = "labelDateEndLesson";
            this.labelDateEndLesson.Size = new System.Drawing.Size(143, 13);
            this.labelDateEndLesson.TabIndex = 4;
            this.labelDateEndLesson.Text = "Время окончания занятия:";
            // 
            // dateTimePickerDateEndLesson
            // 
            this.dateTimePickerDateEndLesson.CustomFormat = "HH:mm:ss";
            this.dateTimePickerDateEndLesson.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDateEndLesson.Location = new System.Drawing.Point(163, 72);
            this.dateTimePickerDateEndLesson.Name = "dateTimePickerDateEndLesson";
            this.dateTimePickerDateEndLesson.Size = new System.Drawing.Size(85, 20);
            this.dateTimePickerDateEndLesson.TabIndex = 5;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(173, 107);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(92, 107);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // ScheduleLessonTimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 142);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.dateTimePickerDateEndLesson);
            this.Controls.Add(this.labelDateEndLesson);
            this.Controls.Add(this.dateTimePickerDateBeginLesson);
            this.Controls.Add(this.labelDateBeginLesson);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.labelTitle);
            this.Name = "ScheduleLessonTimeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Временной интервал";
            this.Load += new System.EventHandler(this.ScheduleLessonTimeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label labelDateBeginLesson;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateBeginLesson;
        private System.Windows.Forms.Label labelDateEndLesson;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateEndLesson;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
    }
}