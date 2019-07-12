namespace LearningProgressControlsAndForms.DisciplineLessonTaskVariant
{
    partial class FormDisciplineLessonTaskVariant
    {
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
            this.textBoxVariantTask = new System.Windows.Forms.TextBox();
            this.textBoxVariantNumber = new System.Windows.Forms.TextBox();
            this.labelVariantTask = new System.Windows.Forms.Label();
            this.labelVariantNumber = new System.Windows.Forms.Label();
            this.textBoxOrder = new System.Windows.Forms.TextBox();
            this.labelOrder = new System.Windows.Forms.Label();
            this.comboBoxDisciplineLessonTask = new System.Windows.Forms.ComboBox();
            this.labelDisciplineLessonTask = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.comboBoxDisciplineLessonTask);
            this.panelMain.Controls.Add(this.labelDisciplineLessonTask);
            this.panelMain.Controls.Add(this.labelVariantNumber);
            this.panelMain.Controls.Add(this.textBoxOrder);
            this.panelMain.Controls.Add(this.labelVariantTask);
            this.panelMain.Controls.Add(this.labelOrder);
            this.panelMain.Controls.Add(this.textBoxVariantNumber);
            this.panelMain.Controls.Add(this.textBoxVariantTask);
            this.panelMain.Size = new System.Drawing.Size(374, 234);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(374, 36);
            // 
            // textBoxVariantTask
            // 
            this.textBoxVariantTask.Location = new System.Drawing.Point(140, 60);
            this.textBoxVariantTask.Multiline = true;
            this.textBoxVariantTask.Name = "textBoxVariantTask";
            this.textBoxVariantTask.Size = new System.Drawing.Size(210, 140);
            this.textBoxVariantTask.TabIndex = 5;
            // 
            // textBoxVariantNumber
            // 
            this.textBoxVariantNumber.Location = new System.Drawing.Point(140, 34);
            this.textBoxVariantNumber.Name = "textBoxVariantNumber";
            this.textBoxVariantNumber.Size = new System.Drawing.Size(210, 20);
            this.textBoxVariantNumber.TabIndex = 3;
            // 
            // labelVariantTask
            // 
            this.labelVariantTask.AutoSize = true;
            this.labelVariantTask.Location = new System.Drawing.Point(12, 63);
            this.labelVariantTask.Name = "labelVariantTask";
            this.labelVariantTask.Size = new System.Drawing.Size(105, 13);
            this.labelVariantTask.TabIndex = 4;
            this.labelVariantTask.Text = "Описание задания:";
            // 
            // labelVariantNumber
            // 
            this.labelVariantNumber.AutoSize = true;
            this.labelVariantNumber.Location = new System.Drawing.Point(12, 37);
            this.labelVariantNumber.Name = "labelVariantNumber";
            this.labelVariantNumber.Size = new System.Drawing.Size(98, 13);
            this.labelVariantNumber.TabIndex = 2;
            this.labelVariantNumber.Text = "Номер варианта*:";
            // 
            // textBoxOrder
            // 
            this.textBoxOrder.Location = new System.Drawing.Point(140, 206);
            this.textBoxOrder.Name = "textBoxOrder";
            this.textBoxOrder.Size = new System.Drawing.Size(210, 20);
            this.textBoxOrder.TabIndex = 7;
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.Location = new System.Drawing.Point(21, 209);
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
            this.comboBoxDisciplineLessonTask.Location = new System.Drawing.Point(140, 7);
            this.comboBoxDisciplineLessonTask.Name = "comboBoxDisciplineLessonTask";
            this.comboBoxDisciplineLessonTask.Size = new System.Drawing.Size(210, 21);
            this.comboBoxDisciplineLessonTask.TabIndex = 1;
            // 
            // labelDisciplineLessonTask
            // 
            this.labelDisciplineLessonTask.AutoSize = true;
            this.labelDisciplineLessonTask.Location = new System.Drawing.Point(12, 10);
            this.labelDisciplineLessonTask.Name = "labelDisciplineLessonTask";
            this.labelDisciplineLessonTask.Size = new System.Drawing.Size(57, 13);
            this.labelDisciplineLessonTask.TabIndex = 0;
            this.labelDisciplineLessonTask.Text = "Задание*:";
            // 
            // FormDisciplineLessonTaskVariant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 270);
            this.Name = "FormDisciplineLessonTaskVariant";
            this.Text = "Задание по варианту";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
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