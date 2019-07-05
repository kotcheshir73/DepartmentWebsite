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
    public partial class ControlTicketTemplateViewerTable : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ITicketTemplateTableService _service;

        private Guid? _ticketTemplateBodyId;

        private Guid _id;

        private Guid? _ticketTemplateTablePropertiesId;

        public ControlTicketTemplateViewerTable(ITicketTemplateTableService service)
        {
            InitializeComponent();
            _service = service;
        }

        public TicketTemplateTableSetBindingModel GetModel => new TicketTemplateTableSetBindingModel
        {
            Id = _id,
            TicketTemplateBodyId = _ticketTemplateBodyId,
            Order = Convert.ToInt32(numericUpDownOrder.Value),
            TicketTemplateTablePropertiesId = _ticketTemplateTablePropertiesId,
            TicketTemplateTablePropertiesSetBindingModel = new TicketTemplateTablePropertiesSetBindingModel
            {
                Id = _ticketTemplateTablePropertiesId.Value,
                TicketTemplateTableId = _id,

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
            },
            TicketTemplateTableRowSetBindingModels = panelRows.Controls.Cast<ControlTicketTemplateViewerTableRow>()?.Select(x => x.GetModel)?.ToList()
        };

        public void LoadView(TicketTemplateTableViewModel model, bool flag = true)
        {
            panelAction.Enabled = flag;

            _id = model.Id;
            _ticketTemplateBodyId = model.TicketTemplateBodyId;
            numericUpDownOrder.Value = model.Order;
            if (model.TicketTemplateTablePropertiesViewModel != null)
            {
                _ticketTemplateTablePropertiesId = model.TicketTemplateTablePropertiesId;
                textBoxLayoutType.Text = model.TicketTemplateTablePropertiesViewModel.LayoutType;
                textBoxLookNoHorizontalBand.Text = model.TicketTemplateTablePropertiesViewModel.LookNoHorizontalBand;
                textBoxLookNoVerticalBand.Text = model.TicketTemplateTablePropertiesViewModel.LookNoVerticalBand;
                textBoxLookValue.Text = model.TicketTemplateTablePropertiesViewModel.LookValue;
                textBoxWidth.Text = model.TicketTemplateTablePropertiesViewModel.Width;

                textBoxLookFirstColumn.Text = model.TicketTemplateTablePropertiesViewModel.LookFirstColumn;
                textBoxLookFirstRow.Text = model.TicketTemplateTablePropertiesViewModel.LookFirstRow;
                textBoxLookLastColumn.Text = model.TicketTemplateTablePropertiesViewModel.LookLastColumn;
                textBoxLookLastRow.Text = model.TicketTemplateTablePropertiesViewModel.LookLastRow;

                textBoxTopColor.Text = model.TicketTemplateTablePropertiesViewModel.BorderTopColor;
                textBoxTopSize.Text = model.TicketTemplateTablePropertiesViewModel.BorderTopSize;
                textBoxTopSpace.Text = model.TicketTemplateTablePropertiesViewModel.BorderTopSpace;
                textBoxTopValue.Text = model.TicketTemplateTablePropertiesViewModel.BorderTopValue;

                textBoxBottomColor.Text = model.TicketTemplateTablePropertiesViewModel.BorderBottomColor;
                textBoxBottomSize.Text = model.TicketTemplateTablePropertiesViewModel.BorderBottomSize;
                textBoxBottomSpace.Text = model.TicketTemplateTablePropertiesViewModel.BorderBottomSpace;
                textBoxBottomValue.Text = model.TicketTemplateTablePropertiesViewModel.BorderBottomValue;

                textBoxLeftColor.Text = model.TicketTemplateTablePropertiesViewModel.BorderLeftColor;
                textBoxLeftSize.Text = model.TicketTemplateTablePropertiesViewModel.BorderLeftSize;
                textBoxLeftSpace.Text = model.TicketTemplateTablePropertiesViewModel.BorderLeftSpace;
                textBoxLeftValue.Text = model.TicketTemplateTablePropertiesViewModel.BorderLeftValue;

                textBoxRightColor.Text = model.TicketTemplateTablePropertiesViewModel.BorderRightColor;
                textBoxRightSize.Text = model.TicketTemplateTablePropertiesViewModel.BorderRightSize;
                textBoxRightSpace.Text = model.TicketTemplateTablePropertiesViewModel.BorderRightSpace;
                textBoxRightValue.Text = model.TicketTemplateTablePropertiesViewModel.BorderRightValue;
            }

            if (model.TicketTemplateTableRowPageViewModel != null)
            {
                panelRows.Controls.Clear();
                model.TicketTemplateTableRowPageViewModel.List.Reverse();
                foreach (var row in model.TicketTemplateTableRowPageViewModel.List)
                {
                    var control = Container.Resolve<ControlTicketTemplateViewerTableRow>();
                    control.Dock = DockStyle.Top;
                    control.LoadView(row, flag);
                    panelRows.Controls.Add(control);
                }
            }
        }

        private void ButtonShowProperties_Click(object sender, EventArgs e)
        {
            if (panelTableProperties.Visible)
            {
                panelTableProperties.Visible = false;
            }
            else
            {
                panelTableProperties.Visible = true;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            ResultService result = _service.UpdateTicketTemplateTable(new TicketTemplateTableSetBindingModel
            {
                Id = _id,
                Order = Convert.ToInt32(numericUpDownOrder.Value),
                TicketTemplateBodyId = _ticketTemplateBodyId,
                TicketTemplateTablePropertiesId = _ticketTemplateTablePropertiesId,
                TicketTemplateTablePropertiesSetBindingModel = new TicketTemplateTablePropertiesSetBindingModel
                {
                    Id = _ticketTemplateTablePropertiesId.Value,
                    TicketTemplateTableId = _id,

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
                }
            });
            if (result.Succeeded)
            {
                MessageBox.Show("Сохранено", "Портал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result.Result != null && result.Result is Guid)
                {
                    var model = _service.GetTicketTemplateTable(new TicketTemplateTableGetBindingModel { Id = (Guid)result.Result });
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
            var result = _service.DeleteTicketTemplateTable(new TicketTemplateTableGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                return;
            }
            Enabled = false;
        }

        private void ButtonAddRow_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<ControlTicketTemplateViewerTableRow>();
            control.Dock = DockStyle.Top;
            control.LoadView(new TicketTemplateTableRowViewModel
            {
                Id = Guid.NewGuid(),
                TicketTemplateTableId = _id,
                Order = 0,
                TicketTemplateTableRowPropertiesId = Guid.NewGuid(),
                TicketTemplateTableRowPropertiesViewModel = new TicketTemplateTableRowPropertiesViewModel()
            });
            panelRows.Controls.Add(control);
        }
    }
}