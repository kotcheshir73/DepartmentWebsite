using System;
using System.Collections.Generic;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class TicketTemplateTableCellGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? TicketTemplateTableRowId { get; set; }
    }

    public class TicketTemplateTableCellSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid? TicketTemplateTableRowId { get; set; }

        public Guid? TicketTemplateTableCellPropertiesId { get; set; }

        public TicketTemplateTableCellPropertiesSetBindingModel TicketTemplateTableCellPropertiesSetBindingModel { get; set; }

        public List<TicketTemplateParagraphSetBindingModel> TicketTemplateParagraphSetBindingModels { get; set; }

        public int Order { get; set; }
    }
}