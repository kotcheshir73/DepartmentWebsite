using DepartmentProcessAccountingService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepartmentModel;
using DepartmentProcessAccountingService.BindingModels;
using DepartmentProcessAccountingService.ViewModels;
using DepartmentService.Context;
using DepartmentModel.Enums;
using DepartmentService;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace DepartmentProcessAccountingService.Services
{
    public class AcademicYearProcessService : IAcademicYearProcessService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Каф_процессы;

        private readonly IDepartmentProcessService _service;

        public AcademicYearProcessService(DepartmentDbContext context, IDepartmentProcessService service)
        {
            _context = context;
            _service = service;
        }

        public ResultService<DepartmentProcessPageViewModel> GetDepartmentProcesses(DepartmentProcessGetBindingModel model)
        {
            return _service.GetDepartmentProcesses(model);
        }

        public ResultService<AcademicYearProcessPageViewModel> GetAcademicYearProcesses(AcademicYearProcessGetBindingModel model)
        {
            try
            {
                int countPages = 0;
                var query = _context.AcademicYearProcesses.Where(d => !d.IsDeleted).AsQueryable();

                if (model.DepartmentProcessId.HasValue)
                {
                    query = query.Where(x => x.DepartmentProcessId == model.DepartmentProcessId);
                }
                if (model.UserId.HasValue)
                {
                    query = query.Where(x => x.UserId == model.UserId);
                }
                if (model.Id.HasValue)
                {
                    query = query.Where(x => x.Id == model.Id);
                }

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query.Include(x => x.DepartmentProcess);

                var result = new AcademicYearProcessPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateAcademicYearProcessViewModel).ToList()
                };

                return ResultService<AcademicYearProcessPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<AcademicYearProcessPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<AcademicYearProcessPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }
        
        public ResultService<AcademicYearProcessViewModel> GetAcademicYearProcess(AcademicYearProcessGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по дисциплинам");
                }

                var entity = _context.AcademicYearProcesses
                                .FirstOrDefault(d => d.Id == model.Id && !d.IsDeleted);
                if (entity == null)
                {
                    return ResultService<AcademicYearProcessViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<AcademicYearProcessViewModel>.Success(ModelFactoryToViewModel.CreateAcademicYearProcessViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<AcademicYearProcessViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<AcademicYearProcessViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateAcademicYearProcess(AcademicYearProcessRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по дисциплинам");
                }

                var entity = ModelFacotryFromBindingModel.CreateAcademicYearProcess(model);

                _context.AcademicYearProcesses.Add(entity);
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

        public ResultService UpdateAcademicYearProcess(AcademicYearProcessRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по дисциплинам");
                }

                var entity = _context.AcademicYearProcesses.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity = ModelFacotryFromBindingModel.CreateAcademicYearProcess(model, entity);

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

        public ResultService DeleteAcademicYearProcess(AcademicYearProcessGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по дисциплинам");
                }

                var entity = _context.AcademicYearProcesses.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
