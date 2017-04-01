using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
	public interface IContingentService
	{
		/// <summary>
		/// Получение списка контингентов
		/// </summary>
		/// <returns></returns>
		ResultService<List<ContingentViewModel>> GetContingents();

		/// <summary>
		/// Получение списка групп
		/// </summary>
		/// <returns></returns>
		ResultService<List<StudentGroupViewModel>> GetStudentGroups();

		/// <summary>
		/// Получения контингента
		/// </summary>
		/// <param name="model">Идентификатор контингента</param>
		/// <returns></returns>
		ResultService<ContingentViewModel> GetContingent(ContingentGetBindingModel model);

		/// <summary>
		/// Создание нового контингента
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateContingent(ContingentRecordBindingModel model);

		/// <summary>
		/// Изменение контингента
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateContingent(ContingentRecordBindingModel model);

		/// <summary>
		/// Удаление контингента
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteContingent(ContingentGetBindingModel model);
	}
}
