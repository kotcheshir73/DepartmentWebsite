namespace AcademicYearControlsAndForms.IndividualPlanNIRContractualWork
{
    partial class FormIndividualPlanNIRContractualWork
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
            this.labelIndividualPlan = new System.Windows.Forms.Label();
            this.comboBoxIndividualPlan = new System.Windows.Forms.ComboBox();
            this.labelJobContent = new System.Windows.Forms.Label();
            this.textBoxJobContent = new System.Windows.Forms.TextBox();
            this.textBoxPost = new System.Windows.Forms.TextBox();
            this.labelPost = new System.Windows.Forms.Label();
            this.textBoxPlannedTerm = new System.Windows.Forms.TextBox();
            this.labelPlannedTerm = new System.Windows.Forms.Label();
            this.labelReadyMark = new System.Windows.Forms.Label();
            this.checkBoxReadyMark = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(152, 166);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(299, 166);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(71, 166);
            // 
            // labelIndividualPlan
            // 
            this.labelIndividualPlan.AutoSize = true;
            this.labelIndividualPlan.Location = new System.Drawing.Point(12, 9);
            this.labelIndividualPlan.Name = "labelIndividualPlan";
            this.labelIndividualPlan.Size = new System.Drawing.Size(128, 13);
            this.labelIndividualPlan.TabIndex = 0;
            this.labelIndividualPlan.Text = "Индивидуальный план*:";
            // 
            // comboBoxIndividualPlan
            // 
            this.comboBoxIndividualPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIndividualPlan.Enabled = false;
            this.comboBoxIndividualPlan.FormattingEnabled = true;
            this.comboBoxIndividualPlan.Location = new System.Drawing.Point(224, 6);
            this.comboBoxIndividualPlan.Name = "comboBoxIndividualPlan";
            this.comboBoxIndividualPlan.Size = new System.Drawing.Size(220, 21);
            this.comboBoxIndividualPlan.TabIndex = 1;
            // 
            // labelJobContent
            // 
            this.labelJobContent.AutoSize = true;
            this.labelJobContent.Location = new System.Drawing.Point(12, 36);
            this.labelJobContent.Name = "labelJobContent";
            this.labelJobContent.Size = new System.Drawing.Size(190, 13);
            this.labelJobContent.TabIndex = 2;
            this.labelJobContent.Text = "Содержание выполняемой работы*:";
            // 
            // textBoxJobContent
            // 
            this.textBoxJobContent.Location = new System.Drawing.Point(224, 33);
            this.textBoxJobContent.Multiline = true;
            this.textBoxJobContent.Name = "textBoxJobContent";
            this.textBoxJobContent.Size = new System.Drawing.Size(220, 40);
            this.textBoxJobContent.TabIndex = 3;
            // 
            // textBoxPost
            // 
            this.textBoxPost.Location = new System.Drawing.Point(224, 79);
            this.textBoxPost.Name = "textBoxPost";
            this.textBoxPost.Size = new System.Drawing.Size(220, 20);
            this.textBoxPost.TabIndex = 5;
            // 
            // labelPost
            // 
            this.labelPost.AutoSize = true;
            this.labelPost.Location = new System.Drawing.Point(12, 82);
            this.labelPost.Name = "labelPost";
            this.labelPost.Size = new System.Drawing.Size(183, 13);
            this.labelPost.TabIndex = 4;
            this.labelPost.Text = "В качестве кого участвует в НИР*:";
            // 
            // textBoxPlannedTerm
            // 
            this.textBoxPlannedTerm.Location = new System.Drawing.Point(224, 105);
            this.textBoxPlannedTerm.Name = "textBoxPlannedTerm";
            this.textBoxPlannedTerm.Size = new System.Drawing.Size(220, 20);
            this.textBoxPlannedTerm.TabIndex = 7;
            // 
            // labelPlannedTerm
            // 
            this.labelPlannedTerm.AutoSize = true;
            this.labelPlannedTerm.Location = new System.Drawing.Point(12, 108);
            this.labelPlannedTerm.Name = "labelPlannedTerm";
            this.labelPlannedTerm.Size = new System.Drawing.Size(177, 13);
            this.labelPlannedTerm.TabIndex = 6;
            this.labelPlannedTerm.Text = "Планируемый срок выполнения*:";
            // 
            // labelReadyMark
            // 
            this.labelReadyMark.AutoSize = true;
            this.labelReadyMark.Location = new System.Drawing.Point(12, 134);
            this.labelReadyMark.Name = "labelReadyMark";
            this.labelReadyMark.Size = new System.Drawing.Size(206, 13);
            this.labelReadyMark.TabIndex = 8;
            this.labelReadyMark.Text = "Отметка о фактиче-ском выполнении*:";
            // 
            // checkBoxReadyMark
            // 
            this.checkBoxReadyMark.AutoSize = true;
            this.checkBoxReadyMark.Location = new System.Drawing.Point(224, 134);
            this.checkBoxReadyMark.Name = "checkBoxReadyMark";
            this.checkBoxReadyMark.Size = new System.Drawing.Size(15, 14);
            this.checkBoxReadyMark.TabIndex = 9;
            this.checkBoxReadyMark.UseVisualStyleBackColor = true;
            // 
            // FormIndividualPlanNIRContractualWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 201);
            this.Controls.Add(this.checkBoxReadyMark);
            this.Controls.Add(this.labelReadyMark);
            this.Controls.Add(this.textBoxPlannedTerm);
            this.Controls.Add(this.labelPlannedTerm);
            this.Controls.Add(this.textBoxPost);
            this.Controls.Add(this.labelPost);
            this.Controls.Add(this.textBoxJobContent);
            this.Controls.Add(this.labelJobContent);
            this.Controls.Add(this.comboBoxIndividualPlan);
            this.Controls.Add(this.labelIndividualPlan);
            this.Name = "FormIndividualPlanNIRContractualWork";
            this.Text = "Участие в хоздоговорной НИР";
            this.Load += new System.EventHandler(this.FormIndividualPlanNIRContractualWork_Load);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.buttonSaveAndClose, 0);
            this.Controls.SetChildIndex(this.labelIndividualPlan, 0);
            this.Controls.SetChildIndex(this.comboBoxIndividualPlan, 0);
            this.Controls.SetChildIndex(this.labelJobContent, 0);
            this.Controls.SetChildIndex(this.textBoxJobContent, 0);
            this.Controls.SetChildIndex(this.labelPost, 0);
            this.Controls.SetChildIndex(this.textBoxPost, 0);
            this.Controls.SetChildIndex(this.labelPlannedTerm, 0);
            this.Controls.SetChildIndex(this.textBoxPlannedTerm, 0);
            this.Controls.SetChildIndex(this.labelReadyMark, 0);
            this.Controls.SetChildIndex(this.checkBoxReadyMark, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelIndividualPlan;
        private System.Windows.Forms.ComboBox comboBoxIndividualPlan;
        private System.Windows.Forms.Label labelJobContent;
        private System.Windows.Forms.TextBox textBoxJobContent;
        private System.Windows.Forms.TextBox textBoxPost;
        private System.Windows.Forms.Label labelPost;
        private System.Windows.Forms.TextBox textBoxPlannedTerm;
        private System.Windows.Forms.Label labelPlannedTerm;
        private System.Windows.Forms.Label labelReadyMark;
        private System.Windows.Forms.CheckBox checkBoxReadyMark;
    }
}