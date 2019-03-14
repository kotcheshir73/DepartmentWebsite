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
    public class StatementService : IStatementService
    {
        private readonly DepartmentDbContext _context;

        private readonly IStatementRecordService _serviceSR;

        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        public StatementService(DepartmentDbContext context, IStatementRecordService serviceSR)
        {
            _context = context;
            _serviceSR = serviceSR;
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
                if (model.AcademicYearId.HasValue)
                {
                    query = query.Where(record => record.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.AcademicYearId);
                }

                query = query.OrderBy(record => record.Semester).ThenBy(record => record.AcademicPlanRecord.Discipline.DisciplineName);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query.Include(apre => apre.AcademicPlanRecord.AcademicPlan).Include(apre => apre.Lecturer).Include(apre => apre.StudentGroup).Include(apre => apre.AcademicPlanRecord.Discipline);

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

        public ResultService CreateAllFindStatement(AcademicYearGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных");
                }
                var statement = _context.Statements.Where(record => !record.IsDeleted && record.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.Id);
                var timeNorm = _context.TimeNorms.Where(record => !record.IsDeleted && record.AcademicYearId == model.Id 
                    && (record.KindOfLoadName == "Экзамен" || record.KindOfLoadName == "Зачет" || record.KindOfLoadName == "Зачет с оценкой" || record.KindOfLoadName == "Курсовая работа")); //Получение трех норм времени для поиска ведомостей
                foreach(var tn in timeNorm)
                {
                    //Поиск найзначеных часов преподавателям
                    var APRM = _context.AcademicPlanRecordMissions.Where(record => !record.IsDeleted).Include(record => record.AcademicPlanRecordElement.AcademicPlanRecord.Contingent)
                        .Include(record => record.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan)
                        .Where(record => record.AcademicPlanRecordElement.TimeNormId == tn.Id);
                    string nameTN = tn.KindOfLoadName == "Зачет с оценкой" ? "Диференцированный_зачет" : tn.KindOfLoadName == "Курсовая работа" ? "Курсовая_работа" : tn.KindOfLoadName;
                    foreach (var APRMRecord in APRM)
                    {

                        var studentGroup = _context.StudentGroups.Where(record => !record.IsDeleted && record.EducationDirectionId == APRMRecord.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.EducationDirectionId
                            && record.Course == APRMRecord.AcademicPlanRecordElement.AcademicPlanRecord.Contingent.Course);
                        foreach(var SGRecord in studentGroup)
                        {
                            if(statement.FirstOrDefault(record => !record.IsDeleted 
                                && record.AcademicPlanRecordId == APRMRecord.AcademicPlanRecordElement.AcademicPlanRecordId
                                && record.LecturerId == APRMRecord.LecturerId
                                && record.StudentGroupId == SGRecord.Id) == null)
                            {
                                var entity = ModelFacotryFromBindingModel.CreateStatement(new StatementSetBindingModel()
                                {
                                    LecturerId = APRMRecord.LecturerId,
                                    AcademicPlanRecordId = APRMRecord.AcademicPlanRecordElement.AcademicPlanRecordId,
                                    Semester = APRMRecord.AcademicPlanRecordElement.AcademicPlanRecord.Semester.ToString(),
                                    Course = APRMRecord.AcademicPlanRecordElement.AcademicPlanRecord.Contingent.Course.ToString(),
                                    TypeOfTest = nameTN,
                                    StudentGroupId = SGRecord.Id
                                });
                                _context.Statements.Add(entity);
                            }
                        }
                    }
                }
                _context.SaveChanges();
                _serviceSR.CreateAllFindStatementRecord(model);
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

    }
}
