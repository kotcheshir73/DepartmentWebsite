using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.LoadDistribution
{
	public partial class LoadDistributionRecordForm : Form
	{
		private readonly ILoadDistributionRecordService _service;

		private long _id = 0;

		private long _ldId = 0;

		private long _apId = 0;

		public LoadDistributionRecordForm(ILoadDistributionRecordService service, long ldId, long apId)
		{
			InitializeComponent();
			_service = service;
			_ldId = ldId;
			_apId = apId;
		}

		public LoadDistributionRecordForm(ILoadDistributionRecordService service, long ldId, long apId, long id)
		{
			InitializeComponent();
			_service = service;
			_apId = apId;
			_ldId = ldId;
			_id = id;
		}

		private void LoadDistributionRecordForm_Load(object sender, EventArgs e)
		{
			var resultAPR = _service.GetAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { AcademicPlanId = _apId });
			if (!resultAPR.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке записей учебного плана возникла ошибка: ", resultAPR.Errors);
				return;
			}

			var resultC = _service.GetContingents();
			if (!resultC.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке контингента возникла ошибка: ", resultC.Errors);
				return;
			}
			var resultTN = _service.GetTimeNorms();
			if (!resultTN.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке норм времени возникла ошибка: ", resultTN.Errors);
				return;
			}

			comboBoxAcademicPlanRecord.ValueMember = "Value";
			comboBoxAcademicPlanRecord.DisplayMember = "Display";
			comboBoxAcademicPlanRecord.DataSource = resultAPR.Result
				.Select(apr => new { Value = apr.Id, Display = apr.Disciplne + "/" + apr.KindOfLoad }).ToList();
			comboBoxAcademicPlanRecord.SelectedValue = _apId;

			comboBoxContingent.ValueMember = "Value";
			comboBoxContingent.DisplayMember = "Display";
			comboBoxContingent.DataSource = resultC.Result
				.Select(c => new { Value = c.Id, Display = c.StudentGroupName }).ToList();
			comboBoxContingent.SelectedItem = null;

			comboBoxTimeNorm.ValueMember = "Value";
			comboBoxTimeNorm.DisplayMember = "Display";
			comboBoxTimeNorm.DataSource = resultTN.Result
				.Select(tn => new { Value = tn.Id, Display = tn.KindOfLoadName }).ToList();
			comboBoxTimeNorm.SelectedItem = null;

			if (_id != 0)
			{
				LoadData();
			}
		}

		private void LoadData()
		{
			var result = _service.GetLoadDistributionRecord(new LoadDistributionRecordGetBindingModel { Id = _id });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

			comboBoxAcademicPlanRecord.SelectedValue = entity.AcademicPlanRecordId;
			comboBoxContingent.SelectedValue = entity.ContingentId;
			comboBoxTimeNorm.SelectedValue = entity.TimeNormId;
		}

		private bool CheckFill()
		{
			if (comboBoxAcademicPlanRecord.SelectedValue == null)
			{
				return false;
			}
			if (comboBoxContingent.SelectedValue == null)
			{
				return false;
			}
			if (comboBoxTimeNorm.SelectedValue == null)
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
					result = _service.CreateLoadDistributionRecord(new LoadDistributionRecordRecordBindingModel
					{
						LoadDistributionId = _ldId,
						AcademicPlanRecordId = Convert.ToInt64(comboBoxAcademicPlanRecord.SelectedValue),
						ContingentId = Convert.ToInt64(comboBoxContingent.SelectedValue),
						TimeNormId = Convert.ToInt64(comboBoxTimeNorm.SelectedValue)
					});
				}
				else
				{
					result = _service.UpdateLoadDistributionRecord(new LoadDistributionRecordRecordBindingModel
					{
						Id = _id,
						LoadDistributionId = _ldId,
						AcademicPlanRecordId = Convert.ToInt64(comboBoxAcademicPlanRecord.SelectedValue),
						ContingentId = Convert.ToInt64(comboBoxContingent.SelectedValue),
						TimeNormId = Convert.ToInt64(comboBoxTimeNorm.SelectedValue)
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
