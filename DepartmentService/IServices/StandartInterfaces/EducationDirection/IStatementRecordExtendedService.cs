using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface IStatementRecordExtendedService
    {
        /// <summary>
		/// Получение списка элементов записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StatementRecordExtendedPageViewModel> GetStatementRecordExtendeds(StatementRecordExtendedGetBindingModel model);

        /// <summary>
		/// Получение элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StatementRecordExtendedViewModel> GetStatementRecordExtended(StatementRecordExtendedGetBindingModel model);

        /// <summary>
		/// Создание новой элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateStatementRecordExtended(StatementRecordExtendedSetBindingModel model);

        /// <summary>
        /// Изменение элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStatementRecordExtended(StatementRecordExtendedSetBindingModel model);

        /// <summary>
        /// Удаление элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStatementRecordExtended(StatementRecordExtendedGetBindingModel model);
    }
}