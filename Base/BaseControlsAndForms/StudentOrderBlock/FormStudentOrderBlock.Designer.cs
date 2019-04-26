namespace BaseControlsAndForms.StudentOrderBlock
{
    partial class FormStudentOrderBlock
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.labelStudentOrderType = new System.Windows.Forms.Label();
            this.comboBoxStudentOrderType = new System.Windows.Forms.ComboBox();
            this.labelStudentOrder = new System.Windows.Forms.Label();
            this.comboBoxStudentOrder = new System.Windows.Forms.ComboBox();
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
            this.panelMain.Size = new System.Drawing.Size(854, 425);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(854, 36);
            // 
            // labelEducationDirection
            // 
            this.labelEducationDirection.AutoSize = true;
            this.labelEducationDirection.Location = new System.Drawing.Point(17, 66);
            this.labelEducationDirection.Name = "labelEducationDirection";
            this.labelEducationDirection.Size = new System.Drawing.Size(82, 13);
            this.labelEducationDirection.TabIndex = 4;
            this.labelEducationDirection.Text = "Направление*:";
            // 
            // comboBoxEducationDirection
            // 
            this.comboBoxEducationDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEducationDirection.FormattingEnabled = true;
            this.comboBoxEducationDirection.Location = new System.Drawing.Point(105, 63);
            this.comboBoxEducationDirection.Name = "comboBoxEducationDirection";
            this.comboBoxEducationDirection.Size = new System.Drawing.Size(300, 21);
            this.comboBoxEducationDirection.TabIndex = 5;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageRecords);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(854, 425);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.labelStudentOrderType);
            this.tabPageConfig.Controls.Add(this.comboBoxStudentOrderType);
            this.tabPageConfig.Controls.Add(this.labelStudentOrder);
            this.tabPageConfig.Controls.Add(this.comboBoxStudentOrder);
            this.tabPageConfig.Controls.Add(this.labelEducationDirection);
            this.tabPageConfig.Controls.Add(this.comboBoxEducationDirection);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(846, 399);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Основные данные";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // labelStudentOrderType
            // 
            this.labelStudentOrderType.AutoSize = true;
            this.labelStudentOrderType.Location = new System.Drawing.Point(17, 39);
            this.labelStudentOrderType.Name = "labelStudentOrderType";
            this.labelStudentOrderType.Size = new System.Drawing.Size(78, 13);
            this.labelStudentOrderType.TabIndex = 2;
            this.labelStudentOrderType.Text = "Тип приказа*:";
            // 
            // comboBoxStudentOrderType
            // 
            this.comboBoxStudentOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudentOrderType.FormattingEnabled = true;
            this.comboBoxStudentOrderType.Location = new System.Drawing.Point(105, 36);
            this.comboBoxStudentOrderType.Name = "comboBoxStudentOrderType";
            this.comboBoxStudentOrderType.Size = new System.Drawing.Size(300, 21);
            this.comboBoxStudentOrderType.TabIndex = 3;
            // 
            // labelStudentOrder
            // 
            this.labelStudentOrder.AutoSize = true;
            this.labelStudentOrder.Location = new System.Drawing.Point(17, 12);
            this.labelStudentOrder.Name = "labelStudentOrder";
            this.labelStudentOrder.Size = new System.Drawing.Size(52, 13);
            this.labelStudentOrder.TabIndex = 0;
            this.labelStudentOrder.Text = "Приказ*:";
            // 
            // comboBoxStudentOrder
            // 
            this.comboBoxStudentOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudentOrder.FormattingEnabled = true;
            this.comboBoxStudentOrder.Location = new System.Drawing.Point(105, 9);
            this.comboBoxStudentOrder.Name = "comboBoxStudentOrder";
            this.comboBoxStudentOrder.Size = new System.Drawing.Size(300, 21);
            this.comboBoxStudentOrder.TabIndex = 1;
            // 
            // tabPageRecords
            // 
            this.tabPageRecords.Location = new System.Drawing.Point(4, 22);
            this.tabPageRecords.Name = "tabPageRecords";
            this.tabPageRecords.Size = new System.Drawing.Size(311, 148);
            this.tabPageRecords.TabIndex = 0;
            this.tabPageRecords.Text = "Студенты";
            this.tabPageRecords.UseVisualStyleBackColor = true;
            // 
            // FormStudentOrderBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 461);
            this.Name = "FormStudentOrderBlock";
            this.Text = "Блок приказа";
            this.Load += new System.EventHandler(this.FormStudentOrderBlock_Load);
            this.panelMain.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelEducationDirection;
        private System.Windows.Forms.ComboBox comboBoxEducationDirection;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TabPage tabPageRecords;
        private System.Windows.Forms.Label labelStudentOrder;
        private System.Windows.Forms.ComboBox comboBoxStudentOrder;
        private System.Windows.Forms.Label labelStudentOrderType;
        private System.Windows.Forms.ComboBox comboBoxStudentOrderType;
    }
}