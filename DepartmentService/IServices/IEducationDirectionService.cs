﻿using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
	public interface IEducationDirectionService
    {
		/// <summary>
		/// Получение списка направлений
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model);

		/// <summary>
		/// Получения направления
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<EducationDirectionViewModel> GetEducationDirection(EducationDirectionGetBindingModel model);

        /// <summary>
        /// Создание нового направления
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateEducationDirection(EducationDirectionRecordBindingModel model);

        /// <summary>
        /// Изменение направления
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateEducationDirection(EducationDirectionRecordBindingModel model);

        /// <summary>
        /// Удаление направления
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteEducationDirection(EducationDirectionGetBindingModel model);
    }
}
