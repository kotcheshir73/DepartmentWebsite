using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
    public interface IEducationDirectionService
    {
		/// <summary>
		/// Получение списка направлений
		/// </summary>
		/// <returns></returns>
		ResultService<List<EducationDirectionViewModel>> GetEducationDirections();

		/// <summary>
		/// Получения направления
		/// </summary>
		/// <param name="model">Идентификатор направления</param>
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
