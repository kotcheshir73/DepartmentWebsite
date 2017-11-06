using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
	public interface IRoleService
	{
		/// <summary>
		/// Получение списка ролей
		/// </summary>
		/// <returns></returns>
		ResultService<List<RoleViewModel>> GetRoles();

		/// <summary>
		/// Получения роли
		/// </summary>
		/// <param name="model">Идентификатор роли</param>
		/// <returns></returns>
		ResultService<RoleViewModel> GetRole(RoleGetBindingModel model);

		/// <summary>
		/// Создание новой роли
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateRole(RoleRecordBindingModel model);

		/// <summary>
		/// Изменение роли
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateRole(RoleRecordBindingModel model);

		/// <summary>
		/// Удаление роли
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteRole(RoleGetBindingModel model);
	}
}
