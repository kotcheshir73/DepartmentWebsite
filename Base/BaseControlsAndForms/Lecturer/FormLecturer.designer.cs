namespace BaseControlsAndForms.Lecturer
{
	partial class LecturerForm
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
            this.buttonUpload = new System.Windows.Forms.Button();
            this.pictureBoxPhoto = new System.Windows.Forms.PictureBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxPatronymic = new System.Windows.Forms.TextBox();
            this.labelPatronymic = new System.Windows.Forms.Label();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.labelFirstName = new System.Windows.Forms.Label();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.labelLastName = new System.Windows.Forms.Label();
            this.labelDateBirth = new System.Windows.Forms.Label();
            this.dateTimePickerDateBirth = new System.Windows.Forms.DateTimePicker();
            this.labelAddress = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.textBoxMobileNumber = new System.Windows.Forms.TextBox();
            this.labelMobileNumber = new System.Windows.Forms.Label();
            this.textBoxHomeNumber = new System.Windows.Forms.TextBox();
            this.labelHomeNumber = new System.Windows.Forms.Label();
            this.labelPost = new System.Windows.Forms.Label();
            this.labelRank = new System.Windows.Forms.Label();
            this.labelAbbreviation = new System.Windows.Forms.Label();
            this.textBoxAbbreviation = new System.Windows.Forms.TextBox();
            this.comboBoxPost = new System.Windows.Forms.ComboBox();
            this.comboBoxRank = new System.Windows.Forms.ComboBox();
            this.comboBoxRank2 = new System.Windows.Forms.ComboBox();
            this.labelRank2 = new System.Windows.Forms.Label();
            this.labelLecturerPost = new System.Windows.Forms.Label();
            this.comboBoxLecturerPost = new System.Windows.Forms.ComboBox();
            this.checkBoxOnlyForPrivate = new System.Windows.Forms.CheckBox();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.checkBoxOnlyForPrivate);
            this.panelMain.Controls.Add(this.comboBoxLecturerPost);
            this.panelMain.Controls.Add(this.labelLastName);
            this.panelMain.Controls.Add(this.labelLecturerPost);
            this.panelMain.Controls.Add(this.textBoxLastName);
            this.panelMain.Controls.Add(this.comboBoxRank2);
            this.panelMain.Controls.Add(this.labelFirstName);
            this.panelMain.Controls.Add(this.labelRank2);
            this.panelMain.Controls.Add(this.textBoxFirstName);
            this.panelMain.Controls.Add(this.comboBoxRank);
            this.panelMain.Controls.Add(this.labelPatronymic);
            this.panelMain.Controls.Add(this.comboBoxPost);
            this.panelMain.Controls.Add(this.textBoxPatronymic);
            this.panelMain.Controls.Add(this.textBoxAbbreviation);
            this.panelMain.Controls.Add(this.labelDescription);
            this.panelMain.Controls.Add(this.labelAbbreviation);
            this.panelMain.Controls.Add(this.textBoxDescription);
            this.panelMain.Controls.Add(this.labelRank);
            this.panelMain.Controls.Add(this.pictureBoxPhoto);
            this.panelMain.Controls.Add(this.labelPost);
            this.panelMain.Controls.Add(this.buttonUpload);
            this.panelMain.Controls.Add(this.textBoxHomeNumber);
            this.panelMain.Controls.Add(this.labelDateBirth);
            this.panelMain.Controls.Add(this.labelHomeNumber);
            this.panelMain.Controls.Add(this.dateTimePickerDateBirth);
            this.panelMain.Controls.Add(this.textBoxMobileNumber);
            this.panelMain.Controls.Add(this.labelAddress);
            this.panelMain.Controls.Add(this.labelMobileNumber);
            this.panelMain.Controls.Add(this.textBoxAddress);
            this.panelMain.Controls.Add(this.textBoxEmail);
            this.panelMain.Controls.Add(this.labelEmail);
            this.panelMain.Size = new System.Drawing.Size(499, 465);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(499, 36);
            // 
            // buttonUpload
            // 
            this.buttonUpload.Location = new System.Drawing.Point(368, 188);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(100, 25);
            this.buttonUpload.TabIndex = 26;
            this.buttonUpload.Text = "Загрузить";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.ButtonUpload_Click);
            // 
            // pictureBoxPhoto
            // 
            this.pictureBoxPhoto.Location = new System.Drawing.Point(340, 20);
            this.pictureBoxPhoto.Name = "pictureBoxPhoto";
            this.pictureBoxPhoto.Size = new System.Drawing.Size(150, 150);
            this.pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPhoto.TabIndex = 23;
            this.pictureBoxPhoto.TabStop = false;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(126, 350);
            this.textBoxDescription.MaxLength = 30;
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(200, 107);
            this.textBoxDescription.TabIndex = 25;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(13, 353);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(45, 13);
            this.labelDescription.TabIndex = 24;
            this.labelDescription.Text = "О себе:";
            // 
            // textBoxPatronymic
            // 
            this.textBoxPatronymic.Location = new System.Drawing.Point(126, 66);
            this.textBoxPatronymic.MaxLength = 30;
            this.textBoxPatronymic.Name = "textBoxPatronymic";
            this.textBoxPatronymic.Size = new System.Drawing.Size(200, 20);
            this.textBoxPatronymic.TabIndex = 5;
            // 
            // labelPatronymic
            // 
            this.labelPatronymic.AutoSize = true;
            this.labelPatronymic.Location = new System.Drawing.Point(12, 69);
            this.labelPatronymic.Name = "labelPatronymic";
            this.labelPatronymic.Size = new System.Drawing.Size(57, 13);
            this.labelPatronymic.TabIndex = 4;
            this.labelPatronymic.Text = "Отчество:";
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Location = new System.Drawing.Point(126, 40);
            this.textBoxFirstName.MaxLength = 20;
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(200, 20);
            this.textBoxFirstName.TabIndex = 3;
            // 
            // labelFirstName
            // 
            this.labelFirstName.AutoSize = true;
            this.labelFirstName.Location = new System.Drawing.Point(12, 43);
            this.labelFirstName.Name = "labelFirstName";
            this.labelFirstName.Size = new System.Drawing.Size(36, 13);
            this.labelFirstName.TabIndex = 2;
            this.labelFirstName.Text = "Имя*:";
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Location = new System.Drawing.Point(126, 14);
            this.textBoxLastName.MaxLength = 30;
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(200, 20);
            this.textBoxLastName.TabIndex = 1;
            // 
            // labelLastName
            // 
            this.labelLastName.AutoSize = true;
            this.labelLastName.Location = new System.Drawing.Point(12, 17);
            this.labelLastName.Name = "labelLastName";
            this.labelLastName.Size = new System.Drawing.Size(63, 13);
            this.labelLastName.TabIndex = 0;
            this.labelLastName.Text = "Фамилия*:";
            // 
            // labelDateBirth
            // 
            this.labelDateBirth.AutoSize = true;
            this.labelDateBirth.Location = new System.Drawing.Point(12, 96);
            this.labelDateBirth.Name = "labelDateBirth";
            this.labelDateBirth.Size = new System.Drawing.Size(93, 13);
            this.labelDateBirth.TabIndex = 6;
            this.labelDateBirth.Text = "Дата рождения*:";
            // 
            // dateTimePickerDateBirth
            // 
            this.dateTimePickerDateBirth.Location = new System.Drawing.Point(126, 92);
            this.dateTimePickerDateBirth.Name = "dateTimePickerDateBirth";
            this.dateTimePickerDateBirth.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerDateBirth.TabIndex = 7;
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(12, 121);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(45, 13);
            this.labelAddress.TabIndex = 8;
            this.labelAddress.Text = "Адрес*:";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Location = new System.Drawing.Point(126, 118);
            this.textBoxAddress.MaxLength = 250;
            this.textBoxAddress.Multiline = true;
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(200, 40);
            this.textBoxAddress.TabIndex = 9;
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(126, 164);
            this.textBoxEmail.MaxLength = 150;
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(200, 20);
            this.textBoxEmail.TabIndex = 11;
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(12, 167);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(44, 13);
            this.labelEmail.TabIndex = 10;
            this.labelEmail.Text = "Почта*:";
            // 
            // textBoxMobileNumber
            // 
            this.textBoxMobileNumber.Location = new System.Drawing.Point(126, 190);
            this.textBoxMobileNumber.MaxLength = 50;
            this.textBoxMobileNumber.Name = "textBoxMobileNumber";
            this.textBoxMobileNumber.Size = new System.Drawing.Size(200, 20);
            this.textBoxMobileNumber.TabIndex = 13;
            // 
            // labelMobileNumber
            // 
            this.labelMobileNumber.AutoSize = true;
            this.labelMobileNumber.Location = new System.Drawing.Point(12, 193);
            this.labelMobileNumber.Name = "labelMobileNumber";
            this.labelMobileNumber.Size = new System.Drawing.Size(108, 13);
            this.labelMobileNumber.TabIndex = 12;
            this.labelMobileNumber.Text = "Мобильный номер*:";
            // 
            // textBoxHomeNumber
            // 
            this.textBoxHomeNumber.Location = new System.Drawing.Point(126, 216);
            this.textBoxHomeNumber.MaxLength = 50;
            this.textBoxHomeNumber.Name = "textBoxHomeNumber";
            this.textBoxHomeNumber.Size = new System.Drawing.Size(200, 20);
            this.textBoxHomeNumber.TabIndex = 15;
            // 
            // labelHomeNumber
            // 
            this.labelHomeNumber.AutoSize = true;
            this.labelHomeNumber.Location = new System.Drawing.Point(12, 219);
            this.labelHomeNumber.Name = "labelHomeNumber";
            this.labelHomeNumber.Size = new System.Drawing.Size(100, 13);
            this.labelHomeNumber.TabIndex = 14;
            this.labelHomeNumber.Text = "Домашний номер:";
            // 
            // labelPost
            // 
            this.labelPost.AutoSize = true;
            this.labelPost.Location = new System.Drawing.Point(12, 245);
            this.labelPost.Name = "labelPost";
            this.labelPost.Size = new System.Drawing.Size(72, 13);
            this.labelPost.TabIndex = 16;
            this.labelPost.Text = "Должность*:";
            // 
            // labelRank
            // 
            this.labelRank.AutoSize = true;
            this.labelRank.Location = new System.Drawing.Point(12, 299);
            this.labelRank.Name = "labelRank";
            this.labelRank.Size = new System.Drawing.Size(47, 13);
            this.labelRank.TabIndex = 20;
            this.labelRank.Text = "Звание:";
            // 
            // labelAbbreviation
            // 
            this.labelAbbreviation.AutoSize = true;
            this.labelAbbreviation.Location = new System.Drawing.Point(337, 245);
            this.labelAbbreviation.Name = "labelAbbreviation";
            this.labelAbbreviation.Size = new System.Drawing.Size(81, 13);
            this.labelAbbreviation.TabIndex = 27;
            this.labelAbbreviation.Text = "Аббревиатура:";
            // 
            // textBoxAbbreviation
            // 
            this.textBoxAbbreviation.Location = new System.Drawing.Point(340, 269);
            this.textBoxAbbreviation.MaxLength = 10;
            this.textBoxAbbreviation.Name = "textBoxAbbreviation";
            this.textBoxAbbreviation.Size = new System.Drawing.Size(100, 20);
            this.textBoxAbbreviation.TabIndex = 28;
            // 
            // comboBoxPost
            // 
            this.comboBoxPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPost.FormattingEnabled = true;
            this.comboBoxPost.Location = new System.Drawing.Point(126, 242);
            this.comboBoxPost.Name = "comboBoxPost";
            this.comboBoxPost.Size = new System.Drawing.Size(200, 21);
            this.comboBoxPost.TabIndex = 17;
            // 
            // comboBoxRank
            // 
            this.comboBoxRank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRank.FormattingEnabled = true;
            this.comboBoxRank.Location = new System.Drawing.Point(126, 296);
            this.comboBoxRank.Name = "comboBoxRank";
            this.comboBoxRank.Size = new System.Drawing.Size(200, 21);
            this.comboBoxRank.TabIndex = 21;
            // 
            // comboBoxRank2
            // 
            this.comboBoxRank2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRank2.FormattingEnabled = true;
            this.comboBoxRank2.Location = new System.Drawing.Point(126, 323);
            this.comboBoxRank2.Name = "comboBoxRank2";
            this.comboBoxRank2.Size = new System.Drawing.Size(200, 21);
            this.comboBoxRank2.TabIndex = 23;
            // 
            // labelRank2
            // 
            this.labelRank2.AutoSize = true;
            this.labelRank2.Location = new System.Drawing.Point(12, 326);
            this.labelRank2.Name = "labelRank2";
            this.labelRank2.Size = new System.Drawing.Size(47, 13);
            this.labelRank2.TabIndex = 22;
            this.labelRank2.Text = "Звание:";
            // 
            // labelLecturerPost
            // 
            this.labelLecturerPost.AutoSize = true;
            this.labelLecturerPost.Location = new System.Drawing.Point(12, 272);
            this.labelLecturerPost.Name = "labelLecturerPost";
            this.labelLecturerPost.Size = new System.Drawing.Size(72, 13);
            this.labelLecturerPost.TabIndex = 18;
            this.labelLecturerPost.Text = "Должность*:";
            // 
            // comboBoxLecturerPost
            // 
            this.comboBoxLecturerPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLecturerPost.FormattingEnabled = true;
            this.comboBoxLecturerPost.Location = new System.Drawing.Point(126, 269);
            this.comboBoxLecturerPost.Name = "comboBoxLecturerPost";
            this.comboBoxLecturerPost.Size = new System.Drawing.Size(200, 21);
            this.comboBoxLecturerPost.TabIndex = 19;
            // 
            // checkBoxOnlyForPrivate
            // 
            this.checkBoxOnlyForPrivate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxOnlyForPrivate.Location = new System.Drawing.Point(340, 309);
            this.checkBoxOnlyForPrivate.Name = "checkBoxOnlyForPrivate";
            this.checkBoxOnlyForPrivate.Size = new System.Drawing.Size(104, 49);
            this.checkBoxOnlyForPrivate.TabIndex = 29;
            this.checkBoxOnlyForPrivate.Text = "Только для внутренного пользования";
            this.checkBoxOnlyForPrivate.UseVisualStyleBackColor = true;
            // 
            // LecturerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 501);
            this.Name = "LecturerForm";
            this.Text = "Преподаватель";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
        
		private System.Windows.Forms.Button buttonUpload;
		private System.Windows.Forms.PictureBox pictureBoxPhoto;
		private System.Windows.Forms.TextBox textBoxDescription;
		private System.Windows.Forms.Label labelDescription;
		private System.Windows.Forms.TextBox textBoxPatronymic;
		private System.Windows.Forms.Label labelPatronymic;
		private System.Windows.Forms.TextBox textBoxFirstName;
		private System.Windows.Forms.Label labelFirstName;
		private System.Windows.Forms.TextBox textBoxLastName;
		private System.Windows.Forms.Label labelLastName;
		private System.Windows.Forms.Label labelDateBirth;
		private System.Windows.Forms.DateTimePicker dateTimePickerDateBirth;
		private System.Windows.Forms.Label labelAddress;
		private System.Windows.Forms.TextBox textBoxAddress;
		private System.Windows.Forms.TextBox textBoxEmail;
		private System.Windows.Forms.Label labelEmail;
		private System.Windows.Forms.TextBox textBoxMobileNumber;
		private System.Windows.Forms.Label labelMobileNumber;
		private System.Windows.Forms.TextBox textBoxHomeNumber;
		private System.Windows.Forms.Label labelHomeNumber;
		private System.Windows.Forms.Label labelPost;
		private System.Windows.Forms.Label labelRank;
		private System.Windows.Forms.Label labelAbbreviation;
		private System.Windows.Forms.TextBox textBoxAbbreviation;
        private System.Windows.Forms.ComboBox comboBoxPost;
        private System.Windows.Forms.ComboBox comboBoxRank;
        private System.Windows.Forms.ComboBox comboBoxRank2;
        private System.Windows.Forms.Label labelRank2;
        private System.Windows.Forms.Label labelLecturerPost;
        private System.Windows.Forms.ComboBox comboBoxLecturerPost;
        private System.Windows.Forms.CheckBox checkBoxOnlyForPrivate;
    }
}