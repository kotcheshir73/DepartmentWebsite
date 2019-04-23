using AcademicYearControlsAndForms.AcademicPlanRecord;
using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using BaseInterfaces.BindingModels;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using Enums;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AcademicYearControlsAndForms.AcademicPlan
{
    public partial class FormAcademicPlan : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAcademicPlanService _service;

        private Guid? _ayId = null;

        public FormAcademicPlan(IAcademicPlanService service, Guid? ayId = null, Guid ? id = null) : base(id)
		{
			InitializeComponent();
			_service = service;
            _ayId = ayId;
        }

		private void FormAcademicPlan_Load(object sender, EventArgs e)
		{
			var resultAY = _service.GetAcademicYears(new AcademicYearGetBindingModel { Id = _ayId });
			if (!resultAY.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
				return;
			}

			var resultED = _service.GetEducationDirections(new EducationDirectionGetBindingModel { });
			if (!resultED.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке направлений возникла ошибка: ", resultED.Errors);
				return;
			}

			foreach (var elem in Enum.GetValues(typeof(AcademicLevel)))
			{
				comboBoxAcademicLevel.Items.Add(elem.ToString());
			}

			comboBoxAcademicYear.ValueMember = "Value";
			comboBoxAcademicYear.DisplayMember = "Display";
			comboBoxAcademicYear.DataSource = resultAY.Result.List
				.Select(ay => new { Value = ay.Id, Display = ay.Title }).ToList();
			comboBoxAcademicYear.SelectedValue = _ayId;

			comboBoxEducationDirection.ValueMember = "Value";
			comboBoxEducationDirection.DisplayMember = "Display";
			comboBoxEducationDirection.DataSource = resultED.Result.List
				.Select(ed => new { Value = ed.Id, Display = ed.Cipher + " " + ed.Title }).ToList();
			comboBoxEducationDirection.SelectedItem = null;

            StandartForm_Load();
		}

		protected override void LoadData()
		{
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlAcademicPlanRecord>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
			(tabPageRecords.Controls[0] as ControlAcademicPlanRecord).LoadData(_id.Value);

			var result = _service.GetAcademicPlan(new AcademicPlanGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

            if (entity.EducationDirectionId.HasValue)
            {
                comboBoxEducationDirection.SelectedValue = entity.EducationDirectionId;
            }
			comboBoxAcademicYear.SelectedValue = entity.AcademicYearId;
			comboBoxAcademicLevel.SelectedIndex = comboBoxAcademicLevel.Items.IndexOf(entity.AcademicLevel);
            if (entity.AcademicCourses.HasValue)
            {
                var courses = (AcademicCourse)Enum.ToObject(typeof(AcademicCourse), entity.AcademicCourses);
                checkBox1.Checked = (courses & AcademicCourse.Course_1) == AcademicCourse.Course_1;
                checkBox2.Checked = (courses & AcademicCourse.Course_2) == AcademicCourse.Course_2;
                checkBox3.Checked = (courses & AcademicCourse.Course_3) == AcademicCourse.Course_3;
                checkBox4.Checked = (courses & AcademicCourse.Course_4) == AcademicCourse.Course_4;
            }
		}

		private bool CheckFill()
		{
			if (comboBoxAcademicYear.SelectedValue == null)
			{
				return false;
			}
			if (string.IsNullOrEmpty(comboBoxAcademicLevel.Text))
			{
				return false;
			}
			return true;
		}

        protected override bool Save()
		{
			if (CheckFill())
			{
                AcademicCourse? courses = null;
                if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked)
                {
                    courses = new AcademicCourse();
                    if (checkBox1.Checked)
                    {
                        courses = courses | AcademicCourse.Course_1;
                    }
                    if (checkBox2.Checked)
                    {
                        courses = courses | AcademicCourse.Course_2;
                    }
                    if (checkBox3.Checked)
                    {
                        courses = courses | AcademicCourse.Course_3;
                    }
                    if (checkBox4.Checked)
                    {
                        courses = courses | AcademicCourse.Course_4;
                    }
                }
				ResultService result;
				if (!_id.HasValue)
				{
					result = _service.CreateAcademicPlan(new AcademicPlanSetBindingModel
					{
						EducationDirectionId = comboBoxEducationDirection.SelectedValue != null ? new Guid(comboBoxEducationDirection.SelectedValue.ToString()) : (Guid?)null,
						AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
						AcademicLevel = comboBoxAcademicLevel.Text,
						AcademicCourses = courses.HasValue ? Convert.ToInt32(courses) : (int?)null
					});
				}
				else
				{
					result = _service.UpdateAcademicPlan(new AcademicPlanSetBindingModel
					{
						Id = _id.Value,
                        EducationDirectionId = comboBoxEducationDirection.SelectedValue != null ? new Guid(comboBoxEducationDirection.SelectedValue.ToString()) : (Guid?)null,
                        AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
						AcademicLevel = comboBoxAcademicLevel.Text,
                        AcademicCourses = courses.HasValue ? Convert.ToInt32(courses) : (int?)null
                    });
				}
				if (result.Succeeded)
				{
					if(result.Result != null)
					{
						if(result.Result is Guid)
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