using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
	public interface IClassroomService
    {
		/// <summary>
		/// Получение списка аудиторий
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<ClassroomPageViewModel> GetClassrooms(ClassroomGetBindingModel model);

		/// <summary>
		/// Получения аудитории
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<ClassroomViewModel> GetClassroom(ClassroomGetBindingModel model);

        /// <summary>
        /// Создание новой аудитории
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateClassroom(ClassroomSetBindingModel model);

        /// <summary>
        /// Изменение аудитории
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateClassroom(ClassroomSetBindingModel model);

        /// <summary>
        /// Удаление аудитории
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteClassroom(ClassroomGetBindingModel model);
    }
}
