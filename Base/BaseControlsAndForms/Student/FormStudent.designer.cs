namespace BaseControlsAndForms.Student
{
    partial class FormStudent
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
            this.labelNumberOfBook = new System.Windows.Forms.Label();
            this.textBoxNumberOfBook = new System.Windows.Forms.TextBox();
            this.labelLastName = new System.Windows.Forms.Label();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.labelFirstName = new System.Windows.Forms.Label();
            this.textBoxPatronymic = new System.Windows.Forms.TextBox();
            this.labelPatronymic = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.pictureBoxPhoto = new System.Windows.Forms.PictureBox();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.labelStudentGroup = new System.Windows.Forms.Label();
            this.comboBoxStudentGroup = new System.Windows.Forms.ComboBox();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.checkBoxIsSteward = new System.Windows.Forms.CheckBox();
            this.buttonStudentOrdersShow = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.buttonStudentOrdersShow);
            this.panelMain.Controls.Add(this.checkBoxIsSteward);
            this.panelMain.Controls.Add(this.labelStudentGroup);
            this.panelMain.Controls.Add(this.textBoxEmail);
            this.panelMain.Controls.Add(this.labelNumberOfBook);
            this.panelMain.Controls.Add(this.labelEmail);
            this.panelMain.Controls.Add(this.textBoxNumberOfBook);
            this.panelMain.Controls.Add(this.comboBoxStudentGroup);
            this.panelMain.Controls.Add(this.labelLastName);
            this.panelMain.Controls.Add(this.textBoxLastName);
            this.panelMain.Controls.Add(this.buttonUpload);
            this.panelMain.Controls.Add(this.labelFirstName);
            this.panelMain.Controls.Add(this.pictureBoxPhoto);
            this.panelMain.Controls.Add(this.textBoxFirstName);
            this.panelMain.Controls.Add(this.textBoxDescription);
            this.panelMain.Controls.Add(this.labelPatronymic);
            this.panelMain.Controls.Add(this.labelDescription);
            this.panelMain.Controls.Add(this.textBoxPatronymic);
            this.panelMain.Size = new System.Drawing.Size(484, 285);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(484, 36);
            // 
            // labelNumberOfBook
            // 
            this.labelNumberOfBook.AutoSize = true;
            this.labelNumberOfBook.Location = new System.Drawing.Point(12, 40);
            this.labelNumberOfBook.Name = "labelNumberOfBook";
            this.labelNumberOfBook.Size = new System.Drawing.Size(91, 13);
            this.labelNumberOfBook.TabIndex = 2;
            this.labelNumberOfBook.Text = "Номер зачетки*:";
            // 
            // textBoxNumberOfBook
            // 
            this.textBoxNumberOfBook.Location = new System.Drawing.Point(109, 37);
            this.textBoxNumberOfBook.Name = "textBoxNumberOfBook";
            this.textBoxNumberOfBook.Size = new System.Drawing.Size(100, 20);
            this.textBoxNumberOfBook.TabIndex = 3;
            // 
            // labelLastName
            // 
            this.labelLastName.AutoSize = true;
            this.labelLastName.Location = new System.Drawing.Point(12, 66);
            this.labelLastName.Name = "labelLastName";
            this.labelLastName.Size = new System.Drawing.Size(63, 13);
            this.labelLastName.TabIndex = 4;
            this.labelLastName.Text = "Фамилия*:";
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Location = new System.Drawing.Point(109, 63);
            this.textBoxLastName.MaxLength = 30;
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(200, 20);
            this.textBoxLastName.TabIndex = 5;
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Location = new System.Drawing.Point(109, 89);
            this.textBoxFirstName.MaxLength = 20;
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(200, 20);
            this.textBoxFirstName.TabIndex = 7;
            // 
            // labelFirstName
            // 
            this.labelFirstName.AutoSize = true;
            this.labelFirstName.Location = new System.Drawing.Point(12, 92);
            this.labelFirstName.Name = "labelFirstName";
            this.labelFirstName.Size = new System.Drawing.Size(36, 13);
            this.labelFirstName.TabIndex = 6;
            this.labelFirstName.Text = "Имя*:";
            // 
            // textBoxPatronymic
            // 
            this.textBoxPatronymic.Location = new System.Drawing.Point(109, 115);
            this.textBoxPatronymic.MaxLength = 30;
            this.textBoxPatronymic.Name = "textBoxPatronymic";
            this.textBoxPatronymic.Size = new System.Drawing.Size(200, 20);
            this.textBoxPatronymic.TabIndex = 9;
            // 
            // labelPatronymic
            // 
            this.labelPatronymic.AutoSize = true;
            this.labelPatronymic.Location = new System.Drawing.Point(12, 118);
            this.labelPatronymic.Name = "labelPatronymic";
            this.labelPatronymic.Size = new System.Drawing.Size(57, 13);
            this.labelPatronymic.TabIndex = 8;
            this.labelPatronymic.Text = "Отчество:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(109, 167);
            this.textBoxDescription.MaxLength = 30;
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(200, 107);
            this.textBoxDescription.TabIndex = 13;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(12, 170);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(60, 13);
            this.labelDescription.TabIndex = 12;
            this.labelDescription.Text = "Описание:";
            // 
            // pictureBoxPhoto
            // 
            this.pictureBoxPhoto.Location = new System.Drawing.Point(325, 10);
            this.pictureBoxPhoto.Name = "pictureBoxPhoto";
            this.pictureBoxPhoto.Size = new System.Drawing.Size(150, 150);
            this.pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPhoto.TabIndex = 10;
            this.pictureBoxPhoto.TabStop = false;
            // 
            // buttonUpload
            // 
            this.buttonUpload.Location = new System.Drawing.Point(353, 178);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(100, 25);
            this.buttonUpload.TabIndex = 14;
            this.buttonUpload.Text = "Загрузить";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.ButtonUpload_Click);
            // 
            // labelStudentGroup
            // 
            this.labelStudentGroup.AutoSize = true;
            this.labelStudentGroup.Location = new System.Drawing.Point(12, 13);
            this.labelStudentGroup.Name = "labelStudentGroup";
            this.labelStudentGroup.Size = new System.Drawing.Size(49, 13);
            this.labelStudentGroup.TabIndex = 0;
            this.labelStudentGroup.Text = "Группа*:";
            // 
            // comboBoxStudentGroup
            // 
            this.comboBoxStudentGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudentGroup.FormattingEnabled = true;
            this.comboBoxStudentGroup.Location = new System.Drawing.Point(109, 10);
            this.comboBoxStudentGroup.Name = "comboBoxStudentGroup";
            this.comboBoxStudentGroup.Size = new System.Drawing.Size(200, 21);
            this.comboBoxStudentGroup.TabIndex = 1;
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(109, 141);
            this.textBoxEmail.MaxLength = 150;
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(200, 20);
            this.textBoxEmail.TabIndex = 11;
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(12, 144);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(44, 13);
            this.labelEmail.TabIndex = 10;
            this.labelEmail.Text = "Почта*:";
            // 
            // checkBoxIsSteward
            // 
            this.checkBoxIsSteward.AutoSize = true;
            this.checkBoxIsSteward.Location = new System.Drawing.Point(339, 220);
            this.checkBoxIsSteward.Name = "checkBoxIsSteward";
            this.checkBoxIsSteward.Size = new System.Drawing.Size(73, 17);
            this.checkBoxIsSteward.TabIndex = 15;
            this.checkBoxIsSteward.Text = "Староста";
            this.checkBoxIsSteward.UseVisualStyleBackColor = true;
            // 
            // buttonStudentOrdersShow
            // 
            this.buttonStudentOrdersShow.Location = new System.Drawing.Point(353, 250);
            this.buttonStudentOrdersShow.Name = "buttonStudentOrdersShow";
            this.buttonStudentOrdersShow.Size = new System.Drawing.Size(100, 23);
            this.buttonStudentOrdersShow.TabIndex = 16;
            this.buttonStudentOrdersShow.Text = "Приказы";
            this.buttonStudentOrdersShow.UseVisualStyleBackColor = true;
            this.buttonStudentOrdersShow.Click += new System.EventHandler(this.buttonStudentOrdersShow_Click);
            // 
            // FormStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 321);
            this.Name = "FormStudent";
            this.Text = "Студент";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelNumberOfBook;
        private System.Windows.Forms.TextBox textBoxNumberOfBook;
        private System.Windows.Forms.Label labelLastName;
        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.Label labelFirstName;
        private System.Windows.Forms.TextBox textBoxPatronymic;
        private System.Windows.Forms.Label labelPatronymic;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.PictureBox pictureBoxPhoto;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.Label labelStudentGroup;
        private System.Windows.Forms.ComboBox comboBoxStudentGroup;
		private System.Windows.Forms.TextBox textBoxEmail;
		private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.CheckBox checkBoxIsSteward;
        private System.Windows.Forms.Button buttonStudentOrdersShow;
    }
}