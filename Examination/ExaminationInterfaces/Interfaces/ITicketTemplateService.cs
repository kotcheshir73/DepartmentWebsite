using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using Tools;

namespace ExaminationInterfaces.Interfaces
{
    public interface ITicketTemplateService
    {
        /// <summary>
        /// Получение списка шаблонов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<TicketTemplatePageViewModel> GetTicketTemplates(TicketTemplateGetBindingModel model);

        /// <summary>
        /// Получения шаблона
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<TicketTemplateViewModel> GetTicketTemplate(TicketTemplateGetBindingModel model);

        /// <summary>
        /// Создание шаблона
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateTicketTemplate(TicketTemplateSetBindingModel model);

        /// <summary>
        /// Изменение шаблона
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateTicketTemplate(TicketTemplateSetBindingModel model);

        /// <summary>
        /// Удаление шаблона
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteTicketTemplate(TicketTemplateGetBindingModel model);
    }
}