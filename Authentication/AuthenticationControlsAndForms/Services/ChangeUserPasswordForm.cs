using AuthenticationInterfaces.BindingModels;
using AuthenticationInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace AuthenticationControlsAndForms.Services
{
    public partial class ChangeUserPasswordForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAuthenticationProcess _process;

        private Guid _id;

        public ChangeUserPasswordForm(IAuthenticationProcess process, Guid id)
        {
            InitializeComponent();
            _process = process;
            _id = id;
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBoxPassword.Text))
            {
                ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("Портал", "Введите пароль") });
                return;
            }

            if(textBoxPassword.Text != textBoxConfirm.Text)
            {
                ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("Портал", "Пароль не совпадает с подтверждением") });
                return;
            }

            var result = _process.ChangePassword(new ChangePasswordBindingModels { Id = _id, NewPassword = textBoxPassword.Text });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке студентов возникла ошибка: ", result.Errors);
            }
            else
            {
                MessageBox.Show("Пароль изменен", "Портал", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
