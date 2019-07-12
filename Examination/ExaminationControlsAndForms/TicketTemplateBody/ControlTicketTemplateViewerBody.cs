using ControlsAndForms.Messangers;
using ExaminationControlsAndForms.TicketTemplateParagraph;
using ExaminationControlsAndForms.TicketTemplateTable;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using ExaminationInterfaces.ViewModels;
using System;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace ExaminationControlsAndForms.TicketTemplateBody
{
    public partial class ControlTicketTemplateViewerBody : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ITicketTemplateBodyService _service;

        private Guid _ticketTemplateId;

        private Guid _id;

        private Guid? _ticketTemplateBodyPropertiesId;

        public ControlTicketTemplateViewerBody(ITicketTemplateBodyService service)
        {
            InitializeComponent();
            _service = service;
        }

        public TicketTemplateBodySetBindingModel GetModel => new TicketTemplateBodySetBindingModel
        {
            Id = _id,
            TicketTemplateId = _ticketTemplateId,
            TicketTemplateBodyPropertiesId = _ticketTemplateBodyPropertiesId,
            TicketTemplateBodyPropertiesSetBindingModel = new TicketTemplateBodyPropertiesSetBindingModel
            {
                Id = _ticketTemplateBodyPropertiesId.Value,
                TicketTemplateBodyId = _id,

                PageMarginBottom = textBoxBottom.Text,
                PageMarginFooter = textBoxFooter.Text,
                PageMarginGutter = textBoxGutter.Text,
                PageMarginLeft = textBoxLeft.Text,
                PageMarginRight = textBoxRight.Text,
                PageMarginTop = textBoxTop.Text,

                PageSizeHeight = textBoxHeight.Text,
                PageSizeOrient = textBoxOrient.Text,
                PageSizeWidth = textBoxOrient.Text
            },
            TicketTemplateParagraphSetBindingModels = panelParagraphs.Controls.Cast<ControlTicketTemplateViewerParagraph>()?.Select(x => x.GetModel)?.ToList(),
            TicketTemplateTableSetBindingModels = panelTables.Controls.Cast<ControlTicketTemplateViewerTable>()?.Select(x => x.GetModel)?.ToList()
        };

        public void LoadView(TicketTemplateBodyViewModel model, bool flag = true)
        {
            panelAction.Enabled = flag;

            _id = model.Id;
            _ticketTemplateId = model.TicketTemplateId;
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
                model.TicketTemplateParagraphPageViewModel.List.Reverse();
                foreach (var run in model.TicketTemplateParagraphPageViewModel.List)
                {
                    var control = Container.Resolve<ControlTicketTemplateViewerParagraph>();
                    control.LoadView(run, flag);
                    control.Dock = DockStyle.Top;
                    panelParagraphs.Controls.Add(control);
                }
            }
            if (model.TicketTemplateTablePageViewModel != null)
            {
                panelTables.Controls.Clear();
                model.TicketTemplateTablePageViewModel.List.Reverse();
                foreach (var table in model.TicketTemplateTablePageViewModel.List)
                {
                    var control = Container.Resolve<ControlTicketTemplateViewerTable>();
                    control.LoadView(table, flag);
                    control.Dock = DockStyle.Top;
                    panelTables.Controls.Add(control);
                }
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            ResultService result = _service.UpdateTicketTemplateBody(new TicketTemplateBodySetBindingModel
            {
                Id = _id,
                TicketTemplateId = _ticketTemplateId,
                TicketTemplateBodyPropertiesId = _ticketTemplateBodyPropertiesId,
                TicketTemplateBodyPropertiesSetBindingModel =
                new TicketTemplateBodyPropertiesSetBindingModel
                {
                    Id = _ticketTemplateBodyPropertiesId.Value,
                    TicketTemplateBodyId = _id,
                    PageMarginBottom = textBoxBottom.Text,
                    PageMarginFooter = textBoxFooter.Text,
                    PageMarginGutter = textBoxGutter.Text,
                    PageMarginLeft = textBoxLeft.Text,
                    PageMarginRight = textBoxRight.Text,
                    PageMarginTop = textBoxTop.Text,
                    PageSizeHeight = textBoxHeight.Text,
                    PageSizeOrient = textBoxOrient.Text,
                    PageSizeWidth = textBoxWidth.Text
                }
            });
            if (result.Succeeded)
            {
                MessageBox.Show("Сохранено", "Портал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result.Result != null && result.Result is Guid)
                {
                    var model = _service.GetTicketTemplateBody(new TicketTemplateBodyGetBindingModel { Id = (Guid)result.Result });
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

        private void ButtonAddParagraph_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<ControlTicketTemplateViewerParagraph>();
            control.Dock = DockStyle.Top;
            control.LoadView(new TicketTemplateParagraphViewModel
            {
                Id = Guid.NewGuid(),
                TicketTemplateBodyId = _id,
                TicketTemplateParagraphPropertiesId = Guid.NewGuid(),
                TicketTemplateParagraphPropertiesViewModel = new TicketTemplateParagraphPropertiesViewModel()
            });
            panelParagraphs.Controls.Add(control);
        }

        private void ButtonAddTable_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<ControlTicketTemplateViewerTable>();
            control.Dock = DockStyle.Top;
            control.LoadView(new TicketTemplateTableViewModel
            {
                Id = Guid.NewGuid(),
                TicketTemplateBodyId = _id,
                TicketTemplateTablePropertiesId = Guid.NewGuid(),
                TicketTemplateTablePropertiesViewModel = new TicketTemplateTablePropertiesViewModel()
            });
            panelTables.Controls.Add(control);
        }
    }
}