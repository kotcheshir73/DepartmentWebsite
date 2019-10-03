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
    public class LecturerService : ILecturerService
	{
        private readonly ILecturerPostSerivce _serviceLP;

		private readonly AccessOperation _serviceOperation = AccessOperation.Преподаватели;

        private readonly string _entity = "Преподаватели";

        public LecturerService(ILecturerPostSerivce serviceLP)
		{
            _serviceLP = serviceLP;
        }

        public ResultService<LecturerPostPageViewModel> GetLecturerPosts(LecturerPostGetBindingModel model)
        {
            return _serviceLP.GetLecturerPosts(model);
        }


		public ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model)
		{
			try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Lecturers.Where(x => !x.IsDeleted).AsQueryable();

                    query = query.OrderBy(x => x.LecturerPost.Hours).ThenBy(x => x.Post).ThenBy(x => x.LastName);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.LecturerPost).Include(x => x.LecturerWorkloads);

                    var result = new LecturerPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ModelFactoryToViewModel.CreateLecturerViewModel).ToList()
                    };

                    return ResultService<LecturerPageViewModel>.Success(result);
                }
			}
			catch (Exception ex)
			{
				return ResultService<LecturerPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<LecturerViewModel> GetLecturer(LecturerGetBindingModel model)
		{
			try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Lecturers
                                .Include(x => x.LecturerPost)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<LecturerViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<LecturerViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<LecturerViewModel>.Success(ModelFactoryToViewModel.CreateLecturerViewModel(entity));
                }
			}
			catch (Exception ex)
			{
				return ResultService<LecturerViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateLecturer(LecturerSetBindingModel model)
		{
			try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ModelFacotryFromBindingModel.CreateLecturer(model);

                    var exsistEntity = context.Lecturers.FirstOrDefault(x => x.LastName == entity.LastName && x.FirstName == entity.FirstName);
                    if (exsistEntity == null)
                    {
                        context.Lecturers.Add(entity);
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

		public ResultService UpdateLecturer(LecturerSetBindingModel model)
		{
			try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Lecturers.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity = ModelFacotryFromBindingModel.CreateLecturer(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService DeleteLecturer(LecturerGetBindingModel model)
		{
			try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Lecturers.FirstOrDefault(x => x.Id == model.Id);
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