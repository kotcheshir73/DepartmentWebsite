using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.Context;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
    public class LecturerWorkloadService : ILecturerWorkloadService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        public LecturerWorkloadService(DepartmentDbContext context)
        {
            _context = context;
        }

        public ResultService<LecturerWorkloadPageViewModel> GetLecturerWorkloads(LecturerWorkloadGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                int countPages = 0;
                var query = _context.LecturerWorkload.Where(record => !record.IsDeleted).AsQueryable();

                if(model.AcademicYearId.HasValue)
                {
                    query = query.Where(record => record.AcademicYearId == model.AcademicYearId);
                }
                if(model.LecturerId.HasValue)
                {
                    query = query.Where(record => record.LecturerId == model.LecturerId);
                }

                query = query.OrderBy(record => record.AcademicYearId).ThenBy(apre => apre.LecturerId);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }
                
                var result = new LecturerWorkloadPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateLecturerWorkloadViewModel).ToList()
                };

                return ResultService<LecturerWorkloadPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<LecturerWorkloadPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<LecturerWorkloadPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<LecturerWorkloadViewModel> GetLecturerWorkload(LecturerWorkloadGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей");
                }

                var entity = _context.LecturerWorkload
                                .FirstOrDefault(record => record.Id == model.Id && !record.IsDeleted);
                if (entity == null)
                {
                    return ResultService<LecturerWorkloadViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<LecturerWorkloadViewModel>.Success(ModelFactoryToViewModel.CreateLecturerWorkloadViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<LecturerWorkloadViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<LecturerWorkloadViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateLecturerWorkload(LecturerWorkloadSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей");
                }

                var entity = ModelFacotryFromBindingModel.CreateLecturerWorkload(model);

                _context.LecturerWorkload.Add(entity);
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

        public ResultService UpdateLecturerWorkload(LecturerWorkloadSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = _context.LecturerWorkload.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                entity = ModelFacotryFromBindingModel.CreateLecturerWorkload(model, entity);

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

        public ResultService DeleteLecturerWorkload(LecturerWorkloadGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по элементам записей");
                }

                var entity = _context.LecturerWorkload.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
