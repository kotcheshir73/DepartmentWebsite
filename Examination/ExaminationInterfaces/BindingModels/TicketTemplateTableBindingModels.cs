using System;
using System.Collections.Generic;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateTableGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? TicketTemplateBodyId { get; set; }
    }

    public class TicketTemplateTableSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid? TicketTemplateBodyId { get; set; }

        public Guid? TicketTemplateTablePropertiesId { get; set; }

        public TicketTemplateTablePropertiesSetBindingModel TicketTemplateTablePropertiesSetBindingModel { get; set; }

        public List<TicketTemplateTableRowSetBindingModel> TicketTemplateTableRowSetBindingModels { get; set; }

        public int Order { get; set; }
    }
}