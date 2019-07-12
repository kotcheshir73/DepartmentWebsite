using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using Tools;

namespace ExaminationInterfaces.Interfaces
{
    public interface ITicketTemplateTableService
    {
        /// <summary>
		/// Получение списка таблиц
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<TicketTemplateTablePageViewModel> GetTicketTemplateTables(TicketTemplateTableGetBindingModel model);

        /// <summary>
		/// Получение таблицы
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<TicketTemplateTableViewModel> GetTicketTemplateTable(TicketTemplateTableGetBindingModel model);

        /// <summary>
        /// Создание таблицы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateTicketTemplateTable(TicketTemplateTableSetBindingModel model);

        /// <summary>
        /// Изменение таблицы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateTicketTemplateTable(TicketTemplateTableSetBindingModel model);

        /// <summary>
        /// Удаление таблицы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteTicketTemplateTable(TicketTemplateTableGetBindingModel model);
    }
}
