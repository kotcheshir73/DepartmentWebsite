using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using Tools;

namespace ExaminationInterfaces.Interfaces
{
    public interface ITicketTemplateBodyService
    {
        /// <summary>
		/// Получение списка тел документов шаблона
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<TicketTemplateBodyPageViewModel> GetTicketTemplateBodys(TicketTemplateBodyGetBindingModel model);

        /// <summary>
		/// Получение тела документа шаблона
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<TicketTemplateBodyViewModel> GetTicketTemplateBody(TicketTemplateBodyGetBindingModel model);

        /// <summary>
        /// Создание тела документа шаблона
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateTicketTemplateBody(TicketTemplateBodySetBindingModel model);

        /// <summary>
        /// Изменение тела документа шаблона
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateTicketTemplateBody(TicketTemplateBodySetBindingModel model);

        /// <summary>
        /// Удаление тела документа шаблона
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteTicketTemplateBody(TicketTemplateBodyGetBindingModel model);
    }
}