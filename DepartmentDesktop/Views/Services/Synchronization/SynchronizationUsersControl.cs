using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity.Attributes;
using Unity;
using DepartmentService.IServices;

namespace DepartmentDesktop.Views.Services.Synchronization
{
    public partial class SynchronizationUsersControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAdministrationProcess _process;

        public SynchronizationUsersControl(IAdministrationProcess process)
        {
            InitializeComponent();
            _process = process;
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            try
            {
                var result = _process.SynchronizationUsers();
                if (result.Succeeded)
                {
                    MessageBox.Show("Синхронизация выполнена успешно", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Program.PrintErrorMessage("", result.Errors);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
