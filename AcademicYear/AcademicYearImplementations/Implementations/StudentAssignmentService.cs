using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using DatabaseContext;
using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;

namespace AcademicYearImplementations.Implementations
{
    public class StudentAssignmentService : IStudentAssignmentService
    {
        private readonly IAcademicYearService _serviceAY;

        private readonly IEducationDirectionService _serviceED;

        private readonly ILecturerService _serviceL;

        private readonly AccessOperation _serviceOperation = AccessOperation.Расчет_штатов;

        private readonly string _entity = "Распределение по научным руководителям";

        public StudentAssignmentService(IAcademicYearService serviceAY, IEducationDirectionService serviceED, ILecturerService serviceL)
        {
            _serviceAY = serviceAY;
            _serviceED = serviceED;
            _serviceL = serviceL;
        }

        public ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model)
        {
            return _serviceAY.GetAcademicYears(model);
        }

        public ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model)
        {
            return _serviceED.GetEducationDirections(model);
        }

        public ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model)
        {
            return _serviceL.GetLecturers(model);
        }

        public ResultService<StudentAssignmentPageViewModel> GetStudentAssignments(StudentAssignmentGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.StudentAssignments
                        .Where(x => !x.IsDeleted)
                        .Include(x => x.AcademicYear)
                        .Include(x => x.EducationDirection)
                        .Include(x => x.Lecturer)
                        .AsQueryable();

                    if (model.AcademicYearId.HasValue)
                    {
                        query = query.Where(x => x.AcademicYearId == model.AcademicYearId);
                    }

                    if (model.EducationDirectionId.HasValue)
                    {
                        query = query.Where(x => x.EducationDirectionId == model.EducationDirectionId);
                    }

                    if (model.LecturerId.HasValue)
                    {
                        query = query.Where(x => x.LecturerId == model.LecturerId);
                    }

                    query = query
                        .OrderBy(x => x.AcademicYear.Title)
                        .ThenBy(x => x.EducationDirection.Cipher)
                        .ThenBy(x => x.Lecturer.LastName);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    var result = new StudentAssignmentPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(AcademicYearModelFactoryToViewModel.CreateStudentAssignmentViewModel).ToList()
                    };

                    return ResultService<StudentAssignmentPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<StudentAssignmentPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<StudentAssignmentViewModel> GetStudentAssignment(StudentAssignmentGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StudentAssignments
                                .Include(x => x.AcademicYear)
                                .Include(x => x.EducationDirection)
                                .Include(x => x.Lecturer)
                                .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService<StudentAssignmentViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<StudentAssignmentViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<StudentAssignmentViewModel>.Success(AcademicYearModelFactoryToViewModel.CreateStudentAssignmentViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<StudentAssignmentViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateStudentAssignment(StudentAssignmentSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AcademicYearModelFacotryFromBindingModel.CreateStudentAssignment(model);

                    var exsistEntity = context.StudentAssignments
                        .FirstOrDefault(x => x.AcademicYearId == entity.AcademicYearId && x.EducationDirectionId == entity.EducationDirectionId && x.LecturerId == entity.LecturerId);

                    if (exsistEntity == null)
                    {
                        context.StudentAssignments.Add(entity);
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

        public ResultService UpdateStudentAssignment(StudentAssignmentSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StudentAssignments.FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = AcademicYearModelFacotryFromBindingModel.CreateStudentAssignment(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteStudentAssignment(StudentAssignmentGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StudentAssignments.FirstOrDefault(x => x.Id == model.Id);

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
