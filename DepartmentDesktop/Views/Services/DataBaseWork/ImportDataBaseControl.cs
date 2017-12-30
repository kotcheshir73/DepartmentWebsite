using DepartmentService.IServices;
using System;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Services.DataBaseWork
{
    public partial class ImportDataBaseControl : UserControl
    {
        private readonly IAdministrationProcessServer _service;

        public ImportDataBaseControl(IAdministrationProcessServer service)
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
                var result = _service.ImportDataToJson(textBoxFolderPath.Text);
                if (!result.Succeeded)
                {
                    Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                }
                MessageBox.Show("", "Сохранено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
