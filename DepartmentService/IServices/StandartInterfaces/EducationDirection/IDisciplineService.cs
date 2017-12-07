using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
	public interface IDisciplineService
    {
        /// <summary>
        /// Получение списка учебных годов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model);

        /// <summary>
        /// Получение списка дат семестра
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<SeasonDatesPageViewModel> GetSeasonDaties(SeasonDatesGetBindingModel model);

        /// <summary>
        /// Получение списка блоков дисциплин
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineBlockPageViewModel> GetDisciplineBlocks(DisciplineBlockGetBindingModel model);

		/// <summary>
		/// Получение списка дисциплин
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model);

		/// <summary>
		/// Получения дисциплины
		/// </summary>
		/// <param name="model"></param>
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
