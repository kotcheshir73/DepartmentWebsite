namespace ExaminationControlsAndForms.TicketTemplateParagraphRun
{
    partial class ControlTicketTemplateViewerParagraphRun
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.panelOrder = new System.Windows.Forms.Panel();
            this.checkBoxBreak = new System.Windows.Forms.CheckBox();
            this.checkBoxTab = new System.Windows.Forms.CheckBox();
            this.numericUpDownOrder = new System.Windows.Forms.NumericUpDown();
            this.labelOrder = new System.Windows.Forms.Label();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.Location = new System.Drawing.Point(128, 0);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(375, 23);
            this.textBox.TabIndex = 1;
            // 
            // panelOrder
            // 
            this.panelOrder.Controls.Add(this.checkBoxBreak);
            this.panelOrder.Controls.Add(this.checkBoxTab);
            this.panelOrder.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelOrder.Location = new System.Drawing.Point(503, 0);
            this.panelOrder.Name = "panelOrder";
            this.panelOrder.Size = new System.Drawing.Size(117, 23);
            this.panelOrder.TabIndex = 3;
            // 
            // checkBoxBreak
            // 
            this.checkBoxBreak.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxBreak.AutoSize = true;
            this.checkBoxBreak.Location = new System.Drawing.Point(60, 3);
            this.checkBoxBreak.Name = "checkBoxBreak";
            this.checkBoxBreak.Size = new System.Drawing.Size(54, 17);
            this.checkBoxBreak.TabIndex = 3;
            this.checkBoxBreak.Text = "Break";
            this.checkBoxBreak.UseVisualStyleBackColor = true;
            this.checkBoxBreak.CheckedChanged += new System.EventHandler(this.CheckBoxBreak_CheckedChanged);
            // 
            // checkBoxTab
            // 
            this.checkBoxTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxTab.AutoSize = true;
            this.checkBoxTab.Location = new System.Drawing.Point(9, 3);
            this.checkBoxTab.Name = "checkBoxTab";
            this.checkBoxTab.Size = new System.Drawing.Size(45, 17);
            this.checkBoxTab.TabIndex = 2;
            this.checkBoxTab.Text = "Tab";
            this.checkBoxTab.UseVisualStyleBackColor = true;
            this.checkBoxTab.CheckedChanged += new System.EventHandler(this.CheckBoxTab_CheckedChanged);
            // 
            // numericUpDownOrder
            // 
            this.numericUpDownOrder.Location = new System.Drawing.Point(55, 1);
            this.numericUpDownOrder.Name = "numericUpDownOrder";
            this.numericUpDownOrder.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownOrder.TabIndex = 1;
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.Location = new System.Drawing.Point(3, 4);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(46, 13);
            this.labelOrder.TabIndex = 0;
            this.labelOrder.Text = "Строка:";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PropertiesToolStripMenuItem,
            this.SaveToolStripMenuItem,
            this.DelToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(164, 70);
            // 
            // PropertiesToolStripMenuItem
            // 
            this.PropertiesToolStripMenuItem.Name = "PropertiesToolStripMenuItem";
            this.PropertiesToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.PropertiesToolStripMenuItem.Text = "Свойства текста";
            this.PropertiesToolStripMenuItem.Click += new System.EventHandler(this.PropertiesToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.SaveToolStripMenuItem.Text = "Сохранить текст";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // DelToolStripMenuItem
            // 
            this.DelToolStripMenuItem.Name = "DelToolStripMenuItem";
            this.DelToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.DelToolStripMenuItem.Text = "Удалить текст";
            this.DelToolStripMenuItem.Click += new System.EventHandler(this.DelToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelOrder);
            this.panel1.Controls.Add(this.numericUpDownOrder);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(128, 23);
            this.panel1.TabIndex = 1;
            // 
            // ControlTicketTemplateViewerParagraphRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.panelOrder);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.Name = "ControlTicketTemplateViewerParagraphRun";
            this.Size = new System.Drawing.Size(620, 23);
            this.panelOrder.ResumeLayout(false);
            this.panelOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Panel panelOrder;
        private System.Windows.Forms.Label labelOrder;
        private System.Windows.Forms.NumericUpDown numericUpDownOrder;
        private System.Windows.Forms.CheckBox checkBoxTab;
        private System.Windows.Forms.CheckBox checkBoxBreak;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem PropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DelToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
    }
}
