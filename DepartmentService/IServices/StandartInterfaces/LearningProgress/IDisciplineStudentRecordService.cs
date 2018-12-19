using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface IDisciplineStudentRecordService
    {
        /// <summary>
        /// Получение списка дисциплин
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model);

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
        /// Получение списка связок дисциплина-студент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineStudentRecordPageViewModel> GetDisciplineStudentRecords(DisciplineStudentRecordGetBindingModel model);

        /// <summary>
        /// Получения связки дисциплина-студент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineStudentRecordViewModel> GetDisciplineStudentRecord(DisciplineStudentRecordGetBindingModel model);

        /// <summary>
        /// Создание новой связки дисциплина-студент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateDisciplineStudentRecord(DisciplineStudentRecordSetBindingModel model);

        /// <summary>
        /// Изменение связки дисциплина-студент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateDisciplineStudentRecord(DisciplineStudentRecordSetBindingModel model);

        /// <summary>
        /// Удаление связки дисциплина-студент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteDisciplineStudentRecord(DisciplineStudentRecordGetBindingModel model);
    }
}
