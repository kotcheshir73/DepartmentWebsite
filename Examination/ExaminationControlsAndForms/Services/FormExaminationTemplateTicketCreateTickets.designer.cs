namespace ExaminationControlsAndForms.Services
{
    partial class FormExaminationTemplateTicketCreateTickets
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
            this.radioButtonByCountTickets = new System.Windows.Forms.RadioButton();
            this.radioButtonWhileCanCreate = new System.Windows.Forms.RadioButton();
            this.radioButtonBySelectedBlock = new System.Windows.Forms.RadioButton();
            this.groupBoxSelect = new System.Windows.Forms.GroupBox();
            this.buttonCreateTickets = new System.Windows.Forms.Button();
            this.groupBoxConfig = new System.Windows.Forms.GroupBox();
            this.panelBlockConfig = new System.Windows.Forms.Panel();
            this.panelHardConfig = new System.Windows.Forms.Panel();
            this.examinationTemplateBlockElement = new ExaminationControlsAndForms.ExaminationTemplateBlock.ControlExaminationTemplateBlockSearch();
            this.labelSeletecExaminationBlock = new System.Windows.Forms.Label();
            this.numericUpDownCountTickets = new System.Windows.Forms.NumericUpDown();
            this.labelCountTickets = new System.Windows.Forms.Label();
            this.groupBoxSelect.SuspendLayout();
            this.groupBoxConfig.SuspendLayout();
            this.panelHardConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCountTickets)).BeginInit();
            this.SuspendLayout();
            // 
            // radioButtonByCountTickets
            // 
            this.radioButtonByCountTickets.AutoSize = true;
            this.radioButtonByCountTickets.Location = new System.Drawing.Point(305, 24);
            this.radioButtonByCountTickets.Name = "radioButtonByCountTickets";
            this.radioButtonByCountTickets.Size = new System.Drawing.Size(143, 17);
            this.radioButtonByCountTickets.TabIndex = 1;
            this.radioButtonByCountTickets.Text = "По количеству билетов";
            this.radioButtonByCountTickets.UseVisualStyleBackColor = true;
            this.radioButtonByCountTickets.CheckedChanged += new System.EventHandler(this.RadioButtonByCountTickets_CheckedChanged);
            // 
            // radioButtonWhileCanCreate
            // 
            this.radioButtonWhileCanCreate.AutoSize = true;
            this.radioButtonWhileCanCreate.Checked = true;
            this.radioButtonWhileCanCreate.Location = new System.Drawing.Point(42, 24);
            this.radioButtonWhileCanCreate.Name = "radioButtonWhileCanCreate";
            this.radioButtonWhileCanCreate.Size = new System.Drawing.Size(162, 17);
            this.radioButtonWhileCanCreate.TabIndex = 0;
            this.radioButtonWhileCanCreate.TabStop = true;
            this.radioButtonWhileCanCreate.Text = "Пока возможно создавать";
            this.radioButtonWhileCanCreate.UseVisualStyleBackColor = true;
            this.radioButtonWhileCanCreate.CheckedChanged += new System.EventHandler(this.RadioButtonWhileCanCreate_CheckedChanged);
            // 
            // radioButtonBySelectedBlock
            // 
            this.radioButtonBySelectedBlock.AutoSize = true;
            this.radioButtonBySelectedBlock.Location = new System.Drawing.Point(624, 24);
            this.radioButtonBySelectedBlock.Name = "radioButtonBySelectedBlock";
            this.radioButtonBySelectedBlock.Size = new System.Drawing.Size(137, 17);
            this.radioButtonBySelectedBlock.TabIndex = 2;
            this.radioButtonBySelectedBlock.Text = "По выбранному блоку";
            this.radioButtonBySelectedBlock.UseVisualStyleBackColor = true;
            this.radioButtonBySelectedBlock.CheckedChanged += new System.EventHandler(this.RadioButtonBySelectedBlock_CheckedChanged);
            // 
            // groupBoxSelect
            // 
            this.groupBoxSelect.Controls.Add(this.buttonCreateTickets);
            this.groupBoxSelect.Controls.Add(this.radioButtonWhileCanCreate);
            this.groupBoxSelect.Controls.Add(this.radioButtonBySelectedBlock);
            this.groupBoxSelect.Controls.Add(this.radioButtonByCountTickets);
            this.groupBoxSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxSelect.Location = new System.Drawing.Point(0, 0);
            this.groupBoxSelect.Name = "groupBoxSelect";
            this.groupBoxSelect.Size = new System.Drawing.Size(1205, 57);
            this.groupBoxSelect.TabIndex = 0;
            this.groupBoxSelect.TabStop = false;
            this.groupBoxSelect.Text = "Выбор способа создания билетов";
            // 
            // buttonCreateTickets
            // 
            this.buttonCreateTickets.Location = new System.Drawing.Point(1016, 17);
            this.buttonCreateTickets.Name = "buttonCreateTickets";
            this.buttonCreateTickets.Size = new System.Drawing.Size(160, 30);
            this.buttonCreateTickets.TabIndex = 3;
            this.buttonCreateTickets.Text = "Создать билеты";
            this.buttonCreateTickets.UseVisualStyleBackColor = true;
            this.buttonCreateTickets.Click += new System.EventHandler(this.ButtonCreateTickets_Click);
            // 
            // groupBoxConfig
            // 
            this.groupBoxConfig.Controls.Add(this.panelBlockConfig);
            this.groupBoxConfig.Controls.Add(this.panelHardConfig);
            this.groupBoxConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxConfig.Location = new System.Drawing.Point(0, 57);
            this.groupBoxConfig.Name = "groupBoxConfig";
            this.groupBoxConfig.Size = new System.Drawing.Size(1205, 651);
            this.groupBoxConfig.TabIndex = 1;
            this.groupBoxConfig.TabStop = false;
            this.groupBoxConfig.Text = "Настройки";
            // 
            // panelBlockConfig
            // 
            this.panelBlockConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBlockConfig.Location = new System.Drawing.Point(3, 65);
            this.panelBlockConfig.Name = "panelBlockConfig";
            this.panelBlockConfig.Size = new System.Drawing.Size(1199, 583);
            this.panelBlockConfig.TabIndex = 1;
            // 
            // panelHardConfig
            // 
            this.panelHardConfig.Controls.Add(this.examinationTemplateBlockElement);
            this.panelHardConfig.Controls.Add(this.labelSeletecExaminationBlock);
            this.panelHardConfig.Controls.Add(this.numericUpDownCountTickets);
            this.panelHardConfig.Controls.Add(this.labelCountTickets);
            this.panelHardConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHardConfig.Location = new System.Drawing.Point(3, 16);
            this.panelHardConfig.Name = "panelHardConfig";
            this.panelHardConfig.Size = new System.Drawing.Size(1199, 49);
            this.panelHardConfig.TabIndex = 0;
            // 
            // examinationTemplateBlockElement
            // 
            this.examinationTemplateBlockElement.Enabled = false;
            this.examinationTemplateBlockElement.ExaminationTemplateId = null;
            this.examinationTemplateBlockElement.Id = null;
            this.examinationTemplateBlockElement.Location = new System.Drawing.Point(720, 16);
            this.examinationTemplateBlockElement.Name = "examinationTemplateBlockElement";
            this.examinationTemplateBlockElement.Service = null;
            this.examinationTemplateBlockElement.Size = new System.Drawing.Size(300, 20);
            this.examinationTemplateBlockElement.TabIndex = 3;
            // 
            // labelSeletecExaminationBlock
            // 
            this.labelSeletecExaminationBlock.AutoSize = true;
            this.labelSeletecExaminationBlock.Location = new System.Drawing.Point(618, 18);
            this.labelSeletecExaminationBlock.Name = "labelSeletecExaminationBlock";
            this.labelSeletecExaminationBlock.Size = new System.Drawing.Size(96, 13);
            this.labelSeletecExaminationBlock.TabIndex = 2;
            this.labelSeletecExaminationBlock.Text = "Выбранный блок:";
            // 
            // numericUpDownCountTickets
            // 
            this.numericUpDownCountTickets.Enabled = false;
            this.numericUpDownCountTickets.Location = new System.Drawing.Point(418, 16);
            this.numericUpDownCountTickets.Name = "numericUpDownCountTickets";
            this.numericUpDownCountTickets.Size = new System.Drawing.Size(85, 20);
            this.numericUpDownCountTickets.TabIndex = 1;
            // 
            // labelCountTickets
            // 
            this.labelCountTickets.AutoSize = true;
            this.labelCountTickets.Location = new System.Drawing.Point(299, 18);
            this.labelCountTickets.Name = "labelCountTickets";
            this.labelCountTickets.Size = new System.Drawing.Size(113, 13);
            this.labelCountTickets.TabIndex = 0;
            this.labelCountTickets.Text = "Количество билетов:";
            // 
            // ExaminationTemplateTicketCreateTicketsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 708);
            this.Controls.Add(this.groupBoxConfig);
            this.Controls.Add(this.groupBoxSelect);
            this.Name = "ExaminationTemplateTicketCreateTicketsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Генерация билетов";
            this.Load += new System.EventHandler(this.FormExaminationTemplateTicketCreateTickets_Load);
            this.groupBoxSelect.ResumeLayout(false);
            this.groupBoxSelect.PerformLayout();
            this.groupBoxConfig.ResumeLayout(false);
            this.panelHardConfig.ResumeLayout(false);
            this.panelHardConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCountTickets)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonByCountTickets;
        private System.Windows.Forms.RadioButton radioButtonWhileCanCreate;
        private System.Windows.Forms.RadioButton radioButtonBySelectedBlock;
        private System.Windows.Forms.GroupBox groupBoxSelect;
        private System.Windows.Forms.GroupBox groupBoxConfig;
        private System.Windows.Forms.Panel panelHardConfig;
        private System.Windows.Forms.Label labelCountTickets;
        private System.Windows.Forms.NumericUpDown numericUpDownCountTickets;
        private System.Windows.Forms.Label labelSeletecExaminationBlock;
        private ExaminationTemplateBlock.ControlExaminationTemplateBlockSearch examinationTemplateBlockElement;
        private System.Windows.Forms.Panel panelBlockConfig;
        private System.Windows.Forms.Button buttonCreateTickets;
    }
}