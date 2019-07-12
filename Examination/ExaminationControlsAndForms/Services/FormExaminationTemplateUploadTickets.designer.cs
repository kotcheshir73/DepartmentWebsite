namespace ExaminationControlsAndForms.Services
{
    partial class FormExaminationTemplateUploadTickets
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
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.buttonFileName = new System.Windows.Forms.Button();
            this.buttonUploadTickets = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Enabled = false;
            this.textBoxFileName.Location = new System.Drawing.Point(115, 14);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(300, 20);
            this.textBoxFileName.TabIndex = 3;
            // 
            // buttonFileName
            // 
            this.buttonFileName.Location = new System.Drawing.Point(12, 12);
            this.buttonFileName.Name = "buttonFileName";
            this.buttonFileName.Size = new System.Drawing.Size(97, 23);
            this.buttonFileName.TabIndex = 2;
            this.buttonFileName.Text = "Путь до файла";
            this.buttonFileName.UseVisualStyleBackColor = true;
            this.buttonFileName.Click += new System.EventHandler(this.ButtonFileName_Click);
            // 
            // buttonUploadTickets
            // 
            this.buttonUploadTickets.Location = new System.Drawing.Point(306, 51);
            this.buttonUploadTickets.Name = "buttonUploadTickets";
            this.buttonUploadTickets.Size = new System.Drawing.Size(75, 23);
            this.buttonUploadTickets.TabIndex = 4;
            this.buttonUploadTickets.Text = "Выгрузить";
            this.buttonUploadTickets.UseVisualStyleBackColor = true;
            this.buttonUploadTickets.Click += new System.EventHandler(this.ButtonUploadTickets_Click);
            // 
            // FormExaminationTemplateUploadTickets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 86);
            this.Controls.Add(this.buttonUploadTickets);
            this.Controls.Add(this.buttonFileName);
            this.Controls.Add(this.textBoxFileName);
            this.Name = "FormExaminationTemplateUploadTickets";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выгрузка билетов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Button buttonFileName;
        private System.Windows.Forms.Button buttonUploadTickets;
    }
}