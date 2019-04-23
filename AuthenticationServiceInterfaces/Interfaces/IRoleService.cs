using AuthenticationServiceInterfaces.BindingModels;
using AuthenticationServiceInterfaces.ViewModels;
using DepartmentModel;

namespace AuthenticationServiceInterfaces.Interfaces
{
    public interface IRoleService
	{
		/// <summary>
		/// Получение списка ролей
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<RolePageViewModel> GetRoles(RoleGetBindingModel model);

		/// <summary>
		/// Получения роли
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<RoleViewModel> GetRole(RoleGetBindingModel model);

		/// <summary>
		/// Создание новой роли
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateRole(RoleSetBindingModel model);

		/// <summary>
		/// Изменение роли
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateRole(RoleSetBindingModel model);

		/// <summary>
		/// Удаление роли
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteRole(RoleGetBindingModel model);
	}
}