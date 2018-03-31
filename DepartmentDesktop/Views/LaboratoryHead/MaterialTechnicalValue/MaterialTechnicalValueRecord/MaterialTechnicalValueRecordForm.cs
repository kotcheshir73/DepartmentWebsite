using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.LaboratoryHead.MaterialTechnicalValueRecord
{
    public partial class MaterialTechnicalValueRecordForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMaterialTechnicalValueRecordService _service;

        private Guid? _id = null;

        private Guid _mtvId;

        public MaterialTechnicalValueRecordForm(IMaterialTechnicalValueRecordService service, Guid mtvId, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _mtvId = mtvId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void MaterialTechnicalValueRecordForm_Load(object sender, EventArgs e)
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
            comboBoxMaterialTechnicalValue.SelectedValue = _mtvId;

            var resultMTVG = _service.GetMaterialTechnicalValueGroups(new MaterialTechnicalValueGroupGetBindingModel { });
            if (!resultMTVG.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке групп описаний мат.тех.ценностей возникла ошибка: ", resultMTV.Errors);
                return;
            }

            comboBoxMaterialTechnicalValueGroup.ValueMember = "Value";
            comboBoxMaterialTechnicalValueGroup.DisplayMember = "Display";
            comboBoxMaterialTechnicalValueGroup.DataSource = resultMTVG.Result.List
                .Select(d => new { Value = d.Id, Display = d.GroupName }).ToList();
            comboBoxMaterialTechnicalValueGroup.SelectedItem = null;

            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            var result = _service.GetMaterialTechnicalValueRecord(new MaterialTechnicalValueRecordGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxMaterialTechnicalValue.SelectedValue = entity.MaterialTechnicalValueId;
            comboBoxMaterialTechnicalValueGroup.SelectedValue = entity.MaterialTechnicalValueGroupId;
            textBoxFieldName.Text = entity.FieldName;
            textBoxFieldValue.Text = entity.FieldValue;
            textBoxOrder.Text = entity.Order.ToString();
        }

        private bool CheckFill()
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
                int cost = 0;
                if (!int.TryParse(textBoxOrder.Text, out cost))
                {
                    return false;
                }
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
                    result = _service.CreateMaterialTechnicalValueRecord(new MaterialTechnicalValueRecordRecordBindingModel
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
                    result = _service.UpdateMaterialTechnicalValueRecord(new MaterialTechnicalValueRecordRecordBindingModel
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
