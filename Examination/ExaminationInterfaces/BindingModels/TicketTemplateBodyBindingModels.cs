using System;
using System.Collections.Generic;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateBodyGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? TicketTemplateId { get; set; }
    }

    public class TicketTemplateBodySetBindingModel : PageSettingSetBinidingModel
    {
        public Guid? TicketTemplateId { get; set; }
        
        public Guid? TicketTemplateBodyPropertiesId { get; set; }

        public TicketTemplateBodyPropertiesSetBindingModel TicketTemplateBodyPropertiesSetBindingModel { get; set; }

        public List<TicketTemplateParagraphSetBindingModel> TicketTemplateParagraphSetBindingModels { get; set; }
    }
}