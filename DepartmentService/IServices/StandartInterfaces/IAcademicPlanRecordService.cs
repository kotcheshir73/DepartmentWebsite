using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
	public interface IAcademicPlanRecordService
	{
		/// <summary>
		/// Получение списка учебных планов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<AcademicPlanPageViewModel> GetAcademicPlans(AcademicPlanGetBindingModel model);

		/// <summary>
		/// Получение списка дисциплин
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model);

		/// <summary>
		/// Получение списка видов нагрузки
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<KindOfLoadPageViewModel> GetKindOfLoads(KindOfLoadGetBindingModel model);

		/// <summary>
		/// Получение списка записей учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<AcademicPlanRecordPageViewModel> GetAcademicPlanRecords(AcademicPlanRecordGetBindingModel model);

		/// <summary>
		/// Получения записи учебного плана
		/// </summary>
		/// <param name="model"></param>
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
