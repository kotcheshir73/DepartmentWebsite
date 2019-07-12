namespace AuthenticationControlsAndForms.Access
{
	partial class FormAccess
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
            this.labelAccessOperation = new System.Windows.Forms.Label();
            this.comboBoxAccessOperation = new System.Windows.Forms.ComboBox();
            this.labelAccessType = new System.Windows.Forms.Label();
            this.comboBoxAccessType = new System.Windows.Forms.ComboBox();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.comboBoxAccessType);
            this.panelMain.Controls.Add(this.labelAccessOperation);
            this.panelMain.Controls.Add(this.labelAccessType);
            this.panelMain.Controls.Add(this.comboBoxAccessOperation);
            this.panelMain.Size = new System.Drawing.Size(330, 95);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(330, 36);
            // 
            // labelAccessOperation
            // 
            this.labelAccessOperation.AutoSize = true;
            this.labelAccessOperation.Location = new System.Drawing.Point(12, 18);
            this.labelAccessOperation.Name = "labelAccessOperation";
            this.labelAccessOperation.Size = new System.Drawing.Size(64, 13);
            this.labelAccessOperation.TabIndex = 0;
            this.labelAccessOperation.Text = "Операция*:";
            // 
            // comboBoxAccessOperation
            // 
            this.comboBoxAccessOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAccessOperation.FormattingEnabled = true;
            this.comboBoxAccessOperation.Location = new System.Drawing.Point(82, 15);
            this.comboBoxAccessOperation.Name = "comboBoxAccessOperation";
            this.comboBoxAccessOperation.Size = new System.Drawing.Size(210, 21);
            this.comboBoxAccessOperation.TabIndex = 1;
            // 
            // labelAccessType
            // 
            this.labelAccessType.AutoSize = true;
            this.labelAccessType.Location = new System.Drawing.Point(12, 57);
            this.labelAccessType.Name = "labelAccessType";
            this.labelAccessType.Size = new System.Drawing.Size(51, 13);
            this.labelAccessType.TabIndex = 2;
            this.labelAccessType.Text = "Доступ*:";
            // 
            // comboBoxAccessType
            // 
            this.comboBoxAccessType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAccessType.FormattingEnabled = true;
            this.comboBoxAccessType.Location = new System.Drawing.Point(82, 54);
            this.comboBoxAccessType.Name = "comboBoxAccessType";
            this.comboBoxAccessType.Size = new System.Drawing.Size(210, 21);
            this.comboBoxAccessType.TabIndex = 3;
            // 
            // FormAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 131);
            this.Name = "FormAccess";
            this.Text = "Доступ";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelAccessOperation;
		private System.Windows.Forms.ComboBox comboBoxAccessOperation;
		private System.Windows.Forms.Label labelAccessType;
		private System.Windows.Forms.ComboBox comboBoxAccessType;
	}
}