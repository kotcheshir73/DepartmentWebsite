namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson.DisciplineLessonTask.DisciplineLessonTaskVariant
{
    partial class FormDisciplineLessonTaskVariantsForm
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
            this.labelCountVariants = new System.Windows.Forms.Label();
            this.textBoxCountVariants = new System.Windows.Forms.TextBox();
            this.buttonForm = new System.Windows.Forms.Button();
            this.labelVariantNumberTemplate = new System.Windows.Forms.Label();
            this.textBoxVariantNumberTemplate = new System.Windows.Forms.TextBox();
            this.panelVariants = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // labelCountVariants
            // 
            this.labelCountVariants.AutoSize = true;
            this.labelCountVariants.Location = new System.Drawing.Point(12, 35);
            this.labelCountVariants.Name = "labelCountVariants";
            this.labelCountVariants.Size = new System.Drawing.Size(129, 13);
            this.labelCountVariants.TabIndex = 2;
            this.labelCountVariants.Text = "Количество вариантов*:";
            // 
            // textBoxCountVariants
            // 
            this.textBoxCountVariants.Location = new System.Drawing.Point(177, 32);
            this.textBoxCountVariants.Name = "textBoxCountVariants";
            this.textBoxCountVariants.Size = new System.Drawing.Size(210, 20);
            this.textBoxCountVariants.TabIndex = 3;
            this.textBoxCountVariants.TextChanged += new System.EventHandler(this.textBoxCountVariants_TextChanged);
            // 
            // buttonForm
            // 
            this.buttonForm.Location = new System.Drawing.Point(115, 426);
            this.buttonForm.Name = "buttonForm";
            this.buttonForm.Size = new System.Drawing.Size(141, 23);
            this.buttonForm.TabIndex = 5;
            this.buttonForm.Text = "Сформировать";
            this.buttonForm.UseVisualStyleBackColor = true;
            this.buttonForm.Click += new System.EventHandler(this.buttonForm_Click);
            // 
            // labelVariantNumberTemplate
            // 
            this.labelVariantNumberTemplate.AutoSize = true;
            this.labelVariantNumberTemplate.Location = new System.Drawing.Point(12, 9);
            this.labelVariantNumberTemplate.Name = "labelVariantNumberTemplate";
            this.labelVariantNumberTemplate.Size = new System.Drawing.Size(159, 13);
            this.labelVariantNumberTemplate.TabIndex = 0;
            this.labelVariantNumberTemplate.Text = "Шаблон заголовка варианта*:";
            // 
            // textBoxVariantNumberTemplate
            // 
            this.textBoxVariantNumberTemplate.Location = new System.Drawing.Point(177, 6);
            this.textBoxVariantNumberTemplate.Name = "textBoxVariantNumberTemplate";
            this.textBoxVariantNumberTemplate.Size = new System.Drawing.Size(210, 20);
            this.textBoxVariantNumberTemplate.TabIndex = 1;
            this.textBoxVariantNumberTemplate.Text = "[N]";
            // 
            // panelVariants
            // 
            this.panelVariants.AutoScroll = true;
            this.panelVariants.Location = new System.Drawing.Point(12, 58);
            this.panelVariants.Name = "panelVariants";
            this.panelVariants.Size = new System.Drawing.Size(375, 362);
            this.panelVariants.TabIndex = 4;
            // 
            // FormDisciplineLessonTaskVariantsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 461);
            this.Controls.Add(this.panelVariants);
            this.Controls.Add(this.labelCountVariants);
            this.Controls.Add(this.textBoxCountVariants);
            this.Controls.Add(this.buttonForm);
            this.Controls.Add(this.labelVariantNumberTemplate);
            this.Controls.Add(this.textBoxVariantNumberTemplate);
            this.Name = "FormDisciplineLessonTaskVariantsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Формирование вариантов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCountVariants;
        private System.Windows.Forms.TextBox textBoxCountVariants;
        private System.Windows.Forms.Button buttonForm;
        private System.Windows.Forms.Label labelVariantNumberTemplate;
        private System.Windows.Forms.TextBox textBoxVariantNumberTemplate;
        private System.Windows.Forms.Panel panelVariants;
    }
}