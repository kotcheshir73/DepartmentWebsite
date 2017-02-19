namespace DepartmentDesktop.Views.Services.Schedule
{
    partial class ScheduleStopWordForm
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
            this.labelStopWordType = new System.Windows.Forms.Label();
            this.comboBoxStopWordType = new System.Windows.Forms.ComboBox();
            this.labelStopWord = new System.Windows.Forms.Label();
            this.textBoxStopWord = new System.Windows.Forms.TextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelStopWordType
            // 
            this.labelStopWordType.AutoSize = true;
            this.labelStopWordType.Location = new System.Drawing.Point(12, 9);
            this.labelStopWordType.Name = "labelStopWordType";
            this.labelStopWordType.Size = new System.Drawing.Size(92, 13);
            this.labelStopWordType.TabIndex = 0;
            this.labelStopWordType.Text = "Тип стоп-слова*:";
            // 
            // comboBoxStopWordType
            // 
            this.comboBoxStopWordType.FormattingEnabled = true;
            this.comboBoxStopWordType.Location = new System.Drawing.Point(110, 6);
            this.comboBoxStopWordType.Name = "comboBoxStopWordType";
            this.comboBoxStopWordType.Size = new System.Drawing.Size(150, 21);
            this.comboBoxStopWordType.TabIndex = 1;
            // 
            // labelStopWord
            // 
            this.labelStopWord.AutoSize = true;
            this.labelStopWord.Location = new System.Drawing.Point(12, 36);
            this.labelStopWord.Name = "labelStopWord";
            this.labelStopWord.Size = new System.Drawing.Size(71, 13);
            this.labelStopWord.TabIndex = 2;
            this.labelStopWord.Text = "Стоп-слово*:";
            // 
            // textBoxStopWord
            // 
            this.textBoxStopWord.Location = new System.Drawing.Point(110, 33);
            this.textBoxStopWord.Name = "textBoxStopWord";
            this.textBoxStopWord.Size = new System.Drawing.Size(150, 20);
            this.textBoxStopWord.TabIndex = 3;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(185, 59);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(104, 59);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // ScheduleStopWordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 91);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxStopWord);
            this.Controls.Add(this.labelStopWord);
            this.Controls.Add(this.comboBoxStopWordType);
            this.Controls.Add(this.labelStopWordType);
            this.Name = "ScheduleStopWordForm";
            this.Text = "Стоп-слово";
            this.Load += new System.EventHandler(this.ScheduleStopWordForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStopWordType;
        private System.Windows.Forms.ComboBox comboBoxStopWordType;
        private System.Windows.Forms.Label labelStopWord;
        private System.Windows.Forms.TextBox textBoxStopWord;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
    }
}