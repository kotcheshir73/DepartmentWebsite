﻿using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
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