namespace ExaminationControlsAndForms.TicketTemplateTable
{
    partial class ControlTicketTemplateViewerTable
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
            this.components = new System.ComponentModel.Container();
            this.panelRows = new System.Windows.Forms.Panel();
            this.panelOrder = new System.Windows.Forms.Panel();
            this.numericUpDownOrder = new System.Windows.Forms.NumericUpDown();
            this.labelOrder = new System.Windows.Forms.Label();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelGridColumns = new System.Windows.Forms.Panel();
            this.panelOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRows
            // 
            this.panelRows.AutoScroll = true;
            this.panelRows.AutoSize = true;
            this.panelRows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRows.Location = new System.Drawing.Point(0, 85);
            this.panelRows.Name = "panelRows";
            this.panelRows.Size = new System.Drawing.Size(1179, 54);
            this.panelRows.TabIndex = 0;
            // 
            // panelOrder
            // 
            this.panelOrder.Controls.Add(this.numericUpDownOrder);
            this.panelOrder.Controls.Add(this.labelOrder);
            this.panelOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOrder.Location = new System.Drawing.Point(0, 0);
            this.panelOrder.Name = "panelOrder";
            this.panelOrder.Size = new System.Drawing.Size(1179, 25);
            this.panelOrder.TabIndex = 0;
            // 
            // numericUpDownOrder
            // 
            this.numericUpDownOrder.Location = new System.Drawing.Point(68, 3);
            this.numericUpDownOrder.Name = "numericUpDownOrder";
            this.numericUpDownOrder.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownOrder.TabIndex = 1;
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.Location = new System.Drawing.Point(9, 6);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(53, 13);
            this.labelOrder.TabIndex = 0;
            this.labelOrder.Text = "Таблица:";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PropertiesToolStripMenuItem,
            this.SaveToolStripMenuItem,
            this.DelToolStripMenuItem,
            this.AddToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(181, 92);
            // 
            // PropertiesToolStripMenuItem
            // 
            this.PropertiesToolStripMenuItem.Name = "PropertiesToolStripMenuItem";
            this.PropertiesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.PropertiesToolStripMenuItem.Text = "Свойства таблицы";
            this.PropertiesToolStripMenuItem.Click += new System.EventHandler(this.PropertiesToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.SaveToolStripMenuItem.Text = "Сохранить таблицу";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // DelToolStripMenuItem
            // 
            this.DelToolStripMenuItem.Name = "DelToolStripMenuItem";
            this.DelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.DelToolStripMenuItem.Text = "Удалить таблицу";
            this.DelToolStripMenuItem.Click += new System.EventHandler(this.DelToolStripMenuItem_Click);
            // 
            // AddToolStripMenuItem
            // 
            this.AddToolStripMenuItem.Name = "AddToolStripMenuItem";
            this.AddToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.AddToolStripMenuItem.Text = "Добавить строку";
            this.AddToolStripMenuItem.Click += new System.EventHandler(this.AddToolStripMenuItem_Click);
            // 
            // panelGridColumns
            // 
            this.panelGridColumns.AutoScroll = true;
            this.panelGridColumns.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGridColumns.Location = new System.Drawing.Point(0, 25);
            this.panelGridColumns.Name = "panelGridColumns";
            this.panelGridColumns.Size = new System.Drawing.Size(1179, 60);
            this.panelGridColumns.TabIndex = 9;
            // 
            // ControlTicketTemplateViewerTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.panelRows);
            this.Controls.Add(this.panelGridColumns);
            this.Controls.Add(this.panelOrder);
            this.Name = "ControlTicketTemplateViewerTable";
            this.Size = new System.Drawing.Size(1179, 139);
            this.panelOrder.ResumeLayout(false);
            this.panelOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelRows;
        private System.Windows.Forms.Panel panelOrder;
        private System.Windows.Forms.NumericUpDown numericUpDownOrder;
        private System.Windows.Forms.Label labelOrder;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem PropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddToolStripMenuItem;
        private System.Windows.Forms.Panel panelGridColumns;
    }
}
