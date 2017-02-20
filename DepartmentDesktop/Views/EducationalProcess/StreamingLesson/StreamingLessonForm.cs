using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Text;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.StreamingLesson
{
    public partial class StreamingLessonForm : Form
    {
        private readonly IStreamingLessonService _service;

        private long _id = 0;

        public StreamingLessonForm(IStreamingLessonService service)
        {
            InitializeComponent();
            _service = service;
        }

        public StreamingLessonForm(IStreamingLessonService service, long id)
        {
            InitializeComponent();
            _service = service;
            _id = id;
        }

        private void StreamingLessonForm_Load(object sender, EventArgs e)
        {
            if (_id != 0)
            {
                var entity = _service.GetStreamingLesson(new StreamingLessonGetBindingModel { Id = _id });
                if (entity == null)
                {
                    MessageBox.Show("Запись не найдена", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                textBoxIncomingGroups.Text = entity.IncomingGroups;
                textBoxStreamName.Text = entity.StreamName;
            }
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (CheckFill())
            {
                ResultService result;
                if (_id == 0)
                {
                    result = _service.CreateStreamingLesson(new StreamingLessonRecordBindingModel
                    {
                        IncomingGroups = textBoxIncomingGroups.Text,
                        StreamName = textBoxStreamName.Text
                    });
                }
                else
                {
                    result = _service.UpdateStreamingLesson(new StreamingLessonRecordBindingModel
                    {
                        Id = _id,
                        IncomingGroups = textBoxIncomingGroups.Text,
                        StreamName = textBoxStreamName.Text
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
