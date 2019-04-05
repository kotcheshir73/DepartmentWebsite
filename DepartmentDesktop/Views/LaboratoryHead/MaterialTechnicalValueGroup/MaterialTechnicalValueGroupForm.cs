using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;
using Unity;

namespace DepartmentDesktop.Views.LaboratoryHead.MaterialTechnicalValueGroup
{
    public partial class MaterialTechnicalValueGroupForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMaterialTechnicalValueGroupService _service;

        private Guid? _id = null;

        public MaterialTechnicalValueGroupForm(IMaterialTechnicalValueGroupService service, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void MaterialTechnicalValueGroupForm_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            var result = _service.GetMaterialTechnicalValueGroup(new MaterialTechnicalValueGroupGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            textBoxGroupName.Text = entity.GroupName;
            textBoxOrder.Text = entity.Order.ToString();
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxGroupName.Text))
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
                    result = _service.CreateMaterialTechnicalValueGroup(new MaterialTechnicalValueGroupSetBindingModel
                    {
                        GroupName = textBoxGroupName.Text,
                        Order = Convert.ToInt32(textBoxOrder.Text)
                    });
                }
                else
                {
                    result = _service.UpdateMaterialTechnicalValueGroup(new MaterialTechnicalValueGroupSetBindingModel
                    {
                        Id = _id.Value,
                        GroupName = textBoxGroupName.Text,
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
