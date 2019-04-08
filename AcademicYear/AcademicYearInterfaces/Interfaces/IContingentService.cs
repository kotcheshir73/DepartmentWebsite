using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using Tools;

namespace AcademicYearInterfaces.Interfaces
{
    public interface IContingentService
	{
		/// <summary>
		/// Получение списка учебных годов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model);

		/// <summary>
		/// Получение списка направлений
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model);

		/// <summary>
		/// Получение списка контингентов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<ContingentPageViewModel> GetContingents(ContingentGetBindingModel model);

		/// <summary>
		/// Получения контингента
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<ContingentViewModel> GetContingent(ContingentGetBindingModel model);

		/// <summary>
		/// Создание нового контингента
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateContingent(ContingentSetBindingModel model);

		/// <summary>
		/// Изменение контингента
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateContingent(ContingentSetBindingModel model);

		/// <summary>
		/// Удаление контингента
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteContingent(ContingentGetBindingModel model);
	}
}