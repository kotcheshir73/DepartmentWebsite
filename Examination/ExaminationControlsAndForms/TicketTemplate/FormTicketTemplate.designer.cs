namespace ExaminationControlsAndForms.TicketTemplate
{
    partial class FormTicketTemplate
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
            this.labelTemplateName = new System.Windows.Forms.Label();
            this.textBoxTemplateName = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.labelLinkToFile = new System.Windows.Forms.Label();
            this.textBoxLinkToFile = new System.Windows.Forms.TextBox();
            this.buttonLoadTemplate = new System.Windows.Forms.Button();
            this.tabPageTicketTemplate = new System.Windows.Forms.TabPage();
            this.tabPageParagraphsData = new System.Windows.Forms.TabPage();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tabControl);
            this.panelMain.Size = new System.Drawing.Size(1184, 625);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(1184, 36);
            // 
            // labelTemplateName
            // 
            this.labelTemplateName.AutoSize = true;
            this.labelTemplateName.Location = new System.Drawing.Point(14, 14);
            this.labelTemplateName.Name = "labelTemplateName";
            this.labelTemplateName.Size = new System.Drawing.Size(111, 13);
            this.labelTemplateName.TabIndex = 2;
            this.labelTemplateName.Text = "Название шаблона*:";
            // 
            // textBoxTemplateName
            // 
            this.textBoxTemplateName.Location = new System.Drawing.Point(131, 11);
            this.textBoxTemplateName.Name = "textBoxTemplateName";
            this.textBoxTemplateName.Size = new System.Drawing.Size(300, 20);
            this.textBoxTemplateName.TabIndex = 3;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageTicketTemplate);
            this.tabControl.Controls.Add(this.tabPageParagraphsData);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1184, 625);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.labelLinkToFile);
            this.tabPageConfig.Controls.Add(this.textBoxLinkToFile);
            this.tabPageConfig.Controls.Add(this.buttonLoadTemplate);
            this.tabPageConfig.Controls.Add(this.labelTemplateName);
            this.tabPageConfig.Controls.Add(this.textBoxTemplateName);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(1176, 599);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Настройки";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // labelLinkToFile
            // 
            this.labelLinkToFile.AutoSize = true;
            this.labelLinkToFile.Location = new System.Drawing.Point(41, 40);
            this.labelLinkToFile.Name = "labelLinkToFile";
            this.labelLinkToFile.Size = new System.Drawing.Size(84, 13);
            this.labelLinkToFile.TabIndex = 8;
            this.labelLinkToFile.Text = "Путь до файла:";
            // 
            // textBoxLinkToFile
            // 
            this.textBoxLinkToFile.Location = new System.Drawing.Point(131, 37);
            this.textBoxLinkToFile.Name = "textBoxLinkToFile";
            this.textBoxLinkToFile.Size = new System.Drawing.Size(300, 20);
            this.textBoxLinkToFile.TabIndex = 9;
            // 
            // buttonLoadTemplate
            // 
            this.buttonLoadTemplate.Location = new System.Drawing.Point(447, 35);
            this.buttonLoadTemplate.Name = "buttonLoadTemplate";
            this.buttonLoadTemplate.Size = new System.Drawing.Size(162, 23);
            this.buttonLoadTemplate.TabIndex = 7;
            this.buttonLoadTemplate.Text = "Загрузить шаблон";
            this.buttonLoadTemplate.UseVisualStyleBackColor = true;
            this.buttonLoadTemplate.Click += new System.EventHandler(this.ButtonLoadTemplate_Click);
            // 
            // tabPageTicketTemplate
            // 
            this.tabPageTicketTemplate.Location = new System.Drawing.Point(4, 22);
            this.tabPageTicketTemplate.Name = "tabPageTicketTemplate";
            this.tabPageTicketTemplate.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTicketTemplate.Size = new System.Drawing.Size(311, 148);
            this.tabPageTicketTemplate.TabIndex = 1;
            this.tabPageTicketTemplate.Text = "Шаблон";
            this.tabPageTicketTemplate.UseVisualStyleBackColor = true;
            // 
            // tabPageParagraphsData
            // 
            this.tabPageParagraphsData.Location = new System.Drawing.Point(4, 22);
            this.tabPageParagraphsData.Name = "tabPageParagraphsData";
            this.tabPageParagraphsData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageParagraphsData.Size = new System.Drawing.Size(1176, 599);
            this.tabPageParagraphsData.TabIndex = 2;
            this.tabPageParagraphsData.Text = "Параграфы";
            this.tabPageParagraphsData.UseVisualStyleBackColor = true;
            // 
            // FormTicketTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Name = "FormTicketTemplate";
            this.Text = "Шаблон билета";
            this.panelMain.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTemplateName;
        private System.Windows.Forms.TextBox textBoxTemplateName;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TabPage tabPageTicketTemplate;
        private System.Windows.Forms.Button buttonLoadTemplate;
        private System.Windows.Forms.Label labelLinkToFile;
        private System.Windows.Forms.TextBox textBoxLinkToFile;
        private System.Windows.Forms.TabPage tabPageParagraphsData;
    }
}