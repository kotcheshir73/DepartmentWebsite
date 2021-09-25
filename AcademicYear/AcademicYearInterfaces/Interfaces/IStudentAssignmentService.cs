using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using Tools;

namespace AcademicYearInterfaces.Interfaces
{
    public interface IStudentAssignmentService
    {
        /// <summary>
        /// Получение списка учебных годов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model);

        /// <summary>
        /// Получение списка направлений подготовки
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model);

        /// <summary>
        /// Получение списка преподавателей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model);

        /// <summary>
		/// Получение списка элементов распределения по научным руководителям
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StudentAssignmentPageViewModel> GetStudentAssignments(StudentAssignmentGetBindingModel model);

        /// <summary>
		/// Получение элемента распределения по научным руководителям
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StudentAssignmentViewModel> GetStudentAssignment(StudentAssignmentGetBindingModel model);

        /// <summary>
        /// Создание новой элемента распределения по научным руководителям
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateStudentAssignment(StudentAssignmentSetBindingModel model);

        /// <summary>
        /// Изменение элемента распределения по научным руководителям
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStudentAssignment(StudentAssignmentSetBindingModel model);

        /// <summary>
        /// Удаление элемента распределения по научным руководителям
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStudentAssignment(StudentAssignmentGetBindingModel model);
    }
}
