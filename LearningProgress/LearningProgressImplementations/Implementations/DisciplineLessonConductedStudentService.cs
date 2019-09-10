using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using DatabaseContext;
using Enums;
using LearningProgressImplementations;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using LearningProgressInterfaces.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;

namespace LearningProgressImplementations.Implementations
{
    public class DisciplineLessonConductedStudentService : IDisciplineLessonConductedStudentService
    {
        private readonly IDisciplineLessonConductedService _serviceDLR;

        private readonly IStudentService _serviceS;

        private readonly AccessOperation _serviceOperation = AccessOperation.Дисциплины;

        private readonly string _entity = "Дисциплины";

        public DisciplineLessonConductedStudentService(IDisciplineLessonConductedService serviceDLR, IStudentService serviceS)
        {
            _serviceDLR = serviceDLR;
            _serviceS = serviceS;
        }

        public ResultService<DisciplineLessonConductedPageViewModel> GetDisciplineLessonConducteds(DisciplineLessonConductedGetBindingModel model)
        {
            return _serviceDLR.GetDisciplineLessonConducteds(model);
        }

        public ResultService<StudentPageViewModel> GetStudents(StudentGetBindingModel model)
        {
            return _serviceS.GetStudents(model);
        }

        public ResultService<DisciplineLessonConductedStudentPageViewModel> GetDisciplineLessonConductedStudents(DisciplineLessonConductedStudentGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.DisciplineLessonConductedStudents.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.DisciplineLessonConductedId.HasValue)
                    {
                        query = query.Where(x => x.DisciplineLessonConductedId == model.DisciplineLessonConductedId);
                    }
                    if (model.StudentId.HasValue)
                    {
                        query = query.Where(x => x.StudentId == model.StudentId);
                    }
                    if (!string.IsNullOrEmpty(model.Comment))
                    {
                        query = query.Where(x => x.Comment.Contains(model.Comment));
                    }
                    if (model.Status.HasValue)
                    {
                        query = query.Where(x => x.Status == model.Status.Value);
                    }
                    if (model.Id.HasValue)
                    {
                        query = query.Where(x => x.Id == model.Id);
                    }

                    query = query.OrderBy(x => x.DisciplineLessonConducted.DisciplineLesson.Title).ThenBy(x => x.Student.LastName);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.DisciplineLessonConducted).Include(x => x.DisciplineLessonConducted.DisciplineLesson).Include(x => x.Student);

                    var result = new DisciplineLessonConductedStudentPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(LearningProgressModelFactoryToViewModel.CreateDisciplineLessonConductedStudentViewModel).ToList()
                    };
                    return ResultService<DisciplineLessonConductedStudentPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineLessonConductedStudentPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<DisciplineLessonConductedStudentViewModel> GetDisciplineLessonConductedStudent(DisciplineLessonConductedStudentGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineLessonConductedStudents
                                .Include(x => x.DisciplineLessonConducted)
                                .Include(x => x.DisciplineLessonConducted.DisciplineLesson)
                                .Include(x => x.Student)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<DisciplineLessonConductedStudentViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<DisciplineLessonConductedStudentViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<DisciplineLessonConductedStudentViewModel>.Success(LearningProgressModelFactoryToViewModel.CreateDisciplineLessonConductedStudentViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineLessonConductedStudentViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateDisciplineLessonConductedStudent(DisciplineLessonConductedStudentSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = LearningProgressModelFacotryFromBindingModel.CreateDisciplineLessonConductedStudent(model);

                    var exsistEntity = context.DisciplineLessonConductedStudents.FirstOrDefault(x => x.DisciplineLessonConductedId == entity.DisciplineLessonConductedId && 
                                                    x.StudentId == model.StudentId && x.Ball == model.Ball);
                    if (exsistEntity == null)
                    {
                        context.DisciplineLessonConductedStudents.Add(entity);
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

        public ResultService UpdateDisciplineLessonConductedStudent(DisciplineLessonConductedStudentSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineLessonConductedStudents.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = LearningProgressModelFacotryFromBindingModel.CreateDisciplineLessonConductedStudent(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteDisciplineLessonConductedStudent(DisciplineLessonConductedStudentGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineLessonConductedStudents.FirstOrDefault(x => x.Id == model.Id);
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