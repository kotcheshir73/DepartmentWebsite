using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

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
        /// Создание записей по контингенту на основе учебных планов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateContingentForAcademicYear(AcademicYearGetBindingModel model);

        /// <summary>
        /// Формирование/перерасчет учебной нагрузки на год
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService MakeLoadDistribution(LoadDistributionGetBindingModel model);

        /// <summary>
        /// Получение списка учебных планов для дисциплины за конкретный год
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<AcademicPlanRecordForDiciplinePageViewModel> GetAcademicPlanRecordsForDiscipline(AcademicPlanRecrodsForDiciplineBindingModel model);

        /// <summary>
        /// Получение записей расписания семестра, зачетов и экзаменов по дисциплине
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ScheduleRecordsForDisciplinePageViewModel> GetScheduleRecordsForDiciplinePageViewModel(ScheduleRecordsForDiciplineBindingModel model);
    }
}
