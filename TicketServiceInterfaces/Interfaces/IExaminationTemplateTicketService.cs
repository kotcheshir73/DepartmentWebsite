using DepartmentModel;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.ViewModels;

namespace TicketServiceInterfaces.Interfaces
{
    public interface IExaminationTemplateTicketService
    {
        /// <summary>
        /// Получение списка экзаменов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ExaminationTemplatePageViewModel> GetExaminationTemplates(ExaminationTemplateGetBindingModel model);

        /// <summary>
        /// Получение списка билетов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ExaminationTemplateTicketPageViewModel> GetExaminationTemplateTickets(ExaminationTemplateTicketGetBindingModel model);

        /// <summary>
        /// Получения билета
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ExaminationTemplateTicketViewModel> GetExaminationTemplateTicket(ExaminationTemplateTicketGetBindingModel model);

        /// <summary>
        /// Создание нового билета
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateExaminationTemplateTicket(ExaminationTemplateTicketSetBindingModel model);

        /// <summary>
        /// Изменение билета
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateExaminationTemplateTicket(ExaminationTemplateTicketSetBindingModel model);

        /// <summary>
        /// Удаление билета
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteExaminationTemplateTicket(ExaminationTemplateTicketGetBindingModel model);
    }
}