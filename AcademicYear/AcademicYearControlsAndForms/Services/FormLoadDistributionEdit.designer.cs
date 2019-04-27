namespace AcademicYearControlsAndForms.Services
{
    partial class FormLoadDistributionEdit
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonAutoComplete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(99, 548);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(246, 548);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(18, 548);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(486, 542);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DataGridView_EditingControlShowing);
            this.dataGridView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DataGridView_KeyPress);
            // 
            // buttonAutoComplete
            // 
            this.buttonAutoComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAutoComplete.Location = new System.Drawing.Point(375, 548);
            this.buttonAutoComplete.Name = "buttonAutoComplete";
            this.buttonAutoComplete.Size = new System.Drawing.Size(99, 23);
            this.buttonAutoComplete.TabIndex = 10;
            this.buttonAutoComplete.Text = "Автозаполнение";
            this.buttonAutoComplete.UseVisualStyleBackColor = true;
            this.buttonAutoComplete.Click += new System.EventHandler(this.ButtonAutoComplete_Click);
            // 
            // LoadDistributionEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 575);
            this.Controls.Add(this.buttonAutoComplete);
            this.Controls.Add(this.dataGridView);
            this.Name = "LoadDistributionEditForm";
            this.Controls.SetChildIndex(this.dataGridView, 0);
            this.Controls.SetChildIndex(this.buttonAutoComplete, 0);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.buttonSaveAndClose, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonAutoComplete;
    }
}