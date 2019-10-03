using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using DatabaseContext;
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
    public class DisciplineStudentRecordService : IDisciplineStudentRecordService
    {
        private readonly IDisciplineService _serviceD;

        private readonly IStudentService _serviceS;

        private readonly AccessOperation _serviceOperation = AccessOperation.Дисциплины;

        private readonly string _entity = "Дисциплины";

        public DisciplineStudentRecordService(IDisciplineService serviceD, IStudentService serviceS)
        {
            _serviceD = serviceD;
            _serviceS = serviceS;
        }

        public ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model)
        {
            return _serviceD.GetDisciplines(model);
        }

        public ResultService<StudentGroupPageViewModel> GetStudentGroups(StudentGroupGetBindingModel model)
        {
            return _serviceS.GetStudentGroups(model);
        }

        public ResultService<StudentPageViewModel> GetStudents(StudentGetBindingModel model)
        {
            return _serviceS.GetStudents(model);
        }

        public ResultService<DisciplineStudentRecordPageViewModel> GetDisciplineStudentRecords(DisciplineStudentRecordGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.DisciplineStudentRecords.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.DisciplineId.HasValue)
                    {
                        query = query.Where(x => x.DisciplineId == model.DisciplineId);
                    }
                    if (model.StudentGroupId.HasValue)
                    {
                        query = query.Where(x => x.Student.StudentGroupId == model.StudentGroupId);
                    }
                    if (model.StudentId.HasValue)
                    {
                        query = query.Where(x => x.StudentId == model.StudentId);
                    }
                    if (!string.IsNullOrEmpty(model.Semester))
                    {
                        Semesters sem = (Semesters)Enum.Parse(typeof(Semesters), model.Semester);

                        query = query.Where(x => x.Semester == sem);
                    }
                    if (model.Id.HasValue)
                    {
                        query = query.Where(x => x.Id == model.Id);
                    }

                    query = query.OrderBy(d => d.Semester).ThenBy(x => x.Discipline.DisciplineName).ThenBy(x => x.Student.LastName).ThenBy(x => x.Student.FirstName);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.Discipline).Include(x => x.Student).Include(x => x.Student.StudentGroup);

                    var result = new DisciplineStudentRecordPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(LearningProgressModelFactoryToViewModel.CreateDisciplineStudentRecordViewModel).ToList()
                    };

                    return ResultService<DisciplineStudentRecordPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineStudentRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<DisciplineStudentRecordViewModel> GetDisciplineStudentRecord(DisciplineStudentRecordGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineStudentRecords
                                .Include(x => x.Discipline)
                                .Include(x => x.Student)
                                .Include(x => x.Student.StudentGroup)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<DisciplineStudentRecordViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<DisciplineStudentRecordViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<DisciplineStudentRecordViewModel>.Success(LearningProgressModelFactoryToViewModel.CreateDisciplineStudentRecordViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineStudentRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateDisciplineStudentRecord(DisciplineStudentRecordSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = LearningProgressModelFacotryFromBindingModel.CreateDisciplineStudentRecord(model);

                    var exsistEntity = context.DisciplineStudentRecords.FirstOrDefault(x => x.DisciplineId == entity.DisciplineId && 
                                                    x.Semester == (Semesters)Enum.Parse(typeof(Semesters), model.Semester) && x.SubGroup == model.SubGroup && x.Variant == model.Variant);
                    if (exsistEntity == null)
                    {
                        context.DisciplineStudentRecords.Add(entity);
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

        public ResultService UpdateDisciplineStudentRecord(DisciplineStudentRecordSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineStudentRecords.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = LearningProgressModelFacotryFromBindingModel.CreateDisciplineStudentRecord(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteDisciplineStudentRecord(DisciplineStudentRecordGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineStudentRecords.FirstOrDefault(x => x.Id == model.Id);
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