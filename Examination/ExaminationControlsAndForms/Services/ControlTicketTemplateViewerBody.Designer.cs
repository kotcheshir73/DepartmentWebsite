namespace ExaminationControlsAndForms.Services
{
    partial class ControlTicketTemplateViewerBody
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
            this.button1 = new System.Windows.Forms.Button();
            this.buttonAddParagraph = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panelParagraphs = new System.Windows.Forms.Panel();
            this.panelProperties = new System.Windows.Forms.Panel();
            this.groupBoxRunningTitle = new System.Windows.Forms.GroupBox();
            this.textBoxFooter = new System.Windows.Forms.TextBox();
            this.labelFooter = new System.Windows.Forms.Label();
            this.textBoxGutter = new System.Windows.Forms.TextBox();
            this.labelGutter = new System.Windows.Forms.Label();
            this.groupBoxPageMargin = new System.Windows.Forms.GroupBox();
            this.textBoxRight = new System.Windows.Forms.TextBox();
            this.labelRight = new System.Windows.Forms.Label();
            this.textBoxLeft = new System.Windows.Forms.TextBox();
            this.labelLeft = new System.Windows.Forms.Label();
            this.textBoxBottom = new System.Windows.Forms.TextBox();
            this.labelBottom = new System.Windows.Forms.Label();
            this.textBoxTop = new System.Windows.Forms.TextBox();
            this.labelTop = new System.Windows.Forms.Label();
            this.groupBoxPageSize = new System.Windows.Forms.GroupBox();
            this.textBoxOrient = new System.Windows.Forms.TextBox();
            this.labelOrient = new System.Windows.Forms.Label();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.labelHeight = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.labelWidth = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panelTables = new System.Windows.Forms.Panel();
            this.panelAction.SuspendLayout();
            this.panelProperties.SuspendLayout();
            this.groupBoxRunningTitle.SuspendLayout();
            this.groupBoxPageMargin.SuspendLayout();
            this.groupBoxPageSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAction
            // 
            this.panelAction.Controls.Add(this.button1);
            this.panelAction.Controls.Add(this.buttonAddParagraph);
            this.panelAction.Controls.Add(this.buttonSave);
            this.panelAction.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelAction.Location = new System.Drawing.Point(750, 0);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(150, 500);
            this.panelAction.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 144);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 40);
            this.button1.TabIndex = 4;
            this.button1.Text = "Добавить таблицу";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // buttonAddParagraph
            // 
            this.buttonAddParagraph.Location = new System.Drawing.Point(24, 84);
            this.buttonAddParagraph.Name = "buttonAddParagraph";
            this.buttonAddParagraph.Size = new System.Drawing.Size(100, 40);
            this.buttonAddParagraph.TabIndex = 1;
            this.buttonAddParagraph.Text = "Добавить параграф";
            this.buttonAddParagraph.UseVisualStyleBackColor = true;
            this.buttonAddParagraph.Click += new System.EventHandler(this.ButtonAddParagraph_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(24, 22);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 40);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Сохранить настройки";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // panelParagraphs
            // 
            this.panelParagraphs.AutoScroll = true;
            this.panelParagraphs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelParagraphs.Location = new System.Drawing.Point(0, 0);
            this.panelParagraphs.Name = "panelParagraphs";
            this.panelParagraphs.Size = new System.Drawing.Size(748, 199);
            this.panelParagraphs.TabIndex = 3;
            // 
            // panelProperties
            // 
            this.panelProperties.Controls.Add(this.groupBoxRunningTitle);
            this.panelProperties.Controls.Add(this.groupBoxPageMargin);
            this.panelProperties.Controls.Add(this.groupBoxPageSize);
            this.panelProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelProperties.Location = new System.Drawing.Point(0, 0);
            this.panelProperties.Name = "panelProperties";
            this.panelProperties.Size = new System.Drawing.Size(750, 98);
            this.panelProperties.TabIndex = 4;
            // 
            // groupBoxRunningTitle
            // 
            this.groupBoxRunningTitle.Controls.Add(this.textBoxFooter);
            this.groupBoxRunningTitle.Controls.Add(this.labelFooter);
            this.groupBoxRunningTitle.Controls.Add(this.textBoxGutter);
            this.groupBoxRunningTitle.Controls.Add(this.labelGutter);
            this.groupBoxRunningTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxRunningTitle.Location = new System.Drawing.Point(561, 0);
            this.groupBoxRunningTitle.Name = "groupBoxRunningTitle";
            this.groupBoxRunningTitle.Size = new System.Drawing.Size(200, 98);
            this.groupBoxRunningTitle.TabIndex = 2;
            this.groupBoxRunningTitle.TabStop = false;
            this.groupBoxRunningTitle.Text = "Колонтитулы";
            // 
            // textBoxFooter
            // 
            this.textBoxFooter.Location = new System.Drawing.Point(77, 45);
            this.textBoxFooter.Name = "textBoxFooter";
            this.textBoxFooter.Size = new System.Drawing.Size(100, 20);
            this.textBoxFooter.TabIndex = 3;
            // 
            // labelFooter
            // 
            this.labelFooter.AutoSize = true;
            this.labelFooter.Location = new System.Drawing.Point(19, 48);
            this.labelFooter.Name = "labelFooter";
            this.labelFooter.Size = new System.Drawing.Size(50, 13);
            this.labelFooter.TabIndex = 2;
            this.labelFooter.Text = "Нижний:";
            // 
            // textBoxGutter
            // 
            this.textBoxGutter.Location = new System.Drawing.Point(77, 19);
            this.textBoxGutter.Name = "textBoxGutter";
            this.textBoxGutter.Size = new System.Drawing.Size(100, 20);
            this.textBoxGutter.TabIndex = 1;
            // 
            // labelGutter
            // 
            this.labelGutter.AutoSize = true;
            this.labelGutter.Location = new System.Drawing.Point(19, 22);
            this.labelGutter.Name = "labelGutter";
            this.labelGutter.Size = new System.Drawing.Size(52, 13);
            this.labelGutter.TabIndex = 0;
            this.labelGutter.Text = "Верхний:";
            // 
            // groupBoxPageMargin
            // 
            this.groupBoxPageMargin.Controls.Add(this.textBoxRight);
            this.groupBoxPageMargin.Controls.Add(this.labelRight);
            this.groupBoxPageMargin.Controls.Add(this.textBoxLeft);
            this.groupBoxPageMargin.Controls.Add(this.labelLeft);
            this.groupBoxPageMargin.Controls.Add(this.textBoxBottom);
            this.groupBoxPageMargin.Controls.Add(this.labelBottom);
            this.groupBoxPageMargin.Controls.Add(this.textBoxTop);
            this.groupBoxPageMargin.Controls.Add(this.labelTop);
            this.groupBoxPageMargin.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxPageMargin.Location = new System.Drawing.Point(212, 0);
            this.groupBoxPageMargin.Name = "groupBoxPageMargin";
            this.groupBoxPageMargin.Size = new System.Drawing.Size(349, 98);
            this.groupBoxPageMargin.TabIndex = 1;
            this.groupBoxPageMargin.TabStop = false;
            this.groupBoxPageMargin.Text = "Отступы";
            // 
            // textBoxRight
            // 
            this.textBoxRight.Location = new System.Drawing.Point(230, 45);
            this.textBoxRight.Name = "textBoxRight";
            this.textBoxRight.Size = new System.Drawing.Size(100, 20);
            this.textBoxRight.TabIndex = 7;
            // 
            // labelRight
            // 
            this.labelRight.AutoSize = true;
            this.labelRight.Location = new System.Drawing.Point(177, 48);
            this.labelRight.Name = "labelRight";
            this.labelRight.Size = new System.Drawing.Size(47, 13);
            this.labelRight.TabIndex = 6;
            this.labelRight.Text = "Справа:";
            // 
            // textBoxLeft
            // 
            this.textBoxLeft.Location = new System.Drawing.Point(230, 19);
            this.textBoxLeft.Name = "textBoxLeft";
            this.textBoxLeft.Size = new System.Drawing.Size(100, 20);
            this.textBoxLeft.TabIndex = 5;
            // 
            // labelLeft
            // 
            this.labelLeft.AutoSize = true;
            this.labelLeft.Location = new System.Drawing.Point(177, 22);
            this.labelLeft.Name = "labelLeft";
            this.labelLeft.Size = new System.Drawing.Size(41, 13);
            this.labelLeft.TabIndex = 4;
            this.labelLeft.Text = "Слева:";
            // 
            // textBoxBottom
            // 
            this.textBoxBottom.Location = new System.Drawing.Point(71, 45);
            this.textBoxBottom.Name = "textBoxBottom";
            this.textBoxBottom.Size = new System.Drawing.Size(100, 20);
            this.textBoxBottom.TabIndex = 3;
            // 
            // labelBottom
            // 
            this.labelBottom.AutoSize = true;
            this.labelBottom.Location = new System.Drawing.Point(18, 48);
            this.labelBottom.Name = "labelBottom";
            this.labelBottom.Size = new System.Drawing.Size(40, 13);
            this.labelBottom.TabIndex = 2;
            this.labelBottom.Text = "Снизу:";
            // 
            // textBoxTop
            // 
            this.textBoxTop.Location = new System.Drawing.Point(71, 19);
            this.textBoxTop.Name = "textBoxTop";
            this.textBoxTop.Size = new System.Drawing.Size(100, 20);
            this.textBoxTop.TabIndex = 1;
            // 
            // labelTop
            // 
            this.labelTop.AutoSize = true;
            this.labelTop.Location = new System.Drawing.Point(18, 22);
            this.labelTop.Name = "labelTop";
            this.labelTop.Size = new System.Drawing.Size(45, 13);
            this.labelTop.TabIndex = 0;
            this.labelTop.Text = "Сверху:";
            // 
            // groupBoxPageSize
            // 
            this.groupBoxPageSize.Controls.Add(this.textBoxOrient);
            this.groupBoxPageSize.Controls.Add(this.labelOrient);
            this.groupBoxPageSize.Controls.Add(this.textBoxHeight);
            this.groupBoxPageSize.Controls.Add(this.labelHeight);
            this.groupBoxPageSize.Controls.Add(this.textBoxWidth);
            this.groupBoxPageSize.Controls.Add(this.labelWidth);
            this.groupBoxPageSize.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxPageSize.Location = new System.Drawing.Point(0, 0);
            this.groupBoxPageSize.Name = "groupBoxPageSize";
            this.groupBoxPageSize.Size = new System.Drawing.Size(212, 98);
            this.groupBoxPageSize.TabIndex = 0;
            this.groupBoxPageSize.TabStop = false;
            this.groupBoxPageSize.Text = "Размеры странцы";
            // 
            // textBoxOrient
            // 
            this.textBoxOrient.Location = new System.Drawing.Point(92, 71);
            this.textBoxOrient.Name = "textBoxOrient";
            this.textBoxOrient.Size = new System.Drawing.Size(100, 20);
            this.textBoxOrient.TabIndex = 5;
            // 
            // labelOrient
            // 
            this.labelOrient.AutoSize = true;
            this.labelOrient.Location = new System.Drawing.Point(15, 74);
            this.labelOrient.Name = "labelOrient";
            this.labelOrient.Size = new System.Drawing.Size(71, 13);
            this.labelOrient.TabIndex = 4;
            this.labelOrient.Text = "Ориентация:";
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(92, 45);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(100, 20);
            this.textBoxHeight.TabIndex = 3;
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(15, 48);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(48, 13);
            this.labelHeight.TabIndex = 2;
            this.labelHeight.Text = "Высота:";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(92, 19);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(100, 20);
            this.textBoxWidth.TabIndex = 1;
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(15, 22);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(49, 13);
            this.labelWidth.TabIndex = 0;
            this.labelWidth.Text = "Ширина:";
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 98);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.panelParagraphs);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panelTables);
            this.splitContainer.Size = new System.Drawing.Size(750, 402);
            this.splitContainer.SplitterDistance = 201;
            this.splitContainer.SplitterWidth = 8;
            this.splitContainer.TabIndex = 4;
            // 
            // panelTables
            // 
            this.panelTables.AutoScroll = true;
            this.panelTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTables.Location = new System.Drawing.Point(0, 0);
            this.panelTables.Name = "panelTables";
            this.panelTables.Size = new System.Drawing.Size(748, 191);
            this.panelTables.TabIndex = 0;
            // 
            // ControlTicketTemplateViewerBody
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelProperties);
            this.Controls.Add(this.panelAction);
            this.Name = "ControlTicketTemplateViewerBody";
            this.Size = new System.Drawing.Size(900, 500);
            this.panelAction.ResumeLayout(false);
            this.panelProperties.ResumeLayout(false);
            this.groupBoxRunningTitle.ResumeLayout(false);
            this.groupBoxRunningTitle.PerformLayout();
            this.groupBoxPageMargin.ResumeLayout(false);
            this.groupBoxPageMargin.PerformLayout();
            this.groupBoxPageSize.ResumeLayout(false);
            this.groupBoxPageSize.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelAction;
        private System.Windows.Forms.Panel panelParagraphs;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonAddParagraph;
        private System.Windows.Forms.Panel panelProperties;
        private System.Windows.Forms.GroupBox groupBoxPageSize;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.TextBox textBoxOrient;
        private System.Windows.Forms.Label labelOrient;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.GroupBox groupBoxPageMargin;
        private System.Windows.Forms.TextBox textBoxTop;
        private System.Windows.Forms.Label labelTop;
        private System.Windows.Forms.TextBox textBoxBottom;
        private System.Windows.Forms.Label labelBottom;
        private System.Windows.Forms.TextBox textBoxLeft;
        private System.Windows.Forms.Label labelLeft;
        private System.Windows.Forms.TextBox textBoxRight;
        private System.Windows.Forms.Label labelRight;
        private System.Windows.Forms.GroupBox groupBoxRunningTitle;
        private System.Windows.Forms.TextBox textBoxFooter;
        private System.Windows.Forms.Label labelFooter;
        private System.Windows.Forms.TextBox textBoxGutter;
        private System.Windows.Forms.Label labelGutter;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panelTables;
    }
}
