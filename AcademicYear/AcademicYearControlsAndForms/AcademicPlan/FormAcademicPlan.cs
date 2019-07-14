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

        public FormAcademicPlan(IAcademicPlanService service, Guid? ayId = null, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _ayId = ayId;
        }

        protected override bool LoadComponents()
        {
            var resultAY = _service.GetAcademicYears(new AcademicYearGetBindingModel { Id = _ayId });
            if (!resultAY.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
                return false;
            }

            var resultED = _service.GetEducationDirections(new EducationDirectionGetBindingModel { });
            if (!resultED.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке направлений возникла ошибка: ", resultED.Errors);
                return false;
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

            return true;
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
            if (entity.AcademicCourses.HasValue)
            {
                var courses = (AcademicCourse)Enum.ToObject(typeof(AcademicCourse), entity.AcademicCourses);
                checkBox1.Checked = (courses & AcademicCourse.Course_1) == AcademicCourse.Course_1;
                checkBox2.Checked = (courses & AcademicCourse.Course_2) == AcademicCourse.Course_2;
                checkBox3.Checked = (courses & AcademicCourse.Course_3) == AcademicCourse.Course_3;
                checkBox4.Checked = (courses & AcademicCourse.Course_4) == AcademicCourse.Course_4;
            }
        }

        protected override bool CheckFill()
        {
            if (comboBoxAcademicYear.SelectedValue == null)
            {
                return false;
            }
            return true;
        }

        protected override bool Save()
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
                    AcademicCourses = courses.HasValue ? Convert.ToInt32(courses) : (int?)null
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
    }
}