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
        /// <returns></returns>
        List<ScheduleLessonTimeViewModel> GetScheduleLessonTimes();

        /// <summary>
        /// Получения временного интервала
        /// </summary>
        /// <param name="model">Идентификатор стоп-слова</param>
        /// <returns></returns>
        ScheduleLessonTimeViewModel GetScheduleLessonTime(ScheduleLessonTimeGetBindingModel model);

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
