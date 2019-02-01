using DepartmentModel;
using DepartmentProcessAccountingService.ViewModels;
using DepartmentProcessAccountingService.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentProcessAccountingService.IServices
{
    public interface IDepartmentProcessService
    {
        /// <summary>
        /// Получение списка процессов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DepartmentProcessPageViewModel> GetDepartmentProcesses(DepartmentProcessGetBindingModel model);

        /// <summary>
        /// Получения процесса
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DepartmentProcessViewModel> GetDepartmentProcess(DepartmentProcessGetBindingModel model);

        /// <summary>
        /// Создание нового процесса
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateDepartmentProcess(DepartmentProcessRecordBindingModel model);

        /// <summary>
        /// Изменение процесса
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateDepartmentProcess(DepartmentProcessRecordBindingModel model);

        /// <summary>
        /// Удаление процесса
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteDepartmentProcess(DepartmentProcessGetBindingModel model);

        /// <summary>
        /// Получение процесса по дате
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DepartmentProcessViewModel> GetDepartmentProcessByDate(DateTime date);
    }
}
