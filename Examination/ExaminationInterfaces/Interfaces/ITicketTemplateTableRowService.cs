using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using Tools;

namespace ExaminationInterfaces.Interfaces
{
    public interface ITicketTemplateTableRowService
    {
        /// <summary>
		/// Получение списка строк таблицы
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<TicketTemplateTableRowPageViewModel> GetTicketTemplateTableRows(TicketTemplateTableRowGetBindingModel model);

        /// <summary>
		/// Получение строки таблицы
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<TicketTemplateTableRowViewModel> GetTicketTemplateTableRow(TicketTemplateTableRowGetBindingModel model);

        /// <summary>
        /// Создание строки таблицы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateTicketTemplateTableRow(TicketTemplateTableRowSetBindingModel model);

        /// <summary>
        /// Изменение таблицы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateTicketTemplateTableRow(TicketTemplateTableRowSetBindingModel model);

        /// <summary>
        /// Удаление строки таблицы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteTicketTemplateTableRow(TicketTemplateTableRowGetBindingModel model);
    }
}