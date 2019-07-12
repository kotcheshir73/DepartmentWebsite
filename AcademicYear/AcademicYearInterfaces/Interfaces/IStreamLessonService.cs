using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.ViewModels;
using Tools;

namespace AcademicYearInterfaces.Interfaces
{
    public interface IStreamLessonService
    {
        /// <summary>
        /// Получение списка учебных годов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model);

        /// <summary>
        /// Получение списка потоков
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StreamLessonPageViewModel> GetStreamLessons(StreamLessonGetBindingModel model);

        /// <summary>
        /// Получения потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StreamLessonViewModel> GetStreamLesson(StreamLessonGetBindingModel model);

        /// <summary>
        /// Создание потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateStreamLesson(StreamLessonSetBindingModel model);

        /// <summary>
        /// Изменение потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStreamLesson(StreamLessonSetBindingModel model);

        /// <summary>
        /// Удаление потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStreamLesson(StreamLessonGetBindingModel model);
    }
}