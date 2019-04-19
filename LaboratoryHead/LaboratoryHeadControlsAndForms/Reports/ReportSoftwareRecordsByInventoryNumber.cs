using LaboratoryHeadInterfaces.BindingModels;
using LaboratoryHeadInterfaces.Interfaces;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;
using Unity;

namespace LaboratoryHeadControlsAndForms.Reports
{
    public partial class ReportSoftwareRecordsByInventoryNumber : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ILaboratoryProcess _process;

        public ReportSoftwareRecordsByInventoryNumber(ILaboratoryProcess process)
        {
            InitializeComponent();
            _process = process;
        }

        private void ReportSoftwareRecordsByInventoryNumber_Load(object sender, EventArgs e)
        {

        }

        private void ButtonGet_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxInventoryNumber.Text))
            {
                ReportParameter parameter = new ReportParameter("ReportParameterTitle",
                    string.Format("Список ПО по инвю номеру №{0}", textBoxInventoryNumber.Text));
                reportViewerReport.LocalReport.SetParameters(parameter);

                var dataSourceSoftware = _process.GetSoftwareRecordsByInventoryNumber(new LaboratoryProcessGetSoftwareRecordsByClassroomBindingModel
                {
                    InventoryNumber = textBoxInventoryNumber.Text
                });
                ReportDataSource sourceSoftware = new ReportDataSource("DataSetSoftwareRecord", dataSourceSoftware.Result.ListFirst);
                reportViewerReport.LocalReport.DataSources.Clear();
                reportViewerReport.LocalReport.DataSources.Add(sourceSoftware);

                reportViewerReport.RefreshReport();
            }
        }
    }
}