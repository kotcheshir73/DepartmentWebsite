using DepartmentModel;
using DepartmentProcessAccountingService.BindingModels;
using DepartmentProcessAccountingService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentProcessAccountingService.IServices
{
    public interface IAcademicYearProcessService
    {
        /// <summary>
        /// Получение списка процессов в году
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<AcademicYearProcessPageViewModel> GetAcademicYearProcesses(AcademicYearProcessGetBindingModel model);
        
        /// <summary>
        /// Получения процесса в году
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<AcademicYearProcessViewModel> GetAcademicYearProcess(AcademicYearProcessGetBindingModel model);

        /// <summary>
        /// Создание нового процесса в году
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateAcademicYearProcess(AcademicYearProcessRecordBindingModel model);

        /// <summary>
        /// Изменение процесса в году
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateAcademicYearProcess(AcademicYearProcessRecordBindingModel model);

        /// <summary>
        /// Удаление процесса в году
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteAcademicYearProcess (AcademicYearProcessGetBindingModel model);
    }
}
