namespace AcademicYearControlsAndForms.IndividualPlanNIRScientificArticle
{
    partial class FormIndividualPlanNIRScientificArticle
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
            this.comboBoxIndividualPlan = new System.Windows.Forms.ComboBox();
            this.labelIndividualPlan = new System.Windows.Forms.Label();
            this.textBoxOrder = new System.Windows.Forms.TextBox();
            this.labelOrder = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxTypeOfPublication = new System.Windows.Forms.TextBox();
            this.labelTypeOfPublication = new System.Windows.Forms.Label();
            this.textBoxVolume = new System.Windows.Forms.TextBox();
            this.labelVolume = new System.Windows.Forms.Label();
            this.textBoxPublishing = new System.Windows.Forms.TextBox();
            this.labelPublishing = new System.Windows.Forms.Label();
            this.textBoxYear = new System.Windows.Forms.TextBox();
            this.labelYear = new System.Windows.Forms.Label();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.textBoxStatus);
            this.panelMain.Controls.Add(this.labelIndividualPlan);
            this.panelMain.Controls.Add(this.labelStatus);
            this.panelMain.Controls.Add(this.comboBoxIndividualPlan);
            this.panelMain.Controls.Add(this.textBoxYear);
            this.panelMain.Controls.Add(this.labelOrder);
            this.panelMain.Controls.Add(this.labelYear);
            this.panelMain.Controls.Add(this.textBoxOrder);
            this.panelMain.Controls.Add(this.textBoxPublishing);
            this.panelMain.Controls.Add(this.labelName);
            this.panelMain.Controls.Add(this.labelPublishing);
            this.panelMain.Controls.Add(this.textBoxName);
            this.panelMain.Controls.Add(this.textBoxVolume);
            this.panelMain.Controls.Add(this.labelTypeOfPublication);
            this.panelMain.Controls.Add(this.labelVolume);
            this.panelMain.Controls.Add(this.textBoxTypeOfPublication);
            this.panelMain.Size = new System.Drawing.Size(384, 225);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(384, 36);
            // 
            // comboBoxIndividualPlan
            // 
            this.comboBoxIndividualPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIndividualPlan.Enabled = false;
            this.comboBoxIndividualPlan.FormattingEnabled = true;
            this.comboBoxIndividualPlan.Location = new System.Drawing.Point(146, 9);
            this.comboBoxIndividualPlan.Name = "comboBoxIndividualPlan";
            this.comboBoxIndividualPlan.Size = new System.Drawing.Size(220, 21);
            this.comboBoxIndividualPlan.TabIndex = 1;
            // 
            // labelIndividualPlan
            // 
            this.labelIndividualPlan.AutoSize = true;
            this.labelIndividualPlan.Location = new System.Drawing.Point(12, 12);
            this.labelIndividualPlan.Name = "labelIndividualPlan";
            this.labelIndividualPlan.Size = new System.Drawing.Size(128, 13);
            this.labelIndividualPlan.TabIndex = 0;
            this.labelIndividualPlan.Text = "Индивидуальный план*:";
            // 
            // textBoxOrder
            // 
            this.textBoxOrder.Location = new System.Drawing.Point(146, 36);
            this.textBoxOrder.Name = "textBoxOrder";
            this.textBoxOrder.Size = new System.Drawing.Size(220, 20);
            this.textBoxOrder.TabIndex = 3;
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.Location = new System.Drawing.Point(12, 39);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(113, 13);
            this.labelOrder.TabIndex = 2;
            this.labelOrder.Text = "Порядковый номер*:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(146, 62);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(220, 20);
            this.textBoxName.TabIndex = 5;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 65);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(64, 13);
            this.labelName.TabIndex = 4;
            this.labelName.Text = "Название*:";
            // 
            // textBoxTypeOfPublication
            // 
            this.textBoxTypeOfPublication.Location = new System.Drawing.Point(146, 88);
            this.textBoxTypeOfPublication.Name = "textBoxTypeOfPublication";
            this.textBoxTypeOfPublication.Size = new System.Drawing.Size(220, 20);
            this.textBoxTypeOfPublication.TabIndex = 7;
            // 
            // labelTypeOfPublication
            // 
            this.labelTypeOfPublication.AutoSize = true;
            this.labelTypeOfPublication.Location = new System.Drawing.Point(12, 91);
            this.labelTypeOfPublication.Name = "labelTypeOfPublication";
            this.labelTypeOfPublication.Size = new System.Drawing.Size(95, 13);
            this.labelTypeOfPublication.TabIndex = 6;
            this.labelTypeOfPublication.Text = "Вид публикации*:";
            // 
            // textBoxVolume
            // 
            this.textBoxVolume.Location = new System.Drawing.Point(146, 114);
            this.textBoxVolume.Name = "textBoxVolume";
            this.textBoxVolume.Size = new System.Drawing.Size(220, 20);
            this.textBoxVolume.TabIndex = 9;
            // 
            // labelVolume
            // 
            this.labelVolume.AutoSize = true;
            this.labelVolume.Location = new System.Drawing.Point(12, 117);
            this.labelVolume.Name = "labelVolume";
            this.labelVolume.Size = new System.Drawing.Size(76, 13);
            this.labelVolume.TabIndex = 8;
            this.labelVolume.Text = "Объем (п.л.)*:";
            // 
            // textBoxPublishing
            // 
            this.textBoxPublishing.Location = new System.Drawing.Point(146, 140);
            this.textBoxPublishing.Name = "textBoxPublishing";
            this.textBoxPublishing.Size = new System.Drawing.Size(220, 20);
            this.textBoxPublishing.TabIndex = 11;
            // 
            // labelPublishing
            // 
            this.labelPublishing.AutoSize = true;
            this.labelPublishing.Location = new System.Drawing.Point(12, 143);
            this.labelPublishing.Name = "labelPublishing";
            this.labelPublishing.Size = new System.Drawing.Size(86, 13);
            this.labelPublishing.TabIndex = 10;
            this.labelPublishing.Text = "Издательство*:";
            // 
            // textBoxYear
            // 
            this.textBoxYear.Location = new System.Drawing.Point(146, 166);
            this.textBoxYear.Name = "textBoxYear";
            this.textBoxYear.Size = new System.Drawing.Size(220, 20);
            this.textBoxYear.TabIndex = 13;
            // 
            // labelYear
            // 
            this.labelYear.AutoSize = true;
            this.labelYear.Location = new System.Drawing.Point(12, 169);
            this.labelYear.Name = "labelYear";
            this.labelYear.Size = new System.Drawing.Size(32, 13);
            this.labelYear.TabIndex = 12;
            this.labelYear.Text = "Год*:";
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Location = new System.Drawing.Point(146, 192);
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.Size = new System.Drawing.Size(220, 20);
            this.textBoxStatus.TabIndex = 15;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(12, 195);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(44, 13);
            this.labelStatus.TabIndex = 14;
            this.labelStatus.Text = "Статус:";
            // 
            // FormIndividualPlanNIRScientificArticle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Name = "FormIndividualPlanNIRScientificArticle";
            this.Text = "Статьи и издания";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxIndividualPlan;
        private System.Windows.Forms.Label labelIndividualPlan;
        private System.Windows.Forms.TextBox textBoxOrder;
        private System.Windows.Forms.Label labelOrder;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxTypeOfPublication;
        private System.Windows.Forms.Label labelTypeOfPublication;
        private System.Windows.Forms.TextBox textBoxVolume;
        private System.Windows.Forms.Label labelVolume;
        private System.Windows.Forms.TextBox textBoxPublishing;
        private System.Windows.Forms.Label labelPublishing;
        private System.Windows.Forms.TextBox textBoxYear;
        private System.Windows.Forms.Label labelYear;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.Label labelStatus;
    }
}