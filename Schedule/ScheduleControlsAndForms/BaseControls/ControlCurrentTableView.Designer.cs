namespace ScheduleControlsAndForms.BaseControls
{
    partial class ControlCurrentTableView
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenuStripButton = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DelRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddSemesterRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddOffsetRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddExaminationRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddConsultationRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.UpdRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripButton.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ContextMenuStrip = this.contextMenuStrip;
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(800, 500);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // contextMenuStripButton
            // 
            this.contextMenuStripButton.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UpdRecordToolStripMenuItem,
            this.DelRecordToolStripMenuItem});
            this.contextMenuStripButton.Name = "contextMenuStripDel";
            this.contextMenuStripButton.Size = new System.Drawing.Size(181, 70);
            // 
            // DelRecordToolStripMenuItem
            // 
            this.DelRecordToolStripMenuItem.Name = "DelRecordToolStripMenuItem";
            this.DelRecordToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.DelRecordToolStripMenuItem.Text = "Удалить";
            this.DelRecordToolStripMenuItem.Click += new System.EventHandler(this.DelRecordToolStripMenuItem_Click);
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
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddSemesterRecordToolStripMenuItem,
            this.AddOffsetRecordToolStripMenuItem,
            this.AddExaminationRecordToolStripMenuItem,
            this.AddConsultationRecordToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(210, 92);
            // 
            // UpdRecordToolStripMenuItem
            // 
            this.UpdRecordToolStripMenuItem.Name = "UpdRecordToolStripMenuItem";
            this.UpdRecordToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.UpdRecordToolStripMenuItem.Text = "Изменить";
            this.UpdRecordToolStripMenuItem.Click += new System.EventHandler(this.UpdRecordToolStripMenuItem_Click);
            // 
            // ControlCurrentTableView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "ControlCurrentTableView";
            this.Size = new System.Drawing.Size(800, 500);
            this.contextMenuStripButton.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripButton;
        private System.Windows.Forms.ToolStripMenuItem DelRecordToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem AddSemesterRecordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddOffsetRecordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddExaminationRecordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddConsultationRecordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UpdRecordToolStripMenuItem;
    }
}
