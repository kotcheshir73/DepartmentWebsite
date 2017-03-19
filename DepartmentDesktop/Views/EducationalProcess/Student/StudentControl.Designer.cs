namespace DepartmentDesktop.Views.EducationalProcess.Student
{
	partial class StudentControl
	{
		/// <summary> 
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором компонентов

		/// <summary> 
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.toolStripMenu = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonUpd = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonRef = new System.Windows.Forms.ToolStripButton();
			this.dataGridViewList = new System.Windows.Forms.DataGridView();
			this.panelDown = new System.Windows.Forms.Panel();
			this.labelPage = new System.Windows.Forms.Label();
			this.buttonPrev = new System.Windows.Forms.Button();
			this.textBoxPageNumber = new System.Windows.Forms.TextBox();
			this.buttonNext = new System.Windows.Forms.Button();
			this.toolStripMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
			this.panelDown.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripMenu
			// 
			this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonUpd,
            this.toolStripSeparator2,
            this.toolStripButtonRef});
			this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
			this.toolStripMenu.Name = "toolStripMenu";
			this.toolStripMenu.Size = new System.Drawing.Size(800, 25);
			this.toolStripMenu.TabIndex = 0;
			this.toolStripMenu.Text = "Действия";
			// 
			// toolStripButtonUpd
			// 
			this.toolStripButtonUpd.Image = global::DepartmentDesktop.Properties.Resources.Upd;
			this.toolStripButtonUpd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonUpd.Name = "toolStripButtonUpd";
			this.toolStripButtonUpd.Size = new System.Drawing.Size(81, 22);
			this.toolStripButtonUpd.Text = "Изменить";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButtonRef
			// 
			this.toolStripButtonRef.Image = global::DepartmentDesktop.Properties.Resources.Ref;
			this.toolStripButtonRef.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonRef.Name = "toolStripButtonRef";
			this.toolStripButtonRef.Size = new System.Drawing.Size(81, 22);
			this.toolStripButtonRef.Text = "Обновить";
			// 
			// dataGridViewList
			// 
			this.dataGridViewList.AllowUserToAddRows = false;
			this.dataGridViewList.AllowUserToDeleteRows = false;
			this.dataGridViewList.AllowUserToResizeRows = false;
			this.dataGridViewList.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
			this.dataGridViewList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewList.Location = new System.Drawing.Point(0, 25);
			this.dataGridViewList.Name = "dataGridViewList";
			this.dataGridViewList.ReadOnly = true;
			this.dataGridViewList.RowHeadersVisible = false;
			this.dataGridViewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewList.Size = new System.Drawing.Size(800, 450);
			this.dataGridViewList.TabIndex = 3;
			// 
			// panelDown
			// 
			this.panelDown.Controls.Add(this.buttonNext);
			this.panelDown.Controls.Add(this.textBoxPageNumber);
			this.panelDown.Controls.Add(this.buttonPrev);
			this.panelDown.Controls.Add(this.labelPage);
			this.panelDown.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelDown.Location = new System.Drawing.Point(0, 475);
			this.panelDown.Name = "panelDown";
			this.panelDown.Size = new System.Drawing.Size(800, 25);
			this.panelDown.TabIndex = 4;
			// 
			// labelPage
			// 
			this.labelPage.AutoSize = true;
			this.labelPage.Location = new System.Drawing.Point(39, 6);
			this.labelPage.Name = "labelPage";
			this.labelPage.Size = new System.Drawing.Size(55, 13);
			this.labelPage.TabIndex = 1;
			this.labelPage.Text = "Страница";
			// 
			// buttonPrev
			// 
			this.buttonPrev.Location = new System.Drawing.Point(3, 0);
			this.buttonPrev.Name = "buttonPrev";
			this.buttonPrev.Size = new System.Drawing.Size(30, 25);
			this.buttonPrev.TabIndex = 0;
			this.buttonPrev.Text = "<<";
			this.buttonPrev.UseVisualStyleBackColor = true;
			// 
			// textBoxPageNumber
			// 
			this.textBoxPageNumber.Location = new System.Drawing.Point(100, 3);
			this.textBoxPageNumber.Name = "textBoxPageNumber";
			this.textBoxPageNumber.Size = new System.Drawing.Size(50, 20);
			this.textBoxPageNumber.TabIndex = 2;
			// 
			// buttonNext
			// 
			this.buttonNext.Location = new System.Drawing.Point(156, 0);
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.Size = new System.Drawing.Size(30, 25);
			this.buttonNext.TabIndex = 3;
			this.buttonNext.Text = ">>";
			this.buttonNext.UseVisualStyleBackColor = true;
			// 
			// StudentControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridViewList);
			this.Controls.Add(this.panelDown);
			this.Controls.Add(this.toolStripMenu);
			this.Name = "StudentControl";
			this.Size = new System.Drawing.Size(800, 500);
			this.toolStripMenu.ResumeLayout(false);
			this.toolStripMenu.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
			this.panelDown.ResumeLayout(false);
			this.panelDown.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolStrip toolStripMenu;
		private System.Windows.Forms.ToolStripButton toolStripButtonUpd;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolStripButtonRef;
		private System.Windows.Forms.DataGridView dataGridViewList;
		private System.Windows.Forms.Panel panelDown;
		private System.Windows.Forms.Button buttonPrev;
		private System.Windows.Forms.Label labelPage;
		private System.Windows.Forms.Button buttonNext;
		private System.Windows.Forms.TextBox textBoxPageNumber;
	}
}
