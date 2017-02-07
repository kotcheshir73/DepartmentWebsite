using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface ISemesterRecordService
    {
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
