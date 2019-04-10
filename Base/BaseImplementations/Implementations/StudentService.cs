using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;

namespace BaseImplementations.Implementations
{
    public class StudentService : IStudentService
	{
		private readonly AccessOperation _serviceOperation = AccessOperation.Студенты;

        private readonly string _entity = "Студенты";

        private readonly IStudentGroupService _serviceSG;

		public StudentService(IStudentGroupService serviceSG)
		{
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Students.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.StudentGroupId.HasValue)
                    {
                        query = query.Where(x => x.StudentGroupId == model.StudentGroupId.Value);
                    }
                    if (model.StudentStatus.HasValue)
                    {
                        query = query.Where(x => x.StudentState == model.StudentStatus.Value);
                    }

                    query = query.OrderBy(x => x.StudentGroupId).ThenBy(x => x.LastName);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.StudentGroup);

                    var result = new StudentPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ModelFactoryToViewModel.CreateStudentViewModel).ToList()
                    };

                    return ResultService<StudentPageViewModel>.Success(result);
                }
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = (!string.IsNullOrEmpty(model.NumberOfBook)) ?
                                        context.Students.FirstOrDefault(x => x.NumberOfBook == model.NumberOfBook)
                                        :
                                        context.Students.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<StudentViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<StudentViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<StudentViewModel>.Success(ModelFactoryToViewModel.CreateStudentViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<StudentViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
		}
		
		public ResultService CreateStudent(StudentSetBindingModel model)
		{
			try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ModelFacotryFromBindingModel.CreateStudent(model);

                    var exsistEntity = context.Students.FirstOrDefault(x => x.NumberOfBook == entity.NumberOfBook);
                    if (exsistEntity == null)
                    {
                        context.Students.Add(entity);
                        context.SaveChanges();
                        return ResultService.Success(entity.Id);
                    }
                    else
                    {
                        if (exsistEntity.IsDeleted)
                        {
                            exsistEntity.IsDeleted = false;
                            context.SaveChanges();
                            return ResultService.Success(exsistEntity.Id);
                        }
                        else
                        {
                            return ResultService.Error("Error:", "Элемент уже существует", ResultServiceStatusCode.ExsistItem);
                        }
                    }
                }
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}
				
		public ResultService UpdateStudent(StudentSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Students.FirstOrDefault(x => x.NumberOfBook == model.NumberOfBook);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity = ModelFacotryFromBindingModel.CreateStudent(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Students.FirstOrDefault(x => x.NumberOfBook == model.NumberOfBook);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity.IsDeleted = true;
                    entity.DateDelete = DateTime.Now;

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
		}
	}
}