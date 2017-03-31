using DepartmentDAL;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
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
                comboBoxTypeClassroom.Items.Add(elem.ToString());
            }
            comboBoxTypeClassroom.SelectedIndex = 0;
            if (!string.IsNullOrEmpty(_id))
            {
				var result = _service.GetClassroom(new ClassroomGetBindingModel { Id = _id });
                if (!result.Succeeded)
				{
					Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
					Close();
                }
				var entity = result.Result;

				comboBoxTypeClassroom.SelectedIndex = comboBoxTypeClassroom.Items.IndexOf(entity.ClassroomType);
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
					Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
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
