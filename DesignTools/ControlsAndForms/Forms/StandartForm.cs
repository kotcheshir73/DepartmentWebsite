using System;
using System.Windows.Forms;

namespace ControlsAndForms.Forms
{
    public partial class StandartForm : Form
    {
        protected Guid? _id = null;

        public StandartForm()
        {
            InitializeComponent();
        }

        public StandartForm(Guid? id = null)
        {
            InitializeComponent();
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        protected void StandartForm_Load()
        {
            if (_id.HasValue)
            {
                LoadData();
            }
        }

        protected virtual void LoadData() { }

        protected virtual bool Save() { return true; }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        private void ButtonSaveAndClose_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}