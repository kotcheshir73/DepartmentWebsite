using DepartmentDAL;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.AcademicPlan
{
	public partial class TimeNormForm : Form
	{
		private readonly ITimeNormService _service;

		private long? _id = 0;

		public TimeNormForm(ITimeNormService service, long? id = null)
		{
			InitializeComponent();
			_service = service;
			_id = id;
		}

		private void TimeNormForm_Load(object sender, EventArgs e)
		{
			var resultKL = _service.GetKindOfLoads(new KindOfLoadGetBindingModel { });
			if (!resultKL.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке видов нагрузок возникла ошибка: ", resultKL.Errors);
				return;
			}

			comboBoxKindOfLoad.ValueMember = "Value";
			comboBoxKindOfLoad.DisplayMember = "Display";
			comboBoxKindOfLoad.DataSource = resultKL.Result.List
				.Select(kl => new { Value = kl.Id, Display = kl.KindOfLoadName }).ToList();
			comboBoxKindOfLoad.SelectedItem = null;

			comboBoxSelectKindOfLoad.ValueMember = "Value";
			comboBoxSelectKindOfLoad.DisplayMember = "Display";
			comboBoxSelectKindOfLoad.DataSource = resultKL.Result.List
				.Select(kl => new { Value = kl.Id, Display = kl.KindOfLoadName }).ToList();
			comboBoxSelectKindOfLoad.SelectedItem = null;

			foreach (var elem in Enum.GetValues(typeof(KindOfLoadType)))
			{
				comboBoxSelectKindOfLoadType.Items.Add(elem);
			}
			comboBoxSelectKindOfLoadType.SelectedIndex = -1;

			if (_id.HasValue)
			{
				LoadData();
			}
		}

		private void CreateFormula()
		{
			// делаем схему [<Название вида нагрузки>]<*><число>*"поток/группа/студенты"
			StringBuilder formula = new StringBuilder();
			if(comboBoxSelectKindOfLoad.SelectedItem != null)
			{
				formula.Append(string.Format("[{0}]", comboBoxSelectKindOfLoad.Text));
			}
			if(!string.IsNullOrEmpty(textBoxHours.Text))
			{
				formula.Append(string.Format("*{0}*", textBoxHours.Text));
			}
			if(comboBoxSelectKindOfLoadType.SelectedItem != null)
			{
				formula.Append(string.Format("\"{0}\"", comboBoxSelectKindOfLoadType.Text));
			}
			textBoxFormula.Text = formula.ToString();
		}

		private void comboBoxSelectKindOfLoad_SelectedIndexChanged(object sender, EventArgs e)
		{
			CreateFormula();
		}

		private void comboBoxSelectKindOfLoadType_SelectedIndexChanged(object sender, EventArgs e)
		{
			CreateFormula();
		}

		private void textBoxHours_Leave(object sender, EventArgs e)
		{
			CreateFormula();
		}

		private void LoadData()
		{
			var result = _service.GetTimeNorm(new TimeNormGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

			comboBoxKindOfLoad.SelectedValue = entity.KindOfLoadId;
			textBoxTitle.Text = entity.Title;
			textBoxFormula.Text = entity.Formula;
			textBoxHours.Text = entity.Hours.ToString();
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
			if (string.IsNullOrEmpty(textBoxFormula.Text))
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

		private bool Save()
		{
			if (CheckFill())
			{
				ResultService result;
				if (!_id.HasValue)
				{
					result = _service.CreateTimeNorm(new TimeNormRecordBindingModel
					{
						KindOfLoadId = Convert.ToInt64(comboBoxKindOfLoad.SelectedValue),
						Title = textBoxTitle.Text,
						Formula = textBoxFormula.Text,
						Hours = Convert.ToDecimal(textBoxHours.Text)
					});
				}
				else
				{
					result = _service.UpdateTimeNorm(new TimeNormRecordBindingModel
					{
						Id = _id.Value,
						KindOfLoadId = Convert.ToInt64(comboBoxKindOfLoad.SelectedValue),
						Title = textBoxTitle.Text,
						Formula = textBoxFormula.Text,
						Hours = Convert.ToDecimal(textBoxHours.Text)
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
