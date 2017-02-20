using DepartmentDAL;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Text;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.Classroom
{
    public partial class ClassroomForm : Form
    {
        private readonly IClassroomService _service;

        private string _id;

        public ClassroomForm(IClassroomService service)
        {
            InitializeComponent();
            _service = service;
        }

        public ClassroomForm(IClassroomService service, string id)
        {
            InitializeComponent();
            _service = service;
            _id = id;
        }

        private void ClassroomForm_Load(object sender, EventArgs e)
        {
            foreach (var elem in Enum.GetValues(typeof(ClassroomTypes)))
            {
                comboBoxTypeClassroom.Items.Add(elem);
            }
            comboBoxTypeClassroom.SelectedIndex = 0;
            if (!string.IsNullOrEmpty(_id))
            {
                var entity = _service.GetClassroom(new ClassroomGetBindingModel { Id = _id });
                if (entity == null)
                {
                    MessageBox.Show("Запись не найдена", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                comboBoxTypeClassroom.SelectedValue = entity.ClassroomType;
                textBoxClassroom.Text = _id;
                textBoxCapacity.Text = entity.Capacity.ToString();
            }
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(comboBoxTypeClassroom.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxClassroom.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxCapacity.Text))
            {
                return false;
            }
            int capacity = 0;
            if (!int.TryParse(textBoxCapacity.Text, out capacity))
            {
                return false;
            }
            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (CheckFill())
            {
                ResultService result;
                if (string.IsNullOrEmpty(_id))
                {
                    result = _service.CreateClassroom(new ClassroomRecordBindingModel
                    {
                        Id = textBoxClassroom.Text,
                        ClassroomType = comboBoxTypeClassroom.Text,
                        Capacity = Convert.ToInt32(textBoxCapacity.Text)
                    });
                }
                else
                {
                    result = _service.UpdateClassroom(new ClassroomRecordBindingModel
                    {
                        Id = textBoxClassroom.Text,
                        ClassroomType = comboBoxTypeClassroom.Text,
                        Capacity = Convert.ToInt32(textBoxCapacity.Text)
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
