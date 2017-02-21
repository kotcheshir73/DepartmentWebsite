using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
    public interface IOffsetRecordService
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
        /// Получения записи о зачете
        /// </summary>
        /// <param name="model">Идентификатор аудитории</param>
        /// <returns></returns>
        OffsetRecordViewModel GetOffsetRecord(OffsetRecordGetBindingModel model);

        /// <summary>
        /// Создание новой записи о зачете
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateOffsetRecord(OffsetRecordRecordBindingModel model);

        /// <summary>
        /// Изменение записи о зачете
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateOffsetRecord(OffsetRecordRecordBindingModel model);

        /// <summary>
        /// Удаление записи о зачете
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteOffsetRecord(OffsetRecordGetBindingModel model);
    }
}
