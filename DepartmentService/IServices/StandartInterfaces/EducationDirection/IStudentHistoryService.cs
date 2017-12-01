using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
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
        ResultService CreateStudentHistory(StudentHistoryRecordBindingModel model);

        /// <summary>
        /// Изменение записи по историям студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStudentHistory(StudentHistoryRecordBindingModel model);

        /// <summary>
        /// Удаление записи по историям студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStudentHistory(StudentHistoryGetBindingModel model);
    }
}
