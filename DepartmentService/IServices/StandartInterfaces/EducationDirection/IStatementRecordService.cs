using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
    public interface IStatementRecordService
    {
        /// <summary>
		/// Получение списка элементов записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StatementRecordPageViewModel> GetStatementRecords(StatementRecordGetBindingModel model);

        /// <summary>
		/// Получение элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StatementRecordViewModel> GetStatementRecord(StatementRecordGetBindingModel model);

        /// <summary>
        /// Для вывода сводной ведомости
        /// </summary>
        /// <returns></returns>
        ResultService<List<object[]>> GetSummaryStatement(StudentGroupGetBindingModel model);

        /// <summary>
        /// Создание новой элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateAllFindStatementRecord(AcademicYearGetBindingModel model);

        /// <summary>
		/// Создание новой элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateStatementRecord(StatementRecordSetBindingModel model);

        /// <summary>
        /// Изменение элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStatementRecord(StatementRecordSetBindingModel model);

        /// <summary>
        /// Удаление элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStatementRecord(StatementRecordGetBindingModel model);
    }
}