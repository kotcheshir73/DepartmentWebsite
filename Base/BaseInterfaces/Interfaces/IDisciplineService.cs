using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using Tools;

namespace BaseInterfaces.Interfaces
{
    public interface IDisciplineService
    {
        /// <summary>
        /// Получение списка блоков дисциплин
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineBlockPageViewModel> GetDisciplineBlocks(DisciplineBlockGetBindingModel model);

		/// <summary>
		/// Получение списка дисциплин
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model);

		/// <summary>
		/// Получения дисциплины
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<DisciplineViewModel> GetDiscipline(DisciplineGetBindingModel model);

		/// <summary>
		/// Создание новой дисциплины
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateDiscipline(DisciplineSetBindingModel model);

		/// <summary>
		/// Изменение дисциплины
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateDiscipline(DisciplineSetBindingModel model);

		/// <summary>
		/// Удаление дисциплины
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteDiscipline(DisciplineGetBindingModel model);
	}
}