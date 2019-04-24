using AcademicYearInterfaces.BindingModels;
using BaseInterfaces.BindingModels;
using System;
using System.Collections.Generic;
using System.Text;
using Tools;

namespace ExaminationInterfaces.Interfaces
{
    interface IExaminationProcess
    {
        /// <summary>
		/// Создание всех возможных ведомостей
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateAllFindStatement(AcademicYearGetBindingModel model);

        /// <summary>
        /// Для вывода сводной ведомости
        /// </summary>
        /// <returns></returns>
        ResultService<List<object[]>> GetSummaryStatement(StudentGroupGetBindingModel model);

        /// <summary>
        /// Создание новой элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateAllFindStatementRecord(AcademicYearGetBindingModel model);
    }
}