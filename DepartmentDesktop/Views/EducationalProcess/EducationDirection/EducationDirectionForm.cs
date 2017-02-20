using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Text;
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
                ResultService result;
                if (_id == 0)
                {
                    result = _service.CreateEducationDirection(new EducationDirectionRecordBindingModel
                    {
                        Cipher = textBoxCipher.Text,
                        Description = textBoxDescription.Text,
                        Title = textBoxTitle.Text
                    });
                }
                else
                {
                    result = _service.UpdateEducationDirection(new EducationDirectionRecordBindingModel
                    {
                        Id = _id,
                        Cipher = textBoxCipher.Text,
                        Description = textBoxDescription.Text,
                        Title = textBoxTitle.Text
                    });
                }
                if (result.Succeeded)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    StringBuilder strRes = new StringBuilder();
                    foreach (var err in result.Errors)
                    {
                        strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
                    }
                    MessageBox.Show("При сохранении возникла ошибка: " + strRes.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
