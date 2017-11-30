using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
	public interface IConsultationRecordService
	{
		/// <summary>
		/// Получение расписания консультаций
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<List<ConsultationRecordShortViewModel>> GetConsultationSchedule(ScheduleGetBindingModel model);

		/// <summary>
		/// Получения записи о консультации
		/// </summary>
		/// <param name="model">Идентификатор аудитории</param>
		/// <returns></returns>
		ResultService<ConsultationRecordViewModel> GetConsultationRecord(ScheduleGetBindingModel model);

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
        ResultService DeleteConsultationRecord(ScheduleGetBindingModel model);
    }
}
