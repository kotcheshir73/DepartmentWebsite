namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson.DisciplineLessonTask.DisciplineLessonTaskVariant
{
    partial class DisciplineLessonTaskVariantForm
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
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxVariantTask = new System.Windows.Forms.TextBox();
            this.textBoxVariantNumber = new System.Windows.Forms.TextBox();
            this.labelVariantTask = new System.Windows.Forms.Label();
            this.labelVariantNumber = new System.Windows.Forms.Label();
            this.textBoxOrder = new System.Windows.Forms.TextBox();
            this.labelOrder = new System.Windows.Forms.Label();
            this.comboBoxDisciplineLessonTask = new System.Windows.Forms.ComboBox();
            this.labelDisciplineLessonTask = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(123, 238);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 9;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(270, 238);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 10;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(42, 238);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxVariantTask
            // 
            this.textBoxVariantTask.Location = new System.Drawing.Point(140, 59);
            this.textBoxVariantTask.Multiline = true;
            this.textBoxVariantTask.Name = "textBoxVariantTask";
            this.textBoxVariantTask.Size = new System.Drawing.Size(210, 140);
            this.textBoxVariantTask.TabIndex = 5;
            // 
            // textBoxVariantNumber
            // 
            this.textBoxVariantNumber.Location = new System.Drawing.Point(140, 33);
            this.textBoxVariantNumber.Name = "textBoxVariantNumber";
            this.textBoxVariantNumber.Size = new System.Drawing.Size(210, 20);
            this.textBoxVariantNumber.TabIndex = 3;
            // 
            // labelVariantTask
            // 
            this.labelVariantTask.AutoSize = true;
            this.labelVariantTask.Location = new System.Drawing.Point(12, 62);
            this.labelVariantTask.Name = "labelVariantTask";
            this.labelVariantTask.Size = new System.Drawing.Size(105, 13);
            this.labelVariantTask.TabIndex = 4;
            this.labelVariantTask.Text = "Описание задания:";
            // 
            // labelVariantNumber
            // 
            this.labelVariantNumber.AutoSize = true;
            this.labelVariantNumber.Location = new System.Drawing.Point(12, 36);
            this.labelVariantNumber.Name = "labelVariantNumber";
            this.labelVariantNumber.Size = new System.Drawing.Size(98, 13);
            this.labelVariantNumber.TabIndex = 2;
            this.labelVariantNumber.Text = "Номер варианта*:";
            // 
            // textBoxOrder
            // 
            this.textBoxOrder.Location = new System.Drawing.Point(140, 205);
            this.textBoxOrder.Name = "textBoxOrder";
            this.textBoxOrder.Size = new System.Drawing.Size(210, 20);
            this.textBoxOrder.TabIndex = 7;
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.Location = new System.Drawing.Point(21, 208);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(113, 13);
            this.labelOrder.TabIndex = 6;
            this.labelOrder.Text = "Порядковый номер*:";
            // 
            // comboBoxDisciplineLessonTask
            // 
            this.comboBoxDisciplineLessonTask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisciplineLessonTask.Enabled = false;
            this.comboBoxDisciplineLessonTask.FormattingEnabled = true;
            this.comboBoxDisciplineLessonTask.Location = new System.Drawing.Point(140, 6);
            this.comboBoxDisciplineLessonTask.Name = "comboBoxDisciplineLessonTask";
            this.comboBoxDisciplineLessonTask.Size = new System.Drawing.Size(210, 21);
            this.comboBoxDisciplineLessonTask.TabIndex = 1;
            // 
            // labelDisciplineLessonTask
            // 
            this.labelDisciplineLessonTask.AutoSize = true;
            this.labelDisciplineLessonTask.Location = new System.Drawing.Point(12, 9);
            this.labelDisciplineLessonTask.Name = "labelDisciplineLessonTask";
            this.labelDisciplineLessonTask.Size = new System.Drawing.Size(57, 13);
            this.labelDisciplineLessonTask.TabIndex = 0;
            this.labelDisciplineLessonTask.Text = "Задание*:";
            // 
            // DisciplineLessonTaskVariantForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 270);
            this.Controls.Add(this.comboBoxDisciplineLessonTask);
            this.Controls.Add(this.labelDisciplineLessonTask);
            this.Controls.Add(this.textBoxOrder);
            this.Controls.Add(this.labelOrder);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxVariantTask);
            this.Controls.Add(this.textBoxVariantNumber);
            this.Controls.Add(this.labelVariantTask);
            this.Controls.Add(this.labelVariantNumber);
            this.Name = "DisciplineLessonTaskVariantForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Задание по варианту";
            this.Load += new System.EventHandler(this.DisciplineLessonTaskVariantForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxVariantTask;
        private System.Windows.Forms.TextBox textBoxVariantNumber;
        private System.Windows.Forms.Label labelVariantTask;
        private System.Windows.Forms.Label labelVariantNumber;
        private System.Windows.Forms.TextBox textBoxOrder;
        private System.Windows.Forms.Label labelOrder;
        private System.Windows.Forms.ComboBox comboBoxDisciplineLessonTask;
        private System.Windows.Forms.Label labelDisciplineLessonTask;
    }
}