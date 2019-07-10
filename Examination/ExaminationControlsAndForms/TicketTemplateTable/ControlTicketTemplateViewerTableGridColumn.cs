using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using System;
using System.Windows.Forms;

namespace ExaminationControlsAndForms.TicketTemplateTable
{
    public partial class ControlTicketTemplateViewerTableGridColumn : UserControl
    {
        private Guid _id;

        private Guid _ticketTemplateTableId;

        public TicketTemplateTableGridColumnSetBindingModel GetModel
        {
            get
            {
                return new TicketTemplateTableGridColumnSetBindingModel
                {
                    Id = _id,
                    TicketTemplateTableId = _ticketTemplateTableId,
                    Order = Convert.ToInt32(numericUpDownOrder.Value),
                    Width = textBoxWidth.Text
                };
            }
        }

        public TicketTemplateTableGridColumnViewModel SetModel
        {
            set
            {
                _id = value.Id;
                _ticketTemplateTableId = value.TicketTemplateTableId;
                numericUpDownOrder.Value = value.Order;
                textBoxWidth.Text = value.Width;
            }
        }

        public ControlTicketTemplateViewerTableGridColumn()
        {
            InitializeComponent();
        }
    }
}