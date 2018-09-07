using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface IDisciplineLessonTaskService
    {
        /// <summary>
        /// Получение списка занятий
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonPageViewModel> GetDisciplineLessons(DisciplineLessonGetBindingModel model);

        /// <summary>
        /// Получение списка заданий
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonTaskPageViewModel> GetDisciplineLessonTasks(DisciplineLessonTaskGetBindingModel model);

        /// <summary>
        /// Получения задания
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonTaskViewModel> GetDisciplineLessonTask(DisciplineLessonTaskGetBindingModel model);

        /// <summary>
        /// Создание нового задания
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateDisciplineLessonTask(DisciplineLessonTaskRecordBindingModel model);

        /// <summary>
        /// Изменение задания
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateDisciplineLessonTask(DisciplineLessonTaskRecordBindingModel model);

        /// <summary>
        /// Удаление задания
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteDisciplineLessonTask(DisciplineLessonTaskGetBindingModel model);
    }
}
