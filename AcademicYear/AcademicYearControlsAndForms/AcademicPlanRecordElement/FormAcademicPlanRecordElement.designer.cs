namespace AcademicYearControlsAndForms.AcademicPlanRecordElement
{
    partial class FormAcademicPlanRecordElement
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
            this.labelAcademicPlanRecord = new System.Windows.Forms.Label();
            this.comboBoxAcademicPlanRecord = new System.Windows.Forms.ComboBox();
            this.comboBoxTimeNorm = new System.Windows.Forms.ComboBox();
            this.labelTimeNorm = new System.Windows.Forms.Label();
            this.labelPlanHours = new System.Windows.Forms.Label();
            this.textBoxPlanHours = new System.Windows.Forms.TextBox();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxFactHours = new System.Windows.Forms.TextBox();
            this.labelFactHours = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelAcademicPlanRecord
            // 
            this.labelAcademicPlanRecord.AutoSize = true;
            this.labelAcademicPlanRecord.Location = new System.Drawing.Point(12, 9);
            this.labelAcademicPlanRecord.Name = "labelAcademicPlanRecord";
            this.labelAcademicPlanRecord.Size = new System.Drawing.Size(132, 13);
            this.labelAcademicPlanRecord.TabIndex = 0;
            this.labelAcademicPlanRecord.Text = "Запись учебного плана*:";
            // 
            // comboBoxAcademicPlanRecord
            // 
            this.comboBoxAcademicPlanRecord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicPlanRecord.Enabled = false;
            this.comboBoxAcademicPlanRecord.FormattingEnabled = true;
            this.comboBoxAcademicPlanRecord.Location = new System.Drawing.Point(150, 6);
            this.comboBoxAcademicPlanRecord.Name = "comboBoxAcademicPlanRecord";
            this.comboBoxAcademicPlanRecord.Size = new System.Drawing.Size(220, 21);
            this.comboBoxAcademicPlanRecord.TabIndex = 1;
            // 
            // comboBoxTimeNorm
            // 
            this.comboBoxTimeNorm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTimeNorm.FormattingEnabled = true;
            this.comboBoxTimeNorm.Location = new System.Drawing.Point(150, 33);
            this.comboBoxTimeNorm.Name = "comboBoxTimeNorm";
            this.comboBoxTimeNorm.Size = new System.Drawing.Size(220, 21);
            this.comboBoxTimeNorm.TabIndex = 3;
            // 
            // labelTimeNorm
            // 
            this.labelTimeNorm.AutoSize = true;
            this.labelTimeNorm.Location = new System.Drawing.Point(12, 36);
            this.labelTimeNorm.Name = "labelTimeNorm";
            this.labelTimeNorm.Size = new System.Drawing.Size(95, 13);
            this.labelTimeNorm.TabIndex = 2;
            this.labelTimeNorm.Text = "Норма времени*:";
            // 
            // labelPlanHours
            // 
            this.labelPlanHours.AutoSize = true;
            this.labelPlanHours.Location = new System.Drawing.Point(12, 63);
            this.labelPlanHours.Name = "labelPlanHours";
            this.labelPlanHours.Size = new System.Drawing.Size(71, 13);
            this.labelPlanHours.TabIndex = 4;
            this.labelPlanHours.Text = "План. часы*:";
            // 
            // textBoxPlanHours
            // 
            this.textBoxPlanHours.Location = new System.Drawing.Point(89, 60);
            this.textBoxPlanHours.Name = "textBoxPlanHours";
            this.textBoxPlanHours.Size = new System.Drawing.Size(80, 20);
            this.textBoxPlanHours.TabIndex = 5;
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(123, 96);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 9;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(270, 96);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 10;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(42, 96);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxFactHours
            // 
            this.textBoxFactHours.Location = new System.Drawing.Point(290, 60);
            this.textBoxFactHours.Name = "textBoxFactHours";
            this.textBoxFactHours.Size = new System.Drawing.Size(80, 20);
            this.textBoxFactHours.TabIndex = 7;
            // 
            // labelFactHours
            // 
            this.labelFactHours.AutoSize = true;
            this.labelFactHours.Location = new System.Drawing.Point(213, 63);
            this.labelFactHours.Name = "labelFactHours";
            this.labelFactHours.Size = new System.Drawing.Size(73, 13);
            this.labelFactHours.TabIndex = 6;
            this.labelFactHours.Text = "Факт. часы*:";
            // 
            // AcademicPlanRecordElementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 131);
            this.Controls.Add(this.textBoxFactHours);
            this.Controls.Add(this.labelFactHours);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxPlanHours);
            this.Controls.Add(this.labelPlanHours);
            this.Controls.Add(this.comboBoxTimeNorm);
            this.Controls.Add(this.labelTimeNorm);
            this.Controls.Add(this.comboBoxAcademicPlanRecord);
            this.Controls.Add(this.labelAcademicPlanRecord);
            this.Name = "AcademicPlanRecordElementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Нагрузка по виду нагрузок";
            this.Load += new System.EventHandler(this.FormAcademicPlanRecordElement_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAcademicPlanRecord;
        private System.Windows.Forms.ComboBox comboBoxAcademicPlanRecord;
        private System.Windows.Forms.ComboBox comboBoxTimeNorm;
        private System.Windows.Forms.Label labelTimeNorm;
        private System.Windows.Forms.Label labelPlanHours;
        private System.Windows.Forms.TextBox textBoxPlanHours;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxFactHours;
        private System.Windows.Forms.Label labelFactHours;
    }
}