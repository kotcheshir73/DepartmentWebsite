using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.EducationDirection
{
    public partial class EducationDirectionForm : Form
    {
        private readonly IEducationDirectionService _service;

        private long _id = 0;

        public EducationDirectionForm(IEducationDirectionService service)
        {
            InitializeComponent();
            _service = service;
        }

        public EducationDirectionForm(IEducationDirectionService service, long id)
        {
            InitializeComponent();
            _service = service;
            _id = id;
        }

        private void EducationDirectionForm_Load(object sender, EventArgs e)
        {
            if(_id != 0)
            {
                var entity = _service.GetEducationDirection(new EducationDirectionGetBindingModel { Id = _id });
                if(entity == null)
                {
                    MessageBox.Show("Запись не найдена", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                textBoxCipher.Text = entity.Cipher;
                textBoxTitle.Text = entity.Title;
                textBoxDescription.Text = entity.Description;
            }
        }

        private bool CheckFill()
        {
            if(string.IsNullOrEmpty(textBoxCipher.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxTitle.Text))
            {
                return false;
            }
            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(CheckFill())
            {
                if(_id == 0)
                {
                    var res = _service.CreateEducationDirection(new EducationDirectionRecordBindingModel
                    {
                        Cipher = textBoxCipher.Text,
                        Description = textBoxDescription.Text,
                        Title = textBoxTitle.Text
                    });
                    if(res.Succeeded)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("При сохранении возникла ошибка: " + res.Errors["error"], "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    var res = _service.UpdateEducationDirection(new EducationDirectionRecordBindingModel
                    {
                        Id = _id,
                        Cipher = textBoxCipher.Text,
                        Description = textBoxDescription.Text,
                        Title = textBoxTitle.Text
                    });
                    if (res.Succeeded)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("При сохранении возникла ошибка: " + res.Errors["error"], "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
