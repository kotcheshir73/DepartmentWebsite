using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;
using Unity;

namespace LearningProgressControlsAndForms.Reports
{
    public partial class ReportDisciplineStudentRecordBySubgroup : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineStudentRecordService _service;

        private Guid? _dId = null;

        private Guid? _sgId = null;

        private string _semester = null;

        public ReportDisciplineStudentRecordBySubgroup(IDisciplineStudentRecordService service, Guid? dId = null, Guid? sgId = null, string semester = null)
        {
            InitializeComponent();
            _service = service;
            _dId = dId;
            _sgId = sgId;
            _semester = semester;
        }

        private void FormDisciplineStudentRecordBySubgroup_Load(object sender, EventArgs e)
        {
            ReportParameter parameter = new ReportParameter("ReportParameterTitle", Text);
            reportViewerReport.LocalReport.SetParameters(parameter);

            var data = _service.GetDisciplineStudentRecords(new DisciplineStudentRecordGetBindingModel
            {
                DisciplineId = _dId,
                StudentGroupId = _sgId,
                Semester = _semester
            });
            if (data.Result.List.Count > 0)
            {
                parameter = new ReportParameter("ReportParameterDiscipline", data.Result.List[0].Discipline);
                reportViewerReport.LocalReport.SetParameters(parameter);
                parameter = new ReportParameter("ReportParameterStudentGroup", data.Result.List[0].StudentGroup);
                reportViewerReport.LocalReport.SetParameters(parameter);
            }
            ReportDataSource source = new ReportDataSource("DataSetDisciplineStudentRecords", data.Result.List);
            reportViewerReport.LocalReport.DataSources.Clear();
            reportViewerReport.LocalReport.DataSources.Add(source);
            reportViewerReport.RefreshReport();
        }
    }
}