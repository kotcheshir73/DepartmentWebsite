using AuthenticationInterfaces.BindingModels;
using AuthenticationInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using Enums;
using System;
using Tools;
using Unity;

namespace AuthenticationControlsAndForms.Access
{
    public partial class FormAccess : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAccessService _service;

        private Guid _roleId;

        public FormAccess(IAccessService service, Guid roleId, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _roleId = roleId;
        }

        protected override bool LoadComponents()
        {
            foreach (var elem in Enum.GetValues(typeof(AccessOperation)))
            {
                comboBoxAccessOperation.Items.Add(elem.ToString());
            }
            comboBoxAccessOperation.SelectedItem = null;

            foreach (var elem in Enum.GetValues(typeof(AccessType)))
            {
                comboBoxAccessType.Items.Add(elem.ToString());
            }
            comboBoxAccessType.SelectedItem = null;

            return true;
        }

        protected override void LoadData()
        {
            var result = _service.GetAccess(new AccessGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxAccessOperation.SelectedIndex = comboBoxAccessOperation.Items.IndexOf(entity.Operation);
            comboBoxAccessType.SelectedIndex = comboBoxAccessType.Items.IndexOf(entity.AccessType);
        }

        protected override bool CheckFill()
        {
            if (string.IsNullOrEmpty(comboBoxAccessOperation.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(comboBoxAccessType.Text))
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
                result = _service.CreateAccess(new AccessSetBindingModel
                {
                    AccessType = comboBoxAccessType.Text,
                    Operation = comboBoxAccessOperation.Text,
                    RoleId = _roleId
                });
            }
            else
            {
                result = _service.UpdateAccess(new AccessSetBindingModel
                {
                    Id = _id.Value,
                    AccessType = comboBoxAccessType.Text,
                    Operation = comboBoxAccessOperation.Text,
                    RoleId = _roleId
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