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
    public class DisciplineLessonConductedService : IDisciplineLessonConductedService
    {
        private readonly DepartmentDbContext _context;

        private readonly IDisciplineLessonService _serviceDL;

        private readonly IStudentGroupService _serviceSG;

        private readonly AccessOperation _serviceOperation = AccessOperation.Дисциплины;

        public DisciplineLessonConductedService(DepartmentDbContext context, IDisciplineLessonService serviceDL, IStudentGroupService serviceSG)
        {
            _context = context;
            _serviceDL = serviceDL;
            _serviceSG = serviceSG;
        }

        public ResultService<DisciplineLessonPageViewModel> GetDisciplineLessons(DisciplineLessonGetBindingModel model)
        {
            return _serviceDL.GetDisciplineLessons(model);
        }

        public ResultService<StudentGroupPageViewModel> GetStudentGroups(StudentGroupGetBindingModel model)
        {
            return _serviceSG.GetStudentGroups(model);
        }

        public ResultService<DisciplineLessonConductedPageViewModel> GetDisciplineLessonConducteds(DisciplineLessonConductedGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по дисциплинам");
                }

                int countPages = 0;
                var query = _context.DisciplineLessonConducteds.Where(d => !d.IsDeleted).AsQueryable();

                if (model.DisciplineLessonId.HasValue)
                {
                    query = query.Where(x => x.DisciplineLessonId == model.DisciplineLessonId);
                }
                if (model.DisciplineId.HasValue)
                {
                    query = query.Where(x => x.DisciplineLesson.DisciplineId == model.DisciplineId);
                }
                if (model.EducationDirectionId.HasValue)
                {
                    query = query.Where(x => x.DisciplineLesson.EducationDirectionId == model.EducationDirectionId);
                }
                if (model.TimeNormId.HasValue)
                {
                    query = query.Where(x => x.DisciplineLesson.TimeNormId == model.TimeNormId);
                }
                if (model.StudentGroupId.HasValue)
                {
                    query = query.Where(x => x.StudentGroupId == model.StudentGroupId);
                }
                if (!string.IsNullOrEmpty(model.Semester))
                {
                    Semesters sem = (Semesters)Enum.Parse(typeof(Semesters), model.Semester);
                    query = query.Where(x => x.DisciplineLesson.Semester == sem);
                }
                if (model.Id.HasValue)
                {
                    query = query.Where(x => x.Id == model.Id);
                }

                query = query.OrderBy(d => d.Subgroup).ThenBy(x => x.DateCreate);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query.Include(x => x.DisciplineLesson).Include(x => x.StudentGroup);

                var result = new DisciplineLessonConductedPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateDisciplineLessonConductedViewModel).ToList()
                };

                return ResultService<DisciplineLessonConductedPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<DisciplineLessonConductedPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineLessonConductedPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<DisciplineLessonConductedViewModel> GetDisciplineLessonConducted(DisciplineLessonConductedGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по дисциплинам");
                }

                var entity = _context.DisciplineLessonConducteds
                                .Include(x => x.DisciplineLesson)
                                .Include(x => x.StudentGroup)
                                .FirstOrDefault(d => d.Id == model.Id && !d.IsDeleted);
                if (entity == null)
                {
                    return ResultService<DisciplineLessonConductedViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<DisciplineLessonConductedViewModel>.Success(ModelFactoryToViewModel.CreateDisciplineLessonConductedViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<DisciplineLessonConductedViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineLessonConductedViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateDisciplineLessonConducted(DisciplineLessonConductedSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по дисциплинам");
                }

                var entity = ModelFacotryFromBindingModel.CreateDisciplineLessonConducted(model);

                _context.DisciplineLessonConducteds.Add(entity);
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

        public ResultService UpdateDisciplineLessonConducted(DisciplineLessonConductedSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по дисциплинам");
                }

                var entity = _context.DisciplineLessonConducteds.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity = ModelFacotryFromBindingModel.CreateDisciplineLessonConducted(model, entity);

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

        public ResultService DeleteDisciplineLessonConducted(DisciplineLessonConductedGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по дисциплинам");
                }

                var entity = _context.DisciplineLessonConducteds.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
