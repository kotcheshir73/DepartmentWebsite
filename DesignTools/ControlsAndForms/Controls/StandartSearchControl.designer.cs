namespace ControlsAndForms.Controls
{
    partial class StandartSearchControl
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
            this.dataGridViewList = new System.Windows.Forms.DataGridView();
            this.toolStripButtonSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.panelSearch = new System.Windows.Forms.Panel();
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
            this.dataGridViewList.Location = new System.Drawing.Point(0, 24);
            this.dataGridViewList.Name = "dataGridViewList";
            this.dataGridViewList.ReadOnly = true;
            this.dataGridViewList.RowHeadersVisible = false;
            this.dataGridViewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewList.Size = new System.Drawing.Size(700, 351);
            this.dataGridViewList.TabIndex = 2;
            this.dataGridViewList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridViewList_CellMouseDoubleClick);
            // 
            // toolStripButtonSelect
            // 
            this.toolStripButtonSelect.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelect.Name = "toolStripButtonSelect";
            this.toolStripButtonSelect.Size = new System.Drawing.Size(58, 22);
            this.toolStripButtonSelect.Text = "Выбрать";
            this.toolStripButtonSelect.Click += new System.EventHandler(this.ToolStripButtonSelect_Click);
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSelect});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 375);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(700, 25);
            this.toolStripMenu.TabIndex = 0;
            this.toolStripMenu.Text = "Действия";
            // 
            // panelSearch
            // 
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(700, 24);
            this.panelSearch.TabIndex = 1;
            // 
            // StandartSearchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewList);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "StandartSearchControl";
            this.Size = new System.Drawing.Size(700, 400);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewList;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelect;
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.Panel panelSearch;
    }
}
