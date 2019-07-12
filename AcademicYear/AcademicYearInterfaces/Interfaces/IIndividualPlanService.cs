using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using Tools;

namespace AcademicYearInterfaces.Interfaces
{
    public interface IIndividualPlanService
    {
        /// <summary>
        /// Получение списка учебных годов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model);

        /// <summary>
        /// Получение списка преподавателей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model);

        /// <summary>
        /// Получение списка индивидуальных планов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<IndividualPlanPageViewModel> GetIndividualPlans(IndividualPlanGetBindingModel model);

        /// <summary>
        /// Получения индивидуального плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<IndividualPlanViewModel> GetIndividualPlan(IndividualPlanGetBindingModel model);

        /// <summary>
        /// Создание нового индивидуального плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateIndividualPlan(IndividualPlanSetBindingModel model);

        /// <summary>
        /// Изменение индивидуального плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateIndividualPlan(IndividualPlanSetBindingModel model);

        /// <summary>
        /// Удаление индивидуального плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteIndividualPlan(IndividualPlanGetBindingModel model);
    }
}