using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
	public interface IAccessService
	{
		/// <summary>
		/// Получение списка прав доступа
		/// </summary>
		/// <returns></returns>
		/// <param name="model"></param>
		ResultService<List<AccessViewModel>> GetAccesses(AccessGetBindingModel model);

		/// <summary>
		/// Получения права доступа
		/// </summary>
		/// <param name="model">Идентификатор права доступа</param>
		/// <returns></returns>
		ResultService<AccessViewModel> GetAccess(AccessGetBindingModel model);

		/// <summary>
		/// Создание новых прав доступа
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateAccess(AccessRecordBindingModel model);

		/// <summary>
		/// Изменение прав доступа
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateAccess(AccessRecordBindingModel model);

		/// <summary>
		/// Удаление прав доступа
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteAccess(AccessGetBindingModel model);
	}
}
