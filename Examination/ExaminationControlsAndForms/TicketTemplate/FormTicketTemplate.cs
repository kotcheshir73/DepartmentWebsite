using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using ExaminationControlsAndForms.Services;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;
using Tools;
using Unity;

namespace ExaminationControlsAndForms.TicketTemplate
{
    public partial class FormTicketTemplate : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ITicketTemplateService _service;

        private readonly ITicketProcess _process;

        public FormTicketTemplate(ITicketTemplateService service, IExaminationTemplateService _serviceET, ITicketProcess process, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _process = process;
        }

        protected override bool LoadComponents()
        {
            var controlView = new ControlTicketTemplateHtmlView
            {
                Dock = DockStyle.Fill
            };
            tabPageHtmlView.Controls.Add(controlView);

            var control = Container.Resolve<ControlTicketTemplateViewerBody>();
            control.Dock = DockStyle.Fill;
            tabPageBody.Controls.Add(control);

            return base.LoadComponents();
        }

        protected override void LoadData()
        {
            var result = _service.GetTicketTemplate(new TicketTemplateGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            buttonLoadTemplate.Visible = labelLinkToFile.Visible = textBoxLinkToFile.Visible = false;
            var entity = result.Result;

            textBoxTemplateName.Text = entity.TemplateName;

            if (entity.Body != null)
            {
                (tabPageHtmlView.Controls[0] as ControlTicketTemplateHtmlView).LoadView(entity);
                (tabPageBody.Controls[0] as ControlTicketTemplateViewerBody).LoadView(entity.Body);
            }
        }

        protected override bool CheckFill()
        {
            labelLinkToFile.ForeColor =
            labelTemplateName.ForeColor =
                SystemColors.ControlText;
            if (textBoxLinkToFile.Visible)
            {
                if (string.IsNullOrEmpty(textBoxLinkToFile.Text))
                {
                    labelLinkToFile.ForeColor = Color.Red;
                    return false;
                }
            }
            if (string.IsNullOrEmpty(textBoxTemplateName.Text))
            {
                labelTemplateName.ForeColor = Color.Red;
                return false;
            }
            return true;
        }

        protected override bool Save()
        {
            ResultService result;
            if (!_id.HasValue)
            {
                result = _service.CreateTicketTemplate(new TicketTemplateSetBindingModel
                {
                    TemplateName = textBoxTemplateName.Text,
                    TicketTemplateBodyId = (tabPageBody.Controls[0] as ControlTicketTemplateViewerBody).GetModel.Id,
                    TicketTemplateBodySetBindingModel = (tabPageBody.Controls[0] as ControlTicketTemplateViewerBody).GetModel
                });
            }
            else
            {
                result = _service.UpdateTicketTemplate(new TicketTemplateSetBindingModel
                {
                    Id = _id.Value,
                    TemplateName = textBoxTemplateName.Text,
                    TicketTemplateBodyId = (tabPageBody.Controls[0] as ControlTicketTemplateViewerBody).GetModel.Id
                });
            }
            if (result.Succeeded)
            {
                if (result.Result != null)
                {
                    if (result.Result is Guid)
                    {
                        _id = (Guid)result.Result;
                    }
                }
                return true;
            }
            else
            {
                ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                return false;
            }
        }

        private void ButtonLoadTemplate_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "docx|*.docx"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxLinkToFile.Text = ofd.FileName;
                var template = _process.LoadTemplate(new TicketProcessLoadTemplateBindingModel
                {
                    FileName = ofd.FileName,
                    TemplateName = textBoxTemplateName.Text
                });
                if (template.Succeeded)
                {
                    (tabPageHtmlView.Controls[0] as ControlTicketTemplateHtmlView).LoadView(template.Result);
                    (tabPageBody.Controls[0] as ControlTicketTemplateViewerBody).LoadView(template.Result.Body, false);
                }
                else
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", template.Errors);
                }
            }
        }
    }
}