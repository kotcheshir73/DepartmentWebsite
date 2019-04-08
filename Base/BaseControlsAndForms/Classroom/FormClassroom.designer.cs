namespace BaseControlsAndForms.Classroom
{
    partial class FormClassroom
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
            this.labelClassroom = new System.Windows.Forms.Label();
            this.textBoxClassroom = new System.Windows.Forms.TextBox();
            this.labelCapacity = new System.Windows.Forms.Label();
            this.textBoxCapacity = new System.Windows.Forms.TextBox();
            this.labelTypeClassroom = new System.Windows.Forms.Label();
            this.comboBoxTypeClassroom = new System.Windows.Forms.ComboBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.checkBoxNotUseInSchedule = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelClassroom
            // 
            this.labelClassroom.AutoSize = true;
            this.labelClassroom.Location = new System.Drawing.Point(12, 9);
            this.labelClassroom.Name = "labelClassroom";
            this.labelClassroom.Size = new System.Drawing.Size(67, 13);
            this.labelClassroom.TabIndex = 0;
            this.labelClassroom.Text = "Аудитория*:";
            // 
            // textBoxClassroom
            // 
            this.textBoxClassroom.Location = new System.Drawing.Point(85, 6);
            this.textBoxClassroom.Name = "textBoxClassroom";
            this.textBoxClassroom.Size = new System.Drawing.Size(100, 20);
            this.textBoxClassroom.TabIndex = 1;
            // 
            // labelCapacity
            // 
            this.labelCapacity.AutoSize = true;
            this.labelCapacity.Location = new System.Drawing.Point(223, 9);
            this.labelCapacity.Name = "labelCapacity";
            this.labelCapacity.Size = new System.Drawing.Size(83, 13);
            this.labelCapacity.TabIndex = 2;
            this.labelCapacity.Text = "Вместимость*:";
            // 
            // textBoxCapacity
            // 
            this.textBoxCapacity.Location = new System.Drawing.Point(312, 6);
            this.textBoxCapacity.MaxLength = 4;
            this.textBoxCapacity.Name = "textBoxCapacity";
            this.textBoxCapacity.Size = new System.Drawing.Size(100, 20);
            this.textBoxCapacity.TabIndex = 3;
            this.textBoxCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelTypeClassroom
            // 
            this.labelTypeClassroom.AutoSize = true;
            this.labelTypeClassroom.Location = new System.Drawing.Point(12, 48);
            this.labelTypeClassroom.Name = "labelTypeClassroom";
            this.labelTypeClassroom.Size = new System.Drawing.Size(88, 13);
            this.labelTypeClassroom.TabIndex = 4;
            this.labelTypeClassroom.Text = "Тип аудитории*:";
            // 
            // comboBoxTypeClassroom
            // 
            this.comboBoxTypeClassroom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTypeClassroom.FormattingEnabled = true;
            this.comboBoxTypeClassroom.Location = new System.Drawing.Point(106, 45);
            this.comboBoxTypeClassroom.Name = "comboBoxTypeClassroom";
            this.comboBoxTypeClassroom.Size = new System.Drawing.Size(210, 21);
            this.comboBoxTypeClassroom.TabIndex = 5;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(350, 104);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 9;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(122, 104);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(203, 104);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 8;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // checkBoxNotUseInSchedule
            // 
            this.checkBoxNotUseInSchedule.AutoSize = true;
            this.checkBoxNotUseInSchedule.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxNotUseInSchedule.Location = new System.Drawing.Point(15, 79);
            this.checkBoxNotUseInSchedule.Name = "checkBoxNotUseInSchedule";
            this.checkBoxNotUseInSchedule.Size = new System.Drawing.Size(186, 17);
            this.checkBoxNotUseInSchedule.TabIndex = 6;
            this.checkBoxNotUseInSchedule.Text = "Не использовать в расписании";
            this.checkBoxNotUseInSchedule.UseVisualStyleBackColor = true;
            // 
            // ClassroomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 138);
            this.Controls.Add(this.checkBoxNotUseInSchedule);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.comboBoxTypeClassroom);
            this.Controls.Add(this.labelTypeClassroom);
            this.Controls.Add(this.textBoxCapacity);
            this.Controls.Add(this.labelCapacity);
            this.Controls.Add(this.textBoxClassroom);
            this.Controls.Add(this.labelClassroom);
            this.Name = "ClassroomForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Аудитория";
            this.Load += new System.EventHandler(this.FormClassroom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelClassroom;
        private System.Windows.Forms.TextBox textBoxClassroom;
        private System.Windows.Forms.Label labelCapacity;
        private System.Windows.Forms.TextBox textBoxCapacity;
        private System.Windows.Forms.Label labelTypeClassroom;
        private System.Windows.Forms.ComboBox comboBoxTypeClassroom;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.CheckBox checkBoxNotUseInSchedule;
    }
}