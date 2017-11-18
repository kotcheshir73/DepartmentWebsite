using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
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
		/// <returns></returns>
		ResultService<List<DisciplineViewModel>> GetDisciplines();

		/// <summary>
		/// Получения дисциплины
		/// </summary>
		/// <param name="model">Идентификатор дисциплины</param>
		/// <returns></returns>
		ResultService<DisciplineViewModel> GetDiscipline(DisciplineGetBindingModel model);

		/// <summary>
		/// Создание новой дисциплины
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateDiscipline(DisciplineRecordBindingModel model);

		/// <summary>
		/// Изменение дисциплины
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateDiscipline(DisciplineRecordBindingModel model);

		/// <summary>
		/// Удаление дисциплины
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteDiscipline(DisciplineGetBindingModel model);
	}
}
