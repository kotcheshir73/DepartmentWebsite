using DepartmentModel;
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

		private Guid? _id;

		private Guid? _ldId;

		private Guid? _apId;

		public LoadDistributionRecordForm(ILoadDistributionRecordService service, Guid ldId, Guid apId, Guid? id = null)
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

			var resultC = _service.GetContingents(new ContingentGetBindingModel { });
			if (!resultC.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке контингента возникла ошибка: ", resultC.Errors);
				return;
			}
			var resultTN = _service.GetTimeNorms(new TimeNormGetBindingModel { });
			if (!resultTN.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке норм времени возникла ошибка: ", resultTN.Errors);
				return;
			}

			comboBoxAcademicPlanRecord.ValueMember = "Value";
			comboBoxAcademicPlanRecord.DisplayMember = "Display";
			comboBoxAcademicPlanRecord.DataSource = resultAPR.Result.List
				.Select(apr => new { Value = apr.Id, Display = apr.Disciplne + "/" + apr.KindOfLoad }).ToList();
			comboBoxAcademicPlanRecord.SelectedValue = _apId;

			comboBoxContingent.ValueMember = "Value";
			comboBoxContingent.DisplayMember = "Display";
			comboBoxContingent.DataSource = resultC.Result.List
				.Select(c => new { Value = c.Id, Display = c.EducationDirectionCipher }).ToList();
			comboBoxContingent.SelectedItem = null;

			comboBoxTimeNorm.ValueMember = "Value";
			comboBoxTimeNorm.DisplayMember = "Display";
			comboBoxTimeNorm.DataSource = resultTN.Result.List
				.Select(tn => new { Value = tn.Id, Display = tn.KindOfLoadName }).ToList();
			comboBoxTimeNorm.SelectedItem = null;

			if (_id.HasValue)
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
			textBoxLoad.Text = entity.Load.ToString();
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
			if(textBoxLoad.Text == "")
			{
				return false;
			}
			decimal load = 0;
			if(!decimal.TryParse(textBoxLoad.Text, out load))
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
					result = _service.CreateLoadDistributionRecord(new LoadDistributionRecordRecordBindingModel
					{
						LoadDistributionId = _ldId.Value,
						AcademicPlanRecordId = new Guid(comboBoxAcademicPlanRecord.SelectedValue.ToString()),
						ContingentId = new Guid(comboBoxContingent.SelectedValue.ToString()),
						TimeNormId = new Guid(comboBoxTimeNorm.SelectedValue.ToString()),
						Load = Convert.ToDecimal(textBoxLoad.Text)
					});
				}
				else
				{
					result = _service.UpdateLoadDistributionRecord(new LoadDistributionRecordRecordBindingModel
					{
						Id = _id.Value,
						LoadDistributionId = _ldId.Value,
						AcademicPlanRecordId = new Guid(comboBoxAcademicPlanRecord.SelectedValue.ToString()),
						ContingentId = new Guid(comboBoxContingent.SelectedValue.ToString()),
						TimeNormId = new Guid(comboBoxTimeNorm.SelectedValue.ToString()),
						Load = Convert.ToDecimal(textBoxLoad.Text)
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
