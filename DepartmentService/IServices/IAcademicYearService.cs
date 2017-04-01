using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
	public interface IAcademicYearService
	{
		/// <summary>
		/// Получение списка учебных годов
		/// </summary>
		/// <returns></returns>
		ResultService<List<AcademicYearViewModel>> GetAcademicYears();

		/// <summary>
		/// Получения учебного года
		/// </summary>
		/// <param name="model">Идентификатор учебного года</param>
		/// <returns></returns>
		ResultService<AcademicYearViewModel> GetAcademicYear(AcademicYearGetBindingModel model);

		/// <summary>
		/// Создание нового учебного года
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateAcademicYear(AcademicYearRecordBindingModel model);

		/// <summary>
		/// Изменение учебного года
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateAcademicYear(AcademicYearRecordBindingModel model);

		/// <summary>
		/// Удаление учебного года
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteAcademicYear(AcademicYearGetBindingModel model);
	}
}
