namespace TicketViews.Views.TicketTemplate
{
    partial class TicketTemplateElement
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
            this.standartElementControl = new TicketViews.Controls.StandartElementControl();
            this.SuspendLayout();
            // 
            // standartElementControl
            // 
            this.standartElementControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.standartElementControl.Id = null;
            this.standartElementControl.Location = new System.Drawing.Point(0, 0);
            this.standartElementControl.Name = "standartElementControl";
            this.standartElementControl.Size = new System.Drawing.Size(300, 20);
            this.standartElementControl.TabIndex = 0;
            // 
            // TicketTemplateElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.standartElementControl);
            this.Name = "TicketTemplateElement";
            this.Size = new System.Drawing.Size(300, 20);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.StandartElementControl standartElementControl;
    }
}
