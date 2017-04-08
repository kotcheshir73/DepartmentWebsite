using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
	public interface ILecturerService
	{
		/// <summary>
		/// Получение списка преподавателей
		/// </summary>
		/// <returns></returns>
		ResultService<List<LecturerViewModel>> GetLecturers();

		/// <summary>
		/// Получения преподавателя
		/// </summary>
		/// <param name="model">Идентификатор преподавателя</param>
		/// <returns></returns>
		ResultService<LecturerViewModel> GetLecturer(LecturerGetBindingModel model);

		/// <summary>
		/// Создание новогопреподавателя
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateLecturer(LecturerRecordBindingModel model);

		/// <summary>
		/// Изменение преподавателя
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateLecturer(LecturerRecordBindingModel model);

		/// <summary>
		/// Удаление преподавателя
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService DeleteLecturer(LecturerGetBindingModel model);
	}
}
