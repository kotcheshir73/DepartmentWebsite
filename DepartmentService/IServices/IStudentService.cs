using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
    public interface IStudentService
    {
        /// <summary>
        /// Получение списка студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<StudentViewModel> GetStudents(StudentGetBindingModel model);

        /// <summary>
        /// Получение списка групп
        /// </summary>
        /// <returns></returns>
        List<StudentGroupViewModel> GetStudentGroups();

        /// <summary>
        /// Получения студента
        /// </summary>
        /// <param name="model">Идентификатор студента</param>
        /// <returns></returns>
        StudentViewModel GetStudent(StudentGetBindingModel model);

        /// <summary>
        /// Загрузка списка студентов из файла
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService LoadStudentsFromFile(StudentLoadDocBindingModel model);

        /// <summary>
        /// Создание новой студента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateStudent(StudentRecordBindingModel model);

        /// <summary>
        /// Изменение студента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStudent(StudentRecordBindingModel model);

        /// <summary>
        /// Удаление студента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStudent(StudentGetBindingModel model);
    }
}
