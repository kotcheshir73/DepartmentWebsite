using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using DatabaseContext;
using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;

namespace BaseImplementations.Implementations
{
    public class StudentGroupService : IStudentGroupService
	{
		private readonly AccessOperation _serviceOperation = AccessOperation.Группы;

        private readonly string _entity = "Группы";

        private readonly IEducationDirectionService _serviceED;

        private readonly ILecturerService _serviceL;

        public StudentGroupService(IEducationDirectionService serviceED, ILecturerService serviceL)
		{
			_serviceED = serviceED;
            _serviceL = serviceL;
        }

		public ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model)
		{
			return _serviceED.GetEducationDirections(model);
        }

        public ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model)
        {
            return _serviceL.GetLecturers(model);
        }


        public ResultService<StudentGroupPageViewModel> GetStudentGroups(StudentGroupGetBindingModel model)
		{
            try
            {
                if (!DepartmentUserManager.CheckAccess(model, _serviceOperation, AccessType.View, _entity))
                {
                    return ResultService<StudentGroupPageViewModel>.Error(new MethodAccessException(DepartmentUserManager.ErrorMessage), ResultServiceStatusCode.Error);
                }

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.StudentGroups.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.EducationDirectionId.HasValue)
                    {
                        query = query.Where(x => x.EducationDirectionId == model.EducationDirectionId);
                    }
                    if (!string.IsNullOrEmpty(model.Course))
                    {
                        AcademicCourse course = (AcademicCourse)Enum.Parse(typeof(AcademicCourse), model.Course);
                        query = query.Where(x => x.Course == course);
                    }

                    query = query.OrderBy(x => x.Course).ThenBy(x => x.EducationDirectionId).ThenBy(x => x.GroupName);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.EducationDirection).Include(x => x.Curator).Include(x => x.Students);

                    var result = new StudentGroupPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ModelFactoryToViewModel.CreateStudentGroupViewModel).ToList()
                    };

                    return ResultService<StudentGroupPageViewModel>.Success(result);
                }
			}
			catch (Exception ex)
			{
				return ResultService<StudentGroupPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<StudentGroupViewModel> GetStudentGroup(StudentGroupGetBindingModel model)
		{
			try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StudentGroups
                                .Include(x => x.EducationDirection)
                                .Include(x => x.Curator)
                                .Include(x => x.Students)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<StudentGroupViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<StudentGroupViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<StudentGroupViewModel>.Success(ModelFactoryToViewModel.CreateStudentGroupViewModel(entity));
                }
			}
			catch (Exception ex)
			{
				return ResultService<StudentGroupViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateStudentGroup(StudentGroupSetBindingModel model)
		{
			try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ModelFacotryFromBindingModel.CreateStudentGroup(model);

                    var exsistEntity = context.StudentGroups.FirstOrDefault(x => x.GroupName == entity.GroupName);
                    if (exsistEntity == null)
                    {
                        context.StudentGroups.Add(entity);
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

		public ResultService UpdateStudentGroup(StudentGroupSetBindingModel model)
		{
			try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StudentGroups.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity = ModelFacotryFromBindingModel.CreateStudentGroup(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService DeleteStudentGroup(StudentGroupGetBindingModel model)
		{
			try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StudentGroups.FirstOrDefault(x => x.Id == model.Id);
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