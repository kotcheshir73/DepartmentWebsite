﻿using DepartmentService.ViewModels;
using System;

namespace TicketServiceInterfaces.ViewModels
{
    public class ExaminationTemplateBlockPageViewModel : PageViewModel<ExaminationTemplateBlockViewModel> { }

    public class ExaminationTemplateBlockViewModel
    {
        public Guid Id { get; set; }

        public Guid ExaminationTemplateId { get; set; }

        public string ExaminationTemplateName { get; set; }

        public string BlockName { get; set; }

        public int CountQuestionInTicket { get; set; }
    }
}