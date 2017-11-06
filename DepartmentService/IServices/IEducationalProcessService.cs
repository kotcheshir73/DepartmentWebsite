using DepartmentDAL;
using DepartmentService.BindingModels;

namespace DepartmentService.IServices
{
	public interface IEducationalProcessService
	{
		/// <summary>
		/// Загрузка записей учебного плана из xml
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService LoadFromXMLAcademicPlanRecord(EducationalProcessLoadFromXMLBindingModel model);

		/// <summary>
		/// Формирование/перерасчет учебной нагрузки на год
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService MakeLoadDistribution(LoadDistributionGetBindingModel model);
	}
}
