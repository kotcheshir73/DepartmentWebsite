﻿using BaseInterfaces.BindingModels;
using ControlsAndForms.Messangers;
using LaboratoryHeadInterfaces.BindingModels;
using LaboratoryHeadInterfaces.Interfaces;
using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace LaboratoryHeadControlsAndForms.Reports
{
    public partial class ReportMaterialTechnicalValueByClassroom : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMaterialTechnicalValueService _service;

        public ReportMaterialTechnicalValueByClassroom(IMaterialTechnicalValueService service)
        {
            InitializeComponent();
            _service = service;
        }

        private void ReportMaterialTechnicalValueByClassroom_Load(object sender, EventArgs e)
        {
            var resultC = _service.GetClassrooms(new ClassroomGetBindingModel { });
            if (!resultC.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке аудиторий возникла ошибка: ", resultC.Errors);
                return;
            }

            comboBoxClassroom.ValueMember = "Value";
            comboBoxClassroom.DisplayMember = "Display";
            comboBoxClassroom.DataSource = resultC.Result.List
                .Select(d => new { Value = d.Id, Display = d.Number }).ToList();
            comboBoxClassroom.SelectedItem = null;
        }

        private void ButtonGet_Click(object sender, EventArgs e)
        {
            if (comboBoxClassroom.SelectedValue != null)
            {
                ReportParameter parameter = new ReportParameter("ReportParameterTitle",
                    string.Format("Инвентарный список по аудитории №{0}", comboBoxClassroom.Text));
                reportViewerReport.LocalReport.SetParameters(parameter);

                var dataSource = _service.GetMaterialTechnicalValues(new MaterialTechnicalValueGetBindingModel
                {
                    ClassroomId = new Guid(comboBoxClassroom.SelectedValue.ToString())
                });
                ReportDataSource source = new ReportDataSource("DataSetMTV", dataSource.Result.List);
                reportViewerReport.LocalReport.DataSources.Clear();
                reportViewerReport.LocalReport.DataSources.Add(source);

                reportViewerReport.RefreshReport();
            }
        }
    }
}