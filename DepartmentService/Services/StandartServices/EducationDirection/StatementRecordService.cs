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
    public class StatementRecordService : IStatementRecordService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        public StatementRecordService(DepartmentDbContext context)
        {
            _context = context;
        }

        public ResultService<StatementRecordPageViewModel> GetStatementRecords(StatementRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                int countPages = 0;
                var query = _context.StatementRecords.Where(record => !record.IsDeleted).AsQueryable();

                if(model.StatementId.HasValue)
                {
                    query = query.Where(record => record.StatementId == model.StatementId);
                }
                if(model.StudentId.HasValue)
                {
                    query = query.Where(record => record.StudentId == model.StudentId);
                }
                query = query.Include(record => record.Statement.AcademicPlanRecord.Discipline).Include(record => record.Student.StudentGroup).Include(record => record.StatementRecordExtendeds);
                query = query.OrderBy(record => record.StatementId).ThenBy(record => record.StudentId);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                var result = new StatementRecordPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateStatementRecordViewModel).ToList()
                };
                return ResultService<StatementRecordPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<StatementRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<StatementRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<StatementRecordViewModel> GetStatementRecord(StatementRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                var entity = _context.StatementRecords
                                .FirstOrDefault(record => record.Id == model.Id && !record.IsDeleted);
                if (entity == null)
                {
                    return ResultService<StatementRecordViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<StatementRecordViewModel>.Success(ModelFactoryToViewModel.CreateStatementRecordViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<StatementRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<StatementRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateAllFindStatementRecord(AcademicYearGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных");
                }
                var statement = _context.Statements.Where(record => !record.IsDeleted && record.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.Id);
                foreach (var sttmnt in statement)
                {
                    var students = _context.Students.Where(record => !record.IsDeleted && record.StudentGroupId == sttmnt.StudentGroupId);
                    var statementRecord = _context.StatementRecords.Where(record => !record.IsDeleted && record.StatementId == sttmnt.Id);
                    foreach (var stdnt in students)
                    {
                        if (statementRecord.FirstOrDefault(record => record.StatementId == sttmnt.Id
                             && record.StudentId == stdnt.Id) == null)
                        {
                            var entity = ModelFacotryFromBindingModel.CreateStatementRecord(new StatementRecordSetBindingModel()
                            {
                                StatementId = sttmnt.Id,
                                StudentId = stdnt.Id,
                                Score = "Не допущен"
                            });
                            if(sttmnt.TypeOfTest == TypeOfTest.Курсовая_работа)
                            {
                                var entityTo = ModelFacotryFromBindingModel.CreateStatementRecordExtended(new StatementRecordExtendedSetBindingModel()
                                {
                                    StatementRecordId = entity.Id,
                                    Name = "Нет темы"
                                });
                                _context.StatementRecordExtendeds.Add(entityTo);
                            }
                            _context.StatementRecords.Add(entity);
                        }
                    }
                }
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

        public ResultService CreateStatementRecord(StatementRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = ModelFacotryFromBindingModel.CreateStatementRecord(model);

                _context.StatementRecords.Add(entity);
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

        public ResultService UpdateStatementRecord(StatementRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = _context.StatementRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                entity = ModelFacotryFromBindingModel.CreateStatementRecord(model, entity);

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

        public ResultService DeleteStatementRecord(StatementRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных о ведомостях");
                }

                var entity = _context.StatementRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
