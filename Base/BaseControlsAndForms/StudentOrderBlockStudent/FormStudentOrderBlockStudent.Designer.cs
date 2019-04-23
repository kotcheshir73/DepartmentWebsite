namespace BaseControlsAndForms.StudentOrderBlockStudent
{
    partial class FormStudentOrderBlockStudent
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
            this.labelStudentOrderBlock = new System.Windows.Forms.Label();
            this.comboBoxStudentOrderBlock = new System.Windows.Forms.ComboBox();
            this.labelStudent = new System.Windows.Forms.Label();
            this.comboBoxStudent = new System.Windows.Forms.ComboBox();
            this.labelStudentGroupFrom = new System.Windows.Forms.Label();
            this.comboBoxStudentGroupFrom = new System.Windows.Forms.ComboBox();
            this.labelStudentGroupTo = new System.Windows.Forms.Label();
            this.comboBoxStudentGroupTo = new System.Windows.Forms.ComboBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(120, 146);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(267, 146);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(39, 146);
            // 
            // labelStudentOrderBlock
            // 
            this.labelStudentOrderBlock.AutoSize = true;
            this.labelStudentOrderBlock.Location = new System.Drawing.Point(12, 9);
            this.labelStudentOrderBlock.Name = "labelStudentOrderBlock";
            this.labelStudentOrderBlock.Size = new System.Drawing.Size(84, 13);
            this.labelStudentOrderBlock.TabIndex = 0;
            this.labelStudentOrderBlock.Text = "Блок приказа*:";
            // 
            // comboBoxStudentOrderBlock
            // 
            this.comboBoxStudentOrderBlock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudentOrderBlock.Enabled = false;
            this.comboBoxStudentOrderBlock.FormattingEnabled = true;
            this.comboBoxStudentOrderBlock.Location = new System.Drawing.Point(104, 6);
            this.comboBoxStudentOrderBlock.Name = "comboBoxStudentOrderBlock";
            this.comboBoxStudentOrderBlock.Size = new System.Drawing.Size(250, 21);
            this.comboBoxStudentOrderBlock.TabIndex = 1;
            // 
            // labelStudent
            // 
            this.labelStudent.AutoSize = true;
            this.labelStudent.Location = new System.Drawing.Point(12, 36);
            this.labelStudent.Name = "labelStudent";
            this.labelStudent.Size = new System.Drawing.Size(54, 13);
            this.labelStudent.TabIndex = 2;
            this.labelStudent.Text = "Студент*:";
            // 
            // comboBoxStudent
            // 
            this.comboBoxStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudent.FormattingEnabled = true;
            this.comboBoxStudent.Location = new System.Drawing.Point(104, 33);
            this.comboBoxStudent.Name = "comboBoxStudent";
            this.comboBoxStudent.Size = new System.Drawing.Size(250, 21);
            this.comboBoxStudent.TabIndex = 3;
            // 
            // labelStudentGroupFrom
            // 
            this.labelStudentGroupFrom.AutoSize = true;
            this.labelStudentGroupFrom.Location = new System.Drawing.Point(12, 63);
            this.labelStudentGroupFrom.Name = "labelStudentGroupFrom";
            this.labelStudentGroupFrom.Size = new System.Drawing.Size(86, 13);
            this.labelStudentGroupFrom.TabIndex = 4;
            this.labelStudentGroupFrom.Text = "Группа откуда*:";
            // 
            // comboBoxStudentGroupFrom
            // 
            this.comboBoxStudentGroupFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudentGroupFrom.FormattingEnabled = true;
            this.comboBoxStudentGroupFrom.Location = new System.Drawing.Point(104, 60);
            this.comboBoxStudentGroupFrom.Name = "comboBoxStudentGroupFrom";
            this.comboBoxStudentGroupFrom.Size = new System.Drawing.Size(250, 21);
            this.comboBoxStudentGroupFrom.TabIndex = 5;
            // 
            // labelStudentGroupTo
            // 
            this.labelStudentGroupTo.AutoSize = true;
            this.labelStudentGroupTo.Location = new System.Drawing.Point(12, 90);
            this.labelStudentGroupTo.Name = "labelStudentGroupTo";
            this.labelStudentGroupTo.Size = new System.Drawing.Size(75, 13);
            this.labelStudentGroupTo.TabIndex = 6;
            this.labelStudentGroupTo.Text = "Группа куда*:";
            // 
            // comboBoxStudentGroupTo
            // 
            this.comboBoxStudentGroupTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudentGroupTo.FormattingEnabled = true;
            this.comboBoxStudentGroupTo.Location = new System.Drawing.Point(104, 87);
            this.comboBoxStudentGroupTo.Name = "comboBoxStudentGroupTo";
            this.comboBoxStudentGroupTo.Size = new System.Drawing.Size(250, 21);
            this.comboBoxStudentGroupTo.TabIndex = 7;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(12, 117);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(76, 13);
            this.labelInfo.TabIndex = 8;
            this.labelInfo.Text = "Информация:";
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Location = new System.Drawing.Point(104, 114);
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.Size = new System.Drawing.Size(250, 20);
            this.textBoxInfo.TabIndex = 9;
            // 
            // FormStudentOrderBlockStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 181);
            this.Controls.Add(this.textBoxInfo);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.labelStudentGroupTo);
            this.Controls.Add(this.comboBoxStudentGroupTo);
            this.Controls.Add(this.labelStudentGroupFrom);
            this.Controls.Add(this.comboBoxStudentGroupFrom);
            this.Controls.Add(this.labelStudent);
            this.Controls.Add(this.comboBoxStudent);
            this.Controls.Add(this.labelStudentOrderBlock);
            this.Controls.Add(this.comboBoxStudentOrderBlock);
            this.Name = "FormStudentOrderBlockStudent";
            this.Text = "Студент приказа";
            this.Load += new System.EventHandler(this.FormStudentOrderBlockStudent_Load);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.buttonSaveAndClose, 0);
            this.Controls.SetChildIndex(this.comboBoxStudentOrderBlock, 0);
            this.Controls.SetChildIndex(this.labelStudentOrderBlock, 0);
            this.Controls.SetChildIndex(this.comboBoxStudent, 0);
            this.Controls.SetChildIndex(this.labelStudent, 0);
            this.Controls.SetChildIndex(this.comboBoxStudentGroupFrom, 0);
            this.Controls.SetChildIndex(this.labelStudentGroupFrom, 0);
            this.Controls.SetChildIndex(this.comboBoxStudentGroupTo, 0);
            this.Controls.SetChildIndex(this.labelStudentGroupTo, 0);
            this.Controls.SetChildIndex(this.labelInfo, 0);
            this.Controls.SetChildIndex(this.textBoxInfo, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStudentOrderBlock;
        private System.Windows.Forms.ComboBox comboBoxStudentOrderBlock;
        private System.Windows.Forms.Label labelStudent;
        private System.Windows.Forms.ComboBox comboBoxStudent;
        private System.Windows.Forms.Label labelStudentGroupFrom;
        private System.Windows.Forms.ComboBox comboBoxStudentGroupFrom;
        private System.Windows.Forms.Label labelStudentGroupTo;
        private System.Windows.Forms.ComboBox comboBoxStudentGroupTo;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.TextBox textBoxInfo;
    }
}