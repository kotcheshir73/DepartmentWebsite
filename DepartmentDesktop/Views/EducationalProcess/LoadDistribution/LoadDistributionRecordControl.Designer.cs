﻿namespace DepartmentDesktop.Views.EducationalProcess.LoadDistribution
{
	partial class LoadDistributionRecordControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadDistributionRecordControl));
			this.dataGridViewList = new System.Windows.Forms.DataGridView();
			this.toolStripMenu = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonUpd = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonDel = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonRef = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripDropDownButtonMoves = new System.Windows.Forms.ToolStripDropDownButton();
			this.MakeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
			this.toolStripMenu.SuspendLayout();
			this.SuspendLayout();
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
			this.dataGridViewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dataGridViewList.Size = new System.Drawing.Size(800, 475);
			this.dataGridViewList.TabIndex = 1;
			this.dataGridViewList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellDoubleClick);
			// 
			// toolStripMenu
			// 
			this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAdd,
            this.toolStripSeparator1,
            this.toolStripButtonUpd,
            this.toolStripSeparator2,
            this.toolStripButtonDel,
            this.toolStripSeparator3,
            this.toolStripButtonRef,
            this.toolStripSeparator4,
            this.toolStripDropDownButtonMoves});
			this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
			this.toolStripMenu.Name = "toolStripMenu";
			this.toolStripMenu.Size = new System.Drawing.Size(800, 25);
			this.toolStripMenu.TabIndex = 0;
			this.toolStripMenu.Text = "Действия";
			// 
			// toolStripButtonAdd
			// 
			this.toolStripButtonAdd.Image = global::DepartmentDesktop.Properties.Resources.Add;
			this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAdd.Name = "toolStripButtonAdd";
			this.toolStripButtonAdd.Size = new System.Drawing.Size(79, 22);
			this.toolStripButtonAdd.Text = "Добавить";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
			// toolStripButtonDel
			// 
			this.toolStripButtonDel.Image = global::DepartmentDesktop.Properties.Resources.Del;
			this.toolStripButtonDel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonDel.Name = "toolStripButtonDel";
			this.toolStripButtonDel.Size = new System.Drawing.Size(71, 22);
			this.toolStripButtonDel.Text = "Удалить";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButtonRef
			// 
			this.toolStripButtonRef.Image = global::DepartmentDesktop.Properties.Resources.Ref;
			this.toolStripButtonRef.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonRef.Name = "toolStripButtonRef";
			this.toolStripButtonRef.Size = new System.Drawing.Size(81, 22);
			this.toolStripButtonRef.Text = "Обновить";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripDropDownButtonMoves
			// 
			this.toolStripDropDownButtonMoves.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButtonMoves.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MakeToolStripMenuItem});
			this.toolStripDropDownButtonMoves.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonMoves.Image")));
			this.toolStripDropDownButtonMoves.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonMoves.Name = "toolStripDropDownButtonMoves";
			this.toolStripDropDownButtonMoves.Size = new System.Drawing.Size(71, 22);
			this.toolStripDropDownButtonMoves.Text = "Действия";
			// 
			// MakeToolStripMenuItem
			// 
			this.MakeToolStripMenuItem.Name = "MakeToolStripMenuItem";
			this.MakeToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.MakeToolStripMenuItem.Text = "Сформировать";
			this.MakeToolStripMenuItem.Click += new System.EventHandler(this.MakeToolStripMenuItem_Click);
			// 
			// LoadDistributionRecordControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridViewList);
			this.Controls.Add(this.toolStripMenu);
			this.Name = "LoadDistributionRecordControl";
			this.Size = new System.Drawing.Size(800, 500);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
			this.toolStripMenu.ResumeLayout(false);
			this.toolStripMenu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewList;
		private System.Windows.Forms.ToolStrip toolStripMenu;
		private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolStripButtonUpd;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolStripButtonDel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton toolStripButtonRef;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonMoves;
		private System.Windows.Forms.ToolStripMenuItem MakeToolStripMenuItem;
	}
}
