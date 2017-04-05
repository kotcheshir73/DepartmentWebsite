﻿using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.AcademicPlan
{
	public partial class AcademicYearForm : Form
	{
		private readonly IAcademicYearService _service;

		private long _id = 0;

		public AcademicYearForm(IAcademicYearService service)
		{
			InitializeComponent();
			_service = service;
		}

		public AcademicYearForm(IAcademicYearService service, long id)
		{
			InitializeComponent();
			_service = service;
			_id = id;
		}

		private void AcademicYearForm_Load(object sender, EventArgs e)
		{
			if (_id != 0)
			{
				var result = _service.GetAcademicYear(new AcademicYearGetBindingModel { Id = _id });
				if (!result.Succeeded)
				{
					Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
					Close();
				}
				var entity = result.Result;

				textBoxTitle.Text = entity.Title;
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
					result = _service.CreateAcademicYear(new AcademicYearRecordBindingModel
					{
						Title = textBoxTitle.Text
					});
				}
				else
				{
					result = _service.UpdateAcademicYear(new AcademicYearRecordBindingModel
					{
						Id = _id,
						Title = textBoxTitle.Text
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