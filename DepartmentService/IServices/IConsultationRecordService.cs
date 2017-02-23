using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface IConsultationRecordService
    {
        /// <summary>
        /// Получения записи о консультации
        /// </summary>
        /// <param name="model">Идентификатор аудитории</param>
        /// <returns></returns>
        ConsultationRecordViewModel GetConsultationRecord(ConsultationRecordGetBindingModel model);

        /// <summary>
        /// Создание новой записи о консультации
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateConsultationRecord(ConsultationRecordRecordBindingModel model);

        /// <summary>
        /// Изменение записи о консультации
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateConsultationRecord(ConsultationRecordRecordBindingModel model);

        /// <summary>
        /// Удаление записи о консультации
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteConsultationRecord(ConsultationRecordGetBindingModel model);
    }
}
