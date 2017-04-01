using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;
namespace DepartmentService.IServices
{
	public interface IAcademicPlanService
	{
		/// <summary>
		/// Получение списка учебных планов
		/// </summary>
		/// <returns></returns>
		ResultService<List<AcademicPlanViewModel>> GetAcademicPlans();

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
		/// Получения учебного плана
		/// </summary>
		/// <param name="model">Идентификатор учебного плана</param>
		/// <returns></returns>
		ResultService<AcademicPlanViewModel> GetAcademicPlan(AcademicPlanGetBindingModel model);

		/// <summary>
		/// Создание нового учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateAcademicPlan(AcademicPlanRecordBindingModel model);

		/// <summary>
		/// Изменение учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateAcademicPlan(AcademicPlanRecordBindingModel model);

		/// <summary>
		/// Удаление учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteAcademicPlan(AcademicPlanGetBindingModel model);
	}
}
