using DepartmentService.BindingModels;
using DepartmentService.IServices;
using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace DepartmentDesktop.Views.LaboratoryHead.SoftwareRecord
{
    public partial class ReportSoftwareRecordsByClassroom : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ILaboratoryProcess _process;

        private readonly IMaterialTechnicalValueService _serviceM;

        public ReportSoftwareRecordsByClassroom(ILaboratoryProcess process, IMaterialTechnicalValueService serviceM)
        {
            InitializeComponent();
            _process = process;
            _serviceM = serviceM;
        }

        private void ReportSoftwareRecordsByClassroom_Load(object sender, EventArgs e)
        {
            var resultC = _serviceM.GetClassrooms(new ClassroomGetBindingModel { });
            if (!resultC.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке аудиторий возникла ошибка: ", resultC.Errors);
                return;
            }

            comboBoxClassroom.ValueMember = "Value";
            comboBoxClassroom.DisplayMember = "Display";
            comboBoxClassroom.DataSource = resultC.Result.List
                .Select(d => new { Value = d.Id, Display = d.Number }).ToList();
            comboBoxClassroom.SelectedItem = null;
        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            if (comboBoxClassroom.SelectedValue != null)
            {
                ReportParameter parameter = new ReportParameter("ReportParameterTitle",
                    string.Format("Список ПО по аудитории №{0}", comboBoxClassroom.Text));
                reportViewerReport.LocalReport.SetParameters(parameter);
                
                var dataSourceSoftware = _process.GetSoftwareRecordsByClassrooms(new LaboratoryProcessGetSoftwareRecordsByClassroomBindingModel
                {
                    ClassroomId = new Guid(comboBoxClassroom.SelectedValue.ToString())
                });
                ReportDataSource sourceMTV = new ReportDataSource("DataSetInventoryNumbers", dataSourceSoftware.Result.ListSecond);
                ReportDataSource sourceSoftware = new ReportDataSource("DataSetSoftwareRecord", dataSourceSoftware.Result.ListFirst);
                reportViewerReport.LocalReport.DataSources.Clear();
                reportViewerReport.LocalReport.DataSources.Add(sourceMTV);
                reportViewerReport.LocalReport.DataSources.Add(sourceSoftware);

                reportViewerReport.RefreshReport();
            }
        }
    }
}
