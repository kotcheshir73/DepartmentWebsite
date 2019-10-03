using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using DatabaseContext;
using Enums;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using ExaminationInterfaces.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;

namespace ExaminationImplementations.Implementations
{
    public class ExaminationListService : IExaminationListService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.Ведомости;

        private readonly string _entity = "Ведомости";

        private readonly ILecturerService _serviceL;

        private readonly IDisciplineService _serviceD;

        private readonly IStudentService _serviceS;

        private readonly IStudentGroupService _serviceSG;

        private readonly IAcademicYearService _serviceAY;

        public ExaminationListService(ILecturerService serviceL, IDisciplineService serviceD, IStudentService serviceS, IStudentGroupService serviceSG, IAcademicYearService serviceAY)
        {
            _serviceL = serviceL;
            _serviceD = serviceD;
            _serviceS = serviceS;
            _serviceSG = serviceSG;
            _serviceAY = serviceAY;
        }

        public ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model)
        {
            return _serviceD.GetDisciplines(model);
        }

        public ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model)
        {
            return _serviceL.GetLecturers(model);
        }

        public ResultService<StudentGroupPageViewModel> GetStudentGroups(StudentGroupGetBindingModel model)
        {
            return _serviceSG.GetStudentGroups(model);
        }

        public ResultService<StudentPageViewModel> GetStudents(StudentGetBindingModel model)
        {
            return _serviceS.GetStudents(model);
        }

        public ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model)
        {
            return _serviceAY.GetAcademicYears(model);
        }

        public ResultService<ExaminationListPageViewModels> GetExaminationLists(ExaminationListGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.ExaminationLists.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.LecturerId.HasValue)
                    {
                        query = query.Where(x => x.LecturerId == model.LecturerId);
                    }
                    if (model.DisciplineId.HasValue)
                    {
                        query = query.Where(x => x.DisciplineId == model.DisciplineId);
                    }
                    if (model.StudentGroupId.HasValue)
                    {
                        query = query.Where(x => x.StudentGroupId == model.LecturerId);
                    }
                    if (model.StudentId.HasValue)
                    {
                        query = query.Where(x => x.StudentId == model.StudentId);
                    }
                    if (model.AcademicYearId.HasValue)
                    {
                        query = query.Where(x => x.AcademicYearId == model.AcademicYearId);
                    }

                    query = query.OrderBy(x => x.Semester).ThenBy(x => x.Discipline.DisciplineName);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.AcademicYear).Include(x => x.Lecturer).Include(x => x.StudentGroup).Include(x => x.Discipline).Include(x => x.Student);

                    var result = new ExaminationListPageViewModels
                    {
                        MaxCount = countPages,
                        List = query.Select(ExaminationModelFactoryToViewModel.CreateExaminationListViewModel).ToList()
                    };

                    return ResultService<ExaminationListPageViewModels>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationListPageViewModels>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<ExaminationListViewModel> GetExaminationList(ExaminationListGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ExaminationLists
                                .Include(x => x.AcademicYear).Include(x => x.Lecturer).Include(x => x.StudentGroup).Include(x => x.Discipline).Include(x => x.Student)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<ExaminationListViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<ExaminationListViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<ExaminationListViewModel>.Success(ExaminationModelFactoryToViewModel.CreateExaminationListViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationListViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateExaminationList(ExaminationListSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ExaminationModelFacotryFromBindingModel.CreateExaminationList(model);

                    var exsistEntity = context.ExaminationLists.FirstOrDefault(x => x.AcademicYearId == entity.AcademicYearId && x.Number == model.Number);
                    if (exsistEntity == null)
                    {
                        context.ExaminationLists.Add(entity);
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

        public ResultService UpdateExaminationList(ExaminationListSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ExaminationLists.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = ExaminationModelFacotryFromBindingModel.CreateExaminationList(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteExaminationList(ExaminationListGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ExaminationLists.FirstOrDefault(x => x.Id == model.Id);
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