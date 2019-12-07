using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateDocumentSettingGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? TicketTemplateId { get; set; }
    }

    public class TicketTemplateDocumentSettingSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid TicketTemplateId { get; set; }

        public string InnerXml { get; set; }

        public int Order { get; set; }
    }
}