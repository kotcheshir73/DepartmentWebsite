using ControlsAndForms.Messangers;
using ExaminationControlsAndForms.TicketTemplateParagraphRun;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using ExaminationInterfaces.ViewModels;
using System;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace ExaminationControlsAndForms.TicketTemplateParagraph
{
    public partial class ControlTicketTemplateViewerParagraph : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ITicketTemplateParagraphService _service;

        private Guid? _ticketTemplateBodyId;

        private Guid _id;

        private Guid? _ticketTemplateTableCellId;

        private FormTicketTemplateParagraphProperties _formParagraphProperties;

        public ControlTicketTemplateViewerParagraph(ITicketTemplateParagraphService service)
        {
            InitializeComponent();
            _service = service;
            _formParagraphProperties = new FormTicketTemplateParagraphProperties();
        }

        public TicketTemplateParagraphSetBindingModel GetModel => new TicketTemplateParagraphSetBindingModel
        {
            Id = _id,
            TicketTemplateBodyId = _ticketTemplateBodyId,
            Order = Convert.ToInt32(numericUpDownOrder.Value),
            TicketTemplateParagraphPropertiesId = _formParagraphProperties.GetTicketTemplateParagraphPropertiesId,
            TicketTemplateTableCellId = _ticketTemplateTableCellId,
            TicketTemplateParagraphPropertiesSetBindingModel  = _formParagraphProperties.GetTicketTemplateParagraphPropertiesSetBindingModel,
            TicketTemplateParagraphRunSetBindingModels = panelRuns.Controls.Cast<ControlTicketTemplateViewerParagraphRun>()?.Select(x => x.GetModel)?.ToList()
        };

        public void LoadView(TicketTemplateParagraphViewModel model, bool flag = true)
        {
            contextMenuStrip.Enabled = flag;

            _id = model.Id;
            _ticketTemplateBodyId = model.TicketTemplateBodyId;
            _ticketTemplateTableCellId = model.TicketTemplateTableCellId;
            numericUpDownOrder.Value = model.Order;
            _formParagraphProperties.SetTicketTemplateParagraphPropertiesViewModel = model.TicketTemplateParagraphPropertiesViewModel;

            if (model.TicketTemplateParagraphRunPageViewModel != null)
            {
                panelRuns.Controls.Clear();
                model.TicketTemplateParagraphRunPageViewModel.List.Reverse();
                foreach (var run in model.TicketTemplateParagraphRunPageViewModel.List)
                {
                    var control = Container.Resolve<ControlTicketTemplateViewerParagraphRun>();
                    control.Dock = DockStyle.Top;
                    control.LoadView(run, flag);
                    panelRuns.Controls.Add(control);
                }
            }
        }

        private void PropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _formParagraphProperties.ShowDialog();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResultService result = _service.UpdateTicketTemplateParagraph(new TicketTemplateParagraphSetBindingModel
            {
                Id = _id,
                Order = Convert.ToInt32(numericUpDownOrder.Value),
                TicketTemplateBodyId = _ticketTemplateBodyId,
                TicketTemplateParagraphPropertiesId = _formParagraphProperties.GetTicketTemplateParagraphPropertiesId,
                TicketTemplateParagraphPropertiesSetBindingModel = _formParagraphProperties.GetTicketTemplateParagraphPropertiesSetBindingModel
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

        private void DelToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<ControlTicketTemplateViewerParagraphRun>();
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