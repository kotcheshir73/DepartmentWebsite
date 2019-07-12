using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using LaboratoryHeadInterfaces.BindingModels;
using LaboratoryHeadInterfaces.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace LaboratoryHeadControlsAndForms.SoftwareRecord
{
    public partial class FormSoftwareRecord : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ISoftwareRecordService _service;

        public FormSoftwareRecord(ISoftwareRecordService service, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
        }

        protected override bool LoadComponents()
        {
            var resultMTV = _service.GetMaterialTechnicalValues(new MaterialTechnicalValueGetBindingModel { });
            if (!resultMTV.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке мат.тех.ценностей возникла ошибка: ", resultMTV.Errors);
                return false;
            }

            comboBoxMaterialTechnicalValue.ValueMember = "Value";
            comboBoxMaterialTechnicalValue.DisplayMember = "Display";
            comboBoxMaterialTechnicalValue.DataSource = resultMTV.Result.List
                .Select(d => new { Value = d.Id, Display = d.InventoryNumber }).ToList();
            comboBoxMaterialTechnicalValue.SelectedItem = null;


            var resultS = _service.GetSoftwares(new SoftwareGetBindingModel { });
            if (!resultS.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке мат.тех.ценностей возникла ошибка: ", resultS.Errors);
                return false;
            }

            comboBoxSoftware.ValueMember = "Value";
            comboBoxSoftware.DisplayMember = "Display";
            comboBoxSoftware.DataSource = resultS.Result.List
                .Select(x => new { Value = x.Id, Display = string.Format("{0} {1}", x.SoftwareName, x.SoftwareKey) }).ToList();
            comboBoxSoftware.SelectedItem = null;

            return true;
        }

        protected override void LoadData()
        {
            var result = _service.GetSoftwareRecord(new SoftwareRecordGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxMaterialTechnicalValue.SelectedValue = entity.MaterialTechnicalValueId;
            comboBoxSoftware.SelectedValue = entity.SoftwareId;
            dateTimePickerDateSetup.Value = entity.DateSetup;
            textBoxSetupDescription.Text = entity.SetupDescription;
            textBoxClaimNumber.Text = entity.ClaimNumber;
        }

        protected override bool CheckFill()
        {
            if (string.IsNullOrEmpty(comboBoxMaterialTechnicalValue.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(comboBoxSoftware.Text))
            {
                return false;
            }
            return true;
        }

        protected override bool Save()
        {
            ResultService result;
            if (!_id.HasValue)
            {
                result = _service.CreateSoftwareRecord(new SoftwareRecordSetBindingModel
                {
                    MaterialTechnicalValueId = new Guid(comboBoxMaterialTechnicalValue.SelectedValue.ToString()),
                    SoftwareId = new Guid(comboBoxSoftware.SelectedValue.ToString()),
                    DateSetup = dateTimePickerDateSetup.Value,
                    SetupDescription = textBoxSetupDescription.Text,
                    ClaimNumber = textBoxClaimNumber.Text
                });
            }
            else
            {
                result = _service.UpdateSoftwareRecord(new SoftwareRecordSetBindingModel
                {
                    Id = _id.Value,
                    MaterialTechnicalValueId = new Guid(comboBoxMaterialTechnicalValue.SelectedValue.ToString()),
                    SoftwareId = new Guid(comboBoxSoftware.SelectedValue.ToString()),
                    DateSetup = dateTimePickerDateSetup.Value,
                    SetupDescription = textBoxSetupDescription.Text,
                    ClaimNumber = textBoxClaimNumber.Text
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