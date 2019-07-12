using BaseInterfaces.BindingModels;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using LaboratoryHeadControlsAndForms.MaterialTechnicalValueRecord;
using LaboratoryHeadInterfaces.BindingModels;
using LaboratoryHeadInterfaces.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace LaboratoryHeadControlsAndForms.MaterialTechnicalValue
{
    public partial class FormMaterialTechnicalValue : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMaterialTechnicalValueService _service;

        public FormMaterialTechnicalValue(IMaterialTechnicalValueService service, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
        }

        protected override bool LoadComponents()
        {
            var resultC = _service.GetClassrooms(new ClassroomGetBindingModel { });
            if (!resultC.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке аудиторий возникла ошибка: ", resultC.Errors);
                return false;
            }

            comboBoxClassroom.ValueMember = "Value";
            comboBoxClassroom.DisplayMember = "Display";
            comboBoxClassroom.DataSource = resultC.Result.List
                .Select(d => new { Value = d.Id, Display = d.Number }).ToList();
            comboBoxClassroom.SelectedItem = null;

            return true;
        }

        protected override void LoadData()
        {
            if (tabPageMaterialTechnicalValueRecords.Controls.Count == 0)
            {
                var controlMTV = Container.Resolve<ControlMaterialTechnicalValueRecord>();
                controlMTV.Dock = DockStyle.Fill;
                tabPageMaterialTechnicalValueRecords.Controls.Add(controlMTV);
            }
            (tabPageMaterialTechnicalValueRecords.Controls[0] as ControlMaterialTechnicalValueRecord).LoadData(_id.Value);
            var result = _service.GetMaterialTechnicalValue(new MaterialTechnicalValueGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxClassroom.SelectedValue = entity.ClassroomId;
            dateTimePickerDateInclude.Value = entity.DateInclude;
            textBoxInventoryNumber.Text = entity.InventoryNumber;
            textBoxFullName.Text = entity.FullName;
            textBoxDescription.Text = entity.Description;
            textBoxLocation.Text = entity.Location;
            textBoxCost.Text = entity.Cost.ToString("N2");
            textBoxDeleteReason.Text = entity.DeleteReason;
            if (entity.DateDelete.HasValue)
            {
                dateTimePickerDateDelete.Value = entity.DateDelete.Value;
            }
        }

        private void TextBoxDeleteReason_TextChanged(object sender, EventArgs e)
        {
            dateTimePickerDateDelete.Visible = !string.IsNullOrEmpty(textBoxDeleteReason.Text);
        }

        protected override bool CheckFill()
        {
            if (string.IsNullOrEmpty(comboBoxClassroom.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxInventoryNumber.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxFullName.Text))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBoxCost.Text))
            {
                if (!decimal.TryParse(textBoxCost.Text, out decimal cost))
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
                result = _service.CreateMaterialTechnicalValue(new MaterialTechnicalValueSetBindingModel
                {
                    ClassroomId = new Guid(comboBoxClassroom.SelectedValue.ToString()),
                    DateInclude = dateTimePickerDateInclude.Value,
                    InventoryNumber = textBoxInventoryNumber.Text,
                    FullName = textBoxFullName.Text,
                    Description = textBoxDescription.Text,
                    Location = textBoxLocation.Text,
                    Cost = string.IsNullOrEmpty(textBoxCost.Text) ? 0 : Convert.ToDecimal(textBoxCost.Text),
                    DeleteReason = textBoxDeleteReason.Text,
                    DateDelete = string.IsNullOrEmpty(textBoxDeleteReason.Text) ? (DateTime?)null : dateTimePickerDateDelete.Value
                });
            }
            else
            {
                result = _service.UpdateMaterialTechnicalValue(new MaterialTechnicalValueSetBindingModel
                {
                    Id = _id.Value,
                    ClassroomId = new Guid(comboBoxClassroom.SelectedValue.ToString()),
                    DateInclude = dateTimePickerDateInclude.Value,
                    InventoryNumber = textBoxInventoryNumber.Text,
                    FullName = textBoxFullName.Text,
                    Description = textBoxDescription.Text,
                    Location = textBoxLocation.Text,
                    Cost = string.IsNullOrEmpty(textBoxCost.Text) ? 0 : Convert.ToDecimal(textBoxCost.Text),
                    DeleteReason = textBoxDeleteReason.Text,
                    DateDelete = string.IsNullOrEmpty(textBoxDeleteReason.Text) ? (DateTime?)null : dateTimePickerDateDelete.Value
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