using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DepartmentService.IServices;

namespace DepartmentDesktop.Views.Services.DataBaseWork
{
    public partial class ExportDataBaseControl : UserControl
    {
        private readonly IAdministrationProcessServer _service;

        public ExportDataBaseControl(IAdministrationProcessServer service)
        {
            InitializeComponent();
            _service = service;
        }

        private void ButtonFolderPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBoxFolderPath.Text = fbd.SelectedPath;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxFolderPath.Text != "")
            {
                var result = _service.ExportDataFromJson(textBoxFolderPath.Text);
                if (!result.Succeeded)
                {
                    Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                }
                MessageBox.Show("", "Сохранено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
