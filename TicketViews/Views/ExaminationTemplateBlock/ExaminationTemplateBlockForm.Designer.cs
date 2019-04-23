namespace TicketViews.Views.ExaminationTemplateBlock
{
    partial class ExaminationTemplateBlockForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.textBoxQuestionTagInTemplate = new System.Windows.Forms.TextBox();
            this.labelQuestionTagInTemplate = new System.Windows.Forms.Label();
            this.textBoxBlockName = new System.Windows.Forms.TextBox();
            this.labelBlockName = new System.Windows.Forms.Label();
            this.numericUpDownCountQuestionInTicket = new System.Windows.Forms.NumericUpDown();
            this.examinationTemplateElement = new TicketViews.Views.ExaminationTemplate.ExaminationTemplateElement();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.labelCountQuestionInTicket = new System.Windows.Forms.Label();
            this.labelExaminationTemplate = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.tabPageRecords = new System.Windows.Forms.TabPage();
            this.checkBoxIsCombine = new System.Windows.Forms.CheckBox();
            this.labelCobineTegs = new System.Windows.Forms.Label();
            this.textBoxCombineBlocks = new System.Windows.Forms.TextBox();
            this.labelIsCombine = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCountQuestionInTicket)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageRecords);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(834, 501);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.labelIsCombine);
            this.tabPageConfig.Controls.Add(this.textBoxCombineBlocks);
            this.tabPageConfig.Controls.Add(this.labelCobineTegs);
            this.tabPageConfig.Controls.Add(this.checkBoxIsCombine);
            this.tabPageConfig.Controls.Add(this.textBoxQuestionTagInTemplate);
            this.tabPageConfig.Controls.Add(this.labelQuestionTagInTemplate);
            this.tabPageConfig.Controls.Add(this.textBoxBlockName);
            this.tabPageConfig.Controls.Add(this.labelBlockName);
            this.tabPageConfig.Controls.Add(this.numericUpDownCountQuestionInTicket);
            this.tabPageConfig.Controls.Add(this.examinationTemplateElement);
            this.tabPageConfig.Controls.Add(this.buttonSaveAndClose);
            this.tabPageConfig.Controls.Add(this.labelCountQuestionInTicket);
            this.tabPageConfig.Controls.Add(this.labelExaminationTemplate);
            this.tabPageConfig.Controls.Add(this.buttonClose);
            this.tabPageConfig.Controls.Add(this.buttonSave);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(826, 475);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Блок экзамена";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // textBoxQuestionTagInTemplate
            // 
            this.textBoxQuestionTagInTemplate.Location = new System.Drawing.Point(222, 63);
            this.textBoxQuestionTagInTemplate.Name = "textBoxQuestionTagInTemplate";
            this.textBoxQuestionTagInTemplate.Size = new System.Drawing.Size(300, 20);
            this.textBoxQuestionTagInTemplate.TabIndex = 5;
            // 
            // labelQuestionTagInTemplate
            // 
            this.labelQuestionTagInTemplate.AutoSize = true;
            this.labelQuestionTagInTemplate.Location = new System.Drawing.Point(12, 66);
            this.labelQuestionTagInTemplate.Name = "labelQuestionTagInTemplate";
            this.labelQuestionTagInTemplate.Size = new System.Drawing.Size(133, 13);
            this.labelQuestionTagInTemplate.TabIndex = 4;
            this.labelQuestionTagInTemplate.Text = "Тег вопроса в шаблоне*:";
            // 
            // textBoxBlockName
            // 
            this.textBoxBlockName.Location = new System.Drawing.Point(222, 37);
            this.textBoxBlockName.Name = "textBoxBlockName";
            this.textBoxBlockName.Size = new System.Drawing.Size(300, 20);
            this.textBoxBlockName.TabIndex = 3;
            // 
            // labelBlockName
            // 
            this.labelBlockName.AutoSize = true;
            this.labelBlockName.Location = new System.Drawing.Point(12, 40);
            this.labelBlockName.Name = "labelBlockName";
            this.labelBlockName.Size = new System.Drawing.Size(97, 13);
            this.labelBlockName.TabIndex = 2;
            this.labelBlockName.Text = "Название блока*:";
            // 
            // numericUpDownCountQuestionInTicket
            // 
            this.numericUpDownCountQuestionInTicket.Location = new System.Drawing.Point(222, 89);
            this.numericUpDownCountQuestionInTicket.Name = "numericUpDownCountQuestionInTicket";
            this.numericUpDownCountQuestionInTicket.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownCountQuestionInTicket.TabIndex = 7;
            // 
            // examinationTemplateElement
            // 
            this.examinationTemplateElement.DisciplineId = null;
            this.examinationTemplateElement.Id = null;
            this.examinationTemplateElement.Location = new System.Drawing.Point(222, 11);
            this.examinationTemplateElement.Name = "examinationTemplateElement";
            this.examinationTemplateElement.Size = new System.Drawing.Size(300, 20);
            this.examinationTemplateElement.TabIndex = 1;
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(201, 200);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(141, 23);
            this.buttonSaveAndClose.TabIndex = 13;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.ButtonSaveAndClose_Click);
            // 
            // labelCountQuestionInTicket
            // 
            this.labelCountQuestionInTicket.AutoSize = true;
            this.labelCountQuestionInTicket.Location = new System.Drawing.Point(12, 91);
            this.labelCountQuestionInTicket.Name = "labelCountQuestionInTicket";
            this.labelCountQuestionInTicket.Size = new System.Drawing.Size(204, 13);
            this.labelCountQuestionInTicket.TabIndex = 6;
            this.labelCountQuestionInTicket.Text = "Количество вопросов блока в билете*:";
            // 
            // labelExaminationTemplate
            // 
            this.labelExaminationTemplate.AutoSize = true;
            this.labelExaminationTemplate.Location = new System.Drawing.Point(12, 13);
            this.labelExaminationTemplate.Name = "labelExaminationTemplate";
            this.labelExaminationTemplate.Size = new System.Drawing.Size(59, 13);
            this.labelExaminationTemplate.TabIndex = 0;
            this.labelExaminationTemplate.Text = "Экзамен*:";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(348, 200);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 14;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(120, 200);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // tabPageRecords
            // 
            this.tabPageRecords.Location = new System.Drawing.Point(4, 22);
            this.tabPageRecords.Name = "tabPageRecords";
            this.tabPageRecords.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRecords.Size = new System.Drawing.Size(826, 475);
            this.tabPageRecords.TabIndex = 1;
            this.tabPageRecords.Text = "Вопросы";
            this.tabPageRecords.UseVisualStyleBackColor = true;
            // 
            // checkBoxIsCombine
            // 
            this.checkBoxIsCombine.AutoSize = true;
            this.checkBoxIsCombine.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxIsCombine.Location = new System.Drawing.Point(222, 115);
            this.checkBoxIsCombine.Name = "checkBoxIsCombine";
            this.checkBoxIsCombine.Size = new System.Drawing.Size(15, 14);
            this.checkBoxIsCombine.TabIndex = 9;
            this.checkBoxIsCombine.UseVisualStyleBackColor = true;
            // 
            // labelCobineTegs
            // 
            this.labelCobineTegs.AutoSize = true;
            this.labelCobineTegs.Location = new System.Drawing.Point(12, 141);
            this.labelCobineTegs.Name = "labelCobineTegs";
            this.labelCobineTegs.Size = new System.Drawing.Size(120, 13);
            this.labelCobineTegs.TabIndex = 10;
            this.labelCobineTegs.Text = "Блоки в объединении:";
            // 
            // textBoxCombineBlocks
            // 
            this.textBoxCombineBlocks.Location = new System.Drawing.Point(222, 138);
            this.textBoxCombineBlocks.Name = "textBoxCombineBlocks";
            this.textBoxCombineBlocks.Size = new System.Drawing.Size(300, 20);
            this.textBoxCombineBlocks.TabIndex = 11;
            // 
            // labelIsCombine
            // 
            this.labelIsCombine.AutoSize = true;
            this.labelIsCombine.Location = new System.Drawing.Point(12, 115);
            this.labelIsCombine.Name = "labelIsCombine";
            this.labelIsCombine.Size = new System.Drawing.Size(87, 13);
            this.labelIsCombine.TabIndex = 8;
            this.labelIsCombine.Text = "Объединенный:";
            // 
            // ExaminationTemplateBlockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 501);
            this.Controls.Add(this.tabControl);
            this.Name = "ExaminationTemplateBlockForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Блок экзамена";
            this.Load += new System.EventHandler(this.ExaminationTemplateBlockForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCountQuestionInTicket)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.NumericUpDown numericUpDownCountQuestionInTicket;
        private ExaminationTemplate.ExaminationTemplateElement examinationTemplateElement;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Label labelCountQuestionInTicket;
        private System.Windows.Forms.Label labelExaminationTemplate;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TabPage tabPageRecords;
        private System.Windows.Forms.TextBox textBoxBlockName;
        private System.Windows.Forms.Label labelBlockName;
        private System.Windows.Forms.TextBox textBoxQuestionTagInTemplate;
        private System.Windows.Forms.Label labelQuestionTagInTemplate;
        private System.Windows.Forms.CheckBox checkBoxIsCombine;
        private System.Windows.Forms.Label labelIsCombine;
        private System.Windows.Forms.TextBox textBoxCombineBlocks;
        private System.Windows.Forms.Label labelCobineTegs;
    }
}