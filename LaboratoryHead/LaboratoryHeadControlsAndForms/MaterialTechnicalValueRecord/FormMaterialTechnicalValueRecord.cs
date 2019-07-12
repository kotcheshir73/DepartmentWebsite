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

namespace LaboratoryHeadControlsAndForms.MaterialTechnicalValueRecord
{
    public partial class FormMaterialTechnicalValueRecord : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMaterialTechnicalValueRecordService _service;

        private Guid _mtvId;

        public FormMaterialTechnicalValueRecord(IMaterialTechnicalValueRecordService service, Guid mtvId, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _mtvId = mtvId;
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
            comboBoxMaterialTechnicalValue.SelectedValue = _mtvId;

            var resultMTVG = _service.GetMaterialTechnicalValueGroups(new MaterialTechnicalValueGroupGetBindingModel { });
            if (!resultMTVG.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке групп описаний мат.тех.ценностей возникла ошибка: ", resultMTV.Errors);
                return false;
            }

            comboBoxMaterialTechnicalValueGroup.ValueMember = "Value";
            comboBoxMaterialTechnicalValueGroup.DisplayMember = "Display";
            comboBoxMaterialTechnicalValueGroup.DataSource = resultMTVG.Result.List
                .Select(d => new { Value = d.Id, Display = d.GroupName }).ToList();
            comboBoxMaterialTechnicalValueGroup.SelectedItem = null;

            return true;
        }

        protected override void LoadData()
        {
            var result = _service.GetMaterialTechnicalValueRecord(new MaterialTechnicalValueRecordGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxMaterialTechnicalValue.SelectedValue = entity.MaterialTechnicalValueId;
            comboBoxMaterialTechnicalValueGroup.SelectedValue = entity.MaterialTechnicalValueGroupId;
            textBoxFieldName.Text = entity.FieldName;
            textBoxFieldValue.Text = entity.FieldValue;
            textBoxOrder.Text = entity.Order.ToString();
        }

        protected override bool CheckFill()
        {
            if (string.IsNullOrEmpty(comboBoxMaterialTechnicalValue.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(comboBoxMaterialTechnicalValueGroup.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxFieldName.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxOrder.Text))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBoxOrder.Text))
            {
                if (!int.TryParse(textBoxOrder.Text, out int cost))
                {
                    return false;
                }
            }
            return true;
        }

        protected override bool Save()
        {
            ResultService result;
            if (!_id.HasValue)
            {
                result = _service.CreateMaterialTechnicalValueRecord(new MaterialTechnicalValueRecordSetBindingModel
                {
                    MaterialTechnicalValueId = new Guid(comboBoxMaterialTechnicalValue.SelectedValue.ToString()),
                    MaterialTechnicalValueGroupId = new Guid(comboBoxMaterialTechnicalValueGroup.SelectedValue.ToString()),
                    FieldName = textBoxFieldName.Text,
                    FieldValue = textBoxFieldValue.Text,
                    Order = Convert.ToInt32(textBoxOrder.Text)
                });
            }
            else
            {
                result = _service.UpdateMaterialTechnicalValueRecord(new MaterialTechnicalValueRecordSetBindingModel
                {
                    Id = _id.Value,
                    MaterialTechnicalValueId = new Guid(comboBoxMaterialTechnicalValue.SelectedValue.ToString()),
                    MaterialTechnicalValueGroupId = new Guid(comboBoxMaterialTechnicalValueGroup.SelectedValue.ToString()),
                    FieldName = textBoxFieldName.Text,
                    FieldValue = textBoxFieldValue.Text,
                    Order = Convert.ToInt32(textBoxOrder.Text)
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