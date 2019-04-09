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
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonUpd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRef = new System.Windows.Forms.ToolStripButton();
            this.panelTop = new System.Windows.Forms.Panel();
            this.buttonCreateGrafics = new System.Windows.Forms.Button();
            this.buttonCreateNIRRecords = new System.Windows.Forms.Button();
            this.buttonCreatStatement = new System.Windows.Forms.Button();
            this.buttonCalcFactHours = new System.Windows.Forms.Button();
            this.comboBoxAcademicYear = new System.Windows.Forms.ComboBox();
            this.labelSelectAcademicYear = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
            this.toolStripMenu.SuspendLayout();
            this.panelTop.SuspendLayout();
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
            this.dataGridViewList.Location = new System.Drawing.Point(0, 60);
            this.dataGridViewList.MultiSelect = false;
            this.dataGridViewList.Name = "dataGridViewList";
            this.dataGridViewList.ReadOnly = true;
            this.dataGridViewList.RowHeadersVisible = false;
            this.dataGridViewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewList.Size = new System.Drawing.Size(899, 440);
            this.dataGridViewList.TabIndex = 2;
            this.dataGridViewList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellDoubleClick);
            this.dataGridViewList.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewList_CellPainting);
            this.dataGridViewList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewList_KeyDown);
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
            this.toolStripMenu.Size = new System.Drawing.Size(899, 25);
            this.toolStripMenu.TabIndex = 0;
            this.toolStripMenu.Text = "Действия";
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.Image = global::AcademicYearControlsAndForms.Properties.Resources.Add;
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
            // toolStripButtonUpd
            // 
            this.toolStripButtonUpd.Image = global::AcademicYearControlsAndForms.Properties.Resources.Upd;
            this.toolStripButtonUpd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUpd.Name = "toolStripButtonUpd";
            this.toolStripButtonUpd.Size = new System.Drawing.Size(81, 22);
            this.toolStripButtonUpd.Text = "Изменить";
            this.toolStripButtonUpd.Click += new System.EventHandler(this.toolStripButtonUpd_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonDel
            // 
            this.toolStripButtonDel.Image = global::AcademicYearControlsAndForms.Properties.Resources.Del;
            this.toolStripButtonDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDel.Name = "toolStripButtonDel";
            this.toolStripButtonDel.Size = new System.Drawing.Size(71, 22);
            this.toolStripButtonDel.Text = "Удалить";
            this.toolStripButtonDel.Click += new System.EventHandler(this.toolStripButtonDel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonRef
            // 
            this.toolStripButtonRef.Image = global::AcademicYearControlsAndForms.Properties.Resources.Ref;
            this.toolStripButtonRef.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRef.Name = "toolStripButtonRef";
            this.toolStripButtonRef.Size = new System.Drawing.Size(81, 22);
            this.toolStripButtonRef.Text = "Обновить";
            this.toolStripButtonRef.Click += new System.EventHandler(this.toolStripButtonRef_Click);
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
            this.panelTop.Size = new System.Drawing.Size(899, 35);
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
            this.buttonCreatStatement.Text = "СОздать ведомости";
            this.buttonCreatStatement.UseVisualStyleBackColor = true;
            this.buttonCreatStatement.Click += new System.EventHandler(this.buttonCreatStatement_Click);
            // 
            // buttonCalcFactHours
            // 
            this.buttonCalcFactHours.Location = new System.Drawing.Point(328, 6);
            this.buttonCalcFactHours.Name = "buttonCalcFactHours";
            this.buttonCalcFactHours.Size = new System.Drawing.Size(102, 21);
            this.buttonCalcFactHours.TabIndex = 3;
            this.buttonCalcFactHours.Text = "Расчитать время";
            this.buttonCalcFactHours.UseVisualStyleBackColor = true;
            this.buttonCalcFactHours.Click += new System.EventHandler(this.buttonCalcFactHours_Click);
            // 
            // comboBoxAcademicYear
            // 
            this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicYear.FormattingEnabled = true;
            this.comboBoxAcademicYear.Location = new System.Drawing.Point(134, 6);
            this.comboBoxAcademicYear.Name = "comboBoxAcademicYear";
            this.comboBoxAcademicYear.Size = new System.Drawing.Size(188, 21);
            this.comboBoxAcademicYear.TabIndex = 2;
            this.comboBoxAcademicYear.SelectedIndexChanged += new System.EventHandler(this.comboBoxAcademicYear_SelectedIndexChanged);
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
            // LoadDistributionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewList);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "LoadDistributionControl";
            this.Size = new System.Drawing.Size(899, 500);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewList;
		private System.Windows.Forms.ToolStrip toolStripMenu;
		private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolStripButtonUpd;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolStripButtonDel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton toolStripButtonRef;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelSelectAcademicYear;
        private System.Windows.Forms.ComboBox comboBoxAcademicYear;
        private System.Windows.Forms.Button buttonCalcFactHours;
        private System.Windows.Forms.Button buttonCreatStatement;
        private System.Windows.Forms.Button buttonCreateNIRRecords;
        private System.Windows.Forms.Button buttonCreateGrafics;
    }
}
