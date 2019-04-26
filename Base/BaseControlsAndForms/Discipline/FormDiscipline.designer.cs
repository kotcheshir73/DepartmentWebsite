namespace BaseControlsAndForms.Discipline
{
	partial class FormDiscipline
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
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelDisciplineBlock = new System.Windows.Forms.Label();
            this.comboBoxDisciplineBlock = new System.Windows.Forms.ComboBox();
            this.textBoxDisciplineShortName = new System.Windows.Forms.TextBox();
            this.labelDisciplineShortName = new System.Windows.Forms.Label();
            this.textBoxDisciplineBlueAsteriskName = new System.Windows.Forms.TextBox();
            this.labelDisciplineBlueAsteriskName = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.textBoxDisciplineBlueAsteriskName);
            this.panelMain.Controls.Add(this.labelTitle);
            this.panelMain.Controls.Add(this.labelDisciplineBlueAsteriskName);
            this.panelMain.Controls.Add(this.comboBoxDisciplineBlock);
            this.panelMain.Controls.Add(this.labelDisciplineShortName);
            this.panelMain.Controls.Add(this.textBoxDisciplineShortName);
            this.panelMain.Controls.Add(this.textBoxTitle);
            this.panelMain.Controls.Add(this.labelDisciplineBlock);
            this.panelMain.Size = new System.Drawing.Size(459, 125);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(459, 36);
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(193, 15);
            this.textBoxTitle.MaxLength = 100;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(250, 20);
            this.textBoxTitle.TabIndex = 1;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(12, 18);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(64, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Название*:";
            // 
            // labelDisciplineBlock
            // 
            this.labelDisciplineBlock.AutoSize = true;
            this.labelDisciplineBlock.Location = new System.Drawing.Point(12, 70);
            this.labelDisciplineBlock.Name = "labelDisciplineBlock";
            this.labelDisciplineBlock.Size = new System.Drawing.Size(39, 13);
            this.labelDisciplineBlock.TabIndex = 4;
            this.labelDisciplineBlock.Text = "Блок*:";
            // 
            // comboBoxDisciplineBlock
            // 
            this.comboBoxDisciplineBlock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisciplineBlock.FormattingEnabled = true;
            this.comboBoxDisciplineBlock.Location = new System.Drawing.Point(193, 67);
            this.comboBoxDisciplineBlock.Name = "comboBoxDisciplineBlock";
            this.comboBoxDisciplineBlock.Size = new System.Drawing.Size(250, 21);
            this.comboBoxDisciplineBlock.TabIndex = 5;
            // 
            // textBoxDisciplineShortName
            // 
            this.textBoxDisciplineShortName.Location = new System.Drawing.Point(193, 41);
            this.textBoxDisciplineShortName.MaxLength = 100;
            this.textBoxDisciplineShortName.Name = "textBoxDisciplineShortName";
            this.textBoxDisciplineShortName.Size = new System.Drawing.Size(250, 20);
            this.textBoxDisciplineShortName.TabIndex = 2;
            // 
            // labelDisciplineShortName
            // 
            this.labelDisciplineShortName.AutoSize = true;
            this.labelDisciplineShortName.Location = new System.Drawing.Point(12, 44);
            this.labelDisciplineShortName.Name = "labelDisciplineShortName";
            this.labelDisciplineShortName.Size = new System.Drawing.Size(56, 13);
            this.labelDisciplineShortName.TabIndex = 2;
            this.labelDisciplineShortName.Text = "Краткое*:";
            // 
            // textBoxDisciplineBlueAsteriskName
            // 
            this.textBoxDisciplineBlueAsteriskName.Location = new System.Drawing.Point(193, 94);
            this.textBoxDisciplineBlueAsteriskName.MaxLength = 100;
            this.textBoxDisciplineBlueAsteriskName.Name = "textBoxDisciplineBlueAsteriskName";
            this.textBoxDisciplineBlueAsteriskName.Size = new System.Drawing.Size(250, 20);
            this.textBoxDisciplineBlueAsteriskName.TabIndex = 7;
            // 
            // labelDisciplineBlueAsteriskName
            // 
            this.labelDisciplineBlueAsteriskName.AutoSize = true;
            this.labelDisciplineBlueAsteriskName.Location = new System.Drawing.Point(12, 97);
            this.labelDisciplineBlueAsteriskName.Name = "labelDisciplineBlueAsteriskName";
            this.labelDisciplineBlueAsteriskName.Size = new System.Drawing.Size(175, 13);
            this.labelDisciplineBlueAsteriskName.TabIndex = 6;
            this.labelDisciplineBlueAsteriskName.Text = "Синоноим для синей звездочки*:";
            // 
            // FormDiscipline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 161);
            this.Name = "FormDiscipline";
            this.Text = "Дисциплина";
            this.Load += new System.EventHandler(this.FormDiscipline_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
        
		private System.Windows.Forms.TextBox textBoxTitle;
		private System.Windows.Forms.Label labelTitle;
		private System.Windows.Forms.Label labelDisciplineBlock;
		private System.Windows.Forms.ComboBox comboBoxDisciplineBlock;
        private System.Windows.Forms.TextBox textBoxDisciplineShortName;
        private System.Windows.Forms.Label labelDisciplineShortName;
        private System.Windows.Forms.TextBox textBoxDisciplineBlueAsteriskName;
        private System.Windows.Forms.Label labelDisciplineBlueAsteriskName;
    }
}