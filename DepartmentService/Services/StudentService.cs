using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using DepartmentDAL.Context;
using DepartmentDAL.Models;
using System.Text;
using System.Data.Entity.Validation;
using Microsoft.Office.Interop.Word;
using System.IO;

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

		public List<StudentViewModel> GetStudents(StudentGetBindingModel model)
		{
			if (model.StudentGroupId.HasValue)
			{
				return ModelFactory.CreateStudents(
					_context.Students
						.Where(e => e.StudentGroupId == model.StudentGroupId.Value && !e.IsDeleted)
						)
				.OrderBy(s => s.LastName).ToList();
			}
			return ModelFactory.CreateStudents(
					_context.Students
						.Where(e => !e.IsDeleted))
				.ToList();
		}

		public List<StudentGroupViewModel> GetStudentGroups()
		{
			return _serviceSG.GetStudentGroups();
		}

		public StudentViewModel GetStudent(StudentGetBindingModel model)
		{
			var entity = _context.Students
							.FirstOrDefault(e => e.NumberOfBook == model.NumberOfBook && !e.IsDeleted);
			if (entity == null)
				return null;
			return ModelFactory.CreateStudentViewModel(entity);
		}

		public ResultService LoadStudentsFromFile(StudentLoadDocBindingModel model)
		{
			var word = new Application();
			if (File.Exists(model.FileName))
			{
				Document document = word.Documents.Open(model.FileName, Type.Missing,
						true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
						Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				var table = document.Tables[1];

				using (var transaction = _context.Database.BeginTransaction())
				{
					try
					{
						for (int i = 2; i < table.Rows.Count; ++i)
						{
							var entity = new Student
							{
								NumberOfBook = table.Cell(i, 2).Range.Text.Replace("\r\a", ""),
								StudentGroupId = model.Id,
								LastName = table.Cell(i, 3).Range.Text.Replace("\r\a", ""),
								FirstName = table.Cell(i, 4).Range.Text.Replace("\r\a", ""),
								Patronymic = table.Cell(i, 5).Range.Text.Replace("\r\a", ""),
								Description = string.Format("{0}  {1}", table.Cell(i, 6).Range.Text.Replace("\r\a", ""),
								table.Cell(i, 7).Range.Text.Replace("\r\a", "")),
								DateCreate = DateTime.Now,
								IsDeleted = false
							};
							if (string.IsNullOrEmpty(entity.NumberOfBook))
							{
								break;
							}
							if (!string.IsNullOrEmpty(entity.LastName) && entity.LastName.Length > 1)
							{
								entity.LastName = entity.LastName[0] + entity.LastName.Substring(1).ToLower();
							}
							if (!string.IsNullOrEmpty(entity.FirstName) && entity.FirstName.Length > 1)
							{
								entity.FirstName = entity.FirstName[0] + entity.FirstName.Substring(1).ToLower();
							}
							if (!string.IsNullOrEmpty(entity.Patronymic) && entity.Patronymic.Length > 1)
							{
								entity.Patronymic = entity.Patronymic[0] + entity.Patronymic.Substring(1).ToLower();
							}
							_context.Students.Add(entity);
							_context.SaveChanges();

							var entityHistory = new StudentHistory
							{
								StudentId = entity.NumberOfBook,
								DateCreate = DateTime.Now,
								TextMessage = string.Format("Студент зачислен в группу {0}", entity.StudentGroup.GroupName)
							};
							_context.StudentHistorys.Add(entityHistory);
							_context.SaveChanges();
						}
						transaction.Commit();
					}
					catch (Exception ex)
					{
						transaction.Rollback();
						document.Close();
						return ResultService.Error("error", ex.Message, 400);
					}
				}
				document.Close();
			}
			return ResultService.Success();
		}

		public ResultService CreateStudent(StudentRecordBindingModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var entity = new Student
					{
						NumberOfBook = model.NumberOfBook,
						StudentGroupId = model.StudentGroupId,
						LastName = model.LastName,
						FirstName = model.FirstName,
						Patronymic = model.Patronymic,
						Description = model.Description,
						Photo = model.Photo,
						DateCreate = DateTime.Now,
						IsDeleted = false
					};
					_context.Students.Add(entity);
					_context.SaveChanges();

					var entityHistory = new StudentHistory
					{
						StudentId = entity.NumberOfBook,
						DateCreate = DateTime.Now,
						TextMessage = string.Format("Студент зачислен в группу {0}", entity.StudentGroup.GroupName)
					};
					_context.StudentHistorys.Add(entityHistory);
					_context.SaveChanges();

					transaction.Commit();
					return ResultService.Success();
				}
				catch (DbEntityValidationException ex)
				{
					transaction.Rollback();
					StringBuilder sr = new StringBuilder();
					foreach (var eve in ex.EntityValidationErrors)
					{
						sr.AppendFormat("ValidationErrors:\r\n");
						foreach (var ve in eve.ValidationErrors)
						{
							sr.AppendFormat("- Свойство: \"{0}\", Ошибка: \"{1}\"\r\n",
								ve.PropertyName, ve.ErrorMessage);
						}
					}
					return ResultService.Error("error", sr.ToString(), 400);
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					return ResultService.Error("error", ex.Message, 400);
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
					return ResultService.Error("entity", "not_found", 404);
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
				StringBuilder sr = new StringBuilder();
				foreach (var eve in ex.EntityValidationErrors)
				{
					sr.AppendFormat("ValidationErrors:\r\n");
					foreach (var ve in eve.ValidationErrors)
					{
						sr.AppendFormat("- Свойство: \"{0}\", Ошибка: \"{1}\"\r\n",
							ve.PropertyName, ve.ErrorMessage);
					}
				}
				return ResultService.Error("error", sr.ToString(), 400);
			}
			catch (Exception ex)
			{
				return ResultService.Error("error", ex.Message, 400);
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
					return ResultService.Error("entity", "not_found", 404);
				}
				entity.IsDeleted = true;
				entity.DateDelete = DateTime.Now;

				_context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
				_context.SaveChanges();
				return ResultService.Success();
			}
			catch (DbEntityValidationException ex)
			{
				StringBuilder sr = new StringBuilder();
				foreach (var eve in ex.EntityValidationErrors)
				{
					sr.AppendFormat("ValidationErrors:\r\n");
					foreach (var ve in eve.ValidationErrors)
					{
						sr.AppendFormat("- Свойство: \"{0}\", Ошибка: \"{1}\"\r\n",
							ve.PropertyName, ve.ErrorMessage);
					}
				}
				return ResultService.Error("error", sr.ToString(), 400);
			}
			catch (Exception ex)
			{
				return ResultService.Error("error", ex.Message, 400);
			}
		}
	}
}
