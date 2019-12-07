using ControlsAndForms.Messangers;
using DatabaseContext;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace LearningProgressControlsAndForms.Reports
{
    public partial class ReportLessonConducted : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonConductedService _service;

        private readonly ILearningProgressProcess _process;

        private Guid? _ayId = null;

        private Guid? _edId = null;

        private Guid? _dId = null;

        private Guid? _tnId = null;

        private string _semester;

        public ReportLessonConducted(IDisciplineLessonConductedService service, ILearningProgressProcess process, Guid? ayId = null, Guid? edId = null, Guid? dId = null, Guid? tnId = null,
            string semester = null)
        {
            InitializeComponent();
            _service = service;
            _process = process;
            _ayId = ayId;
            _edId = edId;
            _dId = dId;
            _tnId = tnId;
            _semester = semester;
        }

        private void ReportLessonConducted_Load(object sender, EventArgs e)
        {
            var resultSemesters = _process.GetSemesters(new LearningProcessSemesterBindingModel
            {
                AcademicYearId = _ayId.Value,
                EducationDirectionId = _edId.Value,
                DisciplineId = _dId.Value,
                UserId = DepartmentUserManager.UserId.Value
            });

            if (!resultSemesters.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке семестров возникла ошибка: ", resultSemesters.Errors);
                return;
            }

            var result = _process.GetStudentGroups(new LearningProcessStudentGroupBindingModel
            {
                EducationDirectionId = _edId.Value,
                Semesters = resultSemesters.Result
            });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке учебных групп возникла ошибка: ", result.Errors);
                return;
            }
            comboBoxStudentGroups.ValueMember = "Value";
            comboBoxStudentGroups.DisplayMember = "Display";
            comboBoxStudentGroups.DataSource = result.Result.Select(y => new { Value = y.Id, Display = y.GroupName }).ToList();
        }

        private void ComboBoxStudentGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStudentGroups.SelectedIndex > -1)
            {
                ReportParameter parameter = new ReportParameter("ReportParameterTitle", string.Format("{0} группы {1}", Text, comboBoxStudentGroups.Text));
                reportViewerReport.LocalReport.SetParameters(parameter);

                var data = _process.GetLessonConducteds(new LessonConductedsBindingModel
                {
                    DisciplineId = _dId.Value,
                    StudentGroupId = new Guid(comboBoxStudentGroups.SelectedValue.ToString()),
                    TimeNormId = _tnId.Value,
                    Semester = _semester
                });
                ReportDataSource source = new ReportDataSource("DataSetDisciplineLessonConductedStudents", data.Result);
                reportViewerReport.LocalReport.DataSources.Clear();
                reportViewerReport.LocalReport.DataSources.Add(source);
                reportViewerReport.RefreshReport();
            }
        }
    }
}