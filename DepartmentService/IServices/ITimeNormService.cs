using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
	public interface ITimeNormService
	{
		/// <summary>
		/// Получение списка норм времени
		/// </summary>
		/// <returns></returns>
		ResultService<List<TimeNormViewModel>> GetTimeNorms();

		/// <summary>
		/// Получение списка видов нагрузок
		/// </summary>
		/// <returns></returns>
		ResultService<List<KindOfLoadViewModel>> GetKindOfLoads();

		/// <summary>
		/// Получения нормы времени
		/// </summary>
		/// <param name="model">Идентификатор нормы времени</param>
		/// <returns></returns>
		ResultService<TimeNormViewModel> GetTimeNorm(TimeNormGetBindingModel model);

		/// <summary>
		/// Создание новой нормы времени
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateTimeNorm(TimeNormRecordBindingModel model);

		/// <summary>
		/// Изменение нормы времени
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateTimeNorm(TimeNormRecordBindingModel model);

		/// <summary>
		/// Удаление нормы времени
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteTimeNorm(TimeNormGetBindingModel model);
	}
}
