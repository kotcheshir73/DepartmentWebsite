using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (_id == 0)
                {
                    var res = _service.CreateStreamingLesson(new StreamingLessonRecordBindingModel
                    {
                        IncomingGroups = textBoxIncomingGroups.Text,
                        StreamName = textBoxStreamName.Text
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
                    var res = _service.UpdateStreamingLesson(new StreamingLessonRecordBindingModel
                    {
                        Id = _id,
                        IncomingGroups = textBoxIncomingGroups.Text,
                        StreamName = textBoxStreamName.Text
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
