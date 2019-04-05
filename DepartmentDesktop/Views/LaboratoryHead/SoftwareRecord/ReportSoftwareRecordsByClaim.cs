using DepartmentService.BindingModels;
using DepartmentService.IServices;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;
using Unity;

namespace DepartmentDesktop.Views.LaboratoryHead.SoftwareRecord
{
    public partial class ReportSoftwareRecordsByClaim : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ILaboratoryProcess _process;

        public ReportSoftwareRecordsByClaim(ILaboratoryProcess process)
        {
            InitializeComponent();
            _process = process;
        }

        private void ReportSoftwareRecordsByClaim_Load(object sender, EventArgs e)
        {

        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxClaim.Text))
            {
                ReportParameter parameter = new ReportParameter("ReportParameterTitle",
                    string.Format("Список ПО по заявке №{0}", textBoxClaim.Text));
                reportViewerReport.LocalReport.SetParameters(parameter);

                var dataSourceSoftware = _process.GetSoftwareRecordsByClaimNumber(new LaboratoryProcessGetSoftwareRecordsByClassroomBindingModel
                {
                    ClaimNumber = textBoxClaim.Text
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
