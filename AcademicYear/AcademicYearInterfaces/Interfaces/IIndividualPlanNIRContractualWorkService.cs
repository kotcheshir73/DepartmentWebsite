using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.ViewModels;
using Tools;

namespace AcademicYearInterfaces.Interfaces
{
    public interface IIndividualPlanNIRContractualWorkService
    {
        /// <summary>
        /// Получение списка индивидуальных планов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<IndividualPlanPageViewModel> GetIndividualPlans(IndividualPlanGetBindingModel model);

        /// <summary>
		/// Получение списка НИР(работа)
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<IndividualPlanNIRContractualWorkPageViewModel> GetIndividualPlanNIRContractualWorks(IndividualPlanNIRContractualWorkGetBindingModel model);

        /// <summary>
		/// Получение элемента НИР(работа)
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<IndividualPlanNIRContractualWorkViewModel> GetIndividualPlanNIRContractualWork(IndividualPlanNIRContractualWorkGetBindingModel model);

        /// <summary>
        /// Создание новой элемента НИР(работа)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateIndividualPlanNIRContractualWork(IndividualPlanNIRContractualWorkSetBindingModel model);

        /// <summary>
        /// Изменение элемента НИР(работа)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateIndividualPlanNIRContractualWork(IndividualPlanNIRContractualWorkSetBindingModel model);

        /// <summary>
        /// Удаление элемента НИР(работа)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteIndividualPlanNIRContractualWork(IndividualPlanNIRContractualWorkGetBindingModel model);
    }
}