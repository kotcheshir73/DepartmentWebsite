using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
	public interface IKindOfLoadService
	{
		/// <summary>
		/// Получение списка видов нагрузок
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<KindOfLoadPageViewModel> GetKindOfLoads(KindOfLoadGetBindingModel model);

		/// <summary>
		/// Получения вида нагрузки
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<KindOfLoadViewModel> GetKindOfLoad(KindOfLoadGetBindingModel model);

		/// <summary>
		/// Создание нового вида нагрузки
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateKindOfLoad(KindOfLoadRecordBindingModel model);

		/// <summary>
		/// Изменение вида нагрузки
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateKindOfLoad(KindOfLoadRecordBindingModel model);

		/// <summary>
		/// Удаление вида нагрузки
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteKindOfLoad(KindOfLoadGetBindingModel model);
	}
}
