using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
	public interface ITimeNormService
	{
        /// <summary>
        /// Получение списка учебных годов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model);

        /// <summary>
        /// Получение списка блоков дисциплин
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineBlockPageViewModel> GetDisciplineBlocks(DisciplineBlockGetBindingModel model);

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
		ResultService CreateTimeNorm(TimeNormSetBindingModel model);

		/// <summary>
		/// Изменение нормы времени
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateTimeNorm(TimeNormSetBindingModel model);

		/// <summary>
		/// Удаление нормы времени
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteTimeNorm(TimeNormGetBindingModel model);
	}
}
