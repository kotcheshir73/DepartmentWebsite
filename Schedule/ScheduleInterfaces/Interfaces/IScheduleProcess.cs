using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using Tools;

namespace ScheduleInterfaces.Interfaces
{
    public interface IScheduleProcess
    {
        /// <summary>
        /// Получение списка аудиторий
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ClassroomPageViewModel> GetClassrooms(ClassroomGetBindingModel model);

        /// <summary>
        /// Получение списка преподавателей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model);

        /// <summary>
        /// Получение списка преподавателей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model);

        /// <summary>
        /// Получение списка групп
        /// </summary>
        /// <returns></returns>
        ResultService<StudentGroupPageViewModel> GetStudentGroups(StudentGroupGetBindingModel model);

        ISemesterRecordService GetSemesterRecordService();

        IExaminationRecordService GetExaminationRecordService();

        IOffsetRecordService GetOffsetRecordService();

        IConsultationRecordService GetConsultationRecordService();

        /// <summary>
        /// Получение списка временных интервалов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<List<DateTime>> GetScheduleLessonTimes();

        /// <summary>
        /// Загрузка расписания в виде html-страницы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<List<ScheduleRecordViewModel>> LoadSchedule(LoadScheduleBindingModel model);

        /// <summary>
        /// Получение записей расписания семестра, зачетов и экзаменов по дисциплине
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //ResultService<ScheduleRecordsForDisciplinePageViewModel> GetScheduleRecordsForDiciplinePageViewModel(ScheduleRecordsForDiciplineBindingModel model);

        /// <summary>
        /// Загрузка расписания
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService Import(ImportToSemesterRecordsBindingModel model);

        /// <summary>
        /// Импорт зачетов из excel-файла
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService Import(ImportToOffsetFromExcel model);

        /// <summary>
        /// Импорт экзаменов из excel-файла
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService Import(ImportToExaminationFromExcel model);

        /// <summary>
        /// Отчистка пар семестра
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService ClearSemesterRecords(ScheduleGetBindingModel model);

		/// <summary>
		/// Отчистка зачетов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService ClearOffsetRecords(ScheduleGetBindingModel model);

		/// <summary>
		/// Отчистка экзаменов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService ClearExaminationRecords(ScheduleGetBindingModel model);

		/// <summary>
		/// Отчистка консультаций
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService ClearConsultationRecords(ScheduleGetBindingModel model);
	}
}