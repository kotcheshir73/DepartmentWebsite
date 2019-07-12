using System;
using System.Collections.Generic;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateTableRowGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? TicketTemplateTableId { get; set; }
    }

    public class TicketTemplateTableRowSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid TicketTemplateTableId { get; set; }

        public Guid? TicketTemplateTableRowPropertiesId { get; set; }

        public TicketTemplateTableRowPropertiesSetBindingModel TicketTemplateTableRowPropertiesSetBindingModel { get; set; }

        public List<TicketTemplateTableCellSetBindingModel> TicketTemplateTableCellSetBindingModels { get; set; }

        public int Order { get; set; }
    }
}