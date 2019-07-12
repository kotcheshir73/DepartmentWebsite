using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.ViewModels;
using Tools;

namespace AcademicYearInterfaces.Interfaces
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
		ResultService CreateAcademicYear(AcademicYearSetBindingModel model);

		/// <summary>
		/// Изменение учебного года
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateAcademicYear(AcademicYearSetBindingModel model);

		/// <summary>
		/// Удаление учебного года
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteAcademicYear(AcademicYearGetBindingModel model);
	}
}