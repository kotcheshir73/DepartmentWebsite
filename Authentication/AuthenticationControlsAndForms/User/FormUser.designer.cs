﻿namespace AuthenticationControlsAndForms.User
{
	partial class FormUser
	{
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
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.labelLogin = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.comboBoxStudent = new System.Windows.Forms.ComboBox();
            this.labelStudent = new System.Windows.Forms.Label();
            this.comboBoxLecturer = new System.Windows.Forms.ComboBox();
            this.labelLecturer = new System.Windows.Forms.Label();
            this.checkBoxBanned = new System.Windows.Forms.CheckBox();
            this.pictureBoxAvatar = new System.Windows.Forms.PictureBox();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.checkBoxBanned);
            this.panelMain.Controls.Add(this.labelLogin);
            this.panelMain.Controls.Add(this.comboBoxLecturer);
            this.panelMain.Controls.Add(this.textBoxLogin);
            this.panelMain.Controls.Add(this.labelLecturer);
            this.panelMain.Controls.Add(this.labelPassword);
            this.panelMain.Controls.Add(this.comboBoxStudent);
            this.panelMain.Controls.Add(this.textBoxPassword);
            this.panelMain.Controls.Add(this.labelStudent);
            this.panelMain.Controls.Add(this.pictureBoxAvatar);
            this.panelMain.Controls.Add(this.buttonUpload);
            this.panelMain.Size = new System.Drawing.Size(535, 254);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(535, 36);
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(107, 14);
            this.textBoxLogin.MaxLength = 100;
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(240, 20);
            this.textBoxLogin.TabIndex = 1;
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(12, 17);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(45, 13);
            this.labelLogin.TabIndex = 0;
            this.labelLogin.Text = "Логин*:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(107, 50);
            this.textBoxPassword.MaxLength = 20;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(240, 20);
            this.textBoxPassword.TabIndex = 3;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(12, 53);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(52, 13);
            this.labelPassword.TabIndex = 2;
            this.labelPassword.Text = "Пароль*:";
            // 
            // buttonUpload
            // 
            this.buttonUpload.Location = new System.Drawing.Point(404, 182);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(100, 25);
            this.buttonUpload.TabIndex = 12;
            this.buttonUpload.Text = "Загрузить";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.ButtonUpload_Click);
            // 
            // comboBoxStudent
            // 
            this.comboBoxStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudent.FormattingEnabled = true;
            this.comboBoxStudent.Location = new System.Drawing.Point(107, 129);
            this.comboBoxStudent.Name = "comboBoxStudent";
            this.comboBoxStudent.Size = new System.Drawing.Size(240, 21);
            this.comboBoxStudent.TabIndex = 7;
            // 
            // labelStudent
            // 
            this.labelStudent.AutoSize = true;
            this.labelStudent.Location = new System.Drawing.Point(12, 132);
            this.labelStudent.Name = "labelStudent";
            this.labelStudent.Size = new System.Drawing.Size(50, 13);
            this.labelStudent.TabIndex = 6;
            this.labelStudent.Text = "Студент:";
            // 
            // comboBoxLecturer
            // 
            this.comboBoxLecturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLecturer.FormattingEnabled = true;
            this.comboBoxLecturer.Location = new System.Drawing.Point(107, 171);
            this.comboBoxLecturer.Name = "comboBoxLecturer";
            this.comboBoxLecturer.Size = new System.Drawing.Size(240, 21);
            this.comboBoxLecturer.TabIndex = 9;
            // 
            // labelLecturer
            // 
            this.labelLecturer.AutoSize = true;
            this.labelLecturer.Location = new System.Drawing.Point(12, 174);
            this.labelLecturer.Name = "labelLecturer";
            this.labelLecturer.Size = new System.Drawing.Size(89, 13);
            this.labelLecturer.TabIndex = 8;
            this.labelLecturer.Text = "Преподаватель:";
            // 
            // checkBoxBanned
            // 
            this.checkBoxBanned.AutoSize = true;
            this.checkBoxBanned.Location = new System.Drawing.Point(122, 218);
            this.checkBoxBanned.Name = "checkBoxBanned";
            this.checkBoxBanned.Size = new System.Drawing.Size(211, 17);
            this.checkBoxBanned.TabIndex = 10;
            this.checkBoxBanned.Text = "Включить блокировку пользователя";
            this.checkBoxBanned.UseVisualStyleBackColor = true;
            // 
            // pictureBoxAvatar
            // 
            this.pictureBoxAvatar.Location = new System.Drawing.Point(376, 14);
            this.pictureBoxAvatar.Name = "pictureBoxAvatar";
            this.pictureBoxAvatar.Size = new System.Drawing.Size(150, 150);
            this.pictureBoxAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxAvatar.TabIndex = 13;
            this.pictureBoxAvatar.TabStop = false;
            // 
            // FormUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 290);
            this.Name = "FormUser";
            this.Text = "Пользователь";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxLogin;
		private System.Windows.Forms.Label labelLogin;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.Label labelPassword;
		private System.Windows.Forms.Button buttonUpload;
		private System.Windows.Forms.PictureBox pictureBoxAvatar;
		private System.Windows.Forms.ComboBox comboBoxStudent;
		private System.Windows.Forms.Label labelStudent;
		private System.Windows.Forms.ComboBox comboBoxLecturer;
		private System.Windows.Forms.Label labelLecturer;
		private System.Windows.Forms.CheckBox checkBoxBanned;
	}
}