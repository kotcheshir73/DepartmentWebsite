using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using Tools;

namespace BaseInterfaces.Interfaces
{
    public interface IDisciplineBlockService
	{
		/// <summary>
		/// Получение списка блоков дисциплин
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<DisciplineBlockPageViewModel> GetDisciplineBlocks(DisciplineBlockGetBindingModel model);

		/// <summary>
		/// Получения блока дисциплин
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<DisciplineBlockViewModel> GetDisciplineBlock(DisciplineBlockGetBindingModel model);

		/// <summary>
		/// Создание нового блока дисциплин
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateDisciplineBlock(DisciplineBlockSetBindingModel model);

		/// <summary>
		/// Изменение блока дисциплин
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateDisciplineBlock(DisciplineBlockSetBindingModel model);

		/// <summary>
		/// Удаление блока дисциплин
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteDisciplineBlock(DisciplineBlockGetBindingModel model);
	}
}