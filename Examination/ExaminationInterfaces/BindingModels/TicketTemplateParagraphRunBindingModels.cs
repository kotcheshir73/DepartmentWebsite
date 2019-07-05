using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateParagraphRunGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? TicketTemplateParagraphId { get; set; }
    }

    public class TicketTemplateParagraphRunSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid TicketTemplateParagraphId { get; set; }
        
        public Guid? TicketTemplateRunPropertiesId { get; set; }

        public TicketTemplateParagraphRunPropertiesSetBindingModel TicketTemplateParagraphRunPropertiesSetBindingModel { get; set; }

        public string Text { get; set; }

        public bool TabChar { get; set; }

        public bool Break { get; set; }

        public int Order { get; set; }
    }
}