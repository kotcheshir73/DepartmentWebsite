using ControlsAndForms.Messangers;
using ExaminationControlsAndForms.TicketTemplateTableRow;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using ExaminationInterfaces.ViewModels;
using System;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace ExaminationControlsAndForms.TicketTemplateTable
{
    public partial class ControlTicketTemplateViewerTable : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ITicketTemplateTableService _service;

        private Guid? _ticketTemplateBodyId;

        private Guid _id;

        private FormTicketTemplateTableProperties _formTableProperties;

        public ControlTicketTemplateViewerTable(ITicketTemplateTableService service)
        {
            InitializeComponent();
            _service = service;
            _formTableProperties = new FormTicketTemplateTableProperties();
        }

        public TicketTemplateTableSetBindingModel GetModel => new TicketTemplateTableSetBindingModel
        {
            Id = _id,
            TicketTemplateBodyId = _ticketTemplateBodyId,
            Order = Convert.ToInt32(numericUpDownOrder.Value),
            TicketTemplateTablePropertiesId = _formTableProperties.GetTicketTemplateTablePropertiesId,
            TicketTemplateTablePropertiesSetBindingModel = _formTableProperties.GetTicketTemplateTablePropertiesSetBindingModel,
            TicketTemplateTableRowSetBindingModels = panelRows.Controls.Cast<ControlTicketTemplateViewerTableRow>()?.Select(x => x.GetModel)?.ToList(),
            TicketTemplateTableGridColumnSetBindingModels = panelGridColumns.Controls.Cast<ControlTicketTemplateViewerTableGridColumn>()?.Select(x => x.GetModel)?.ToList()
        };

        public void LoadView(TicketTemplateTableViewModel model, bool flag = true)
        {
            contextMenuStrip.Enabled = flag;

            _id = model.Id;
            _ticketTemplateBodyId = model.TicketTemplateBodyId;
            numericUpDownOrder.Value = model.Order;
            _formTableProperties.SetTicketTemplateTablePropertiesViewModel = model.TicketTemplateTablePropertiesViewModel;

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

            if (model.TicketTemplateTableGridColumnPageViewModel != null)
            {
                panelGridColumns.Controls.Clear();
                model.TicketTemplateTableGridColumnPageViewModel.List.Reverse();
                foreach (var grid in model.TicketTemplateTableGridColumnPageViewModel.List)
                {
                    var control = Container.Resolve<ControlTicketTemplateViewerTableGridColumn>();
                    control.Dock = DockStyle.Left;
                    control.SetModel = grid;
                    panelGridColumns.Controls.Add(control);
                }
            }
        }

        private void PropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _formTableProperties.ShowDialog();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResultService result = _service.UpdateTicketTemplateTable(new TicketTemplateTableSetBindingModel
            {
                Id = _id,
                Order = Convert.ToInt32(numericUpDownOrder.Value),
                TicketTemplateBodyId = _ticketTemplateBodyId,
                TicketTemplateTablePropertiesId = _formTableProperties.GetTicketTemplateTablePropertiesId,
                TicketTemplateTablePropertiesSetBindingModel = _formTableProperties.GetTicketTemplateTablePropertiesSetBindingModel
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

        private void DelToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
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