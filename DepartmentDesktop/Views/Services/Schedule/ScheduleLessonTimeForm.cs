using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Text;
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
                var entity = _service.GetScheduleLessonTime(new ScheduleLessonTimeGetBindingModel { Id = _id });
                if (entity == null)
                {
                    MessageBox.Show("Запись не найдена", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
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
