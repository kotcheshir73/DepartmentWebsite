using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using Tools;

namespace ExaminationInterfaces.Interfaces
{
    public interface IStatementService
    {
        /// <summary>
        /// Получение списка преподавателей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model);

        /// <summary>
        /// Получение списка дисциплин
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model);

        /// <summary>
        /// Получение списка групп
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StudentGroupPageViewModel> GetStudentGroups(StudentGroupGetBindingModel model);

        /// <summary>
        /// Получение списка учебных годов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model);

        /// <summary>
		/// Получение списка ведомостей
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StatementPageViewModel> GetStatements(StatementGetBindingModel model);

        /// <summary>
		/// Получение ведомости
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StatementViewModel> GetStatement(StatementGetBindingModel model);

        /// <summary>
        /// Создание новой ведомости
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateStatement(StatementSetBindingModel model);

        /// <summary>
        /// Изменение ведомости
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStatement(StatementSetBindingModel model);

        /// <summary>
        /// Удаление ведомости
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStatement(StatementGetBindingModel model);
    }
}