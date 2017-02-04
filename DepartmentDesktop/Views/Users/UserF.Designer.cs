namespace DepartmentDesktop.Views.Users
{
    partial class UserF
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
            this.labelId = new System.Windows.Forms.Label();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.labelLogin = new System.Windows.Forms.Label();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxConfirm = new System.Windows.Forms.TextBox();
            this.labelConfirm = new System.Windows.Forms.Label();
            this.pictureBoxAvatar = new System.Windows.Forms.PictureBox();
            this.labelDateLastVisit = new System.Windows.Forms.Label();
            this.labelCreate = new System.Windows.Forms.Label();
            this.textBoxRule = new System.Windows.Forms.TextBox();
            this.labelRule = new System.Windows.Forms.Label();
            this.textBoxStudent = new System.Windows.Forms.TextBox();
            this.labelStudent = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.labelLecturer = new System.Windows.Forms.Label();
            this.checkBoxBan = new System.Windows.Forms.CheckBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // labelId
            // 
            this.labelId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelId.AutoSize = true;
            this.labelId.Location = new System.Drawing.Point(387, 15);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(29, 13);
            this.labelId.TabIndex = 0;
            this.labelId.Text = "Код:";
            // 
            // textBoxId
            // 
            this.textBoxId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxId.Location = new System.Drawing.Point(422, 12);
            this.textBoxId.MaxLength = 5;
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.ReadOnly = true;
            this.textBoxId.Size = new System.Drawing.Size(50, 20);
            this.textBoxId.TabIndex = 1;
            this.textBoxId.Text = "0000";
            this.textBoxId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(32, 54);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(41, 13);
            this.labelLogin.TabIndex = 2;
            this.labelLogin.Text = "Логин:";
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(129, 51);
            this.textBoxLogin.MaxLength = 100;
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(200, 20);
            this.textBoxLogin.TabIndex = 3;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(32, 80);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(48, 13);
            this.labelPassword.TabIndex = 4;
            this.labelPassword.Text = "Пароль:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(129, 77);
            this.textBoxPassword.MaxLength = 20;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(200, 20);
            this.textBoxPassword.TabIndex = 5;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // textBoxConfirm
            // 
            this.textBoxConfirm.Location = new System.Drawing.Point(129, 103);
            this.textBoxConfirm.MaxLength = 20;
            this.textBoxConfirm.Name = "textBoxConfirm";
            this.textBoxConfirm.Size = new System.Drawing.Size(200, 20);
            this.textBoxConfirm.TabIndex = 7;
            this.textBoxConfirm.UseSystemPasswordChar = true;
            // 
            // labelConfirm
            // 
            this.labelConfirm.AutoSize = true;
            this.labelConfirm.Location = new System.Drawing.Point(32, 106);
            this.labelConfirm.Name = "labelConfirm";
            this.labelConfirm.Size = new System.Drawing.Size(91, 13);
            this.labelConfirm.TabIndex = 6;
            this.labelConfirm.Text = "Подтверждение:";
            // 
            // pictureBoxAvatar
            // 
            this.pictureBoxAvatar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxAvatar.Location = new System.Drawing.Point(372, 38);
            this.pictureBoxAvatar.Name = "pictureBoxAvatar";
            this.pictureBoxAvatar.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxAvatar.TabIndex = 8;
            this.pictureBoxAvatar.TabStop = false;
            // 
            // labelDateLastVisit
            // 
            this.labelDateLastVisit.AutoSize = true;
            this.labelDateLastVisit.Location = new System.Drawing.Point(221, 15);
            this.labelDateLastVisit.Name = "labelDateLastVisit";
            this.labelDateLastVisit.Size = new System.Drawing.Size(26, 13);
            this.labelDateLastVisit.TabIndex = 9;
            this.labelDateLastVisit.Text = "Visit";
            // 
            // labelCreate
            // 
            this.labelCreate.AutoSize = true;
            this.labelCreate.Location = new System.Drawing.Point(32, 15);
            this.labelCreate.Name = "labelCreate";
            this.labelCreate.Size = new System.Drawing.Size(38, 13);
            this.labelCreate.TabIndex = 10;
            this.labelCreate.Text = "Create";
            // 
            // textBoxRule
            // 
            this.textBoxRule.Location = new System.Drawing.Point(129, 129);
            this.textBoxRule.MaxLength = 20;
            this.textBoxRule.Name = "textBoxRule";
            this.textBoxRule.Size = new System.Drawing.Size(200, 20);
            this.textBoxRule.TabIndex = 12;
            this.textBoxRule.UseSystemPasswordChar = true;
            // 
            // labelRule
            // 
            this.labelRule.AutoSize = true;
            this.labelRule.Location = new System.Drawing.Point(32, 132);
            this.labelRule.Name = "labelRule";
            this.labelRule.Size = new System.Drawing.Size(35, 13);
            this.labelRule.TabIndex = 11;
            this.labelRule.Text = "Роль:";
            // 
            // textBoxStudent
            // 
            this.textBoxStudent.Location = new System.Drawing.Point(129, 155);
            this.textBoxStudent.MaxLength = 20;
            this.textBoxStudent.Name = "textBoxStudent";
            this.textBoxStudent.Size = new System.Drawing.Size(200, 20);
            this.textBoxStudent.TabIndex = 14;
            this.textBoxStudent.UseSystemPasswordChar = true;
            // 
            // labelStudent
            // 
            this.labelStudent.AutoSize = true;
            this.labelStudent.Location = new System.Drawing.Point(32, 158);
            this.labelStudent.Name = "labelStudent";
            this.labelStudent.Size = new System.Drawing.Size(50, 13);
            this.labelStudent.TabIndex = 13;
            this.labelStudent.Text = "Студент:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(129, 181);
            this.textBox2.MaxLength = 20;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(200, 20);
            this.textBox2.TabIndex = 16;
            this.textBox2.UseSystemPasswordChar = true;
            // 
            // labelLecturer
            // 
            this.labelLecturer.AutoSize = true;
            this.labelLecturer.Location = new System.Drawing.Point(32, 184);
            this.labelLecturer.Name = "labelLecturer";
            this.labelLecturer.Size = new System.Drawing.Size(89, 13);
            this.labelLecturer.TabIndex = 15;
            this.labelLecturer.Text = "Преподаватель:";
            // 
            // checkBoxBan
            // 
            this.checkBoxBan.AutoSize = true;
            this.checkBoxBan.Location = new System.Drawing.Point(372, 157);
            this.checkBoxBan.Name = "checkBoxBan";
            this.checkBoxBan.Size = new System.Drawing.Size(104, 17);
            this.checkBoxBan.TabIndex = 17;
            this.checkBoxBan.Text = "Заблокировать";
            this.checkBoxBan.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(135, 218);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 18;
            this.buttonOK.Text = "Сохранить";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(245, 218);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 19;
            this.buttonCancel.Text = "Отменить";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // UserF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 262);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.checkBoxBan);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.labelLecturer);
            this.Controls.Add(this.textBoxStudent);
            this.Controls.Add(this.labelStudent);
            this.Controls.Add(this.textBoxRule);
            this.Controls.Add(this.labelRule);
            this.Controls.Add(this.labelCreate);
            this.Controls.Add(this.labelDateLastVisit);
            this.Controls.Add(this.pictureBoxAvatar);
            this.Controls.Add(this.textBoxConfirm);
            this.Controls.Add(this.labelConfirm);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.textBoxId);
            this.Controls.Add(this.labelId);
            this.Name = "UserF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Пользователь";
            this.Load += new System.EventHandler(this.UserF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxConfirm;
        private System.Windows.Forms.Label labelConfirm;
        private System.Windows.Forms.PictureBox pictureBoxAvatar;
        private System.Windows.Forms.Label labelDateLastVisit;
        private System.Windows.Forms.Label labelCreate;
        private System.Windows.Forms.TextBox textBoxRule;
        private System.Windows.Forms.Label labelRule;
        private System.Windows.Forms.TextBox textBoxStudent;
        private System.Windows.Forms.Label labelStudent;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label labelLecturer;
        private System.Windows.Forms.CheckBox checkBoxBan;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}