namespace AcademicYearControlsAndForms.DisciplineTimeDistributionClassroom
{
    partial class FormDisciplineTimeDistributionClassroom
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
            this.labelDisciplineTimeDistribution = new System.Windows.Forms.Label();
            this.comboBoxDisciplineTimeDistribution = new System.Windows.Forms.ComboBox();
            this.labelTimeNorm = new System.Windows.Forms.Label();
            this.comboBoxTimeNorm = new System.Windows.Forms.ComboBox();
            this.labelClassroomDescription = new System.Windows.Forms.Label();
            this.textBoxClassroomDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(105, 96);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(252, 96);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(24, 96);
            // 
            // labelDisciplineTimeDistribution
            // 
            this.labelDisciplineTimeDistribution.AutoSize = true;
            this.labelDisciplineTimeDistribution.Location = new System.Drawing.Point(12, 9);
            this.labelDisciplineTimeDistribution.Name = "labelDisciplineTimeDistribution";
            this.labelDisciplineTimeDistribution.Size = new System.Drawing.Size(74, 13);
            this.labelDisciplineTimeDistribution.TabIndex = 0;
            this.labelDisciplineTimeDistribution.Text = "Расчасовка*:";
            // 
            // comboBoxDisciplineTimeDistribution
            // 
            this.comboBoxDisciplineTimeDistribution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisciplineTimeDistribution.Enabled = false;
            this.comboBoxDisciplineTimeDistribution.FormattingEnabled = true;
            this.comboBoxDisciplineTimeDistribution.Location = new System.Drawing.Point(113, 6);
            this.comboBoxDisciplineTimeDistribution.Name = "comboBoxDisciplineTimeDistribution";
            this.comboBoxDisciplineTimeDistribution.Size = new System.Drawing.Size(220, 21);
            this.comboBoxDisciplineTimeDistribution.TabIndex = 1;
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
            // comboBoxTimeNorm
            // 
            this.comboBoxTimeNorm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTimeNorm.FormattingEnabled = true;
            this.comboBoxTimeNorm.Location = new System.Drawing.Point(113, 33);
            this.comboBoxTimeNorm.Name = "comboBoxTimeNorm";
            this.comboBoxTimeNorm.Size = new System.Drawing.Size(220, 21);
            this.comboBoxTimeNorm.TabIndex = 3;
            // 
            // labelClassroomDescription
            // 
            this.labelClassroomDescription.AutoSize = true;
            this.labelClassroomDescription.Location = new System.Drawing.Point(12, 63);
            this.labelClassroomDescription.Name = "labelClassroomDescription";
            this.labelClassroomDescription.Size = new System.Drawing.Size(67, 13);
            this.labelClassroomDescription.TabIndex = 4;
            this.labelClassroomDescription.Text = "Аудитории*:";
            // 
            // textBoxClassroomDescription
            // 
            this.textBoxClassroomDescription.Location = new System.Drawing.Point(113, 60);
            this.textBoxClassroomDescription.Name = "textBoxClassroomDescription";
            this.textBoxClassroomDescription.Size = new System.Drawing.Size(220, 20);
            this.textBoxClassroomDescription.TabIndex = 5;
            // 
            // FormDisciplineTimeDistributionClassroom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 131);
            this.Controls.Add(this.labelClassroomDescription);
            this.Controls.Add(this.textBoxClassroomDescription);
            this.Controls.Add(this.labelDisciplineTimeDistribution);
            this.Controls.Add(this.comboBoxDisciplineTimeDistribution);
            this.Controls.Add(this.labelTimeNorm);
            this.Controls.Add(this.comboBoxTimeNorm);
            this.Name = "FormDisciplineTimeDistributionClassroom";
            this.Text = "Аудитории расчасовки";
            this.Load += new System.EventHandler(this.FormDisciplineTimeDistributionClassroom_Load);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.buttonSaveAndClose, 0);
            this.Controls.SetChildIndex(this.comboBoxTimeNorm, 0);
            this.Controls.SetChildIndex(this.labelTimeNorm, 0);
            this.Controls.SetChildIndex(this.comboBoxDisciplineTimeDistribution, 0);
            this.Controls.SetChildIndex(this.labelDisciplineTimeDistribution, 0);
            this.Controls.SetChildIndex(this.textBoxClassroomDescription, 0);
            this.Controls.SetChildIndex(this.labelClassroomDescription, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDisciplineTimeDistribution;
        private System.Windows.Forms.ComboBox comboBoxDisciplineTimeDistribution;
        private System.Windows.Forms.Label labelTimeNorm;
        private System.Windows.Forms.ComboBox comboBoxTimeNorm;
        private System.Windows.Forms.Label labelClassroomDescription;
        private System.Windows.Forms.TextBox textBoxClassroomDescription;
    }
}