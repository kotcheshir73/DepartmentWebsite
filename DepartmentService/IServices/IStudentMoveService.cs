using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
	public interface IStudentMoveService
	{
		/// <summary>
		/// Загрузка списка студентов из файла
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StudentPageViewModel> LoadStudentsFromFile(StudentLoadDocBindingModel model);

		/// <summary>
		/// Зачисление студентов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService EnrollmentStudents(StudentEnrollmentBindingModel model);

		/// <summary>
		/// Перевод студентов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService TransferStudents(StudentTransferBindingModel model);

		/// <summary>
		/// Отчисление студентов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeductionStudents(StudentDeductionBindingModel model);

		/// <summary>
		/// Уход студентов в академ
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService ToAcademStudents(StudentToAcademBindingModel model);
	}
}
