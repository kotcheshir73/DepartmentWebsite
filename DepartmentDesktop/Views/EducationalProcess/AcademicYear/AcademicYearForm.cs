using DepartmentDesktop.Views.EducationalProcess.AcademicPlan;
using DepartmentDesktop.Views.EducationalProcess.AcademicYear.StreamLesson;
using DepartmentDesktop.Views.EducationalProcess.Contingent;
using DepartmentDesktop.Views.EducationalProcess.SeasonDates;
using DepartmentDesktop.Views.EducationalProcess.TimeNorm;
using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;
using Unity;

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
            if (_id.HasValue)
            {
                LoadData();
			}
		}

		private void LoadData()
        {
            if (tabPageAcademicPlans.Controls.Count == 0)
            {
                var controlAP = Container.Resolve<AcademicPlanControl>();
                controlAP.Dock = DockStyle.Fill;
                tabPageAcademicPlans.Controls.Add(controlAP);
            }
            (tabPageAcademicPlans.Controls[0] as AcademicPlanControl).LoadData(_id.Value);

            if (tabPageTimeNorms.Controls.Count == 0)
            {
                var controlSL = Container.Resolve<StreamLessonControl>();
                controlSL.Dock = DockStyle.Fill;
                tabPageStreamLessons.Controls.Add(controlSL);
            }
            (tabPageStreamLessons.Controls[0] as StreamLessonControl).LoadData(_id.Value);

            if (tabPageTimeNorms.Controls.Count == 0)
            {
                var controlTN = Container.Resolve<TimeNormControl>();
                controlTN.Dock = DockStyle.Fill;
                tabPageTimeNorms.Controls.Add(controlTN);
            }
            (tabPageTimeNorms.Controls[0] as TimeNormControl).LoadData(_id.Value);

            if (tabPageContingents.Controls.Count == 0)
            {
                var controlC = Container.Resolve<ContingentControl>();
                controlC.Dock = DockStyle.Fill;
                tabPageContingents.Controls.Add(controlC);
            }
            (tabPageContingents.Controls[0] as ContingentControl).LoadData(_id.Value);

            if (tabPageSeasonDates.Controls.Count == 0)
            {
                var controlSD = Container.Resolve<SeasonDatesControl>();
                controlSD.Dock = DockStyle.Fill;
                tabPageSeasonDates.Controls.Add(controlSD);
            }
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
					result = _service.CreateAcademicYear(new AcademicYearSetBindingModel
					{
						Title = textBoxTitle.Text
					});
				}
				else
				{
					result = _service.UpdateAcademicYear(new AcademicYearSetBindingModel
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
