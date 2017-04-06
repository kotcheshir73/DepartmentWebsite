using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
	public interface ILoadDistributionService
	{
		/// <summary>
		/// Получение списка распределений нагрузок
		/// </summary>
		/// <returns></returns>
		ResultService<List<LoadDistributionViewModel>> GetLoadDistributions();

		/// <summary>
		/// Получение списка учебных годов
		/// </summary>
		/// <returns></returns>
		ResultService<List<AcademicYearViewModel>> GetAcademicYears();

		/// <summary>
		/// Получения распределения нагрузок
		/// </summary>
		/// <param name="model">Идентификатор распределения нагрузок</param>
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
