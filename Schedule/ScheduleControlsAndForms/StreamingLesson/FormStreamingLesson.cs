using ControlsAndForms.Messangers;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using System;
using System.Windows.Forms;
using Tools;
using Unity;

namespace ScheduleControlsAndForms.StreamingLesson
{
    public partial class FormStreamingLesson : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStreamingLessonService _service;

        private Guid? _id = null;

        public FormStreamingLesson(IStreamingLessonService service, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void FormStreamingLesson_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
				LoadData();
			}
		}

		private void LoadData()
		{
			var result = _service.GetStreamingLesson(new StreamingLessonGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

			textBoxIncomingGroups.Text = entity.IncomingGroups;
			textBoxStreamName.Text = entity.StreamName;
		}

		private bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxIncomingGroups.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxStreamName.Text))
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
					result = _service.CreateStreamingLesson(new StreamingLessonSetBindingModel
					{
						IncomingGroups = textBoxIncomingGroups.Text,
						StreamName = textBoxStreamName.Text
					});
				}
				else
				{
					result = _service.UpdateStreamingLesson(new StreamingLessonSetBindingModel
					{
						Id = _id.Value,
						IncomingGroups = textBoxIncomingGroups.Text,
						StreamName = textBoxStreamName.Text
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