namespace DepartmentTablet.Conducted
{
    partial class ControlConductedStudentMark
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.labelComment = new System.Windows.Forms.Label();
            this.checkBoxBall = new System.Windows.Forms.CheckBox();
            this.textBoxBall = new System.Windows.Forms.TextBox();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.buttonSave);
            this.panelMain.Controls.Add(this.textBoxComment);
            this.panelMain.Controls.Add(this.labelComment);
            this.panelMain.Controls.Add(this.checkBoxBall);
            this.panelMain.Controls.Add(this.textBoxBall);
            this.panelMain.Controls.Add(this.comboBoxStatus);
            this.panelMain.Controls.Add(this.labelStatus);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(150, 153);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(142, 78);
            this.textBoxComment.Multiline = true;
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(210, 54);
            this.textBoxComment.TabIndex = 5;
            // 
            // labelComment
            // 
            this.labelComment.AutoSize = true;
            this.labelComment.Location = new System.Drawing.Point(23, 81);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(80, 13);
            this.labelComment.TabIndex = 4;
            this.labelComment.Text = "Комментарий:";
            // 
            // checkBoxBall
            // 
            this.checkBoxBall.AutoSize = true;
            this.checkBoxBall.Location = new System.Drawing.Point(26, 55);
            this.checkBoxBall.Name = "checkBoxBall";
            this.checkBoxBall.Size = new System.Drawing.Size(54, 17);
            this.checkBoxBall.TabIndex = 2;
            this.checkBoxBall.Text = "Балл:";
            this.checkBoxBall.UseVisualStyleBackColor = true;
            this.checkBoxBall.CheckedChanged += new System.EventHandler(this.checkBoxBall_CheckedChanged);
            // 
            // textBoxBall
            // 
            this.textBoxBall.Enabled = false;
            this.textBoxBall.Location = new System.Drawing.Point(142, 52);
            this.textBoxBall.Name = "textBoxBall";
            this.textBoxBall.Size = new System.Drawing.Size(72, 20);
            this.textBoxBall.TabIndex = 3;
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(142, 25);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(210, 21);
            this.comboBoxStatus.TabIndex = 1;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(23, 28);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(48, 13);
            this.labelStatus.TabIndex = 0;
            this.labelStatus.Text = "Статус*:";
            // 
            // ControlConductedStudentMark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ControlConductedStudentMark";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.Label labelComment;
        private System.Windows.Forms.CheckBox checkBoxBall;
        private System.Windows.Forms.TextBox textBoxBall;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label labelStatus;
    }
}
