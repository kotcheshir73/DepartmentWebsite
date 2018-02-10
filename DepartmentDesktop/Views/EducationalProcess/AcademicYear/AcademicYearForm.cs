using DepartmentDesktop.Views.EducationalProcess.AcademicPlan;
using DepartmentDesktop.Views.EducationalProcess.Contingent;
using DepartmentDesktop.Views.EducationalProcess.SeasonDates;
using DepartmentDesktop.Views.EducationalProcess.TimeNorm;
using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using Microsoft.Practices.Unity;
using System;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.AcademicYear
{
    public partial class AcademicYearForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAcademicYearService _service;

        private Guid? _id = null;

		public AcademicYearForm(IAcademicYearService service, Guid? id = null)
		{
			InitializeComponent();
			_service = service;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

		private void AcademicYearForm_Load(object sender, EventArgs e)
        {
            var controlAP = Container.Resolve<AcademicPlanControl>();

            controlAP.Left = 0;
            controlAP.Top = 0;
            controlAP.Height = Height - 60;
            controlAP.Width = Width - 15;
            controlAP.Anchor = (((AnchorStyles.Top
                    | AnchorStyles.Bottom)
                    | AnchorStyles.Left)
                    | AnchorStyles.Right);

            tabPageAcademicPlans.Controls.Add(controlAP);

            var controlTN = Container.Resolve<TimeNormControl>();

            controlTN.Left = 0;
            controlTN.Top = 0;
            controlTN.Height = Height - 60;
            controlTN.Width = Width - 15;
            controlTN.Anchor = (((AnchorStyles.Top
                    | AnchorStyles.Bottom)
                    | AnchorStyles.Left)
                    | AnchorStyles.Right);

            tabPageTimeNorms.Controls.Add(controlTN);

            var controlC = Container.Resolve<ContingentControl>();

            controlC.Left = 0;
            controlC.Top = 0;
            controlC.Height = Height - 60;
            controlC.Width = Width - 15;
            controlC.Anchor = (((AnchorStyles.Top
                    | AnchorStyles.Bottom)
                    | AnchorStyles.Left)
                    | AnchorStyles.Right);

            tabPageContingent.Controls.Add(controlC);

            var controlSD = Container.Resolve<SeasonDatesControl>();

            controlSD.Left = 0;
            controlSD.Top = 0;
            controlSD.Height = Height - 60;
            controlSD.Width = Width - 15;
            controlSD.Anchor = (((AnchorStyles.Top
                    | AnchorStyles.Bottom)
                    | AnchorStyles.Left)
                    | AnchorStyles.Right);

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
