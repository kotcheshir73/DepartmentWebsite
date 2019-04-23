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
    public class DisciplineLessonTaskStudentAcceptService : IDisciplineLessonTaskStudentAcceptService
    {
        private readonly DepartmentDbContext _context;

        private readonly IDisciplineLessonTaskService _serviceDLT;

        private readonly IStudentService _serviceS;

        private readonly AccessOperation _serviceOperation = AccessOperation.Дисциплины;

        public DisciplineLessonTaskStudentAcceptService(DepartmentDbContext context, IDisciplineLessonTaskService serviceDLT, IStudentService serviceS)
        {
            _context = context;
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
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по дисциплинам");
                }

                int countPages = 0;
                var query = _context.DisciplineLessonTaskStudentAccepts.Where(d => !d.IsDeleted).AsQueryable();

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
                    List = query.Select(ModelFactoryToViewModel.CreateDisciplineLessonTaskStudentAcceptViewModel).ToList()
                };

                return ResultService<DisciplineLessonTaskStudentAcceptPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<DisciplineLessonTaskStudentAcceptPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
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
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по дисциплинам");
                }

                var entity = _context.DisciplineLessonTaskStudentAccepts
                                .Include(x => x.DisciplineLessonTask)
                                .Include(x => x.Student)
                                .FirstOrDefault(d => d.Id == model.Id && !d.IsDeleted);
                if (entity == null)
                {
                    return ResultService<DisciplineLessonTaskStudentAcceptViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<DisciplineLessonTaskStudentAcceptViewModel>.Success(ModelFactoryToViewModel.CreateDisciplineLessonTaskStudentAcceptViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<DisciplineLessonTaskStudentAcceptViewModel>.Error(ex, ResultServiceStatusCode.Error);
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
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по дисциплинам");
                }

                var entity = ModelFacotryFromBindingModel.CreateDisciplineLessonTaskStudentAccept(model);

                _context.DisciplineLessonTaskStudentAccepts.Add(entity);
                entity.Log = string.Format("{0} {1}", entity.Result.ToString(), entity.DateAccept.ToShortDateString());
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

        public ResultService UpdateDisciplineLessonTaskStudentAccept(DisciplineLessonTaskStudentAcceptSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по дисциплинам");
                }

                var entity = _context.DisciplineLessonTaskStudentAccepts.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                model.Log = (entity.Result.ToString() != model.Result) ?
                    string.Format("{0}{1}{2} {3}", entity.Log, Environment.NewLine, model.Result, model.DateAccept.ToShortDateString()) :
                    entity.Log;
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity = ModelFacotryFromBindingModel.CreateDisciplineLessonTaskStudentAccept(model, entity);

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

        public ResultService DeleteDisciplineLessonTaskStudentAccept(DisciplineLessonTaskStudentAcceptGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по дисциплинам");
                }

                var entity = _context.DisciplineLessonTaskStudentAccepts.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
