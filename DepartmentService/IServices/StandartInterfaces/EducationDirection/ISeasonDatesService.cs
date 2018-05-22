﻿using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
	public interface ISeasonDatesService
    {
        /// <summary>
        /// Получение списка учебных годов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model);

        /// <summary>
        /// Получение списка дат семестра
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<SeasonDatesPageViewModel> GetSeasonDaties(SeasonDatesGetBindingModel model);

		/// <summary>
		/// Получить запись по датам семестра
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<SeasonDatesViewModel> GetSeasonDates(SeasonDatesGetBindingModel model);

        /// <summary>
        /// Создание новой записи по датам семестра
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateSeasonDates(SeasonDatesSetBindingModel model);

        /// <summary>
        /// Изменение записи по датам семестра
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateSeasonDates(SeasonDatesSetBindingModel model);

        /// <summary>
        /// Удаление записи по датам семестра
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteSeasonDates(SeasonDatesGetBindingModel model);
    }
}
