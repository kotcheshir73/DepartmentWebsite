namespace DepartmentDesktop.Views.EducationalProcess.StudentGroup
{
    partial class StudentGroupForm
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
            this.labelEducationDirection = new System.Windows.Forms.Label();
            this.comboBoxEducationDirection = new System.Windows.Forms.ComboBox();
            this.labelGroupName = new System.Windows.Forms.Label();
            this.textBoxGroupName = new System.Windows.Forms.TextBox();
            this.labelKurs = new System.Windows.Forms.Label();
            this.textBoxKurs = new System.Windows.Forms.TextBox();
            this.labelStudents = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelEducationDirection
            // 
            this.labelEducationDirection.AutoSize = true;
            this.labelEducationDirection.Location = new System.Drawing.Point(12, 9);
            this.labelEducationDirection.Name = "labelEducationDirection";
            this.labelEducationDirection.Size = new System.Drawing.Size(82, 13);
            this.labelEducationDirection.TabIndex = 0;
            this.labelEducationDirection.Text = "Направление*:";
            // 
            // comboBoxEducationDirection
            // 
            this.comboBoxEducationDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEducationDirection.FormattingEnabled = true;
            this.comboBoxEducationDirection.Location = new System.Drawing.Point(100, 6);
            this.comboBoxEducationDirection.Name = "comboBoxEducationDirection";
            this.comboBoxEducationDirection.Size = new System.Drawing.Size(320, 21);
            this.comboBoxEducationDirection.TabIndex = 1;
            // 
            // labelGroupName
            // 
            this.labelGroupName.AutoSize = true;
            this.labelGroupName.Location = new System.Drawing.Point(12, 36);
            this.labelGroupName.Name = "labelGroupName";
            this.labelGroupName.Size = new System.Drawing.Size(103, 13);
            this.labelGroupName.TabIndex = 2;
            this.labelGroupName.Text = "Название группы*:";
            // 
            // textBoxGroupName
            // 
            this.textBoxGroupName.Location = new System.Drawing.Point(121, 33);
            this.textBoxGroupName.MaxLength = 20;
            this.textBoxGroupName.Name = "textBoxGroupName";
            this.textBoxGroupName.Size = new System.Drawing.Size(133, 20);
            this.textBoxGroupName.TabIndex = 3;
            // 
            // labelKurs
            // 
            this.labelKurs.AutoSize = true;
            this.labelKurs.Location = new System.Drawing.Point(314, 36);
            this.labelKurs.Name = "labelKurs";
            this.labelKurs.Size = new System.Drawing.Size(38, 13);
            this.labelKurs.TabIndex = 4;
            this.labelKurs.Text = "Курс*:";
            // 
            // textBoxKurs
            // 
            this.textBoxKurs.Location = new System.Drawing.Point(358, 33);
            this.textBoxKurs.MaxLength = 3;
            this.textBoxKurs.Name = "textBoxKurs";
            this.textBoxKurs.Size = new System.Drawing.Size(62, 20);
            this.textBoxKurs.TabIndex = 5;
            this.textBoxKurs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelStudents
            // 
            this.labelStudents.AutoSize = true;
            this.labelStudents.Location = new System.Drawing.Point(12, 66);
            this.labelStudents.Name = "labelStudents";
            this.labelStudents.Size = new System.Drawing.Size(58, 13);
            this.labelStudents.TabIndex = 6;
            this.labelStudents.Text = "Студенты:";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(345, 347);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 8;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(264, 347);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // StudentGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 382);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelStudents);
            this.Controls.Add(this.textBoxKurs);
            this.Controls.Add(this.labelKurs);
            this.Controls.Add(this.textBoxGroupName);
            this.Controls.Add(this.labelGroupName);
            this.Controls.Add(this.comboBoxEducationDirection);
            this.Controls.Add(this.labelEducationDirection);
            this.Name = "StudentGroupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Группа";
            this.Load += new System.EventHandler(this.StudentGroupForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelEducationDirection;
        private System.Windows.Forms.ComboBox comboBoxEducationDirection;
        private System.Windows.Forms.Label labelGroupName;
        private System.Windows.Forms.TextBox textBoxGroupName;
        private System.Windows.Forms.Label labelKurs;
        private System.Windows.Forms.TextBox textBoxKurs;
        private System.Windows.Forms.Label labelStudents;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
    }
}