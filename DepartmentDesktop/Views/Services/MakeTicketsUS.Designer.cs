namespace DepartmentDesktop.Controllers
{
    partial class MakeTicketsUS
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageGenerTemplate = new System.Windows.Forms.TabPage();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.buttonMakeTickets = new System.Windows.Forms.Button();
            this.tabControlTemplates = new System.Windows.Forms.TabControl();
            this.buttonLoadTemplate = new System.Windows.Forms.Button();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxGTRemoveQuestions = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.tabPageGenerTemplate.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageGenerTemplate);
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 500);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageGenerTemplate
            // 
            this.tabPageGenerTemplate.Controls.Add(this.progressBar);
            this.tabPageGenerTemplate.Controls.Add(this.buttonMakeTickets);
            this.tabPageGenerTemplate.Controls.Add(this.tabControlTemplates);
            this.tabPageGenerTemplate.Controls.Add(this.buttonLoadTemplate);
            this.tabPageGenerTemplate.Location = new System.Drawing.Point(4, 22);
            this.tabPageGenerTemplate.Name = "tabPageGenerTemplate";
            this.tabPageGenerTemplate.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGenerTemplate.Size = new System.Drawing.Size(792, 474);
            this.tabPageGenerTemplate.TabIndex = 0;
            this.tabPageGenerTemplate.Text = "Генерация по шаблону";
            this.tabPageGenerTemplate.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(311, 6);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(300, 25);
            this.progressBar.TabIndex = 2;
            // 
            // buttonMakeTickets
            // 
            this.buttonMakeTickets.Enabled = false;
            this.buttonMakeTickets.Location = new System.Drawing.Point(142, 6);
            this.buttonMakeTickets.Name = "buttonMakeTickets";
            this.buttonMakeTickets.Size = new System.Drawing.Size(150, 25);
            this.buttonMakeTickets.TabIndex = 1;
            this.buttonMakeTickets.Text = "Сформировать билеты";
            this.buttonMakeTickets.UseVisualStyleBackColor = true;
            this.buttonMakeTickets.Click += new System.EventHandler(this.buttonMakeTickets_Click);
            // 
            // tabControlTemplates
            // 
            this.tabControlTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlTemplates.Location = new System.Drawing.Point(8, 37);
            this.tabControlTemplates.Name = "tabControlTemplates";
            this.tabControlTemplates.SelectedIndex = 0;
            this.tabControlTemplates.Size = new System.Drawing.Size(781, 434);
            this.tabControlTemplates.TabIndex = 3;
            // 
            // buttonLoadTemplate
            // 
            this.buttonLoadTemplate.Location = new System.Drawing.Point(6, 6);
            this.buttonLoadTemplate.Name = "buttonLoadTemplate";
            this.buttonLoadTemplate.Size = new System.Drawing.Size(120, 25);
            this.buttonLoadTemplate.TabIndex = 0;
            this.buttonLoadTemplate.Text = "Загрузить шаблон";
            this.buttonLoadTemplate.UseVisualStyleBackColor = true;
            this.buttonLoadTemplate.Click += new System.EventHandler(this.buttonLoadTemplate_Click);
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.groupBox1);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(792, 474);
            this.tabPageConfig.TabIndex = 1;
            this.tabPageConfig.Text = "Настройки";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxGTRemoveQuestions);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 168);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Генерация по шаблону";
            // 
            // checkBoxGTRemoveQuestions
            // 
            this.checkBoxGTRemoveQuestions.AutoSize = true;
            this.checkBoxGTRemoveQuestions.Checked = true;
            this.checkBoxGTRemoveQuestions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGTRemoveQuestions.Location = new System.Drawing.Point(18, 28);
            this.checkBoxGTRemoveQuestions.Name = "checkBoxGTRemoveQuestions";
            this.checkBoxGTRemoveQuestions.Size = new System.Drawing.Size(318, 17);
            this.checkBoxGTRemoveQuestions.TabIndex = 0;
            this.checkBoxGTRemoveQuestions.Text = "Удалять из списка вопросов вопросы, попавшие в билет";
            this.checkBoxGTRemoveQuestions.UseVisualStyleBackColor = true;
            // 
            // MakeTicketsUS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "MakeTicketsUS";
            this.Size = new System.Drawing.Size(800, 500);
            this.tabControl.ResumeLayout(false);
            this.tabPageGenerTemplate.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageGenerTemplate;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button buttonMakeTickets;
        private System.Windows.Forms.TabControl tabControlTemplates;
        private System.Windows.Forms.Button buttonLoadTemplate;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxGTRemoveQuestions;
    }
}
