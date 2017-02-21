using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
    public interface IStudentGroupService
    {
        /// <summary>
        /// Получение списка групп
        /// </summary>
        /// <returns></returns>
        List<StudentGroupViewModel> GetStudentGroups();

        /// <summary>
        /// Получение списка направлений
        /// </summary>
        /// <returns></returns>
        List<EducationDirectionViewModel> GetEducationDirections();

        /// <summary>
        /// Получения группы
        /// </summary>
        /// <param name="model">Идентификатор группы</param>
        /// <returns></returns>
        StudentGroupViewModel GetStudentGroup(StudentGroupGetBindingModel model);

        /// <summary>
        /// Создание новой группы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateStudentGroup(StudentGroupRecordBindingModel model);

        /// <summary>
        /// Изменение группы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStudentGroup(StudentGroupRecordBindingModel model);

        /// <summary>
        /// Удаление группы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStudentGroup(StudentGroupGetBindingModel model);
    }
}
