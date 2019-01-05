using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepartmentModel;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketServiceInterfaces.ViewModels;

namespace TicketServiceImplementations.Implementations
{
    public class ExaminationTemplateTicketService : IExaminationTemplateTicketService
    {
        public ResultService CreateExaminationTemplateTicket(ExaminationTemplateTicketSetBindingModel model)
        {
            throw new NotImplementedException();
        }

        public ResultService DeleteExaminationTemplateTicket(ExaminationTemplateTicketGetBindingModel model)
        {
            throw new NotImplementedException();
        }

        public ResultService<ExaminationTemplatePageViewModel> GetExaminationTemplates(ExaminationTemplateGetBindingModel model)
        {
            throw new NotImplementedException();
        }

        public ResultService<ExaminationTemplateTicketViewModel> GetExaminationTemplateTicket(ExaminationTemplateTicketGetBindingModel model)
        {
            throw new NotImplementedException();
        }

        public ResultService<ExaminationTemplateTicketPageViewModel> GetExaminationTemplateTickets(ExaminationTemplateTicketGetBindingModel model)
        {
            throw new NotImplementedException();
        }

        public ResultService UpdateExaminationTemplateTicket(ExaminationTemplateTicketSetBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
