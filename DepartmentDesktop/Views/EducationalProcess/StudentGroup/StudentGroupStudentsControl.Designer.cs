namespace DepartmentDesktop.Views.EducationalProcess.StudentGroup
{
    partial class StudentGroupStudentsControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StudentGroupStudentsControl));
			this.dataGridViewList = new System.Windows.Forms.DataGridView();
			this.toolStripMenu = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonUpd = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonRef = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripDropDownButtonMoves = new System.Windows.Forms.ToolStripDropDownButton();
			this.enrollmentStudentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.transferStudentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deductionStudentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toAcademStudentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
			this.dataGridViewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewList.Size = new System.Drawing.Size(800, 475);
			this.dataGridViewList.TabIndex = 1;
			// 
			// toolStripMenu
			// 
			this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonUpd,
            this.toolStripSeparator2,
            this.toolStripButtonRef,
            this.toolStripSeparator4,
            this.toolStripDropDownButtonMoves});
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
			this.toolStripButtonUpd.Click += new System.EventHandler(this.toolStripButtonUpd_Click);
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
			this.toolStripButtonRef.Click += new System.EventHandler(this.toolStripButtonRef_Click);
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
            this.enrollmentStudentsToolStripMenuItem,
            this.transferStudentsToolStripMenuItem,
            this.deductionStudentsToolStripMenuItem,
            this.toAcademStudentsToolStripMenuItem});
			this.toolStripDropDownButtonMoves.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonMoves.Image")));
			this.toolStripDropDownButtonMoves.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonMoves.Name = "toolStripDropDownButtonMoves";
			this.toolStripDropDownButtonMoves.Size = new System.Drawing.Size(71, 22);
			this.toolStripDropDownButtonMoves.Text = "Действия";
			// 
			// enrollmentStudentsToolStripMenuItem
			// 
			this.enrollmentStudentsToolStripMenuItem.Name = "enrollmentStudentsToolStripMenuItem";
			this.enrollmentStudentsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.enrollmentStudentsToolStripMenuItem.Text = "Зачислить студентов";
			this.enrollmentStudentsToolStripMenuItem.Click += new System.EventHandler(this.enrollmentStudentsToolStripMenuItem_Click);
			// 
			// transferStudentsToolStripMenuItem
			// 
			this.transferStudentsToolStripMenuItem.Name = "transferStudentsToolStripMenuItem";
			this.transferStudentsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.transferStudentsToolStripMenuItem.Text = "Перевести студентов";
			this.transferStudentsToolStripMenuItem.Click += new System.EventHandler(this.transferStudentsToolStripMenuItem_Click);
			// 
			// deductionStudentsToolStripMenuItem
			// 
			this.deductionStudentsToolStripMenuItem.Name = "deductionStudentsToolStripMenuItem";
			this.deductionStudentsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.deductionStudentsToolStripMenuItem.Text = "Отчислить студентов";
			this.deductionStudentsToolStripMenuItem.Click += new System.EventHandler(this.deductionStudentsToolStripMenuItem_Click);
			// 
			// toAcademStudentsToolStripMenuItem
			// 
			this.toAcademStudentsToolStripMenuItem.Name = "toAcademStudentsToolStripMenuItem";
			this.toAcademStudentsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.toAcademStudentsToolStripMenuItem.Text = "Перевести в академ";
			this.toAcademStudentsToolStripMenuItem.Click += new System.EventHandler(this.toAcademStudentsToolStripMenuItem_Click);
			// 
			// StudentGroupStudentsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridViewList);
			this.Controls.Add(this.toolStripMenu);
			this.Name = "StudentGroupStudentsControl";
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonRef;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonMoves;
		private System.Windows.Forms.ToolStripButton toolStripButtonUpd;
		private System.Windows.Forms.ToolStripMenuItem enrollmentStudentsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem transferStudentsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deductionStudentsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toAcademStudentsToolStripMenuItem;
	}
}
