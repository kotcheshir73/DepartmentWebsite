using DepartmentModel;
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
        /// Получения записи об экзамене
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ExaminationRecordViewModel> GetExaminationRecord(ScheduleGetBindingModel model);

        /// <summary>
        /// Создание новой записи об экзамене
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateExaminationRecord(ExaminationRecordRecordBindingModel model);

        /// <summary>
        /// Изменение записи об экзамене
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateExaminationRecord(ExaminationRecordRecordBindingModel model);

        /// <summary>
        /// Удаление записи об экзамене
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteExaminationRecord(ScheduleGetBindingModel model);

        /// <summary>
        /// Отчистка пар в сессию
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService ClearExaminationRecords(ScheduleGetBindingModel model);
    }
}
