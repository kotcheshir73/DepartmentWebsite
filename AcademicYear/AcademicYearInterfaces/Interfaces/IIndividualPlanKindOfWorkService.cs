using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.ViewModels;
using Tools;

namespace AcademicYearInterfaces.Interfaces
{
    public interface IIndividualPlanKindOfWorkService
    {
        /// <summary>
		/// Получение списка элементов записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<IndividualPlanTitlePageViewModel> GetIndividualPlanTitles(IndividualPlanTitleGetBindingModel model);

        /// <summary>
		/// Получение списка элементов записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<IndividualPlanKindOfWorkPageViewModel> GetIndividualPlanKindOfWorks(IndividualPlanKindOfWorkGetBindingModel model);

        /// <summary>
		/// Получение элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<IndividualPlanKindOfWorkViewModel> GetIndividualPlanKindOfWork(IndividualPlanKindOfWorkGetBindingModel model);
        
        /// <summary>
		/// Создание новой элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateIndividualPlanKindOfWork(IndividualPlanKindOfWorkSetBindingModel model);

        /// <summary>
        /// Изменение элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateIndividualPlanKindOfWork(IndividualPlanKindOfWorkSetBindingModel model);

        /// <summary>
        /// Удаление элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteIndividualPlanKindOfWork(IndividualPlanKindOfWorkGetBindingModel model);
    }
}