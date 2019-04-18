using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.LaboratoryHead.SoftwareRecord
{
    public partial class SoftwareRecordForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ISoftwareRecordService _service;

        private Guid? _id = null;

        public SoftwareRecordForm(ISoftwareRecordService service, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void SoftwareRecordForm_Load(object sender, EventArgs e)
        {
            var resultMTV = _service.GetMaterialTechnicalValues(new MaterialTechnicalValueGetBindingModel { });
            if (!resultMTV.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке мат.тех.ценностей возникла ошибка: ", resultMTV.Errors);
                return;
            }

            comboBoxMaterialTechnicalValue.ValueMember = "Value";
            comboBoxMaterialTechnicalValue.DisplayMember = "Display";
            comboBoxMaterialTechnicalValue.DataSource = resultMTV.Result.List
                .Select(d => new { Value = d.Id, Display = d.InventoryNumber }).ToList();
            comboBoxMaterialTechnicalValue.SelectedItem = null;


            var resultS = _service.GetSoftwares(new SoftwareGetBindingModel { });
            if (!resultS.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке мат.тех.ценностей возникла ошибка: ", resultS.Errors);
                return;
            }

            comboBoxSoftware.ValueMember = "Value";
            comboBoxSoftware.DisplayMember = "Display";
            comboBoxSoftware.DataSource = resultS.Result.List
                .Select(x => new { Value = x.Id, Display = string.Format("{0} {1}", x.SoftwareName, x.SoftwareKey) }).ToList();
            comboBoxSoftware.SelectedItem = null;

            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            var result = _service.GetSoftwareRecord(new SoftwareRecordGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxMaterialTechnicalValue.SelectedValue = entity.MaterialTechnicalValueId;
            comboBoxSoftware.SelectedValue = entity.SoftwareId;
            dateTimePickerDateSetup.Value = entity.DateSetup;
            textBoxSetupDescription.Text = entity.SetupDescription;
            textBoxClaimNumber.Text = entity.ClaimNumber;
        }

        private bool CheckFill()
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

        private bool Save()
        {
            if (CheckFill())
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
                    Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
