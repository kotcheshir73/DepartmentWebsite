using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using System;
using System.Windows.Forms;

namespace ExaminationControlsAndForms.TicketTemplateParagraphRun
{
    public partial class FormTicketTemplateParagraphRunProperties : Form
    {
        private Guid? _ticketTemplateParagraphRunPropertiesId;

        private Guid? _ticketTemplateParagraphRunId;

        public TicketTemplateParagraphRunPropertiesViewModel SetTicketTemplateParagraphRunPropertiesViewModel
        {
            set
            {
                if (value != null)
                {
                    _ticketTemplateParagraphRunPropertiesId = value.Id;
                    _ticketTemplateParagraphRunId = value.TicketTemplateParagraphRunId;
                    checkBoxBold.Checked = value.RunBold;
                    checkBoxItalic.Checked = value.RunItalic;
                    checkBoxUnderline.Checked = value.RunUnderline;
                    textBoxSize.Text = value.RunSize;
                }
            }
        }

        public TicketTemplateParagraphRunPropertiesSetBindingModel GetTicketTemplateParagraphRunPropertiesSetBindingModel
        {
            get
            {
                return new TicketTemplateParagraphRunPropertiesSetBindingModel
                {
                    Id = _ticketTemplateParagraphRunPropertiesId.Value,
                    TicketTemplateParagraphRunId = _ticketTemplateParagraphRunId.Value,
                    RunBold = checkBoxBold.Checked,
                    RunItalic = checkBoxItalic.Checked,
                    RunUnderline = checkBoxUnderline.Checked,
                    RunSize = textBoxSize.Text
                };
            }
        }

        public Guid? GetTicketTemplateParagraphRunPropertiesId
        {
            get { return _ticketTemplateParagraphRunPropertiesId; }
        }

        public FormTicketTemplateParagraphRunProperties()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}