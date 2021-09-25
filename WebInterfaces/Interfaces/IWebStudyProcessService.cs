using AcademicYearInterfaces.BindingModels;
using System;
using System.Collections.Generic;
using System.IO;
using Tools;

namespace WebInterfaces.Interfaces
{
    public interface IStudyProcessService
    {
        /// <summary>
        /// Получение списка с базовой информацией для расчета штатов по учебному году
        /// </summary>
        /// <param name="model">Модель учебного года</param>
        /// <returns></returns>
        ResultService<List<List<object>>> GetAcademicYearLoading(AcademicYearGetBindingModel model);

        /// <summary>
        /// Получение списка с часами выбранного преподавателя по дисциплинам в определнном порядке
        /// </summary>
        /// <param name="LecturerId">Преподаватель</param>
        /// <param name="AcademicYearId">Академический год</param>
        /// <returns></returns>
        ResultService<List<object>> GetLecturerMissions(Guid LecturerId, Guid AcademicYearId);

        /// <summary>
        /// Получение потока архива с нагрузками преподавателей для выгрузки
        /// </summary>
        /// <param name="model">Модель информации для выгрузки (путь, id)</param>
        /// <returns></returns>
        ResultService<MemoryStream> ImportLecturerWorkloads(ImportLecturerWorkloadBindingModel model);

        /// <summary>
        /// Получение потока архива с расчасовками преподавателей для выгрузки
        /// </summary>
        /// <param name="model">Модель информации для выгрузки (путь, id)</param>
        /// <returns></returns>
        ResultService<MemoryStream> ImportDisciplineTimeDistributions(ImportDisciplineTimeDistributionsBindingModel model);

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
