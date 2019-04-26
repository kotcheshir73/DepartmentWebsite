namespace BaseControlsAndForms.DisciplineBlock
{
	partial class FormDisciplineBlock
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.checkBoxDisciplineBlockUseForGrouping = new System.Windows.Forms.CheckBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.textBoxDisciplineBlockOrder = new System.Windows.Forms.TextBox();
            this.labelDisciplineBlockOrder = new System.Windows.Forms.Label();
            this.textBoxDisciplineBlockBlueAsteriskName = new System.Windows.Forms.TextBox();
            this.labelDisciplineBlockBlueAsteriskName = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.labelTitle);
            this.panelMain.Controls.Add(this.textBoxTitle);
            this.panelMain.Controls.Add(this.checkBoxDisciplineBlockUseForGrouping);
            this.panelMain.Controls.Add(this.labelDisciplineBlockBlueAsteriskName);
            this.panelMain.Controls.Add(this.textBoxDisciplineBlockBlueAsteriskName);
            this.panelMain.Controls.Add(this.textBoxDisciplineBlockOrder);
            this.panelMain.Controls.Add(this.labelDisciplineBlockOrder);
            this.panelMain.Size = new System.Drawing.Size(453, 92);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(453, 36);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(10, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(64, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Название*:";
            // 
            // checkBoxDisciplineBlockUseForGrouping
            // 
            this.checkBoxDisciplineBlockUseForGrouping.AutoSize = true;
            this.checkBoxDisciplineBlockUseForGrouping.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxDisciplineBlockUseForGrouping.Location = new System.Drawing.Point(13, 60);
            this.checkBoxDisciplineBlockUseForGrouping.Name = "checkBoxDisciplineBlockUseForGrouping";
            this.checkBoxDisciplineBlockUseForGrouping.Size = new System.Drawing.Size(139, 17);
            this.checkBoxDisciplineBlockUseForGrouping.TabIndex = 4;
            this.checkBoxDisciplineBlockUseForGrouping.Text = "Группировать по нему";
            this.checkBoxDisciplineBlockUseForGrouping.UseVisualStyleBackColor = true;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(191, 6);
            this.textBoxTitle.MaxLength = 100;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(250, 20);
            this.textBoxTitle.TabIndex = 1;
            // 
            // textBoxDisciplineBlockOrder
            // 
            this.textBoxDisciplineBlockOrder.Location = new System.Drawing.Point(330, 58);
            this.textBoxDisciplineBlockOrder.MaxLength = 100;
            this.textBoxDisciplineBlockOrder.Name = "textBoxDisciplineBlockOrder";
            this.textBoxDisciplineBlockOrder.Size = new System.Drawing.Size(111, 20);
            this.textBoxDisciplineBlockOrder.TabIndex = 6;
            // 
            // labelDisciplineBlockOrder
            // 
            this.labelDisciplineBlockOrder.AutoSize = true;
            this.labelDisciplineBlockOrder.Location = new System.Drawing.Point(266, 61);
            this.labelDisciplineBlockOrder.Name = "labelDisciplineBlockOrder";
            this.labelDisciplineBlockOrder.Size = new System.Drawing.Size(58, 13);
            this.labelDisciplineBlockOrder.TabIndex = 5;
            this.labelDisciplineBlockOrder.Text = "Порядок*:";
            // 
            // textBoxDisciplineBlockBlueAsteriskName
            // 
            this.textBoxDisciplineBlockBlueAsteriskName.Location = new System.Drawing.Point(191, 32);
            this.textBoxDisciplineBlockBlueAsteriskName.MaxLength = 100;
            this.textBoxDisciplineBlockBlueAsteriskName.Name = "textBoxDisciplineBlockBlueAsteriskName";
            this.textBoxDisciplineBlockBlueAsteriskName.Size = new System.Drawing.Size(250, 20);
            this.textBoxDisciplineBlockBlueAsteriskName.TabIndex = 3;
            // 
            // labelDisciplineBlockBlueAsteriskName
            // 
            this.labelDisciplineBlockBlueAsteriskName.AutoSize = true;
            this.labelDisciplineBlockBlueAsteriskName.Location = new System.Drawing.Point(10, 35);
            this.labelDisciplineBlockBlueAsteriskName.Name = "labelDisciplineBlockBlueAsteriskName";
            this.labelDisciplineBlockBlueAsteriskName.Size = new System.Drawing.Size(171, 13);
            this.labelDisciplineBlockBlueAsteriskName.TabIndex = 2;
            this.labelDisciplineBlockBlueAsteriskName.Text = "Синоноим для синей звездочки:";
            // 
            // FormDisciplineBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 128);
            this.Name = "FormDisciplineBlock";
            this.Text = "Блок дисциплин";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.CheckBox checkBoxDisciplineBlockUseForGrouping;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.TextBox textBoxDisciplineBlockOrder;
        private System.Windows.Forms.Label labelDisciplineBlockOrder;
        private System.Windows.Forms.TextBox textBoxDisciplineBlockBlueAsteriskName;
        private System.Windows.Forms.Label labelDisciplineBlockBlueAsteriskName;
    }
}