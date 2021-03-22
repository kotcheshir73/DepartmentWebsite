using System;
using System.Collections.Generic;
using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.ViewModels;

namespace WebInterfaces.Interfaces
{
    public interface IWebStudyProcessService
    {
        /// <summary>
        /// Получение списка учебных годов
        /// </summary>
        /// <returns></returns>
        ResultService<WebAcademicYearPageViewModel> GetAcademicYears(WebAcademicYearGetBindingModel model);

        /// <summary>
        /// Получение учебного года
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebAcademicYearViewModel> GetAcademicYear(WebAcademicYearGetBindingModel model);

        /// <summary>
        /// Создание учебного года
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateAcademicYear(WebAcademicYearSetBindingModel model);

        /// <summary>
        /// Изменение учебного года
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateAcademicYear(WebAcademicYearSetBindingModel model);

        /// <summary>
        /// Удаление учебного года
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteAcademicYear(WebAcademicYearGetBindingModel model);

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------

        /// <summary>
        /// Получение списка учебных планов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebAcademicPlanPageViewModel> GetAcademicPlans(WebAcademicPlanGetBindingModel model);

        /// <summary>
        /// Получения учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebAcademicPlanViewModel> GetAcademicPlan(WebAcademicPlanGetBindingModel model);

        /// <summary>
        /// Создание нового учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateAcademicPlan(WebAcademicPlanSetBindingModel model);

        /// <summary>
        /// Изменение учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateAcademicPlan(WebAcademicPlanSetBindingModel model);

        /// <summary>
        /// Удаление учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteAcademicPlan(WebAcademicPlanGetBindingModel model);

        //-------------------------------------------------------------------------

        /// <summary>
		/// Получение списка записей учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<WebAcademicPlanRecordPageViewModel> GetAcademicPlanRecords(WebAcademicPlanRecordGetBindingModel model);

        /// <summary>
        /// Получения записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebAcademicPlanRecordViewModel> GetAcademicPlanRecord(WebAcademicPlanRecordGetBindingModel model);

        /// <summary>
        /// Создание новой записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateAcademicPlanRecord(WebAcademicPlanRecordSetBindingModel model);

        /// <summary>
        /// Изменение записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateAcademicPlanRecord(WebAcademicPlanRecordSetBindingModel model);

        /// <summary>
        /// Удаление записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteAcademicPlanRecord(WebAcademicPlanRecordGetBindingModel model);

        //-------------------------------------------------------------------------

        /// <summary>
        /// Получение списка элементов записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebAcademicPlanRecordElementPageViewModel> GetAcademicPlanRecordElements(WebAcademicPlanRecordElementGetBindingModel model);

        /// <summary>
		/// Получение элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<WebAcademicPlanRecordElementViewModel> GetAcademicPlanRecordElement(WebAcademicPlanRecordElementGetBindingModel model);

        /// <summary>
        /// Создание новой элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateAcademicPlanRecordElement(WebAcademicPlanRecordElementSetBindingModel model);

        /// <summary>
        /// Изменение элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateAcademicPlanRecordElement(WebAcademicPlanRecordElementSetBindingModel model);

        /// <summary>
        /// Удаление элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteAcademicPlanRecordElement(WebAcademicPlanRecordElementGetBindingModel model);

        //-------------------------------------------------------------------------

        /// <summary>
        /// Получение списка элементов записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebAcademicPlanRecordMissionPageViewModel> GetAcademicPlanRecordMissions(WebAcademicPlanRecordMissionGetBindingModel model);

        /// <summary>
		/// Получение элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<WebAcademicPlanRecordMissionViewModel> GetAcademicPlanRecordMission(WebAcademicPlanRecordMissionGetBindingModel model);

        /// <summary>
        /// Создание новой элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateAcademicPlanRecordMission(WebAcademicPlanRecordMissionSetBindingModel model);

        /// <summary>
        /// Изменение элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateAcademicPlanRecordMission(WebAcademicPlanRecordMissionSetBindingModel model);

        /// <summary>
        /// Удаление элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteAcademicPlanRecordMission(WebAcademicPlanRecordMissionGetBindingModel model);

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------

