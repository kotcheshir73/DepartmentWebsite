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
		/// Получение списка учебных годов
		/// </summary>
		/// <returns></returns>
		ResultService<List<AcademicYearViewModel>> GetAcademicYears();

		/// <summary>
		/// Получение списка направлений
		/// </summary>
		/// <returns></returns>
		ResultService<List<EducationDirectionViewModel>> GetEducationDirections();

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
