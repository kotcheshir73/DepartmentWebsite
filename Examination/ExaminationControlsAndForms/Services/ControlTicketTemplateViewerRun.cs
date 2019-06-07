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
    public partial class ControlTicketTemplateViewerRun : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ITicketTemplateParagraphRunService _service;

        private Guid _ticketTemplateParagraphId;

        private Guid _id;

        private Guid? _ticketTemplateRunPropertiesId;

        public ControlTicketTemplateViewerRun(ITicketTemplateParagraphRunService service)
        {
            InitializeComponent();
            _service = service;
        }

        public TicketTemplateParagraphRunSetBindingModel GetModel => new TicketTemplateParagraphRunSetBindingModel
        {
            Id = _id,
            TicketTemplateParagraphId = _ticketTemplateParagraphId,
            Order = Convert.ToInt32(numericUpDownOrder.Value),
            Text = textBox.Text,
            TabChar = checkBoxTab.Checked,
            TicketTemplateRunPropertiesId = _ticketTemplateRunPropertiesId,
            TicketTemplateParagraphRunPropertiesSetBindingModel = new TicketTemplateParagraphRunPropertiesSetBindingModel
            {
                Id = _ticketTemplateRunPropertiesId.Value,
                TicketTemplateParagraphRunId = _id,
                RunBold = checkBoxBold.Checked,
                RunItalic = checkBoxItalic.Checked,
                RunUnderline = checkBoxUnderline.Checked,
                RunSize = textBoxSize.Text
            }
        };

        public void LoadView(TicketTemplateParagraphRunViewModel model, bool flag = true)
        {
            panelAction.Enabled = flag;

            _id = model.Id;
            _ticketTemplateParagraphId = model.TicketTemplateParagraphId;
            numericUpDownOrder.Value = model.Order;
            checkBoxTab.Checked = model.TabChar;
            textBox.Text = model.Text;
            if (model.TicketTemplateParagraphRunPropertiesViewModel != null)
            {
                _ticketTemplateRunPropertiesId = model.TicketTemplateRunPropertiesId;
                checkBoxBold.Checked = model.TicketTemplateParagraphRunPropertiesViewModel.RunBold;
                checkBoxItalic.Checked = model.TicketTemplateParagraphRunPropertiesViewModel.RunItalic;
                checkBoxUnderline.Checked = model.TicketTemplateParagraphRunPropertiesViewModel.RunUnderline;
                textBoxSize.Text = model.TicketTemplateParagraphRunPropertiesViewModel.RunSize;
            }
        }

        private void ButtonShowProperties_Click(object sender, EventArgs e)
        {
            if (panelProperties.Visible)
            {
                panelProperties.Visible = false;
                buttonShowProperties.BackgroundImage = Properties.Resources.Right;
            }
            else
            {
                panelProperties.Visible = true;
                buttonShowProperties.BackgroundImage = Properties.Resources.Left;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            ResultService result = _service.UpdateTicketTemplateParagraphRun(new TicketTemplateParagraphRunSetBindingModel
            {
                Id = _id,
                Order = Convert.ToInt32(numericUpDownOrder.Value),
                Text = textBox.Text,
                TabChar = checkBoxTab.Checked,
                TicketTemplateParagraphId = _ticketTemplateParagraphId,
                TicketTemplateRunPropertiesId = _ticketTemplateRunPropertiesId,
                TicketTemplateParagraphRunPropertiesSetBindingModel = new TicketTemplateParagraphRunPropertiesSetBindingModel
                {
                    Id = _ticketTemplateRunPropertiesId.Value,
                    TicketTemplateParagraphRunId = _id,
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
                    var model = _service.GetTicketTemplateParagraphRun(new TicketTemplateParagraphRunGetBindingModel { Id = (Guid)result.Result });
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
            var result = _service.DeleteTicketTemplateParagraphRun(new TicketTemplateParagraphRunGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                return;
            }
            Enabled = false;
        }

        private void CheckBoxTab_CheckedChanged(object sender, EventArgs e)
        {
            textBox.Visible = !checkBoxTab.Checked;
        }
    }
}