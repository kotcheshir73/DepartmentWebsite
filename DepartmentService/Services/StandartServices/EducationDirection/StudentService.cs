using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
	public class StudentService : IStudentService
	{
		private readonly DepartmentDbContext _context;

		private readonly AccessOperation _serviceOperation = AccessOperation.Студенты;

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
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по студентам");
				}

				int countPages = 0;
				var query = _context.Students.Where(s => !s.IsDeleted).AsQueryable();

				if (model.StudentGroupId.HasValue)
				{
					query = query.Where(s => s.StudentGroupId == model.StudentGroupId.Value);
				}
				if (model.StudentStatus.HasValue)
				{
					query = query.Where(s => s.StudentState == model.StudentStatus.Value);
                }

                query = query.OrderBy(s => s.StudentGroupId).ThenBy(s => s.LastName);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				query = query.Include(s => s.StudentGroup);

				var result = new StudentPageViewModel
				{
					MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateStudentViewModel).ToList()
                };

				return ResultService<StudentPageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<StudentPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<StudentPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<StudentViewModel> GetStudent(StudentGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по студентам");
				}

				var entity = (string.IsNullOrEmpty(model.NumberOfBook)) ?
                                        _context.Students.FirstOrDefault(s => s.NumberOfBook == model.NumberOfBook && !s.IsDeleted)
                                        :
                                        _context.Students.FirstOrDefault(s => s.Id == model.Id && !s.IsDeleted);
				if (entity == null)
				{
					return ResultService<StudentViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<StudentViewModel>.Success(ModelFactoryToViewModel.CreateStudentViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<StudentViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<StudentViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}
		
		public ResultService CreateStudent(StudentRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по студентам");
				}

				var entity = ModelFacotryFromBindingModel.CreateStudent(model);

				_context.Students.Add(entity);
				_context.SaveChanges();

                SetSteward(model);


                return ResultService.Success(entity.NumberOfBook);
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
				
		public ResultService UpdateStudent(StudentRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по студентам");
				}

				var entity = _context.Students.FirstOrDefault(e => e.NumberOfBook == model.NumberOfBook && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateStudent(model, entity);

                SetSteward(model);

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
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
				{
					throw new Exception("Нет доступа на удаление данных по студентам");
				}

				var entity = _context.Students.FirstOrDefault(e => e.NumberOfBook == model.NumberOfBook && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
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

        /// <summary>
        /// Доп функция установки старосты группы
        /// </summary>
        /// <param name="model"></param>
        private void SetSteward(StudentRecordBindingModel model)
        {
            if(model.IsSteward)
            {
                var group = _context.StudentGroups.FirstOrDefault(sg => sg.Id == model.StudentGroupId && !sg.IsDeleted);
                if (group != null && !group.StewardName.Contains(model.LastName))
                {
                    group.StewardName = string.Format("{0} {1} {2}", model.LastName, model.FirstName, model.Patronymic);

                    _context.SaveChanges();
                }
            }
        }
	}
}
