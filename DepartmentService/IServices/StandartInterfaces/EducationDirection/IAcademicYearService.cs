using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
	public interface IAcademicYearService
	{
		/// <summary>
		/// Получение списка учебных годов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model);

		/// <summary>
		/// Получения учебного года
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<AcademicYearViewModel> GetAcademicYear(AcademicYearGetBindingModel model);

		/// <summary>
		/// Создание нового учебного года
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateAcademicYear(AcademicYearRecordBindingModel model);

		/// <summary>
		/// Изменение учебного года
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateAcademicYear(AcademicYearRecordBindingModel model);

		/// <summary>
		/// Удаление учебного года
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteAcademicYear(AcademicYearGetBindingModel model);
	}
}
