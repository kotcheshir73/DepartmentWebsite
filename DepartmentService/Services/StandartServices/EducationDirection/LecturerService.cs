using DepartmentContext;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
    public class LecturerService : ILecturerService
	{
		private readonly DepartmentDbContext _context;

        private readonly ILecturerPostSerivce _serviceLP;

		private readonly AccessOperation _serviceOperation = AccessOperation.Преподаватели;

		public LecturerService(DepartmentDbContext context, ILecturerPostSerivce serviceLP)
		{
			_context = context;
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
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по преподавателям");
				}

				int countPages = 0;
				var query = _context.Lecturers.Where(l => !l.IsDeleted).AsQueryable();

                query = query.OrderBy(l => l.LecturerPost.Hours).ThenBy(l => l.Post).ThenBy(l => l.LastName);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

                query = query.Include(l => l.LecturerPost).Include(l => l.LecturerWorkloads);

				var result = new LecturerPageViewModel
				{
					MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateLecturerViewModel).ToList()
                };

				return ResultService<LecturerPageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<LecturerPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
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
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по преподавателям");
				}

				var entity = _context.Lecturers
                                .Include(l => l.LecturerPost)
                                .FirstOrDefault(l => l.Id == model.Id && !l.IsDeleted);
				if (entity == null)
				{
					return ResultService<LecturerViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<LecturerViewModel>.Success(ModelFactoryToViewModel.CreateLecturerViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<LecturerViewModel>.Error(ex, ResultServiceStatusCode.Error);
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
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по преподавателям");
				}

				var entity = ModelFacotryFromBindingModel.CreateLecturer(model);

				_context.Lecturers.Add(entity);
				_context.SaveChanges();

				return ResultService.Success(entity.Id);
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

		public ResultService UpdateLecturer(LecturerSetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по преподавателям");
				}

				var entity = _context.Lecturers.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateLecturer(model, entity);

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

		public ResultService DeleteLecturer(LecturerGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
				{
					throw new Exception("Нет доступа на удаление данных по преподавателям");
				}

				var entity = _context.Lecturers.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
    }
}
