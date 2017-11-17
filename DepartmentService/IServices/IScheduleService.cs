using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
    public interface IScheduleService
    {
		/// <summary>
		/// Получение списка аудиторий
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<ClassroomPageViewModel> GetClassrooms(ClassroomGetBindingModel model);

		/// <summary>
		/// Получение списка групп
		/// </summary>
		/// <returns></returns>
		ResultService<List<StudentGroupViewModel>> GetStudentGroups();

		/// <summary>
		/// Получение списка преподавателей
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<List<LecturerViewModel>> GetLecturers(LecturerGetBindingModel model);

		/// <summary>
		/// Получение списка дат семестра
		/// </summary>
		/// <returns></returns>
		ResultService<List<SeasonDatesViewModel>> GetSeasonDaties();

		/// <summary>
		/// Получение списка временных интервалов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<List<ScheduleLessonTimeViewModel>> GetScheduleLessonTimes(ScheduleLessonTimeGetBindingModel model);

		/// <summary>
		/// Получить даты по текущему семестру
		/// </summary>
		/// <returns></returns>
		ResultService<SeasonDatesViewModel> GetCurrentDates();

        /// <summary>
        /// Измненение текущих настроек
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateCurrentDates(SeasonDatesGetBindingModel model);

		/// <summary>
		/// Получение расписания занятий в семестре
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<List<SemesterRecordShortViewModel>> GetScheduleSemester(ScheduleBindingModel model);

		/// <summary>
		/// Получение расписания консультаций
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<List<ConsultationRecordShortViewModel>> GetScheduleConsultation(ScheduleBindingModel model);

		/// <summary>
		/// Получение расписания зачетов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<List<OffsetRecordShortViewModel>> GetScheduleOffset(ScheduleBindingModel model);

		/// <summary>
		/// Получение расписания экзаменов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<List<ExaminationRecordShortViewModel>> GetScheduleExamination(ScheduleBindingModel model);

        /// <summary>
        /// Загрузка арсписания по аудиториям
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService LoadScheduleHTMLForClassrooms(LoadHTMLForClassroomsBindingModel model);

        /// <summary>
        /// Импорт зачетов из excel-файла
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService ImportExcel(ImportToOffsetFromExcel model);

        /// <summary>
        /// Импорт экзаменов из excel-файла
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService ImportExcel(ImportToExaminationFromExcel model);

        /// <summary>
        /// Проверка расписания на предмет записей без названия дисциплины или с неизвестным типом занятия
        /// </summary>
        /// <returns></returns>
        ResultService CheckSemesterRecordsIfNotComplite();

        /// <summary>
        /// Отчистка пар семестра по аудиториям
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService ClearSemesterRecords(ClassroomGetBindingModel model);

		/// <summary>
		/// Отчистка пар семестра по группам
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService ClearSemesterRecords(StudentGroupGetBindingModel model);

		/// <summary>
		/// Отчистка зачетов по аудиториям
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService ClearOffsetRecords(ClassroomGetBindingModel model);

		/// <summary>
		/// Отчистка зачетов по группам
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService ClearOffsetRecords(StudentGroupGetBindingModel model);

		/// <summary>
		/// Отчистка экзаменов по аудиториям
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService ClearExaminationRecords(ClassroomGetBindingModel model);

		/// <summary>
		/// Отчистка экзаменов по группам
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService ClearExaminationRecords(StudentGroupGetBindingModel model);

		/// <summary>
		/// Отчистка консультаций по аудиториям
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService ClearConsultationRecords(ClassroomGetBindingModel model);

		/// <summary>
		/// Отчистка консультаций по группам
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService ClearConsultationRecords(StudentGroupGetBindingModel model);

		/// <summary>
		/// Выгрузка данных в Excel
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService ExportSemesterRecordExcel(ExportToExcelClassroomsBindingModel model);

        /// <summary>
        /// Выгрузка данных в Excel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService ExportOffsetRecordExcel(ExportToExcelClassroomsBindingModel model);

        /// <summary>
        /// Выгрузка данных в Excel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService ExportExaminationRecordExcel(ExportToExcelClassroomsBindingModel model);

        /// <summary>
        /// Выгрузка данных в html
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService ExportSemesterRecordHTML(ExportToHTMLClassroomsBindingModel model);

        /// <summary>
        /// Выгрузка данных в html
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService ExportOffsetRecordHTML(ExportToHTMLClassroomsBindingModel model);

        /// <summary>
        /// Выгрузка данных в html
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService ExportExaminationRecordHTML(ExportToHTMLClassroomsBindingModel model);
    }
}
