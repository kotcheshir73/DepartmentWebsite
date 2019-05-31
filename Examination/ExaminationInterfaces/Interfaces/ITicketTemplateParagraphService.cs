using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using Tools;

namespace ExaminationInterfaces.Interfaces
{
    public interface ITicketTemplateParagraphService
    {
        /// <summary>
		/// Получение списка строк параграфа
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<TicketTemplateParagraphPageViewModel> GetTicketTemplateParagraphs(TicketTemplateParagraphGetBindingModel model);

        /// <summary>
		/// Получение строки параграфа
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<TicketTemplateParagraphViewModel> GetTicketTemplateParagraph(TicketTemplateParagraphGetBindingModel model);

        /// <summary>
        /// Создание строки параграфа
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateTicketTemplateParagraph(TicketTemplateParagraphSetBindingModel model);

        /// <summary>
        /// Изменение строки параграфа
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateTicketTemplateParagraph(TicketTemplateParagraphSetBindingModel model);

        /// <summary>
        /// Удаление строки параграфа
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteTicketTemplateParagraph(TicketTemplateParagraphGetBindingModel model);
    }
}