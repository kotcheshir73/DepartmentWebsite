using DepartmentDesktop.Views.LaboratoryHead.MaterialTechnicalValueRecord;
using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.LaboratoryHead.MaterialTechnicalValue
{
    public partial class MaterialTechnicalValueForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMaterialTechnicalValueService _service;

        private Guid? _id = null;

        public MaterialTechnicalValueForm(IMaterialTechnicalValueService service, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void MaterialTechnicalValueForm_Load(object sender, EventArgs e)
        {
            var resultC = _service.GetClassrooms(new ClassroomGetBindingModel { });
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

            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            if (tabPageMaterialTechnicalValueRecords.Controls.Count == 0)
            {
                var controlMTV = Container.Resolve<MaterialTechnicalValueRecordControl>();
                controlMTV.Dock = DockStyle.Fill;
                tabPageMaterialTechnicalValueRecords.Controls.Add(controlMTV);
            }
            (tabPageMaterialTechnicalValueRecords.Controls[0] as MaterialTechnicalValueRecordControl).LoadData(_id.Value);
            var result = _service.GetMaterialTechnicalValue(new MaterialTechnicalValueGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
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

        private void textBoxDeleteReason_TextChanged(object sender, EventArgs e)
        {
            dateTimePickerDateDelete.Visible = !string.IsNullOrEmpty(textBoxDeleteReason.Text);
        }

        private bool CheckFill()
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
            if(!string.IsNullOrEmpty(textBoxCost.Text))
            {
                decimal cost = 0;
                if (!decimal.TryParse(textBoxCost.Text, out cost))
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
                    result = _service.CreateMaterialTechnicalValue(new MaterialTechnicalValueRecordBindingModel
                    {
                        ClassroomId = new Guid(comboBoxClassroom.SelectedValue.ToString()),
                        DateInclude = dateTimePickerDateInclude.Value,
                        InventoryNumber = textBoxInventoryNumber.Text,
                        FullName = textBoxFullName.Text,
                        Description = textBoxDescription.Text,
                        Location = textBoxLocation.Text,
                        Cost = string.IsNullOrEmpty(textBoxCost.Text)? 0 : Convert.ToDecimal(textBoxCost.Text),
                        DeleteReason = textBoxDeleteReason.Text,
                        DateDelete = string.IsNullOrEmpty(textBoxDeleteReason.Text) ? (DateTime?)null : dateTimePickerDateDelete.Value
                    });
                }
                else
                {
                    result = _service.UpdateMaterialTechnicalValue(new MaterialTechnicalValueRecordBindingModel
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
