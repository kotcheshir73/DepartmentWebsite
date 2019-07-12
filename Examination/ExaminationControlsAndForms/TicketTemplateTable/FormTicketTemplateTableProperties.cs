using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using System;
using System.Windows.Forms;

namespace ExaminationControlsAndForms.TicketTemplateTable
{
    public partial class FormTicketTemplateTableProperties : Form
    {
        private Guid? _ticketTemplateTablePropertiesId;

        private Guid? _ticketTemplateTableId;

        public TicketTemplateTablePropertiesViewModel SetTicketTemplateTablePropertiesViewModel
        {
            set
            {
                if (value != null)
                {
                    _ticketTemplateTablePropertiesId = value.Id;
                    _ticketTemplateTableId = value.TicketTemplateTableId;

                    textBoxLayoutType.Text = value.LayoutType;
                    textBoxLookNoHorizontalBand.Text = value.LookNoHorizontalBand;
                    textBoxLookNoVerticalBand.Text = value.LookNoVerticalBand;
                    textBoxLookValue.Text = value.LookValue;
                    textBoxWidth.Text = value.Width;

                    textBoxLookFirstColumn.Text = value.LookFirstColumn;
                    textBoxLookFirstRow.Text = value.LookFirstRow;
                    textBoxLookLastColumn.Text = value.LookLastColumn;
                    textBoxLookLastRow.Text = value.LookLastRow;

                    textBoxTopColor.Text = value.BorderTopColor;
                    textBoxTopSize.Text = value.BorderTopSize;
                    textBoxTopSpace.Text = value.BorderTopSpace;
                    textBoxTopValue.Text = value.BorderTopValue;

                    textBoxBottomColor.Text = value.BorderBottomColor;
                    textBoxBottomSize.Text = value.BorderBottomSize;
                    textBoxBottomSpace.Text = value.BorderBottomSpace;
                    textBoxBottomValue.Text = value.BorderBottomValue;

                    textBoxLeftColor.Text = value.BorderLeftColor;
                    textBoxLeftSize.Text = value.BorderLeftSize;
                    textBoxLeftSpace.Text = value.BorderLeftSpace;
                    textBoxLeftValue.Text = value.BorderLeftValue;

                    textBoxRightColor.Text = value.BorderRightColor;
                    textBoxRightSize.Text = value.BorderRightSize;
                    textBoxRightSpace.Text = value.BorderRightSpace;
                    textBoxRightValue.Text = value.BorderRightValue;
                }
            }
        }

        public TicketTemplateTablePropertiesSetBindingModel GetTicketTemplateTablePropertiesSetBindingModel
        {
            get
            {
                return new TicketTemplateTablePropertiesSetBindingModel
                {
                    Id = _ticketTemplateTablePropertiesId.Value,
                    TicketTemplateTableId = _ticketTemplateTableId.Value,

                    LayoutType = textBoxLayoutType.Text,
                    LookNoHorizontalBand = textBoxLookNoHorizontalBand.Text,
                    LookNoVerticalBand = textBoxLookNoVerticalBand.Text,
                    LookValue = textBoxLookValue.Text,
                    Width = textBoxWidth.Text,

                    LookFirstColumn = textBoxLookFirstColumn.Text,
                    LookFirstRow = textBoxLookFirstRow.Text,
                    LookLastColumn = textBoxLookLastColumn.Text,
                    LookLastRow = textBoxLookLastRow.Text,

                    BorderTopColor = textBoxTopColor.Text,
                    BorderTopSize = textBoxTopSize.Text,
                    BorderTopSpace = textBoxTopSpace.Text,
                    BorderTopValue = textBoxTopValue.Text,

                    BorderBottomColor = textBoxBottomColor.Text,
                    BorderBottomSize = textBoxBottomSize.Text,
                    BorderBottomSpace = textBoxBottomSpace.Text,
                    BorderBottomValue = textBoxBottomValue.Text,

                    BorderLeftColor = textBoxLeftColor.Text,
                    BorderLeftSize = textBoxLeftSize.Text,
                    BorderLeftSpace = textBoxLeftSpace.Text,
                    BorderLeftValue = textBoxLeftValue.Text,

                    BorderRightColor = textBoxRightColor.Text,
                    BorderRightSize = textBoxRightSize.Text,
                    BorderRightSpace = textBoxRightSpace.Text,
                    BorderRightValue = textBoxRightValue.Text
                };
            }
        }

        public Guid? GetTicketTemplateTablePropertiesId
        {
            get { return _ticketTemplateTablePropertiesId; }
        }

        public FormTicketTemplateTableProperties()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}