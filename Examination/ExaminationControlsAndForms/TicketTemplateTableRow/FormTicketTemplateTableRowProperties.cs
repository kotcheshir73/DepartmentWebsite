using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using System;
using System.Windows.Forms;

namespace ExaminationControlsAndForms.TicketTemplateTableRow
{
    public partial class FormTicketTemplateTableRowProperties : Form
    {
        private Guid? _ticketTemplateTableRowPropertiesId;

        private Guid? _ticketTemplateTableRowId;

        public TicketTemplateTableRowPropertiesViewModel SetTicketTemplateTableRowPropertiesViewModel
        {
            set
            {
                if (value != null)
                {
                    _ticketTemplateTableRowPropertiesId = value.Id;
                    _ticketTemplateTableRowId = value.TicketTemplateTableRowId;

                    textBoxTableRowHeight.Text = value.TableRowHeight;
                    textBoxCantSplit.Text = value.CantSplit;
                }
            }
        }

        public TicketTemplateTableRowPropertiesSetBindingModel GetTicketTemplateTableRowPropertiesSetBindingModel
        {
            get
            {
                return new TicketTemplateTableRowPropertiesSetBindingModel
                {
                    Id = _ticketTemplateTableRowPropertiesId.Value,
                    TicketTemplateTableRowId = _ticketTemplateTableRowId.Value,

                    TableRowHeight = textBoxTableRowHeight.Text,
                    CantSplit = textBoxCantSplit.Text
                };
            }
        }

        public Guid? GetTicketTemplateTableRowPropertiesId
        {
            get { return _ticketTemplateTableRowPropertiesId; }
        }

        public FormTicketTemplateTableRowProperties()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}