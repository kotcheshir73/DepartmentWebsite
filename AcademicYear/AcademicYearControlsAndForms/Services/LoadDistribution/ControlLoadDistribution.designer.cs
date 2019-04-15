namespace AcademicYearControlsAndForms.Services.LoadDistribution
{
	partial class ControlLoadDistribution
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.dataGridViewList = new System.Windows.Forms.DataGridView();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonUpd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRef = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panelTop = new System.Windows.Forms.Panel();
            this.buttonCreateGrafics = new System.Windows.Forms.Button();
            this.buttonCreateNIRRecords = new System.Windows.Forms.Button();
            this.buttonCreatStatement = new System.Windows.Forms.Button();
            this.buttonCalcFactHours = new System.Windows.Forms.Button();
            this.comboBoxAcademicYear = new System.Windows.Forms.ComboBox();
            this.labelSelectAcademicYear = new System.Windows.Forms.Label();
            this.panelConfig = new System.Windows.Forms.Panel();
            this.checkBoxSelectAll = new System.Windows.Forms.CheckBox();
            this.checkBoxLecturerLoad = new System.Windows.Forms.CheckBox();
            this.buttonPanelConfig = new System.Windows.Forms.Button();
            this.checkBoxTimeNorm = new System.Windows.Forms.CheckBox();
            this.dataGridViewColumns = new System.Windows.Forms.DataGridView();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
            this.toolStripMenu.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewList
            // 
            this.dataGridViewList.AllowUserToAddRows = false;
            this.dataGridViewList.AllowUserToDeleteRows = false;
            this.dataGridViewList.AllowUserToResizeRows = false;
            this.dataGridViewList.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewList.ColumnHeadersHeight = 110;
            this.dataGridViewList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewList.Location = new System.Drawing.Point(0, 90);
            this.dataGridViewList.MultiSelect = false;
            this.dataGridViewList.Name = "dataGridViewList";
            this.dataGridViewList.ReadOnly = true;
            this.dataGridViewList.RowHeadersVisible = false;
            this.dataGridViewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewList.Size = new System.Drawing.Size(1400, 410);
            this.dataGridViewList.TabIndex = 2;
            this.dataGridViewList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewList_CellDoubleClick);
            this.dataGridViewList.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DataGridViewList_CellPainting);
            this.dataGridViewList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridViewList_KeyDown);
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonUpd,
            this.toolStripSeparator2,
            this.toolStripButtonRef,
            this.toolStripSeparator1});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(1400, 25);
            this.toolStripMenu.TabIndex = 0;
            this.toolStripMenu.Text = "Действия";
            // 
            // toolStripButtonUpd
            // 
            this.toolStripButtonUpd.Image = global::AcademicYearControlsAndForms.Properties.Resources.Upd;
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
            // toolStripButtonRef
            // 
            this.toolStripButtonRef.Image = global::AcademicYearControlsAndForms.Properties.Resources.Ref;
            this.toolStripButtonRef.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRef.Name = "toolStripButtonRef";
            this.toolStripButtonRef.Size = new System.Drawing.Size(81, 22);
            this.toolStripButtonRef.Text = "Обновить";
            this.toolStripButtonRef.Click += new System.EventHandler(this.ToolStripButtonRef_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.buttonCreateGrafics);
            this.panelTop.Controls.Add(this.buttonCreateNIRRecords);
            this.panelTop.Controls.Add(this.buttonCreatStatement);
            this.panelTop.Controls.Add(this.buttonCalcFactHours);
            this.panelTop.Controls.Add(this.comboBoxAcademicYear);
            this.panelTop.Controls.Add(this.labelSelectAcademicYear);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 25);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1400, 35);
            this.panelTop.TabIndex = 1;
            // 
            // buttonCreateGrafics
            // 
            this.buttonCreateGrafics.Location = new System.Drawing.Point(722, 5);
            this.buttonCreateGrafics.Name = "buttonCreateGrafics";
            this.buttonCreateGrafics.Size = new System.Drawing.Size(164, 23);
            this.buttonCreateGrafics.TabIndex = 6;
            this.buttonCreateGrafics.Text = "Создать расчасовки";
            this.buttonCreateGrafics.UseVisualStyleBackColor = true;
            this.buttonCreateGrafics.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonCreateNIRRecords
            // 
            this.buttonCreateNIRRecords.Location = new System.Drawing.Point(582, 5);
            this.buttonCreateNIRRecords.Name = "buttonCreateNIRRecords";
            this.buttonCreateNIRRecords.Size = new System.Drawing.Size(138, 23);
            this.buttonCreateNIRRecords.TabIndex = 5;
            this.buttonCreateNIRRecords.Text = "Создать записи НИР";
            this.buttonCreateNIRRecords.UseVisualStyleBackColor = true;
            this.buttonCreateNIRRecords.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonCreatStatement
            // 
            this.buttonCreatStatement.Location = new System.Drawing.Point(436, 6);
            this.buttonCreatStatement.Name = "buttonCreatStatement";
            this.buttonCreatStatement.Size = new System.Drawing.Size(140, 23);
            this.buttonCreatStatement.TabIndex = 4;
            this.buttonCreatStatement.Text = "Создать ведомости";
            this.buttonCreatStatement.UseVisualStyleBackColor = true;
            this.buttonCreatStatement.Click += new System.EventHandler(this.buttonCreatStatement_Click);
            // 
            // buttonCalcFactHours
            // 
            this.buttonCalcFactHours.Location = new System.Drawing.Point(328, 6);
            this.buttonCalcFactHours.Name = "buttonCalcFactHours";
            this.buttonCalcFactHours.Size = new System.Drawing.Size(102, 23);
            this.buttonCalcFactHours.TabIndex = 3;
            this.buttonCalcFactHours.Text = "Расчитать время";
            this.buttonCalcFactHours.UseVisualStyleBackColor = true;
            this.buttonCalcFactHours.Click += new System.EventHandler(this.ButtonCalcFactHours_Click);
            // 
            // comboBoxAcademicYear
            // 
            this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicYear.FormattingEnabled = true;
            this.comboBoxAcademicYear.Location = new System.Drawing.Point(134, 6);
            this.comboBoxAcademicYear.Name = "comboBoxAcademicYear";
            this.comboBoxAcademicYear.Size = new System.Drawing.Size(188, 21);
            this.comboBoxAcademicYear.TabIndex = 2;
            this.comboBoxAcademicYear.SelectedIndexChanged += new System.EventHandler(this.ComboBoxAcademicYear_SelectedIndexChanged);
            // 
            // labelSelectAcademicYear
            // 
            this.labelSelectAcademicYear.AutoSize = true;
            this.labelSelectAcademicYear.Location = new System.Drawing.Point(9, 9);
            this.labelSelectAcademicYear.Name = "labelSelectAcademicYear";
            this.labelSelectAcademicYear.Size = new System.Drawing.Size(119, 13);
            this.labelSelectAcademicYear.TabIndex = 0;
            this.labelSelectAcademicYear.Text = "Выбрать учебный год:";
            // 
            // panelConfig
            // 
            this.panelConfig.Controls.Add(this.checkBoxSelectAll);
            this.panelConfig.Controls.Add(this.checkBoxLecturerLoad);
            this.panelConfig.Controls.Add(this.buttonPanelConfig);
            this.panelConfig.Controls.Add(this.checkBoxTimeNorm);
            this.panelConfig.Controls.Add(this.dataGridViewColumns);
            this.panelConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelConfig.Location = new System.Drawing.Point(0, 60);
            this.panelConfig.Name = "panelConfig";
            this.panelConfig.Size = new System.Drawing.Size(1400, 30);
            this.panelConfig.TabIndex = 3;
            // 
            // checkBoxSelectAll
            // 
            this.checkBoxSelectAll.AutoSize = true;
            this.checkBoxSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxSelectAll.Checked = true;
            this.checkBoxSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSelectAll.Location = new System.Drawing.Point(280, 6);
            this.checkBoxSelectAll.Name = "checkBoxSelectAll";
            this.checkBoxSelectAll.Size = new System.Drawing.Size(91, 17);
            this.checkBoxSelectAll.TabIndex = 6;
            this.checkBoxSelectAll.Text = "Выбрать все";
            this.checkBoxSelectAll.UseVisualStyleBackColor = true;
            this.checkBoxSelectAll.Visible = false;
            this.checkBoxSelectAll.CheckedChanged += new System.EventHandler(this.СheckBoxSelectAll_CheckedChanged);
            // 
            // checkBoxLecturerLoad
            // 
            this.checkBoxLecturerLoad.AutoSize = true;
            this.checkBoxLecturerLoad.Location = new System.Drawing.Point(402, 6);
            this.checkBoxLecturerLoad.Name = "checkBoxLecturerLoad";
            this.checkBoxLecturerLoad.Size = new System.Drawing.Size(154, 17);
            this.checkBoxLecturerLoad.TabIndex = 5;
            this.checkBoxLecturerLoad.Text = "Нагрузка преподавателя";
            this.checkBoxLecturerLoad.UseVisualStyleBackColor = true;
            this.checkBoxLecturerLoad.CheckedChanged += new System.EventHandler(this.CheckBoxLecturerLoad_CheckedChanged);
            // 
            // buttonPanelConfig
            // 
            this.buttonPanelConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPanelConfig.BackgroundImage = global::AcademicYearControlsAndForms.Properties.Resources.Down;
            this.buttonPanelConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonPanelConfig.Location = new System.Drawing.Point(1322, 4);
            this.buttonPanelConfig.Name = "buttonPanelConfig";
            this.buttonPanelConfig.Size = new System.Drawing.Size(75, 23);
            this.buttonPanelConfig.TabIndex = 4;
            this.buttonPanelConfig.UseVisualStyleBackColor = true;
            this.buttonPanelConfig.Click += new System.EventHandler(this.ButtonPanelConfig_Click);
            // 
            // checkBoxTimeNorm
            // 
            this.checkBoxTimeNorm.AutoSize = true;
            this.checkBoxTimeNorm.Location = new System.Drawing.Point(19, 6);
            this.checkBoxTimeNorm.Name = "checkBoxTimeNorm";
            this.checkBoxTimeNorm.Size = new System.Drawing.Size(109, 17);
            this.checkBoxTimeNorm.TabIndex = 0;
            this.checkBoxTimeNorm.Text = "Нормы времени";
            this.checkBoxTimeNorm.UseVisualStyleBackColor = true;
            this.checkBoxTimeNorm.CheckedChanged += new System.EventHandler(this.CheckBoxTimeNorm_CheckedChanged);
            // 
            // dataGridViewColumns
            // 
            this.dataGridViewColumns.AllowUserToAddRows = false;
            this.dataGridViewColumns.AllowUserToDeleteRows = false;
            this.dataGridViewColumns.AllowUserToOrderColumns = true;
            this.dataGridViewColumns.AllowUserToResizeColumns = false;
            this.dataGridViewColumns.AllowUserToResizeRows = false;
            this.dataGridViewColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewColumns.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnName,
            this.ColumnNumberColumn,
            this.ColumnColumn,
            this.ColumnSelect});
            this.dataGridViewColumns.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewColumns.Location = new System.Drawing.Point(7, 29);
            this.dataGridViewColumns.Name = "dataGridViewColumns";
            this.dataGridViewColumns.RowHeadersVisible = false;
            this.dataGridViewColumns.Size = new System.Drawing.Size(380, 0);
            this.dataGridViewColumns.TabIndex = 2;
            // 
            // ColumnName
            // 
            this.ColumnName.HeaderText = "Column1";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.Visible = false;
            // 
            // ColumnNumberColumn
            // 
            this.ColumnNumberColumn.HeaderText = "ColumnNumberColumn";
            this.ColumnNumberColumn.Name = "ColumnNumberColumn";
            this.ColumnNumberColumn.Visible = false;
            // 
            // ColumnColumn
            // 
            this.ColumnColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnColumn.HeaderText = "Преподаватель";
            this.ColumnColumn.Name = "ColumnColumn";
            // 
            // ColumnSelect
            // 
            this.ColumnSelect.HeaderText = "Видимый";
            this.ColumnSelect.Name = "ColumnSelect";
            // 
            // ControlLoadDistribution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewList);
            this.Controls.Add(this.panelConfig);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "ControlLoadDistribution";
            this.Size = new System.Drawing.Size(1400, 500);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelConfig.ResumeLayout(false);
            this.panelConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewList;
		private System.Windows.Forms.ToolStrip toolStripMenu;
		private System.Windows.Forms.ToolStripButton toolStripButtonUpd;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolStripButtonRef;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelSelectAcademicYear;
        private System.Windows.Forms.ComboBox comboBoxAcademicYear;
        private System.Windows.Forms.Button buttonCalcFactHours;
        private System.Windows.Forms.Button buttonCreatStatement;
        private System.Windows.Forms.Button buttonCreateNIRRecords;
        private System.Windows.Forms.Button buttonCreateGrafics;
        private System.Windows.Forms.Panel panelConfig;
        private System.Windows.Forms.CheckBox checkBoxTimeNorm;
        private System.Windows.Forms.DataGridView dataGridViewColumns;
        private System.Windows.Forms.Button buttonPanelConfig;
        private System.Windows.Forms.CheckBox checkBoxLecturerLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSelect;
        private System.Windows.Forms.CheckBox checkBoxSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
