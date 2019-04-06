using DepartmentModel;
using ScheduleServiceInterfaces.BindingModels;
using ScheduleServiceInterfaces.ViewModels;
using System.Collections.Generic;

namespace ScheduleServiceInterfaces.Interfaces
{
    public interface IOffsetRecordService
	{
        /// <summary>
        /// Получение расписания зачетов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<List<OffsetRecordShortViewModel>> GetOffsetSchedule(ScheduleGetBindingModel model);

		/// <summary>
		/// Получения записи о зачете
		/// </summary>
		/// <param name="model">Идентификатор аудитории</param>
		/// <returns></returns>
		ResultService<OffsetRecordViewModel> GetOffsetRecord(ScheduleGetBindingModel model);

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
        ResultService DeleteOffsetRecord(ScheduleGetBindingModel model);

        /// <summary>
        /// Отчистка пар на зачетной неделе
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService ClearOffsetRecords(ScheduleGetBindingModel model);
    }
}