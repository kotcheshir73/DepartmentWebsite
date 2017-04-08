﻿using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
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
				LoadData();
			}
		}

		private void LoadData()
		{
			var result = _service.GetStreamingLesson(new StreamingLessonGetBindingModel { Id = _id });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
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
					if (result.Result != null)
					{
						if (result.Result is long)
						{
							_id = (long)result.Result;
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
