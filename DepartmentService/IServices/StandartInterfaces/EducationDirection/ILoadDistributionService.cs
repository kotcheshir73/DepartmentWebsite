using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
	public interface ILoadDistributionService
	{
		/// <summary>
		/// Получение списка учебных годов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model);

		/// <summary>
		/// Получение списка распределений нагрузок
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<LoadDistributionPageViewModel> GetLoadDistributions(LoadDistributionGetBindingModel model);

		/// <summary>
		/// Получения распределения нагрузок
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<LoadDistributionViewModel> GetLoadDistribution(LoadDistributionGetBindingModel model);

		/// <summary>
		/// Создание нового распределения нагрузок
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateLoadDistribution(LoadDistributionRecordBindingModel model);

		/// <summary>
		/// Изменение распределения нагрузок
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateLoadDistribution(LoadDistributionRecordBindingModel model);

		/// <summary>
		/// Удаление распределения нагрузок
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteLoadDistribution(LoadDistributionGetBindingModel model);
	}
}
