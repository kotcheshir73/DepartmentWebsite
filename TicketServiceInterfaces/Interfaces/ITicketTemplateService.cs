using DepartmentModel;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.ViewModels;

namespace TicketServiceInterfaces.Interfaces
{
    public interface ITicketTemplateService
    {
        /// <summary>
        /// Получение списка экзаменов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ExaminationTemplatePageViewModel> GetExaminationTemplates(ExaminationTemplateGetBindingModel model);

        /// <summary>
        /// Получение списка экзаменов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<TicketTemplatePageViewModel> GetTicketTemplates(TicketTemplateGetBindingModel model);

        /// <summary>
        /// Получения экзамена
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<TicketTemplateViewModel> GetTicketTemplate(TicketTemplateGetBindingModel model);

        /// <summary>
        /// Изменение экзамена
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateTicketTemplate(TicketTemplateSetBindingModel model);

        /// <summary>
        /// Удаление экзамена
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteTicketTemplate(TicketTemplateGetBindingModel model);
    }
}