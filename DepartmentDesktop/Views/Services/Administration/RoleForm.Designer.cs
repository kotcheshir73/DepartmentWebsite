namespace DepartmentDesktop.Views.Services.Administration
{
	partial class RoleForm
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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageConfig = new System.Windows.Forms.TabPage();
			this.tabPageAccesses = new System.Windows.Forms.TabPage();
			this.labelRoleName = new System.Windows.Forms.Label();
			this.textBoxRoleName = new System.Windows.Forms.TextBox();
			this.buttonSaveAndClose = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.tabControl.SuspendLayout();
			this.tabPageConfig.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageConfig);
			this.tabControl.Controls.Add(this.tabPageAccesses);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(772, 551);
			this.tabControl.TabIndex = 0;
			// 
			// tabPageConfig
			// 
			this.tabPageConfig.Controls.Add(this.buttonSaveAndClose);
			this.tabPageConfig.Controls.Add(this.buttonClose);
			this.tabPageConfig.Controls.Add(this.buttonSave);
			this.tabPageConfig.Controls.Add(this.textBoxRoleName);
			this.tabPageConfig.Controls.Add(this.labelRoleName);
			this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
			this.tabPageConfig.Name = "tabPageConfig";
			this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageConfig.Size = new System.Drawing.Size(764, 525);
			this.tabPageConfig.TabIndex = 0;
			this.tabPageConfig.Text = "Роль";
			this.tabPageConfig.UseVisualStyleBackColor = true;
			// 
			// tabPageAccesses
			// 
			this.tabPageAccesses.Location = new System.Drawing.Point(4, 22);
			this.tabPageAccesses.Name = "tabPageAccesses";
			this.tabPageAccesses.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageAccesses.Size = new System.Drawing.Size(764, 525);
			this.tabPageAccesses.TabIndex = 1;
			this.tabPageAccesses.Text = "Доступы";
			this.tabPageAccesses.UseVisualStyleBackColor = true;
			// 
			// labelRoleName
			// 
			this.labelRoleName.AutoSize = true;
			this.labelRoleName.Location = new System.Drawing.Point(12, 13);
			this.labelRoleName.Name = "labelRoleName";
			this.labelRoleName.Size = new System.Drawing.Size(64, 13);
			this.labelRoleName.TabIndex = 0;
			this.labelRoleName.Text = "Название*:";
			// 
			// textBoxRoleName
			// 
			this.textBoxRoleName.Location = new System.Drawing.Point(82, 10);
			this.textBoxRoleName.MaxLength = 20;
			this.textBoxRoleName.Name = "textBoxRoleName";
			this.textBoxRoleName.Size = new System.Drawing.Size(240, 20);
			this.textBoxRoleName.TabIndex = 1;
			// 
			// buttonSaveAndClose
			// 
			this.buttonSaveAndClose.Location = new System.Drawing.Point(96, 44);
			this.buttonSaveAndClose.Name = "buttonSaveAndClose";
			this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
			this.buttonSaveAndClose.TabIndex = 3;
			this.buttonSaveAndClose.Text = "Сохранить и закрыть";
			this.buttonSaveAndClose.UseVisualStyleBackColor = true;
			this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(243, 44);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 4;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(15, 44);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 2;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// RoleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(772, 551);
			this.Controls.Add(this.tabControl);
			this.Name = "RoleForm";
			this.Text = "Роль";
			this.Load += new System.EventHandler(this.RoleForm_Load);
			this.tabControl.ResumeLayout(false);
			this.tabPageConfig.ResumeLayout(false);
			this.tabPageConfig.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageConfig;
		private System.Windows.Forms.TabPage tabPageAccesses;
		private System.Windows.Forms.Label labelRoleName;
		private System.Windows.Forms.TextBox textBoxRoleName;
		private System.Windows.Forms.Button buttonSaveAndClose;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
	}
}