namespace BaseControlsAndForms.LecturerPost
{
    partial class FormLecturerPost
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
            this.labelPostTitle = new System.Windows.Forms.Label();
            this.textBoxPostTitle = new System.Windows.Forms.TextBox();
            this.labelHours = new System.Windows.Forms.Label();
            this.textBoxHours = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(145, 66);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(292, 66);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(64, 66);
            // 
            // labelPostTitle
            // 
            this.labelPostTitle.AutoSize = true;
            this.labelPostTitle.Location = new System.Drawing.Point(12, 9);
            this.labelPostTitle.Name = "labelPostTitle";
            this.labelPostTitle.Size = new System.Drawing.Size(64, 13);
            this.labelPostTitle.TabIndex = 0;
            this.labelPostTitle.Text = "Название*:";
            // 
            // textBoxPostTitle
            // 
            this.textBoxPostTitle.Location = new System.Drawing.Point(82, 6);
            this.textBoxPostTitle.Name = "textBoxPostTitle";
            this.textBoxPostTitle.Size = new System.Drawing.Size(311, 20);
            this.textBoxPostTitle.TabIndex = 1;
            // 
            // labelHours
            // 
            this.labelHours.AutoSize = true;
            this.labelHours.Location = new System.Drawing.Point(12, 35);
            this.labelHours.Name = "labelHours";
            this.labelHours.Size = new System.Drawing.Size(42, 13);
            this.labelHours.TabIndex = 2;
            this.labelHours.Text = "Часы*:";
            // 
            // textBoxHours
            // 
            this.textBoxHours.Location = new System.Drawing.Point(82, 32);
            this.textBoxHours.Name = "textBoxHours";
            this.textBoxHours.Size = new System.Drawing.Size(93, 20);
            this.textBoxHours.TabIndex = 3;
            // 
            // FormLecturerPost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 101);
            this.Controls.Add(this.textBoxHours);
            this.Controls.Add(this.labelHours);
            this.Controls.Add(this.textBoxPostTitle);
            this.Controls.Add(this.labelPostTitle);
            this.Name = "FormLecturerPost";
            this.Text = "Преподавательская должность";
            this.Load += new System.EventHandler(this.FormLecturerPost_Load);
            this.Controls.SetChildIndex(this.labelPostTitle, 0);
            this.Controls.SetChildIndex(this.textBoxPostTitle, 0);
            this.Controls.SetChildIndex(this.labelHours, 0);
            this.Controls.SetChildIndex(this.textBoxHours, 0);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.buttonSaveAndClose, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPostTitle;
        private System.Windows.Forms.TextBox textBoxPostTitle;
        private System.Windows.Forms.Label labelHours;
        private System.Windows.Forms.TextBox textBoxHours;
    }
}