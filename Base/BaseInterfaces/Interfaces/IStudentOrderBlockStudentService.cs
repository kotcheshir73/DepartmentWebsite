using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using Tools;

namespace BaseInterfaces.Interfaces
{
    public interface IStudentOrderBlockStudentService
    {
        /// <summary>
        /// Получение списка блоков приказа по студентам
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StudentOrderBlockPageViewModel> GetStudentOrderBlocks(StudentOrderBlockGetBindingModel model);

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
        /// Получение списка студентов блока приказа по студентам
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StudentOrderBlockStudentPageViewModel> GetStudentOrderBlockStudents(StudentOrderBlockStudentGetBindingModel model);

        /// <summary>
        /// Получения студента блока приказа по студентам
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StudentOrderBlockStudentViewModel> GetStudentOrderBlockStudent(StudentOrderBlockStudentGetBindingModel model);

        /// <summary>
        /// Создание студента блока приказа по студентам
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateStudentOrderBlockStudent(StudentOrderBlockStudentSetBindingModel model);

        /// <summary>
        /// Изменение студента блока приказа по студентам
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStudentOrderBlockStudent(StudentOrderBlockStudentSetBindingModel model);

        /// <summary>
        /// Удаление студента блока приказа по студентам
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStudentOrderBlockStudent(StudentOrderBlockStudentGetBindingModel model);
    }
}