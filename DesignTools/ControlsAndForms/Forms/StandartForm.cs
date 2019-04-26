using System;
using System.Windows.Forms;

namespace ControlsAndForms.Forms
{
    public partial class StandartForm : Form
    {
        protected Guid? _id = null;

        private event Action _onCloseEvent;

        public StandartForm()
        {
            InitializeComponent();
        }

        public StandartForm(Guid? id)
        {
            InitializeComponent();
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void StandartForm_Load(object sender, EventArgs e)
        {
            if (LoadComponents())
            {
                if (_id.HasValue)
                {
                    LoadData();
                }
            }
            else
            {
                Close();
            }
        }

        protected virtual bool LoadComponents() { return true; }

        protected virtual void LoadData() { }

        protected virtual bool CheckFill() { return true; }

        protected virtual bool Save() { return true; }

        public void AddCloseEvent(Action method)
        {
            _onCloseEvent += method;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (CheckFill())
            {
                if (Save())
                {
                    MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonSaveAndClose_Click(object sender, EventArgs e)
        {
            if (CheckFill())
            {
                if (Save())
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void StandartForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _onCloseEvent?.Invoke();
        }
    }
}