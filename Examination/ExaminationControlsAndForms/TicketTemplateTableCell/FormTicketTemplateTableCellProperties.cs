using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using System;
using System.Windows.Forms;

namespace ExaminationControlsAndForms.TicketTemplateTableCell
{
    public partial class FormTicketTemplateTableCellProperties : Form
    {
        private Guid? _ticketTemplateTableCellPropertiesId;

        private Guid? _ticketTemplateTableCellId;

        public TicketTemplateTableCellPropertiesViewModel SetTicketTemplateTableCellPropertiesViewModel
        {
            set
            {
                if (value != null)
                {
                    _ticketTemplateTableCellPropertiesId = value.Id;
                    _ticketTemplateTableCellId = value.TicketTemplateTableCellId;

                    textBoxTableCellWidth.Text = value.TableCellWidth;
                    textBoxGridSpan.Text = value.GridSpan;
                    textBoxVerticalMerge.Text = value.VerticalMerge;
                    textBoxShadingColor.Text = value.ShadingColor;
                    textBoxShadingFill.Text = value.ShadingFill;
                    textBoxShadingValue.Text = value.ShadingValue;
                }
            }
        }

        public TicketTemplateTableCellPropertiesSetBindingModel GetTicketTemplateTableCellPropertiesSetBindingModel
        {
            get
            {
                return new TicketTemplateTableCellPropertiesSetBindingModel
                {
                    Id = _ticketTemplateTableCellPropertiesId.Value,
                    TicketTemplateTableCellId = _ticketTemplateTableCellId.Value,

                    TableCellWidth = textBoxTableCellWidth.Text,
                    GridSpan = textBoxGridSpan.Text,
                    VerticalMerge = textBoxVerticalMerge.Text,
                    ShadingColor = textBoxShadingColor.Text,
                    ShadingFill = textBoxShadingFill.Text,
                    ShadingValue = textBoxShadingValue.Text
                };
            }
        }

        public Guid? GetTicketTemplateTableCellPropertiesId
        {
            get { return _ticketTemplateTableCellPropertiesId; }
        }

        public FormTicketTemplateTableCellProperties()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}