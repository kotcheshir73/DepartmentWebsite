using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.ViewModels;
using Tools;

namespace LearningProgressInterfaces.Interfaces
{
    public interface IDisciplineLessonConductedService
    {
        /// <summary>
        /// Получение списка занятий
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonPageViewModel> GetDisciplineLessons(DisciplineLessonGetBindingModel model);

        /// <summary>
        /// Получение списка групп
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StudentGroupPageViewModel> GetStudentGroups(StudentGroupGetBindingModel model);

        /// <summary>
        /// Получение списка записей о занятии
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonConductedPageViewModel> GetDisciplineLessonConducteds(DisciplineLessonConductedGetBindingModel model);

        /// <summary>
        /// Получения записи о занятии
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonConductedViewModel> GetDisciplineLessonConducted(DisciplineLessonConductedGetBindingModel model);

        /// <summary>
        /// Создание записи о занятии
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateDisciplineLessonConducted(DisciplineLessonConductedSetBindingModel model);

        /// <summary>
        /// Изменение записи о занятии
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateDisciplineLessonConducted(DisciplineLessonConductedSetBindingModel model);

        /// <summary>
        /// Удаление записи о занятии
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteDisciplineLessonConducted(DisciplineLessonConductedGetBindingModel model);
    }
}