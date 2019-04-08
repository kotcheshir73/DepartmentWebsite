using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using Enums;
using System;
using System.Windows.Forms;
using Tools;
using Unity;

namespace BaseControlsAndForms.Classroom
{
    public partial class FormClassroom : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IClassroomService _service;

        private Guid? _id = null;

        public FormClassroom(IClassroomService service, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void FormClassroom_Load(object sender, EventArgs e)
        {
            foreach (var elem in Enum.GetValues(typeof(ClassroomTypes)))
            {
                comboBoxTypeClassroom.Items.Add(elem.ToString());
            }
            comboBoxTypeClassroom.SelectedIndex = 0;

            if (_id.HasValue)
            {
				LoadData();
			}
		}

		private void LoadData()
		{
			var result = _service.GetClassroom(new ClassroomGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

			comboBoxTypeClassroom.SelectedIndex = comboBoxTypeClassroom.Items.IndexOf(entity.ClassroomType);
			textBoxClassroom.Text = entity.Number;
			textBoxCapacity.Text = entity.Capacity.ToString();
            checkBoxNotUseInSchedule.Checked = entity.NotUseInSchedule;
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
				if (!_id.HasValue)
				{
					result = _service.CreateClassroom(new ClassroomSetBindingModel
					{
						Number = textBoxClassroom.Text,
						ClassroomType = comboBoxTypeClassroom.Text,
						Capacity = Convert.ToInt32(textBoxCapacity.Text),
                        NotUseInSchedule = checkBoxNotUseInSchedule.Checked
					});
				}
				else
				{
					result = _service.UpdateClassroom(new ClassroomSetBindingModel
					{
                        Id = _id.Value,
						Number = textBoxClassroom.Text,
						ClassroomType = comboBoxTypeClassroom.Text,
						Capacity = Convert.ToInt32(textBoxCapacity.Text),
                        NotUseInSchedule = checkBoxNotUseInSchedule.Checked
                    });
				}
				if (result.Succeeded)
				{
					if (result.Result != null)
					{
						if (result.Result is Guid)
						{
							_id = (Guid)result.Result;
						}
					}
					return true;
				}
				else
				{
                    ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
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