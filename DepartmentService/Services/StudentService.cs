﻿using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentDAL.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;

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


		public ResultService<StudentGroupPageViewModel> GetStudentGroups(StudentGroupGetBindingModel model)
		{
			return _serviceSG.GetStudentGroups(model);
		}


		public ResultService<StudentPageViewModel> GetStudents(StudentGetBindingModel model)
		{
			try
			{
				int countPages = 0;
				var query = _context.Students.Include(s => s.StudentGroup).AsQueryable();
				if (model.StudentGroupId.HasValue)
				{
					query = query.Where(e => e.StudentGroupId == model.StudentGroupId.Value && !e.IsDeleted);
				}
				if (model.StudentStatus.HasValue)
				{
					query = query.Where(e => e.StudentState == model.StudentStatus.Value && !e.IsDeleted);
				}
				if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.OrderBy(c => c.StudentGroupId).ThenBy(s => s.LastName)
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}
				var result = new StudentPageViewModel
				{
					MaxCount = countPages,
					List = ModelFactoryToViewModel.CreateStudents(query)
					.ToList()
				};

				return ResultService<StudentPageViewModel>.Success(
					result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<StudentPageViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<StudentPageViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
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
					ModelFactoryToViewModel.CreateStudentViewModel(entity));
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


		public ResultService EnrollmentStudents(StudentEnrollmentBindingModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					if (model.StudentList.Count <= 0)
					{
						return ResultService.Error("Error:", "Students not found",
							ResultServiceStatusCode.NotFound);
					}
					for (int i = 0; i < model.StudentList.Count; ++i)
					{
						var entity = ModelFacotryFromBindingModel.CreateStudent(model.StudentList[i]);

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

		public ResultService TransferStudents(StudentTransferBindingModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var newGroup = _context.StudentGroups
						.FirstOrDefault(st => st.Id == model.NewStudentGroupId);
					if (newGroup == null)
					{
						return ResultService.Error("Error:", "NewStudentGroup not found",
							ResultServiceStatusCode.NotFound);
					}
					if (model.StudentList.Count <= 0)
					{
						return ResultService.Error("Error:", "Students not found",
							ResultServiceStatusCode.NotFound);
					}
					var oldGroup = _context.StudentGroups
						.FirstOrDefault(st => st.Id == model.OldStudentGroupId);
					if (oldGroup == null)
					{
						return ResultService.Error("Error:", "OldStudentGroup not found",
							ResultServiceStatusCode.NotFound);
					}
					for (int i = 0; i < model.StudentList.Count; ++i)
					{
						string numberofBook = model.StudentList[i].NumberOfBook;
						var entity = _context.Students
								.FirstOrDefault(e => e.NumberOfBook == numberofBook && !e.IsDeleted);
						if (entity == null)
						{
							return ResultService.Error("Error:", "Student not found",
								ResultServiceStatusCode.NotFound);
						}
						entity.StudentGroup = newGroup;
						_context.SaveChanges();
						if (oldGroup.StewardId == numberofBook)
						{
							oldGroup.Steward = null;
							newGroup.Steward = entity;
							_context.SaveChanges();
						}

						var entityHistory = new StudentHistory
						{
							StudentId = entity.NumberOfBook,
							DateCreate = DateTime.Now,
							TextMessage = string.Format("Студент переведен в группу {0} на основании: {1} {2}", entity.StudentGroup.GroupName,
								model.TransferReason, model.TransferDate.ToShortDateString())
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

		public ResultService DeductionStudents(StudentDeductionBindingModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					if (model.StudentList.Count <= 0)
					{
						return ResultService.Error("Error:", "Students not found",
							ResultServiceStatusCode.NotFound);
					}
					var oldGroup = _context.StudentGroups
						.FirstOrDefault(st => st.Id == model.StudentGroupId);
					if (oldGroup == null)
					{
						return ResultService.Error("Error:", "OldStudentGroup not found",
							ResultServiceStatusCode.NotFound);
					}
					for (int i = 0; i < model.StudentList.Count; ++i)
					{
						string numberofBook = model.StudentList[i].NumberOfBook;
						var entity = _context.Students
								.FirstOrDefault(e => e.NumberOfBook == numberofBook && !e.IsDeleted);
						if (entity == null)
						{
							return ResultService.Error("Error:", "Student not found",
								ResultServiceStatusCode.NotFound);
						}
						entity.StudentState = StudentState.Завершил;
						entity.StudentGroup = null;
						_context.SaveChanges();
						if (oldGroup.StewardId == numberofBook)
						{
							oldGroup.Steward = null;
							_context.SaveChanges();
						}

						var entityHistory = new StudentHistory
						{
							StudentId = entity.NumberOfBook,
							DateCreate = DateTime.Now,
							TextMessage = string.Format("Студент отчислен на основании: {0}. Приказ №{1} от {2}",
								model.DeductionReason, model.DeductionOrderNumber, model.DeductionDate.ToShortDateString())
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

		public ResultService ToAcademStudents(StudentToAcademBindingModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					if (model.StudentList.Count <= 0)
					{
						return ResultService.Error("Error:", "Students not found",
							ResultServiceStatusCode.NotFound);
					}
					var oldGroup = _context.StudentGroups
						.FirstOrDefault(st => st.Id == model.StudentGroupId);
					if (oldGroup == null)
					{
						return ResultService.Error("Error:", "OldStudentGroup not found",
							ResultServiceStatusCode.NotFound);
					}
					for (int i = 0; i < model.StudentList.Count; ++i)
					{
						string numberofBook = model.StudentList[i].NumberOfBook;
						var entity = _context.Students
								.FirstOrDefault(e => e.NumberOfBook == numberofBook && !e.IsDeleted);
						if (entity == null)
						{
							return ResultService.Error("Error:", "Student not found",
								ResultServiceStatusCode.NotFound);
						}
						entity.StudentState = StudentState.Академ;
						entity.StudentGroup = null;
						_context.SaveChanges();
						if (oldGroup.StewardId == numberofBook)
						{
							oldGroup.Steward = null;
							_context.SaveChanges();
						}

						var entityHistory = new StudentHistory
						{
							StudentId = entity.NumberOfBook,
							DateCreate = DateTime.Now,
							TextMessage = string.Format("Студент ушел в академ на основании: {0}. Приказ №{1} от {2}",
								model.ToAcademReason, model.ToAcademOrderNumber, model.ToAcademDate.ToShortDateString())
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
				entity = ModelFacotryFromBindingModel.CreateStudent(model, entity);

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
							Email = "отсутсвует",
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
	}
}
