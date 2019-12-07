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
    public class StatementService : IStatementService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.Ведомости;

        private readonly string _entity = "Ведомости";

        private readonly ILecturerService _serviceL;

        private readonly IDisciplineService _serviceD;

        private readonly IStudentGroupService _serviceSG;

        private readonly IAcademicYearService _serviceAY;

        public StatementService(ILecturerService serviceL, IDisciplineService serviceD, IStudentGroupService serviceSG, IAcademicYearService serviceAY)
        {
            _serviceL = serviceL;
            _serviceD = serviceD;
            _serviceSG = serviceSG;
            _serviceAY = serviceAY;
        }

        public ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model)
        {
            return _serviceL.GetLecturers(model);
        }

        public ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model)
        {
            return _serviceD.GetDisciplines(model);
        }

        public ResultService<StudentGroupPageViewModel> GetStudentGroups(StudentGroupGetBindingModel model)
        {
            return _serviceSG.GetStudentGroups(model);
        }

        public ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model)
        {
            return _serviceAY.GetAcademicYears(model);
        }

        public ResultService<StatementPageViewModel> GetStatements(StatementGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Statements.Where(x => !x.IsDeleted).AsQueryable();

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

                    query = query.Include(x => x.AcademicYear).Include(x => x.Lecturer).Include(x => x.StudentGroup).Include(x => x.Discipline);

                    var result = new StatementPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ExaminationModelFactoryToViewModel.CreateStatementViewModel).ToList()
                    };

                    return ResultService<StatementPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<StatementPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<StatementViewModel> GetStatement(StatementGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Statements
                                .Include(x => x.AcademicYear).Include(x => x.Lecturer).Include(x => x.StudentGroup).Include(x => x.Discipline)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<StatementViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<StatementViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<StatementViewModel>.Success(ExaminationModelFactoryToViewModel.CreateStatementViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<StatementViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateStatement(StatementSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ExaminationModelFacotryFromBindingModel.CreateStatement(model);

                    var exsistEntity = context.Statements.FirstOrDefault(x => x.AcademicYearId == entity.AcademicYearId && x.DisciplineId == model.DisciplineId && 
                                                            (x.IsMain == model.IsMain && x.IsMain));
                    if (exsistEntity == null)
                    {
                        context.Statements.Add(entity);
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

        public ResultService UpdateStatement(StatementSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Statements.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = ExaminationModelFacotryFromBindingModel.CreateStatement(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteStatement(StatementGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Statements.FirstOrDefault(x => x.Id == model.Id);
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