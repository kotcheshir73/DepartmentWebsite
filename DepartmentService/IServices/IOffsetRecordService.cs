using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface IOffsetRecordService
    {
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
