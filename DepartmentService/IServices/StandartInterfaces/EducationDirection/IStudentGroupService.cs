using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
	public interface IStudentGroupService
    {
		/// <summary>
		/// Получение списка направлений
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model);

        /// <summary>
        /// Получение списка преподавателей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model);

        /// <summary>
        /// Получение списка групп
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StudentGroupPageViewModel> GetStudentGroups(StudentGroupGetBindingModel model);

		/// <summary>
		/// Получения группы
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StudentGroupViewModel> GetStudentGroup(StudentGroupGetBindingModel model);

        /// <summary>
        /// Создание новой группы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateStudentGroup(StudentGroupRecordBindingModel model);

        /// <summary>
        /// Изменение группы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStudentGroup(StudentGroupRecordBindingModel model);

        /// <summary>
        /// Удаление группы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStudentGroup(StudentGroupGetBindingModel model);
    }
}
