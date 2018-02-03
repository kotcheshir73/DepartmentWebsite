using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
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
		ResultService CreateDisciplineBlock(DisciplineBlockRecordBindingModel model);

		/// <summary>
		/// Изменение блока дисциплин
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateDisciplineBlock(DisciplineBlockRecordBindingModel model);

		/// <summary>
		/// Удаление блока дисциплин
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteDisciplineBlock(DisciplineBlockGetBindingModel model);
	}
}
