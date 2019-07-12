using AuthenticationInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using System;
using System.Windows.Forms;
using Unity;

namespace AuthenticationControlsAndForms.Services.Synchronization
{
    public partial class SynchronizationRolesControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAuthenticationProcess _process;

        public SynchronizationRolesControl(IAuthenticationProcess process)
        {
            InitializeComponent();
            _process = process;
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            try
            {
                var result = _process.SynchronizationRolesAndAccess();
                if (result.Succeeded)
                {
                    MessageBox.Show("Синхронизация выполнена успешно", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ErrorMessanger.PrintErrorMessage("", result.Errors);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}