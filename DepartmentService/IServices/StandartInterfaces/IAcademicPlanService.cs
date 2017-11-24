using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
	public interface IAcademicPlanService
	{
		/// <summary>
		/// Получение списка учебных годов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model);

		/// <summary>
		/// Получение списка направлений
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model);

		/// <summary>
		/// Получение списка учебных планов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<AcademicPlanPageViewModel> GetAcademicPlans(AcademicPlanGetBindingModel model);

		/// <summary>
		/// Получения учебного плана
		/// </summary>
		/// <param name="model"></param>
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
