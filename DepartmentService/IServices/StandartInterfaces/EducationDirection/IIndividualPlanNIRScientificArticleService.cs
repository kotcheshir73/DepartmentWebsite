using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface IIndividualPlanNIRScientificArticleService
    {
        /// <summary>
		/// Получение списка НИР(материалы)
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<IndividualPlanNIRScientificArticlePageViewModel> GetIndividualPlanNIRScientificArticles(IndividualPlanNIRScientificArticleGetBindingModel model);

        /// <summary>
		/// Получение элемента НИР(материалы)
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<IndividualPlanNIRScientificArticleViewModel> GetIndividualPlanNIRScientificArticle(IndividualPlanNIRScientificArticleGetBindingModel model);

        /// <summary>
        /// Создание новой элемента НИР(материалы)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateIndividualPlanNIRScientificArticle(IndividualPlanNIRScientificArticleSetBindingModel model);

        /// <summary>
        /// Изменение элемента НИР(материалы)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateIndividualPlanNIRScientificArticle(IndividualPlanNIRScientificArticleSetBindingModel model);

        /// <summary>
        /// Удаление элемента НИР(материалы)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteIndividualPlanNIRScientificArticle(IndividualPlanNIRScientificArticleGetBindingModel model);
    }
}