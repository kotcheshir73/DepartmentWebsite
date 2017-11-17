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

        public ClassroomForm(IClassroomService service, string id = null)
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
				LoadData();
			}
		}

		private void LoadData()
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

		private bool Save()
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
					if (result.Result != null)
					{
						if (result.Result is string)
						{
							_id = (string)result.Result;
						}
					}
					return true;
				}
				else
				{
					Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
					return false;
				}
			}
			else
			{
				MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (Save())
			{
				MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
				LoadData();
			}
		}

		private void buttonSaveAndClose_Click(object sender, EventArgs e)
		{
			if (Save())
			{
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
