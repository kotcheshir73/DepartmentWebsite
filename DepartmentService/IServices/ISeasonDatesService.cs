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
    public interface ISeasonDatesService
    {
        /// <summary>
        /// Получение списка дат семестра
        /// </summary>
        /// <returns></returns>
        List<SeasonDatesViewModel> GetSeasonDaties();

        /// <summary>
        /// Получить запись по датам семестра
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        SeasonDatesViewModel GetSeasonDates(SeasonDatesGetBindingModel model);

        /// <summary>
        /// Создание новой записи по датам семестра
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateSeasonDates(SeasonDatesRecordBindingModel model);

        /// <summary>
        /// Изменение записи по датам семестра
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateSeasonDates(SeasonDatesRecordBindingModel model);

        /// <summary>
        /// Удаление записи по датам семестра
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteSeasonDates(SeasonDatesGetBindingModel model);
    }
}
