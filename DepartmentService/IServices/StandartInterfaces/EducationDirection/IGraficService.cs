using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface IGraficService
    {
        /// <summary>
		/// Получение списка элементов записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<GraficPageViewModel> GetGrafics(GraficGetBindingModel model);

        /// <summary>
		/// Получение элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<GraficViewModel> GetGrafic(GraficGetBindingModel model);
        
        /// <summary>
		/// Создание новой элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateGrafic(GraficSetBindingModel model);

        /// <summary>
		/// Создание всех возможных ведомостей
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateAllFindGrafic(AcademicYearGetBindingModel model);

        /// <summary>
        /// Изменение элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateGrafic(GraficSetBindingModel model);

        /// <summary>
        /// Удаление элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteGrafic(GraficGetBindingModel model);
    }
}