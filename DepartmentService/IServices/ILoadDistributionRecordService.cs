using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentService.IServices
{
	public interface ILoadDistributionRecordService
	{
		/// <summary>
		/// Получение списка записей учебного плана
		/// </summary>
		/// <param name="model">Идентификатор учебного плана</param>
		/// <returns></returns>
		ResultService<List<AcademicPlanRecordViewModel>> GetAcademicPlanRecords(AcademicPlanRecordGetBindingModel model);

		/// <summary>
		/// Получение списка контингентов
		/// </summary>
		/// <returns></returns>
		ResultService<List<ContingentViewModel>> GetContingents();

		/// <summary>
		/// Получение списка преподавателей
		/// </summary>
		/// <returns></returns>
		ResultService<List<LecturerViewModel>> GetLecturers();

		/// <summary>
		/// Получение списка норм времени
		/// </summary>
		/// <returns></returns>
		ResultService<List<TimeNormViewModel>> GetTimeNorms();

		/// <summary>
		/// Получение списка записей распределения нагрузок
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<List<LoadDistributionRecordViewModel>> GetLoadDistributionRecords(LoadDistributionRecordGetBindingModel model);

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

		/// <summary>
		/// Формирование/перерасчет учебной нагрузки на год
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService MakeLoadDistribution(LoadDistributionGetBindingModel model);
	}
}
