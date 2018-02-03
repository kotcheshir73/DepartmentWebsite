using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
	public interface ITimeNormService
	{
		/// <summary>
		/// Получение списка видов нагрузок
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<KindOfLoadPageViewModel> GetKindOfLoads(KindOfLoadGetBindingModel model);

        /// <summary>
        /// Получение списка учебных годов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model);

        /// <summary>
        /// Получение списка норм времени
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<TimeNormPageViewModel> GetTimeNorms(TimeNormGetBindingModel model);

		/// <summary>
		/// Получения нормы времени
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<TimeNormViewModel> GetTimeNorm(TimeNormGetBindingModel model);

		/// <summary>
		/// Создание новой нормы времени
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateTimeNorm(TimeNormRecordBindingModel model);

		/// <summary>
		/// Изменение нормы времени
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateTimeNorm(TimeNormRecordBindingModel model);

		/// <summary>
		/// Удаление нормы времени
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteTimeNorm(TimeNormGetBindingModel model);
	}
}
