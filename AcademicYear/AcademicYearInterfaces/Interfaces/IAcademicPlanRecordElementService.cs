using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.ViewModels;
using Tools;

namespace AcademicYearInterfaces.Interfaces
{
    public interface IAcademicPlanRecordElementService
    {
        /// <summary>
        /// Получение списка норм времени
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<TimeNormPageViewModel> GetTimeNorms(TimeNormGetBindingModel model);

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
		ResultService CreateAcademicPlanRecordElement(AcademicPlanRecordElementSetBindingModel model);

        /// <summary>
        /// Изменение элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateAcademicPlanRecordElement(AcademicPlanRecordElementSetBindingModel model);

        /// <summary>
        /// Удаление элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteAcademicPlanRecordElement(AcademicPlanRecordElementGetBindingModel model);
    }
}