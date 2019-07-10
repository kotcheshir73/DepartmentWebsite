using System;
using System.Collections.Generic;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateGetBindingModel : PageSettingGetBinidingModel { }

    public class TicketTemplateSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid? TicketTemplateBodyId { get; set; }

        public string TemplateName { get; set; }

        public TicketTemplateBodySetBindingModel TicketTemplateBodySetBindingModel { get; set; }

        public List<TicketTemplateDocumentSettingSetBindingModel> TicketTemplateDocumentSettingSetBindingModels { get; set; }

        public List<TicketTemplateFontTableSetBindingModel> TicketTemplateFontTableSetBindingModels { get; set; }

        public List<TicketTemplateNumberingSetBindingModel> TicketTemplateNumberingSetBindingModels { get; set; }

        public List<TicketTemplateStyleDefinitionSetBindingModel> TicketTemplateStyleDefinitionSetBindingModels { get; set; }

        public List<TicketTemplateThemePartSetBindingModel> TicketTemplateThemePartSetBindingModels { get; set; }

        public List<TicketTemplateWebSettingSetBindingModel> TicketTemplateWebSettingSetBindingModels { get; set; }
    }
}