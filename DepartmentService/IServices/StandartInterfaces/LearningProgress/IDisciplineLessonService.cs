using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface IDisciplineLessonService
    {
        /// <summary>
        /// Получение списка учебных годов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model);

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
