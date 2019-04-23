using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.ViewModels;
using Tools;

namespace LearningProgressInterfaces.Interfaces
{
    public interface IDisciplineLessonTaskStudentAcceptService
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
        /// Получение списка групп
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StudentGroupPageViewModel> GetStudentGroups(StudentGroupGetBindingModel model);

        /// <summary>
        /// Получение списка студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StudentPageViewModel> GetStudents(StudentGetBindingModel model);

        /// <summary>
        /// Получение списка заданий
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonTaskStudentAcceptPageViewModel> GetDisciplineLessonTaskStudentAccepts(DisciplineLessonTaskStudentAcceptGetBindingModel model);

        /// <summary>
        /// Получения задания
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonTaskStudentAcceptViewModel> GetDisciplineLessonTaskStudentAccept(DisciplineLessonTaskStudentAcceptGetBindingModel model);

        /// <summary>
        /// Создание нового задания
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateDisciplineLessonTaskStudentAccept(DisciplineLessonTaskStudentAcceptSetBindingModel model);

        /// <summary>
        /// Изменение задания
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateDisciplineLessonTaskStudentAccept(DisciplineLessonTaskStudentAcceptSetBindingModel model);

        /// <summary>
        /// Удаление задания
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteDisciplineLessonTaskStudentAccept(DisciplineLessonTaskStudentAcceptGetBindingModel model);
    }
}