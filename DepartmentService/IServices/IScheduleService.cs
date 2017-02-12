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
        /// <returns></returns>
        List<ClassroomViewModel> GetClassrooms();

        /// <summary>
        /// Получение списка дат семестра
        /// </summary>
        /// <returns></returns>
        List<SeasonDatesViewModel> GetSeasonDaties();

        /// <summary>
        /// Получить даты по текущему семестру
        /// </summary>
        /// <returns></returns>
        SeasonDatesViewModel GetCurrentDates();

        /// <summary>
        /// Измненение текущих настроек
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateCurrentDates(SeasonDatesGetBindingModel model);

        /// <summary>
        /// Получение занятий в семестре по аудитории
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<SemesterRecordViewModel> GetSemesterRecords(ClassroomGetBindingModel model);

        /// <summary>
        /// Получение расписания занятий в семестре
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<SemesterRecordShortViewModel> GetScheduleSemester(ScheduleSemesterBindingModel model);

        /// <summary>
        /// Загрузка арсписания по аудиториям
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService LoadScheduleHTMLForClassrooms(LoadHTMLForClassroomsBindingModel model);

        /// <summary>
        /// Отчистка записей по аудиториям
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService ClearSemesterRecords(ClassroomGetBindingModel model);

        /// <summary>
        /// Выгрузка данных в Excel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService ExportExcel(ExportToExcelClassroomsBindingModel model);

        /// <summary>
        /// Выгрузка данных в html
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService ExportHTML(ExportToHTMLClassroomsBindingModel model);
    }
}
