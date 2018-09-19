using DepartmentProcessAccountingService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepartmentModel;
using DepartmentProcessAccountingService.BindingModels;
using DepartmentProcessAccountingService.ViewModels;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using DepartmentService.Context;
using DepartmentService.IServices;
using DepartmentModel.Enums;
using DepartmentService;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace DepartmentProcessAccountingService.Services
{
    public class ProcessDirectionRecordService : IProcessDirectionRecordService
    {
        private readonly DepartmentDbContext _context;
        private readonly IDepartmentProcessService _serviceDP;
        private readonly IEducationDirectionService _serviceED;
        private readonly AccessOperation _serviceOperation = AccessOperation.Дисциплины;

        public ProcessDirectionRecordService(DepartmentDbContext context, IDepartmentProcessService serviceDP, IEducationDirectionService serviceED)
        {
            _context = context;
            _serviceDP = serviceDP;
            _serviceED = serviceED;
        }

        public ResultService<DepartmentProcessPageViewModel> GetDepartmentProcesses(DepartmentProcessGetBindingModel model)
        {
            return _serviceDP.GetDepartmentProcesses(model);
        }

        public ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model)
        {
            return _serviceED.GetEducationDirections(model);
        }

        public ResultService<ProcessDirectionRecordPageViewModel> GetProcessDirectionRecords(ProcessDirectionRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по дисциплинам");
                }

                int countPages = 0;
                var query = _context.ProcessDirectionRecords.Where(d => !d.IsDeleted).AsQueryable();

                if (model.DepartmentProcessId.HasValue)
                {
                    query = query.Where(x => x.DepartmentProcessId == model.DepartmentProcessId);
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

                var result = new ProcessDirectionRecordPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateProcessDirectionRecordViewModel).ToList()
                };

                return ResultService<ProcessDirectionRecordPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<ProcessDirectionRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<ProcessDirectionRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<ProcessDirectionRecordViewModel> GetProcessDirectionRecord(ProcessDirectionRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по дисциплинам");
                }

                var entity = _context.ProcessDirectionRecords
                                .FirstOrDefault(d => d.Id == model.Id && !d.IsDeleted);
                if (entity == null)
                {
                    return ResultService<ProcessDirectionRecordViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<ProcessDirectionRecordViewModel>.Success(ModelFactoryToViewModel.CreateProcessDirectionRecordViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<ProcessDirectionRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<ProcessDirectionRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateProcessDirectionRecord(ProcessDirectionRecordRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по дисциплинам");
                }

                var entity = ModelFacotryFromBindingModel.CreateProcessDirectionRecord(model);

                _context.ProcessDirectionRecords.Add(entity);
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

        public ResultService UpdateProcessDirectionRecord(ProcessDirectionRecordRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по дисциплинам");
                }

                var entity = _context.ProcessDirectionRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity = ModelFacotryFromBindingModel.CreateProcessDirectionRecord(model, entity);

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

        public ResultService DeleteProcessDirectionRecord(ProcessDirectionRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по дисциплинам");
                }

                var entity = _context.ProcessDirectionRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
