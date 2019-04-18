using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using System.Collections.Generic;
using Tools;

namespace AcademicYearInterfaces.Interfaces
{
    public interface IAcademicYearProcess
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
        /// Формирование листа учебной нагрузки по предмету на год
        /// </summary>
        /// <param name="modelYear"></param>
        /// <param name="modelPlanRecord"></param>
        /// <returns></returns>
        ResultService<List<object[]>> GetListAPRE(AcademicYearGetBindingModel modelYear, AcademicPlanRecordGetBindingModel modelPlanRecord);

        /// <summary>
        /// Формирование листа для назначения времени по предмету преподавателю
        /// </summary>
        /// <param name="modelYear"></param>
        /// <param name="modelPlanRecord"></param>
        /// <param name="modelLecturer"></param>
        /// <returns></returns>
        ResultService<List<object[]>> GetListAPRM(AcademicYearGetBindingModel modelYear, AcademicPlanRecordGetBindingModel modelPlanRecord, LecturerGetBindingModel modelLecturer);

        /// <summary>
        /// Получение списка учебных планов для дисциплины за конкретный год
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<AcademicPlanRecordForDiciplinePageViewModel> GetAcademicPlanRecordsForDiscipline(AcademicPlanRecordsForDiciplineBindingModel model);

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

        /// <summary>
        /// Создание потоков на основе лекций
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateStreamsForAcademicYear(EducationalProcessCreateStreams model);

        /// <summary>
		/// Создание всех возможных расчасовок
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateAllFindDisciplineTimeDistributionRecord(AcademicYearGetBindingModel model);

        /// <summary>
		/// Создание всех возможных расчасовок
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateAllFindDisciplineTimeDistribution(AcademicYearGetBindingModel model);

        /// <summary>
		/// Создание нагрузки преподавателей
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateLecturerWorkload(AcademicYearGetBindingModel model);

        /// <summary>
        /// Выгрузка нагрузки преподавателей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService ImportLecturerWorkload(ImportLecturerWorkloadBindingModel model);
    }
}