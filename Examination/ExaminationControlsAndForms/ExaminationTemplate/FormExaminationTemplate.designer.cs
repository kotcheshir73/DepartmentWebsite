namespace ExaminationControlsAndForms.ExaminationTemplate
{
    partial class FormExaminationTemplate
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.textBoxExaminationTemplateName = new System.Windows.Forms.TextBox();
            this.labelExaminationTemplateName = new System.Windows.Forms.Label();
            this.labelSemester = new System.Windows.Forms.Label();
            this.comboBoxSemester = new System.Windows.Forms.ComboBox();
            this.comboBoxDiscipline = new System.Windows.Forms.ComboBox();
            this.labelDiscipline = new System.Windows.Forms.Label();
            this.comboBoxEducationDirection = new System.Windows.Forms.ComboBox();
            this.labelEducationDirection = new System.Windows.Forms.Label();
            this.tabPageRecords = new System.Windows.Forms.TabPage();
            this.tabPageTickets = new System.Windows.Forms.TabPage();
            this.tabPageTicketTemplate = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(186, 474);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(333, 474);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(105, 474);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageRecords);
            this.tabControl.Controls.Add(this.tabPageTickets);
            this.tabControl.Controls.Add(this.tabPageTicketTemplate);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(834, 472);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.textBoxExaminationTemplateName);
            this.tabPageConfig.Controls.Add(this.labelExaminationTemplateName);
            this.tabPageConfig.Controls.Add(this.labelSemester);
            this.tabPageConfig.Controls.Add(this.comboBoxSemester);
            this.tabPageConfig.Controls.Add(this.comboBoxDiscipline);
            this.tabPageConfig.Controls.Add(this.labelDiscipline);
            this.tabPageConfig.Controls.Add(this.comboBoxEducationDirection);
            this.tabPageConfig.Controls.Add(this.labelEducationDirection);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(826, 446);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Экзамен";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // textBoxExaminationTemplateName
            // 
            this.textBoxExaminationTemplateName.Enabled = false;
            this.textBoxExaminationTemplateName.Location = new System.Drawing.Point(127, 93);
            this.textBoxExaminationTemplateName.Name = "textBoxExaminationTemplateName";
            this.textBoxExaminationTemplateName.Size = new System.Drawing.Size(300, 20);
            this.textBoxExaminationTemplateName.TabIndex = 7;
            // 
            // labelExaminationTemplateName
            // 
            this.labelExaminationTemplateName.AutoSize = true;
            this.labelExaminationTemplateName.Location = new System.Drawing.Point(8, 96);
            this.labelExaminationTemplateName.Name = "labelExaminationTemplateName";
            this.labelExaminationTemplateName.Size = new System.Drawing.Size(113, 13);
            this.labelExaminationTemplateName.TabIndex = 6;
            this.labelExaminationTemplateName.Text = "Название экзамена:";
            // 
            // labelSemester
            // 
            this.labelSemester.AutoSize = true;
            this.labelSemester.Location = new System.Drawing.Point(8, 68);
            this.labelSemester.Name = "labelSemester";
            this.labelSemester.Size = new System.Drawing.Size(54, 13);
            this.labelSemester.TabIndex = 4;
            this.labelSemester.Text = "Семестр:";
            // 
            // comboBoxSemester
            // 
            this.comboBoxSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSemester.FormattingEnabled = true;
            this.comboBoxSemester.Location = new System.Drawing.Point(127, 66);
            this.comboBoxSemester.Name = "comboBoxSemester";
            this.comboBoxSemester.Size = new System.Drawing.Size(300, 21);
            this.comboBoxSemester.TabIndex = 5;
            this.comboBoxSemester.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
            // 
            // comboBoxDiscipline
            // 
            this.comboBoxDiscipline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiscipline.FormattingEnabled = true;
            this.comboBoxDiscipline.Location = new System.Drawing.Point(127, 12);
            this.comboBoxDiscipline.Name = "comboBoxDiscipline";
            this.comboBoxDiscipline.Size = new System.Drawing.Size(300, 21);
            this.comboBoxDiscipline.TabIndex = 1;
            this.comboBoxDiscipline.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
            // 
            // labelDiscipline
            // 
            this.labelDiscipline.AutoSize = true;
            this.labelDiscipline.Location = new System.Drawing.Point(8, 15);
            this.labelDiscipline.Name = "labelDiscipline";
            this.labelDiscipline.Size = new System.Drawing.Size(77, 13);
            this.labelDiscipline.TabIndex = 0;
            this.labelDiscipline.Text = "Дисциплина*:";
            // 
            // comboBoxEducationDirection
            // 
            this.comboBoxEducationDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEducationDirection.FormattingEnabled = true;
            this.comboBoxEducationDirection.Location = new System.Drawing.Point(127, 39);
            this.comboBoxEducationDirection.Name = "comboBoxEducationDirection";
            this.comboBoxEducationDirection.Size = new System.Drawing.Size(300, 21);
            this.comboBoxEducationDirection.TabIndex = 3;
            this.comboBoxEducationDirection.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
            // 
            // labelEducationDirection
            // 
            this.labelEducationDirection.AutoSize = true;
            this.labelEducationDirection.Location = new System.Drawing.Point(8, 42);
            this.labelEducationDirection.Name = "labelEducationDirection";
            this.labelEducationDirection.Size = new System.Drawing.Size(78, 13);
            this.labelEducationDirection.TabIndex = 2;
            this.labelEducationDirection.Text = "Направление:";
            // 
            // tabPageRecords
            // 
            this.tabPageRecords.Location = new System.Drawing.Point(4, 22);
            this.tabPageRecords.Name = "tabPageRecords";
            this.tabPageRecords.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRecords.Size = new System.Drawing.Size(826, 446);
            this.tabPageRecords.TabIndex = 1;
            this.tabPageRecords.Text = "Блоки вопросов";
            this.tabPageRecords.UseVisualStyleBackColor = true;
            // 
            // tabPageTickets
            // 
            this.tabPageTickets.Location = new System.Drawing.Point(4, 22);
            this.tabPageTickets.Name = "tabPageTickets";
            this.tabPageTickets.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTickets.Size = new System.Drawing.Size(826, 446);
            this.tabPageTickets.TabIndex = 2;
            this.tabPageTickets.Text = "Билеты";
            this.tabPageTickets.UseVisualStyleBackColor = true;
            // 
            // tabPageTicketTemplate
            // 
            this.tabPageTicketTemplate.Location = new System.Drawing.Point(4, 22);
            this.tabPageTicketTemplate.Name = "tabPageTicketTemplate";
            this.tabPageTicketTemplate.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTicketTemplate.Size = new System.Drawing.Size(826, 446);
            this.tabPageTicketTemplate.TabIndex = 3;
            this.tabPageTicketTemplate.Text = "Шаблон билета";
            this.tabPageTicketTemplate.UseVisualStyleBackColor = true;
            // 
            // ExaminationTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 501);
            this.Controls.Add(this.tabControl);
            this.Name = "ExaminationTemplateForm";
            this.Text = "Экзамен";
            this.Load += new System.EventHandler(this.FormExaminationTemplate_Load);
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

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TabPage tabPageRecords;
        private System.Windows.Forms.ComboBox comboBoxEducationDirection;
        private System.Windows.Forms.Label labelEducationDirection;
        private System.Windows.Forms.ComboBox comboBoxDiscipline;
        private System.Windows.Forms.Label labelDiscipline;
        private System.Windows.Forms.Label labelSemester;
        private System.Windows.Forms.ComboBox comboBoxSemester;
        private System.Windows.Forms.TabPage tabPageTickets;
        private System.Windows.Forms.TabPage tabPageTicketTemplate;
        private System.Windows.Forms.TextBox textBoxExaminationTemplateName;
        private System.Windows.Forms.Label labelExaminationTemplateName;
    }
}