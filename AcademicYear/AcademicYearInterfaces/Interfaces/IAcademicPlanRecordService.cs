using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using Tools;

namespace AcademicYearInterfaces.Interfaces
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
        /// Получение списка контингентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ContingentPageViewModel> GetContingents(ContingentGetBindingModel model);

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
		ResultService CreateAcademicPlanRecord(AcademicPlanRecordSetBindingModel model);

		/// <summary>
		/// Изменение записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateAcademicPlanRecord(AcademicPlanRecordSetBindingModel model);

		/// <summary>
		/// Удаление записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteAcademicPlanRecord(AcademicPlanRecordGetBindingModel model);
	}
}