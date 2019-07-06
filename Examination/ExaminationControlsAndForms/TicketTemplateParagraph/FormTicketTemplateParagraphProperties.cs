using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using System;
using System.Windows.Forms;

namespace ExaminationControlsAndForms.TicketTemplateParagraph
{
    public partial class FormTicketTemplateParagraphProperties : Form
    {
        private Guid? _ticketTemplateParagraphPropertiesId;

        private Guid? _ticketTemplateParagraphId;

        public TicketTemplateParagraphPropertiesViewModel SetTicketTemplateParagraphPropertiesViewModel
        {
            set
            {
                if (value != null)
                {
                    _ticketTemplateParagraphPropertiesId = value.Id;
                    _ticketTemplateParagraphId = value.TicketTemplateParagraphId;

                    textBoxJustification.Text = value.Justification;

                    textBoxLine.Text = value.SpacingBetweenLinesLine;
                    textBoxLineRule.Text = value.SpacingBetweenLinesLineRule;
                    textBoxBefore.Text = value.SpacingBetweenLinesBefore;
                    textBoxAfter.Text = value.SpacingBetweenLinesAfter;

                    textBoxFirstLine.Text = value.IndentationFirstLine;
                    textBoxHanging.Text = value.IndentationHanging;
                    textBoxLeft.Text = value.IndentationLeft;
                    textBoxRight.Text = value.IndentationRight;

                    checkBoxBold.Checked = value.RunBold;
                    checkBoxItalic.Checked = value.RunItalic;
                    checkBoxUnderline.Checked = value.RunUnderline;
                    textBoxSize.Text = value.RunSize;
                }
            }
        }

        public TicketTemplateParagraphPropertiesSetBindingModel GetTicketTemplateParagraphPropertiesSetBindingModel
        {
            get
            {
                return new TicketTemplateParagraphPropertiesSetBindingModel
                {
                    Id = _ticketTemplateParagraphPropertiesId.Value,
                    TicketTemplateParagraphId = _ticketTemplateParagraphId.Value,

                    IndentationFirstLine = textBoxFirstLine.Text,
                    IndentationHanging = textBoxHanging.Text,
                    IndentationLeft = textBoxLeft.Text,
                    IndentationRight = textBoxRight.Text,

                    Justification = textBoxJustification.Text,

                    SpacingBetweenLinesAfter = textBoxAfter.Text,
                    SpacingBetweenLinesBefore = textBoxBefore.Text,
                    SpacingBetweenLinesLine = textBoxLine.Text,
                    SpacingBetweenLinesLineRule = textBoxLineRule.Text,

                    RunBold = checkBoxBold.Checked,
                    RunItalic = checkBoxItalic.Checked,
                    RunUnderline = checkBoxUnderline.Checked,
                    RunSize = textBoxSize.Text
                };
            }
        }

        public Guid? GetTicketTemplateParagraphPropertiesId
        {
            get { return _ticketTemplateParagraphPropertiesId; }
        }

        public FormTicketTemplateParagraphProperties()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}