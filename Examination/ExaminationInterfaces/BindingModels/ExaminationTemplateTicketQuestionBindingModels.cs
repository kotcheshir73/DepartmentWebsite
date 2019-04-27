using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class ExaminationTemplateTicketQuestionGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? ExaminationTemplateTicketId { get; set; }
    }

    public class ExaminationTemplateTicketQuestionSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid ExaminationTemplateTicketId { get; set; }

        public Guid ExaminationTemplateBlockQuestionId { get; set; }

        public Guid ExaminationTemplateBlockId { get; set; }

        public int Order { get; set; }
    }
}