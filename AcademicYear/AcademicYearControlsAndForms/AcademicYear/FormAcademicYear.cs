using AcademicYearControlsAndForms.AcademicPlan;
using AcademicYearControlsAndForms.Contingent;
using AcademicYearControlsAndForms.LecturerWorkload;
using AcademicYearControlsAndForms.SeasonDates;
using AcademicYearControlsAndForms.StreamLesson;
using AcademicYearControlsAndForms.TimeNorm;
using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AcademicYearControlsAndForms.AcademicYear
{
    public partial class FormAcademicYear : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAcademicYearService _service;

		public FormAcademicYear(IAcademicYearService service, Guid? id = null) : base(id)
		{
			InitializeComponent();
			_service = service;
        }

		private void FormAcademicYear_Load(object sender, EventArgs e)
        {
            StandartForm_Load();
		}

        protected override void LoadData()
        {
            if (tabPageAcademicPlans.Controls.Count == 0)
            {
                var controlAP = Container.Resolve<ControlAcademicPlan>();
                controlAP.Dock = DockStyle.Fill;
                tabPageAcademicPlans.Controls.Add(controlAP);
            }
            (tabPageAcademicPlans.Controls[0] as ControlAcademicPlan).LoadData(_id.Value);

            if (tabPageTimeNorms.Controls.Count == 0)
            {
                var controlSL = Container.Resolve<ControlStreamLesson>();
                controlSL.Dock = DockStyle.Fill;
                tabPageStreamLessons.Controls.Add(controlSL);
            }
            (tabPageStreamLessons.Controls[0] as ControlStreamLesson).LoadData(_id.Value);

            if (tabPageTimeNorms.Controls.Count == 0)
            {
                var controlTN = Container.Resolve<ControlTimeNorm>();
                controlTN.Dock = DockStyle.Fill;
                tabPageTimeNorms.Controls.Add(controlTN);
            }
            (tabPageTimeNorms.Controls[0] as ControlTimeNorm).LoadData(_id.Value);

            if (tabPageContingents.Controls.Count == 0)
            {
                var controlC = Container.Resolve<ControlContingent>();
                controlC.Dock = DockStyle.Fill;
                tabPageContingents.Controls.Add(controlC);
            }
            (tabPageContingents.Controls[0] as ControlContingent).LoadData(_id.Value);

            if (tabPageSeasonDates.Controls.Count == 0)
            {
                var controlSD = Container.Resolve<ControlSeasonDates>();
                controlSD.Dock = DockStyle.Fill;
                tabPageSeasonDates.Controls.Add(controlSD);
            }
            (tabPageSeasonDates.Controls[0] as ControlSeasonDates).LoadData(_id.Value);

            if (tabPageLecturerWorkload.Controls.Count == 0)
            {
                var controlLW = Container.Resolve<ControlLecturerWorkload>();
                controlLW.Dock = DockStyle.Fill;
                tabPageLecturerWorkload.Controls.Add(controlLW);
            }
            (tabPageLecturerWorkload.Controls[0] as ControlLecturerWorkload).LoadData(_id.Value);

            var result = _service.GetAcademicYear(new AcademicYearGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
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

        protected override bool Save()
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
                    ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
					return false;
				}
			}
			else
			{
				MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}
	}
}