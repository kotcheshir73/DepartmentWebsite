namespace ExaminationControlsAndForms.Services
{
    partial class ControlTicketTemplateViewerRun
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
            this.panelProperties = new System.Windows.Forms.Panel();
            this.textBoxSize = new System.Windows.Forms.TextBox();
            this.labelSize = new System.Windows.Forms.Label();
            this.checkBoxUnderline = new System.Windows.Forms.CheckBox();
            this.checkBoxItalic = new System.Windows.Forms.CheckBox();
            this.checkBoxBold = new System.Windows.Forms.CheckBox();
            this.textBox = new System.Windows.Forms.TextBox();
            this.panelAction = new System.Windows.Forms.Panel();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panelOrder = new System.Windows.Forms.Panel();
            this.checkBoxTab = new System.Windows.Forms.CheckBox();
            this.numericUpDownOrder = new System.Windows.Forms.NumericUpDown();
            this.labelOrder = new System.Windows.Forms.Label();
            this.buttonShowProperties = new System.Windows.Forms.Button();
            this.panelProperties.SuspendLayout();
            this.panelAction.SuspendLayout();
            this.panelOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // panelProperties
            // 
            this.panelProperties.Controls.Add(this.textBoxSize);
            this.panelProperties.Controls.Add(this.labelSize);
            this.panelProperties.Controls.Add(this.checkBoxUnderline);
            this.panelProperties.Controls.Add(this.checkBoxItalic);
            this.panelProperties.Controls.Add(this.checkBoxBold);
            this.panelProperties.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelProperties.Location = new System.Drawing.Point(0, 0);
            this.panelProperties.Name = "panelProperties";
            this.panelProperties.Size = new System.Drawing.Size(191, 67);
            this.panelProperties.TabIndex = 0;
            this.panelProperties.Visible = false;
            // 
            // textBoxSize
            // 
            this.textBoxSize.Location = new System.Drawing.Point(133, 33);
            this.textBoxSize.Name = "textBoxSize";
            this.textBoxSize.Size = new System.Drawing.Size(50, 20);
            this.textBoxSize.TabIndex = 4;
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(78, 36);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(49, 13);
            this.labelSize.TabIndex = 3;
            this.labelSize.Text = "Размер:";
            // 
            // checkBoxUnderline
            // 
            this.checkBoxUnderline.AutoSize = true;
            this.checkBoxUnderline.Location = new System.Drawing.Point(81, 9);
            this.checkBoxUnderline.Name = "checkBoxUnderline";
            this.checkBoxUnderline.Size = new System.Drawing.Size(91, 17);
            this.checkBoxUnderline.TabIndex = 2;
            this.checkBoxUnderline.Text = "Подчеркнуто";
            this.checkBoxUnderline.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxUnderline.UseVisualStyleBackColor = true;
            // 
            // checkBoxItalic
            // 
            this.checkBoxItalic.AutoSize = true;
            this.checkBoxItalic.Location = new System.Drawing.Point(6, 35);
            this.checkBoxItalic.Name = "checkBoxItalic";
            this.checkBoxItalic.Size = new System.Drawing.Size(62, 17);
            this.checkBoxItalic.TabIndex = 1;
            this.checkBoxItalic.Text = "Курсив";
            this.checkBoxItalic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxItalic.UseVisualStyleBackColor = true;
            // 
            // checkBoxBold
            // 
            this.checkBoxBold.AutoSize = true;
            this.checkBoxBold.Location = new System.Drawing.Point(6, 9);
            this.checkBoxBold.Name = "checkBoxBold";
            this.checkBoxBold.Size = new System.Drawing.Size(69, 17);
            this.checkBoxBold.TabIndex = 0;
            this.checkBoxBold.Text = "Жирный";
            this.checkBoxBold.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxBold.UseVisualStyleBackColor = true;
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.Location = new System.Drawing.Point(218, 0);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(266, 42);
            this.textBox.TabIndex = 1;
            // 
            // panelAction
            // 
            this.panelAction.Controls.Add(this.buttonDel);
            this.panelAction.Controls.Add(this.buttonSave);
            this.panelAction.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelAction.Location = new System.Drawing.Point(484, 0);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(88, 67);
            this.panelAction.TabIndex = 2;
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(6, 35);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(75, 23);
            this.buttonDel.TabIndex = 1;
            this.buttonDel.Text = "Удалить";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.ButtonDel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(6, 6);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // panelOrder
            // 
            this.panelOrder.Controls.Add(this.checkBoxTab);
            this.panelOrder.Controls.Add(this.numericUpDownOrder);
            this.panelOrder.Controls.Add(this.labelOrder);
            this.panelOrder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelOrder.Location = new System.Drawing.Point(218, 42);
            this.panelOrder.Name = "panelOrder";
            this.panelOrder.Size = new System.Drawing.Size(266, 25);
            this.panelOrder.TabIndex = 3;
            // 
            // checkBoxTab
            // 
            this.checkBoxTab.AutoSize = true;
            this.checkBoxTab.Location = new System.Drawing.Point(208, 4);
            this.checkBoxTab.Name = "checkBoxTab";
            this.checkBoxTab.Size = new System.Drawing.Size(45, 17);
            this.checkBoxTab.TabIndex = 2;
            this.checkBoxTab.Text = "Tab";
            this.checkBoxTab.UseVisualStyleBackColor = true;
            this.checkBoxTab.CheckedChanged += new System.EventHandler(this.CheckBoxTab_CheckedChanged);
            // 
            // numericUpDownOrder
            // 
            this.numericUpDownOrder.Location = new System.Drawing.Point(121, 3);
            this.numericUpDownOrder.Name = "numericUpDownOrder";
            this.numericUpDownOrder.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownOrder.TabIndex = 1;
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.Location = new System.Drawing.Point(6, 5);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(109, 13);
            this.labelOrder.TabIndex = 0;
            this.labelOrder.Text = "Порядковый номер:";
            // 
            // buttonShowProperties
            // 
            this.buttonShowProperties.BackgroundImage = global::ExaminationControlsAndForms.Properties.Resources.Right;
            this.buttonShowProperties.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonShowProperties.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonShowProperties.Location = new System.Drawing.Point(191, 0);
            this.buttonShowProperties.Name = "buttonShowProperties";
            this.buttonShowProperties.Size = new System.Drawing.Size(27, 67);
            this.buttonShowProperties.TabIndex = 4;
            this.buttonShowProperties.UseVisualStyleBackColor = true;
            this.buttonShowProperties.Click += new System.EventHandler(this.ButtonShowProperties_Click);
            // 
            // ControlTicketTemplateViewerRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.panelOrder);
            this.Controls.Add(this.buttonShowProperties);
            this.Controls.Add(this.panelAction);
            this.Controls.Add(this.panelProperties);
            this.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.Name = "ControlTicketTemplateViewerRun";
            this.Size = new System.Drawing.Size(572, 67);
            this.panelProperties.ResumeLayout(false);
            this.panelProperties.PerformLayout();
            this.panelAction.ResumeLayout(false);
            this.panelOrder.ResumeLayout(false);
            this.panelOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelProperties;
        private System.Windows.Forms.TextBox textBoxSize;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.CheckBox checkBoxUnderline;
        private System.Windows.Forms.CheckBox checkBoxItalic;
        private System.Windows.Forms.CheckBox checkBoxBold;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Panel panelAction;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panelOrder;
        private System.Windows.Forms.Label labelOrder;
        private System.Windows.Forms.NumericUpDown numericUpDownOrder;
        private System.Windows.Forms.Button buttonShowProperties;
        private System.Windows.Forms.CheckBox checkBoxTab;
    }
}
