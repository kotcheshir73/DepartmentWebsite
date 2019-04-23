namespace AcademicYearControlsAndForms.DisciplineTimeDistribution
{
    partial class FormDisciplineTimeDistribution
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
            this.labelAcademicPlanRecord = new System.Windows.Forms.Label();
            this.textBoxCommentWishesOfTeacher = new System.Windows.Forms.TextBox();
            this.comboBoxAcademicPlanRecord = new System.Windows.Forms.ComboBox();
            this.labelCommentWishesOfTeacher = new System.Windows.Forms.Label();
            this.labelStudentGroup = new System.Windows.Forms.Label();
            this.comboBoxStudentGroup = new System.Windows.Forms.ComboBox();
            this.labelComment = new System.Windows.Forms.Label();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.tabPageRecords = new System.Windows.Forms.TabPage();
            this.tabPageClassrooms = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(375, 496);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(522, 496);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(294, 496);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageRecords);
            this.tabControl.Controls.Add(this.tabPageClassrooms);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(884, 490);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.labelAcademicPlanRecord);
            this.tabPageConfig.Controls.Add(this.textBoxCommentWishesOfTeacher);
            this.tabPageConfig.Controls.Add(this.comboBoxAcademicPlanRecord);
            this.tabPageConfig.Controls.Add(this.labelCommentWishesOfTeacher);
            this.tabPageConfig.Controls.Add(this.labelStudentGroup);
            this.tabPageConfig.Controls.Add(this.comboBoxStudentGroup);
            this.tabPageConfig.Controls.Add(this.labelComment);
            this.tabPageConfig.Controls.Add(this.textBoxComment);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(876, 464);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Расчасовка";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // labelAcademicPlanRecord
            // 
            this.labelAcademicPlanRecord.AutoSize = true;
            this.labelAcademicPlanRecord.Location = new System.Drawing.Point(6, 14);
            this.labelAcademicPlanRecord.Name = "labelAcademicPlanRecord";
            this.labelAcademicPlanRecord.Size = new System.Drawing.Size(132, 13);
            this.labelAcademicPlanRecord.TabIndex = 0;
            this.labelAcademicPlanRecord.Text = "Запись учебного плана*:";
            // 
            // textBoxCommentWishesOfTeacher
            // 
            this.textBoxCommentWishesOfTeacher.Location = new System.Drawing.Point(160, 91);
            this.textBoxCommentWishesOfTeacher.Name = "textBoxCommentWishesOfTeacher";
            this.textBoxCommentWishesOfTeacher.Size = new System.Drawing.Size(444, 20);
            this.textBoxCommentWishesOfTeacher.TabIndex = 7;
            // 
            // comboBoxAcademicPlanRecord
            // 
            this.comboBoxAcademicPlanRecord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicPlanRecord.Enabled = false;
            this.comboBoxAcademicPlanRecord.FormattingEnabled = true;
            this.comboBoxAcademicPlanRecord.Location = new System.Drawing.Point(160, 11);
            this.comboBoxAcademicPlanRecord.Name = "comboBoxAcademicPlanRecord";
            this.comboBoxAcademicPlanRecord.Size = new System.Drawing.Size(220, 21);
            this.comboBoxAcademicPlanRecord.TabIndex = 1;
            // 
            // labelCommentWishesOfTeacher
            // 
            this.labelCommentWishesOfTeacher.AutoSize = true;
            this.labelCommentWishesOfTeacher.Location = new System.Drawing.Point(6, 94);
            this.labelCommentWishesOfTeacher.Name = "labelCommentWishesOfTeacher";
            this.labelCommentWishesOfTeacher.Size = new System.Drawing.Size(148, 13);
            this.labelCommentWishesOfTeacher.TabIndex = 6;
            this.labelCommentWishesOfTeacher.Text = "Пожелания преподавателя:";
            // 
            // labelStudentGroup
            // 
            this.labelStudentGroup.AutoSize = true;
            this.labelStudentGroup.Location = new System.Drawing.Point(6, 41);
            this.labelStudentGroup.Name = "labelStudentGroup";
            this.labelStudentGroup.Size = new System.Drawing.Size(49, 13);
            this.labelStudentGroup.TabIndex = 2;
            this.labelStudentGroup.Text = "Группа*:";
            // 
            // comboBoxStudentGroup
            // 
            this.comboBoxStudentGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudentGroup.FormattingEnabled = true;
            this.comboBoxStudentGroup.Location = new System.Drawing.Point(160, 38);
            this.comboBoxStudentGroup.Name = "comboBoxStudentGroup";
            this.comboBoxStudentGroup.Size = new System.Drawing.Size(220, 21);
            this.comboBoxStudentGroup.TabIndex = 3;
            // 
            // labelComment
            // 
            this.labelComment.AutoSize = true;
            this.labelComment.Location = new System.Drawing.Point(6, 68);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(80, 13);
            this.labelComment.TabIndex = 4;
            this.labelComment.Text = "Комментарий:";
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(160, 65);
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(444, 20);
            this.textBoxComment.TabIndex = 5;
            // 
            // tabPageRecords
            // 
            this.tabPageRecords.Location = new System.Drawing.Point(4, 22);
            this.tabPageRecords.Name = "tabPageRecords";
            this.tabPageRecords.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRecords.Size = new System.Drawing.Size(876, 464);
            this.tabPageRecords.TabIndex = 1;
            this.tabPageRecords.Text = "Часы";
            this.tabPageRecords.UseVisualStyleBackColor = true;
            // 
            // tabPageClassrooms
            // 
            this.tabPageClassrooms.Location = new System.Drawing.Point(4, 22);
            this.tabPageClassrooms.Name = "tabPageClassrooms";
            this.tabPageClassrooms.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageClassrooms.Size = new System.Drawing.Size(876, 464);
            this.tabPageClassrooms.TabIndex = 2;
            this.tabPageClassrooms.Text = "Аудитории";
            this.tabPageClassrooms.UseVisualStyleBackColor = true;
            // 
            // FormDisciplineTimeDistribution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 531);
            this.Controls.Add(this.tabControl);
            this.Name = "FormDisciplineTimeDistribution";
            this.Text = "Расчасовка";
            this.Load += new System.EventHandler(this.FormDisciplineTimeDistribution_Load);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.buttonSaveAndClose, 0);
            this.Controls.SetChildIndex(this.tabControl, 0);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.Label labelAcademicPlanRecord;
        private System.Windows.Forms.TextBox textBoxCommentWishesOfTeacher;
        private System.Windows.Forms.ComboBox comboBoxAcademicPlanRecord;
        private System.Windows.Forms.Label labelCommentWishesOfTeacher;
        private System.Windows.Forms.Label labelStudentGroup;
        private System.Windows.Forms.ComboBox comboBoxStudentGroup;
        private System.Windows.Forms.Label labelComment;
        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.TabPage tabPageRecords;
        private System.Windows.Forms.TabPage tabPageClassrooms;
    }
}