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
            this.components = new System.ComponentModel.Container();
            this.panelTop = new System.Windows.Forms.Panel();
            this.buttonNextDate = new System.Windows.Forms.Button();
            this.buttonPrevDate = new System.Windows.Forms.Button();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddSemesterRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddOffsetRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddExaminationRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddConsultationRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStripDel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DelRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelTop.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.contextMenuStripDel.SuspendLayout();
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
            this.buttonNextDate.Location = new System.Drawing.Point(197, 4);
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
            this.dateTimePicker.Location = new System.Drawing.Point(53, 4);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(138, 20);
            this.dateTimePicker.TabIndex = 0;
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.DateTimePicker_ValueChanged);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoScroll = true;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ContextMenuStrip = this.contextMenuStrip;
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 30);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(800, 470);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddSemesterRecordToolStripMenuItem,
            this.AddOffsetRecordToolStripMenuItem,
            this.AddExaminationRecordToolStripMenuItem,
            this.AddConsultationRecordToolStripMenuItem,
            this.toolStripSeparator1});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(210, 98);
            // 
            // AddSemesterRecordToolStripMenuItem
            // 
            this.AddSemesterRecordToolStripMenuItem.Name = "AddSemesterRecordToolStripMenuItem";
            this.AddSemesterRecordToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.AddSemesterRecordToolStripMenuItem.Text = "Добавить пару";
            this.AddSemesterRecordToolStripMenuItem.Click += new System.EventHandler(this.AddSemesterRecordToolStripMenuItem_Click);
            // 
            // AddOffsetRecordToolStripMenuItem
            // 
            this.AddOffsetRecordToolStripMenuItem.Name = "AddOffsetRecordToolStripMenuItem";
            this.AddOffsetRecordToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.AddOffsetRecordToolStripMenuItem.Text = "Добавить зачет";
            this.AddOffsetRecordToolStripMenuItem.Click += new System.EventHandler(this.AddOffsetRecordToolStripMenuItem_Click);
            // 
            // AddExaminationRecordToolStripMenuItem
            // 
            this.AddExaminationRecordToolStripMenuItem.Name = "AddExaminationRecordToolStripMenuItem";
            this.AddExaminationRecordToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.AddExaminationRecordToolStripMenuItem.Text = "Добавить экзамен";
            this.AddExaminationRecordToolStripMenuItem.Click += new System.EventHandler(this.AddExaminationRecordToolStripMenuItem_Click);
            // 
            // AddConsultationRecordToolStripMenuItem
            // 
            this.AddConsultationRecordToolStripMenuItem.Name = "AddConsultationRecordToolStripMenuItem";
            this.AddConsultationRecordToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.AddConsultationRecordToolStripMenuItem.Text = "Добавить консультацию";
            this.AddConsultationRecordToolStripMenuItem.Click += new System.EventHandler(this.AddConsultationRecordToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(206, 6);
            // 
            // contextMenuStripDel
            // 
            this.contextMenuStripDel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DelRecordToolStripMenuItem});
            this.contextMenuStripDel.Name = "contextMenuStripDel";
            this.contextMenuStripDel.Size = new System.Drawing.Size(119, 26);
            // 
            // DelRecordToolStripMenuItem
            // 
            this.DelRecordToolStripMenuItem.Name = "DelRecordToolStripMenuItem";
            this.DelRecordToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.DelRecordToolStripMenuItem.Text = "Удалить";
            this.DelRecordToolStripMenuItem.Click += new System.EventHandler(this.DelRecordToolStripMenuItem_Click);
            // 
            // ControlCurrentObjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.panelTop);
            this.Name = "ControlCurrentObjects";
            this.Size = new System.Drawing.Size(800, 500);
            this.panelTop.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.contextMenuStripDel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDel;
        private System.Windows.Forms.ToolStripMenuItem DelRecordToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem AddSemesterRecordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddOffsetRecordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddExaminationRecordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddConsultationRecordToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button buttonPrevDate;
        private System.Windows.Forms.Button buttonNextDate;
    }
}
