namespace LearningProgressControlsAndForms.DisciplineLessonTask
{
    partial class FormDisciplineLessonTask
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
            this.checkBoxIsNecessarily = new System.Windows.Forms.CheckBox();
            this.textBoxMaxBall = new System.Windows.Forms.TextBox();
            this.labelMaxBall = new System.Windows.Forms.Label();
            this.textBoxTask = new System.Windows.Forms.TextBox();
            this.labelTask = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.buttonGetFile = new System.Windows.Forms.Button();
            this.buttonAddFile = new System.Windows.Forms.Button();
            this.checkBoxMaxBall = new System.Windows.Forms.CheckBox();
            this.textBoxOrder = new System.Windows.Forms.TextBox();
            this.labelOrder = new System.Windows.Forms.Label();
            this.textBoxDiscription = new System.Windows.Forms.TextBox();
            this.labelDiscription = new System.Windows.Forms.Label();
            this.comboBoxDisciplineLesson = new System.Windows.Forms.ComboBox();
            this.labelDisciplineLesson = new System.Windows.Forms.Label();
            this.tabPageRecords = new System.Windows.Forms.TabPage();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tabControl);
            this.panelMain.Size = new System.Drawing.Size(884, 435);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(884, 36);
            // 
            // checkBoxIsNecessarily
            // 
            this.checkBoxIsNecessarily.AutoSize = true;
            this.checkBoxIsNecessarily.Location = new System.Drawing.Point(130, 158);
            this.checkBoxIsNecessarily.Name = "checkBoxIsNecessarily";
            this.checkBoxIsNecessarily.Size = new System.Drawing.Size(144, 17);
            this.checkBoxIsNecessarily.TabIndex = 6;
            this.checkBoxIsNecessarily.Text = "Обязательное задание";
            this.checkBoxIsNecessarily.UseVisualStyleBackColor = true;
            // 
            // textBoxMaxBall
            // 
            this.textBoxMaxBall.Enabled = false;
            this.textBoxMaxBall.Location = new System.Drawing.Point(151, 207);
            this.textBoxMaxBall.Name = "textBoxMaxBall";
            this.textBoxMaxBall.Size = new System.Drawing.Size(72, 20);
            this.textBoxMaxBall.TabIndex = 11;
            // 
            // labelMaxBall
            // 
            this.labelMaxBall.AutoSize = true;
            this.labelMaxBall.Location = new System.Drawing.Point(8, 210);
            this.labelMaxBall.Name = "labelMaxBall";
            this.labelMaxBall.Size = new System.Drawing.Size(116, 13);
            this.labelMaxBall.TabIndex = 9;
            this.labelMaxBall.Text = "Максимальный балл:";
            // 
            // textBoxTask
            // 
            this.textBoxTask.Location = new System.Drawing.Point(130, 41);
            this.textBoxTask.Name = "textBoxTask";
            this.textBoxTask.Size = new System.Drawing.Size(210, 20);
            this.textBoxTask.TabIndex = 3;
            // 
            // labelTask
            // 
            this.labelTask.AutoSize = true;
            this.labelTask.Location = new System.Drawing.Point(8, 44);
            this.labelTask.Name = "labelTask";
            this.labelTask.Size = new System.Drawing.Size(57, 13);
            this.labelTask.TabIndex = 2;
            this.labelTask.Text = "Задание*:";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageRecords);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(884, 435);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.buttonGetFile);
            this.tabPageConfig.Controls.Add(this.buttonAddFile);
            this.tabPageConfig.Controls.Add(this.checkBoxMaxBall);
            this.tabPageConfig.Controls.Add(this.textBoxOrder);
            this.tabPageConfig.Controls.Add(this.labelOrder);
            this.tabPageConfig.Controls.Add(this.textBoxDiscription);
            this.tabPageConfig.Controls.Add(this.labelDiscription);
            this.tabPageConfig.Controls.Add(this.comboBoxDisciplineLesson);
            this.tabPageConfig.Controls.Add(this.labelDisciplineLesson);
            this.tabPageConfig.Controls.Add(this.labelTask);
            this.tabPageConfig.Controls.Add(this.checkBoxIsNecessarily);
            this.tabPageConfig.Controls.Add(this.textBoxTask);
            this.tabPageConfig.Controls.Add(this.textBoxMaxBall);
            this.tabPageConfig.Controls.Add(this.labelMaxBall);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(876, 409);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Задание";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // buttonGetFile
            // 
            this.buttonGetFile.Enabled = false;
            this.buttonGetFile.Location = new System.Drawing.Point(198, 249);
            this.buttonGetFile.Name = "buttonGetFile";
            this.buttonGetFile.Size = new System.Drawing.Size(113, 23);
            this.buttonGetFile.TabIndex = 13;
            this.buttonGetFile.Text = "Получить файл";
            // 
            // buttonAddFile
            // 
            this.buttonAddFile.Enabled = false;
            this.buttonAddFile.Location = new System.Drawing.Point(48, 249);
            this.buttonAddFile.Name = "buttonAddFile";
            this.buttonAddFile.Size = new System.Drawing.Size(113, 23);
            this.buttonAddFile.TabIndex = 12;
            this.buttonAddFile.Text = "Добавить файл";
            // 
            // checkBoxMaxBall
            // 
            this.checkBoxMaxBall.AutoSize = true;
            this.checkBoxMaxBall.Location = new System.Drawing.Point(130, 209);
            this.checkBoxMaxBall.Name = "checkBoxMaxBall";
            this.checkBoxMaxBall.Size = new System.Drawing.Size(15, 14);
            this.checkBoxMaxBall.TabIndex = 10;
            this.checkBoxMaxBall.UseVisualStyleBackColor = true;
            this.checkBoxMaxBall.CheckedChanged += new System.EventHandler(this.CheckBoxMaxBall_CheckedChanged);
            // 
            // textBoxOrder
            // 
            this.textBoxOrder.Location = new System.Drawing.Point(130, 181);
            this.textBoxOrder.Name = "textBoxOrder";
            this.textBoxOrder.Size = new System.Drawing.Size(210, 20);
            this.textBoxOrder.TabIndex = 8;
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.Location = new System.Drawing.Point(8, 184);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(113, 13);
            this.labelOrder.TabIndex = 7;
            this.labelOrder.Text = "Порядковый номер*:";
            // 
            // textBoxDiscription
            // 
            this.textBoxDiscription.Location = new System.Drawing.Point(130, 67);
            this.textBoxDiscription.Multiline = true;
            this.textBoxDiscription.Name = "textBoxDiscription";
            this.textBoxDiscription.Size = new System.Drawing.Size(210, 85);
            this.textBoxDiscription.TabIndex = 5;
            // 
            // labelDiscription
            // 
            this.labelDiscription.AutoSize = true;
            this.labelDiscription.Location = new System.Drawing.Point(8, 70);
            this.labelDiscription.Name = "labelDiscription";
            this.labelDiscription.Size = new System.Drawing.Size(64, 13);
            this.labelDiscription.TabIndex = 4;
            this.labelDiscription.Text = "Описание*:";
            // 
            // comboBoxDisciplineLesson
            // 
            this.comboBoxDisciplineLesson.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisciplineLesson.Enabled = false;
            this.comboBoxDisciplineLesson.FormattingEnabled = true;
            this.comboBoxDisciplineLesson.Location = new System.Drawing.Point(130, 14);
            this.comboBoxDisciplineLesson.Name = "comboBoxDisciplineLesson";
            this.comboBoxDisciplineLesson.Size = new System.Drawing.Size(210, 21);
            this.comboBoxDisciplineLesson.TabIndex = 1;
            // 
            // labelDisciplineLesson
            // 
            this.labelDisciplineLesson.AutoSize = true;
            this.labelDisciplineLesson.Location = new System.Drawing.Point(8, 17);
            this.labelDisciplineLesson.Name = "labelDisciplineLesson";
            this.labelDisciplineLesson.Size = new System.Drawing.Size(56, 13);
            this.labelDisciplineLesson.TabIndex = 0;
            this.labelDisciplineLesson.Text = "Занятие*:";
            // 
            // tabPageRecords
            // 
            this.tabPageRecords.Location = new System.Drawing.Point(4, 22);
            this.tabPageRecords.Name = "tabPageRecords";
            this.tabPageRecords.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRecords.Size = new System.Drawing.Size(876, 414);
            this.tabPageRecords.TabIndex = 1;
            this.tabPageRecords.Text = "Варианты";
            this.tabPageRecords.UseVisualStyleBackColor = true;
            // 
            // FormDisciplineLessonTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 471);
            this.Name = "FormDisciplineLessonTask";
            this.Text = "Задание";
            this.Load += new System.EventHandler(this.FormDisciplineLessonTask_Load);
            this.panelMain.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxIsNecessarily;
        private System.Windows.Forms.TextBox textBoxMaxBall;
        private System.Windows.Forms.Label labelMaxBall;
        private System.Windows.Forms.TextBox textBoxTask;
        private System.Windows.Forms.Label labelTask;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TabPage tabPageRecords;
        private System.Windows.Forms.ComboBox comboBoxDisciplineLesson;
        private System.Windows.Forms.Label labelDisciplineLesson;
        private System.Windows.Forms.TextBox textBoxDiscription;
        private System.Windows.Forms.Label labelDiscription;
        private System.Windows.Forms.TextBox textBoxOrder;
        private System.Windows.Forms.Label labelOrder;
        private System.Windows.Forms.CheckBox checkBoxMaxBall;
        private System.Windows.Forms.Button buttonGetFile;
        private System.Windows.Forms.Button buttonAddFile;
    }
}