namespace ExaminationControlsAndForms.TicketTemplateTableRow
{
    partial class FormTicketTemplateTableRowProperties
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
            this.textBoxTableRowHeight = new System.Windows.Forms.TextBox();
            this.labelTableRowHeight = new System.Windows.Forms.Label();
            this.textBoxCantSplit = new System.Windows.Forms.TextBox();
            this.labelCantSplit = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxTableRowHeight
            // 
            this.textBoxTableRowHeight.Location = new System.Drawing.Point(104, 32);
            this.textBoxTableRowHeight.Name = "textBoxTableRowHeight";
            this.textBoxTableRowHeight.Size = new System.Drawing.Size(100, 20);
            this.textBoxTableRowHeight.TabIndex = 3;
            // 
            // labelTableRowHeight
            // 
            this.labelTableRowHeight.AutoSize = true;
            this.labelTableRowHeight.Location = new System.Drawing.Point(12, 35);
            this.labelTableRowHeight.Name = "labelTableRowHeight";
            this.labelTableRowHeight.Size = new System.Drawing.Size(86, 13);
            this.labelTableRowHeight.TabIndex = 2;
            this.labelTableRowHeight.Text = "Высота строки:";
            // 
            // textBoxCantSplit
            // 
            this.textBoxCantSplit.Location = new System.Drawing.Point(104, 6);
            this.textBoxCantSplit.Name = "textBoxCantSplit";
            this.textBoxCantSplit.Size = new System.Drawing.Size(100, 20);
            this.textBoxCantSplit.TabIndex = 1;
            // 
            // labelCantSplit
            // 
            this.labelCantSplit.AutoSize = true;
            this.labelCantSplit.Location = new System.Drawing.Point(12, 9);
            this.labelCantSplit.Name = "labelCantSplit";
            this.labelCantSplit.Size = new System.Drawing.Size(52, 13);
            this.labelCantSplit.TabIndex = 0;
            this.labelCantSplit.Text = "CantSplit:";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(104, 68);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // FormTicketTemplateTableRowProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 98);
            this.ControlBox = false;
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.textBoxTableRowHeight);
            this.Controls.Add(this.labelTableRowHeight);
            this.Controls.Add(this.textBoxCantSplit);
            this.Controls.Add(this.labelCantSplit);
            this.Name = "FormTicketTemplateTableRowProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Свойства строки таблицы";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxTableRowHeight;
        private System.Windows.Forms.Label labelTableRowHeight;
        private System.Windows.Forms.TextBox textBoxCantSplit;
        private System.Windows.Forms.Label labelCantSplit;
        private System.Windows.Forms.Button buttonClose;
    }
}