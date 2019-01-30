using DepartmentContext;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
    public class StatementRecordExtendedService : IStatementRecordExtendedService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        public StatementRecordExtendedService(DepartmentDbContext context)
        {
            _context = context;
        }

        public ResultService<StatementRecordExtendedPageViewModel> GetStatementRecordExtendeds(StatementRecordExtendedGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }
                
                var query = _context.StatementRecordExtendeds.Where(record => !record.IsDeleted).AsQueryable();

                if(model.StatementRecordId.HasValue)
                {
                    query = query.Where(record => record.StatementRecordId == model.StatementRecordId);
                }
                query = query.OrderBy(record => record.Name);
                
                
                var result = new StatementRecordExtendedPageViewModel
                {
                    List = query.Select(ModelFactoryToViewModel.CreateStatementRecordExtendedViewModel).ToList()
                };

                return ResultService<StatementRecordExtendedPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<StatementRecordExtendedPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<StatementRecordExtendedPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<StatementRecordExtendedViewModel> GetStatementRecordExtended(StatementRecordExtendedGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                var entity = _context.StatementRecordExtendeds
                                .FirstOrDefault(record => record.Id == model.Id && !record.IsDeleted);
                if (entity == null)
                {
                    return ResultService<StatementRecordExtendedViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<StatementRecordExtendedViewModel>.Success(ModelFactoryToViewModel.CreateStatementRecordExtendedViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<StatementRecordExtendedViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<StatementRecordExtendedViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateStatementRecordExtended(StatementRecordExtendedSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = ModelFacotryFromBindingModel.CreateStatementRecordExtended(model);

                _context.StatementRecordExtendeds.Add(entity);
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

        public ResultService UpdateStatementRecordExtended(StatementRecordExtendedSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = _context.StatementRecordExtendeds.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                entity = ModelFacotryFromBindingModel.CreateStatementRecordExtended(model, entity);

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

        public ResultService DeleteStatementRecordExtended(StatementRecordExtendedGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных о ведомостях");
                }

                var entity = _context.StatementRecordExtendeds.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
