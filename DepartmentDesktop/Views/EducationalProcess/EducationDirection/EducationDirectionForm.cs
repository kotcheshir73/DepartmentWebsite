﻿using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.EducationDirection
{
    public partial class EducationDirectionForm : Form
    {
        private readonly IEducationDirectionService _service;

        private long _id = 0;

        public EducationDirectionForm(IEducationDirectionService service)
        {
            InitializeComponent();
            _service = service;
        }

        public EducationDirectionForm(IEducationDirectionService service, long id)
        {
            InitializeComponent();
            _service = service;
            _id = id;
        }

        private void EducationDirectionForm_Load(object sender, EventArgs e)
        {
            if(_id != 0)
            {
				LoadData();
			}
		}

		private void LoadData()
		{
			var result = _service.GetEducationDirection(new EducationDirectionGetBindingModel { Id = _id });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;
			textBoxCipher.Text = entity.Cipher;
			textBoxTitle.Text = entity.Title;
			textBoxDescription.Text = entity.Description;
		}

		private bool CheckFill()
        {
            if(string.IsNullOrEmpty(textBoxCipher.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxTitle.Text))
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
					result = _service.CreateEducationDirection(new EducationDirectionRecordBindingModel
					{
						Cipher = textBoxCipher.Text,
						Description = textBoxDescription.Text,
						Title = textBoxTitle.Text
					});
				}
				else
				{
					result = _service.UpdateEducationDirection(new EducationDirectionRecordBindingModel
					{
						Id = _id,
						Cipher = textBoxCipher.Text,
						Description = textBoxDescription.Text,
						Title = textBoxTitle.Text
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
