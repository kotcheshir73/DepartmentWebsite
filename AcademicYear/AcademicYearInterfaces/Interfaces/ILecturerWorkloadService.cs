using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.ViewModels;
using Tools;

namespace AcademicYearInterfaces.Interfaces
{
    public interface ILecturerWorkloadService
    {
        /// <summary>
		/// Получение списка элементов записи ставок
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<LecturerWorkloadPageViewModel> GetLecturerWorkloads(LecturerWorkloadGetBindingModel model);

        /// <summary>
		/// Получение элемента записи ставки
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<LecturerWorkloadViewModel> GetLecturerWorkload(LecturerWorkloadGetBindingModel model);
        
        /// <summary>
		/// Создание новой элемента записи ставки
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateLecturerWorkload(LecturerWorkloadSetBindingModel model);

        /// <summary>
        /// Изменение элемента записи ставки
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateLecturerWorkload(LecturerWorkloadSetBindingModel model);

        /// <summary>
        /// Удаление элемента записи ставки
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteLecturerWorkload(LecturerWorkloadGetBindingModel model);
    }
}