using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
	public interface ILoadDistributionRecordService
	{
		/// <summary>
		/// Получение списка записей учебного плана
		/// </summary>
		/// <param name="model">Идентификатор учебного плана</param>
		/// <returns></returns>
		ResultService<AcademicPlanRecordPageViewModel> GetAcademicPlanRecords(AcademicPlanRecordGetBindingModel model);

		/// <summary>
		/// Получение списка контингентов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<ContingentPageViewModel> GetContingents(ContingentGetBindingModel model);

		/// <summary>
		/// Получение списка преподавателей
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model);

		/// <summary>
		/// Получение списка норм времени
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<TimeNormPageViewModel> GetTimeNorms(TimeNormGetBindingModel model);

		/// <summary>
		/// Получение списка записей распределения нагрузок
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<LoadDistributionRecordPageViewModel> GetLoadDistributionRecords(LoadDistributionRecordGetBindingModel model);

		/// <summary>
		/// Получения записи распределения нагрузок
		/// </summary>
		/// <param name="model">Идентификатор записи распределения нагрузок</param>
		/// <returns></returns>
		ResultService<LoadDistributionRecordViewModel> GetLoadDistributionRecord(LoadDistributionRecordGetBindingModel model);

		/// <summary>
		/// Создание новой записи распределения нагрузок
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateLoadDistributionRecord(LoadDistributionRecordRecordBindingModel model);

		/// <summary>
		/// Изменение записи распределения нагрузок
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateLoadDistributionRecord(LoadDistributionRecordRecordBindingModel model);

		/// <summary>
		/// Удаление записи распределения нагрузок
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteLoadDistributionRecord(LoadDistributionRecordGetBindingModel model);
	}
}
