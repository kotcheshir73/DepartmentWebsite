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
using DepartmentModel.Enums;
using DepartmentService.Context;
using DepartmentService.IServices;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace DepartmentService.Services.StandartServices.LearningProgress
{
    public class DisciplineLessonStudentRecordService : IDisciplineLessonStudentRecordService
    {
        private readonly DepartmentDbContext _context;

        private readonly IDisciplineLessonRecordService _serviceDLR;

        private readonly IStudentService _serviceS;

        private readonly AccessOperation _serviceOperation = AccessOperation.Дисциплины;

        public DisciplineLessonStudentRecordService(DepartmentDbContext context, IDisciplineLessonRecordService serviceDLR, IStudentService serviceS)
        {
            _context = context;
            _serviceDLR = serviceDLR;
            _serviceS = serviceS;
        }

        public ResultService<DisciplineLessonRecordPageViewModel> GetDisciplineLessonRecords(DisciplineLessonRecordGetBindingModel model)
        {
            return _serviceDLR.GetDisciplineLessonRecords(model);
        }
        
        public ResultService<StudentPageViewModel> GetStudents(StudentGetBindingModel model)
        {
            return _serviceS.GetStudents(model);
        }

        public ResultService<DisciplineLessonStudentRecordPageViewModel> GetDisciplineLessonStudentRecords(DisciplineLessonStudentRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по дисциплинам");
                }

                int countPages = 0;
                var query = _context.DisciplineLessonStudentRecords.Where(d => !d.IsDeleted).AsQueryable();

                if (model.DisciplineLessonRecordId.HasValue)
                {
                    query = query.Where(x => x.DisciplineLessonRecordId == model.DisciplineLessonRecordId);
                }
                if (model.StudentId.HasValue)
                {
                    query = query.Where(x => x.StudentId == model.StudentId);
                }
                if (!string.IsNullOrEmpty(model.Comment))
                {
                    query = query.Where(x => x.Comment == model.Comment);
                }
                if (model.Id.HasValue)
                {
                    query = query.Where(x => x.Id == model.Id);
                }
                // тут пока нету балла и статуса

                query = query.OrderBy(d => d.DisciplineLessonRecord).ThenBy(x => x.Student.LastName);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query.Include(x => x.DisciplineLessonRecord).Include(x => x.Student);

                var result = new DisciplineLessonStudentRecordPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateDisciplineLessonStudentRecordViewModel).ToList()
                };
                return ResultService<DisciplineLessonStudentRecordPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<DisciplineLessonStudentRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineLessonStudentRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<DisciplineLessonStudentRecordViewModel> GetDisciplineLessonStudentRecord(DisciplineLessonStudentRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по дисциплинам");
                }

                var entity = _context.DisciplineLessonStudentRecords
                                .FirstOrDefault(d => d.Id == model.Id && !d.IsDeleted);
                if (entity == null)
                {
                    return ResultService<DisciplineLessonStudentRecordViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<DisciplineLessonStudentRecordViewModel>.Success(ModelFactoryToViewModel.CreateDisciplineLessonStudentRecordViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<DisciplineLessonStudentRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineLessonStudentRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateDisciplineLessonStudentRecord(DisciplineLessonStudentRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по дисциплинам");
                }

                var entity = ModelFacotryFromBindingModel.CreateDisciplineLessonStudentRecord(model);

                _context.DisciplineLessonStudentRecords.Add(entity);
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

        public ResultService UpdateDisciplineLessonStudentRecord(DisciplineLessonStudentRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по дисциплинам");
                }

                var entity = _context.DisciplineLessonStudentRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity = ModelFacotryFromBindingModel.CreateDisciplineLessonStudentRecord(model, entity);

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

        public ResultService DeleteDisciplineLessonStudentRecord(DisciplineLessonStudentRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по дисциплинам");
                }

                var entity = _context.DisciplineLessonStudentRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
