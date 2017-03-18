using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
    public interface IStreamingLessonService
    {
		/// <summary>
		/// Получение списка потоковых занятий
		/// </summary>
		/// <returns></returns>
		ResultService<List<StreamingLessonViewModel>> GetStreamingLessons();

		/// <summary>
		/// Получения потока
		/// </summary>
		/// <param name="model">Идентификатор аудитории</param>
		/// <returns></returns>
		ResultService<StreamingLessonViewModel> GetStreamingLesson(StreamingLessonGetBindingModel model);

        /// <summary>
        /// Создание нового потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateStreamingLesson(StreamingLessonRecordBindingModel model);

        /// <summary>
        /// Изменение потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStreamingLesson(StreamingLessonRecordBindingModel model);

        /// <summary>
        /// Удаление потока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStreamingLesson(StreamingLessonGetBindingModel model);
    }
}
