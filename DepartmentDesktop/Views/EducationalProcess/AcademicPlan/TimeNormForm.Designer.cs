﻿namespace DepartmentDesktop.Views.EducationalProcess.AcademicPlan
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
			this.labelParentTimeNorm = new System.Windows.Forms.Label();
			this.comboBoxTimeNorm = new System.Windows.Forms.ComboBox();
			this.textBoxHours = new System.Windows.Forms.TextBox();
			this.labelHours = new System.Windows.Forms.Label();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// comboBoxKindOfLoad
			// 
			this.comboBoxKindOfLoad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxKindOfLoad.FormattingEnabled = true;
			this.comboBoxKindOfLoad.Location = new System.Drawing.Point(104, 6);
			this.comboBoxKindOfLoad.Name = "comboBoxKindOfLoad";
			this.comboBoxKindOfLoad.Size = new System.Drawing.Size(200, 21);
			this.comboBoxKindOfLoad.TabIndex = 1;
			// 
			// labelKindOfLoad
			// 
			this.labelKindOfLoad.AutoSize = true;
			this.labelKindOfLoad.Location = new System.Drawing.Point(12, 9);
			this.labelKindOfLoad.Name = "labelKindOfLoad";
			this.labelKindOfLoad.Size = new System.Drawing.Size(82, 13);
			this.labelKindOfLoad.TabIndex = 0;
			this.labelKindOfLoad.Text = "Вид нагрузки*:";
			// 
			// labelTitle
			// 
			this.labelTitle.AutoSize = true;
			this.labelTitle.Location = new System.Drawing.Point(12, 36);
			this.labelTitle.Name = "labelTitle";
			this.labelTitle.Size = new System.Drawing.Size(64, 13);
			this.labelTitle.TabIndex = 2;
			this.labelTitle.Text = "Название*:";
			// 
			// textBoxTitle
			// 
			this.textBoxTitle.Location = new System.Drawing.Point(104, 33);
			this.textBoxTitle.Name = "textBoxTitle";
			this.textBoxTitle.Size = new System.Drawing.Size(200, 20);
			this.textBoxTitle.TabIndex = 3;
			// 
			// labelParentTimeNorm
			// 
			this.labelParentTimeNorm.AutoSize = true;
			this.labelParentTimeNorm.Location = new System.Drawing.Point(12, 62);
			this.labelParentTimeNorm.Name = "labelParentTimeNorm";
			this.labelParentTimeNorm.Size = new System.Drawing.Size(74, 13);
			this.labelParentTimeNorm.TabIndex = 4;
			this.labelParentTimeNorm.Text = "Привязать к:";
			// 
			// comboBoxTimeNorm
			// 
			this.comboBoxTimeNorm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxTimeNorm.FormattingEnabled = true;
			this.comboBoxTimeNorm.Location = new System.Drawing.Point(104, 59);
			this.comboBoxTimeNorm.Name = "comboBoxTimeNorm";
			this.comboBoxTimeNorm.Size = new System.Drawing.Size(200, 21);
			this.comboBoxTimeNorm.TabIndex = 5;
			// 
			// textBoxHours
			// 
			this.textBoxHours.Location = new System.Drawing.Point(104, 86);
			this.textBoxHours.Name = "textBoxHours";
			this.textBoxHours.Size = new System.Drawing.Size(200, 20);
			this.textBoxHours.TabIndex = 7;
			// 
			// labelHours
			// 
			this.labelHours.AutoSize = true;
			this.labelHours.Location = new System.Drawing.Point(12, 89);
			this.labelHours.Name = "labelHours";
			this.labelHours.Size = new System.Drawing.Size(42, 13);
			this.labelHours.TabIndex = 6;
			this.labelHours.Text = "Часы*:";
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(229, 112);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 13;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(148, 112);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 12;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// TimeNormForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(314, 142);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.textBoxHours);
			this.Controls.Add(this.labelHours);
			this.Controls.Add(this.comboBoxTimeNorm);
			this.Controls.Add(this.labelParentTimeNorm);
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
		private System.Windows.Forms.Label labelParentTimeNorm;
		private System.Windows.Forms.ComboBox comboBoxTimeNorm;
		private System.Windows.Forms.TextBox textBoxHours;
		private System.Windows.Forms.Label labelHours;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
	}
}