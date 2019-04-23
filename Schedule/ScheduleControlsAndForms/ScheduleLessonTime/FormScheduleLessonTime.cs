using ControlsAndForms.Messangers;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using System;
using System.Windows.Forms;
using Tools;

namespace ScheduleControlsAndForms.ScheduleLessonTime
{
    public partial class FormScheduleLessonTime : Form
    {
        private readonly IScheduleLessonTimeService _service;

        private Guid? _id = null;

        public FormScheduleLessonTime(IScheduleLessonTimeService service, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void FormScheduleLessonTime_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                var result = _service.GetScheduleLessonTime(new ScheduleLessonTimeGetBindingModel { Id = _id.Value });
				if (!result.Succeeded)
				{
                    ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
					Close();
				}
				var entity = result.Result;

				textBoxTitle.Text = entity.Title;
                textBoxOrder.Text = entity.Order.ToString();
                dateTimePickerDateBeginLesson.Value = entity.DateBeginLesson;
                dateTimePickerDateEndLesson.Value = entity.DateEndLesson;
            }
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxTitle.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxOrder.Text))
            {
                return false;
            }
            if (!int.TryParse(textBoxOrder.Text, out int order))
            {
                return false;
            }
            return true;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (CheckFill())
            {
                ResultService result;
                if (!_id.HasValue)
                {
                    result = _service.CreateScheduleLessonTime(new ScheduleLessonTimeSetBindingModel
                    {
                        Title = textBoxTitle.Text,
                        Order = Convert.ToInt32(textBoxOrder.Text),
                        DateBeginLesson = dateTimePickerDateBeginLesson.Value,
                        DateEndLesson = dateTimePickerDateEndLesson.Value
                    });
                }
                else
                {
                    result = _service.UpdateScheduleLessonTime(new ScheduleLessonTimeSetBindingModel
                    {
                        Id = _id.Value,
                        Title = textBoxTitle.Text,
                        Order = Convert.ToInt32(textBoxOrder.Text),
                        DateBeginLesson = dateTimePickerDateBeginLesson.Value,
                        DateEndLesson = dateTimePickerDateEndLesson.Value
                    });
                }
                if (result.Succeeded)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
				{
                    ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
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
    }
}