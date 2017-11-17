using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
	public interface IUserService
	{
		/// <summary>
		/// Получение списка ролей
		/// </summary>
		/// <returns></returns>
		ResultService<List<RoleViewModel>> GetRoles();

		/// <summary>
		/// Получение списка студентов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<StudentPageViewModel> GetStudents(StudentGetBindingModel model);

		/// <summary>
		/// Получение списка преподавателей
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<List<LecturerViewModel>> GetLecturers(LecturerGetBindingModel model);

		/// <summary>
		/// Получение списка пользователей
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<List<UserViewModel>> GetUsers(UserGetBindingModel model);

		/// <summary>
		/// Получения пользователя
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<UserViewModel> GetUser(UserGetBindingModel model);

		/// <summary>
		/// Создание нового пользователя
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateUser(UserRecordBindingModel model);

		/// <summary>
		/// Изменение пользователя
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateUser(UserRecordBindingModel model);

		/// <summary>
		/// Удаление пользователя
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteUser(UserGetBindingModel model);
	}
}
