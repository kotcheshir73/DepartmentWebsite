namespace ScheduleControlsAndForms.BaseControls
{
    partial class ControlCurrentObjects
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.buttonNextDate = new System.Windows.Forms.Button();
            this.buttonPrevDate = new System.Windows.Forms.Button();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.controlCurrentTableView = new ScheduleControlsAndForms.BaseControls.ControlCurrentTableView();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.buttonNextDate);
            this.panelTop.Controls.Add(this.buttonPrevDate);
            this.panelTop.Controls.Add(this.dateTimePicker);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(800, 30);
            this.panelTop.TabIndex = 0;
            // 
            // buttonNextDate
            // 
            this.buttonNextDate.BackgroundImage = global::ScheduleControlsAndForms.Properties.Resources.Right;
            this.buttonNextDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonNextDate.Location = new System.Drawing.Point(239, 4);
            this.buttonNextDate.Name = "buttonNextDate";
            this.buttonNextDate.Size = new System.Drawing.Size(20, 20);
            this.buttonNextDate.TabIndex = 2;
            this.buttonNextDate.UseVisualStyleBackColor = true;
            this.buttonNextDate.Click += new System.EventHandler(this.ButtonNextDate_Click);
            // 
            // buttonPrevDate
            // 
            this.buttonPrevDate.BackgroundImage = global::ScheduleControlsAndForms.Properties.Resources.Left;
            this.buttonPrevDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPrevDate.Location = new System.Drawing.Point(27, 4);
            this.buttonPrevDate.Name = "buttonPrevDate";
            this.buttonPrevDate.Size = new System.Drawing.Size(20, 20);
            this.buttonPrevDate.TabIndex = 1;
            this.buttonPrevDate.UseVisualStyleBackColor = true;
            this.buttonPrevDate.Click += new System.EventHandler(this.ButtonPrevDate_Click);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.CustomFormat = "dd.MM.yyyy dddd";
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker.Location = new System.Drawing.Point(53, 4);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(180, 20);
            this.dateTimePicker.TabIndex = 0;
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.DateTimePicker_ValueChanged);
            // 
            // controlCurrentTableView
            // 
            this.controlCurrentTableView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlCurrentTableView.Location = new System.Drawing.Point(0, 30);
            this.controlCurrentTableView.Name = "controlCurrentTableView";
            this.controlCurrentTableView.Size = new System.Drawing.Size(800, 470);
            this.controlCurrentTableView.TabIndex = 1;
            // 
            // ControlCurrentObjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.controlCurrentTableView);
            this.Controls.Add(this.panelTop);
            this.Name = "ControlCurrentObjects";
            this.Size = new System.Drawing.Size(800, 500);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Button buttonPrevDate;
        private System.Windows.Forms.Button buttonNextDate;
        private ControlCurrentTableView controlCurrentTableView;
    }
}
