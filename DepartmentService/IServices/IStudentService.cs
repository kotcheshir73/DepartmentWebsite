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
		ResultService<List<StudentViewModel>> GetStudents(StudentGetBindingModel model);

		/// <summary>
		/// Получение списка групп
		/// </summary>
		/// <returns></returns>
		ResultService<List<StudentGroupViewModel>> GetStudentGroups();

		/// <summary>
		/// Получения студента
		/// </summary>
		/// <param name="model">Идентификатор студента</param>
		/// <returns></returns>
		ResultService<StudentViewModel> GetStudent(StudentGetBindingModel model);

        /// <summary>
        /// Загрузка списка студентов из файла
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<List<StudentViewModel>> LoadStudentsFromFile(StudentLoadDocBindingModel model);

        /// <summary>
        /// Зачисление студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService EnrollmentStudents(StudentEnrollmentBindingModel model);

		/// <summary>
		/// Перевод студентов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService TransferToNextYearStudents(StudentTransferBindingModel model);

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
