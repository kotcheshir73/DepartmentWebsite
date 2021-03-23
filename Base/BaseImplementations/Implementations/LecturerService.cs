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
		private readonly ILecturerDepartmentPostSerivce _serviceLDP;

		private readonly ILecturerStudyPostSerivce _serviceLSP;

		private readonly AccessOperation _serviceOperation = AccessOperation.Преподаватели;

		private readonly string _entity = "Преподаватели";

		public LecturerService(ILecturerDepartmentPostSerivce serviceLDP, ILecturerStudyPostSerivce serviceLSP)
		{
			_serviceLDP = serviceLDP;
			_serviceLSP = serviceLSP;
		}

		public ResultService<LecturerDepartmentPostPageViewModel> GetLecturerDepartmentPosts(LecturerDepartmentPostGetBindingModel model)
		{
			return _serviceLDP.GetLecturerDepartmentPosts(model);
		}

		public ResultService<LecturerStudyPostPageViewModel> GetLecturerStudyPosts(LecturerStudyPostGetBindingModel model)
		{
			return _serviceLSP.GetLecturerStudyPosts(model);
		}


		public ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model)
		{
			try
			{
				if (!DepartmentUserManager.CheckAccess(model, _serviceOperation, AccessType.View, _entity))
				{
					return ResultService<LecturerPageViewModel>.Error(new MethodAccessException(DepartmentUserManager.ErrorMessage), ResultServiceStatusCode.Error);
				}

				int countPages = 0;
				using (var context = DepartmentUserManager.GetContext)
				{
					var query = context.Lecturers.Where(x => !x.IsDeleted).AsQueryable();

					query = query.OrderByDescending(x => x.LecturerDepartmentPost.Order).ThenBy(x => x.LecturerStudyPost.Hours).ThenBy(x => x.LastName);

					if (model.PageNumber.HasValue && model.PageSize.HasValue)
					{
						countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
						query = query
									.Skip(model.PageSize.Value * model.PageNumber.Value)
									.Take(model.PageSize.Value);
					}

					query = query.Include(x => x.LecturerDepartmentPost).Include(x => x.LecturerStudyPost).Include(x => x.LecturerWorkloads);

					var result = new LecturerPageViewModel
					{
						MaxCount = countPages,
						List = query.Select(ModelFactoryToViewModel.CreateLecturerViewModel).ToList()
					};

					// если данные без проверки авторизации получаются
					if (model.SkipCheck)
					{
						result.List.ForEach(x =>
						{
							x.Email = string.Empty;
							x.DateBirth = DateTime.Now;
							x.Address = string.Empty;
							x.HomeNumber = string.Empty;
							x.MobileNumber = string.Empty;
						});
					}

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
				if (!DepartmentUserManager.CheckAccess(model, _serviceOperation, AccessType.View, _entity))
				{
					return ResultService<LecturerViewModel>.Error(new MethodAccessException(DepartmentUserManager.ErrorMessage), ResultServiceStatusCode.Error);
				}

				using (var context = DepartmentUserManager.GetContext)
				{
					var entity = context.Lecturers
								.Include(x => x.LecturerDepartmentPost)
								.Include(x => x.LecturerStudyPost)
								.FirstOrDefault(x => x.Id == model.Id);
					if (entity == null)
					{
						return ResultService<LecturerViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
					}
					else if (entity.IsDeleted)
					{
						return ResultService<LecturerViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
					}

					// если данные без проверки авторизации получаются
					if (model.SkipCheck)
					{
						entity.Email = string.Empty;
						entity.DateBirth = DateTime.Now;
						entity.Address = string.Empty;
						entity.HomeNumber = string.Empty;
						entity.MobileNumber = string.Empty;
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