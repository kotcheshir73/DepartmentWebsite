using System;
using System.Collections;
using System.Windows.Forms;

namespace DepartmentTablet.CustomControls
{
    public class CustomControl : UserControl
    {
        protected Panel panelNavigation;
        private Label labelTitle;
        protected Panel panelMain;
        private Button buttonBack;

        public CustomControl()
        {
            InitializeComponent();
        }

        protected int _elemensInRow;

        protected string _selectedText;

        protected string _selectedValue;

        protected event EventHandler _selectValue;

        protected event EventHandler _moveBack;

        public string Title { set { labelTitle.Text = value; } }

        public string SelectedText { get { return _selectedText.Replace(Environment.NewLine, " "); } }

        public string SelectedValue { get { return _selectedValue; } }

        public event EventHandler SelectValue { add { _selectValue += value; } remove { _selectValue -= value; } }

        public event EventHandler MoveBack { add { _moveBack += value; buttonBack.Visible = _moveBack != null; } remove { _moveBack -= value; buttonBack.Visible = _moveBack != null; } }

        public virtual void LoadData(ArrayList list = null) { }

        protected void RunClick(object sender, EventArgs e)
        {
            _selectValue?.Invoke(this, e);
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            _moveBack?.Invoke(this, e);
        }

        private void CustomControl_Resize(object sender, EventArgs e)
        {
            panelMain.Width = Width - 10;
            panelMain.Height = Height - panelMain.Top - 10;
        }

        private void InitializeComponent()
        {
            this.panelNavigation = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonBack = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelNavigation.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelNavigation
            // 
            this.panelNavigation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelNavigation.Controls.Add(this.labelTitle);
            this.panelNavigation.Controls.Add(this.buttonBack);
            this.panelNavigation.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNavigation.Location = new System.Drawing.Point(0, 0);
            this.panelNavigation.Name = "panelNavigation";
            this.panelNavigation.Size = new System.Drawing.Size(800, 60);
            this.panelNavigation.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(150, 22);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(61, 13);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Заголовок";
            // 
            // buttonBack
            // 
            this.buttonBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonBack.Location = new System.Drawing.Point(10, 8);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(100, 40);
            this.buttonBack.TabIndex = 0;
            this.buttonBack.Text = "<< НАЗАД";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Visible = false;
            this.buttonBack.Click += new System.EventHandler(this.ButtonBack_Click);
            // 
            // panelMain
            // 
            this.panelMain.AutoScroll = true;
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelMain.Location = new System.Drawing.Point(3, 63);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(794, 434);
            this.panelMain.TabIndex = 1;
            // 
            // CustomControl
            // 
            this.AutoScroll = true;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelNavigation);
            this.Name = "CustomControl";
            this.Size = new System.Drawing.Size(800, 500);
            this.Resize += new System.EventHandler(this.CustomControl_Resize);
            this.panelNavigation.ResumeLayout(false);
            this.panelNavigation.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
