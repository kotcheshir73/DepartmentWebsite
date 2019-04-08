namespace BaseControlsAndForms.DisciplineBlock
{
	partial class FormDisciplineBlock
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.checkBoxDisciplineBlockUseForGrouping = new System.Windows.Forms.CheckBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.textBoxDisciplineBlockOrder = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelDisciplineBlockOrder = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.textBoxDisciplineBlockBlueAsteriskName = new System.Windows.Forms.TextBox();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.labelDisciplineBlockBlueAsteriskName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(64, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Название*:";
            // 
            // checkBoxDisciplineBlockUseForGrouping
            // 
            this.checkBoxDisciplineBlockUseForGrouping.AutoSize = true;
            this.checkBoxDisciplineBlockUseForGrouping.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxDisciplineBlockUseForGrouping.Location = new System.Drawing.Point(15, 60);
            this.checkBoxDisciplineBlockUseForGrouping.Name = "checkBoxDisciplineBlockUseForGrouping";
            this.checkBoxDisciplineBlockUseForGrouping.Size = new System.Drawing.Size(139, 17);
            this.checkBoxDisciplineBlockUseForGrouping.TabIndex = 4;
            this.checkBoxDisciplineBlockUseForGrouping.Text = "Группировать по нему";
            this.checkBoxDisciplineBlockUseForGrouping.UseVisualStyleBackColor = true;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(193, 6);
            this.textBoxTitle.MaxLength = 100;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(250, 20);
            this.textBoxTitle.TabIndex = 1;
            // 
            // textBoxDisciplineBlockOrder
            // 
            this.textBoxDisciplineBlockOrder.Location = new System.Drawing.Point(332, 58);
            this.textBoxDisciplineBlockOrder.MaxLength = 100;
            this.textBoxDisciplineBlockOrder.Name = "textBoxDisciplineBlockOrder";
            this.textBoxDisciplineBlockOrder.Size = new System.Drawing.Size(111, 20);
            this.textBoxDisciplineBlockOrder.TabIndex = 6;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(83, 91);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelDisciplineBlockOrder
            // 
            this.labelDisciplineBlockOrder.AutoSize = true;
            this.labelDisciplineBlockOrder.Location = new System.Drawing.Point(268, 61);
            this.labelDisciplineBlockOrder.Name = "labelDisciplineBlockOrder";
            this.labelDisciplineBlockOrder.Size = new System.Drawing.Size(58, 13);
            this.labelDisciplineBlockOrder.TabIndex = 5;
            this.labelDisciplineBlockOrder.Text = "Порядок*:";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(311, 91);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 9;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // textBoxDisciplineBlockBlueAsteriskName
            // 
            this.textBoxDisciplineBlockBlueAsteriskName.Location = new System.Drawing.Point(193, 32);
            this.textBoxDisciplineBlockBlueAsteriskName.MaxLength = 100;
            this.textBoxDisciplineBlockBlueAsteriskName.Name = "textBoxDisciplineBlockBlueAsteriskName";
            this.textBoxDisciplineBlockBlueAsteriskName.Size = new System.Drawing.Size(250, 20);
            this.textBoxDisciplineBlockBlueAsteriskName.TabIndex = 3;
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(164, 91);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 8;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // labelDisciplineBlockBlueAsteriskName
            // 
            this.labelDisciplineBlockBlueAsteriskName.AutoSize = true;
            this.labelDisciplineBlockBlueAsteriskName.Location = new System.Drawing.Point(12, 35);
            this.labelDisciplineBlockBlueAsteriskName.Name = "labelDisciplineBlockBlueAsteriskName";
            this.labelDisciplineBlockBlueAsteriskName.Size = new System.Drawing.Size(171, 13);
            this.labelDisciplineBlockBlueAsteriskName.TabIndex = 2;
            this.labelDisciplineBlockBlueAsteriskName.Text = "Синоноим для синей звездочки:";
            // 
            // DisciplineBlockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 128);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.checkBoxDisciplineBlockUseForGrouping);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.textBoxDisciplineBlockOrder);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelDisciplineBlockOrder);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.textBoxDisciplineBlockBlueAsteriskName);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.labelDisciplineBlockBlueAsteriskName);
            this.Name = "DisciplineBlockForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Блок дисциплин";
            this.Load += new System.EventHandler(this.FormDisciplineBlock_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.CheckBox checkBoxDisciplineBlockUseForGrouping;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.TextBox textBoxDisciplineBlockOrder;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelDisciplineBlockOrder;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TextBox textBoxDisciplineBlockBlueAsteriskName;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Label labelDisciplineBlockBlueAsteriskName;
    }
}