using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

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
        /// Загрузка записей учебного плана из xml по синей звездочке
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService LoadFromBlueAsteriskAcademicPlanRecord(EducationalProcessLoadFromXMLBindingModel model);

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
        ResultService<List<object[]>> GetAcademicYearLoading(AcademicYearGetBindingModel model);

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

        /// <summary>
        /// Дублирование записей из одного учебного года в другой
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DuplicateAcademicYearElements(EducationalProcessDuplicateAcademicYear model);

        /// <summary>
        /// Расчет фактических часов для учебного года
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CalcFactHoursForAcademicYear(AcademicYearGetBindingModel model);
    }
}
