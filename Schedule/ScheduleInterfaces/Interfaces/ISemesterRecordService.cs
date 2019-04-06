﻿using DepartmentModel;
using ScheduleServiceInterfaces.BindingModels;
using ScheduleServiceInterfaces.ViewModels;
using System.Collections.Generic;

namespace ScheduleServiceInterfaces.Interfaces
{
    public interface ISemesterRecordService
	{
        /// <summary>
        /// Получение расписания занятий в семестре
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<List<SemesterRecordShortViewModel>> GetSemesterSchedule(ScheduleGetBindingModel model);

		/// <summary>
		/// Получения записи о паре
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<SemesterRecordViewModel> GetSemesterRecord(ScheduleGetBindingModel model);

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
        ResultService DeleteSemesterRecord(ScheduleGetBindingModel model);

        /// <summary>
        /// Отчистка пар в семестре
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService ClearSemesterRecords(ScheduleGetBindingModel model);
    }
}