using AuthenticationServiceInterfaces.BindingModels;
using AuthenticationServiceInterfaces.ViewModels;
using DepartmentModel;

namespace AuthenticationServiceInterfaces.Interfaces
{
    public interface IAccessService
	{
		/// <summary>
		/// Получение списка прав доступа
		/// </summary>
		/// <returns></returns>
		/// <param name="model"></param>
		ResultService<AccessPageViewModel> GetAccesses(AccessGetBindingModel model);

		/// <summary>
		/// Получения права доступа
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<AccessViewModel> GetAccess(AccessGetBindingModel model);

		/// <summary>
		/// Создание новых прав доступа
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateAccess(AccessSetBindingModel model);

		/// <summary>
		/// Изменение прав доступа
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateAccess(AccessSetBindingModel model);

		/// <summary>
		/// Удаление прав доступа
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteAccess(AccessGetBindingModel model);
	}
}