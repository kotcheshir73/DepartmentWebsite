using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.BindingModels.StandartBindingModels.EducationDirection;
using DepartmentService.ViewModels;
using DepartmentService.ViewModels.StandartViewModels.EducationDirection;

namespace DepartmentService.IServices.StandartInterfaces.EducationDirection
{
    public interface IDisciplineLessonService
    {
        /// <summary>
        /// Получение списка дисциплин
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model);

        /// <summary>
        /// Получение списка занятий
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonPageViewModel> GetDisciplineLessons(DisciplineLessonGetBindingModel model);
        
        /// <summary>
        /// Получения занятия
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonViewModel> GetDisciplineLesson(DisciplineLessonGetBindingModel model);

        /// <summary>
        /// Создание нового занятия
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateDisciplineLesson(DisciplineLessonRecordBindingModel model);

        /// <summary>
        /// Изменение занятия
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateDisciplineLesson(DisciplineLessonRecordBindingModel model);

        /// <summary>
        /// Удаление занятия
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteDisciplineLesson(DisciplineLessonGetBindingModel model);
    }
}
