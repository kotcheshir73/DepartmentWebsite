using DepartmentDAL;
using DepartmentDesktop.Views.EducationalProcess.AcademicPlan;
using DepartmentDesktop.Views.EducationalProcess.Contingent;
using DepartmentDesktop.Views.EducationalProcess.SeasonDates;
using DepartmentDesktop.Views.EducationalProcess.TimeNorm;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.AcademicYear
{
	public partial class AcademicYearForm : Form
	{
		private readonly IAcademicYearService _service;
 
		private readonly IAcademicPlanService _serviceAP;
 
		private readonly ITimeNormService _serviceTM;
	
		private readonly IContingentService _serviceC;

        private readonly ISeasonDatesService _serviceSD;

        private readonly IAcademicPlanRecordService _serviceAPR;

        private readonly IEducationalProcessService _serviceEP;

        private Guid? _id = null;

		public AcademicYearForm(IAcademicYearService service, IAcademicPlanService serviceAP, ITimeNormService serviceTM, IContingentService serviceC, 
            ISeasonDatesService serviceSD, IAcademicPlanRecordService serviceAPR, IEducationalProcessService serviceEP, Guid? id = null)
		{
			InitializeComponent();
			_service = service;
            _serviceAP = serviceAP;
            _serviceTM = serviceTM;
            _serviceC = serviceC;
            _serviceSD = serviceSD;
            _serviceAPR = serviceAPR;
            _serviceEP = serviceEP;
            _id = id;
		}

		private void AcademicYearForm_Load(object sender, EventArgs e)
        {
            var controlAP = new AcademicPlanControl(_serviceAP, _serviceAPR, _serviceEP)
            {
                Left = 0,
                Top = 0,
                Height = Height - 60,
                Width = Width - 15,
                Anchor = (((AnchorStyles.Top
                        | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right)
            };
            tabPageAcademicPlans.Controls.Add(controlAP);

            var controlTN = new TimeNormControl(_serviceTM)
            {
                Left = 0,
                Top = 0,
                Height = Height - 60,
                Width = Width - 15,
                Anchor = (((AnchorStyles.Top
                        | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right)
            };
            tabPageTimeNorms.Controls.Add(controlTN);

            var controlC = new ContingentControl(_serviceC)
            {
                Left = 0,
                Top = 0,
                Height = Height - 60,
                Width = Width - 15,
                Anchor = (((AnchorStyles.Top
                        | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right)
            };
            tabPageContingent.Controls.Add(controlC);

            var controlSD = new SeasonDatesControl(_serviceSD)
            {
                Left = 0,
                Top = 0,
                Height = Height - 60,
                Width = Width - 15,
                Anchor = (((AnchorStyles.Top
                        | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right)
            };
            tabPageSeasonDates.Controls.Add(controlSD);

            if (_id.HasValue)
			{
				LoadData();
			}
		}

		private void LoadData()
        {
            (tabPageAcademicPlans.Controls[0] as AcademicPlanControl).LoadData(_id.Value);
            (tabPageTimeNorms.Controls[0] as TimeNormControl).LoadData(_id.Value);
            (tabPageContingent.Controls[0] as ContingentControl).LoadData(_id.Value);
            (tabPageSeasonDates.Controls[0] as SeasonDatesControl).LoadData(_id.Value);
            var result = _service.GetAcademicYear(new AcademicYearGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

			textBoxTitle.Text = entity.Title;
		}

		private bool CheckFill()
		{
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
				if (!_id.HasValue)
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
						Id = _id.Value,
						Title = textBoxTitle.Text
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
