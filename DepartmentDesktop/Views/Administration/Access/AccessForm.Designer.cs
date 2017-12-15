namespace DepartmentDesktop.Views.Administration.Access
{
	partial class AccessForm
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
			this.labelAccessOperation = new System.Windows.Forms.Label();
			this.comboBoxAccessOperation = new System.Windows.Forms.ComboBox();
			this.labelAccessType = new System.Windows.Forms.Label();
			this.comboBoxAccessType = new System.Windows.Forms.ComboBox();
			this.buttonSaveAndClose = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelAccessOperation
			// 
			this.labelAccessOperation.AutoSize = true;
			this.labelAccessOperation.Location = new System.Drawing.Point(12, 9);
			this.labelAccessOperation.Name = "labelAccessOperation";
			this.labelAccessOperation.Size = new System.Drawing.Size(64, 13);
			this.labelAccessOperation.TabIndex = 0;
			this.labelAccessOperation.Text = "Операция*:";
			// 
			// comboBoxAccessOperation
			// 
			this.comboBoxAccessOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAccessOperation.FormattingEnabled = true;
			this.comboBoxAccessOperation.Location = new System.Drawing.Point(82, 6);
			this.comboBoxAccessOperation.Name = "comboBoxAccessOperation";
			this.comboBoxAccessOperation.Size = new System.Drawing.Size(210, 21);
			this.comboBoxAccessOperation.TabIndex = 1;
			// 
			// labelAccessType
			// 
			this.labelAccessType.AutoSize = true;
			this.labelAccessType.Location = new System.Drawing.Point(12, 48);
			this.labelAccessType.Name = "labelAccessType";
			this.labelAccessType.Size = new System.Drawing.Size(51, 13);
			this.labelAccessType.TabIndex = 2;
			this.labelAccessType.Text = "Доступ*:";
			// 
			// comboBoxAccessType
			// 
			this.comboBoxAccessType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAccessType.FormattingEnabled = true;
			this.comboBoxAccessType.Location = new System.Drawing.Point(82, 45);
			this.comboBoxAccessType.Name = "comboBoxAccessType";
			this.comboBoxAccessType.Size = new System.Drawing.Size(210, 21);
			this.comboBoxAccessType.TabIndex = 3;
			// 
			// buttonSaveAndClose
			// 
			this.buttonSaveAndClose.Location = new System.Drawing.Point(97, 89);
			this.buttonSaveAndClose.Name = "buttonSaveAndClose";
			this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
			this.buttonSaveAndClose.TabIndex = 10;
			this.buttonSaveAndClose.Text = "Сохранить и закрыть";
			this.buttonSaveAndClose.UseVisualStyleBackColor = true;
			this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(244, 89);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 11;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(16, 89);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 9;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// AccessForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(330, 131);
			this.Controls.Add(this.buttonSaveAndClose);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.comboBoxAccessType);
			this.Controls.Add(this.labelAccessType);
			this.Controls.Add(this.comboBoxAccessOperation);
			this.Controls.Add(this.labelAccessOperation);
			this.Name = "AccessForm";
			this.Text = "Доступ";
			this.Load += new System.EventHandler(this.AccessForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelAccessOperation;
		private System.Windows.Forms.ComboBox comboBoxAccessOperation;
		private System.Windows.Forms.Label labelAccessType;
		private System.Windows.Forms.ComboBox comboBoxAccessType;
		private System.Windows.Forms.Button buttonSaveAndClose;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
	}
}