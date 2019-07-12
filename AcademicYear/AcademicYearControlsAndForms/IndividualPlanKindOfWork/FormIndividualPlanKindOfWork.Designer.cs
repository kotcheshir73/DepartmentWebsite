namespace AcademicYearControlsAndForms.IndividualPlanKindOfWork
{
    partial class FormIndividualPlanKindOfWork
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
            this.comboBoxIndividualPlanTitle = new System.Windows.Forms.ComboBox();
            this.labelIndividualPlanTitle = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxOrder = new System.Windows.Forms.TextBox();
            this.labelOrder = new System.Windows.Forms.Label();
            this.textBoxTimeNormDescription = new System.Windows.Forms.TextBox();
            this.labelTimeNormDescription = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.textBoxTimeNormDescription);
            this.panelMain.Controls.Add(this.labelIndividualPlanTitle);
            this.panelMain.Controls.Add(this.labelTimeNormDescription);
            this.panelMain.Controls.Add(this.comboBoxIndividualPlanTitle);
            this.panelMain.Controls.Add(this.textBoxName);
            this.panelMain.Controls.Add(this.labelOrder);
            this.panelMain.Controls.Add(this.labelName);
            this.panelMain.Controls.Add(this.textBoxOrder);
            this.panelMain.Size = new System.Drawing.Size(384, 115);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(384, 36);
            // 
            // comboBoxIndividualPlanTitle
            // 
            this.comboBoxIndividualPlanTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIndividualPlanTitle.Enabled = false;
            this.comboBoxIndividualPlanTitle.FormattingEnabled = true;
            this.comboBoxIndividualPlanTitle.Location = new System.Drawing.Point(146, 10);
            this.comboBoxIndividualPlanTitle.Name = "comboBoxIndividualPlanTitle";
            this.comboBoxIndividualPlanTitle.Size = new System.Drawing.Size(220, 21);
            this.comboBoxIndividualPlanTitle.TabIndex = 1;
            // 
            // labelIndividualPlanTitle
            // 
            this.labelIndividualPlanTitle.AutoSize = true;
            this.labelIndividualPlanTitle.Location = new System.Drawing.Point(12, 13);
            this.labelIndividualPlanTitle.Name = "labelIndividualPlanTitle";
            this.labelIndividualPlanTitle.Size = new System.Drawing.Size(68, 13);
            this.labelIndividualPlanTitle.TabIndex = 0;
            this.labelIndividualPlanTitle.Text = "Заголовок*:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(146, 63);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(220, 20);
            this.textBoxName.TabIndex = 5;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 66);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(64, 13);
            this.labelName.TabIndex = 4;
            this.labelName.Text = "Название*:";
            // 
            // textBoxOrder
            // 
            this.textBoxOrder.Location = new System.Drawing.Point(146, 37);
            this.textBoxOrder.Name = "textBoxOrder";
            this.textBoxOrder.Size = new System.Drawing.Size(220, 20);
            this.textBoxOrder.TabIndex = 3;
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.Location = new System.Drawing.Point(12, 40);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(113, 13);
            this.labelOrder.TabIndex = 2;
            this.labelOrder.Text = "Порядковый номер*:";
            // 
            // textBoxTimeNormDescription
            // 
            this.textBoxTimeNormDescription.Location = new System.Drawing.Point(146, 89);
            this.textBoxTimeNormDescription.Name = "textBoxTimeNormDescription";
            this.textBoxTimeNormDescription.Size = new System.Drawing.Size(220, 20);
            this.textBoxTimeNormDescription.TabIndex = 7;
            // 
            // labelTimeNormDescription
            // 
            this.labelTimeNormDescription.AutoSize = true;
            this.labelTimeNormDescription.Location = new System.Drawing.Point(12, 92);
            this.labelTimeNormDescription.Name = "labelTimeNormDescription";
            this.labelTimeNormDescription.Size = new System.Drawing.Size(64, 13);
            this.labelTimeNormDescription.TabIndex = 6;
            this.labelTimeNormDescription.Text = "Описание*:";
            // 
            // FormIndividualPlanKindOfWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 151);
            this.Name = "FormIndividualPlanKindOfWork";
            this.Text = "Вид работы";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxIndividualPlanTitle;
        private System.Windows.Forms.Label labelIndividualPlanTitle;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxOrder;
        private System.Windows.Forms.Label labelOrder;
        private System.Windows.Forms.TextBox textBoxTimeNormDescription;
        private System.Windows.Forms.Label labelTimeNormDescription;
    }
}