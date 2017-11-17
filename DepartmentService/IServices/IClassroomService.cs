using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
    public interface IClassroomService
    {
		/// <summary>
		/// Получение списка аудиторий
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<List<ClassroomViewModel>> GetClassrooms(ClassroomGetBindingModel model);

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
        ResultService CreateClassroom(ClassroomRecordBindingModel model);

        /// <summary>
        /// Изменение аудитории
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateClassroom(ClassroomRecordBindingModel model);

        /// <summary>
        /// Удаление аудитории
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteClassroom(ClassroomGetBindingModel model);
    }
}
