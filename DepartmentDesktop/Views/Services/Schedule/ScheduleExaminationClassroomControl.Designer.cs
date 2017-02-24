namespace DepartmentDesktop.Views.Services.Schedule
{
    partial class ScheduleExaminationClassroomControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewFirstWeek = new System.Windows.Forms.DataGridView();
            this.contextMenuStripExamination = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.отчисткаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.полнаяОтчисткаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.импортToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hTMLToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.импортВсегоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.hTMLToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортВсегоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.hTMLToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelTop = new System.Windows.Forms.Label();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRef = new System.Windows.Forms.ToolStripButton();
            this.ColumnWeek1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWeek1Lesson1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWeek1Lesson2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWeek1Lesson3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWeek1Lesson5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWeek1Lesson6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFirstWeek)).BeginInit();
            this.contextMenuStripExamination.SuspendLayout();
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
            this.ColumnWeek1Lesson3,
            this.ColumnWeek1Lesson5,
            this.ColumnWeek1Lesson6});
            this.dataGridViewFirstWeek.ContextMenuStrip = this.contextMenuStripExamination;
            this.dataGridViewFirstWeek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewFirstWeek.Location = new System.Drawing.Point(0, 55);
            this.dataGridViewFirstWeek.MultiSelect = false;
            this.dataGridViewFirstWeek.Name = "dataGridViewFirstWeek";
            this.dataGridViewFirstWeek.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFirstWeek.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewFirstWeek.RowHeadersVisible = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFirstWeek.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewFirstWeek.RowTemplate.Height = 45;
            this.dataGridViewFirstWeek.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewFirstWeek.Size = new System.Drawing.Size(800, 445);
            this.dataGridViewFirstWeek.TabIndex = 2;
            this.dataGridViewFirstWeek.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDoubleClick);
            this.dataGridViewFirstWeek.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_KeyDown);
            this.dataGridViewFirstWeek.Resize += new System.EventHandler(this.dataGridView_Resize);
            // 
            // contextMenuStripExamination
            // 
            this.contextMenuStripExamination.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отчисткаToolStripMenuItem,
            this.полнаяОтчисткаToolStripMenuItem,
            this.импортToolStripMenuItem,
            this.экспортToolStripMenuItem,
            this.импортВсегоToolStripMenuItem,
            this.экспортВсегоToolStripMenuItem});
            this.contextMenuStripExamination.Name = "contextMenuStrip";
            this.contextMenuStripExamination.Size = new System.Drawing.Size(169, 136);
            // 
            // отчисткаToolStripMenuItem
            // 
            this.отчисткаToolStripMenuItem.Name = "отчисткаToolStripMenuItem";
            this.отчисткаToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.отчисткаToolStripMenuItem.Tag = "0_1";
            this.отчисткаToolStripMenuItem.Text = "Отчистка";
            // 
            // полнаяОтчисткаToolStripMenuItem
            // 
            this.полнаяОтчисткаToolStripMenuItem.Name = "полнаяОтчисткаToolStripMenuItem";
            this.полнаяОтчисткаToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.полнаяОтчисткаToolStripMenuItem.Text = "Полная отчистка";
            // 
            // импортToolStripMenuItem
            // 
            this.импортToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excelToolStripMenuItem1,
            this.hTMLToolStripMenuItem1,
            this.xMLToolStripMenuItem1});
            this.импортToolStripMenuItem.Name = "импортToolStripMenuItem";
            this.импортToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.импортToolStripMenuItem.Text = "Импорт";
            // 
            // excelToolStripMenuItem1
            // 
            this.excelToolStripMenuItem1.Name = "excelToolStripMenuItem1";
            this.excelToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.excelToolStripMenuItem1.Text = "Excel";
            // 
            // hTMLToolStripMenuItem1
            // 
            this.hTMLToolStripMenuItem1.Name = "hTMLToolStripMenuItem1";
            this.hTMLToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.hTMLToolStripMenuItem1.Text = "HTML";
            // 
            // xMLToolStripMenuItem1
            // 
            this.xMLToolStripMenuItem1.Name = "xMLToolStripMenuItem1";
            this.xMLToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.xMLToolStripMenuItem1.Text = "XML";
            // 
            // экспортToolStripMenuItem
            // 
            this.экспортToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excelToolStripMenuItem,
            this.hTMLToolStripMenuItem,
            this.xMLToolStripMenuItem});
            this.экспортToolStripMenuItem.Name = "экспортToolStripMenuItem";
            this.экспортToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.экспортToolStripMenuItem.Text = "Экспорт";
            // 
            // excelToolStripMenuItem
            // 
            this.excelToolStripMenuItem.Name = "excelToolStripMenuItem";
            this.excelToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.excelToolStripMenuItem.Text = "Excel";
            // 
            // hTMLToolStripMenuItem
            // 
            this.hTMLToolStripMenuItem.Name = "hTMLToolStripMenuItem";
            this.hTMLToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.hTMLToolStripMenuItem.Text = "HTML";
            // 
            // xMLToolStripMenuItem
            // 
            this.xMLToolStripMenuItem.Name = "xMLToolStripMenuItem";
            this.xMLToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.xMLToolStripMenuItem.Text = "XML";
            // 
            // импортВсегоToolStripMenuItem
            // 
            this.импортВсегоToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excelToolStripMenuItem2,
            this.hTMLToolStripMenuItem2,
            this.xMLToolStripMenuItem2});
            this.импортВсегоToolStripMenuItem.Name = "импортВсегоToolStripMenuItem";
            this.импортВсегоToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.импортВсегоToolStripMenuItem.Text = "Импорт всего";
            // 
            // excelToolStripMenuItem2
            // 
            this.excelToolStripMenuItem2.Name = "excelToolStripMenuItem2";
            this.excelToolStripMenuItem2.Size = new System.Drawing.Size(107, 22);
            this.excelToolStripMenuItem2.Text = "Excel";
            // 
            // hTMLToolStripMenuItem2
            // 
            this.hTMLToolStripMenuItem2.Name = "hTMLToolStripMenuItem2";
            this.hTMLToolStripMenuItem2.Size = new System.Drawing.Size(107, 22);
            this.hTMLToolStripMenuItem2.Text = "HTML";
            // 
            // xMLToolStripMenuItem2
            // 
            this.xMLToolStripMenuItem2.Name = "xMLToolStripMenuItem2";
            this.xMLToolStripMenuItem2.Size = new System.Drawing.Size(107, 22);
            this.xMLToolStripMenuItem2.Text = "XML";
            // 
            // экспортВсегоToolStripMenuItem
            // 
            this.экспортВсегоToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excelToolStripMenuItem3,
            this.hTMLToolStripMenuItem3,
            this.xMLToolStripMenuItem3});
            this.экспортВсегоToolStripMenuItem.Name = "экспортВсегоToolStripMenuItem";
            this.экспортВсегоToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.экспортВсегоToolStripMenuItem.Text = "Экспорт всего";
            // 
            // excelToolStripMenuItem3
            // 
            this.excelToolStripMenuItem3.Name = "excelToolStripMenuItem3";
            this.excelToolStripMenuItem3.Size = new System.Drawing.Size(107, 22);
            this.excelToolStripMenuItem3.Text = "Excel";
            // 
            // hTMLToolStripMenuItem3
            // 
            this.hTMLToolStripMenuItem3.Name = "hTMLToolStripMenuItem3";
            this.hTMLToolStripMenuItem3.Size = new System.Drawing.Size(107, 22);
            this.hTMLToolStripMenuItem3.Text = "HTML";
            // 
            // xMLToolStripMenuItem3
            // 
            this.xMLToolStripMenuItem3.Name = "xMLToolStripMenuItem3";
            this.xMLToolStripMenuItem3.Size = new System.Drawing.Size(107, 22);
            this.xMLToolStripMenuItem3.Text = "XML";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
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
            this.toolStripButtonRef});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(800, 25);
            this.toolStripMenu.TabIndex = 0;
            this.toolStripMenu.Text = "Действия";
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.Image = global::DepartmentDesktop.Properties.Resources.Add;
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(79, 22);
            this.toolStripButtonAdd.Text = "Добавить";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonRef
            // 
            this.toolStripButtonRef.Image = global::DepartmentDesktop.Properties.Resources.Ref;
            this.toolStripButtonRef.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRef.Name = "toolStripButtonRef";
            this.toolStripButtonRef.Size = new System.Drawing.Size(81, 22);
            this.toolStripButtonRef.Text = "Обновить";
            this.toolStripButtonRef.Click += new System.EventHandler(this.toolStripButtonRef_Click);
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
            // ColumnWeek1Lesson3
            // 
            this.ColumnWeek1Lesson3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            this.ColumnWeek1Lesson3.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnWeek1Lesson3.HeaderText = "Консультация 14:00";
            this.ColumnWeek1Lesson3.Name = "ColumnWeek1Lesson3";
            this.ColumnWeek1Lesson3.ReadOnly = true;
            this.ColumnWeek1Lesson3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnWeek1Lesson5
            // 
            this.ColumnWeek1Lesson5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightGray;
            this.ColumnWeek1Lesson5.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnWeek1Lesson5.HeaderText = "Консультация 16:00";
            this.ColumnWeek1Lesson5.Name = "ColumnWeek1Lesson5";
            this.ColumnWeek1Lesson5.ReadOnly = true;
            this.ColumnWeek1Lesson5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnWeek1Lesson6
            // 
            this.ColumnWeek1Lesson6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnWeek1Lesson6.HeaderText = "Консультация 17:00";
            this.ColumnWeek1Lesson6.Name = "ColumnWeek1Lesson6";
            this.ColumnWeek1Lesson6.ReadOnly = true;
            this.ColumnWeek1Lesson6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ScheduleExaminationClassroomControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewFirstWeek);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "ScheduleExaminationClassroomControl";
            this.Size = new System.Drawing.Size(800, 500);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFirstWeek)).EndInit();
            this.contextMenuStripExamination.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewFirstWeek;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripExamination;
        private System.Windows.Forms.ToolStripMenuItem отчисткаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem полнаяОтчисткаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem импортToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hTMLToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem экспортToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem импортВсегоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem hTMLToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem экспортВсегоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem hTMLToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelTop;
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonRef;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWeek1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWeek1Lesson1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWeek1Lesson2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWeek1Lesson3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWeek1Lesson5;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWeek1Lesson6;
    }
}
