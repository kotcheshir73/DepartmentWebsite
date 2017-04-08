namespace DepartmentDesktop.Views.EducationalProcess.KindOfLoad
{
	partial class KindOfLoadForm
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
			this.textBoxTitle = new System.Windows.Forms.TextBox();
			this.labelTitle = new System.Windows.Forms.Label();
			this.labelKindOfLoadType = new System.Windows.Forms.Label();
			this.comboBoxKindOfLoadTypes = new System.Windows.Forms.ComboBox();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonSaveAndClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
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
			// labelKindOfLoadType
			// 
			this.labelKindOfLoadType.AutoSize = true;
			this.labelKindOfLoadType.Location = new System.Drawing.Point(12, 35);
			this.labelKindOfLoadType.Name = "labelKindOfLoadType";
			this.labelKindOfLoadType.Size = new System.Drawing.Size(82, 13);
			this.labelKindOfLoadType.TabIndex = 2;
			this.labelKindOfLoadType.Text = "Тип нагрузки*:";
			// 
			// comboBoxKindOfLoadTypes
			// 
			this.comboBoxKindOfLoadTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxKindOfLoadTypes.FormattingEnabled = true;
			this.comboBoxKindOfLoadTypes.Location = new System.Drawing.Point(100, 32);
			this.comboBoxKindOfLoadTypes.Name = "comboBoxKindOfLoadTypes";
			this.comboBoxKindOfLoadTypes.Size = new System.Drawing.Size(250, 21);
			this.comboBoxKindOfLoadTypes.TabIndex = 3;
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(277, 59);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 10;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(49, 59);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 8;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonSaveAndClose
			// 
			this.buttonSaveAndClose.Location = new System.Drawing.Point(130, 59);
			this.buttonSaveAndClose.Name = "buttonSaveAndClose";
			this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
			this.buttonSaveAndClose.TabIndex = 9;
			this.buttonSaveAndClose.Text = "Сохранить и закрыть";
			this.buttonSaveAndClose.UseVisualStyleBackColor = true;
			this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
			// 
			// KindOfLoadForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(364, 92);
			this.Controls.Add(this.buttonSaveAndClose);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.comboBoxKindOfLoadTypes);
			this.Controls.Add(this.labelKindOfLoadType);
			this.Controls.Add(this.textBoxTitle);
			this.Controls.Add(this.labelTitle);
			this.Name = "KindOfLoadForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Вид нагрузки";
			this.Load += new System.EventHandler(this.KindOfLoadForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxTitle;
		private System.Windows.Forms.Label labelTitle;
		private System.Windows.Forms.Label labelKindOfLoadType;
		private System.Windows.Forms.ComboBox comboBoxKindOfLoadTypes;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonSaveAndClose;
	}
}