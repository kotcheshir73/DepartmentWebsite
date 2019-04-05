using DepartmentService.BindingModels;
using DepartmentService.IServices;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;
using Unity;

namespace DepartmentDesktop.Views.LaboratoryHead.MaterialTechnicalValue
{
    public partial class MaterialTechnicalValuePassport : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        
        private readonly IMaterialTechnicalValueService _serviceMTV;

        private readonly IMaterialTechnicalValueRecordService _serviceMTVR;

        private Guid? _id = null;

        public MaterialTechnicalValuePassport(IMaterialTechnicalValueService serviceMTV, IMaterialTechnicalValueRecordService serviceMTVR, Guid? id = null)
        {
            InitializeComponent();
            _serviceMTV = serviceMTV;
            _serviceMTVR = serviceMTVR;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void MaterialTechnicalValuePassport_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                var entity = _serviceMTV.GetMaterialTechnicalValue(new MaterialTechnicalValueGetBindingModel { Id = _id.Value });

                ReportParameter parameterName = new ReportParameter("ReportParameterName",
                    entity.Result.FullName);
                reportViewerReport.LocalReport.SetParameters(parameterName);

                ReportParameter parameterInventoryNumber = new ReportParameter("ReportParameterInventoryNumber",
                    string.Format("Инв. номер: {0}", entity.Result.InventoryNumber));
                reportViewerReport.LocalReport.SetParameters(parameterInventoryNumber);

                var dataSource = _serviceMTVR.GetMaterialTechnicalValueRecords(new MaterialTechnicalValueRecordGetBindingModel
                {
                    MaterialTechnicalValueId = _id.Value
                });
                ReportDataSource source = new ReportDataSource("DataSetRecords", dataSource.Result.List);
                reportViewerReport.LocalReport.DataSources.Clear();
                reportViewerReport.LocalReport.DataSources.Add(source);

                reportViewerReport.RefreshReport();
            }
        }
    }
}
