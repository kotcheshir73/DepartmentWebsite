using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using Tools;

namespace BaseInterfaces.Interfaces
{
	public interface ILecturerDepartmentPostSerivce
    {
        /// <summary>
        /// Получение списка должностей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<LecturerDepartmentPostPageViewModel> GetLecturerDepartmentPosts(LecturerDepartmentPostGetBindingModel model);

        /// <summary>
        /// Получения должности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<LecturerDepartmentPostViewModel> GetLecturerDepartmentPost(LecturerDepartmentPostGetBindingModel model);

        /// <summary>
        /// Создание новой должности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateLecturerDepartmentPost(LecturerDepartmentPostSetBindingModel model);

        /// <summary>
        /// Изменение должности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateLecturerDepartmentPost(LecturerDepartmentPostSetBindingModel model);

        /// <summary>
        /// Удаление должности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteLecturerDepartmentPost(LecturerDepartmentPostGetBindingModel model);
    }
}