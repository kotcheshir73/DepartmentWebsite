using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface IExaminationRecordService
    {
		/// <summary>
		/// Получения записи о паре
		/// </summary>
		/// <param name="model">Идентификатор аудитории</param>
		/// <returns></returns>
		ResultService<ExaminationRecordViewModel> GetExaminationRecord(ExaminationRecordGetBindingModel model);

        /// <summary>
        /// Создание новой записи о паре
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateExaminationRecord(ExaminationRecordRecordBindingModel model);

        /// <summary>
        /// Изменение записи о паре
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateExaminationRecord(ExaminationRecordRecordBindingModel model);

        /// <summary>
        /// Удаление записи о паре
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteExaminationRecord(ExaminationRecordGetBindingModel model);
    }
}
