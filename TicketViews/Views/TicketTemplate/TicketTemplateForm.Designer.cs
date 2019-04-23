﻿namespace TicketViews.Views.TicketTemplate
{
    partial class TicketTemplateForm
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
            this.labelTemplateName = new System.Windows.Forms.Label();
            this.textBoxTemplateName = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.labelLinkToFile = new System.Windows.Forms.Label();
            this.textBoxLinkToFile = new System.Windows.Forms.TextBox();
            this.buttonLoadTemplate = new System.Windows.Forms.Button();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.examinationTemplateElement = new TicketViews.Views.ExaminationTemplate.ExaminationTemplateElement();
            this.labelExaminationTemplate = new System.Windows.Forms.Label();
            this.tabPageTicketTemplate = new System.Windows.Forms.TabPage();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.tabPageParagraphsData = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.tabPageTicketTemplate.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTemplateName
            // 
            this.labelTemplateName.AutoSize = true;
            this.labelTemplateName.Location = new System.Drawing.Point(14, 35);
            this.labelTemplateName.Name = "labelTemplateName";
            this.labelTemplateName.Size = new System.Drawing.Size(111, 13);
            this.labelTemplateName.TabIndex = 2;
            this.labelTemplateName.Text = "Название шаблона*:";
            // 
            // textBoxTemplateName
            // 
            this.textBoxTemplateName.Location = new System.Drawing.Point(131, 32);
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
            this.tabControl.Size = new System.Drawing.Size(1184, 661);
            this.tabControl.TabIndex = 1;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.labelLinkToFile);
            this.tabPageConfig.Controls.Add(this.textBoxLinkToFile);
            this.tabPageConfig.Controls.Add(this.buttonLoadTemplate);
            this.tabPageConfig.Controls.Add(this.buttonSaveAndClose);
            this.tabPageConfig.Controls.Add(this.buttonClose);
            this.tabPageConfig.Controls.Add(this.buttonSave);
            this.tabPageConfig.Controls.Add(this.labelTemplateName);
            this.tabPageConfig.Controls.Add(this.textBoxTemplateName);
            this.tabPageConfig.Controls.Add(this.examinationTemplateElement);
            this.tabPageConfig.Controls.Add(this.labelExaminationTemplate);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(1155, 641);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Настройки";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // labelLinkToFile
            // 
            this.labelLinkToFile.AutoSize = true;
            this.labelLinkToFile.Location = new System.Drawing.Point(489, 40);
            this.labelLinkToFile.Name = "labelLinkToFile";
            this.labelLinkToFile.Size = new System.Drawing.Size(84, 13);
            this.labelLinkToFile.TabIndex = 8;
            this.labelLinkToFile.Text = "Путь до файла:";
            // 
            // textBoxLinkToFile
            // 
            this.textBoxLinkToFile.Location = new System.Drawing.Point(579, 37);
            this.textBoxLinkToFile.Name = "textBoxLinkToFile";
            this.textBoxLinkToFile.Size = new System.Drawing.Size(541, 20);
            this.textBoxLinkToFile.TabIndex = 9;
            // 
            // buttonLoadTemplate
            // 
            this.buttonLoadTemplate.Location = new System.Drawing.Point(672, 8);
            this.buttonLoadTemplate.Name = "buttonLoadTemplate";
            this.buttonLoadTemplate.Size = new System.Drawing.Size(162, 23);
            this.buttonLoadTemplate.TabIndex = 7;
            this.buttonLoadTemplate.Text = "Загрузить шаблон";
            this.buttonLoadTemplate.UseVisualStyleBackColor = true;
            this.buttonLoadTemplate.Click += new System.EventHandler(this.ButtonLoadTemplate_Click);
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(149, 79);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 5;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.ButtonSaveAndClose_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(296, 79);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 6;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(68, 79);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // examinationTemplateElement
            // 
            this.examinationTemplateElement.DisciplineId = null;
            this.examinationTemplateElement.Id = null;
            this.examinationTemplateElement.Location = new System.Drawing.Point(131, 6);
            this.examinationTemplateElement.Name = "examinationTemplateElement";
            this.examinationTemplateElement.Size = new System.Drawing.Size(300, 20);
            this.examinationTemplateElement.TabIndex = 1;
            // 
            // labelExaminationTemplate
            // 
            this.labelExaminationTemplate.AutoSize = true;
            this.labelExaminationTemplate.Location = new System.Drawing.Point(14, 8);
            this.labelExaminationTemplate.Name = "labelExaminationTemplate";
            this.labelExaminationTemplate.Size = new System.Drawing.Size(59, 13);
            this.labelExaminationTemplate.TabIndex = 0;
            this.labelExaminationTemplate.Text = "Экзамен*:";
            // 
            // tabPageTicketTemplate
            // 
            this.tabPageTicketTemplate.Controls.Add(this.webBrowser);
            this.tabPageTicketTemplate.Location = new System.Drawing.Point(4, 22);
            this.tabPageTicketTemplate.Name = "tabPageTicketTemplate";
            this.tabPageTicketTemplate.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTicketTemplate.Size = new System.Drawing.Size(1155, 641);
            this.tabPageTicketTemplate.TabIndex = 1;
            this.tabPageTicketTemplate.Text = "Шаблон";
            this.tabPageTicketTemplate.UseVisualStyleBackColor = true;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(3, 3);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(1149, 635);
            this.webBrowser.TabIndex = 0;
            // 
            // tabPageParagraphsData
            // 
            this.tabPageParagraphsData.Location = new System.Drawing.Point(4, 22);
            this.tabPageParagraphsData.Name = "tabPageParagraphsData";
            this.tabPageParagraphsData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageParagraphsData.Size = new System.Drawing.Size(1176, 635);
            this.tabPageParagraphsData.TabIndex = 2;
            this.tabPageParagraphsData.Text = "Параграфы";
            this.tabPageParagraphsData.UseVisualStyleBackColor = true;
            // 
            // TicketTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.tabControl);
            this.Name = "TicketTemplateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Шаблон билета";
            this.Load += new System.EventHandler(this.TicketTemplateForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.tabPageTicketTemplate.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelTemplateName;
        private System.Windows.Forms.TextBox textBoxTemplateName;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private ExaminationTemplate.ExaminationTemplateElement examinationTemplateElement;
        private System.Windows.Forms.Label labelExaminationTemplate;
        private System.Windows.Forms.TabPage tabPageTicketTemplate;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoadTemplate;
        private System.Windows.Forms.Label labelLinkToFile;
        private System.Windows.Forms.TextBox textBoxLinkToFile;
        private System.Windows.Forms.TabPage tabPageParagraphsData;
    }
}