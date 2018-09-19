using DepartmentModel;
using DepartmentProcessAccountingService.ViewModels;
using DepartmentProcessAccountingService.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepartmentService.ViewModels;
using DepartmentService.BindingModels;

namespace DepartmentProcessAccountingService.IServices
{
    public interface IProcessDirectionRecordService
    {
        /// <summary>
        /// Получение списка процессов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DepartmentProcessPageViewModel> GetDepartmentProcesses(DepartmentProcessGetBindingModel model);

        /// <summary>
        /// Получение списка направлений
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model); 

        /// <summary>
        /// Получение списка привязок процессов к направлениям
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ProcessDirectionRecordPageViewModel> GetProcessDirectionRecords(ProcessDirectionRecordGetBindingModel model);

        /// <summary>
        /// Получения записи о привязке процесса к направлению
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ProcessDirectionRecordViewModel> GetProcessDirectionRecord(ProcessDirectionRecordGetBindingModel model);

        /// <summary>
        /// Создание новой записи о привязке процесса к направлению
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateProcessDirectionRecord(ProcessDirectionRecordRecordBindingModel model);

        /// <summary>
        /// Изменение записи о привязке процесса к направлению
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateProcessDirectionRecord(ProcessDirectionRecordRecordBindingModel model);

        /// <summary>
        /// Удаление записи о привязке процесса к направлению
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteProcessDirectionRecord(ProcessDirectionRecordGetBindingModel model);
    }
}
