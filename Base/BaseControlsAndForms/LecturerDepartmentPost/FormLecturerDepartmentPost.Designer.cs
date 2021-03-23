
namespace BaseControlsAndForms.LecturerDepartmentPost
{
	partial class FormLecturerDepartmentPost
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
			this.labelDepartmentPostTitle = new System.Windows.Forms.Label();
			this.labelOrder = new System.Windows.Forms.Label();
			this.textBoxDepartmentPostTitle = new System.Windows.Forms.TextBox();
			this.numericUpDownOrder = new System.Windows.Forms.NumericUpDown();
			this.panelMain.SuspendLayout();
			this.panelTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).BeginInit();
			this.SuspendLayout();
			// 
			// panelMain
			// 
			this.panelMain.Controls.Add(this.numericUpDownOrder);
			this.panelMain.Controls.Add(this.labelDepartmentPostTitle);
			this.panelMain.Controls.Add(this.labelOrder);
			this.panelMain.Controls.Add(this.textBoxDepartmentPostTitle);
			this.panelMain.Size = new System.Drawing.Size(414, 65);
			// 
			// panelTop
			// 
			this.panelTop.Size = new System.Drawing.Size(414, 36);
			// 
			// labelDepartmentPostTitle
			// 
			this.labelDepartmentPostTitle.AutoSize = true;
			this.labelDepartmentPostTitle.Location = new System.Drawing.Point(12, 14);
			this.labelDepartmentPostTitle.Name = "labelDepartmentPostTitle";
			this.labelDepartmentPostTitle.Size = new System.Drawing.Size(64, 13);
			this.labelDepartmentPostTitle.TabIndex = 0;
			this.labelDepartmentPostTitle.Text = "Название*:";
			// 
			// labelOrder
			// 
			this.labelOrder.AutoSize = true;
			this.labelOrder.Location = new System.Drawing.Point(12, 40);
			this.labelOrder.Name = "labelOrder";
			this.labelOrder.Size = new System.Drawing.Size(58, 13);
			this.labelOrder.TabIndex = 2;
			this.labelOrder.Text = "Порядок*:";
			// 
			// textBoxDepartmentPostTitle
			// 
			this.textBoxDepartmentPostTitle.Location = new System.Drawing.Point(82, 11);
			this.textBoxDepartmentPostTitle.Name = "textBoxDepartmentPostTitle";
			this.textBoxDepartmentPostTitle.Size = new System.Drawing.Size(311, 20);
			this.textBoxDepartmentPostTitle.TabIndex = 1;
			// 
			// numericUpDownOrder
			// 
			this.numericUpDownOrder.Location = new System.Drawing.Point(82, 38);
			this.numericUpDownOrder.Name = "numericUpDownOrder";
			this.numericUpDownOrder.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownOrder.TabIndex = 3;
			// 
			// FormLecturerDepartmentPost
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(414, 101);
			this.Name = "FormLecturerDepartmentPost";
			this.Text = "Преподавательская должность";
			this.panelMain.ResumeLayout(false);
			this.panelMain.PerformLayout();
			this.panelTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label labelDepartmentPostTitle;
		private System.Windows.Forms.Label labelOrder;
		private System.Windows.Forms.TextBox textBoxDepartmentPostTitle;
		private System.Windows.Forms.NumericUpDown numericUpDownOrder;
	}
}