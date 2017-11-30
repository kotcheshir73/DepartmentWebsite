using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
    public interface IExaminationRecordService
	{
        /// <summary>
        /// Получение расписания экзаменов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<List<ExaminationRecordShortViewModel>> GetExaminationSchedule(ScheduleGetBindingModel model);

		/// <summary>
		/// Получения записи о паре
		/// </summary>
		/// <param name="model">Идентификатор аудитории</param>
		/// <returns></returns>
		ResultService<ExaminationRecordViewModel> GetExaminationRecord(ScheduleGetBindingModel model);

        /// <summary>
        /// Создание новой записи о паре
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateExaminationRecord(ExaminationRecordRecordBindingModel model);

        /// <summary>
        /// Изменение записи о паре
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateExaminationRecord(ExaminationRecordRecordBindingModel model);

        /// <summary>
        /// Удаление записи о паре
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteExaminationRecord(ScheduleGetBindingModel model);
    }
}
