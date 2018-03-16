using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface IAcademicPlanRecordElementService
    {
        /// <summary>
        /// Получение списка видов нагрузки
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<KindOfLoadPageViewModel> GetKindOfLoads(KindOfLoadGetBindingModel model);

        /// <summary>
        /// Получение списка записей учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<AcademicPlanRecordPageViewModel> GetAcademicPlanRecords(AcademicPlanRecordGetBindingModel model);

        /// <summary>
		/// Получение списка элементов записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<AcademicPlanRecordElementPageViewModel> GetAcademicPlanRecordElements(AcademicPlanRecordElementGetBindingModel model);

        /// <summary>
		/// Получение элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<AcademicPlanRecordElementViewModel> GetAcademicPlanRecordElement(AcademicPlanRecordElementGetBindingModel model);
        
        /// <summary>
		/// Создание новой элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateAcademicPlanRecordElement(AcademicPlanRecordElementRecordBindingModel model);

        /// <summary>
        /// Изменение элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateAcademicPlanRecordElement(AcademicPlanRecordElementRecordBindingModel model);

        /// <summary>
        /// Удаление элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteAcademicPlanRecordElement(AcademicPlanRecordElementGetBindingModel model);
    }
}