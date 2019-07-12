namespace ExaminationControlsAndForms.TicketTemplateTable
{
    partial class ControlTicketTemplateViewerTableGridColumn
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
            this.numericUpDownOrder = new System.Windows.Forms.NumericUpDown();
            this.labelOrder = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownOrder
            // 
            this.numericUpDownOrder.Location = new System.Drawing.Point(167, 6);
            this.numericUpDownOrder.Name = "numericUpDownOrder";
            this.numericUpDownOrder.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownOrder.TabIndex = 1;
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.Location = new System.Drawing.Point(7, 9);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(154, 13);
            this.labelOrder.TabIndex = 0;
            this.labelOrder.Text = "Порядковый номер колонки:";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(7, 35);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(94, 13);
            this.labelWidth.TabIndex = 2;
            this.labelWidth.Text = "Ширина колонки:";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(107, 32);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(100, 20);
            this.textBoxWidth.TabIndex = 3;
            // 
            // ControlTicketTemplateViewerTableGridColumn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.labelWidth);
            this.Controls.Add(this.numericUpDownOrder);
            this.Controls.Add(this.labelOrder);
            this.Name = "ControlTicketTemplateViewerTableGridColumn";
            this.Size = new System.Drawing.Size(240, 60);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownOrder;
        private System.Windows.Forms.Label labelOrder;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.TextBox textBoxWidth;
    }
}
