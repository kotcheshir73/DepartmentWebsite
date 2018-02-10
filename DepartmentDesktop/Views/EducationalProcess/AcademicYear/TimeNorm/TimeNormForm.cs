using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.TimeNorm
{
	public partial class TimeNormForm : Form
	{
		private readonly ITimeNormService _service;

        private Guid _ayId;

		private Guid? _id = null;

        public TimeNormForm(ITimeNormService service, Guid ayId, Guid? id = null)
		{
			InitializeComponent();
			_service = service;
            _ayId = ayId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

		private void TimeNormForm_Load(object sender, EventArgs e)
        {
            var resultAY = _service.GetAcademicYears(new AcademicYearGetBindingModel { });
            if (!resultAY.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
                return;
            }

            var resultKL = _service.GetKindOfLoads(new KindOfLoadGetBindingModel { });
			if (!resultKL.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке видов нагрузок возникла ошибка: ", resultKL.Errors);
				return;
            }

            comboBoxAcademicYear.ValueMember = "Value";
            comboBoxAcademicYear.DisplayMember = "Display";
            comboBoxAcademicYear.DataSource = resultAY.Result.List
                .Select(ay => new { Value = ay.Id, Display = ay.Title }).ToList();
            comboBoxAcademicYear.SelectedItem = _ayId;

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
						KindOfLoadId = new Guid(comboBoxKindOfLoad.SelectedValue.ToString()),
                        AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
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
						KindOfLoadId = new Guid(comboBoxKindOfLoad.SelectedValue.ToString()),
                        AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                        Title = textBoxTitle.Text,
						Formula = textBoxFormula.Text,
						Hours = Convert.ToDecimal(textBoxHours.Text)
					});
				}
				if (result.Succeeded)
				{
					if (result.Result != null)
					{
						if (result.Result is Guid)
						{
							_id = (Guid)result.Result;
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
