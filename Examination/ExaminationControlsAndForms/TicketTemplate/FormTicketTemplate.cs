using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using ExaminationControlsAndForms.Services;
using ExaminationControlsAndForms.TicketTemplateBody;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using ExaminationInterfaces.ViewModels;
using System;
using System.Drawing;
using System.Linq;
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

        private TicketTemplateDocumentSettingPageViewModel _ticketTemplateDocumentSettingPageViewModel;

        private TicketTemplateFontTablePageViewModel _ticketTemplateFontTablePageViewModel;

        private TicketTemplateNumberingPageViewModel _ticketTemplateNumberingPageViewModel;

        private TicketTemplateStyleDefinitionPageViewModel _ticketTemplateStyleDefinitionPageViewModel;

        private TicketTemplateThemePartPageViewModel _ticketTemplateThemePartPageViewModel;

        private TicketTemplateWebSettingPageViewModel _ticketTemplateWebSettingPageViewModel;

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
            _ticketTemplateDocumentSettingPageViewModel = entity.TicketTemplateDocumentSettingPageViewModel;
            _ticketTemplateFontTablePageViewModel = entity.TicketTemplateFontTablePageViewModel;
            _ticketTemplateNumberingPageViewModel = entity.TicketTemplateNumberingPageViewModel;
            _ticketTemplateStyleDefinitionPageViewModel = entity.TicketTemplateStyleDefinitionPageViewModel;
            _ticketTemplateThemePartPageViewModel = entity.TicketTemplateThemePartPageViewModel;
            _ticketTemplateWebSettingPageViewModel = entity.TicketTemplateWebSettingPageViewModel;

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
                    TicketTemplateBodySetBindingModel = (tabPageBody.Controls[0] as ControlTicketTemplateViewerBody).GetModel,
                    TicketTemplateDocumentSettingSetBindingModels = _ticketTemplateDocumentSettingPageViewModel?.List.Select(x => new TicketTemplateDocumentSettingSetBindingModel
                        {
                            Id = x.Id,
                            InnerXml = x.InnerXml,
                            Order = x.Order,
                            TicketTemplateId = new Guid()
                        })?.ToList(),
                    TicketTemplateFontTableSetBindingModels = _ticketTemplateFontTablePageViewModel?.List.Select(x => new TicketTemplateFontTableSetBindingModel
                        {
                            Id = x.Id,
                            InnerXml = x.InnerXml,
                            Order = x.Order,
                            TicketTemplateId = new Guid()
                        })?.ToList(),
                    TicketTemplateNumberingSetBindingModels = _ticketTemplateNumberingPageViewModel?.List.Select(x => new TicketTemplateNumberingSetBindingModel
                        {
                            Id = x.Id,
                            InnerXml = x.InnerXml,
                            Order = x.Order,
                            TicketTemplateId = new Guid()
                        })?.ToList(),
                    TicketTemplateStyleDefinitionSetBindingModels = _ticketTemplateStyleDefinitionPageViewModel?.List.Select(x => new TicketTemplateStyleDefinitionSetBindingModel
                        {
                            Id = x.Id,
                            InnerXml = x.InnerXml,
                            Order = x.Order,
                            TicketTemplateId = new Guid()
                        })?.ToList(),
                    TicketTemplateThemePartSetBindingModels = _ticketTemplateThemePartPageViewModel?.List.Select(x => new TicketTemplateThemePartSetBindingModel
                        {
                            Id = x.Id,
                            InnerXml = x.InnerXml,
                            Order = x.Order,
                            TicketTemplateId = new Guid()
                        })?.ToList(),
                    TicketTemplateWebSettingSetBindingModels = _ticketTemplateWebSettingPageViewModel?.List.Select(x => new TicketTemplateWebSettingSetBindingModel
                        {
                            Id = x.Id,
                            InnerXml = x.InnerXml,
                            Order = x.Order,
                            TicketTemplateId = new Guid()
                        })?.ToList()
                });
            }
            else
            {
                result = _service.UpdateTicketTemplate(new TicketTemplateSetBindingModel
                {
                    Id = _id.Value,
                    TemplateName = textBoxTemplateName.Text,
                    TicketTemplateBodyId = (tabPageBody.Controls[0] as ControlTicketTemplateViewerBody).GetModel.Id,
                    TicketTemplateDocumentSettingSetBindingModels = _ticketTemplateDocumentSettingPageViewModel?.List.Select(x => new TicketTemplateDocumentSettingSetBindingModel
                        {
                            Id = x.Id,
                            InnerXml = x.InnerXml,
                            Order = x.Order,
                            TicketTemplateId = _id.Value
                        })?.ToList(),
                    TicketTemplateFontTableSetBindingModels = _ticketTemplateFontTablePageViewModel?.List.Select(x => new TicketTemplateFontTableSetBindingModel
                        {
                            Id = x.Id,
                            InnerXml = x.InnerXml,
                            Order = x.Order,
                            TicketTemplateId = _id.Value
                        })?.ToList(),
                    TicketTemplateNumberingSetBindingModels = _ticketTemplateNumberingPageViewModel?.List.Select(x => new TicketTemplateNumberingSetBindingModel
                        {
                            Id = x.Id,
                            InnerXml = x.InnerXml,
                            Order = x.Order,
                            TicketTemplateId = _id.Value
                        })?.ToList(),
                    TicketTemplateStyleDefinitionSetBindingModels = _ticketTemplateStyleDefinitionPageViewModel?.List.Select(x => new TicketTemplateStyleDefinitionSetBindingModel
                        {
                            Id = x.Id,
                            InnerXml = x.InnerXml,
                            Order = x.Order,
                            TicketTemplateId = _id.Value
                        })?.ToList(),
                    TicketTemplateThemePartSetBindingModels = _ticketTemplateThemePartPageViewModel?.List.Select(x => new TicketTemplateThemePartSetBindingModel
                        {
                            Id = x.Id,
                            InnerXml = x.InnerXml,
                            Order = x.Order,
                            TicketTemplateId = _id.Value
                        })?.ToList(),
                    TicketTemplateWebSettingSetBindingModels = _ticketTemplateWebSettingPageViewModel?.List.Select(x => new TicketTemplateWebSettingSetBindingModel
                        {
                            Id = x.Id,
                            InnerXml = x.InnerXml,
                            Order = x.Order,
                            TicketTemplateId = _id.Value
                        })?.ToList()
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

                    _ticketTemplateDocumentSettingPageViewModel = template.Result.TicketTemplateDocumentSettingPageViewModel;
                    _ticketTemplateFontTablePageViewModel = template.Result.TicketTemplateFontTablePageViewModel;
                    _ticketTemplateNumberingPageViewModel = template.Result.TicketTemplateNumberingPageViewModel;
                    _ticketTemplateStyleDefinitionPageViewModel = template.Result.TicketTemplateStyleDefinitionPageViewModel;
                    _ticketTemplateThemePartPageViewModel = template.Result.TicketTemplateThemePartPageViewModel;
                    _ticketTemplateWebSettingPageViewModel = template.Result.TicketTemplateWebSettingPageViewModel;
                }
                else
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", template.Errors);
                }
            }
        }
    }
}