        /// <summary>
        /// Получение списка потоков
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebStreamLessonPageViewModel> GetStreamLessons(WebStreamLessonGetBindingModel model);

        /// <summary>
        /// Получения потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebStreamLessonViewModel> GetStreamLesson(WebStreamLessonGetBindingModel model);

        /// <summary>
        /// Создание потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateStreamLesson(WebStreamLessonSetBindingModel model);

        /// <summary>
        /// Изменение потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStreamLesson(WebStreamLessonSetBindingModel model);

        /// <summary>
        /// Удаление потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStreamLesson(WebStreamLessonGetBindingModel model);

        //-------------------------------------------------------------------------

        /// <summary>
        /// Получение списка записей потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebStreamLessonRecordPageViewModel> GetStreamLessonRecords(WebStreamLessonRecordGetBindingModel model);

        /// <summary>
        /// Получения записи потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebStreamLessonRecordViewModel> GetStreamLessonRecord(WebStreamLessonRecordGetBindingModel model);

        /// <summary>
        /// Создание новой записи потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateStreamLessonRecord(WebStreamLessonRecordSetBindingModel model);

        /// <summary>
        /// Изменение записи потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStreamLessonRecord(WebStreamLessonRecordSetBindingModel model);

        /// <summary>
        /// Удаление записи потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStreamLessonRecord(WebStreamLessonRecordGetBindingModel model);

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------

        /// <summary>
        /// Получение норм времени
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebTimeNormPageViewModel> GetTimeNorms(WebTimeNormGetBindingModel model);

        /// <summary>
        /// Получение нормы времени
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebTimeNormViewModel> GetTimeNorm(WebTimeNormGetBindingModel model);

        /// <summary>
        /// Создание нормы времени
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateTimeNorm(WebTimeNormSetBindingModel model);

        /// <summary>
        /// Изменение нормы времени
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateTimeNorm(WebTimeNormSetBindingModel model);

        /// <summary>
        /// Удаление нормы времени
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteTimeNorm(WebTimeNormGetBindingModel model);

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------

        /// <summary>
        /// Получение списка контенгента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebContingentPageViewModel> GetContingents(WebContingentGetBindingModel model);

        /// <summary>
        /// Получение контингента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebContingentViewModel> GetContingent(WebContingentGetBindingModel model);

        /// <summary>
        /// Создание контингента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateContingent(WebContingentSetBindingModel model);

        /// <summary>
        /// Изменение контингента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateContingent(WebContingentSetBindingModel model);

        /// <summary>
        /// Удаление контингента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteContingent(WebContingentGetBindingModel model);

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------

        /// <summary>
        /// Получение нагрузки преподавателей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebLecturerWorkloadPageViewModel> GetLecturerWorkloads(WebLecturerWorkloadGetBindingModel model);

        /// <summary>
        /// Получение нагрузки преподавателя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebLecturerWorkloadViewModel> GetLecturerWorkload(WebLecturerWorkloadGetBindingModel model);

        /// <summary>
        /// Создание нагрузки преподавателя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateLecturerWorkload(WebLecturerWorkloadSetBindingModel model);

        /// <summary>
        /// Изменение нагрузки преподавателя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateLecturerWorkload(WebLecturerWorkloadSetBindingModel model);

        /// <summary>
        /// Удаление нагрузки преподавателя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteLecturerWorkload(WebLecturerWorkloadGetBindingModel model);

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------

        /// <summary>
        /// Получение отображаемых имен свойств типа и их названий
        /// </summary>
        /// <param name="type">Тип</param>
        /// <returns></returns>
        (List<string> displayNames, List<string> propertiesNames) GetPropertiesNames(Type type);

        /// <summary>
        /// Получение значений переданных свойств каждого элемента списка
        /// </summary>
        /// <param name="list">Список объектов</param>
        /// <param name="propertiesNames">Список с названиями свойств</param>
        /// <returns></returns>
        List<List<object>> GetPropertiesValues<T>(List<T> list, List<string> propertiesNames);
    }
}
