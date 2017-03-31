using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
	public interface IKindOfLoadService
	{
		/// <summary>
		/// Получение списка видов нагрузок
		/// </summary>
		/// <returns></returns>
		ResultService<List<KindOfLoadViewModel>> GetKindOfLoads();

		/// <summary>
		/// Получения вида нагрузки
		/// </summary>
		/// <param name="model">Идентификатор вида нагрузки</param>
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
