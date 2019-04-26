namespace BaseControlsAndForms.StudentGroup
{
    partial class FormStudentGroup
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
            this.labelEducationDirection = new System.Windows.Forms.Label();
            this.comboBoxEducationDirection = new System.Windows.Forms.ComboBox();
            this.labelGroupName = new System.Windows.Forms.Label();
            this.textBoxGroupName = new System.Windows.Forms.TextBox();
            this.labelKurs = new System.Windows.Forms.Label();
            this.textBoxKurs = new System.Windows.Forms.TextBox();
            this.labelSteward = new System.Windows.Forms.Label();
            this.comboBoxCurator = new System.Windows.Forms.ComboBox();
            this.labelCurator = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageMainData = new System.Windows.Forms.TabPage();
            this.textBoxSteward = new System.Windows.Forms.TextBox();
            this.tabPageStudents = new System.Windows.Forms.TabPage();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageMainData.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tabControl);
            this.panelMain.Size = new System.Drawing.Size(884, 436);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(884, 36);
            // 
            // labelEducationDirection
            // 
            this.labelEducationDirection.AutoSize = true;
            this.labelEducationDirection.Location = new System.Drawing.Point(6, 13);
            this.labelEducationDirection.Name = "labelEducationDirection";
            this.labelEducationDirection.Size = new System.Drawing.Size(82, 13);
            this.labelEducationDirection.TabIndex = 0;
            this.labelEducationDirection.Text = "Направление*:";
            // 
            // comboBoxEducationDirection
            // 
            this.comboBoxEducationDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEducationDirection.FormattingEnabled = true;
            this.comboBoxEducationDirection.Location = new System.Drawing.Point(94, 10);
            this.comboBoxEducationDirection.Name = "comboBoxEducationDirection";
            this.comboBoxEducationDirection.Size = new System.Drawing.Size(300, 21);
            this.comboBoxEducationDirection.TabIndex = 1;
            // 
            // labelGroupName
            // 
            this.labelGroupName.AutoSize = true;
            this.labelGroupName.Location = new System.Drawing.Point(6, 40);
            this.labelGroupName.Name = "labelGroupName";
            this.labelGroupName.Size = new System.Drawing.Size(103, 13);
            this.labelGroupName.TabIndex = 2;
            this.labelGroupName.Text = "Название группы*:";
            // 
            // textBoxGroupName
            // 
            this.textBoxGroupName.Location = new System.Drawing.Point(119, 37);
            this.textBoxGroupName.MaxLength = 20;
            this.textBoxGroupName.Name = "textBoxGroupName";
            this.textBoxGroupName.Size = new System.Drawing.Size(133, 20);
            this.textBoxGroupName.TabIndex = 3;
            // 
            // labelKurs
            // 
            this.labelKurs.AutoSize = true;
            this.labelKurs.Location = new System.Drawing.Point(288, 40);
            this.labelKurs.Name = "labelKurs";
            this.labelKurs.Size = new System.Drawing.Size(38, 13);
            this.labelKurs.TabIndex = 4;
            this.labelKurs.Text = "Курс*:";
            // 
            // textBoxKurs
            // 
            this.textBoxKurs.Location = new System.Drawing.Point(332, 37);
            this.textBoxKurs.MaxLength = 3;
            this.textBoxKurs.Name = "textBoxKurs";
            this.textBoxKurs.Size = new System.Drawing.Size(62, 20);
            this.textBoxKurs.TabIndex = 5;
            this.textBoxKurs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelSteward
            // 
            this.labelSteward.AutoSize = true;
            this.labelSteward.Location = new System.Drawing.Point(6, 66);
            this.labelSteward.Name = "labelSteward";
            this.labelSteward.Size = new System.Drawing.Size(57, 13);
            this.labelSteward.TabIndex = 6;
            this.labelSteward.Text = "Староста:";
            // 
            // comboBoxCurator
            // 
            this.comboBoxCurator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCurator.FormattingEnabled = true;
            this.comboBoxCurator.Location = new System.Drawing.Point(94, 90);
            this.comboBoxCurator.Name = "comboBoxCurator";
            this.comboBoxCurator.Size = new System.Drawing.Size(300, 21);
            this.comboBoxCurator.TabIndex = 9;
            // 
            // labelCurator
            // 
            this.labelCurator.AutoSize = true;
            this.labelCurator.Location = new System.Drawing.Point(6, 93);
            this.labelCurator.Name = "labelCurator";
            this.labelCurator.Size = new System.Drawing.Size(51, 13);
            this.labelCurator.TabIndex = 8;
            this.labelCurator.Text = "Куратор:";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageMainData);
            this.tabControl.Controls.Add(this.tabPageStudents);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(884, 436);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageMainData
            // 
            this.tabPageMainData.Controls.Add(this.textBoxSteward);
            this.tabPageMainData.Controls.Add(this.labelEducationDirection);
            this.tabPageMainData.Controls.Add(this.comboBoxCurator);
            this.tabPageMainData.Controls.Add(this.comboBoxEducationDirection);
            this.tabPageMainData.Controls.Add(this.labelCurator);
            this.tabPageMainData.Controls.Add(this.labelGroupName);
            this.tabPageMainData.Controls.Add(this.textBoxGroupName);
            this.tabPageMainData.Controls.Add(this.labelSteward);
            this.tabPageMainData.Controls.Add(this.labelKurs);
            this.tabPageMainData.Controls.Add(this.textBoxKurs);
            this.tabPageMainData.Location = new System.Drawing.Point(4, 22);
            this.tabPageMainData.Name = "tabPageMainData";
            this.tabPageMainData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMainData.Size = new System.Drawing.Size(876, 410);
            this.tabPageMainData.TabIndex = 0;
            this.tabPageMainData.Text = "Основные данные";
            this.tabPageMainData.UseVisualStyleBackColor = true;
            // 
            // textBoxSteward
            // 
            this.textBoxSteward.Location = new System.Drawing.Point(94, 63);
            this.textBoxSteward.Name = "textBoxSteward";
            this.textBoxSteward.Size = new System.Drawing.Size(300, 20);
            this.textBoxSteward.TabIndex = 7;
            // 
            // tabPageStudents
            // 
            this.tabPageStudents.Location = new System.Drawing.Point(4, 22);
            this.tabPageStudents.Name = "tabPageStudents";
            this.tabPageStudents.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStudents.Size = new System.Drawing.Size(876, 410);
            this.tabPageStudents.TabIndex = 1;
            this.tabPageStudents.Text = "Студенты";
            this.tabPageStudents.UseVisualStyleBackColor = true;
            // 
            // FormStudentGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 472);
            this.Name = "FormStudentGroup";
            this.Text = "Группа";
            this.Load += new System.EventHandler(this.FormStudentGroup_Load);
            this.panelMain.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageMainData.ResumeLayout(false);
            this.tabPageMainData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelEducationDirection;
        private System.Windows.Forms.ComboBox comboBoxEducationDirection;
        private System.Windows.Forms.Label labelGroupName;
        private System.Windows.Forms.TextBox textBoxGroupName;
        private System.Windows.Forms.Label labelKurs;
        private System.Windows.Forms.TextBox textBoxKurs;
        private System.Windows.Forms.Label labelSteward;
        private System.Windows.Forms.ComboBox comboBoxCurator;
        private System.Windows.Forms.Label labelCurator;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageMainData;
        private System.Windows.Forms.TabPage tabPageStudents;
        private System.Windows.Forms.TextBox textBoxSteward;
    }
}