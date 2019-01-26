using DepartmentModel;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.ViewModels;

namespace TicketServiceInterfaces.Interfaces
{
    public interface ITicketProcess
    {
        ResultService<TicketTemplateViewModel> LoadTemplate(TicketProcessLoadTemplateBindingModel model);

        ResultService SaveTemplate(TicketProcessLoadTemplateBindingModel model);

        ResultService LoadQuestions(TicketProcessLoadQuestionsBindingModel model);
    }
}