using System;

namespace TicketServiceInterfaces.BindingModels
{
    public class TicketProcessLoadTemplateBindingModel
    {
        public string FileName { get; set; }

        public string TemplateName { get; set; }

        public Guid? ExaminationTemplateId { get; set; }
    }

    public class TicketProcessLoadQuestionsBindingModel
    {
        public string FileName { get; set; }

        public Guid ExaminationTemplateBlockId { get; set; }
    }
}