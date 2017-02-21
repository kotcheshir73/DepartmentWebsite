using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
    public interface ISemesterRecordService
    {
        /// <summary>
        /// Получение списка аудиторий
        /// </summary>
        /// <returns></returns>
        List<ClassroomViewModel> GetClassrooms();

        /// <summary>
        /// Получение списка групп
        /// </summary>
        /// <returns></returns>
        List<StudentGroupViewModel> GetStudentGroups();

        /// <summary>
        /// Получения записи о паре
        /// </summary>
        /// <param name="model">Идентификатор аудитории</param>
        /// <returns></returns>
        SemesterRecordViewModel GetSemesterRecord(SemesterRecordGetBindingModel model);

        /// <summary>
        /// Создание новой записи о паре
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateSemesterRecord(SemesterRecordRecordBindingModel model);

        /// <summary>
        /// Изменение записи о паре
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateSemesterRecord(SemesterRecordRecordBindingModel model);

        /// <summary>
        /// Удаление записи о паре
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteSemesterRecord(SemesterRecordGetBindingModel model);
    }
}
