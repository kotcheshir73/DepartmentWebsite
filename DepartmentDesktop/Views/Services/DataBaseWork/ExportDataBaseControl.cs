using AuthenticationServiceInterfaces.Interfaces;
using System;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Services.DataBaseWork
{
    public partial class ExportDataBaseControl : UserControl
    {
        private readonly IAdministrationProcess _process;

        public ExportDataBaseControl(IAdministrationProcess process)
        {
            InitializeComponent();
            _process = process;
        }

        private void ButtonFolderPathJson_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBoxFolderPathJson.Text = fbd.SelectedPath;
            }
        }

        private void ButtonLoadJson_Click(object sender, EventArgs e)
        {
            if (textBoxFolderPathJson.Text != "")
            {
                var result = _process.ExportDataFromJson(textBoxFolderPathJson.Text);
                if (!result.Succeeded)
                {
                    Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                }
                MessageBox.Show("", "Загружено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ButtonFileName_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "backup files(*.bak)|*.bak|all files(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxFileNameBackUp.Text = ofd.FileName;
            }
        }

        private void ButtonRestoreBackUp_Click(object sender, EventArgs e)
        {
            if (textBoxFileNameBackUp.Text != "")
            {
                var result = _process.RestoreBackUp(textBoxFileNameBackUp.Text);
                if (!result.Succeeded)
                {
                    Program.PrintErrorMessage("При восстановлении backup возникла ошибка: ", result.Errors);
                }
                MessageBox.Show("Восстановлено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
