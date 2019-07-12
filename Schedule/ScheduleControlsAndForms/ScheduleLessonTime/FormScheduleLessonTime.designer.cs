namespace ScheduleControlsAndForms.ScheduleLessonTime
{
    partial class FormScheduleLessonTime
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
            this.textBoxOrder = new System.Windows.Forms.TextBox();
            this.labelOrder = new System.Windows.Forms.Label();
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
            this.labelDateBeginLesson.Location = new System.Drawing.Point(12, 69);
            this.labelDateBeginLesson.Name = "labelDateBeginLesson";
            this.labelDateBeginLesson.Size = new System.Drawing.Size(125, 13);
            this.labelDateBeginLesson.TabIndex = 4;
            this.labelDateBeginLesson.Text = "Время начала занятия:";
            // 
            // dateTimePickerDateBeginLesson
            // 
            this.dateTimePickerDateBeginLesson.CustomFormat = "HH:mm:ss";
            this.dateTimePickerDateBeginLesson.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDateBeginLesson.Location = new System.Drawing.Point(163, 65);
            this.dateTimePickerDateBeginLesson.Name = "dateTimePickerDateBeginLesson";
            this.dateTimePickerDateBeginLesson.Size = new System.Drawing.Size(85, 20);
            this.dateTimePickerDateBeginLesson.TabIndex = 5;
            // 
            // labelDateEndLesson
            // 
            this.labelDateEndLesson.AutoSize = true;
            this.labelDateEndLesson.Location = new System.Drawing.Point(12, 109);
            this.labelDateEndLesson.Name = "labelDateEndLesson";
            this.labelDateEndLesson.Size = new System.Drawing.Size(143, 13);
            this.labelDateEndLesson.TabIndex = 6;
            this.labelDateEndLesson.Text = "Время окончания занятия:";
            // 
            // dateTimePickerDateEndLesson
            // 
            this.dateTimePickerDateEndLesson.CustomFormat = "HH:mm:ss";
            this.dateTimePickerDateEndLesson.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDateEndLesson.Location = new System.Drawing.Point(163, 105);
            this.dateTimePickerDateEndLesson.Name = "dateTimePickerDateEndLesson";
            this.dateTimePickerDateEndLesson.Size = new System.Drawing.Size(85, 20);
            this.dateTimePickerDateEndLesson.TabIndex = 7;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(173, 140);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 9;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(92, 140);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // textBoxOrder
            // 
            this.textBoxOrder.Location = new System.Drawing.Point(135, 36);
            this.textBoxOrder.MaxLength = 150;
            this.textBoxOrder.Name = "textBoxOrder";
            this.textBoxOrder.Size = new System.Drawing.Size(113, 20);
            this.textBoxOrder.TabIndex = 3;
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.Location = new System.Drawing.Point(12, 39);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(117, 13);
            this.labelOrder.TabIndex = 2;
            this.labelOrder.Text = "Порядок следования:";
            // 
            // ScheduleLessonTimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 173);
            this.Controls.Add(this.textBoxOrder);
            this.Controls.Add(this.labelOrder);
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
            this.Load += new System.EventHandler(this.FormScheduleLessonTime_Load);
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
        private System.Windows.Forms.TextBox textBoxOrder;
        private System.Windows.Forms.Label labelOrder;
    }
}