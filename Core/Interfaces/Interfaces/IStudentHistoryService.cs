using Interfaces.BindingModels;
using Interfaces.ViewModels;

namespace Interfaces.Interfaces
{
    public interface IStudentHistoryService
    {
		/// <summary>
		/// Получение списка историй студентов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StudentHistoryPageViewModel> GetStudentHistorys(StudentHistoryGetBindingModel model);

		/// <summary>
		/// Получения записи по историям студентов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StudentHistoryViewModel> GetStudentHistory(StudentHistoryGetBindingModel model);

        /// <summary>
        /// Создание новой записи по историям студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateStudentHistory(StudentHistorySetBindingModel model);

        /// <summary>
        /// Изменение записи по историям студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStudentHistory(StudentHistorySetBindingModel model);

        /// <summary>
        /// Удаление записи по историям студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStudentHistory(StudentHistoryGetBindingModel model);
    }
}