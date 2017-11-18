namespace DepartmentDesktop.Controllers
{
	partial class StandartControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StandartControl));
			this.dataGridViewList = new System.Windows.Forms.DataGridView();
			this.toolStripMenu = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripPages = new System.Windows.Forms.ToolStrip();
			this.toolStripTextBoxPage = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripLabelPage = new System.Windows.Forms.ToolStripLabel();
			this.toolStripLabelCountPages = new System.Windows.Forms.ToolStripLabel();
			this.toolStripButtonBefore = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonUpd = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonDel = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonRef = new System.Windows.Forms.ToolStripButton();
			this.toolStripDropDownButtonMoves = new System.Windows.Forms.ToolStripSplitButton();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
			this.toolStripMenu.SuspendLayout();
			this.toolStripPages.SuspendLayout();
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
			this.dataGridViewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewList.Size = new System.Drawing.Size(700, 350);
			this.dataGridViewList.TabIndex = 1;
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
			this.toolStripMenu.Size = new System.Drawing.Size(700, 25);
			this.toolStripMenu.TabIndex = 0;
			this.toolStripMenu.Text = "Действия";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripPages
			// 
			this.toolStripPages.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.toolStripPages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonBefore,
            this.toolStripLabelPage,
            this.toolStripTextBoxPage,
            this.toolStripLabelCountPages,
            this.toolStripButtonNext});
			this.toolStripPages.Location = new System.Drawing.Point(0, 375);
			this.toolStripPages.Name = "toolStripPages";
			this.toolStripPages.Size = new System.Drawing.Size(700, 25);
			this.toolStripPages.TabIndex = 2;
			this.toolStripPages.Text = "Пагинация";
			// 
			// toolStripTextBoxPage
			// 
			this.toolStripTextBoxPage.MaxLength = 3;
			this.toolStripTextBoxPage.Name = "toolStripTextBoxPage";
			this.toolStripTextBoxPage.Size = new System.Drawing.Size(30, 25);
			this.toolStripTextBoxPage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ToolStripTextBoxPage_KeyDown);
			// 
			// toolStripLabelPage
			// 
			this.toolStripLabelPage.Name = "toolStripLabelPage";
			this.toolStripLabelPage.Size = new System.Drawing.Size(60, 22);
			this.toolStripLabelPage.Text = "Страница";
			// 
			// toolStripLabelCountPages
			// 
			this.toolStripLabelCountPages.Name = "toolStripLabelCountPages";
			this.toolStripLabelCountPages.Size = new System.Drawing.Size(19, 22);
			this.toolStripLabelCountPages.Text = "из";
			// 
			// toolStripButtonBefore
			// 
			this.toolStripButtonBefore.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonBefore.Image = global::DepartmentDesktop.Properties.Resources.Left;
			this.toolStripButtonBefore.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonBefore.Name = "toolStripButtonBefore";
			this.toolStripButtonBefore.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonBefore.Text = "Предыдущая";
			this.toolStripButtonBefore.Click += new System.EventHandler(this.ToolStripButtonBefore_Click);
			// 
			// toolStripButtonNext
			// 
			this.toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonNext.Image = global::DepartmentDesktop.Properties.Resources.Right;
			this.toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonNext.Name = "toolStripButtonNext";
			this.toolStripButtonNext.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonNext.Text = "Следующая";
			this.toolStripButtonNext.Click += new System.EventHandler(this.ToolStripButtonNext_Click);
			// 
			// toolStripButtonAdd
			// 
			this.toolStripButtonAdd.Image = global::DepartmentDesktop.Properties.Resources.Add;
			this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAdd.Name = "toolStripButtonAdd";
			this.toolStripButtonAdd.Size = new System.Drawing.Size(79, 22);
			this.toolStripButtonAdd.Text = "Добавить";
			// 
			// toolStripButtonUpd
			// 
			this.toolStripButtonUpd.Image = global::DepartmentDesktop.Properties.Resources.Upd;
			this.toolStripButtonUpd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonUpd.Name = "toolStripButtonUpd";
			this.toolStripButtonUpd.Size = new System.Drawing.Size(81, 22);
			this.toolStripButtonUpd.Text = "Изменить";
			// 
			// toolStripButtonDel
			// 
			this.toolStripButtonDel.Image = global::DepartmentDesktop.Properties.Resources.Del;
			this.toolStripButtonDel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonDel.Name = "toolStripButtonDel";
			this.toolStripButtonDel.Size = new System.Drawing.Size(71, 22);
			this.toolStripButtonDel.Text = "Удалить";
			// 
			// toolStripButtonRef
			// 
			this.toolStripButtonRef.Image = global::DepartmentDesktop.Properties.Resources.Ref;
			this.toolStripButtonRef.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonRef.Name = "toolStripButtonRef";
			this.toolStripButtonRef.Size = new System.Drawing.Size(81, 22);
			this.toolStripButtonRef.Text = "Обновить";
			// 
			// toolStripDropDownButtonMoves
			// 
			this.toolStripDropDownButtonMoves.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButtonMoves.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonMoves.Image")));
			this.toolStripDropDownButtonMoves.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonMoves.Name = "toolStripDropDownButtonMoves";
			this.toolStripDropDownButtonMoves.Size = new System.Drawing.Size(74, 22);
			this.toolStripDropDownButtonMoves.Text = "Действия";
			this.toolStripDropDownButtonMoves.ToolTipText = "Действия";
			// 
			// StandartControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridViewList);
			this.Controls.Add(this.toolStripPages);
			this.Controls.Add(this.toolStripMenu);
			this.Name = "StandartControl";
			this.Size = new System.Drawing.Size(700, 400);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
			this.toolStripMenu.ResumeLayout(false);
			this.toolStripMenu.PerformLayout();
			this.toolStripPages.ResumeLayout(false);
			this.toolStripPages.PerformLayout();
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
		private System.Windows.Forms.ToolStripSplitButton toolStripDropDownButtonMoves;
		private System.Windows.Forms.ToolStrip toolStripPages;
		private System.Windows.Forms.ToolStripButton toolStripButtonBefore;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBoxPage;
		private System.Windows.Forms.ToolStripButton toolStripButtonNext;
		private System.Windows.Forms.ToolStripLabel toolStripLabelPage;
		private System.Windows.Forms.ToolStripLabel toolStripLabelCountPages;
	}
}
