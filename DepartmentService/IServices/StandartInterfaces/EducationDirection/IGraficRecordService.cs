using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface IGraficRecordService
    {
        /// <summary>
		/// Получение списка элементов записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<GraficRecordPageViewModel> GetGraficRecords(GraficRecordGetBindingModel model);

        /// <summary>
		/// Получение элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<GraficRecordViewModel> GetGraficRecord(GraficRecordGetBindingModel model);
        
        /// <summary>
		/// Создание новой элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateGraficRecord(GraficRecordSetBindingModel model);

        /// <summary>
		/// Создание всех возможных ведомостей
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateAllFindGraficRecord(AcademicYearGetBindingModel model);

        /// <summary>
        /// Изменение элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateGraficRecord(GraficRecordSetBindingModel model);

        /// <summary>
        /// Удаление элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteGraficRecord(GraficRecordGetBindingModel model);
    }
}