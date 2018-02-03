using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.LoadDistribution
{
	public partial class LoadDistributionForm : Form
	{
		private readonly ILoadDistributionService _service;

		private readonly ILoadDistributionRecordService _serviceLDR;

		private readonly IEducationalProcessService _serviceEP;

		private Guid? _id;

		public LoadDistributionForm(ILoadDistributionService service, ILoadDistributionRecordService serviceLDR, IEducationalProcessService serviceEP, Guid? id = null)
		{
			InitializeComponent();
			_service = service;
			_serviceLDR = serviceLDR;
			_serviceEP = serviceEP;
			_id = id;
		}

		private void LoadDistributionForm_Load(object sender, EventArgs e)
		{
			var resultAY = _service.GetAcademicYears(new AcademicYearGetBindingModel { });
			if (!resultAY.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
				return;
			}

			comboBoxAcademicYear.ValueMember = "Value";
			comboBoxAcademicYear.DisplayMember = "Display";
			comboBoxAcademicYear.DataSource = resultAY.Result.List
				.Select(ay => new { Value = ay.Id, Display = ay.Title }).ToList();
			comboBoxAcademicYear.SelectedItem = null;

			var control = new LoadDistributionRecordControl(_serviceLDR, _serviceEP);
			control.Left = 0;
			control.Top = 0;
			control.Height = Height - 60;
			control.Width = Width - 15;
			control.Anchor = (((AnchorStyles.Top
						| AnchorStyles.Bottom)
						| AnchorStyles.Left)
						| AnchorStyles.Right);
			tabPageRecords.Controls.Add(control);

			if (_id.HasValue)
			{
				LoadData();
			}
		}

		private void LoadData()
		{
			(tabPageRecords.Controls[0] as LoadDistributionRecordControl).LoadData(_id.Value);
			var result = _service.GetLoadDistribution(new LoadDistributionGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;
			
			comboBoxAcademicYear.SelectedValue = entity.AcademicYearId;
		}

		private bool CheckFill()
		{
			if (comboBoxAcademicYear.SelectedValue == null)
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
					result = _service.CreateLoadDistribution(new LoadDistributionRecordBindingModel
					{
						AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString())
					});
				}
				else
				{
					result = _service.UpdateLoadDistribution(new LoadDistributionRecordBindingModel
					{
						Id = _id.Value,
						AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString())
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
