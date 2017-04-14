namespace DepartmentDesktop.Views.EducationalProcess.Lecturer
{
	partial class LecturerForm
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
			this.buttonSaveAndClose = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
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
			this.textBoxPost = new System.Windows.Forms.TextBox();
			this.labelPost = new System.Windows.Forms.Label();
			this.textBoxRank = new System.Windows.Forms.TextBox();
			this.labelRank = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonSaveAndClose
			// 
			this.buttonSaveAndClose.Location = new System.Drawing.Point(267, 401);
			this.buttonSaveAndClose.Name = "buttonSaveAndClose";
			this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
			this.buttonSaveAndClose.TabIndex = 24;
			this.buttonSaveAndClose.Text = "Сохранить и закрыть";
			this.buttonSaveAndClose.UseVisualStyleBackColor = true;
			this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(414, 401);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 25;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(186, 401);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 23;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonUpload
			// 
			this.buttonUpload.Location = new System.Drawing.Point(367, 180);
			this.buttonUpload.Name = "buttonUpload";
			this.buttonUpload.Size = new System.Drawing.Size(100, 25);
			this.buttonUpload.TabIndex = 22;
			this.buttonUpload.Text = "Загрузить";
			this.buttonUpload.UseVisualStyleBackColor = true;
			this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
			// 
			// pictureBoxPhoto
			// 
			this.pictureBoxPhoto.Location = new System.Drawing.Point(339, 12);
			this.pictureBoxPhoto.Name = "pictureBoxPhoto";
			this.pictureBoxPhoto.Size = new System.Drawing.Size(150, 150);
			this.pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxPhoto.TabIndex = 23;
			this.pictureBoxPhoto.TabStop = false;
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Location = new System.Drawing.Point(125, 286);
			this.textBoxDescription.MaxLength = 30;
			this.textBoxDescription.Multiline = true;
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new System.Drawing.Size(200, 107);
			this.textBoxDescription.TabIndex = 21;
			// 
			// labelDescription
			// 
			this.labelDescription.AutoSize = true;
			this.labelDescription.Location = new System.Drawing.Point(12, 289);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(45, 13);
			this.labelDescription.TabIndex = 20;
			this.labelDescription.Text = "О себе:";
			// 
			// textBoxPatronymic
			// 
			this.textBoxPatronymic.Location = new System.Drawing.Point(125, 58);
			this.textBoxPatronymic.MaxLength = 30;
			this.textBoxPatronymic.Name = "textBoxPatronymic";
			this.textBoxPatronymic.Size = new System.Drawing.Size(200, 20);
			this.textBoxPatronymic.TabIndex = 5;
			// 
			// labelPatronymic
			// 
			this.labelPatronymic.AutoSize = true;
			this.labelPatronymic.Location = new System.Drawing.Point(11, 61);
			this.labelPatronymic.Name = "labelPatronymic";
			this.labelPatronymic.Size = new System.Drawing.Size(57, 13);
			this.labelPatronymic.TabIndex = 4;
			this.labelPatronymic.Text = "Отчество:";
			// 
			// textBoxFirstName
			// 
			this.textBoxFirstName.Location = new System.Drawing.Point(125, 32);
			this.textBoxFirstName.MaxLength = 20;
			this.textBoxFirstName.Name = "textBoxFirstName";
			this.textBoxFirstName.Size = new System.Drawing.Size(200, 20);
			this.textBoxFirstName.TabIndex = 3;
			// 
			// labelFirstName
			// 
			this.labelFirstName.AutoSize = true;
			this.labelFirstName.Location = new System.Drawing.Point(11, 35);
			this.labelFirstName.Name = "labelFirstName";
			this.labelFirstName.Size = new System.Drawing.Size(36, 13);
			this.labelFirstName.TabIndex = 2;
			this.labelFirstName.Text = "Имя*:";
			// 
			// textBoxLastName
			// 
			this.textBoxLastName.Location = new System.Drawing.Point(125, 6);
			this.textBoxLastName.MaxLength = 30;
			this.textBoxLastName.Name = "textBoxLastName";
			this.textBoxLastName.Size = new System.Drawing.Size(200, 20);
			this.textBoxLastName.TabIndex = 1;
			// 
			// labelLastName
			// 
			this.labelLastName.AutoSize = true;
			this.labelLastName.Location = new System.Drawing.Point(11, 9);
			this.labelLastName.Name = "labelLastName";
			this.labelLastName.Size = new System.Drawing.Size(63, 13);
			this.labelLastName.TabIndex = 0;
			this.labelLastName.Text = "Фамилия*:";
			// 
			// labelDateBirth
			// 
			this.labelDateBirth.AutoSize = true;
			this.labelDateBirth.Location = new System.Drawing.Point(11, 88);
			this.labelDateBirth.Name = "labelDateBirth";
			this.labelDateBirth.Size = new System.Drawing.Size(93, 13);
			this.labelDateBirth.TabIndex = 6;
			this.labelDateBirth.Text = "Дата рождения*:";
			// 
			// dateTimePickerDateBirth
			// 
			this.dateTimePickerDateBirth.Location = new System.Drawing.Point(125, 84);
			this.dateTimePickerDateBirth.Name = "dateTimePickerDateBirth";
			this.dateTimePickerDateBirth.Size = new System.Drawing.Size(200, 20);
			this.dateTimePickerDateBirth.TabIndex = 7;
			// 
			// labelAddress
			// 
			this.labelAddress.AutoSize = true;
			this.labelAddress.Location = new System.Drawing.Point(11, 113);
			this.labelAddress.Name = "labelAddress";
			this.labelAddress.Size = new System.Drawing.Size(45, 13);
			this.labelAddress.TabIndex = 8;
			this.labelAddress.Text = "Адрес*:";
			// 
			// textBoxAddress
			// 
			this.textBoxAddress.Location = new System.Drawing.Point(125, 110);
			this.textBoxAddress.MaxLength = 250;
			this.textBoxAddress.Multiline = true;
			this.textBoxAddress.Name = "textBoxAddress";
			this.textBoxAddress.Size = new System.Drawing.Size(200, 40);
			this.textBoxAddress.TabIndex = 9;
			// 
			// textBoxEmail
			// 
			this.textBoxEmail.Location = new System.Drawing.Point(125, 156);
			this.textBoxEmail.MaxLength = 150;
			this.textBoxEmail.Name = "textBoxEmail";
			this.textBoxEmail.Size = new System.Drawing.Size(200, 20);
			this.textBoxEmail.TabIndex = 11;
			// 
			// labelEmail
			// 
			this.labelEmail.AutoSize = true;
			this.labelEmail.Location = new System.Drawing.Point(11, 159);
			this.labelEmail.Name = "labelEmail";
			this.labelEmail.Size = new System.Drawing.Size(44, 13);
			this.labelEmail.TabIndex = 10;
			this.labelEmail.Text = "Почта*:";
			// 
			// textBoxMobileNumber
			// 
			this.textBoxMobileNumber.Location = new System.Drawing.Point(125, 182);
			this.textBoxMobileNumber.MaxLength = 50;
			this.textBoxMobileNumber.Name = "textBoxMobileNumber";
			this.textBoxMobileNumber.Size = new System.Drawing.Size(200, 20);
			this.textBoxMobileNumber.TabIndex = 13;
			// 
			// labelMobileNumber
			// 
			this.labelMobileNumber.AutoSize = true;
			this.labelMobileNumber.Location = new System.Drawing.Point(11, 185);
			this.labelMobileNumber.Name = "labelMobileNumber";
			this.labelMobileNumber.Size = new System.Drawing.Size(108, 13);
			this.labelMobileNumber.TabIndex = 12;
			this.labelMobileNumber.Text = "Мобильный номер*:";
			// 
			// textBoxHomeNumber
			// 
			this.textBoxHomeNumber.Location = new System.Drawing.Point(125, 208);
			this.textBoxHomeNumber.MaxLength = 50;
			this.textBoxHomeNumber.Name = "textBoxHomeNumber";
			this.textBoxHomeNumber.Size = new System.Drawing.Size(200, 20);
			this.textBoxHomeNumber.TabIndex = 15;
			// 
			// labelHomeNumber
			// 
			this.labelHomeNumber.AutoSize = true;
			this.labelHomeNumber.Location = new System.Drawing.Point(11, 211);
			this.labelHomeNumber.Name = "labelHomeNumber";
			this.labelHomeNumber.Size = new System.Drawing.Size(100, 13);
			this.labelHomeNumber.TabIndex = 14;
			this.labelHomeNumber.Text = "Домашний номер:";
			// 
			// textBoxPost
			// 
			this.textBoxPost.Location = new System.Drawing.Point(125, 234);
			this.textBoxPost.MaxLength = 50;
			this.textBoxPost.Name = "textBoxPost";
			this.textBoxPost.Size = new System.Drawing.Size(200, 20);
			this.textBoxPost.TabIndex = 17;
			// 
			// labelPost
			// 
			this.labelPost.AutoSize = true;
			this.labelPost.Location = new System.Drawing.Point(11, 237);
			this.labelPost.Name = "labelPost";
			this.labelPost.Size = new System.Drawing.Size(72, 13);
			this.labelPost.TabIndex = 16;
			this.labelPost.Text = "Должность*:";
			// 
			// textBoxRank
			// 
			this.textBoxRank.Location = new System.Drawing.Point(125, 260);
			this.textBoxRank.MaxLength = 50;
			this.textBoxRank.Name = "textBoxRank";
			this.textBoxRank.Size = new System.Drawing.Size(200, 20);
			this.textBoxRank.TabIndex = 19;
			// 
			// labelRank
			// 
			this.labelRank.AutoSize = true;
			this.labelRank.Location = new System.Drawing.Point(11, 263);
			this.labelRank.Name = "labelRank";
			this.labelRank.Size = new System.Drawing.Size(47, 13);
			this.labelRank.TabIndex = 18;
			this.labelRank.Text = "Звание:";
			// 
			// LecturerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(499, 432);
			this.Controls.Add(this.textBoxRank);
			this.Controls.Add(this.labelRank);
			this.Controls.Add(this.textBoxPost);
			this.Controls.Add(this.labelPost);
			this.Controls.Add(this.textBoxHomeNumber);
			this.Controls.Add(this.labelHomeNumber);
			this.Controls.Add(this.textBoxMobileNumber);
			this.Controls.Add(this.labelMobileNumber);
			this.Controls.Add(this.textBoxEmail);
			this.Controls.Add(this.labelEmail);
			this.Controls.Add(this.textBoxAddress);
			this.Controls.Add(this.labelAddress);
			this.Controls.Add(this.dateTimePickerDateBirth);
			this.Controls.Add(this.labelDateBirth);
			this.Controls.Add(this.buttonSaveAndClose);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.buttonUpload);
			this.Controls.Add(this.pictureBoxPhoto);
			this.Controls.Add(this.textBoxDescription);
			this.Controls.Add(this.labelDescription);
			this.Controls.Add(this.textBoxPatronymic);
			this.Controls.Add(this.labelPatronymic);
			this.Controls.Add(this.textBoxFirstName);
			this.Controls.Add(this.labelFirstName);
			this.Controls.Add(this.textBoxLastName);
			this.Controls.Add(this.labelLastName);
			this.Name = "LecturerForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Преподаватель";
			this.Load += new System.EventHandler(this.LecturerForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonSaveAndClose;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
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
		private System.Windows.Forms.TextBox textBoxPost;
		private System.Windows.Forms.Label labelPost;
		private System.Windows.Forms.TextBox textBoxRank;
		private System.Windows.Forms.Label labelRank;
	}
}