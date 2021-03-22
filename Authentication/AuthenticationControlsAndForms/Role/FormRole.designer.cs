namespace AuthenticationControlsAndForms.Role
{
	partial class FormRole
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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageConfig = new System.Windows.Forms.TabPage();
			this.textBoxRoleName = new System.Windows.Forms.TextBox();
			this.labelRoleName = new System.Windows.Forms.Label();
			this.tabPageAccesses = new System.Windows.Forms.TabPage();
			this.labelRolePriority = new System.Windows.Forms.Label();
			this.numericUpDownRolePriority = new System.Windows.Forms.NumericUpDown();
			this.panelMain.SuspendLayout();
			this.panelTop.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPageConfig.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRolePriority)).BeginInit();
			this.SuspendLayout();
			// 
			// panelMain
			// 
			this.panelMain.Controls.Add(this.tabControl);
			this.panelMain.Size = new System.Drawing.Size(772, 515);
			// 
			// panelTop
			// 
			this.panelTop.Size = new System.Drawing.Size(772, 36);
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageConfig);
			this.tabControl.Controls.Add(this.tabPageAccesses);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(772, 515);
			this.tabControl.TabIndex = 0;
			// 
			// tabPageConfig
			// 
			this.tabPageConfig.Controls.Add(this.numericUpDownRolePriority);
			this.tabPageConfig.Controls.Add(this.labelRolePriority);
			this.tabPageConfig.Controls.Add(this.textBoxRoleName);
			this.tabPageConfig.Controls.Add(this.labelRoleName);
			this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
			this.tabPageConfig.Name = "tabPageConfig";
			this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageConfig.Size = new System.Drawing.Size(764, 489);
			this.tabPageConfig.TabIndex = 0;
			this.tabPageConfig.Text = "Роль";
			this.tabPageConfig.UseVisualStyleBackColor = true;
			// 
			// textBoxRoleName
			// 
			this.textBoxRoleName.Location = new System.Drawing.Point(82, 10);
			this.textBoxRoleName.MaxLength = 20;
			this.textBoxRoleName.Name = "textBoxRoleName";
			this.textBoxRoleName.Size = new System.Drawing.Size(240, 20);
			this.textBoxRoleName.TabIndex = 1;
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
			// tabPageAccesses
			// 
			this.tabPageAccesses.Location = new System.Drawing.Point(4, 22);
			this.tabPageAccesses.Name = "tabPageAccesses";
			this.tabPageAccesses.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageAccesses.Size = new System.Drawing.Size(311, 148);
			this.tabPageAccesses.TabIndex = 1;
			this.tabPageAccesses.Text = "Доступы";
			this.tabPageAccesses.UseVisualStyleBackColor = true;
			// 
			// labelRolePriority
			// 
			this.labelRolePriority.AutoSize = true;
			this.labelRolePriority.Location = new System.Drawing.Point(12, 49);
			this.labelRolePriority.Name = "labelRolePriority";
			this.labelRolePriority.Size = new System.Drawing.Size(68, 13);
			this.labelRolePriority.TabIndex = 2;
			this.labelRolePriority.Text = "Приоритет*:";
			// 
			// numericUpDownRolePriority
			// 
			this.numericUpDownRolePriority.Location = new System.Drawing.Point(82, 47);
			this.numericUpDownRolePriority.Name = "numericUpDownRolePriority";
			this.numericUpDownRolePriority.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownRolePriority.TabIndex = 3;
			// 
			// FormRole
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(772, 551);
			this.Name = "FormRole";
			this.Text = "Роль";
			this.panelMain.ResumeLayout(false);
			this.panelTop.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.tabPageConfig.ResumeLayout(false);
			this.tabPageConfig.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRolePriority)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageConfig;
		private System.Windows.Forms.TabPage tabPageAccesses;
		private System.Windows.Forms.Label labelRoleName;
		private System.Windows.Forms.TextBox textBoxRoleName;
		private System.Windows.Forms.Label labelRolePriority;
		private System.Windows.Forms.NumericUpDown numericUpDownRolePriority;
	}
}