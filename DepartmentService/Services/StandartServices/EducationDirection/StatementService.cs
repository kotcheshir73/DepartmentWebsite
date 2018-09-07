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
    public class StatementService : IStatementService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        public StatementService(DepartmentDbContext context)
        {
            _context = context;
        }

        public ResultService<StatementPageViewModel> GetStatements(StatementGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                int countPages = 0;
                var query = _context.Statements.Where(record => !record.IsDeleted).AsQueryable();

                if(model.AcademicPlanRecordId.HasValue)
                {
                    query = query.Where(record => record.AcademicPlanRecordId == model.AcademicPlanRecordId);
                }
                if(model.LecturerId.HasValue)
                {
                    query = query.Where(record => record.LecturerId == model.LecturerId);
                }
                if (model.StudentGroupId.HasValue)
                {
                    query = query.Where(record => record.StudentGroupId == model.LecturerId);
                }

                query = query.OrderBy(record => record.AcademicPlanRecordId).ThenBy(record => record.LecturerId);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                //query = query.Include(apre => apre.AcademicPlanRecordElement).Include(apre => apre.AcademicPlanRecordElement.).Include(apre => apre.TimeNorm); не понятно что с этим делать

                var result = new StatementPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateStatementViewModel).ToList()
                };

                return ResultService<StatementPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<StatementPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<StatementPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<StatementViewModel> GetStatement(StatementGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                var entity = _context.Statements
                                .FirstOrDefault(record => record.Id == model.Id && !record.IsDeleted);
                if (entity == null)
                {
                    return ResultService<StatementViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<StatementViewModel>.Success(ModelFactoryToViewModel.CreateStatementViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<StatementViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<StatementViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateStatement(StatementSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = ModelFacotryFromBindingModel.CreateStatement(model);

                _context.Statements.Add(entity);
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

        public ResultService UpdateStatement(StatementSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = _context.Statements.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                //_context.Statements.Attach(entity);   //не уверен что так правильно

                entity = ModelFacotryFromBindingModel.CreateStatement(model, entity);

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

        public ResultService DeleteStatement(StatementGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных о ведомостях");
                }

                var entity = _context.Statements.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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

        public ResultService CreateAllFindStatement(StatementSetBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
