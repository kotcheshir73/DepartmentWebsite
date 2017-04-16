namespace DepartmentDesktop.Views.EducationalProcess.Discipline
{
	partial class DisciplineForm
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
			this.buttonSaveAndClose = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.textBoxTitle = new System.Windows.Forms.TextBox();
			this.labelTitle = new System.Windows.Forms.Label();
			this.labelDisciplineBlock = new System.Windows.Forms.Label();
			this.comboBoxDisciplineBlock = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// buttonSaveAndClose
			// 
			this.buttonSaveAndClose.Location = new System.Drawing.Point(130, 59);
			this.buttonSaveAndClose.Name = "buttonSaveAndClose";
			this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
			this.buttonSaveAndClose.TabIndex = 5;
			this.buttonSaveAndClose.Text = "Сохранить и закрыть";
			this.buttonSaveAndClose.UseVisualStyleBackColor = true;
			this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(277, 59);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 6;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(49, 59);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 4;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// textBoxTitle
			// 
			this.textBoxTitle.Location = new System.Drawing.Point(100, 6);
			this.textBoxTitle.MaxLength = 100;
			this.textBoxTitle.Name = "textBoxTitle";
			this.textBoxTitle.Size = new System.Drawing.Size(250, 20);
			this.textBoxTitle.TabIndex = 1;
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
			// labelDisciplineBlock
			// 
			this.labelDisciplineBlock.AutoSize = true;
			this.labelDisciplineBlock.Location = new System.Drawing.Point(12, 35);
			this.labelDisciplineBlock.Name = "labelDisciplineBlock";
			this.labelDisciplineBlock.Size = new System.Drawing.Size(39, 13);
			this.labelDisciplineBlock.TabIndex = 2;
			this.labelDisciplineBlock.Text = "Блок*:";
			// 
			// comboBoxDisciplineBlock
			// 
			this.comboBoxDisciplineBlock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDisciplineBlock.FormattingEnabled = true;
			this.comboBoxDisciplineBlock.Location = new System.Drawing.Point(100, 32);
			this.comboBoxDisciplineBlock.Name = "comboBoxDisciplineBlock";
			this.comboBoxDisciplineBlock.Size = new System.Drawing.Size(250, 21);
			this.comboBoxDisciplineBlock.TabIndex = 3;
			// 
			// DisciplineForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(364, 92);
			this.Controls.Add(this.comboBoxDisciplineBlock);
			this.Controls.Add(this.labelDisciplineBlock);
			this.Controls.Add(this.buttonSaveAndClose);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.textBoxTitle);
			this.Controls.Add(this.labelTitle);
			this.Name = "DisciplineForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Дисциплина";
			this.Load += new System.EventHandler(this.DisciplineForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonSaveAndClose;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.TextBox textBoxTitle;
		private System.Windows.Forms.Label labelTitle;
		private System.Windows.Forms.Label labelDisciplineBlock;
		private System.Windows.Forms.ComboBox comboBoxDisciplineBlock;
	}
}