using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.ViewModels;
using Tools;

namespace ScheduleInterfaces.Interfaces
{
    public interface IStreamingLessonService
    {
		/// <summary>
		/// Получение списка потоковых занятий
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StreamingLessonPageViewModel> GetStreamingLessons(StreamingLessonGetBindingModel model);

		/// <summary>
		/// Получения потока
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StreamingLessonViewModel> GetStreamingLesson(StreamingLessonGetBindingModel model);

        /// <summary>
        /// Создание нового потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateStreamingLesson(StreamingLessonSetBindingModel model);

        /// <summary>
        /// Изменение потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStreamingLesson(StreamingLessonSetBindingModel model);

        /// <summary>
        /// Удаление потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStreamingLesson(StreamingLessonGetBindingModel model);
    }
}