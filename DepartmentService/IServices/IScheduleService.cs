using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

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

        /// <summary>
        /// Получение списка временных интервалов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<SeasonDatesPageViewModel> GetSeasonDaties(SeasonDatesGetBindingModel model);

        /// <summary>
        /// Получить даты по текущему семестру
        /// </summary>
        /// <returns></returns>
        ResultService<SeasonDatesViewModel> GetCurrentDates();

        /// <summary>
        /// Получение списка временных интервалов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ScheduleLessonTimePageViewModel> GetScheduleLessonTimes(ScheduleLessonTimeGetBindingModel model);
        
        /// <summary>
        /// Измненение текущих настроек
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateCurrentDates(SeasonDatesGetBindingModel model);

        /// <summary>
        /// Загрузка расписания
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService ImportHtml(ImportToSemesterFromHTMLBindingModel model);

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
