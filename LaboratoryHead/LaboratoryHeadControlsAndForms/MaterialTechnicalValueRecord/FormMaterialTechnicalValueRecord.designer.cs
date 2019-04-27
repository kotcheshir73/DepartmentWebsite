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
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(107, 148);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(254, 148);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(26, 148);
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
            this.Controls.Add(this.textBoxOrder);
            this.Controls.Add(this.labelOrder);
            this.Controls.Add(this.textBoxFieldValue);
            this.Controls.Add(this.labelFieldValue);
            this.Controls.Add(this.textBoxFieldName);
            this.Controls.Add(this.labelFieldName);
            this.Controls.Add(this.comboBoxMaterialTechnicalValueGroup);
            this.Controls.Add(this.labelMaterialTechnicalValueGroup);
            this.Controls.Add(this.comboBoxMaterialTechnicalValue);
            this.Controls.Add(this.labelMaterialTechnicalValue);
            this.Name = "MaterialTechnicalValueRecordForm";
            this.Text = "Запись описания МТЦ";
            this.Controls.SetChildIndex(this.labelMaterialTechnicalValue, 0);
            this.Controls.SetChildIndex(this.comboBoxMaterialTechnicalValue, 0);
            this.Controls.SetChildIndex(this.labelMaterialTechnicalValueGroup, 0);
            this.Controls.SetChildIndex(this.comboBoxMaterialTechnicalValueGroup, 0);
            this.Controls.SetChildIndex(this.labelFieldName, 0);
            this.Controls.SetChildIndex(this.textBoxFieldName, 0);
            this.Controls.SetChildIndex(this.labelFieldValue, 0);
            this.Controls.SetChildIndex(this.textBoxFieldValue, 0);
            this.Controls.SetChildIndex(this.labelOrder, 0);
            this.Controls.SetChildIndex(this.textBoxOrder, 0);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.buttonSaveAndClose, 0);
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