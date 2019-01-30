using DepartmentDesktop.Views.LearningProgress.DisciplineStudentRecord;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.LearningProgress
{
    public partial class StudentsDistributionControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ILearningProgressProcess _process;

        private readonly IAcademicYearService _serviceAY;

        private readonly IEducationDirectionService _serviceED;

        private readonly IStudentGroupService _serviceSD;

        public StudentsDistributionControl(ILearningProgressProcess process, IAcademicYearService serviceAY, IEducationDirectionService serviceED, IStudentGroupService serviceSD)
        {
            InitializeComponent();
            _process = process;
            _serviceAY = serviceAY;
            _serviceED = serviceED;
            _serviceSD = serviceSD;
        }

        public void LoadData()
        {
            var resultAY = _serviceAY.GetAcademicYears( new AcademicYearGetBindingModel { });
            if (!resultAY.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
                return;
            }

            comboBoxAcademicYear.ValueMember = "Value";
            comboBoxAcademicYear.DisplayMember = "Display";
            comboBoxAcademicYear.DataSource = resultAY.Result.List.Select(y => new {Value = y.Id, Display = y.Title}).ToList();
            comboBoxAcademicYear.SelectedValue = _process.GetCurrentAcademicYear().Result;

            var resultED = _serviceED.GetEducationDirections(new EducationDirectionGetBindingModel { });
            if (!resultED.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке направлений возникла ошибка: ", resultED.Errors);
                return;
            }

            comboBoxEducationDirection.ValueMember = "Value";
            comboBoxEducationDirection.DisplayMember = "Display";
            comboBoxEducationDirection.DataSource = resultED.Result.List.Select(y => new { Value = y.Id, Display = y.ShortName }).ToList();
        }

        private void GetDisciplineDetails()
        {
            if (comboBoxAcademicYear.SelectedValue != null && comboBoxEducationDirection.SelectedValue != null)
            {
                var result = _process.GetDisciplines(new LearningProcessDisciplineBindingModel
                {
                    AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                    EducationDirectionId = new Guid(comboBoxEducationDirection.SelectedValue.ToString()),
                    UserId = AuthorizationService.UserId.Value
                });
                if (!result.Succeeded)
                {
                    Program.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", result.Errors);
                    return;
                }

                comboBoxDisciplines.ValueMember = "Value";
                comboBoxDisciplines.DisplayMember = "Display";
                comboBoxDisciplines.DataSource = result.Result
                    .Select(d => new { Value = d.Id, Display = d.DisciplineName }).ToList();
            }
        }

        private void GetStudentGroups()
        {
            if (comboBoxDisciplines.SelectedValue != null && comboBoxEducationDirection.SelectedValue != null)
            {
                var resultSemesters = _process.GetSemesters(new LearningProcessSemesterBindingModel
                {
                    AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                    EducationDirectionId = new Guid(comboBoxEducationDirection.SelectedValue.ToString()),
                    DisciplineId = new Guid(comboBoxDisciplines.SelectedValue.ToString()),
                    UserId = AuthorizationService.UserId.Value
                });
                
                if (!resultSemesters.Succeeded)
                {
                    Program.PrintErrorMessage("При загрузке семестров возникла ошибка: ", resultSemesters.Errors);
                    return;
                }
                comboBoxSemester.Items.Clear();
                foreach (var elem in resultSemesters.Result)
                {
                    comboBoxSemester.Items.Add(elem.ToString());
                }

                var result = _process.GetStudentGroups(new LearningProcessStudentGroupBindingModel
                {
                    EducationDirectionId = new Guid(comboBoxEducationDirection.SelectedValue.ToString()),
                    Semesters = resultSemesters.Result
                });
                if (!result.Succeeded)
                {
                    Program.PrintErrorMessage("При загрузке учебных групп возникла ошибка: ", result.Errors);
                    return;
                }
                comboBoxStudentGroups.ValueMember = "Value";
                comboBoxStudentGroups.DisplayMember = "Display";
                comboBoxStudentGroups.DataSource = result.Result.Select(y => new { Value = y.Id, Display = y.GroupName }).ToList();
            }
        }

        private void LoadConotrol()
        {
            if (comboBoxStudentGroups.SelectedValue != null && !string.IsNullOrEmpty(comboBoxSemester.Text))
            {
                panelControl.Controls.Clear();
                var control = Container.Resolve<DisciplineStudentRecordControl>();
                control.Dock = DockStyle.Fill;
                control.LoadData(new Guid(comboBoxDisciplines.SelectedValue.ToString()), new Guid(comboBoxStudentGroups.SelectedValue.ToString()),
                    comboBoxSemester.Text);
                panelControl.Controls.Add(control);
            }
        }

        private void comboBoxAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDisciplineDetails();
        }

        private void comboBoxEducationDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDisciplineDetails();
            GetStudentGroups();
        }

        private void comboBoxDisciplines_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetStudentGroups();
        }

        private void comboBoxStudentGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadConotrol();
        }

        private void comboBoxSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadConotrol();
        }
    }
}
