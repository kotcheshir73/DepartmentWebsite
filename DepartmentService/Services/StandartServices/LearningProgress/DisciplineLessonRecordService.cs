using DepartmentService.IServices.StandartInterfaces.LearningProgress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.BindingModels.StandartBindingModels.LearningProgress;
using DepartmentService.ViewModels;
using DepartmentService.ViewModels.StandartViewModels.LearningProgress;
using DepartmentService.Context;
using DepartmentModel.Enums;
using DepartmentService.IServices;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace DepartmentService.Services.StandartServices.LearningProgress
{
    public class DisciplineLessonRecordService : IDisciplineLessonRecordService
    {
        private readonly DepartmentDbContext _context;

        private readonly IDisciplineLessonService _serviceDL;

        private readonly AccessOperation _serviceOperation = AccessOperation.Дисциплины;

        public DisciplineLessonRecordService(DepartmentDbContext context, IDisciplineLessonService serviceDL)
        {
            _context = context;
            _serviceDL = serviceDL;
        }

        public ResultService<DisciplineLessonPageViewModel> GetDisciplineLessons(DisciplineLessonGetBindingModel model)
        {
            return _serviceDL.GetDisciplineLessons(model);
        }

        public ResultService<DisciplineLessonRecordPageViewModel> GetDisciplineLessonRecords(DisciplineLessonRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по дисциплинам");
                }

                int countPages = 0;
                var query = _context.DisciplineLessonRecords.Where(d => !d.IsDeleted).AsQueryable();

                if (model.DisciplineLessonId.HasValue)
                {
                    query = query.Where(x => x.DisciplineLessonId == model.DisciplineLessonId);
                }
                if (!string.IsNullOrEmpty(model.Subgroup))
                {
                    query = query.Where(x => x.Subgroup == model.Subgroup);
                }
                if (model.Id.HasValue)
                {
                    query = query.Where(x => x.Id == model.Id);
                }
                if (model.Date!=null)
                {
                    query = query.Where(x => x.Date == model.Date);
                }

                query = query.OrderBy(d => d.Subgroup).ThenBy(x => x.Date);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query.Include(x => x.DisciplineLesson);

                var result = new DisciplineLessonRecordPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateDisciplineLessonRecordViewModel).ToList()
                };

                return ResultService<DisciplineLessonRecordPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<DisciplineLessonRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineLessonRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<DisciplineLessonRecordViewModel> GetDisciplineLessonRecord(DisciplineLessonRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по дисциплинам");
                }

                var entity = _context.DisciplineLessonRecords
                                .FirstOrDefault(d => d.Id == model.Id && !d.IsDeleted);
                if (entity == null)
                {
                    return ResultService<DisciplineLessonRecordViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<DisciplineLessonRecordViewModel>.Success(ModelFactoryToViewModel.CreateDisciplineLessonRecordViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<DisciplineLessonRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineLessonRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateDisciplineLessonRecord(DisciplineLessonRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по дисциплинам");
                }

                var entity = ModelFacotryFromBindingModel.CreateDisciplineLessonRecord(model);

                _context.DisciplineLessonRecords.Add(entity);
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

        public ResultService UpdateDisciplineLessonRecord(DisciplineLessonRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по дисциплинам");
                }

                var entity = _context.DisciplineLessonRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity = ModelFacotryFromBindingModel.CreateDisciplineLessonRecord(model, entity);

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

        public ResultService DeleteDisciplineLessonRecord(DisciplineLessonRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по дисциплинам");
                }

                var entity = _context.DisciplineLessonRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
