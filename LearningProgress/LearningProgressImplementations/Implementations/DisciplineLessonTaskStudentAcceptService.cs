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
    public class DisciplineLessonTaskStudentAcceptService : IDisciplineLessonTaskStudentAcceptService
    {
        private readonly IDisciplineLessonTaskService _serviceDLT;

        private readonly IStudentService _serviceS;

        private readonly AccessOperation _serviceOperation = AccessOperation.Дисциплины;

        private readonly string _entity = "Дисциплины";

        public DisciplineLessonTaskStudentAcceptService(IDisciplineLessonTaskService serviceDLT, IStudentService serviceS)
        {
            _serviceDLT = serviceDLT;
            _serviceS = serviceS;
        }

        public ResultService<DisciplineLessonPageViewModel> GetDisciplineLessons(DisciplineLessonGetBindingModel model)
        {
            return _serviceDLT.GetDisciplineLessons(model);
        }

        public ResultService<DisciplineLessonTaskPageViewModel> GetDisciplineLessonTasks(DisciplineLessonTaskGetBindingModel model)
        {
            return _serviceDLT.GetDisciplineLessonTasks(model);
        }

        public ResultService<StudentGroupPageViewModel> GetStudentGroups(StudentGroupGetBindingModel model)
        {
            return _serviceS.GetStudentGroups(model);
        }

        public ResultService<StudentPageViewModel> GetStudents(StudentGetBindingModel model)
        {
            return _serviceS.GetStudents(model);
        }

        public ResultService<DisciplineLessonTaskStudentAcceptPageViewModel> GetDisciplineLessonTaskStudentAccepts(DisciplineLessonTaskStudentAcceptGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.DisciplineLessonTaskStudentAccepts.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.DisciplineLessonTaskId.HasValue)
                    {
                        query = query.Where(x => x.DisciplineLessonTaskId == model.DisciplineLessonTaskId);
                    }
                    if (model.DisciplineLessonId.HasValue)
                    {
                        query = query.Where(x => x.DisciplineLessonTask.DisciplineLessonId == model.DisciplineLessonId);
                    }
                    if (model.StudentGroupId.HasValue)
                    {
                        query = query.Where(x => x.Student.StudentGroupId == model.StudentGroupId);
                    }
                    if (model.StudentId.HasValue)
                    {
                        query = query.Where(x => x.StudentId == model.StudentId);
                    }
                    if (model.Id.HasValue)
                    {
                        query = query.Where(x => x.Id == model.Id);
                    }

                    query = query.OrderBy(x => x.DisciplineLessonTask.Order).ThenBy(x => x.Student.LastName).ThenBy(x => x.Student.FirstName);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.DisciplineLessonTask).Include(x => x.Student);

                    var result = new DisciplineLessonTaskStudentAcceptPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(LearningProgressModelFactoryToViewModel.CreateDisciplineLessonTaskStudentAcceptViewModel).ToList()
                    };

                    return ResultService<DisciplineLessonTaskStudentAcceptPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineLessonTaskStudentAcceptPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<DisciplineLessonTaskStudentAcceptViewModel> GetDisciplineLessonTaskStudentAccept(DisciplineLessonTaskStudentAcceptGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineLessonTaskStudentAccepts
                                .Include(x => x.DisciplineLessonTask)
                                .Include(x => x.Student)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<DisciplineLessonTaskStudentAcceptViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<DisciplineLessonTaskStudentAcceptViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<DisciplineLessonTaskStudentAcceptViewModel>.Success(LearningProgressModelFactoryToViewModel.CreateDisciplineLessonTaskStudentAcceptViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineLessonTaskStudentAcceptViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateDisciplineLessonTaskStudentAccept(DisciplineLessonTaskStudentAcceptSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = LearningProgressModelFacotryFromBindingModel.CreateDisciplineLessonTaskStudentAccept(model);

                    var exsistEntity = context.DisciplineLessonTaskStudentAccepts.FirstOrDefault(x => x.DisciplineLessonTaskId == entity.DisciplineLessonTaskId && 
                                                    x.StudentId == model.StudentId && x.Task == model.Task);
                    if (exsistEntity == null)
                    {
                        context.DisciplineLessonTaskStudentAccepts.Add(entity);
                        entity.Log = string.Format("{0} {1}", entity.Result.ToString(), entity.DateAccept.ToShortDateString());
                        context.SaveChanges();
                        return ResultService.Success(entity.Id);
                    }
                    else
                    {
                        if (exsistEntity.IsDeleted)
                        {
                            exsistEntity.IsDeleted = false;
                            entity.Log = string.Format("{0} {1}", entity.Result.ToString(), entity.DateAccept.ToShortDateString());
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

        public ResultService UpdateDisciplineLessonTaskStudentAccept(DisciplineLessonTaskStudentAcceptSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineLessonTaskStudentAccepts.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    model.Log = (entity.Result.ToString() != model.Result) ?
                        string.Format("{0}{1}{2} {3}", entity.Log, Environment.NewLine, model.Result, model.DateAccept.ToShortDateString()) :
                        entity.Log;

                    entity = LearningProgressModelFacotryFromBindingModel.CreateDisciplineLessonTaskStudentAccept(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteDisciplineLessonTaskStudentAccept(DisciplineLessonTaskStudentAcceptGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineLessonTaskStudentAccepts.FirstOrDefault(x => x.Id == model.Id);
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