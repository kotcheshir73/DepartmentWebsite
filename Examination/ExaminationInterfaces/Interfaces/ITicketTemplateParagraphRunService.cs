using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using Tools;

namespace ExaminationInterfaces.Interfaces
{
    public interface ITicketTemplateParagraphRunService
    {
        /// <summary>
		/// Получение списка строк параграфа
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<TicketTemplateParagraphRunPageViewModel> GetTicketTemplateParagraphRuns(TicketTemplateParagraphRunGetBindingModel model);

        /// <summary>
		/// Получение строки параграфа
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<TicketTemplateParagraphRunViewModel> GetTicketTemplateParagraphRun(TicketTemplateParagraphRunGetBindingModel model);

        /// <summary>
        /// Создание строки параграфа
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateTicketTemplateParagraphRun(TicketTemplateParagraphRunSetBindingModel model);

        /// <summary>
        /// Изменение строки параграфа
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateTicketTemplateParagraphRun(TicketTemplateParagraphRunSetBindingModel model);

        /// <summary>
        /// Удаление строки параграфа
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteTicketTemplateParagraphRun(TicketTemplateParagraphRunGetBindingModel model);
    }
}