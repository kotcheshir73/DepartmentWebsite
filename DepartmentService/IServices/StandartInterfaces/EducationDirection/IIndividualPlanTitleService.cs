using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface IIndividualPlanTitleService
    {
        /// <summary>
		/// Получение списка элементов записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<IndividualPlanTitlePageViewModel> GetIndividualPlanTitles(IndividualPlanTitleGetBindingModel model);

        /// <summary>
		/// Получение элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<IndividualPlanTitleViewModel> GetIndividualPlanTitle(IndividualPlanTitleGetBindingModel model);
        
        /// <summary>
		/// Создание новой элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateIndividualPlanTitle(IndividualPlanTitleSetBindingModel model);

        /// <summary>
        /// Изменение элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateIndividualPlanTitle(IndividualPlanTitleSetBindingModel model);

        /// <summary>
        /// Удаление элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteIndividualPlanTitle(IndividualPlanTitleGetBindingModel model);
    }
}