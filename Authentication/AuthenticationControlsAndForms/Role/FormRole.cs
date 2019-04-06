﻿using AuthenticationControlsAndForms.Access;
using AuthenticationInterfaces.BindingModels;
using AuthenticationInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using System;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AuthenticationControlsAndForms.Role
{
    public partial class FormRole : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IRoleService _service;

        private Guid? _id = null;

        public FormRole(IRoleService service, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void FormRole_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
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

        private bool Save()
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