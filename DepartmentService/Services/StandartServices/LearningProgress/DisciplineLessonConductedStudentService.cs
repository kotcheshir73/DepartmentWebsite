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
    public class DisciplineLessonConductedStudentService : IDisciplineLessonConductedStudentService
    {
        private readonly DepartmentDbContext _context;

        private readonly IDisciplineLessonConductedService _serviceDLR;

        private readonly IStudentService _serviceS;

        private readonly AccessOperation _serviceOperation = AccessOperation.Дисциплины;

        public DisciplineLessonConductedStudentService(DepartmentDbContext context, IDisciplineLessonConductedService serviceDLR, IStudentService serviceS)
        {
            _context = context;
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
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по дисциплинам");
                }

                int countPages = 0;
                var query = _context.DisciplineLessonConductedStudents.Where(d => !d.IsDeleted).AsQueryable();

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

                query = query.OrderBy(d => d.DisciplineLessonConducted.DisciplineLesson.Title).ThenBy(x => x.Student.LastName);

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
                    List = query.Select(ModelFactoryToViewModel.CreateDisciplineLessonConductedStudentViewModel).ToList()
                };
                return ResultService<DisciplineLessonConductedStudentPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<DisciplineLessonConductedStudentPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
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
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по дисциплинам");
                }

                var entity = _context.DisciplineLessonConductedStudents
                                .Include(x => x.DisciplineLessonConducted)
                                .Include(x => x.DisciplineLessonConducted.DisciplineLesson)
                                .Include(x => x.Student)
                                .FirstOrDefault(d => d.Id == model.Id && !d.IsDeleted);
                if (entity == null)
                {
                    return ResultService<DisciplineLessonConductedStudentViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<DisciplineLessonConductedStudentViewModel>.Success(ModelFactoryToViewModel.CreateDisciplineLessonConductedStudentViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<DisciplineLessonConductedStudentViewModel>.Error(ex, ResultServiceStatusCode.Error);
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
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по дисциплинам");
                }

                var entity = ModelFacotryFromBindingModel.CreateDisciplineLessonConductedStudent(model);

                _context.DisciplineLessonConductedStudents.Add(entity);
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

        public ResultService UpdateDisciplineLessonConductedStudent(DisciplineLessonConductedStudentSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по дисциплинам");
                }

                var entity = _context.DisciplineLessonConductedStudents.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity = ModelFacotryFromBindingModel.CreateDisciplineLessonConductedStudent(model, entity);

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

        public ResultService DeleteDisciplineLessonConductedStudent(DisciplineLessonConductedStudentGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по дисциплинам");
                }

                var entity = _context.DisciplineLessonConductedStudents.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
