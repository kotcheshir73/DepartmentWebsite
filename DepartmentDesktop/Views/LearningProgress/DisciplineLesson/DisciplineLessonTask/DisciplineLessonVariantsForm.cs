using DepartmentService.BindingModels;
using DepartmentService.IServices;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson.DisciplineLessonTask
{
    public partial class DisciplineLessonVariantsForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ILearningProgressProcess _process;

        private Guid? _dlId = null;

        public DisciplineLessonVariantsForm(ILearningProgressProcess process, Guid? dlId)
        {
            InitializeComponent();
            _process = process;
            _dlId = dlId;
        }

        private void DisciplineLessonVariantsForm_Load(object sender, EventArgs e)
        {
            ReportParameter parameter = new ReportParameter("ReportParameterTitle", Text);
            reportViewerReport.LocalReport.SetParameters(parameter);

            var data = _process.GetDisciplineLessonTaskVariants(new GetDisciplineLessonTaskVariantsBindingModel { DisciplineLessonId = _dlId.Value });
            ReportDataSource source = new ReportDataSource("DataSetDisciplineLessonTaskVariant", data.Result);
            reportViewerReport.LocalReport.DataSources.Clear();
            reportViewerReport.LocalReport.DataSources.Add(source);

            reportViewerReport.RefreshReport();
        }
    }
}
