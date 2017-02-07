using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.StudentGroup
{
    public partial class StudentGroupForm : Form
    {
        private readonly IStudentGroupService _service;

        private long _id = 0;

        public StudentGroupForm(IStudentGroupService service)
        {
            InitializeComponent();
            _service = service;
        }

        public StudentGroupForm(IStudentGroupService service, long id)
        {
            InitializeComponent();
            _service = service;
            _id = id;
        }

        private void StudentGroupForm_Load(object sender, EventArgs e)
        {
            comboBoxEducationDirection.ValueMember = "Value";
            comboBoxEducationDirection.DisplayMember = "Display";
            comboBoxEducationDirection.DataSource = _service.GetEducationDirections()
                .Select(ed => new { Value = ed.Id, Display = ed.Cipher + " " + ed.Title }).ToList();
            if (_id != 0)
            {
                var entity = _service.GetStudentGroup(new StudentGroupGetBindingModel { Id = _id });
                if (entity == null)
                {
                    MessageBox.Show("Запись не найдена", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                comboBoxEducationDirection.SelectedValue = entity.EducationDirectionId;
                textBoxGroupName.Text = entity.GroupName;
                textBoxKurs.Text = entity.Kurs.ToString();
            }
        }

        private bool CheckFill()
        {
            if (comboBoxEducationDirection.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxGroupName.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxKurs.Text))
            {
                return false;
            }
            int kurs = 0;
            if (!int.TryParse(textBoxKurs.Text, out kurs))
            {
                return false;
            }
            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (CheckFill())
            {
                if (_id == 0)
                {
                    var res = _service.CreateStudentGroup(new StudentGroupRecordBindingModel
                    {
                        EducationDirectionId = Convert.ToInt64(comboBoxEducationDirection.SelectedValue),
                        GroupName = textBoxGroupName.Text,
                        Kurs = Convert.ToInt32(textBoxKurs.Text)
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
                else
                {
                    var res = _service.UpdateStudentGroup(new StudentGroupRecordBindingModel
                    {
                        Id = _id,
                        EducationDirectionId = Convert.ToInt64(comboBoxEducationDirection.SelectedValue),
                        GroupName = textBoxGroupName.Text,
                        Kurs = Convert.ToInt32(textBoxKurs.Text)
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
