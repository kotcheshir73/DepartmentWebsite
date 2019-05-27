namespace ExaminationControlsAndForms.Services
{
    partial class ControlTicketTemplateLoader
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonLoadTemplate = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageBrowser = new System.Windows.Forms.TabPage();
            this.tabPageElements = new System.Windows.Forms.TabPage();
            this.buttonSaveTemplate = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageBrowser.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLoadTemplate
            // 
            this.buttonLoadTemplate.Location = new System.Drawing.Point(11, 9);
            this.buttonLoadTemplate.Name = "buttonLoadTemplate";
            this.buttonLoadTemplate.Size = new System.Drawing.Size(162, 23);
            this.buttonLoadTemplate.TabIndex = 0;
            this.buttonLoadTemplate.Text = "Загрузить шаблон";
            this.buttonLoadTemplate.UseVisualStyleBackColor = true;
            this.buttonLoadTemplate.Click += new System.EventHandler(this.ButtonLoadTemplate_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.buttonSaveTemplate);
            this.panelTop.Controls.Add(this.buttonLoadTemplate);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(804, 43);
            this.panelTop.TabIndex = 0;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(3, 3);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(790, 439);
            this.webBrowser.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageBrowser);
            this.tabControl1.Controls.Add(this.tabPageElements);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 43);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(804, 471);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPageBrowser
            // 
            this.tabPageBrowser.Controls.Add(this.webBrowser);
            this.tabPageBrowser.Location = new System.Drawing.Point(4, 22);
            this.tabPageBrowser.Name = "tabPageBrowser";
            this.tabPageBrowser.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBrowser.Size = new System.Drawing.Size(796, 445);
            this.tabPageBrowser.TabIndex = 0;
            this.tabPageBrowser.Text = "Внешний вид";
            this.tabPageBrowser.UseVisualStyleBackColor = true;
            // 
            // tabPageElements
            // 
            this.tabPageElements.Location = new System.Drawing.Point(4, 22);
            this.tabPageElements.Name = "tabPageElements";
            this.tabPageElements.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageElements.Size = new System.Drawing.Size(796, 445);
            this.tabPageElements.TabIndex = 1;
            this.tabPageElements.Text = "Элементы";
            this.tabPageElements.UseVisualStyleBackColor = true;
            // 
            // buttonSaveTemplate
            // 
            this.buttonSaveTemplate.Location = new System.Drawing.Point(213, 9);
            this.buttonSaveTemplate.Name = "buttonSaveTemplate";
            this.buttonSaveTemplate.Size = new System.Drawing.Size(162, 23);
            this.buttonSaveTemplate.TabIndex = 1;
            this.buttonSaveTemplate.Text = "Сохранить шаблон";
            this.buttonSaveTemplate.UseVisualStyleBackColor = true;
            this.buttonSaveTemplate.Click += new System.EventHandler(this.ButtonSaveTemplate_Click);
            // 
            // ControlTicketTemplateLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelTop);
            this.Name = "ControlTicketTemplateLoader";
            this.Size = new System.Drawing.Size(804, 514);
            this.panelTop.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageBrowser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonLoadTemplate;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageBrowser;
        private System.Windows.Forms.TabPage tabPageElements;
        private System.Windows.Forms.Button buttonSaveTemplate;
    }
}
