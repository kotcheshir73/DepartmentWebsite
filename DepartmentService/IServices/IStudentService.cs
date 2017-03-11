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
        /// Получения студента
        /// </summary>
        /// <param name="model">Идентификатор студента</param>
        /// <returns></returns>
        StudentViewModel GetStudent(StudentGetBindingModel model);

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
