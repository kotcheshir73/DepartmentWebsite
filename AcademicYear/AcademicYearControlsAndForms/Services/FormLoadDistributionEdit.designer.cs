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
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.dataGridView);
            this.panelMain.Size = new System.Drawing.Size(486, 539);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.buttonAutoComplete);
            this.panelTop.Size = new System.Drawing.Size(486, 36);
            this.panelTop.Controls.SetChildIndex(this.buttonClose, 0);
            this.panelTop.Controls.SetChildIndex(this.buttonSave, 0);
            this.panelTop.Controls.SetChildIndex(this.buttonSaveAndClose, 0);
            this.panelTop.Controls.SetChildIndex(this.buttonAutoComplete, 0);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(486, 539);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DataGridView_EditingControlShowing);
            this.dataGridView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DataGridView_KeyPress);
            // 
            // buttonAutoComplete
            // 
            this.buttonAutoComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAutoComplete.Location = new System.Drawing.Point(375, 7);
            this.buttonAutoComplete.Name = "buttonAutoComplete";
            this.buttonAutoComplete.Size = new System.Drawing.Size(99, 23);
            this.buttonAutoComplete.TabIndex = 10;
            this.buttonAutoComplete.Text = "Автозаполнение";
            this.buttonAutoComplete.UseVisualStyleBackColor = true;
            this.buttonAutoComplete.Click += new System.EventHandler(this.ButtonAutoComplete_Click);
            // 
            // FormLoadDistributionEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 575);
            this.Name = "FormLoadDistributionEdit";
            this.Text = "Форма заполнения";
            this.panelMain.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonAutoComplete;
    }
}