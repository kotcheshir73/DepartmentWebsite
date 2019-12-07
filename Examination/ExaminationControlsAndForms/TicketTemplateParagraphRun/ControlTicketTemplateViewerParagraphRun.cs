using ControlsAndForms.Messangers;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using ExaminationInterfaces.ViewModels;
using System;
using System.Windows.Forms;
using Tools;
using Unity;

namespace ExaminationControlsAndForms.TicketTemplateParagraphRun
{
    public partial class ControlTicketTemplateViewerParagraphRun : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ITicketTemplateParagraphRunService _service;

        private Guid _ticketTemplateParagraphId;

        private Guid _id;

        private FormTicketTemplateParagraphRunProperties _formParagraphRunProperties;

        private string _breakType;

        public ControlTicketTemplateViewerParagraphRun(ITicketTemplateParagraphRunService service)
        {
            InitializeComponent();
            _service = service;
            _formParagraphRunProperties = new FormTicketTemplateParagraphRunProperties();
        }

        public TicketTemplateParagraphRunSetBindingModel GetModel => new TicketTemplateParagraphRunSetBindingModel
        {
            Id = _id,
            TicketTemplateParagraphId = _ticketTemplateParagraphId,
            Order = Convert.ToInt32(numericUpDownOrder.Value),
            Text = textBox.Text,
            TabChar = checkBoxTab.Checked,
            Break = checkBoxBreak.Checked,
            BreakType = _breakType,
            TicketTemplateRunPropertiesId = _formParagraphRunProperties.GetTicketTemplateParagraphRunPropertiesId,
            TicketTemplateParagraphRunPropertiesSetBindingModel = _formParagraphRunProperties.GetTicketTemplateParagraphRunPropertiesSetBindingModel
        };

        public void LoadView(TicketTemplateParagraphRunViewModel model, bool flag = true)
        {
            contextMenuStrip.Enabled = flag;

            _id = model.Id;
            _ticketTemplateParagraphId = model.TicketTemplateParagraphId;
            numericUpDownOrder.Value = model.Order;
            checkBoxTab.Checked = model.TabChar;
            checkBoxBreak.Checked = model.Break;
            _breakType = model.BreakType;
            textBox.Text = model.Text;
            _formParagraphRunProperties.SetTicketTemplateParagraphRunPropertiesViewModel = model.TicketTemplateParagraphRunPropertiesViewModel;
        }

        private void CheckBoxTab_CheckedChanged(object sender, EventArgs e)
        {
            SetTabOrBreak();
        }

        private void CheckBoxBreak_CheckedChanged(object sender, EventArgs e)
        {
            SetTabOrBreak();
        }

        private void SetTabOrBreak()
        {
            textBox.Visible = !(checkBoxBreak.Checked || checkBoxTab.Checked);
            if (checkBoxBreak.Checked)
            {
                checkBoxTab.Checked = false;
            }
            if (checkBoxTab.Checked)
            {
                checkBoxBreak.Checked = false;
            }
        }

        private void PropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _formParagraphRunProperties.ShowDialog();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResultService result = _service.UpdateTicketTemplateParagraphRun(new TicketTemplateParagraphRunSetBindingModel
            {
                Id = _id,
                Order = Convert.ToInt32(numericUpDownOrder.Value),
                Text = textBox.Text,
                TabChar = checkBoxTab.Checked,
                TicketTemplateParagraphId = _ticketTemplateParagraphId,
                TicketTemplateRunPropertiesId = _formParagraphRunProperties.GetTicketTemplateParagraphRunPropertiesId,
                TicketTemplateParagraphRunPropertiesSetBindingModel = _formParagraphRunProperties.GetTicketTemplateParagraphRunPropertiesSetBindingModel
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

        private void DelToolStripMenuItem_Click(object sender, EventArgs e)
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
    }
}