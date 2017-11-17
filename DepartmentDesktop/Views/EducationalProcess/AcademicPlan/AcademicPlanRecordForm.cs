using DepartmentDAL;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.AcademicPlan
{
	public partial class AcademicPlanRecordForm : Form
	{
		private readonly IAcademicPlanRecordService _service;

		private long? _id = 0;

		private long _apId = 0;

		public AcademicPlanRecordForm(IAcademicPlanRecordService service, long apId, long? id = null)
		{
			InitializeComponent();
			_service = service;
			_apId = apId;
			_id = id;
		}

		private void AcademicPlanRecordForm_Load(object sender, EventArgs e)
		{
			if(_apId == 0)
			{
				MessageBox.Show("Неуказан учебный план", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			var resultAP = _service.GetAcademicPlans();
			if (!resultAP.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке учебных планов возникла ошибка: ", resultAP.Errors);
				return;
			}

			var resultD = _service.GetDisciplines();
			if (!resultD.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", resultD.Errors);
				return;
			}
			var resultKL = _service.GetKindOfLoads();
			if (!resultKL.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке видов нагрузок возникла ошибка: ", resultKL.Errors);
				return;
			}

			foreach (var elem in Enum.GetValues(typeof(Semesters)))
			{
				comboBoxSemester.Items.Add(elem.ToString());
			}

			comboBoxAcademicPlan.ValueMember = "Value";
			comboBoxAcademicPlan.DisplayMember = "Display";
			comboBoxAcademicPlan.DataSource = resultAP.Result
				.Select(ap => new { Value = ap.Id, Display = ap.EducationDirection + "/" + ap.AcademicYear }).ToList();
			comboBoxAcademicPlan.SelectedValue = _apId;

			comboBoxDiscipline.ValueMember = "Value";
			comboBoxDiscipline.DisplayMember = "Display";
			comboBoxDiscipline.DataSource = resultD.Result
				.Select(d=> new { Value = d.Id, Display = d.DisciplineName }).ToList();
			comboBoxDiscipline.SelectedItem = null;

			comboBoxKindOfLoad.ValueMember = "Value";
			comboBoxKindOfLoad.DisplayMember = "Display";
			comboBoxKindOfLoad.DataSource = resultKL.Result
				.Select(kl => new { Value = kl.Id, Display = kl.KindOfLoadName }).ToList();
			comboBoxKindOfLoad.SelectedItem = null;

			if (_id.HasValue)
			{
				LoadData();
			}
		}

		private void LoadData()
		{
			var result = _service.GetAcademicPlanRecord(new AcademicPlanRecordGetBindingModel { Id = _id });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

			comboBoxAcademicPlan.SelectedValue = entity.AcademicPlanId;
			comboBoxDiscipline.SelectedValue = entity.DisciplineId;
			comboBoxKindOfLoad.SelectedValue = entity.KindOfLoadId;
			comboBoxSemester.SelectedIndex = comboBoxSemester.Items.IndexOf(entity.Semester);
			textBoxHours.Text = entity.Hours.ToString();
		}

		private bool CheckFill()
		{
			if (comboBoxAcademicPlan.SelectedValue == null)
			{
				return false;
			}
			if (comboBoxDiscipline.SelectedValue == null)
			{
				return false;
			}
			if (comboBoxKindOfLoad.SelectedValue == null)
			{
				return false;
			}
			if (string.IsNullOrEmpty(comboBoxSemester.Text))
			{
				return false;
			}
			if (string.IsNullOrEmpty(textBoxHours.Text))
			{
				return false;
			}
			int hours = 0;
			if (!int.TryParse(textBoxHours.Text, out hours))
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
					result = _service.CreateAcademicPlanRecord(new AcademicPlanRecordRecordBindingModel
					{
						AcademicPlanId = Convert.ToInt64(comboBoxAcademicPlan.SelectedValue),
						DisciplineId = Convert.ToInt64(comboBoxDiscipline.SelectedValue),
						KindOfLoadId = Convert.ToInt64(comboBoxKindOfLoad.SelectedValue),
						Semester = comboBoxSemester.Text,
						Hours = Convert.ToInt32(textBoxHours.Text)
					});
				}
				else
				{
					result = _service.UpdateAcademicPlanRecord(new AcademicPlanRecordRecordBindingModel
					{
						Id = _id.Value,
						AcademicPlanId = Convert.ToInt64(comboBoxAcademicPlan.SelectedValue),
						DisciplineId = Convert.ToInt64(comboBoxDiscipline.SelectedValue),
						KindOfLoadId = Convert.ToInt64(comboBoxKindOfLoad.SelectedValue),
						Semester = comboBoxSemester.Text,
						Hours = Convert.ToInt32(textBoxHours.Text)
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
