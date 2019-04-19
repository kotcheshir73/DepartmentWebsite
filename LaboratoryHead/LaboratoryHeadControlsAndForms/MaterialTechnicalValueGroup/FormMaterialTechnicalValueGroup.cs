using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using LaboratoryHeadInterfaces.BindingModels;
using LaboratoryHeadInterfaces.IServices;
using System;
using System.Windows.Forms;
using Tools;
using Unity;

namespace LaboratoryHeadControlsAndForms.MaterialTechnicalValueGroup
{
    public partial class FormMaterialTechnicalValueGroup : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMaterialTechnicalValueGroupService _service;

        public FormMaterialTechnicalValueGroup(IMaterialTechnicalValueGroupService service, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
        }

        private void FormMaterialTechnicalValueGroup_Load(object sender, EventArgs e)
        {
            StandartForm_Load();
        }

        protected override void LoadData()
        {
            var result = _service.GetMaterialTechnicalValueGroup(new MaterialTechnicalValueGroupGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
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
                if (!int.TryParse(textBoxOrder.Text, out int cost))
                {
                    return false;
                }
            }
            return true;
        }

        protected override bool Save()
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
                    ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}