namespace ScheduleControlsAndForms.Consultation
{
    partial class ScheduleConsultationControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonUpd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRef = new System.Windows.Forms.ToolStripButton();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.labelTop = new System.Windows.Forms.Label();
            this.ColumnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAdd,
            this.toolStripSeparator1,
            this.toolStripButtonUpd,
            this.toolStripSeparator2,
            this.toolStripButtonDel,
            this.toolStripSeparator3,
            this.toolStripButtonRef});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(800, 25);
            this.toolStripMenu.TabIndex = 0;
            this.toolStripMenu.Text = "Действия";
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.Image = global::ScheduleControlsAndForms.Properties.Resources.Add;
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(79, 22);
            this.toolStripButtonAdd.Text = "Добавить";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.ToolStripButtonAdd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonUpd
            // 
            this.toolStripButtonUpd.Image = global::ScheduleControlsAndForms.Properties.Resources.Upd;
            this.toolStripButtonUpd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUpd.Name = "toolStripButtonUpd";
            this.toolStripButtonUpd.Size = new System.Drawing.Size(81, 22);
            this.toolStripButtonUpd.Text = "Изменить";
            this.toolStripButtonUpd.Click += new System.EventHandler(this.ToolStripButtonUpd_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonDel
            // 
            this.toolStripButtonDel.Image = global::ScheduleControlsAndForms.Properties.Resources.Del;
            this.toolStripButtonDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDel.Name = "toolStripButtonDel";
            this.toolStripButtonDel.Size = new System.Drawing.Size(71, 22);
            this.toolStripButtonDel.Text = "Удалить";
            this.toolStripButtonDel.Click += new System.EventHandler(this.ToolStripButtonDel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonRef
            // 
            this.toolStripButtonRef.Image = global::ScheduleControlsAndForms.Properties.Resources.Ref;
            this.toolStripButtonRef.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRef.Name = "toolStripButtonRef";
            this.toolStripButtonRef.Size = new System.Drawing.Size(81, 22);
            this.toolStripButtonRef.Text = "Обновить";
            this.toolStripButtonRef.Click += new System.EventHandler(this.ToolStripButtonRef_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDate});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 55);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(800, 445);
            this.dataGridView.TabIndex = 1;
            this.dataGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_CellMouseDoubleClick);
            this.dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridView_KeyDown);
            this.dataGridView.Resize += new System.EventHandler(this.DataGridView_Resize);
            // 
            // labelTop
            // 
            this.labelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTop.Location = new System.Drawing.Point(0, 25);
            this.labelTop.Name = "labelTop";
            this.labelTop.Size = new System.Drawing.Size(800, 30);
            this.labelTop.TabIndex = 2;
            this.labelTop.Text = "Text";
            this.labelTop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ColumnDate
            // 
            this.ColumnDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnDate.HeaderText = "Дата";
            this.ColumnDate.Name = "ColumnDate";
            this.ColumnDate.ReadOnly = true;
            // 
            // ScheduleConsultationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.labelTop);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "ScheduleConsultationControl";
            this.Size = new System.Drawing.Size(800, 500);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonUpd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonDel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonRef;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label labelTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDate;
    }
}
