using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.ViewModels;
using Tools;

namespace AcademicYearInterfaces.Interfaces
{
    public interface IStreamLessonRecordService
    {
        /// <summary>
        /// Получение списка потоков
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StreamLessonPageViewModel> GetStreamLessons(StreamLessonGetBindingModel model);

        /// <summary>
        /// Получение списка учебных планов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<AcademicPlanPageViewModel> GetAcademicPlans(AcademicPlanGetBindingModel model);

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
        /// Получение списка записей потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StreamLessonRecordPageViewModel> GetStreamLessonRecords(StreamLessonRecordGetBindingModel model);

        /// <summary>
        /// Получения записи потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StreamLessonRecordViewModel> GetStreamLessonRecord(StreamLessonRecordGetBindingModel model);

        /// <summary>
        /// Создание новой записи потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateStreamLessonRecord(StreamLessonRecordSetBindingModel model);

        /// <summary>
        /// Изменение записи потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStreamLessonRecord(StreamLessonRecordSetBindingModel model);

        /// <summary>
        /// Удаление записи потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStreamLessonRecord(StreamLessonRecordGetBindingModel model);
    }
}