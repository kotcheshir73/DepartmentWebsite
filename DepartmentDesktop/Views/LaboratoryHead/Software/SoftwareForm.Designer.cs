namespace DepartmentDesktop.Views.LaboratoryHead.Software
{
    partial class SoftwareForm
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
            this.textBoxSoftwareDescription = new System.Windows.Forms.TextBox();
            this.labelSoftwareDescription = new System.Windows.Forms.Label();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxSoftwareK = new System.Windows.Forms.TextBox();
            this.labelSoftwareK = new System.Windows.Forms.Label();
            this.textBoxSoftwareKey = new System.Windows.Forms.TextBox();
            this.labelSoftwareKey = new System.Windows.Forms.Label();
            this.textBoxSoftwareName = new System.Windows.Forms.TextBox();
            this.labelSoftwareName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxSoftwareDescription
            // 
            this.textBoxSoftwareDescription.Location = new System.Drawing.Point(136, 32);
            this.textBoxSoftwareDescription.Multiline = true;
            this.textBoxSoftwareDescription.Name = "textBoxSoftwareDescription";
            this.textBoxSoftwareDescription.Size = new System.Drawing.Size(210, 40);
            this.textBoxSoftwareDescription.TabIndex = 3;
            // 
            // labelSoftwareDescription
            // 
            this.labelSoftwareDescription.AutoSize = true;
            this.labelSoftwareDescription.Location = new System.Drawing.Point(12, 35);
            this.labelSoftwareDescription.Name = "labelSoftwareDescription";
            this.labelSoftwareDescription.Size = new System.Drawing.Size(60, 13);
            this.labelSoftwareDescription.TabIndex = 2;
            this.labelSoftwareDescription.Text = "Описание:";
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(110, 139);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 9;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(257, 139);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 10;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(29, 139);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxSoftwareK
            // 
            this.textBoxSoftwareK.Location = new System.Drawing.Point(136, 104);
            this.textBoxSoftwareK.Name = "textBoxSoftwareK";
            this.textBoxSoftwareK.Size = new System.Drawing.Size(210, 20);
            this.textBoxSoftwareK.TabIndex = 7;
            // 
            // labelSoftwareK
            // 
            this.labelSoftwareK.AutoSize = true;
            this.labelSoftwareK.Location = new System.Drawing.Point(12, 107);
            this.labelSoftwareK.Name = "labelSoftwareK";
            this.labelSoftwareK.Size = new System.Drawing.Size(97, 13);
            this.labelSoftwareK.TabIndex = 6;
            this.labelSoftwareK.Text = "К (лиценз. отдел):";
            // 
            // textBoxSoftwareKey
            // 
            this.textBoxSoftwareKey.Location = new System.Drawing.Point(136, 78);
            this.textBoxSoftwareKey.Name = "textBoxSoftwareKey";
            this.textBoxSoftwareKey.Size = new System.Drawing.Size(210, 20);
            this.textBoxSoftwareKey.TabIndex = 5;
            // 
            // labelSoftwareKey
            // 
            this.labelSoftwareKey.AutoSize = true;
            this.labelSoftwareKey.Location = new System.Drawing.Point(12, 81);
            this.labelSoftwareKey.Name = "labelSoftwareKey";
            this.labelSoftwareKey.Size = new System.Drawing.Size(36, 13);
            this.labelSoftwareKey.TabIndex = 4;
            this.labelSoftwareKey.Text = "Ключ:";
            // 
            // textBoxSoftwareName
            // 
            this.textBoxSoftwareName.Location = new System.Drawing.Point(136, 6);
            this.textBoxSoftwareName.Name = "textBoxSoftwareName";
            this.textBoxSoftwareName.Size = new System.Drawing.Size(210, 20);
            this.textBoxSoftwareName.TabIndex = 1;
            // 
            // labelSoftwareName
            // 
            this.labelSoftwareName.AutoSize = true;
            this.labelSoftwareName.Location = new System.Drawing.Point(12, 9);
            this.labelSoftwareName.Name = "labelSoftwareName";
            this.labelSoftwareName.Size = new System.Drawing.Size(83, 13);
            this.labelSoftwareName.TabIndex = 0;
            this.labelSoftwareName.Text = "Название ПО*:";
            // 
            // SoftwareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 171);
            this.Controls.Add(this.textBoxSoftwareDescription);
            this.Controls.Add(this.labelSoftwareDescription);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxSoftwareK);
            this.Controls.Add(this.labelSoftwareK);
            this.Controls.Add(this.textBoxSoftwareKey);
            this.Controls.Add(this.labelSoftwareKey);
            this.Controls.Add(this.textBoxSoftwareName);
            this.Controls.Add(this.labelSoftwareName);
            this.Name = "SoftwareForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ПО";
            this.Load += new System.EventHandler(this.SoftwareForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSoftwareDescription;
        private System.Windows.Forms.Label labelSoftwareDescription;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxSoftwareK;
        private System.Windows.Forms.Label labelSoftwareK;
        private System.Windows.Forms.TextBox textBoxSoftwareKey;
        private System.Windows.Forms.Label labelSoftwareKey;
        private System.Windows.Forms.TextBox textBoxSoftwareName;
        private System.Windows.Forms.Label labelSoftwareName;
    }
}