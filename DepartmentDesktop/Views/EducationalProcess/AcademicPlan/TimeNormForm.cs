using DepartmentDAL;
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

namespace DepartmentDesktop.Views.EducationalProcess.AcademicPlan
{
	public partial class TimeNormForm : Form
	{
		private readonly ITimeNormService _service;

		private long _id = 0;

		public TimeNormForm(ITimeNormService service)
		{
			InitializeComponent();
			_service = service;
		}

		public TimeNormForm(ITimeNormService service, long id)
		{
			InitializeComponent();
			_service = service;
			_id = id;
		}

		private void TimeNormForm_Load(object sender, EventArgs e)
		{
			var resultKL = _service.GetKindOfLoads();
			if (!resultKL.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке видов нагрузок возникла ошибка: ", resultKL.Errors);
				return;
			}

			var resultTN = _service.GetTimeNorms();
			if (!resultKL.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке норм времени возникла ошибка: ", resultKL.Errors);
				return;
			}

			comboBoxKindOfLoad.ValueMember = "Value";
			comboBoxKindOfLoad.DisplayMember = "Display";
			comboBoxKindOfLoad.DataSource = resultKL.Result
				.Select(kl => new { Value = kl.Id, Display = kl.KindOfLoadName }).ToList();
			comboBoxKindOfLoad.SelectedItem = null;

			comboBoxTimeNorm.ValueMember = "Value";
			comboBoxTimeNorm.DisplayMember = "Display";
			comboBoxTimeNorm.DataSource = resultTN.Result
				.Select(tn => new { Value = tn.Id, Display = tn.Title }).ToList();
			comboBoxTimeNorm.SelectedItem = null;

			if (_id != 0)
			{
				var result = _service.GetTimeNorm(new TimeNormGetBindingModel { Id = _id });
				if (!result.Succeeded)
				{
					Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
					Close();
				}
				var entity = result.Result;
				
				comboBoxKindOfLoad.SelectedValue = entity.KindOfLoadId;
				comboBoxTimeNorm.SelectedValue = entity.ParentTimeNormId;
				textBoxTitle.Text = entity.Title;
				textBoxHours.Text = entity.Hours.ToString();
			}
		}

		private bool CheckFill()
		{
			if (comboBoxKindOfLoad.SelectedValue == null)
			{
				return false;
			}
			if (string.IsNullOrEmpty(textBoxTitle.Text))
			{
				return false;
			}
			if (string.IsNullOrEmpty(textBoxHours.Text))
			{
				return false;
			}
			decimal hours = 0;
			if (!decimal.TryParse(textBoxHours.Text, out hours))
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
				long? paretnId = null;
				if(comboBoxTimeNorm.SelectedValue != null)
				{
					paretnId = Convert.ToInt64(comboBoxTimeNorm.SelectedValue);
				}
				if (_id == 0)
				{
					result = _service.CreateTimeNorm(new TimeNormRecordBindingModel
					{
						KindOfLoadId = Convert.ToInt64(comboBoxKindOfLoad.SelectedValue),
						Title = textBoxTitle.Text,
						ParentTimeNormId = paretnId,
						Hours = Convert.ToDecimal(textBoxHours.Text)
					});
				}
				else
				{
					result = _service.UpdateTimeNorm(new TimeNormRecordBindingModel
					{
						Id = _id,
						KindOfLoadId = Convert.ToInt64(comboBoxKindOfLoad.SelectedValue),
						Title = textBoxTitle.Text,
						ParentTimeNormId = paretnId,
						Hours = Convert.ToDecimal(textBoxHours.Text)
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
