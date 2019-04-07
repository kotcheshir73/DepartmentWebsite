using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.ViewModels;
using Tools;

namespace ScheduleServiceInterfaces.Interfaces
{
    public interface IScheduleLessonTimeService
    {
		/// <summary>
		/// Получение списка временных интервалов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<ScheduleLessonTimePageViewModel> GetScheduleLessonTimes(ScheduleLessonTimeGetBindingModel model);

		/// <summary>
		/// Получения временного интервала
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<ScheduleLessonTimeViewModel> GetScheduleLessonTime(ScheduleLessonTimeGetBindingModel model);

        /// <summary>
        /// Создание нового временного интервала
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateScheduleLessonTime(ScheduleLessonTimeSetBindingModel model);

        /// <summary>
        /// Изменение временного интервала
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateScheduleLessonTime(ScheduleLessonTimeSetBindingModel model);

        /// <summary>
        /// Удаление временного интервала
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteScheduleLessonTime(ScheduleLessonTimeGetBindingModel model);
    }
}