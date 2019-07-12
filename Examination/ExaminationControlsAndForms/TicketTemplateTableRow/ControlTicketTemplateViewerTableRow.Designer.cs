namespace ExaminationControlsAndForms.TicketTemplateTableRow
{
    partial class ControlTicketTemplateViewerTableRow
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
            this.numericUpDownOrder = new System.Windows.Forms.NumericUpDown();
            this.labelOrder = new System.Windows.Forms.Label();
            this.panelOrder = new System.Windows.Forms.Panel();
            this.panelCells = new System.Windows.Forms.Panel();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).BeginInit();
            this.panelOrder.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // numericUpDownOrder
            // 
            this.numericUpDownOrder.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDownOrder.Location = new System.Drawing.Point(406, 3);
            this.numericUpDownOrder.Name = "numericUpDownOrder";
            this.numericUpDownOrder.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownOrder.TabIndex = 1;
            // 
            // labelOrder
            // 
            this.labelOrder.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelOrder.AutoSize = true;
            this.labelOrder.Location = new System.Drawing.Point(308, 5);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(92, 13);
            this.labelOrder.TabIndex = 0;
            this.labelOrder.Text = "Строка таблицы:";
            // 
            // panelOrder
            // 
            this.panelOrder.Controls.Add(this.numericUpDownOrder);
            this.panelOrder.Controls.Add(this.labelOrder);
            this.panelOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOrder.Location = new System.Drawing.Point(0, 0);
            this.panelOrder.Name = "panelOrder";
            this.panelOrder.Size = new System.Drawing.Size(800, 25);
            this.panelOrder.TabIndex = 2;
            // 
            // panelCells
            // 
            this.panelCells.AutoScroll = true;
            this.panelCells.AutoSize = true;
            this.panelCells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCells.Location = new System.Drawing.Point(0, 25);
            this.panelCells.Name = "panelCells";
            this.panelCells.Padding = new System.Windows.Forms.Padding(3);
            this.panelCells.Size = new System.Drawing.Size(800, 135);
            this.panelCells.TabIndex = 3;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PropertiesToolStripMenuItem,
            this.SaveToolStripMenuItem,
            this.DelToolStripMenuItem,
            this.AddToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(173, 92);
            // 
            // PropertiesToolStripMenuItem
            // 
            this.PropertiesToolStripMenuItem.Name = "PropertiesToolStripMenuItem";
            this.PropertiesToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.PropertiesToolStripMenuItem.Text = "Свойства строки";
            this.PropertiesToolStripMenuItem.Click += new System.EventHandler(this.PropertiesToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.SaveToolStripMenuItem.Text = "Сохранить строку";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // DelToolStripMenuItem
            // 
            this.DelToolStripMenuItem.Name = "DelToolStripMenuItem";
            this.DelToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.DelToolStripMenuItem.Text = "Удалить строку";
            this.DelToolStripMenuItem.Click += new System.EventHandler(this.DelToolStripMenuItem_Click);
            // 
            // AddToolStripMenuItem
            // 
            this.AddToolStripMenuItem.Name = "AddToolStripMenuItem";
            this.AddToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.AddToolStripMenuItem.Text = "Добавить ячейку";
            this.AddToolStripMenuItem.Click += new System.EventHandler(this.AddToolStripMenuItem_Click);
            // 
            // ControlTicketTemplateViewerTableRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.panelCells);
            this.Controls.Add(this.panelOrder);
            this.Name = "ControlTicketTemplateViewerTableRow";
            this.Size = new System.Drawing.Size(800, 160);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).EndInit();
            this.panelOrder.ResumeLayout(false);
            this.panelOrder.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown numericUpDownOrder;
        private System.Windows.Forms.Label labelOrder;
        private System.Windows.Forms.Panel panelOrder;
        private System.Windows.Forms.Panel panelCells;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem PropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddToolStripMenuItem;
    }
}
