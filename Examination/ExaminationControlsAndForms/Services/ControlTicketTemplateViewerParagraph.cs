using ControlsAndForms.Messangers;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using ExaminationInterfaces.ViewModels;
using System;
using System.Linq;
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

        private Guid? _ticketTemplateBodyId;

        private Guid _id;

        private Guid? _ticketTemplateParagraphPropertiesId;

        private Guid? _ticketTemplateTableCellId;

        public ControlTicketTemplateViewerParagraph(ITicketTemplateParagraphService service)
        {
            InitializeComponent();
            _service = service;
        }

        public TicketTemplateParagraphSetBindingModel GetModel => new TicketTemplateParagraphSetBindingModel
        {
            Id = _id,
            TicketTemplateBodyId = _ticketTemplateBodyId,
            Order = Convert.ToInt32(numericUpDownOrder.Value),
            TicketTemplateParagraphPropertiesId = _ticketTemplateParagraphPropertiesId,
            TicketTemplateTableCellId = _ticketTemplateTableCellId,
            TicketTemplateParagraphPropertiesSetBindingModel  = new TicketTemplateParagraphPropertiesSetBindingModel
            {
                Id = _ticketTemplateParagraphPropertiesId.Value,
                TicketTemplateParagraphId = _id,

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
            },
            TicketTemplateParagraphRunSetBindingModels = panelRuns.Controls.Cast<ControlTicketTemplateViewerRun>()?.Select(x => x.GetModel)?.ToList()
        };

        public void LoadView(TicketTemplateParagraphViewModel model, bool flag = true)
        {
            panelAction.Enabled = flag;

            _id = model.Id;
            _ticketTemplateBodyId = model.TicketTemplateBodyId;
            _ticketTemplateTableCellId = model.TicketTemplateTableCellId;
            numericUpDownOrder.Value = model.Order;
            if (model.TicketTemplateParagraphPropertiesViewModel != null)
            {
                _ticketTemplateParagraphPropertiesId = model.TicketTemplateParagraphPropertiesId;
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
                model.TicketTemplateParagraphRunPageViewModel.List.Reverse();
                foreach (var run in model.TicketTemplateParagraphRunPageViewModel.List)
                {
                    var control = Container.Resolve<ControlTicketTemplateViewerRun>();
                    control.Dock = DockStyle.Top;
                    control.LoadView(run, flag);
                    panelRuns.Controls.Add(control);
                }
            }
        }

        private void ButtonShowProperties_Click(object sender, EventArgs e)
        {
            if (panelParagraphProperties.Visible)
            {
                panelParagraphProperties.Visible = false;
            }
            else
            {
                panelParagraphProperties.Visible = true;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            ResultService result = _service.UpdateTicketTemplateParagraph(new TicketTemplateParagraphSetBindingModel
            {
                Id = _id,
                Order = Convert.ToInt32(numericUpDownOrder.Value),
                TicketTemplateBodyId = _ticketTemplateBodyId,
                TicketTemplateParagraphPropertiesId = _ticketTemplateParagraphPropertiesId,
                TicketTemplateParagraphPropertiesSetBindingModel = new TicketTemplateParagraphPropertiesSetBindingModel
                {
                    Id = _ticketTemplateParagraphPropertiesId.Value,
                    TicketTemplateParagraphId = _id,
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
            if (result.Succeeded)
            {
                MessageBox.Show("Сохранено", "Портал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result.Result != null && result.Result is Guid)
                {
                    var model = _service.GetTicketTemplateParagraph(new TicketTemplateParagraphGetBindingModel { Id = (Guid)result.Result });
                    if (model.Succeeded)
                    {
                        LoadView(model.Result);
                    }
                }
            }
            else
            {
                ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
            }
        }

        private void ButtonDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            var result = _service.DeleteTicketTemplateParagraph(new TicketTemplateParagraphGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                return;
            }
            Enabled = false;
        }

        private void ButtonAddRun_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<ControlTicketTemplateViewerRun>();
            control.Dock = DockStyle.Top;
            control.LoadView(new TicketTemplateParagraphRunViewModel
            {
                Id = Guid.NewGuid(),
                TicketTemplateParagraphId = _id,
                Order = 0,
                TicketTemplateRunPropertiesId = Guid.NewGuid(),
                TicketTemplateParagraphRunPropertiesViewModel = new TicketTemplateParagraphRunPropertiesViewModel()
            });
            panelRuns.Controls.Add(control);
        }
    }
}