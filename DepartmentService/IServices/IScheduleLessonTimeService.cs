using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
    public interface IScheduleLessonTimeService
    {
		/// <summary>
		/// Получение списка временных интервалов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<List<ScheduleLessonTimeViewModel>> GetScheduleLessonTimes(ScheduleLessonTimeGetBindingModel model);

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
        ResultService CreateScheduleLessonTime(ScheduleLessonTimeRecordBindingModel model);

        /// <summary>
        /// Изменение временного интервала
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateScheduleLessonTime(ScheduleLessonTimeRecordBindingModel model);

        /// <summary>
        /// Удаление временного интервала
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteScheduleLessonTime(ScheduleLessonTimeGetBindingModel model);
    }
}
