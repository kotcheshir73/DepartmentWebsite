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
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(125, 116);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(272, 116);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(44, 116);
            // 
            // comboBoxIndividualPlanTitle
            // 
            this.comboBoxIndividualPlanTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIndividualPlanTitle.Enabled = false;
            this.comboBoxIndividualPlanTitle.FormattingEnabled = true;
            this.comboBoxIndividualPlanTitle.Location = new System.Drawing.Point(146, 6);
            this.comboBoxIndividualPlanTitle.Name = "comboBoxIndividualPlanTitle";
            this.comboBoxIndividualPlanTitle.Size = new System.Drawing.Size(220, 21);
            this.comboBoxIndividualPlanTitle.TabIndex = 1;
            // 
            // labelIndividualPlanTitle
            // 
            this.labelIndividualPlanTitle.AutoSize = true;
            this.labelIndividualPlanTitle.Location = new System.Drawing.Point(12, 9);
            this.labelIndividualPlanTitle.Name = "labelIndividualPlanTitle";
            this.labelIndividualPlanTitle.Size = new System.Drawing.Size(68, 13);
            this.labelIndividualPlanTitle.TabIndex = 0;
            this.labelIndividualPlanTitle.Text = "Заголовок*:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(146, 59);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(220, 20);
            this.textBoxName.TabIndex = 5;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 62);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(64, 13);
            this.labelName.TabIndex = 4;
            this.labelName.Text = "Название*:";
            // 
            // textBoxOrder
            // 
            this.textBoxOrder.Location = new System.Drawing.Point(146, 33);
            this.textBoxOrder.Name = "textBoxOrder";
            this.textBoxOrder.Size = new System.Drawing.Size(220, 20);
            this.textBoxOrder.TabIndex = 3;
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.Location = new System.Drawing.Point(12, 36);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(113, 13);
            this.labelOrder.TabIndex = 2;
            this.labelOrder.Text = "Порядковый номер*:";
            // 
            // textBoxTimeNormDescription
            // 
            this.textBoxTimeNormDescription.Location = new System.Drawing.Point(146, 85);
            this.textBoxTimeNormDescription.Name = "textBoxTimeNormDescription";
            this.textBoxTimeNormDescription.Size = new System.Drawing.Size(220, 20);
            this.textBoxTimeNormDescription.TabIndex = 7;
            // 
            // labelTimeNormDescription
            // 
            this.labelTimeNormDescription.AutoSize = true;
            this.labelTimeNormDescription.Location = new System.Drawing.Point(12, 88);
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
            this.Controls.Add(this.textBoxTimeNormDescription);
            this.Controls.Add(this.labelTimeNormDescription);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxOrder);
            this.Controls.Add(this.labelOrder);
            this.Controls.Add(this.comboBoxIndividualPlanTitle);
            this.Controls.Add(this.labelIndividualPlanTitle);
            this.Name = "FormIndividualPlanKindOfWork";
            this.Text = "Вид работы";
            this.Load += new System.EventHandler(this.FormIndividualPlanKindOfWork_Load);
            this.Controls.SetChildIndex(this.labelIndividualPlanTitle, 0);
            this.Controls.SetChildIndex(this.comboBoxIndividualPlanTitle, 0);
            this.Controls.SetChildIndex(this.labelOrder, 0);
            this.Controls.SetChildIndex(this.textBoxOrder, 0);
            this.Controls.SetChildIndex(this.labelName, 0);
            this.Controls.SetChildIndex(this.textBoxName, 0);
            this.Controls.SetChildIndex(this.labelTimeNormDescription, 0);
            this.Controls.SetChildIndex(this.textBoxTimeNormDescription, 0);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.buttonSaveAndClose, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

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