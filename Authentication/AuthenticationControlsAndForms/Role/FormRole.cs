using AuthenticationControlsAndForms.Access;
using AuthenticationInterfaces.BindingModels;
using AuthenticationInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AuthenticationControlsAndForms.Role
{
    public partial class FormRole : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IRoleService _service;

        public FormRole(IRoleService service, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
        }

        private void FormRole_Load(object sender, EventArgs e)
        {
            StandartForm_Load();
        }

        protected override void LoadData()
        {
            if (tabPageAccesses.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlAccess>();
                control.Dock = DockStyle.Fill;
                tabPageAccesses.Controls.Add(control);
            }
            (tabPageAccesses.Controls[0] as ControlAccess).LoadData(_id.Value);
            var result = _service.GetRole(new RoleGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            textBoxRoleName.Text = entity.RoleName;
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxRoleName.Text))
            {
                return false;
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
                    result = _service.CreateRole(new RoleSetBindingModel
                    {
                        RoleName = textBoxRoleName.Text
                    });
                }
                else
                {
                    result = _service.UpdateRole(new RoleSetBindingModel
                    {
                        Id = _id.Value,
                        RoleName = textBoxRoleName.Text
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