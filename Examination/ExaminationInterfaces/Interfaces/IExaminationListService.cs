using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Tools;

namespace ExaminationInterfaces.Interfaces
{
    public interface IExaminationListService
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
        /// Получение списка студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StudentPageViewModel> GetStudents(StudentGetBindingModel model);

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
		ResultService<ExaminationListPageViewModels> GetExaminationLists(ExaminationListGetBindingModel model);

        /// <summary>
		/// Получение направления
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<ExaminationListViewModel> GetExaminationList(ExaminationListGetBindingModel model);

        /// <summary>
        /// Создание новой направления
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateExaminationList(ExaminationListSetBindingModel model);

        /// <summary>
        /// Изменение направления
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateExaminationList(ExaminationListSetBindingModel model);

        /// <summary>
        /// Удаление направления
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteExaminationList(ExaminationListGetBindingModel model);
    }
}