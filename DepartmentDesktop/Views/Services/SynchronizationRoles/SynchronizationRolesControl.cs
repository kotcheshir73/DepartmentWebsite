using DepartmentService.IServices;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.Services.SynchronizationRoles
{
    public partial class SynchronizationRolesControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAdministrationProcess _process;

        public SynchronizationRolesControl(IAdministrationProcess process)
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
                    Program.PrintErrorMessage("", result.Errors);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
