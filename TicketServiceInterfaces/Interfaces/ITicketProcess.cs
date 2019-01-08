using DepartmentModel;
using TicketServiceInterfaces.BindingModels;

namespace TicketServiceInterfaces.Interfaces
{
    public interface ITicketProcess
    {
        ResultService LoadTemplate(TicketProcessLoadTemplateBindingModel model);
    }
}
