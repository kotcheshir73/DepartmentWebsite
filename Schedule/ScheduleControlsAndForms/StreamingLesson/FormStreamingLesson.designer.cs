namespace ScheduleControlsAndForms.StreamingLesson
{
    partial class FormStreamingLesson
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
			this.textBoxStreamName = new System.Windows.Forms.TextBox();
			this.labelStreamName = new System.Windows.Forms.Label();
			this.textBoxIncomingGroups = new System.Windows.Forms.TextBox();
			this.labelIncomingGroups = new System.Windows.Forms.Label();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonSaveAndClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxStreamName
			// 
			this.textBoxStreamName.Location = new System.Drawing.Point(122, 32);
			this.textBoxStreamName.MaxLength = 100;
			this.textBoxStreamName.Name = "textBoxStreamName";
			this.textBoxStreamName.Size = new System.Drawing.Size(290, 20);
			this.textBoxStreamName.TabIndex = 3;
			// 
			// labelStreamName
			// 
			this.labelStreamName.AutoSize = true;
			this.labelStreamName.Location = new System.Drawing.Point(12, 35);
			this.labelStreamName.Name = "labelStreamName";
			this.labelStreamName.Size = new System.Drawing.Size(64, 13);
			this.labelStreamName.TabIndex = 2;
			this.labelStreamName.Text = "Название*:";
			// 
			// textBoxIncomingGroups
			// 
			this.textBoxIncomingGroups.Location = new System.Drawing.Point(122, 6);
			this.textBoxIncomingGroups.MaxLength = 200;
			this.textBoxIncomingGroups.Name = "textBoxIncomingGroups";
			this.textBoxIncomingGroups.Size = new System.Drawing.Size(290, 20);
			this.textBoxIncomingGroups.TabIndex = 1;
			// 
			// labelIncomingGroups
			// 
			this.labelIncomingGroups.AutoSize = true;
			this.labelIncomingGroups.Location = new System.Drawing.Point(12, 9);
			this.labelIncomingGroups.Name = "labelIncomingGroups";
			this.labelIncomingGroups.Size = new System.Drawing.Size(104, 13);
			this.labelIncomingGroups.TabIndex = 0;
			this.labelIncomingGroups.Text = "Входящие группы*:";
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(337, 58);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 5;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(109, 58);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 4;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
			// 
			// buttonSaveAndClose
			// 
			this.buttonSaveAndClose.Location = new System.Drawing.Point(190, 58);
			this.buttonSaveAndClose.Name = "buttonSaveAndClose";
			this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
			this.buttonSaveAndClose.TabIndex = 12;
			this.buttonSaveAndClose.Text = "Сохранить и закрыть";
			this.buttonSaveAndClose.UseVisualStyleBackColor = true;
			this.buttonSaveAndClose.Click += new System.EventHandler(this.ButtonSaveAndClose_Click);
			// 
			// StreamingLessonForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(424, 92);
			this.Controls.Add(this.buttonSaveAndClose);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.textBoxStreamName);
			this.Controls.Add(this.labelStreamName);
			this.Controls.Add(this.textBoxIncomingGroups);
			this.Controls.Add(this.labelIncomingGroups);
			this.Name = "StreamingLessonForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Поток";
			this.Load += new System.EventHandler(this.FormStreamingLesson_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxStreamName;
        private System.Windows.Forms.Label labelStreamName;
        private System.Windows.Forms.TextBox textBoxIncomingGroups;
        private System.Windows.Forms.Label labelIncomingGroups;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonSaveAndClose;
	}
}