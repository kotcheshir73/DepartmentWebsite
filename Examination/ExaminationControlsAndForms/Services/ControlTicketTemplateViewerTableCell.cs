using ControlsAndForms.Messangers;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using ExaminationInterfaces.ViewModels;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace ExaminationControlsAndForms.Services
{
    public partial class ControlTicketTemplateViewerTableCell : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ITicketTemplateTableCellService _service;

        private Guid? _ticketTemplateTableRowId;

        private Guid _id;

        private Guid? _ticketTemplateTableCellPropertiesId;

        public ControlTicketTemplateViewerTableCell(ITicketTemplateTableCellService service)
        {
            InitializeComponent();
            _service = service;
        }

        public TicketTemplateTableCellSetBindingModel GetModel => new TicketTemplateTableCellSetBindingModel
        {
            Id = _id,
            TicketTemplateTableRowId = _ticketTemplateTableRowId,
            Order = Convert.ToInt32(numericUpDownOrder.Value),
            TicketTemplateTableCellPropertiesId = _ticketTemplateTableCellPropertiesId,
            TicketTemplateTableCellPropertiesSetBindingModel = new TicketTemplateTableCellPropertiesSetBindingModel
            {
                Id = _ticketTemplateTableCellPropertiesId.Value,
                TicketTemplateTableCellId = _id,

                TableCellWidth = textBoxTableCellWidth.Text,
                GridSpan = textBoxGridSpan.Text,
                VerticalMerge = textBoxVerticalMerge.Text,
                ShadingColor = textBoxShadingColor.Text,
                ShadingFill = textBoxShadingFill.Text,
                ShadingValue = textBoxShadingValue.Text
            },
            TicketTemplateParagraphSetBindingModels = panelParagraphs.Controls.Cast<ControlTicketTemplateViewerParagraph>()?.Select(x => x.GetModel)?.ToList()
        };

        public void LoadView(TicketTemplateTableCellViewModel model, bool flag = true)
        {
            panelAction.Enabled = flag;

            _id = model.Id;
            _ticketTemplateTableRowId = model.TicketTemplateTableRowId;
            numericUpDownOrder.Value = model.Order;
            if (model.TicketTemplateTableCellPropertiesViewModel != null)
            {
                _ticketTemplateTableCellPropertiesId = model.TicketTemplateTableCellPropertiesId;
                textBoxTableCellWidth.Text = model.TicketTemplateTableCellPropertiesViewModel.TableCellWidth;
                textBoxGridSpan.Text = model.TicketTemplateTableCellPropertiesViewModel.GridSpan;
                textBoxVerticalMerge.Text = model.TicketTemplateTableCellPropertiesViewModel.VerticalMerge;
                textBoxShadingColor.Text = model.TicketTemplateTableCellPropertiesViewModel.ShadingColor;
                textBoxShadingFill.Text = model.TicketTemplateTableCellPropertiesViewModel.ShadingFill;
                textBoxShadingValue.Text = model.TicketTemplateTableCellPropertiesViewModel.ShadingValue;
            }

            if (model.TicketTemplateParagraphPageViewModel != null)
            {
                panelParagraphs.Controls.Clear();
                model.TicketTemplateParagraphPageViewModel.List.Reverse();
                foreach (var paragraph in model.TicketTemplateParagraphPageViewModel.List)
                {
                    var control = Container.Resolve<ControlTicketTemplateViewerParagraph>();
                    control.Dock = DockStyle.Top;
                    control.LoadView(paragraph, flag);
                    panelParagraphs.Controls.Add(control);
                }
            }
        }

        private void ButtonShowProperties_Click(object sender, EventArgs e)
        {
            if (panelTableCellProperties.Visible)
            {
                panelTableCellProperties.Visible = false;
            }
            else
            {
                panelTableCellProperties.Visible = true;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            ResultService result = _service.UpdateTicketTemplateTableCell(new TicketTemplateTableCellSetBindingModel
            {
                Id = _id,
                Order = Convert.ToInt32(numericUpDownOrder.Value),
                TicketTemplateTableRowId = _ticketTemplateTableRowId,
                TicketTemplateTableCellPropertiesId = _ticketTemplateTableCellPropertiesId,
                TicketTemplateTableCellPropertiesSetBindingModel = new TicketTemplateTableCellPropertiesSetBindingModel
                {
                    Id = _ticketTemplateTableCellPropertiesId.Value,
                    TicketTemplateTableCellId = _id,

                    TableCellWidth = textBoxTableCellWidth.Text,
                    GridSpan = textBoxGridSpan.Text,
                    VerticalMerge = textBoxVerticalMerge.Text,
                    ShadingColor = textBoxShadingColor.Text,
                    ShadingFill = textBoxShadingFill.Text,
                    ShadingValue = textBoxShadingValue.Text
                }
            });
            if (result.Succeeded)
            {
                MessageBox.Show("Сохранено", "Портал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result.Result != null && result.Result is Guid)
                {
                    var model = _service.GetTicketTemplateTableCell(new TicketTemplateTableCellGetBindingModel { Id = (Guid)result.Result });
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
            var result = _service.DeleteTicketTemplateTableCell(new TicketTemplateTableCellGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                return;
            }
            Enabled = false;
        }

        private void ButtonAddParagraph_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<ControlTicketTemplateViewerParagraph>();
            control.Dock = DockStyle.Top;
            control.LoadView(new TicketTemplateParagraphViewModel
            {
                Id = Guid.NewGuid(),
                TicketTemplateTableCellId = _id,
                Order = 0,
                TicketTemplateParagraphPropertiesId = Guid.NewGuid(),
                TicketTemplateParagraphPropertiesViewModel = new TicketTemplateParagraphPropertiesViewModel()
            });
            panelParagraphs.Controls.Add(control);
        }
    }
}