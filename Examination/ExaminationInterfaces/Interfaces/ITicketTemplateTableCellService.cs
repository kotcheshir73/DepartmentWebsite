using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Tools;

namespace ExaminationInterfaces.Interfaces
{
    public interface ITicketTemplateTableCellService
    {
        /// <summary>
		/// Получение списка ячеек строки таблицы
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<TicketTemplateTableCellPageViewModel> GetTicketTemplateTableCells(TicketTemplateTableCellGetBindingModel model);

        /// <summary>
		/// Получение ячейки строки таблицы
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<TicketTemplateTableCellViewModel> GetTicketTemplateTableCell(TicketTemplateTableCellGetBindingModel model);

        /// <summary>
        /// Создание ячейки строки таблицы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateTicketTemplateTableCell(TicketTemplateTableCellSetBindingModel model);

        /// <summary>
        /// Изменение ячейки таблицы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateTicketTemplateTableCell(TicketTemplateTableCellSetBindingModel model);

        /// <summary>
        /// Удаление ячейки строки таблицы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteTicketTemplateTableCell(TicketTemplateTableCellGetBindingModel model);
    }
}
