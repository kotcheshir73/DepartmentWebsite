using System;
using System.Collections.Generic;
using TicketModels.Enums;

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

    public class TicketProcessSynchronizeBlocksByTemplateBindingModel
    {
        public Guid ExaminationTemplateId { get; set; }
    }

    public class TicketProcessMakeTicketsBindingModel
    {
        public Guid ExaminationTemplateId { get; set; }

        public HowCreateTickets HowCreateTickets { get; set; }

        public int? CountTickets { get; set; }

        public Guid? SelectedBlock { get; set; }

        public Dictionary<string, HowUseExaminationBlock> HowUseBlock { get; set; }

        public Dictionary<string, HowGetQuestionFromExaminationBlock> HowGetQuestionFromBlock { get; set; }
    }

    public class TicketProcessUploadTicketsBindingModel
    {
        public string FileName { get; set; }

        public Guid TicketTemplateId { get; set; }
    }

    public class TicketProcessGetParagraphDatasBindingModel
    {
        public Guid TicketTemplateId { get; set; }
    }
}