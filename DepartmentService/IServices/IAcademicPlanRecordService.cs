using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
	public interface IAcademicPlanRecordService
	{
		/// <summary>
		/// Получение списка записей учебного плана
		/// </summary>
		/// <param name="model">Идентификатор учебного плана</param>
		/// <returns></returns>
		ResultService<List<AcademicPlanRecordViewModel>> GetAcademicPlanRecords(AcademicPlanRecordGetBindingModel model);

		/// <summary>
		/// Получение списка учебных планов
		/// </summary>
		/// <returns></returns>
		ResultService<List<AcademicPlanViewModel>> GetAcademicPlans();

		/// <summary>
		/// Получение списка дисциплин
		/// </summary>
		/// <returns></returns>
		ResultService<List<DisciplineViewModel>> GetDisciplines();

		/// <summary>
		/// Получение списка видов нагрузки
		/// </summary>
		/// <returns></returns>
		ResultService<List<KindOfLoadViewModel>> GetKindOfLoads();

		/// <summary>
		/// Загрузка записей учебного плана из xml
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService LoadFromXMLAcademicPlanRecord(AcademicPlanRecordLoadFromXMLBindingModel model);

		/// <summary>
		/// Получения записи учебного плана
		/// </summary>
		/// <param name="model">Идентификатор записи учебного плана</param>
		/// <returns></returns>
		ResultService<AcademicPlanRecordViewModel> GetAcademicPlanRecord(AcademicPlanRecordGetBindingModel model);

		/// <summary>
		/// Создание новой записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateAcademicPlanRecord(AcademicPlanRecordRecordBindingModel model);

		/// <summary>
		/// Изменение записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateAcademicPlanRecord(AcademicPlanRecordRecordBindingModel model);

		/// <summary>
		/// Удаление записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteAcademicPlanRecord(AcademicPlanRecordGetBindingModel model);
	}
}
