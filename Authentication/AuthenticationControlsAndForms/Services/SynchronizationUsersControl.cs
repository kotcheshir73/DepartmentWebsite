﻿using AuthenticationInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using System;
using System.Windows.Forms;
using Unity;

namespace AuthenticationControlsAndForms.Services.Synchronization
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
                    ErrorMessanger.PrintErrorMessage("", result.Errors);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}