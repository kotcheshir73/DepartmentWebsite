namespace ScheduleControlsAndForms.Examination
{
    partial class ScheduleExaminationControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewFirstWeek = new System.Windows.Forms.DataGridView();
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelTop = new System.Windows.Forms.Label();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonUpd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRef = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonConsultation = new System.Windows.Forms.ToolStripButton();
            this.ColumnWeek1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWeek1Lesson1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWeek1Lesson2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWeek1Lesson7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWeek1Lesson8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFirstWeek)).BeginInit();
            this.panelTop.SuspendLayout();
            this.toolStripMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewFirstWeek
            // 
            this.dataGridViewFirstWeek.AllowUserToAddRows = false;
            this.dataGridViewFirstWeek.AllowUserToDeleteRows = false;
            this.dataGridViewFirstWeek.AllowUserToOrderColumns = true;
            this.dataGridViewFirstWeek.AllowUserToResizeColumns = false;
            this.dataGridViewFirstWeek.AllowUserToResizeRows = false;
            this.dataGridViewFirstWeek.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFirstWeek.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewFirstWeek.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFirstWeek.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnWeek1,
            this.ColumnWeek1Lesson1,
            this.ColumnWeek1Lesson2,
            this.ColumnWeek1Lesson7,
            this.ColumnWeek1Lesson8});
            this.dataGridViewFirstWeek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewFirstWeek.Location = new System.Drawing.Point(0, 55);
            this.dataGridViewFirstWeek.MultiSelect = false;
            this.dataGridViewFirstWeek.Name = "dataGridViewFirstWeek";
            this.dataGridViewFirstWeek.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFirstWeek.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewFirstWeek.RowHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFirstWeek.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewFirstWeek.RowTemplate.Height = 45;
            this.dataGridViewFirstWeek.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewFirstWeek.Size = new System.Drawing.Size(800, 445);
            this.dataGridViewFirstWeek.TabIndex = 2;
            this.dataGridViewFirstWeek.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_CellMouseDoubleClick);
            this.dataGridViewFirstWeek.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridView_KeyDown);
            this.dataGridViewFirstWeek.Resize += new System.EventHandler(this.DataGridView_Resize);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelTop);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 25);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(800, 30);
            this.panelTop.TabIndex = 1;
            // 
            // labelTop
            // 
            this.labelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTop.Location = new System.Drawing.Point(0, 0);
            this.labelTop.Name = "labelTop";
            this.labelTop.Size = new System.Drawing.Size(800, 30);
            this.labelTop.TabIndex = 1;
            this.labelTop.Text = "Text";
            this.labelTop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.toolStripButtonRef,
            this.toolStripSeparator4,
            this.toolStripButtonConsultation});
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonConsultation
            // 
            this.toolStripButtonConsultation.Image = global::ScheduleControlsAndForms.Properties.Resources.Add;
            this.toolStripButtonConsultation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonConsultation.Name = "toolStripButtonConsultation";
            this.toolStripButtonConsultation.Size = new System.Drawing.Size(104, 22);
            this.toolStripButtonConsultation.Text = "Консультация";
            this.toolStripButtonConsultation.Click += new System.EventHandler(this.ToolStripButtonConsultation_Click);
            // 
            // ColumnWeek1
            // 
            this.ColumnWeek1.HeaderText = "Дата";
            this.ColumnWeek1.Name = "ColumnWeek1";
            this.ColumnWeek1.ReadOnly = true;
            this.ColumnWeek1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnWeek1Lesson1
            // 
            this.ColumnWeek1Lesson1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            this.ColumnWeek1Lesson1.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnWeek1Lesson1.HeaderText = "Экзамен 8:00";
            this.ColumnWeek1Lesson1.Name = "ColumnWeek1Lesson1";
            this.ColumnWeek1Lesson1.ReadOnly = true;
            this.ColumnWeek1Lesson1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnWeek1Lesson2
            // 
            this.ColumnWeek1Lesson2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnWeek1Lesson2.HeaderText = "Экзамен 12:00";
            this.ColumnWeek1Lesson2.Name = "ColumnWeek1Lesson2";
            this.ColumnWeek1Lesson2.ReadOnly = true;
            this.ColumnWeek1Lesson2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnWeek1Lesson7
            // 
            this.ColumnWeek1Lesson7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            this.ColumnWeek1Lesson7.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnWeek1Lesson7.HeaderText = "Консультация 16:00";
            this.ColumnWeek1Lesson7.Name = "ColumnWeek1Lesson7";
            this.ColumnWeek1Lesson7.ReadOnly = true;
            this.ColumnWeek1Lesson7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnWeek1Lesson8
            // 
            this.ColumnWeek1Lesson8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnWeek1Lesson8.HeaderText = "Консультация 17:00";
            this.ColumnWeek1Lesson8.Name = "ColumnWeek1Lesson8";
            this.ColumnWeek1Lesson8.ReadOnly = true;
            this.ColumnWeek1Lesson8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ScheduleExaminationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewFirstWeek);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "ScheduleExaminationControl";
            this.Size = new System.Drawing.Size(800, 500);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFirstWeek)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewFirstWeek;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelTop;
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonUpd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonDel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonRef;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButtonConsultation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWeek1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWeek1Lesson1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWeek1Lesson2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWeek1Lesson7;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWeek1Lesson8;
    }
}
