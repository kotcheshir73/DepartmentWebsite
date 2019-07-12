using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.ViewModels;
using Tools;

namespace AcademicYearInterfaces.Interfaces
{
    public interface IIndividualPlanRecordService
    {
        /// <summary>
        /// Получение списка индивидуальных планов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<IndividualPlanPageViewModel> GetIndividualPlans(IndividualPlanGetBindingModel model);

        /// <summary>
		/// Получение списка элементов записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<IndividualPlanKindOfWorkPageViewModel> GetIndividualPlanKindOfWorks(IndividualPlanKindOfWorkGetBindingModel model);

        /// <summary>
		/// Получение списка элементов записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<IndividualPlanRecordPageViewModel> GetIndividualPlanRecords(IndividualPlanRecordGetBindingModel model);

        /// <summary>
		/// Получение элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<IndividualPlanRecordViewModel> GetIndividualPlanRecord(IndividualPlanRecordGetBindingModel model);

        /// <summary>
        /// Создание новой элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateIndividualPlanRecord(IndividualPlanRecordSetBindingModel model);

        /// <summary>
        /// Изменение элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateIndividualPlanRecord(IndividualPlanRecordSetBindingModel model);

        /// <summary>
        /// Удаление элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteIndividualPlanRecord(IndividualPlanRecordGetBindingModel model);
    }
}