using AuthenticationInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using System;
using System.Windows.Forms;

namespace AuthenticationControlsAndForms.Services.DataBaseWork
{
    public partial class ImportDataBaseControl : UserControl
    {
        private readonly IAdministrationProcess _process;

        public ImportDataBaseControl(IAdministrationProcess process)
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

        private void ButtonSaveJson_Click(object sender, EventArgs e)
        {
            if (textBoxFolderPathJson.Text != "")
            {
                var result = _process.ImportDataToJson(textBoxFolderPathJson.Text);
                if (!result.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                }
                MessageBox.Show("Сохранено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ButtonFileName_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "backup files(*.bak)|*.bak|all files(*.*)|*.*";
            sfd.FileName = "backup_database_" + DateTime.Now.ToShortDateString().Replace("/", "_");
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                textBoxFileNameBackUp.Text = sfd.FileName;
            }
        }

        private void ButtonSaveBackUp_Click(object sender, EventArgs e)
        {
            if (textBoxFileNameBackUp.Text != "")
            {
                var result = _process.CreateBackUp(textBoxFileNameBackUp.Text);
                if (!result.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("При создании backup возникла ошибка: ", result.Errors);
                }
                MessageBox.Show("Сохранено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}