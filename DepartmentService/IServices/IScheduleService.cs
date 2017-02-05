using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Получить даты по текущему семестру
        /// </summary>
        /// <returns></returns>
        SeasonDatesViewModel GetCurrentDates();

        /// <summary>
        /// Получение занятий в семестре по аудитории
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<SemesterRecordViewModel> GetSemesterRecords(ClassroomGetBindingModel model);

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
    }
}
