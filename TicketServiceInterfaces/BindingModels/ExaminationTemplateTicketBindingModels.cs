using System;

namespace TicketServiceInterfaces.BindingModels
{
    public class ExaminationTemplateTicketGetBindingModel
    {
        public Guid? Id { get; set; }

        public Guid? ExaminationTemplateId { get; set; }
    }

    public class ExaminationTemplateTicketSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid ExaminationTemplateId { get; set; }

        public int TicketNumber { get; set; }
    }
}