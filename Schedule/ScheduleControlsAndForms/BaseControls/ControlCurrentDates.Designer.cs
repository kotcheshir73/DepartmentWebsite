namespace ScheduleControlsAndForms.BaseControls
{
    partial class ControlCurrentDates
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
            this.labelTop = new System.Windows.Forms.Label();
            this.labelFinishDate = new System.Windows.Forms.Label();
            this.labelStartDate = new System.Windows.Forms.Label();
            this.buttonNextDate = new System.Windows.Forms.Button();
            this.buttonPrevDate = new System.Windows.Forms.Button();
            this.controlCurrentTableView = new ScheduleControlsAndForms.BaseControls.ControlCurrentTableView();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelTop);
            this.panelTop.Controls.Add(this.labelFinishDate);
            this.panelTop.Controls.Add(this.labelStartDate);
            this.panelTop.Controls.Add(this.buttonNextDate);
            this.panelTop.Controls.Add(this.buttonPrevDate);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(800, 30);
            this.panelTop.TabIndex = 0;
            // 
            // labelTop
            // 
            this.labelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTop.Location = new System.Drawing.Point(100, 0);
            this.labelTop.Name = "labelTop";
            this.labelTop.Size = new System.Drawing.Size(600, 30);
            this.labelTop.TabIndex = 4;
            this.labelTop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelFinishDate
            // 
            this.labelFinishDate.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelFinishDate.Location = new System.Drawing.Point(700, 0);
            this.labelFinishDate.Name = "labelFinishDate";
            this.labelFinishDate.Size = new System.Drawing.Size(70, 30);
            this.labelFinishDate.TabIndex = 3;
            this.labelFinishDate.Text = "label2";
            this.labelFinishDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStartDate
            // 
            this.labelStartDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelStartDate.Location = new System.Drawing.Point(30, 0);
            this.labelStartDate.Name = "labelStartDate";
            this.labelStartDate.Size = new System.Drawing.Size(70, 30);
            this.labelStartDate.TabIndex = 2;
            this.labelStartDate.Text = "label1";
            this.labelStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonNextDate
            // 
            this.buttonNextDate.BackgroundImage = global::ScheduleControlsAndForms.Properties.Resources.Right;
            this.buttonNextDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonNextDate.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonNextDate.Location = new System.Drawing.Point(770, 0);
            this.buttonNextDate.Name = "buttonNextDate";
            this.buttonNextDate.Size = new System.Drawing.Size(30, 30);
            this.buttonNextDate.TabIndex = 1;
            this.buttonNextDate.UseVisualStyleBackColor = true;
            this.buttonNextDate.Click += new System.EventHandler(this.ButtonNextDate_Click);
            // 
            // buttonPrevDate
            // 
            this.buttonPrevDate.BackgroundImage = global::ScheduleControlsAndForms.Properties.Resources.Left;
            this.buttonPrevDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPrevDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonPrevDate.Location = new System.Drawing.Point(0, 0);
            this.buttonPrevDate.Name = "buttonPrevDate";
            this.buttonPrevDate.Size = new System.Drawing.Size(30, 30);
            this.buttonPrevDate.TabIndex = 0;
            this.buttonPrevDate.UseVisualStyleBackColor = true;
            this.buttonPrevDate.Click += new System.EventHandler(this.ButtonPrevDate_Click);
            // 
            // controlCurrentTableView
            // 
            this.controlCurrentTableView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlCurrentTableView.Location = new System.Drawing.Point(0, 30);
            this.controlCurrentTableView.Name = "controlCurrentTableView";
            this.controlCurrentTableView.Size = new System.Drawing.Size(800, 470);
            this.controlCurrentTableView.TabIndex = 1;
            // 
            // ControlCurrentDates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.controlCurrentTableView);
            this.Controls.Add(this.panelTop);
            this.Name = "ControlCurrentDates";
            this.Size = new System.Drawing.Size(800, 500);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button buttonPrevDate;
        private System.Windows.Forms.Button buttonNextDate;
        private System.Windows.Forms.Label labelFinishDate;
        private System.Windows.Forms.Label labelStartDate;
        private System.Windows.Forms.Label labelTop;
        private ControlCurrentTableView controlCurrentTableView;
    }
}
