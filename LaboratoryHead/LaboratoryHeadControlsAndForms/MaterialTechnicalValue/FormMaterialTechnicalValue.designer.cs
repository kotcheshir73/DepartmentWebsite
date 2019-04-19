namespace LaboratoryHeadControlsAndForms.MaterialTechnicalValue
{
    partial class FormMaterialTechnicalValue
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
            this.labelClassroom = new System.Windows.Forms.Label();
            this.comboBoxClassroom = new System.Windows.Forms.ComboBox();
            this.labelInventoryNumber = new System.Windows.Forms.Label();
            this.textBoxInventoryNumber = new System.Windows.Forms.TextBox();
            this.textBoxFullName = new System.Windows.Forms.TextBox();
            this.labelFullName = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.labelLocation = new System.Windows.Forms.Label();
            this.labelCost = new System.Windows.Forms.Label();
            this.textBoxDeleteReason = new System.Windows.Forms.TextBox();
            this.labelDeleteReason = new System.Windows.Forms.Label();
            this.labelDateInclude = new System.Windows.Forms.Label();
            this.dateTimePickerDateInclude = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerDateDelete = new System.Windows.Forms.DateTimePicker();
            this.labelDateDelete = new System.Windows.Forms.Label();
            this.textBoxCost = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.tabPageMaterialTechnicalValueRecords = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(300, 343);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(447, 343);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(219, 343);
            // 
            // labelClassroom
            // 
            this.labelClassroom.AutoSize = true;
            this.labelClassroom.Location = new System.Drawing.Point(15, 12);
            this.labelClassroom.Name = "labelClassroom";
            this.labelClassroom.Size = new System.Drawing.Size(67, 13);
            this.labelClassroom.TabIndex = 0;
            this.labelClassroom.Text = "Аудитория*:";
            // 
            // comboBoxClassroom
            // 
            this.comboBoxClassroom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClassroom.FormattingEnabled = true;
            this.comboBoxClassroom.Location = new System.Drawing.Point(139, 9);
            this.comboBoxClassroom.Name = "comboBoxClassroom";
            this.comboBoxClassroom.Size = new System.Drawing.Size(210, 21);
            this.comboBoxClassroom.TabIndex = 1;
            // 
            // labelInventoryNumber
            // 
            this.labelInventoryNumber.AutoSize = true;
            this.labelInventoryNumber.Location = new System.Drawing.Point(15, 39);
            this.labelInventoryNumber.Name = "labelInventoryNumber";
            this.labelInventoryNumber.Size = new System.Drawing.Size(118, 13);
            this.labelInventoryNumber.TabIndex = 4;
            this.labelInventoryNumber.Text = "Инвентарный номер*:";
            // 
            // textBoxInventoryNumber
            // 
            this.textBoxInventoryNumber.Location = new System.Drawing.Point(139, 36);
            this.textBoxInventoryNumber.Name = "textBoxInventoryNumber";
            this.textBoxInventoryNumber.Size = new System.Drawing.Size(210, 20);
            this.textBoxInventoryNumber.TabIndex = 5;
            // 
            // textBoxFullName
            // 
            this.textBoxFullName.Location = new System.Drawing.Point(139, 62);
            this.textBoxFullName.Multiline = true;
            this.textBoxFullName.Name = "textBoxFullName";
            this.textBoxFullName.Size = new System.Drawing.Size(480, 40);
            this.textBoxFullName.TabIndex = 9;
            // 
            // labelFullName
            // 
            this.labelFullName.AutoSize = true;
            this.labelFullName.Location = new System.Drawing.Point(15, 65);
            this.labelFullName.Name = "labelFullName";
            this.labelFullName.Size = new System.Drawing.Size(90, 13);
            this.labelFullName.TabIndex = 8;
            this.labelFullName.Text = "Наименование*:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(139, 108);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(480, 100);
            this.textBoxDescription.TabIndex = 11;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(15, 111);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(60, 13);
            this.labelDescription.TabIndex = 10;
            this.labelDescription.Text = "Описание:";
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.Location = new System.Drawing.Point(139, 214);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Size = new System.Drawing.Size(480, 20);
            this.textBoxLocation.TabIndex = 13;
            // 
            // labelLocation
            // 
            this.labelLocation.AutoSize = true;
            this.labelLocation.Location = new System.Drawing.Point(15, 217);
            this.labelLocation.Name = "labelLocation";
            this.labelLocation.Size = new System.Drawing.Size(85, 13);
            this.labelLocation.TabIndex = 12;
            this.labelLocation.Text = "Расположение:";
            // 
            // labelCost
            // 
            this.labelCost.AutoSize = true;
            this.labelCost.Location = new System.Drawing.Point(366, 39);
            this.labelCost.Name = "labelCost";
            this.labelCost.Size = new System.Drawing.Size(36, 13);
            this.labelCost.TabIndex = 6;
            this.labelCost.Text = "Цена:";
            // 
            // textBoxDeleteReason
            // 
            this.textBoxDeleteReason.Location = new System.Drawing.Point(139, 240);
            this.textBoxDeleteReason.Multiline = true;
            this.textBoxDeleteReason.Name = "textBoxDeleteReason";
            this.textBoxDeleteReason.Size = new System.Drawing.Size(210, 40);
            this.textBoxDeleteReason.TabIndex = 15;
            this.textBoxDeleteReason.TextChanged += new System.EventHandler(this.TextBoxDeleteReason_TextChanged);
            // 
            // labelDeleteReason
            // 
            this.labelDeleteReason.AutoSize = true;
            this.labelDeleteReason.Location = new System.Drawing.Point(15, 243);
            this.labelDeleteReason.Name = "labelDeleteReason";
            this.labelDeleteReason.Size = new System.Drawing.Size(104, 13);
            this.labelDeleteReason.TabIndex = 14;
            this.labelDeleteReason.Text = "Причина списания:";
            // 
            // labelDateInclude
            // 
            this.labelDateInclude.AutoSize = true;
            this.labelDateInclude.Location = new System.Drawing.Point(366, 12);
            this.labelDateInclude.Name = "labelDateInclude";
            this.labelDateInclude.Size = new System.Drawing.Size(107, 13);
            this.labelDateInclude.TabIndex = 2;
            this.labelDateInclude.Text = "Дата поступления*:";
            // 
            // dateTimePickerDateInclude
            // 
            this.dateTimePickerDateInclude.Location = new System.Drawing.Point(479, 10);
            this.dateTimePickerDateInclude.Name = "dateTimePickerDateInclude";
            this.dateTimePickerDateInclude.Size = new System.Drawing.Size(140, 20);
            this.dateTimePickerDateInclude.TabIndex = 3;
            // 
            // dateTimePickerDateDelete
            // 
            this.dateTimePickerDateDelete.Location = new System.Drawing.Point(479, 241);
            this.dateTimePickerDateDelete.Name = "dateTimePickerDateDelete";
            this.dateTimePickerDateDelete.Size = new System.Drawing.Size(140, 20);
            this.dateTimePickerDateDelete.TabIndex = 17;
            // 
            // labelDateDelete
            // 
            this.labelDateDelete.AutoSize = true;
            this.labelDateDelete.Location = new System.Drawing.Point(366, 243);
            this.labelDateDelete.Name = "labelDateDelete";
            this.labelDateDelete.Size = new System.Drawing.Size(87, 13);
            this.labelDateDelete.TabIndex = 16;
            this.labelDateDelete.Text = "Дата списания:";
            // 
            // textBoxCost
            // 
            this.textBoxCost.Location = new System.Drawing.Point(409, 36);
            this.textBoxCost.Name = "textBoxCost";
            this.textBoxCost.Size = new System.Drawing.Size(210, 20);
            this.textBoxCost.TabIndex = 7;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageMaterialTechnicalValueRecords);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(730, 341);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.labelClassroom);
            this.tabPageConfig.Controls.Add(this.textBoxCost);
            this.tabPageConfig.Controls.Add(this.comboBoxClassroom);
            this.tabPageConfig.Controls.Add(this.labelInventoryNumber);
            this.tabPageConfig.Controls.Add(this.textBoxInventoryNumber);
            this.tabPageConfig.Controls.Add(this.labelFullName);
            this.tabPageConfig.Controls.Add(this.dateTimePickerDateDelete);
            this.tabPageConfig.Controls.Add(this.textBoxFullName);
            this.tabPageConfig.Controls.Add(this.labelDateDelete);
            this.tabPageConfig.Controls.Add(this.labelDescription);
            this.tabPageConfig.Controls.Add(this.dateTimePickerDateInclude);
            this.tabPageConfig.Controls.Add(this.textBoxDescription);
            this.tabPageConfig.Controls.Add(this.labelDateInclude);
            this.tabPageConfig.Controls.Add(this.labelLocation);
            this.tabPageConfig.Controls.Add(this.textBoxDeleteReason);
            this.tabPageConfig.Controls.Add(this.textBoxLocation);
            this.tabPageConfig.Controls.Add(this.labelDeleteReason);
            this.tabPageConfig.Controls.Add(this.labelCost);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(722, 315);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Настройки";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // tabPageMaterialTechnicalValueRecords
            // 
            this.tabPageMaterialTechnicalValueRecords.Location = new System.Drawing.Point(4, 22);
            this.tabPageMaterialTechnicalValueRecords.Name = "tabPageMaterialTechnicalValueRecords";
            this.tabPageMaterialTechnicalValueRecords.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMaterialTechnicalValueRecords.Size = new System.Drawing.Size(722, 315);
            this.tabPageMaterialTechnicalValueRecords.TabIndex = 1;
            this.tabPageMaterialTechnicalValueRecords.Text = "Характеристики";
            this.tabPageMaterialTechnicalValueRecords.UseVisualStyleBackColor = true;
            // 
            // MaterialTechnicalValueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 370);
            this.Controls.Add(this.tabControl);
            this.Name = "MaterialTechnicalValueForm";
            this.Text = "Материально-техническая ценность";
            this.Load += new System.EventHandler(this.FormMaterialTechnicalValue_Load);
            this.Controls.SetChildIndex(this.tabControl, 0);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.buttonSaveAndClose, 0);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelClassroom;
        private System.Windows.Forms.ComboBox comboBoxClassroom;
        private System.Windows.Forms.Label labelInventoryNumber;
        private System.Windows.Forms.TextBox textBoxInventoryNumber;
        private System.Windows.Forms.TextBox textBoxFullName;
        private System.Windows.Forms.Label labelFullName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.Label labelLocation;
        private System.Windows.Forms.Label labelCost;
        private System.Windows.Forms.TextBox textBoxDeleteReason;
        private System.Windows.Forms.Label labelDeleteReason;
        private System.Windows.Forms.Label labelDateInclude;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateInclude;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateDelete;
        private System.Windows.Forms.Label labelDateDelete;
        private System.Windows.Forms.TextBox textBoxCost;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TabPage tabPageMaterialTechnicalValueRecords;
    }
}