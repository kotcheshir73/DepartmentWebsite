namespace ExaminationControlsAndForms.Services
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
            this.panelAction = new System.Windows.Forms.Panel();
            this.buttonAddCell = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonShowProperties = new System.Windows.Forms.Button();
            this.numericUpDownOrder = new System.Windows.Forms.NumericUpDown();
            this.labelOrder = new System.Windows.Forms.Label();
            this.panelOrder = new System.Windows.Forms.Panel();
            this.textBoxTableRowHeight = new System.Windows.Forms.TextBox();
            this.labelTableRowHeight = new System.Windows.Forms.Label();
            this.textBoxCantSplit = new System.Windows.Forms.TextBox();
            this.labelCantSplit = new System.Windows.Forms.Label();
            this.panelTableRowProperties = new System.Windows.Forms.Panel();
            this.groupBoxIndentation = new System.Windows.Forms.GroupBox();
            this.panelCells = new System.Windows.Forms.Panel();
            this.panelAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).BeginInit();
            this.panelOrder.SuspendLayout();
            this.panelTableRowProperties.SuspendLayout();
            this.groupBoxIndentation.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAction
            // 
            this.panelAction.Controls.Add(this.buttonAddCell);
            this.panelAction.Controls.Add(this.buttonDel);
            this.panelAction.Controls.Add(this.buttonSave);
            this.panelAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAction.Location = new System.Drawing.Point(128, 0);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(672, 55);
            this.panelAction.TabIndex = 1;
            // 
            // buttonAddCell
            // 
            this.buttonAddCell.Location = new System.Drawing.Point(298, 6);
            this.buttonAddCell.Name = "buttonAddCell";
            this.buttonAddCell.Size = new System.Drawing.Size(100, 40);
            this.buttonAddCell.TabIndex = 4;
            this.buttonAddCell.Text = "Добавить ячейку";
            this.buttonAddCell.UseVisualStyleBackColor = true;
            this.buttonAddCell.Visible = false;
            this.buttonAddCell.Click += new System.EventHandler(this.ButtonAddCell_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(148, 6);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(100, 40);
            this.buttonDel.TabIndex = 3;
            this.buttonDel.Text = "Удалить строку";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.ButtonDel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(11, 6);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 40);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Сохранить строку";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // buttonShowProperties
            // 
            this.buttonShowProperties.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonShowProperties.Location = new System.Drawing.Point(0, 0);
            this.buttonShowProperties.Name = "buttonShowProperties";
            this.buttonShowProperties.Size = new System.Drawing.Size(135, 25);
            this.buttonShowProperties.TabIndex = 2;
            this.buttonShowProperties.Text = "Свойства строки";
            this.buttonShowProperties.UseVisualStyleBackColor = true;
            this.buttonShowProperties.Click += new System.EventHandler(this.ButtonShowProperties_Click);
            // 
            // numericUpDownOrder
            // 
            this.numericUpDownOrder.Location = new System.Drawing.Point(256, 4);
            this.numericUpDownOrder.Name = "numericUpDownOrder";
            this.numericUpDownOrder.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownOrder.TabIndex = 1;
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.Location = new System.Drawing.Point(141, 6);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(109, 13);
            this.labelOrder.TabIndex = 0;
            this.labelOrder.Text = "Порядковый номер:";
            // 
            // panelOrder
            // 
            this.panelOrder.Controls.Add(this.buttonShowProperties);
            this.panelOrder.Controls.Add(this.numericUpDownOrder);
            this.panelOrder.Controls.Add(this.labelOrder);
            this.panelOrder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelOrder.Location = new System.Drawing.Point(128, 475);
            this.panelOrder.Name = "panelOrder";
            this.panelOrder.Size = new System.Drawing.Size(672, 25);
            this.panelOrder.TabIndex = 2;
            // 
            // textBoxTableRowHeight
            // 
            this.textBoxTableRowHeight.Location = new System.Drawing.Point(9, 77);
            this.textBoxTableRowHeight.Name = "textBoxTableRowHeight";
            this.textBoxTableRowHeight.Size = new System.Drawing.Size(100, 20);
            this.textBoxTableRowHeight.TabIndex = 3;
            // 
            // labelTableRowHeight
            // 
            this.labelTableRowHeight.AutoSize = true;
            this.labelTableRowHeight.Location = new System.Drawing.Point(9, 61);
            this.labelTableRowHeight.Name = "labelTableRowHeight";
            this.labelTableRowHeight.Size = new System.Drawing.Size(86, 13);
            this.labelTableRowHeight.TabIndex = 2;
            this.labelTableRowHeight.Text = "Высота строки:";
            // 
            // textBoxCantSplit
            // 
            this.textBoxCantSplit.Location = new System.Drawing.Point(9, 38);
            this.textBoxCantSplit.Name = "textBoxCantSplit";
            this.textBoxCantSplit.Size = new System.Drawing.Size(100, 20);
            this.textBoxCantSplit.TabIndex = 1;
            // 
            // labelCantSplit
            // 
            this.labelCantSplit.AutoSize = true;
            this.labelCantSplit.Location = new System.Drawing.Point(9, 22);
            this.labelCantSplit.Name = "labelCantSplit";
            this.labelCantSplit.Size = new System.Drawing.Size(52, 13);
            this.labelCantSplit.TabIndex = 0;
            this.labelCantSplit.Text = "CantSplit:";
            // 
            // panelTableRowProperties
            // 
            this.panelTableRowProperties.Controls.Add(this.groupBoxIndentation);
            this.panelTableRowProperties.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTableRowProperties.Location = new System.Drawing.Point(0, 0);
            this.panelTableRowProperties.Name = "panelTableRowProperties";
            this.panelTableRowProperties.Size = new System.Drawing.Size(128, 500);
            this.panelTableRowProperties.TabIndex = 0;
            this.panelTableRowProperties.Visible = false;
            // 
            // groupBoxIndentation
            // 
            this.groupBoxIndentation.Controls.Add(this.textBoxTableRowHeight);
            this.groupBoxIndentation.Controls.Add(this.labelTableRowHeight);
            this.groupBoxIndentation.Controls.Add(this.textBoxCantSplit);
            this.groupBoxIndentation.Controls.Add(this.labelCantSplit);
            this.groupBoxIndentation.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxIndentation.Location = new System.Drawing.Point(0, 0);
            this.groupBoxIndentation.Name = "groupBoxIndentation";
            this.groupBoxIndentation.Size = new System.Drawing.Size(128, 106);
            this.groupBoxIndentation.TabIndex = 2;
            this.groupBoxIndentation.TabStop = false;
            this.groupBoxIndentation.Text = "Прочие:";
            // 
            // panelCells
            // 
            this.panelCells.AutoScroll = true;
            this.panelCells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCells.Location = new System.Drawing.Point(0, 0);
            this.panelCells.Name = "panelCells";
            this.panelCells.Size = new System.Drawing.Size(800, 500);
            this.panelCells.TabIndex = 3;
            // 
            // ControlTicketTemplateViewerTableRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelAction);
            this.Controls.Add(this.panelOrder);
            this.Controls.Add(this.panelTableRowProperties);
            this.Controls.Add(this.panelCells);
            this.Name = "ControlTicketTemplateViewerTableRow";
            this.Size = new System.Drawing.Size(800, 500);
            this.panelAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).EndInit();
            this.panelOrder.ResumeLayout(false);
            this.panelOrder.PerformLayout();
            this.panelTableRowProperties.ResumeLayout(false);
            this.groupBoxIndentation.ResumeLayout(false);
            this.groupBoxIndentation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelAction;
        private System.Windows.Forms.Button buttonAddCell;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonShowProperties;
        private System.Windows.Forms.NumericUpDown numericUpDownOrder;
        private System.Windows.Forms.Label labelOrder;
        private System.Windows.Forms.Panel panelOrder;
        private System.Windows.Forms.TextBox textBoxTableRowHeight;
        private System.Windows.Forms.Label labelTableRowHeight;
        private System.Windows.Forms.TextBox textBoxCantSplit;
        private System.Windows.Forms.Label labelCantSplit;
        private System.Windows.Forms.Panel panelTableRowProperties;
        private System.Windows.Forms.GroupBox groupBoxIndentation;
        private System.Windows.Forms.Panel panelCells;
    }
}
