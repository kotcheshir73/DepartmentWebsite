namespace BaseControlsAndForms.StudentOrder
{
    partial class FormStudentOrder
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
            this.dateTimePickerOrderDate = new System.Windows.Forms.DateTimePicker();
            this.labelOrderDate = new System.Windows.Forms.Label();
            this.textBoxOrderNumber = new System.Windows.Forms.TextBox();
            this.labelOrderNumber = new System.Windows.Forms.Label();
            this.labelStudentOrderType = new System.Windows.Forms.Label();
            this.comboBoxStudentOrderType = new System.Windows.Forms.ComboBox();
            this.tabPageRecords = new System.Windows.Forms.TabPage();
            this.tabPageStudents = new System.Windows.Forms.TabPage();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tabControl);
            this.panelMain.Size = new System.Drawing.Size(784, 325);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(784, 36);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageRecords);
            this.tabControl.Controls.Add(this.tabPageStudents);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(784, 325);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.dateTimePickerOrderDate);
            this.tabPageConfig.Controls.Add(this.labelOrderDate);
            this.tabPageConfig.Controls.Add(this.textBoxOrderNumber);
            this.tabPageConfig.Controls.Add(this.labelOrderNumber);
            this.tabPageConfig.Controls.Add(this.labelStudentOrderType);
            this.tabPageConfig.Controls.Add(this.comboBoxStudentOrderType);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(776, 299);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Приказ";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerOrderDate
            // 
            this.dateTimePickerOrderDate.Location = new System.Drawing.Point(116, 37);
            this.dateTimePickerOrderDate.Name = "dateTimePickerOrderDate";
            this.dateTimePickerOrderDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerOrderDate.TabIndex = 3;
            // 
            // labelOrderDate
            // 
            this.labelOrderDate.AutoSize = true;
            this.labelOrderDate.Location = new System.Drawing.Point(17, 40);
            this.labelOrderDate.Name = "labelOrderDate";
            this.labelOrderDate.Size = new System.Drawing.Size(85, 13);
            this.labelOrderDate.TabIndex = 2;
            this.labelOrderDate.Text = "Дата приказа*:";
            // 
            // textBoxOrderNumber
            // 
            this.textBoxOrderNumber.Location = new System.Drawing.Point(116, 11);
            this.textBoxOrderNumber.Name = "textBoxOrderNumber";
            this.textBoxOrderNumber.Size = new System.Drawing.Size(229, 20);
            this.textBoxOrderNumber.TabIndex = 1;
            // 
            // labelOrderNumber
            // 
            this.labelOrderNumber.AutoSize = true;
            this.labelOrderNumber.Location = new System.Drawing.Point(17, 14);
            this.labelOrderNumber.Name = "labelOrderNumber";
            this.labelOrderNumber.Size = new System.Drawing.Size(93, 13);
            this.labelOrderNumber.TabIndex = 0;
            this.labelOrderNumber.Text = "Номер приказа*:";
            // 
            // labelStudentOrderType
            // 
            this.labelStudentOrderType.AutoSize = true;
            this.labelStudentOrderType.Location = new System.Drawing.Point(17, 66);
            this.labelStudentOrderType.Name = "labelStudentOrderType";
            this.labelStudentOrderType.Size = new System.Drawing.Size(78, 13);
            this.labelStudentOrderType.TabIndex = 4;
            this.labelStudentOrderType.Text = "Тип приказа*:";
            // 
            // comboBoxStudentOrderType
            // 
            this.comboBoxStudentOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudentOrderType.FormattingEnabled = true;
            this.comboBoxStudentOrderType.Location = new System.Drawing.Point(116, 63);
            this.comboBoxStudentOrderType.Name = "comboBoxStudentOrderType";
            this.comboBoxStudentOrderType.Size = new System.Drawing.Size(300, 21);
            this.comboBoxStudentOrderType.TabIndex = 5;
            // 
            // tabPageRecords
            // 
            this.tabPageRecords.Location = new System.Drawing.Point(4, 22);
            this.tabPageRecords.Name = "tabPageRecords";
            this.tabPageRecords.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRecords.Size = new System.Drawing.Size(311, 148);
            this.tabPageRecords.TabIndex = 1;
            this.tabPageRecords.Text = "Блоки приказа";
            this.tabPageRecords.UseVisualStyleBackColor = true;
            // 
            // tabPageStudents
            // 
            this.tabPageStudents.Location = new System.Drawing.Point(4, 22);
            this.tabPageStudents.Name = "tabPageStudents";
            this.tabPageStudents.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStudents.Size = new System.Drawing.Size(311, 148);
            this.tabPageStudents.TabIndex = 2;
            this.tabPageStudents.Text = "Студенты";
            this.tabPageStudents.UseVisualStyleBackColor = true;
            // 
            // FormStudentOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Name = "FormStudentOrder";
            this.Text = "Приказ";
            this.panelMain.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TabPage tabPageRecords;
        private System.Windows.Forms.Label labelStudentOrderType;
        private System.Windows.Forms.ComboBox comboBoxStudentOrderType;
        private System.Windows.Forms.Label labelOrderNumber;
        private System.Windows.Forms.TextBox textBoxOrderNumber;
        private System.Windows.Forms.DateTimePicker dateTimePickerOrderDate;
        private System.Windows.Forms.Label labelOrderDate;
        private System.Windows.Forms.TabPage tabPageStudents;
    }
}