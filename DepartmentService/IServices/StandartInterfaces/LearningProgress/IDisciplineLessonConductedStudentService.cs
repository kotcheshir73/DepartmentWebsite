using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface IDisciplineLessonConductedStudentService
    {
        /// <summary>
        /// Получение списка записей о занятии
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonConductedPageViewModel> GetDisciplineLessonConducteds(DisciplineLessonConductedGetBindingModel model);

        /// <summary>
        /// Получение списка студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StudentPageViewModel> GetStudents(StudentGetBindingModel model);

        /// <summary>
        /// Получение списка связок занятие-студент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonConductedStudentPageViewModel> GetDisciplineLessonConductedStudents(DisciplineLessonConductedStudentGetBindingModel model);

        /// <summary>
        /// Получения связки занятие-студент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonConductedStudentViewModel> GetDisciplineLessonConductedStudent(DisciplineLessonConductedStudentGetBindingModel model);

        /// <summary>
        /// Создание новой связки занятие-студент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateDisciplineLessonConductedStudent(DisciplineLessonConductedStudentSetBindingModel model);

        /// <summary>
        /// Изменение связки занятие-студент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateDisciplineLessonConductedStudent(DisciplineLessonConductedStudentSetBindingModel model);

        /// <summary>
        /// Удаление связки занятие-студент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteDisciplineLessonConductedStudent(DisciplineLessonConductedStudentGetBindingModel model);
    }
}
