using ControlsAndForms.Messangers;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using ExaminationInterfaces.ViewModels;
using System;
using System.Windows.Forms;
using Tools;
using Unity;

namespace ExaminationControlsAndForms.Services
{
    public partial class ControlTicketTemplateViewerParagraph : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ITicketTemplateParagraphService _service;

        private Guid _ticketTemplateBodyId;

        private Guid? _id;

        private Guid? _ticketTemplatePropertiesId;

        public ControlTicketTemplateViewerParagraph(ITicketTemplateParagraphService service)
        {
            InitializeComponent();
            _service = service;
        }

        public void LoadView(TicketTemplateParagraphViewModel model, Guid ticketTemplateBodyId)
        {
            _id = model.Id;
            _ticketTemplateBodyId = ticketTemplateBodyId;
            numericUpDownOrder.Value = model.Order;
            if (model.TicketTemplateParagraphPropertiesViewModel != null)
            {
                _ticketTemplatePropertiesId = model.TicketTemplateParagraphPropertiesId;
                textBoxJustification.Text = model.TicketTemplateParagraphPropertiesViewModel.Justification;

                textBoxLine.Text = model.TicketTemplateParagraphPropertiesViewModel.SpacingBetweenLinesLine;
                textBoxLineRule.Text = model.TicketTemplateParagraphPropertiesViewModel.SpacingBetweenLinesLineRule;
                textBoxBefore.Text = model.TicketTemplateParagraphPropertiesViewModel.SpacingBetweenLinesBefore;
                textBoxAfter.Text = model.TicketTemplateParagraphPropertiesViewModel.SpacingBetweenLinesAfter;

                textBoxFirstLine.Text = model.TicketTemplateParagraphPropertiesViewModel.IndentationFirstLine;
                textBoxHanging.Text = model.TicketTemplateParagraphPropertiesViewModel.IndentationHanging;
                textBoxLeft.Text = model.TicketTemplateParagraphPropertiesViewModel.IndentationLeft;
                textBoxRight.Text = model.TicketTemplateParagraphPropertiesViewModel.IndentationRight;

                checkBoxBold.Checked = model.TicketTemplateParagraphPropertiesViewModel.RunBold;
                checkBoxItalic.Checked = model.TicketTemplateParagraphPropertiesViewModel.RunItalic;
                checkBoxUnderline.Checked = model.TicketTemplateParagraphPropertiesViewModel.RunUnderline;
                textBoxSize.Text = model.TicketTemplateParagraphPropertiesViewModel.RunSize;
            }
            if (model.TicketTemplateParagraphRunPageViewModel != null)
            {
                panelRuns.Controls.Clear();
                int top = 0;
                foreach (var run in model.TicketTemplateParagraphRunPageViewModel.List)
                {
                    var control = Container.Resolve<ControlTicketTemplateViewerRun>();
                    control.Location = new System.Drawing.Point(0, top);
                    control.LoadView(run, model.Id);
                    panelRuns.Controls.Add(control);
                    top += control.Height;
                }
            }
            buttonAddRun.Visible = _id.HasValue;
        }

        private void ButtonShowProperties_Click(object sender, System.EventArgs e)
        {
            if(panelParagraphProperties.Visible)
            {
                panelParagraphProperties.Visible = false;
            }
            else
            {
                panelParagraphProperties.Visible = true;
            }
        }

        private void ButtonSave_Click(object sender, System.EventArgs e)
        {
            ResultService result;
            if (!_id.HasValue)
            {
                result = _service.CreateTicketTemplateParagraph(new TicketTemplateParagraphSetBindingModel
                {
                    Order = Convert.ToInt32(numericUpDownOrder.Value),
                    TicketTemplateBodyId = _ticketTemplateBodyId,
                    TicketTemplateParagraphPropertiesSetBindingModel = new TicketTemplateParagraphPropertiesSetBindingModel
                    {
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
                    }
                });
            }
            else
            {
                result = _service.UpdateTicketTemplateParagraph(new TicketTemplateParagraphSetBindingModel
                {
                    Id = _id.Value,
                    Order = Convert.ToInt32(numericUpDownOrder.Value),
                    TicketTemplateBodyId = _ticketTemplateBodyId,
                    TicketTemplateParagraphPropertiesId = _ticketTemplatePropertiesId,
                    TicketTemplateParagraphPropertiesSetBindingModel = _ticketTemplatePropertiesId.HasValue ? 
                    new TicketTemplateParagraphPropertiesSetBindingModel
                    {
                        Id = _ticketTemplatePropertiesId.Value,
                        TicketTemplateParagraphId = _id.Value,
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
                    } : null
                });
            }
            if (result.Succeeded)
            {
                MessageBox.Show("Сохранено", "Портал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result.Result != null && result.Result is Guid)
                {
                    var model = _service.GetTicketTemplateParagraph(new TicketTemplateParagraphGetBindingModel { Id = (Guid)result.Result });
                    if (model.Succeeded)
                    {
                        LoadView(model.Result, _ticketTemplateBodyId);
                    }
                }
            }
            else
            {
                ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
            }
        }

        private void ButtonDel_Click(object sender, System.EventArgs e)
        {
            if (_id.HasValue)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
                var result = _service.DeleteTicketTemplateParagraph(new TicketTemplateParagraphGetBindingModel { Id = _id.Value });
                if (!result.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                    return;
                }
            }
            Enabled = false;
        }

        private void ButtonAddRun_Click(object sender, EventArgs e)
        {
            int top = 0;
            foreach(var contrl in panelRuns.Controls)
            {
                if((contrl as ControlTicketTemplateViewerRun).Top >= top)
                {
                    top = (contrl as ControlTicketTemplateViewerRun).Top;
                    top += (contrl as ControlTicketTemplateViewerRun).Height;
                }
            }
            var control = Container.Resolve<ControlTicketTemplateViewerRun>();
            control.Location = new System.Drawing.Point(0, top);
            control.LoadView(_id.Value);
            panelRuns.Controls.Add(control);
        }
    }
}