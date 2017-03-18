using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using DepartmentDAL.Context;
using DepartmentDAL.Models;
using System.Data.Entity.Validation;
using Microsoft.Office.Interop.Word;
using System.IO;
using DepartmentDAL.Enums;

namespace DepartmentService.Services
{
	public class StudentService : IStudentService
	{
		private readonly DepartmentDbContext _context;

		private readonly IStudentGroupService _serviceSG;

		public StudentService(DepartmentDbContext context, IStudentGroupService serviceSG)
		{
			_context = context;
			_serviceSG = serviceSG;
		}

		public ResultService<List<StudentViewModel>> GetStudents(StudentGetBindingModel model)
		{
			try
			{
				if (model.StudentGroupId.HasValue)
				{
					return ResultService<List<StudentViewModel>>.Success(
						ModelFactory.CreateStudents(_context.Students
							.Where(e => e.StudentGroupId == model.StudentGroupId.Value && !e.IsDeleted)
							)
					.OrderBy(s => s.LastName).ToList());
				}
				return ResultService<List<StudentViewModel>>.Success(
					ModelFactory.CreateStudents(_context.Students
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<StudentViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<StudentViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<List<StudentGroupViewModel>> GetStudentGroups()
		{
			return _serviceSG.GetStudentGroups();
		}

		public ResultService<StudentViewModel> GetStudent(StudentGetBindingModel model)
		{
			try
			{
				var entity = _context.Students
								.FirstOrDefault(e => e.NumberOfBook == model.NumberOfBook && !e.IsDeleted);
				if (entity == null)
					return ResultService<StudentViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<StudentViewModel>.Success(
					ModelFactory.CreateStudentViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<StudentViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<StudentViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<List<StudentViewModel>> LoadStudentsFromFile(StudentLoadDocBindingModel model)
		{
			var word = new Application();
			if (File.Exists(model.FileName))
			{
				Document document = word.Documents.Open(model.FileName, Type.Missing,
						true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
						Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				var table = document.Tables[1];

				try
				{
					var list = new List<StudentViewModel>();
					for (int i = 2; i < table.Rows.Count; ++i)
					{
						var studentModel = new StudentViewModel
						{
							NumberOfBook = table.Cell(i, 2).Range.Text.Replace("\r\a", ""),
							StudentGroupId = model.Id,
							LastName = table.Cell(i, 3).Range.Text.Replace("\r\a", ""),
							FirstName = table.Cell(i, 4).Range.Text.Replace("\r\a", ""),
							Patronymic = table.Cell(i, 5).Range.Text.Replace("\r\a", ""),
							Description = string.Format("{0}  {1}", table.Cell(i, 6).Range.Text.Replace("\r\a", ""),
							table.Cell(i, 7).Range.Text.Replace("\r\a", ""))
						};
						if (string.IsNullOrEmpty(studentModel.NumberOfBook))
						{
							break;
						}
						if (!string.IsNullOrEmpty(studentModel.LastName) && studentModel.LastName.Length > 1)
						{
							studentModel.LastName = studentModel.LastName[0] + studentModel.LastName.Substring(1).ToLower();
						}
						if (!string.IsNullOrEmpty(studentModel.FirstName) && studentModel.FirstName.Length > 1)
						{
							studentModel.FirstName = studentModel.FirstName[0] + studentModel.FirstName.Substring(1).ToLower();
						}
						if (!string.IsNullOrEmpty(studentModel.Patronymic) && studentModel.Patronymic.Length > 1)
						{
							studentModel.Patronymic = studentModel.Patronymic[0] + studentModel.Patronymic.Substring(1).ToLower();
						}
						list.Add(studentModel);
					}
					document.Close();
					return ResultService<List<StudentViewModel>>.Success(list);
				}
				catch (Exception ex)
				{
					document.Close();
					return ResultService<List<StudentViewModel>>.Error(ex, ResultServiceStatusCode.Error);
				}
			}
			return ResultService<List<StudentViewModel>>.Error("Error:", "File not found", ResultServiceStatusCode.FileNotFound);
		}

		public ResultService EnrollmentStudent(StudentEnrollmentBindingModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					for (int i = 0; i < model.StudentList.Count; ++i)
					{
						var entity = new Student
						{
							NumberOfBook = model.StudentList[i].NumberOfBook,
							StudentGroupId = model.StudentList[i].StudentGroupId,
							LastName = model.StudentList[i].LastName,
							FirstName = model.StudentList[i].FirstName,
							Patronymic = model.StudentList[i].Patronymic,
							Description = model.StudentList[i].Description,
							DateCreate = DateTime.Now,
							IsDeleted = false
						};
						_context.Students.Add(entity);
						_context.SaveChanges();

						var entityHistory = new StudentHistory
						{
							StudentId = entity.NumberOfBook,
							DateCreate = DateTime.Now,
							TextMessage = string.Format("Студент зачислен в группу {0} по приказу №{1} от {2}", entity.StudentGroup.GroupName,
							model.OrderNumber, model.OrderDate.ToShortDateString())
						};
						_context.StudentHistorys.Add(entityHistory);
						_context.SaveChanges();
					}
					transaction.Commit();
					return ResultService.Success();
				}
				catch (DbEntityValidationException ex)
				{
					return ResultService.Error(ex, ResultServiceStatusCode.Error);
				}
				catch (Exception ex)
				{
					return ResultService.Error(ex, ResultServiceStatusCode.Error);
				}
			}
		}

		public ResultService UpdateStudent(StudentRecordBindingModel model)
		{
			try
			{
				var entity = _context.Students
								.FirstOrDefault(e => e.NumberOfBook == model.NumberOfBook && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity.LastName = model.LastName;
				entity.FirstName = model.FirstName;
				entity.Patronymic = model.Patronymic;
				entity.Description = model.Description;
				entity.Photo = model.Photo;

				_context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
				_context.SaveChanges();
				return ResultService.Success();
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService DeleteStudent(StudentGetBindingModel model)
		{
			try
			{
				var entity = _context.Students
								.FirstOrDefault(e => e.NumberOfBook == model.NumberOfBook && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity.IsDeleted = true;
				entity.DateDelete = DateTime.Now;

				_context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
				_context.SaveChanges();
				return ResultService.Success();
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}
	}
}
