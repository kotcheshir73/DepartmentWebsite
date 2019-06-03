using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using Tools;

namespace ExaminationInterfaces.Interfaces
{
    public interface ITicketTemplateParagraphService
    {
        /// <summary>
		/// Получение списка параграфов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<TicketTemplateParagraphPageViewModel> GetTicketTemplateParagraphs(TicketTemplateParagraphGetBindingModel model);

        /// <summary>
		/// Получение параграфа
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<TicketTemplateParagraphViewModel> GetTicketTemplateParagraph(TicketTemplateParagraphGetBindingModel model);

        /// <summary>
        /// Создание параграфа
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateTicketTemplateParagraph(TicketTemplateParagraphSetBindingModel model);

        /// <summary>
        /// Изменение параграфа
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateTicketTemplateParagraph(TicketTemplateParagraphSetBindingModel model);

        /// <summary>
        /// Удаление параграфа
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteTicketTemplateParagraph(TicketTemplateParagraphGetBindingModel model);
    }
}