using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using Microsoft.Practices.Unity;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.AcademicPlan
{
	public partial class AcademicPlanRecordForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAcademicPlanRecordService _service;

		private Guid? _id = null;

		private Guid _apId;

		public AcademicPlanRecordForm(IAcademicPlanRecordService service, Guid apId, Guid? id = null)
		{
			InitializeComponent();
			_service = service;
			_apId = apId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

		private void AcademicPlanRecordForm_Load(object sender, EventArgs e)
		{
			if(_apId == null)
			{
				MessageBox.Show("Неуказан учебный план", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			var resultAP = _service.GetAcademicPlans(new AcademicPlanGetBindingModel { });
			if (!resultAP.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке учебных планов возникла ошибка: ", resultAP.Errors);
				return;
			}

			var resultD = _service.GetDisciplines( new DisciplineGetBindingModel { } );
			if (!resultD.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", resultD.Errors);
				return;
			}
			/*var resultKL = _service.GetKindOfLoads(new KindOfLoadGetBindingModel { });
			if (!resultKL.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке видов нагрузок возникла ошибка: ", resultKL.Errors);
				return;
			}*/

			foreach (var elem in Enum.GetValues(typeof(Semesters)))
			{
				comboBoxSemester.Items.Add(elem.ToString());
			}

			comboBoxAcademicPlan.ValueMember = "Value";
			comboBoxAcademicPlan.DisplayMember = "Display";
			comboBoxAcademicPlan.DataSource = resultAP.Result.List
				.Select(ap => new { Value = ap.Id, Display = ap.EducationDirection + "/" + ap.AcademicYear }).ToList();
			comboBoxAcademicPlan.SelectedValue = _apId;

			comboBoxDiscipline.ValueMember = "Value";
			comboBoxDiscipline.DisplayMember = "Display";
			comboBoxDiscipline.DataSource = resultD.Result.List
				.Select(d=> new { Value = d.Id, Display = d.DisciplineName }).ToList();
			comboBoxDiscipline.SelectedItem = null;

			/*comboBoxKindOfLoad.ValueMember = "Value";
			comboBoxKindOfLoad.DisplayMember = "Display";
			comboBoxKindOfLoad.DataSource = resultKL.Result.List
				.Select(kl => new { Value = kl.Id, Display = kl.KindOfLoadName }).ToList();
			comboBoxKindOfLoad.SelectedItem = null;*/

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
			//comboBoxKindOfLoad.SelectedValue = entity.KindOfLoadId;
			comboBoxSemester.SelectedIndex = comboBoxSemester.Items.IndexOf(entity.Semester);
			textBoxZet.Text = entity.Zet.ToString();
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
			/*if (comboBoxKindOfLoad.SelectedValue == null)
			{
				return false;
			}*/
			if (string.IsNullOrEmpty(comboBoxSemester.Text))
			{
				return false;
			}
			if (string.IsNullOrEmpty(textBoxZet.Text))
			{
				return false;
			}
			int zet = 0;
			if (!int.TryParse(textBoxZet.Text, out zet))
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
						AcademicPlanId = new Guid(comboBoxAcademicPlan.SelectedValue.ToString()),
						DisciplineId = new Guid(comboBoxDiscipline.SelectedValue.ToString()),
						//KindOfLoadId = new Guid(comboBoxKindOfLoad.SelectedValue.ToString()),
						Semester = comboBoxSemester.Text,
						Zet = Convert.ToInt32(textBoxZet.Text)
					});
				}
				else
				{
					result = _service.UpdateAcademicPlanRecord(new AcademicPlanRecordRecordBindingModel
					{
						Id = _id.Value,
						AcademicPlanId = new Guid(comboBoxAcademicPlan.SelectedValue.ToString()),
						DisciplineId = new Guid(comboBoxDiscipline.SelectedValue.ToString()),
						//KindOfLoadId = new Guid(comboBoxKindOfLoad.SelectedValue.ToString()),
						Semester = comboBoxSemester.Text,
						Zet = Convert.ToInt32(textBoxZet.Text)
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
