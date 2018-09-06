using DepartmentService.IServices.StandartInterfaces.EducationDirection;
using System;
using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.BindingModels.StandartBindingModels.EducationDirection;
using DepartmentService.ViewModels;
using DepartmentService.ViewModels.StandartViewModels.EducationDirection;
using DepartmentService.Context;
using DepartmentService.IServices;
using DepartmentModel.Enums;
using System.Data.Entity.Validation;
using System.Linq;
using System.Data.Entity;

namespace DepartmentService.Services.StandartServices.EducationDirection
{
    public class DisciplineLessonService : IDisciplineLessonService
    {
        private readonly DepartmentDbContext _context;
        private readonly IDisciplineService _serviceD;
        private readonly AccessOperation _serviceOperation = AccessOperation.Дисциплины;
        
        public DisciplineLessonService(DepartmentDbContext context, IDisciplineService serviceD)
        {
            _context = context;
            _serviceD = serviceD;
        }

        public ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model)
        {
            return _serviceD.GetDisciplines(model);
        }

        public ResultService<DisciplineLessonPageViewModel> GetDisciplineLessons(DisciplineLessonGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по дисциплинам");
                }

                int countPages = 0;
                var query = _context.DisciplineLessons.Where(d => !d.IsDeleted&&d.DisciplineId==model.DisciplineId).AsQueryable();

                query = query.OrderBy(d => d.Order);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                var result = new DisciplineLessonPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateDisciplineLessonViewModel).ToList()
                };

                return ResultService<DisciplineLessonPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<DisciplineLessonPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineLessonPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<DisciplineLessonViewModel> GetDisciplineLesson(DisciplineLessonGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по дисциплинам");
                }

                var entity = _context.DisciplineLessons
                                .FirstOrDefault(d => d.Id == model.Id && !d.IsDeleted);
                if (entity == null)
                {
                    return ResultService<DisciplineLessonViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<DisciplineLessonViewModel>.Success(ModelFactoryToViewModel.CreateDisciplineLessonViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<DisciplineLessonViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineLessonViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateDisciplineLesson(DisciplineLessonRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по дисциплинам");
                }

                var entity = ModelFacotryFromBindingModel.CreateDisciplineLesson(model);

                _context.DisciplineLessons.Add(entity);
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

        public ResultService UpdateDisciplineLesson(DisciplineLessonRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по дисциплинам");
                }

                var entity = _context.DisciplineLessons.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity = ModelFacotryFromBindingModel.CreateDisciplineLesson(model, entity);

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

        public ResultService DeleteDisciplineLesson(DisciplineLessonGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по дисциплинам");
                }

                var entity = _context.DisciplineLessons.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
