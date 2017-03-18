using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Services.Schedule
{
    public partial class ScheduleLessonTimeForm : Form
    {
        private readonly IScheduleLessonTimeService _service;

        private long _id;

        public ScheduleLessonTimeForm(IScheduleLessonTimeService service)
        {
            InitializeComponent();
            _service = service;
        }

        public ScheduleLessonTimeForm(IScheduleLessonTimeService service, long id)
        {
            InitializeComponent();
            _service = service;
            _id = id;
        }

        private void ScheduleLessonTimeForm_Load(object sender, EventArgs e)
        {
            if (_id != 0)
            {
                var result = _service.GetScheduleLessonTime(new ScheduleLessonTimeGetBindingModel { Id = _id });
				if (!result.Succeeded)
				{
					Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
					Close();
				}
				var entity = result.Result;

				textBoxTitle.Text = entity.Title;
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
            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (CheckFill())
            {
                ResultService result;
                if (_id == 0)
                {
                    result = _service.CreateScheduleLessonTime(new ScheduleLessonTimeRecordBindingModel
                    {
                        Title = textBoxTitle.Text,
                        DateBeginLesson = dateTimePickerDateBeginLesson.Value,
                        DateEndLesson = dateTimePickerDateEndLesson.Value
                    });
                }
                else
                {
                    result = _service.UpdateScheduleLessonTime(new ScheduleLessonTimeRecordBindingModel
                    {
                        Id = _id,
                        Title = textBoxTitle.Text,
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
