namespace DepartmentDesktop.Views.EducationalProcess.AcademicPlan
{
	partial class TimeNormForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.comboBoxKindOfLoad = new System.Windows.Forms.ComboBox();
			this.labelKindOfLoad = new System.Windows.Forms.Label();
			this.labelTitle = new System.Windows.Forms.Label();
			this.textBoxTitle = new System.Windows.Forms.TextBox();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonSaveAndClose = new System.Windows.Forms.Button();
			this.labelFormula = new System.Windows.Forms.Label();
			this.textBoxFormula = new System.Windows.Forms.TextBox();
			this.labelSelectKindOfLoad = new System.Windows.Forms.Label();
			this.comboBoxSelectKindOfLoad = new System.Windows.Forms.ComboBox();
			this.labelSelectKindOfLoadType = new System.Windows.Forms.Label();
			this.comboBoxSelectKindOfLoadType = new System.Windows.Forms.ComboBox();
			this.labelHours = new System.Windows.Forms.Label();
			this.textBoxHours = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// comboBoxKindOfLoad
			// 
			this.comboBoxKindOfLoad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxKindOfLoad.FormattingEnabled = true;
			this.comboBoxKindOfLoad.Location = new System.Drawing.Point(176, 6);
			this.comboBoxKindOfLoad.Name = "comboBoxKindOfLoad";
			this.comboBoxKindOfLoad.Size = new System.Drawing.Size(220, 21);
			this.comboBoxKindOfLoad.TabIndex = 1;
			// 
			// labelKindOfLoad
			// 
			this.labelKindOfLoad.AutoSize = true;
			this.labelKindOfLoad.Location = new System.Drawing.Point(19, 9);
			this.labelKindOfLoad.Name = "labelKindOfLoad";
			this.labelKindOfLoad.Size = new System.Drawing.Size(153, 13);
			this.labelKindOfLoad.TabIndex = 0;
			this.labelKindOfLoad.Text = "Привязать к виду нагрузки*:";
			// 
			// labelTitle
			// 
			this.labelTitle.AutoSize = true;
			this.labelTitle.Location = new System.Drawing.Point(108, 36);
			this.labelTitle.Name = "labelTitle";
			this.labelTitle.Size = new System.Drawing.Size(64, 13);
			this.labelTitle.TabIndex = 2;
			this.labelTitle.Text = "Название*:";
			// 
			// textBoxTitle
			// 
			this.textBoxTitle.Location = new System.Drawing.Point(176, 33);
			this.textBoxTitle.Name = "textBoxTitle";
			this.textBoxTitle.Size = new System.Drawing.Size(220, 20);
			this.textBoxTitle.TabIndex = 3;
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(311, 177);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 14;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(83, 177);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 12;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonSaveAndClose
			// 
			this.buttonSaveAndClose.Location = new System.Drawing.Point(164, 177);
			this.buttonSaveAndClose.Name = "buttonSaveAndClose";
			this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
			this.buttonSaveAndClose.TabIndex = 13;
			this.buttonSaveAndClose.Text = "Сохранить и закрыть";
			this.buttonSaveAndClose.UseVisualStyleBackColor = true;
			this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
			// 
			// labelFormula
			// 
			this.labelFormula.AutoSize = true;
			this.labelFormula.Location = new System.Drawing.Point(108, 62);
			this.labelFormula.Name = "labelFormula";
			this.labelFormula.Size = new System.Drawing.Size(62, 13);
			this.labelFormula.TabIndex = 4;
			this.labelFormula.Text = "Формула*:";
			// 
			// textBoxFormula
			// 
			this.textBoxFormula.Enabled = false;
			this.textBoxFormula.Location = new System.Drawing.Point(176, 59);
			this.textBoxFormula.Name = "textBoxFormula";
			this.textBoxFormula.Size = new System.Drawing.Size(220, 20);
			this.textBoxFormula.TabIndex = 5;
			// 
			// labelSelectKindOfLoad
			// 
			this.labelSelectKindOfLoad.AutoSize = true;
			this.labelSelectKindOfLoad.Location = new System.Drawing.Point(12, 88);
			this.labelSelectKindOfLoad.Name = "labelSelectKindOfLoad";
			this.labelSelectKindOfLoad.Size = new System.Drawing.Size(158, 13);
			this.labelSelectKindOfLoad.TabIndex = 6;
			this.labelSelectKindOfLoad.Text = "Выбрать нагрузку в формулу:";
			// 
			// comboBoxSelectKindOfLoad
			// 
			this.comboBoxSelectKindOfLoad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSelectKindOfLoad.FormattingEnabled = true;
			this.comboBoxSelectKindOfLoad.Location = new System.Drawing.Point(176, 85);
			this.comboBoxSelectKindOfLoad.Name = "comboBoxSelectKindOfLoad";
			this.comboBoxSelectKindOfLoad.Size = new System.Drawing.Size(220, 21);
			this.comboBoxSelectKindOfLoad.TabIndex = 7;
			this.comboBoxSelectKindOfLoad.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectKindOfLoad_SelectedIndexChanged);
			// 
			// labelSelectKindOfLoadType
			// 
			this.labelSelectKindOfLoadType.AutoSize = true;
			this.labelSelectKindOfLoadType.Location = new System.Drawing.Point(47, 141);
			this.labelSelectKindOfLoadType.Name = "labelSelectKindOfLoadType";
			this.labelSelectKindOfLoadType.Size = new System.Drawing.Size(123, 13);
			this.labelSelectKindOfLoadType.TabIndex = 10;
			this.labelSelectKindOfLoadType.Text = "Выбрать тип нагрузки:";
			// 
			// comboBoxSelectKindOfLoadType
			// 
			this.comboBoxSelectKindOfLoadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSelectKindOfLoadType.FormattingEnabled = true;
			this.comboBoxSelectKindOfLoadType.Location = new System.Drawing.Point(176, 138);
			this.comboBoxSelectKindOfLoadType.Name = "comboBoxSelectKindOfLoadType";
			this.comboBoxSelectKindOfLoadType.Size = new System.Drawing.Size(220, 21);
			this.comboBoxSelectKindOfLoadType.TabIndex = 11;
			this.comboBoxSelectKindOfLoadType.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectKindOfLoadType_SelectedIndexChanged);
			// 
			// labelHours
			// 
			this.labelHours.AutoSize = true;
			this.labelHours.Location = new System.Drawing.Point(128, 115);
			this.labelHours.Name = "labelHours";
			this.labelHours.Size = new System.Drawing.Size(42, 13);
			this.labelHours.TabIndex = 8;
			this.labelHours.Text = "Часы*:";
			// 
			// textBoxHours
			// 
			this.textBoxHours.Location = new System.Drawing.Point(176, 112);
			this.textBoxHours.Name = "textBoxHours";
			this.textBoxHours.Size = new System.Drawing.Size(220, 20);
			this.textBoxHours.TabIndex = 9;
			this.textBoxHours.Leave += new System.EventHandler(this.textBoxHours_Leave);
			// 
			// TimeNormForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(408, 215);
			this.Controls.Add(this.textBoxHours);
			this.Controls.Add(this.labelHours);
			this.Controls.Add(this.comboBoxSelectKindOfLoadType);
			this.Controls.Add(this.labelSelectKindOfLoadType);
			this.Controls.Add(this.comboBoxSelectKindOfLoad);
			this.Controls.Add(this.labelSelectKindOfLoad);
			this.Controls.Add(this.textBoxFormula);
			this.Controls.Add(this.labelFormula);
			this.Controls.Add(this.buttonSaveAndClose);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.textBoxTitle);
			this.Controls.Add(this.labelTitle);
			this.Controls.Add(this.comboBoxKindOfLoad);
			this.Controls.Add(this.labelKindOfLoad);
			this.Name = "TimeNormForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Норма времени";
			this.Load += new System.EventHandler(this.TimeNormForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxKindOfLoad;
		private System.Windows.Forms.Label labelKindOfLoad;
		private System.Windows.Forms.Label labelTitle;
		private System.Windows.Forms.TextBox textBoxTitle;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonSaveAndClose;
		private System.Windows.Forms.Label labelFormula;
		private System.Windows.Forms.TextBox textBoxFormula;
		private System.Windows.Forms.Label labelSelectKindOfLoad;
		private System.Windows.Forms.ComboBox comboBoxSelectKindOfLoad;
		private System.Windows.Forms.Label labelSelectKindOfLoadType;
		private System.Windows.Forms.ComboBox comboBoxSelectKindOfLoadType;
		private System.Windows.Forms.Label labelHours;
		private System.Windows.Forms.TextBox textBoxHours;
	}
}