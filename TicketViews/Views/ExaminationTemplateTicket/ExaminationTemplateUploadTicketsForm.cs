using System;
using System.Drawing;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using Unity;
using Unity.Attributes;

namespace TicketViews.Views.ExaminationTemplateTicket
{
    public partial class ExaminationTemplateUploadTicketsForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private ITicketProcess _process;

        public ExaminationTemplateUploadTicketsForm(ITicketProcess process, ITicketTemplateService serviceTT, Guid? examinationTemplateId = null)
        {
            InitializeComponent();
            _process = process;
            ticketTemplateElement.Service = serviceTT;
            ticketTemplateElement.ExaminationTemplateId = examinationTemplateId;
        }

        private void ButtonFileName_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                Filter = "doc|*.doc|docx|*.docx"
            };
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                textBoxFileName.Text = sfd.FileName;
            }
        }

        private void ButtonUploadTickets_Click(object sender, EventArgs e)
        {
            labelTicketTemplate.ForeColor =
            buttonFileName.ForeColor =
                SystemColors.ControlText;
            if (!ticketTemplateElement.Id.HasValue)
            {
                labelTicketTemplate.ForeColor = Color.Red;
            }
            if(string.IsNullOrEmpty(textBoxFileName.Text))
            {
                buttonFileName.ForeColor = Color.Red;
            }
            var result = _process.UploadTickets(new TicketProcessUploadTicketsBindingModel
            {
                TicketTemplateId = ticketTemplateElement.Id.Value,
                FileName = textBoxFileName.Text
            });
            if (result.Succeeded)
            {
                MessageBox.Show("Билеты выгружены", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Program.PrintErrorMessage("При выгрузке возникла ошибка: ", result.Errors);
            }
        }
    }
}