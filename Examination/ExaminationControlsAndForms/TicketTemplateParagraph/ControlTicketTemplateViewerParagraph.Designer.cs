namespace ExaminationControlsAndForms.TicketTemplateParagraph
{
    partial class ControlTicketTemplateViewerParagraph
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
            this.panelRuns = new System.Windows.Forms.Panel();
            this.panelOrder = new System.Windows.Forms.Panel();
            this.numericUpDownOrder = new System.Windows.Forms.NumericUpDown();
            this.labelOrder = new System.Windows.Forms.Label();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRuns
            // 
            this.panelRuns.AutoScroll = true;
            this.panelRuns.AutoSize = true;
            this.panelRuns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRuns.Location = new System.Drawing.Point(144, 2);
            this.panelRuns.Name = "panelRuns";
            this.panelRuns.Size = new System.Drawing.Size(564, 20);
            this.panelRuns.TabIndex = 2;
            // 
            // panelOrder
            // 
            this.panelOrder.Controls.Add(this.numericUpDownOrder);
            this.panelOrder.Controls.Add(this.labelOrder);
            this.panelOrder.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelOrder.Location = new System.Drawing.Point(2, 2);
            this.panelOrder.Name = "panelOrder";
            this.panelOrder.Size = new System.Drawing.Size(142, 20);
            this.panelOrder.TabIndex = 4;
            // 
            // numericUpDownOrder
            // 
            this.numericUpDownOrder.Location = new System.Drawing.Point(70, 1);
            this.numericUpDownOrder.Name = "numericUpDownOrder";
            this.numericUpDownOrder.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownOrder.TabIndex = 1;
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.Location = new System.Drawing.Point(3, 3);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(61, 13);
            this.labelOrder.TabIndex = 0;
            this.labelOrder.Text = "Параграф:";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PropertiesToolStripMenuItem,
            this.SaveToolStripMenuItem,
            this.DelToolStripMenuItem,
            this.AddToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(189, 92);
            // 
            // PropertiesToolStripMenuItem
            // 
            this.PropertiesToolStripMenuItem.Name = "PropertiesToolStripMenuItem";
            this.PropertiesToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.PropertiesToolStripMenuItem.Text = "Свойства параграфа";
            this.PropertiesToolStripMenuItem.Click += new System.EventHandler(this.PropertiesToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.SaveToolStripMenuItem.Text = "Сохранить параграф";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // DelToolStripMenuItem
            // 
            this.DelToolStripMenuItem.Name = "DelToolStripMenuItem";
            this.DelToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.DelToolStripMenuItem.Text = "Удалить параграф";
            this.DelToolStripMenuItem.Click += new System.EventHandler(this.DelToolStripMenuItem_Click);
            // 
            // AddToolStripMenuItem
            // 
            this.AddToolStripMenuItem.Name = "AddToolStripMenuItem";
            this.AddToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.AddToolStripMenuItem.Text = "Добавить текст";
            this.AddToolStripMenuItem.Click += new System.EventHandler(this.AddToolStripMenuItem_Click);
            // 
            // ControlTicketTemplateViewerParagraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.panelRuns);
            this.Controls.Add(this.panelOrder);
            this.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.MinimumSize = new System.Drawing.Size(0, 24);
            this.Name = "ControlTicketTemplateViewerParagraph";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(710, 24);
            this.panelOrder.ResumeLayout(false);
            this.panelOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelRuns;
        private System.Windows.Forms.Panel panelOrder;
        private System.Windows.Forms.NumericUpDown numericUpDownOrder;
        private System.Windows.Forms.Label labelOrder;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem PropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddToolStripMenuItem;
    }
}
