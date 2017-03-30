namespace DepartmentDesktop
{
	partial class FormError
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
			this.dataGridViewErrors = new System.Windows.Forms.DataGridView();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewErrors)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewErrors
			// 
			this.dataGridViewErrors.AllowUserToAddRows = false;
			this.dataGridViewErrors.AllowUserToDeleteRows = false;
			this.dataGridViewErrors.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
			this.dataGridViewErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewErrors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
			this.dataGridViewErrors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewErrors.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewErrors.Name = "dataGridViewErrors";
			this.dataGridViewErrors.ReadOnly = true;
			this.dataGridViewErrors.RowHeadersVisible = false;
			this.dataGridViewErrors.Size = new System.Drawing.Size(401, 347);
			this.dataGridViewErrors.TabIndex = 0;
			// 
			// Column1
			// 
			this.Column1.HeaderText = "Ключ";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			// 
			// Column2
			// 
			this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Column2.HeaderText = "Значение";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			// 
			// FormError
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(401, 347);
			this.Controls.Add(this.dataGridViewErrors);
			this.Name = "FormError";
			this.Text = "Ошибки";
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewErrors)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewErrors;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
	}
}