namespace LaboratoryHeadControlsAndForms.MaterialTechnicalValueRecord
{
    partial class FormMaterialTechnicalValueRecord
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
            this.comboBoxMaterialTechnicalValue = new System.Windows.Forms.ComboBox();
            this.labelMaterialTechnicalValue = new System.Windows.Forms.Label();
            this.comboBoxMaterialTechnicalValueGroup = new System.Windows.Forms.ComboBox();
            this.labelMaterialTechnicalValueGroup = new System.Windows.Forms.Label();
            this.textBoxFieldName = new System.Windows.Forms.TextBox();
            this.labelFieldName = new System.Windows.Forms.Label();
            this.textBoxFieldValue = new System.Windows.Forms.TextBox();
            this.labelFieldValue = new System.Windows.Forms.Label();
            this.textBoxOrder = new System.Windows.Forms.TextBox();
            this.labelOrder = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.textBoxOrder);
            this.panelMain.Controls.Add(this.labelOrder);
            this.panelMain.Controls.Add(this.textBoxFieldValue);
            this.panelMain.Controls.Add(this.labelFieldValue);
            this.panelMain.Controls.Add(this.textBoxFieldName);
            this.panelMain.Controls.Add(this.labelFieldName);
            this.panelMain.Controls.Add(this.comboBoxMaterialTechnicalValueGroup);
            this.panelMain.Controls.Add(this.labelMaterialTechnicalValueGroup);
            this.panelMain.Controls.Add(this.comboBoxMaterialTechnicalValue);
            this.panelMain.Controls.Add(this.labelMaterialTechnicalValue);
            this.panelMain.Size = new System.Drawing.Size(364, 65);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(364, 36);
            // 
            // comboBoxMaterialTechnicalValue
            // 
            this.comboBoxMaterialTechnicalValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMaterialTechnicalValue.Enabled = false;
            this.comboBoxMaterialTechnicalValue.FormattingEnabled = true;
            this.comboBoxMaterialTechnicalValue.Location = new System.Drawing.Point(136, 6);
            this.comboBoxMaterialTechnicalValue.Name = "comboBoxMaterialTechnicalValue";
            this.comboBoxMaterialTechnicalValue.Size = new System.Drawing.Size(210, 21);
            this.comboBoxMaterialTechnicalValue.TabIndex = 1;
            // 
            // labelMaterialTechnicalValue
            // 
            this.labelMaterialTechnicalValue.AutoSize = true;
            this.labelMaterialTechnicalValue.Location = new System.Drawing.Point(12, 9);
            this.labelMaterialTechnicalValue.Name = "labelMaterialTechnicalValue";
            this.labelMaterialTechnicalValue.Size = new System.Drawing.Size(72, 13);
            this.labelMaterialTechnicalValue.TabIndex = 0;
            this.labelMaterialTechnicalValue.Text = "Инв. номер*:";
            // 
            // comboBoxMaterialTechnicalValueGroup
            // 
            this.comboBoxMaterialTechnicalValueGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMaterialTechnicalValueGroup.FormattingEnabled = true;
            this.comboBoxMaterialTechnicalValueGroup.Location = new System.Drawing.Point(136, 33);
            this.comboBoxMaterialTechnicalValueGroup.Name = "comboBoxMaterialTechnicalValueGroup";
            this.comboBoxMaterialTechnicalValueGroup.Size = new System.Drawing.Size(210, 21);
            this.comboBoxMaterialTechnicalValueGroup.TabIndex = 3;
            // 
            // labelMaterialTechnicalValueGroup
            // 
            this.labelMaterialTechnicalValueGroup.AutoSize = true;
            this.labelMaterialTechnicalValueGroup.Location = new System.Drawing.Point(12, 36);
            this.labelMaterialTechnicalValueGroup.Name = "labelMaterialTechnicalValueGroup";
            this.labelMaterialTechnicalValueGroup.Size = new System.Drawing.Size(100, 13);
            this.labelMaterialTechnicalValueGroup.TabIndex = 2;
            this.labelMaterialTechnicalValueGroup.Text = "Группа сведений*:";
            // 
            // textBoxFieldName
            // 
            this.textBoxFieldName.Location = new System.Drawing.Point(136, 60);
            this.textBoxFieldName.Name = "textBoxFieldName";
            this.textBoxFieldName.Size = new System.Drawing.Size(210, 20);
            this.textBoxFieldName.TabIndex = 5;
            // 
            // labelFieldName
            // 
            this.labelFieldName.AutoSize = true;
            this.labelFieldName.Location = new System.Drawing.Point(12, 63);
            this.labelFieldName.Name = "labelFieldName";
            this.labelFieldName.Size = new System.Drawing.Size(64, 13);
            this.labelFieldName.TabIndex = 4;
            this.labelFieldName.Text = "Название*:";
            // 
            // textBoxFieldValue
            // 
            this.textBoxFieldValue.Location = new System.Drawing.Point(136, 86);
            this.textBoxFieldValue.Name = "textBoxFieldValue";
            this.textBoxFieldValue.Size = new System.Drawing.Size(210, 20);
            this.textBoxFieldValue.TabIndex = 7;
            // 
            // labelFieldValue
            // 
            this.labelFieldValue.AutoSize = true;
            this.labelFieldValue.Location = new System.Drawing.Point(12, 89);
            this.labelFieldValue.Name = "labelFieldValue";
            this.labelFieldValue.Size = new System.Drawing.Size(58, 13);
            this.labelFieldValue.TabIndex = 6;
            this.labelFieldValue.Text = "Значение:";
            // 
            // textBoxOrder
            // 
            this.textBoxOrder.Location = new System.Drawing.Point(136, 112);
            this.textBoxOrder.Name = "textBoxOrder";
            this.textBoxOrder.Size = new System.Drawing.Size(210, 20);
            this.textBoxOrder.TabIndex = 9;
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.Location = new System.Drawing.Point(12, 115);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(113, 13);
            this.labelOrder.TabIndex = 8;
            this.labelOrder.Text = "Порядковый номер*:";
            // 
            // MaterialTechnicalValueRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 183);
            this.Name = "MaterialTechnicalValueRecordForm";
            this.Text = "Запись описания МТЦ";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxMaterialTechnicalValue;
        private System.Windows.Forms.Label labelMaterialTechnicalValue;
        private System.Windows.Forms.ComboBox comboBoxMaterialTechnicalValueGroup;
        private System.Windows.Forms.Label labelMaterialTechnicalValueGroup;
        private System.Windows.Forms.TextBox textBoxFieldName;
        private System.Windows.Forms.Label labelFieldName;
        private System.Windows.Forms.TextBox textBoxFieldValue;
        private System.Windows.Forms.Label labelFieldValue;
        private System.Windows.Forms.TextBox textBoxOrder;
        private System.Windows.Forms.Label labelOrder;
    }
}