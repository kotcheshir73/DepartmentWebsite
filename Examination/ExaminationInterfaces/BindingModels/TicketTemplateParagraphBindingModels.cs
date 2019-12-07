using System;
using System.Collections.Generic;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateParagraphGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? TicketTemplateBodyId { get; set; }
    }

    public class TicketTemplateParagraphSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid? TicketTemplateBodyId { get; set; }
        
        public Guid? TicketTemplateTableCellId { get; set; }
        
        public Guid? TicketTemplateParagraphPropertiesId { get; set; }

        public TicketTemplateParagraphPropertiesSetBindingModel TicketTemplateParagraphPropertiesSetBindingModel { get; set; }

        public List<TicketTemplateParagraphRunSetBindingModel> TicketTemplateParagraphRunSetBindingModels { get; set; }

        public int Order { get; set; }
    }
}