namespace DepartmentDesktop.Views.EducationalProcess.TimeNorm
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
            this.labelSelectKindOfLoad = new System.Windows.Forms.Label();
            this.comboBoxSelectKindOfLoad = new System.Windows.Forms.ComboBox();
            this.labelSelectKindOfLoadType = new System.Windows.Forms.Label();
            this.comboBoxSelectKindOfLoadType = new System.Windows.Forms.ComboBox();
            this.labelHours = new System.Windows.Forms.Label();
            this.textBoxHours = new System.Windows.Forms.TextBox();
            this.comboBoxAcademicYear = new System.Windows.Forms.ComboBox();
            this.labelAcademicYear = new System.Windows.Forms.Label();
            this.comboBoxTimeNormKoef = new System.Windows.Forms.ComboBox();
            this.labelTimeNormKoef = new System.Windows.Forms.Label();
            this.textBoxNumKoef = new System.Windows.Forms.TextBox();
            this.labelNumKoef = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxKindOfLoad
            // 
            this.comboBoxKindOfLoad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKindOfLoad.FormattingEnabled = true;
            this.comboBoxKindOfLoad.Location = new System.Drawing.Point(264, 51);
            this.comboBoxKindOfLoad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxKindOfLoad.Name = "comboBoxKindOfLoad";
            this.comboBoxKindOfLoad.Size = new System.Drawing.Size(328, 28);
            this.comboBoxKindOfLoad.TabIndex = 3;
            // 
            // labelKindOfLoad
            // 
            this.labelKindOfLoad.AutoSize = true;
            this.labelKindOfLoad.Location = new System.Drawing.Point(18, 55);
            this.labelKindOfLoad.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelKindOfLoad.Name = "labelKindOfLoad";
            this.labelKindOfLoad.Size = new System.Drawing.Size(224, 20);
            this.labelKindOfLoad.TabIndex = 2;
            this.labelKindOfLoad.Text = "Привязать к виду нагрузки*:";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(18, 97);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(93, 20);
            this.labelTitle.TabIndex = 4;
            this.labelTitle.Text = "Название*:";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(264, 92);
            this.textBoxTitle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(328, 26);
            this.textBoxTitle.TabIndex = 5;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(434, 342);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(112, 35);
            this.buttonClose.TabIndex = 16;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(92, 342);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(112, 35);
            this.buttonSave.TabIndex = 14;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(214, 342);
            this.buttonSaveAndClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(212, 35);
            this.buttonSaveAndClose.TabIndex = 15;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // labelSelectKindOfLoad
            // 
            this.labelSelectKindOfLoad.AutoSize = true;
            this.labelSelectKindOfLoad.Location = new System.Drawing.Point(18, 133);
            this.labelSelectKindOfLoad.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSelectKindOfLoad.Name = "labelSelectKindOfLoad";
            this.labelSelectKindOfLoad.Size = new System.Drawing.Size(233, 20);
            this.labelSelectKindOfLoad.TabIndex = 8;
            this.labelSelectKindOfLoad.Text = "Выбрать нагрузку в формулу:";
            // 
            // comboBoxSelectKindOfLoad
            // 
            this.comboBoxSelectKindOfLoad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectKindOfLoad.FormattingEnabled = true;
            this.comboBoxSelectKindOfLoad.Location = new System.Drawing.Point(264, 128);
            this.comboBoxSelectKindOfLoad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxSelectKindOfLoad.Name = "comboBoxSelectKindOfLoad";
            this.comboBoxSelectKindOfLoad.Size = new System.Drawing.Size(328, 28);
            this.comboBoxSelectKindOfLoad.TabIndex = 9;
            this.comboBoxSelectKindOfLoad.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectKindOfLoad_SelectedIndexChanged);
            // 
            // labelSelectKindOfLoadType
            // 
            this.labelSelectKindOfLoadType.AutoSize = true;
            this.labelSelectKindOfLoadType.Location = new System.Drawing.Point(18, 214);
            this.labelSelectKindOfLoadType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSelectKindOfLoadType.Name = "labelSelectKindOfLoadType";
            this.labelSelectKindOfLoadType.Size = new System.Drawing.Size(181, 20);
            this.labelSelectKindOfLoadType.TabIndex = 12;
            this.labelSelectKindOfLoadType.Text = "Выбрать тип нагрузки:";
            // 
            // comboBoxSelectKindOfLoadType
            // 
            this.comboBoxSelectKindOfLoadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectKindOfLoadType.FormattingEnabled = true;
            this.comboBoxSelectKindOfLoadType.Location = new System.Drawing.Point(264, 210);
            this.comboBoxSelectKindOfLoadType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxSelectKindOfLoadType.Name = "comboBoxSelectKindOfLoadType";
            this.comboBoxSelectKindOfLoadType.Size = new System.Drawing.Size(328, 28);
            this.comboBoxSelectKindOfLoadType.TabIndex = 13;
            this.comboBoxSelectKindOfLoadType.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectKindOfLoadType_SelectedIndexChanged);
            // 
            // labelHours
            // 
            this.labelHours.AutoSize = true;
            this.labelHours.Location = new System.Drawing.Point(18, 174);
            this.labelHours.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelHours.Name = "labelHours";
            this.labelHours.Size = new System.Drawing.Size(59, 20);
            this.labelHours.TabIndex = 10;
            this.labelHours.Text = "Часы*:";
            // 
            // textBoxHours
            // 
            this.textBoxHours.Location = new System.Drawing.Point(264, 170);
            this.textBoxHours.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxHours.Name = "textBoxHours";
            this.textBoxHours.Size = new System.Drawing.Size(328, 26);
            this.textBoxHours.TabIndex = 11;
            this.textBoxHours.Leave += new System.EventHandler(this.textBoxHours_Leave);
            // 
            // comboBoxAcademicYear
            // 
            this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicYear.Enabled = false;
            this.comboBoxAcademicYear.FormattingEnabled = true;
            this.comboBoxAcademicYear.Location = new System.Drawing.Point(264, 9);
            this.comboBoxAcademicYear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxAcademicYear.Name = "comboBoxAcademicYear";
            this.comboBoxAcademicYear.Size = new System.Drawing.Size(328, 28);
            this.comboBoxAcademicYear.TabIndex = 1;
            // 
            // labelAcademicYear
            // 
            this.labelAcademicYear.AutoSize = true;
            this.labelAcademicYear.Location = new System.Drawing.Point(18, 14);
            this.labelAcademicYear.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAcademicYear.Name = "labelAcademicYear";
            this.labelAcademicYear.Size = new System.Drawing.Size(116, 20);
            this.labelAcademicYear.TabIndex = 0;
            this.labelAcademicYear.Text = "Учебный год*:";
            // 
            // comboBoxTimeNormKoef
            // 
            this.comboBoxTimeNormKoef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTimeNormKoef.FormattingEnabled = true;
            this.comboBoxTimeNormKoef.Location = new System.Drawing.Point(264, 293);
            this.comboBoxTimeNormKoef.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxTimeNormKoef.Name = "comboBoxTimeNormKoef";
            this.comboBoxTimeNormKoef.Size = new System.Drawing.Size(328, 28);
            this.comboBoxTimeNormKoef.TabIndex = 29;
            // 
            // labelTimeNormKoef
            // 
            this.labelTimeNormKoef.AutoSize = true;
            this.labelTimeNormKoef.Location = new System.Drawing.Point(18, 293);
            this.labelTimeNormKoef.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTimeNormKoef.Name = "labelTimeNormKoef";
            this.labelTimeNormKoef.Size = new System.Drawing.Size(242, 20);
            this.labelTimeNormKoef.TabIndex = 28;
            this.labelTimeNormKoef.Text = "Коэффициент норм времени*:";
            // 
            // textBoxNumKoef
            // 
            this.textBoxNumKoef.Location = new System.Drawing.Point(264, 254);
            this.textBoxNumKoef.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxNumKoef.Name = "textBoxNumKoef";
            this.textBoxNumKoef.Size = new System.Drawing.Size(328, 26);
            this.textBoxNumKoef.TabIndex = 27;
            // 
            // labelNumKoef
            // 
            this.labelNumKoef.AutoSize = true;
            this.labelNumKoef.Location = new System.Drawing.Point(18, 257);
            this.labelNumKoef.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNumKoef.Name = "labelNumKoef";
            this.labelNumKoef.Size = new System.Drawing.Size(208, 20);
            this.labelNumKoef.TabIndex = 26;
            this.labelNumKoef.Text = "Числовой коэффициент*:";
            // 
            // TimeNormForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 391);
            this.Controls.Add(this.comboBoxTimeNormKoef);
            this.Controls.Add(this.labelTimeNormKoef);
            this.Controls.Add(this.textBoxNumKoef);
            this.Controls.Add(this.labelNumKoef);
            this.Controls.Add(this.comboBoxAcademicYear);
            this.Controls.Add(this.labelAcademicYear);
            this.Controls.Add(this.textBoxHours);
            this.Controls.Add(this.labelHours);
            this.Controls.Add(this.comboBoxSelectKindOfLoadType);
            this.Controls.Add(this.labelSelectKindOfLoadType);
            this.Controls.Add(this.comboBoxSelectKindOfLoad);
            this.Controls.Add(this.labelSelectKindOfLoad);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.comboBoxKindOfLoad);
            this.Controls.Add(this.labelKindOfLoad);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
		private System.Windows.Forms.Label labelSelectKindOfLoad;
		private System.Windows.Forms.ComboBox comboBoxSelectKindOfLoad;
		private System.Windows.Forms.Label labelSelectKindOfLoadType;
		private System.Windows.Forms.ComboBox comboBoxSelectKindOfLoadType;
		private System.Windows.Forms.Label labelHours;
		private System.Windows.Forms.TextBox textBoxHours;
        private System.Windows.Forms.ComboBox comboBoxAcademicYear;
        private System.Windows.Forms.Label labelAcademicYear;
        private System.Windows.Forms.ComboBox comboBoxTimeNormKoef;
        private System.Windows.Forms.Label labelTimeNormKoef;
        private System.Windows.Forms.TextBox textBoxNumKoef;
        private System.Windows.Forms.Label labelNumKoef;
    }
}