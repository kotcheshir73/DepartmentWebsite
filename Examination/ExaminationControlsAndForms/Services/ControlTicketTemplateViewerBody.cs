using ExaminationInterfaces.ViewModels;
using System;
using System.Windows.Forms;
using Unity;

namespace ExaminationControlsAndForms.Services
{
    public partial class ControlTicketTemplateViewerBody : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

       // private readonly ITicketTemplateParagraphService _service;

        private Guid _ticketTemplateId;

        private Guid? _id;

        private Guid? _ticketTemplateBodyPropertiesId;

        public ControlTicketTemplateViewerBody()
        {
            InitializeComponent();
        }

        public void LoadView(TicketTemplateBodyViewModel model, Guid ticketTemplateId)
        {
            _id = model.Id;
            _ticketTemplateId = ticketTemplateId;
            if (model.TicketTemplateBodyPropertiesViewModel != null)
            {
                _ticketTemplateBodyPropertiesId = model.TicketTemplateBodyPropertiesId;

                textBoxWidth.Text = model.TicketTemplateBodyPropertiesViewModel.PageSizeWidth;
                textBoxHeight.Text = model.TicketTemplateBodyPropertiesViewModel.PageSizeHeight;
                textBoxOrient.Text = model.TicketTemplateBodyPropertiesViewModel.PageSizeOrient;

                textBoxTop.Text = model.TicketTemplateBodyPropertiesViewModel.PageMarginTop;
                textBoxBottom.Text = model.TicketTemplateBodyPropertiesViewModel.PageMarginBottom;
                textBoxLeft.Text = model.TicketTemplateBodyPropertiesViewModel.PageMarginLeft;
                textBoxRight.Text = model.TicketTemplateBodyPropertiesViewModel.PageMarginRight;

                textBoxGutter.Text = model.TicketTemplateBodyPropertiesViewModel.PageMarginGutter;
                textBoxFooter.Text = model.TicketTemplateBodyPropertiesViewModel.PageMarginFooter;
            }
            if (model.TicketTemplateParagraphPageViewModel != null)
            {
                panelParagraphs.Controls.Clear();
                int top = 0;
                foreach (var run in model.TicketTemplateParagraphPageViewModel.List)
                {
                    var control = Container.Resolve<ControlTicketTemplateViewerParagraph>();
                    control.Location = new System.Drawing.Point(0, top);
                    control.LoadView(run, model.Id);
                    panelParagraphs.Controls.Add(control);
                    top += control.Height + 5;
                }
            }
            buttonAddParagraph.Visible = _id.HasValue;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {

        }
    }
}
