namespace BaseControlsAndForms.LecturerStudyPost
{
    partial class FormLecturerStudyPost
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
			this.labelStudyPostTitle = new System.Windows.Forms.Label();
			this.textBoxStudyPostTitle = new System.Windows.Forms.TextBox();
			this.labelHours = new System.Windows.Forms.Label();
			this.textBoxHours = new System.Windows.Forms.TextBox();
			this.panelMain.SuspendLayout();
			this.panelTop.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelMain
			// 
			this.panelMain.Controls.Add(this.textBoxHours);
			this.panelMain.Controls.Add(this.labelStudyPostTitle);
			this.panelMain.Controls.Add(this.labelHours);
			this.panelMain.Controls.Add(this.textBoxStudyPostTitle);
			this.panelMain.Size = new System.Drawing.Size(414, 65);
			// 
			// panelTop
			// 
			this.panelTop.Size = new System.Drawing.Size(414, 36);
			// 
			// labelStudyPostTitle
			// 
			this.labelStudyPostTitle.AutoSize = true;
			this.labelStudyPostTitle.Location = new System.Drawing.Point(12, 12);
			this.labelStudyPostTitle.Name = "labelStudyPostTitle";
			this.labelStudyPostTitle.Size = new System.Drawing.Size(64, 13);
			this.labelStudyPostTitle.TabIndex = 0;
			this.labelStudyPostTitle.Text = "Название*:";
			// 
			// textBoxStudyPostTitle
			// 
			this.textBoxStudyPostTitle.Location = new System.Drawing.Point(82, 9);
			this.textBoxStudyPostTitle.Name = "textBoxStudyPostTitle";
			this.textBoxStudyPostTitle.Size = new System.Drawing.Size(311, 20);
			this.textBoxStudyPostTitle.TabIndex = 1;
			// 
			// labelHours
			// 
			this.labelHours.AutoSize = true;
			this.labelHours.Location = new System.Drawing.Point(12, 38);
			this.labelHours.Name = "labelHours";
			this.labelHours.Size = new System.Drawing.Size(42, 13);
			this.labelHours.TabIndex = 2;
			this.labelHours.Text = "Часы*:";
			// 
			// textBoxHours
			// 
			this.textBoxHours.Location = new System.Drawing.Point(82, 35);
			this.textBoxHours.Name = "textBoxHours";
			this.textBoxHours.Size = new System.Drawing.Size(93, 20);
			this.textBoxHours.TabIndex = 3;
			// 
			// FormLecturerStudyPost
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(414, 101);
			this.Name = "FormLecturerStudyPost";
			this.Text = "Преподавательская должность";
			this.panelMain.ResumeLayout(false);
			this.panelMain.PerformLayout();
			this.panelTop.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelStudyPostTitle;
        private System.Windows.Forms.TextBox textBoxStudyPostTitle;
        private System.Windows.Forms.Label labelHours;
        private System.Windows.Forms.TextBox textBoxHours;
    }
}