using Interfaces.BindingModels;
using Interfaces.ViewModels;
using Tools;

namespace Interfaces.Interfaces
{
    public interface ILecturerService
    {
        /// <summary>
        /// Получение списка должностей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<LecturerPostPageViewModel> GetLecturerPosts(LecturerPostGetBindingModel model);

        /// <summary>
        /// Получение списка преподавателей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model);

		/// <summary>
		/// Получения преподавателя
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<LecturerViewModel> GetLecturer(LecturerGetBindingModel model);

		/// <summary>
		/// Создание новогопреподавателя
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateLecturer(LecturerSetBindingModel model);

		/// <summary>
		/// Изменение преподавателя
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateLecturer(LecturerSetBindingModel model);

		/// <summary>
		/// Удаление преподавателя
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteLecturer(LecturerGetBindingModel model);
	}
}