using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface ILecturerPostSerivce
    {
        /// <summary>
        /// Получение списка должностей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<LecturerPostPageViewModel> GetLecturerPosts(LecturerPostGetBindingModel model);

        /// <summary>
        /// Получения должности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<LecturerPostViewModel> GetLecturerPost(LecturerPostGetBindingModel model);

        /// <summary>
        /// Создание новой должности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateLecturerPost(LecturerPostSetBindingModel model);

        /// <summary>
        /// Изменение должности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateLecturerPost(LecturerPostSetBindingModel model);

        /// <summary>
        /// Удаление должности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteLecturerPost(LecturerPostGetBindingModel model);
    }
}
