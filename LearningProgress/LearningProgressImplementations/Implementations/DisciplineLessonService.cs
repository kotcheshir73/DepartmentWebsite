using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using Enums;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using LearningProgressInterfaces.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;

namespace LearningProgressImplementations.Implementations
{
    public class DisciplineLessonService : IDisciplineLessonService
    {
        private readonly IAcademicYearService _serviceAY;

        private readonly IDisciplineService _serviceD;

        private readonly IEducationDirectionService _serviceED;

        private readonly ITimeNormService _serviceTN;

        private readonly AccessOperation _serviceOperation = AccessOperation.Дисциплины;

        private readonly string _entity = "Дисциплины";

        public DisciplineLessonService(IAcademicYearService serviceAY, IDisciplineService serviceD, IEducationDirectionService serviceED, ITimeNormService serviceTN)
        {
            _serviceAY = serviceAY;
            _serviceD = serviceD;
            _serviceED = serviceED;
            _serviceTN = serviceTN;
        }

        public ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model)
        {
            return _serviceAY.GetAcademicYears(model);
        }

        public ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model)
        {
            return _serviceD.GetDisciplines(model);
        }

        public ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model)
        {
            return _serviceED.GetEducationDirections(model);
        }

        public ResultService<TimeNormPageViewModel> GetTimeNorms(TimeNormGetBindingModel model)
        {
            return _serviceTN.GetTimeNorms(model);
        }

        public ResultService<DisciplineLessonPageViewModel> GetDisciplineLessons(DisciplineLessonGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.DisciplineLessons.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.AcademicYearId.HasValue)
                    {
                        query = query.Where(x => x.AcademicYearId == model.AcademicYearId);
                    }
                    if (model.DisciplineId.HasValue)
                    {
                        query = query.Where(x => x.DisciplineId == model.DisciplineId);
                    }
                    if (model.EducationDirectionId.HasValue)
                    {
                        query = query.Where(x => x.EducationDirectionId == model.EducationDirectionId);
                    }
                    if (model.TimeNormId.HasValue)
                    {
                        query = query.Where(x => x.TimeNormId == model.TimeNormId);
                    }
                    if (model.Id.HasValue)
                    {
                        query = query.Where(x => x.Id == model.Id);
                    }
                    if (!string.IsNullOrEmpty(model.Semester))
                    {
                        Semesters sem = (Semesters)Enum.Parse(typeof(Semesters), model.Semester);
                        query = query.Where(x => x.Semester == sem);
                    }

                    query = query.OrderBy(d => d.Semester).ThenBy(x => x.Order);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.AcademicYear).Include(x => x.Discipline).Include(x => x.EducationDirection).Include(x => x.TimeNorm).Include(x => x.DisciplineLessonTasks);

                    var result = new DisciplineLessonPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(LearningProgressModelFactoryToViewModel.CreateDisciplineLessonViewModel).ToList()
                    };

                    return ResultService<DisciplineLessonPageViewModel>.Success(result);
                }
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineLessons
                                .Include(x => x.AcademicYear).Include(x => x.Discipline).Include(x => x.EducationDirection).Include(x => x.TimeNorm).Include(x => x.DisciplineLessonTasks)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<DisciplineLessonViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<DisciplineLessonViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<DisciplineLessonViewModel>.Success(LearningProgressModelFactoryToViewModel.CreateDisciplineLessonViewModel(entity));
                }
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = LearningProgressModelFacotryFromBindingModel.CreateDisciplineLesson(model);

                    var exsistEntity = context.DisciplineLessons.FirstOrDefault(x => x.Title == entity.Title && x.AcademicYearId == model.AcademicYearId);
                    if (exsistEntity == null)
                    {
                        context.DisciplineLessons.Add(entity);
                        context.SaveChanges();
                        return ResultService.Success(entity.Id);
                    }
                    else
                    {
                        if (exsistEntity.IsDeleted)
                        {
                            exsistEntity.IsDeleted = false;
                            context.SaveChanges();
                            return ResultService.Success(exsistEntity.Id);
                        }
                        else
                        {
                            return ResultService.Error("Error:", "Элемент уже существует", ResultServiceStatusCode.ExsistItem);
                        }
                    }
                }
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineLessons.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = LearningProgressModelFacotryFromBindingModel.CreateDisciplineLesson(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineLessons.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity.IsDeleted = true;
                    entity.DateDelete = DateTime.Now;

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}