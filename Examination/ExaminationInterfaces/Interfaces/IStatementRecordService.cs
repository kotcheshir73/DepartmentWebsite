using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using Tools;

namespace ExaminationInterfaces.Interfaces
{
    public interface IStatementRecordService
    {
        /// <summary>
		/// Получение списка ведомостей
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StatementPageViewModel> GetStatements(StatementGetBindingModel model);

        /// <summary>
        /// Получение списка студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StudentPageViewModel> GetStudents(StudentGetBindingModel model);

        /// <summary>
		/// Получение списка элементов ведомости
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StatementRecordPageViewModel> GetStatementRecords(StatementRecordGetBindingModel model);

        /// <summary>
		/// Получение элемента ведомости
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StatementRecordViewModel> GetStatementRecord(StatementRecordGetBindingModel model);

        /// <summary>
		/// Создание новой элемента ведомости
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateStatementRecord(StatementRecordSetBindingModel model);

        /// <summary>
        /// Изменение элемента ведомости
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStatementRecord(StatementRecordSetBindingModel model);

        /// <summary>
        /// Удаление элемента ведомости
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStatementRecord(StatementRecordGetBindingModel model);
    }
}