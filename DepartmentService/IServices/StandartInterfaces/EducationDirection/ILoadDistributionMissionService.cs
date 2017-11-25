using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
	public interface ILoadDistributionMissionService
	{
		/// <summary>
		/// Получение списка поручений в распределении нагрузки
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<LoadDistributionMissionPageViewModel> GetLoadDistributionMissions(LoadDistributionMissionGetBindingModel model);

		/// <summary>
		/// Получения поручения в распределении нагрузки
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<LoadDistributionMissionViewModel> GetLoadDistributionMission(LoadDistributionMissionGetBindingModel model);

		/// <summary>
		/// Создание нового поручения в распределении нагрузки
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateLoadDistributionMission(LoadDistributionMissionRecordBindingModel model);

		/// <summary>
		/// Изменение поручения в распределении нагрузки
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateLoadDistributionMission(LoadDistributionMissionRecordBindingModel model);

		/// <summary>
		/// Удаление поручения в распределении нагрузки
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteLoadDistributionMission(LoadDistributionMissionGetBindingModel model);
	}
}
