using ControlsAndForms.Messangers;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;
using Unity;

namespace ExaminationControlsAndForms.Services
{
    public partial class FormExaminationTemplateUploadTickets : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private ITicketProcess _process;

        private Guid _examinationTemplateId;

        public FormExaminationTemplateUploadTickets(ITicketProcess process, Guid? examinationTemplateId = null)
        {
            InitializeComponent();
            _process = process;
            _examinationTemplateId = examinationTemplateId.Value;
        }

        private void ButtonFileName_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                Filter = "docx|*.docx"
            };
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                textBoxFileName.Text = sfd.FileName;
            }
        }

        private void ButtonUploadTickets_Click(object sender, EventArgs e)
        {
            buttonFileName.ForeColor =
                SystemColors.ControlText;
            if(string.IsNullOrEmpty(textBoxFileName.Text))
            {
                buttonFileName.ForeColor = Color.Red;
            }
            var result = _process.UploadTickets(new TicketProcessUploadTicketsBindingModel
            {
                ExaminationTemplateId = _examinationTemplateId,
                FileName = textBoxFileName.Text
            });
            if (result.Succeeded)
            {
                MessageBox.Show("Билеты выгружены", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ErrorMessanger.PrintErrorMessage("При выгрузке возникла ошибка: ", result.Errors);
            }
        }
    }
}