using DepartmentService.ViewModels;
using System;

namespace TicketServiceInterfaces.ViewModels
{
    public class TicketTemplatePageViewModel : PageViewModel<TicketTemplateViewModel> { }

    public class TicketTemplateViewModel
    {
        public Guid Id { get; set; }

        public string TemplateName { get; set; }

        public TicketProcessBodyViewModel Body { get; set; }
    }
}