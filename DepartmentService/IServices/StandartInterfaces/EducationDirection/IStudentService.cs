using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
	public interface IStudentService
    {
		/// <summary>
		/// Получение списка групп
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StudentGroupPageViewModel> GetStudentGroups(StudentGroupGetBindingModel model);

		/// <summary>
		/// Получение списка студентов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StudentPageViewModel> GetStudents(StudentGetBindingModel model);

		/// <summary>
		/// Получения студента
		/// </summary>
		/// <param name="model">Идентификатор студента</param>
		/// <returns></returns>
		ResultService<StudentViewModel> GetStudent(StudentGetBindingModel model);

		/// <summary>
		/// Создание студента
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateStudent(StudentRecordBindingModel model);

		/// <summary>
		/// Изменение студента
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateStudent(StudentRecordBindingModel model);

        /// <summary>
        /// Удаление студента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStudent(StudentGetBindingModel model);
    }
}
