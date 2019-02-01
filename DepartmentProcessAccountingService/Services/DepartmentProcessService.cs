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
using System.Data.Entity.Validation;
using DepartmentModel.Models;

namespace DepartmentProcessAccountingService.Services
{
    public class DepartmentProcessService : IDepartmentProcessService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Каф_процессы;

        public DepartmentProcessService(DepartmentDbContext context)
        {
            _context = context;
        }

        public ResultService<DepartmentProcessPageViewModel> GetDepartmentProcesses(DepartmentProcessGetBindingModel model)
        {
            try
            {
                //if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                //{
                //    throw new Exception("Нет доступа на чтение данных по дисциплинам");
                //}

                int countPages = 0;
                var query = _context.DepartmentProcesses.Where(d => !d.IsDeleted).AsQueryable();
                
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

                var result = new DepartmentProcessPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateDepartmentProcessViewModel).ToList()
                };

                return ResultService<DepartmentProcessPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<DepartmentProcessPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<DepartmentProcessPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<DepartmentProcessViewModel> GetDepartmentProcess(DepartmentProcessGetBindingModel model)
        {
            try
            {
                //if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                //{
                //    throw new Exception("Нет доступа на чтение данных по дисциплинам");
                //}

                var entity = _context.DepartmentProcesses
                                .FirstOrDefault(d => d.Id == model.Id && !d.IsDeleted);
                if (entity == null)
                {
                    return ResultService<DepartmentProcessViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<DepartmentProcessViewModel>.Success(ModelFactoryToViewModel.CreateDepartmentProcessViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<DepartmentProcessViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<DepartmentProcessViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<DepartmentProcessViewModel> GetDepartmentProcessByDate(DateTime date)
        {
            try
            {
                //if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                //{
                //    throw new Exception("Нет доступа на чтение данных по дисциплинам");
                //}

                var entity = _context.DepartmentProcesses
                                .FirstOrDefault(d => d.DateStart==date && !d.IsDeleted);
                if (entity == null)
                {
                    return ResultService<DepartmentProcessViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<DepartmentProcessViewModel>.Success(ModelFactoryToViewModel.CreateDepartmentProcessViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<DepartmentProcessViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<DepartmentProcessViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateDepartmentProcess(DepartmentProcessRecordBindingModel model)
        {
            try
            {
                //if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                //{
                //    throw new Exception("Нет доступа на изменение данных по дисциплинам");
                //}

                var entity = ModelFacotryFromBindingModel.CreateDepartmentProcess(model);
                _context.DepartmentProcesses.Add(entity);
                _context.SaveChanges();

                var res = _context.DepartmentProcesses.FirstOrDefault(d => d.Id == entity.Id);
                AcademicYearProcessRecordBindingModel modelAY = new AcademicYearProcessRecordBindingModel {
                    DepartmentProcessId = entity.Id,
                    IsConfirmed = entity.Confirmability,
                    Status = ProcessStatus.запущен,
                    UserId = new Guid("926ed7de-a082-4b68-a622-fceeff9b60d3"),
                    AcademicYearId=new Guid("134ed8ea-5bc8-4ff1-9a52-8ff6dd8775fc")
                };

                var entityAY = ModelFacotryFromBindingModel.CreateAcademicYearProcess(modelAY);
                _context.AcademicYearProcesses.Add(entityAY);
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

        public ResultService UpdateDepartmentProcess(DepartmentProcessRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по дисциплинам");
                }

                var entity = _context.DepartmentProcesses.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity = ModelFacotryFromBindingModel.CreateDepartmentProcess(model, entity);

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
        
        public ResultService DeleteDepartmentProcess(DepartmentProcessGetBindingModel model)
        {
            try
            {
                //if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                //{
                //    throw new Exception("Нет доступа на удаление данных по дисциплинам");
                //}

                var entity = _context.DepartmentProcesses.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
