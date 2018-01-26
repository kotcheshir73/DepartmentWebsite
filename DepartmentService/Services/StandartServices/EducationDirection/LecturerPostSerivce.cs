using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
    public class LecturerPostSerivce : ILecturerPostSerivce
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Должности_преподавателей;

        public LecturerPostSerivce(DepartmentDbContext context)
        {
            _context = context;
        }


        public ResultService<LecturerPostPageViewModel> GetLecturerPosts(LecturerPostGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по должностям");
                }

                int countPages = 0;
                var query = _context.LecturerPosts.Where(ed => !ed.IsDeleted).AsQueryable();

                query = query.OrderBy(ed => ed.PostTitle);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                var result = new LecturerPostPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateLecturerPostViewModel).ToList()
                };

                return ResultService<LecturerPostPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<LecturerPostPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<LecturerPostPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<LecturerPostViewModel> GetLecturerPost(LecturerPostGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по должностям");
                }

                var entity = _context.LecturerPosts
                                .FirstOrDefault(ed => ed.Id == model.Id && !ed.IsDeleted);
                if (entity == null)
                {
                    return ResultService<LecturerPostViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                return ResultService<LecturerPostViewModel>.Success(ModelFactoryToViewModel.CreateLecturerPostViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<LecturerPostViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<LecturerPostViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateLecturerPost(LecturerPostRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по должностям");
                }

                var entity = ModelFacotryFromBindingModel.CreateLecturerPost(model);
                _context.LecturerPosts.Add(entity);
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

        public ResultService UpdateLecturerPost(LecturerPostRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по должностям");
                }

                var entity = _context.LecturerPosts.FirstOrDefault(ed => ed.Id == model.Id && !ed.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity = ModelFacotryFromBindingModel.CreateLecturerPost(model, entity);

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

        public ResultService DeleteLecturerPost(LecturerPostGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по должностям");
                }

                var entity = _context.LecturerPosts.FirstOrDefault(ed => ed.Id == model.Id && !ed.IsDeleted);
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
