using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
	public interface IDisciplineBlockService
	{
		/// <summary>
		/// Получение списка блоков дисциплин
		/// </summary>
		/// <returns></returns>
		ResultService<List<DisciplineBlockViewModel>> GetDisciplineBlocks();

		/// <summary>
		/// Получения блока дисциплин
		/// </summary>
		/// <param name="model">Идентификатор блока дисциплин</param>
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
