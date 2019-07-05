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
    public partial class ControlTicketTemplateViewerTableRow : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ITicketTemplateTableRowService _service;

        private Guid? _ticketTemplateTableId;

        private Guid _id;

        private Guid? _ticketTemplateTableRowPropertiesId;

        public ControlTicketTemplateViewerTableRow(ITicketTemplateTableRowService service)
        {
            InitializeComponent();
            _service = service;
        }

        public TicketTemplateTableRowSetBindingModel GetModel => new TicketTemplateTableRowSetBindingModel
        {
            Id = _id,
            TicketTemplateTableId = _ticketTemplateTableId,
            Order = Convert.ToInt32(numericUpDownOrder.Value),
            TicketTemplateTableRowPropertiesId = _ticketTemplateTableRowPropertiesId,
            TicketTemplateTableRowPropertiesSetBindingModel = new TicketTemplateTableRowPropertiesSetBindingModel
            {
                Id = _ticketTemplateTableRowPropertiesId.Value,
                TicketTemplateTableRowId = _id,

                TableRowHeight = textBoxTableRowHeight.Text,
                CantSplit = textBoxCantSplit.Text
            },
            TicketTemplateTableCellSetBindingModels = panelCells.Controls.Cast<ControlTicketTemplateViewerTableCell>()?.Select(x => x.GetModel)?.ToList()
        };

        public void LoadView(TicketTemplateTableRowViewModel model, bool flag = true)
        {
            panelAction.Enabled = flag;

            _id = model.Id;
            _ticketTemplateTableId = model.TicketTemplateTableId;
            numericUpDownOrder.Value = model.Order;
            if (model.TicketTemplateTableRowPropertiesViewModel != null)
            {
                _ticketTemplateTableRowPropertiesId = model.TicketTemplateTableRowPropertiesId;
                textBoxTableRowHeight.Text = model.TicketTemplateTableRowPropertiesViewModel.TableRowHeight;
                textBoxCantSplit.Text = model.TicketTemplateTableRowPropertiesViewModel.CantSplit;
            }

            if (model.TicketTemplateTableCellPageViewModel != null)
            {
                panelCells.Controls.Clear();
                model.TicketTemplateTableCellPageViewModel.List.Reverse();
                foreach (var cell in model.TicketTemplateTableCellPageViewModel.List)
                {
                    var control = Container.Resolve<ControlTicketTemplateViewerTableCell>();
                    control.Dock = DockStyle.Top;
                    control.LoadView(cell, flag);
                    panelCells.Controls.Add(control);
                }
            }
        }

        private void ButtonShowProperties_Click(object sender, EventArgs e)
        {
            if (panelTableRowProperties.Visible)
            {
                panelTableRowProperties.Visible = false;
            }
            else
            {
                panelTableRowProperties.Visible = true;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            ResultService result = _service.UpdateTicketTemplateTableRow(new TicketTemplateTableRowSetBindingModel
            {
                Id = _id,
                Order = Convert.ToInt32(numericUpDownOrder.Value),
                TicketTemplateTableId = _ticketTemplateTableId,
                TicketTemplateTableRowPropertiesId = _ticketTemplateTableRowPropertiesId,
                TicketTemplateTableRowPropertiesSetBindingModel = new TicketTemplateTableRowPropertiesSetBindingModel
                {
                    Id = _ticketTemplateTableRowPropertiesId.Value,
                    TicketTemplateTableRowId = _id,

                    TableRowHeight = textBoxTableRowHeight.Text,
                    CantSplit = textBoxCantSplit.Text
                }
            });
            if (result.Succeeded)
            {
                MessageBox.Show("Сохранено", "Портал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result.Result != null && result.Result is Guid)
                {
                    var model = _service.GetTicketTemplateTableRow(new TicketTemplateTableRowGetBindingModel { Id = (Guid)result.Result });
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
            var result = _service.DeleteTicketTemplateTableRow(new TicketTemplateTableRowGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                return;
            }
            Enabled = false;
        }

        private void ButtonAddCell_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<ControlTicketTemplateViewerTableCell>();
            control.Dock = DockStyle.Top;
            control.LoadView(new TicketTemplateTableCellViewModel
            {
                Id = Guid.NewGuid(),
                TicketTemplateTableRowId = _id,
                Order = 0,
                TicketTemplateTableCellPropertiesId = Guid.NewGuid(),
                TicketTemplateTableCellPropertiesViewModel = new TicketTemplateTableCellPropertiesViewModel()
            });
            panelCells.Controls.Add(control);
        }
    }
}