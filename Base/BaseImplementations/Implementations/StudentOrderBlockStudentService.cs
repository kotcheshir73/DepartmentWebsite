using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using DatabaseContext;
using Enums;
using Microsoft.EntityFrameworkCore;
using Models.Base;
using System;
using System.Linq;
using Tools;

namespace BaseImplementations.Implementations
{
    public class StudentOrderBlockStudentService : IStudentOrderBlockStudentService
    {
        private readonly IStudentOrderBlockService _serviceSOB;

        private readonly IStudentGroupService _serviceSG;

        private readonly IStudentService _serviceS;

        private readonly AccessOperation _serviceOperation = AccessOperation.Приказы_студентов;

        private readonly string _entity = "Приказы студентов";

        public StudentOrderBlockStudentService(IStudentOrderBlockService serviceSOB, IStudentGroupService serviceSG, IStudentService serviceS)
        {
            _serviceSOB = serviceSOB;
            _serviceSG = serviceSG;
            _serviceS = serviceS;
        }

        public ResultService<StudentOrderBlockPageViewModel> GetStudentOrderBlocks(StudentOrderBlockGetBindingModel model)
        {
            return _serviceSOB.GetStudentOrderBlocks(model);
        }

        public ResultService<StudentGroupPageViewModel> GetStudentGroups(StudentGroupGetBindingModel model)
        {
            return _serviceSG.GetStudentGroups(model);
        }

        public ResultService<StudentPageViewModel> GetStudents(StudentGetBindingModel model)
        {
            return _serviceS.GetStudents(model);
        }

        public ResultService<StudentOrderBlockStudentPageViewModel> GetStudentOrderBlockStudents(StudentOrderBlockStudentGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.StudentOrderBlockStudents.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.StudentOrderBlockId.HasValue)
                    {
                        query = query.Where(x => x.StudentOrderBlockId == model.StudentOrderBlockId.Value);
                    }
                    if (model.StudentOrderId.HasValue)
                    {
                        query = query.Where(x => x.StudentOrderBlock.StudentOrderId == model.StudentOrderId.Value);
                    }
                    if (model.StudentId.HasValue)
                    {
                        query = query.Where(x => x.StudentId == model.StudentId.Value);
                    }
                    if (model.StudentGroupFromId.HasValue)
                    {
                        query = query.Where(x => x.StudentGroupFromId == model.StudentGroupFromId.Value);
                    }
                    if (model.StudentGroupToId.HasValue)
                    {
                        query = query.Where(x => x.StudentGroupToId == model.StudentGroupToId.Value);
                    }

                    query = query.OrderBy(x => x.DateCreate);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.StudentOrderBlock).Include(x => x.StudentOrderBlock.StudentOrder).Include(x => x.Student);

                    var result = new StudentOrderBlockStudentPageViewModel
                    {
                        MaxCount = countPages,
                        List = query
                            .Select(x => ModelFactoryToViewModel.CreateStudentOrderBlockStudentViewModel(new StudentOrderBlockStudent
                            {
                                Id = x.Id,
                                DateCreate = x.DateCreate,
                                DateDelete = x.DateDelete,
                                IsDeleted = x.IsDeleted,
                                StudentId = x.StudentId,
                                Student = x.Student,
                                StudentOrderBlockId = x.StudentOrderBlockId,
                                StudentOrderBlock = x.StudentOrderBlock,
                                StudentGroupFromId = x.StudentGroupFromId,
                                StudentGroupFrom = x.StudentGroupFromId.HasValue ? context.StudentGroups.FirstOrDefault(y => y.Id == x.StudentGroupFromId) : null,
                                StudentGroupToId = x.StudentGroupToId,
                                StudentGroupTo = x.StudentGroupToId.HasValue ? context.StudentGroups.FirstOrDefault(y => y.Id == x.StudentGroupToId) : null,
                                Info = x.Info
                            })).ToList()
                    };

                    return ResultService<StudentOrderBlockStudentPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<StudentOrderBlockStudentPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<StudentOrderBlockStudentViewModel> GetStudentOrderBlockStudent(StudentOrderBlockStudentGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                    var entity = context.StudentOrderBlockStudents
                                    .Include(x => x.StudentOrderBlock)
                                    .Include(x => x.StudentOrderBlock.StudentOrder)
                                    .Include(x => x.Student)
                                    .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<StudentOrderBlockStudentViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<StudentOrderBlockStudentViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<StudentOrderBlockStudentViewModel>.Success(ModelFactoryToViewModel.CreateStudentOrderBlockStudentViewModel(new StudentOrderBlockStudent
                    {
                        Id = entity.Id,
                        DateCreate = entity.DateCreate,
                        DateDelete = entity.DateDelete,
                        IsDeleted = entity.IsDeleted,
                        StudentId = entity.StudentId,
                        Student = entity.Student,
                        StudentOrderBlockId = entity.StudentOrderBlockId,
                        StudentOrderBlock = entity.StudentOrderBlock,
                        StudentGroupFromId = entity.StudentGroupFromId,
                        StudentGroupFrom = entity.StudentGroupFromId.HasValue ? context.StudentGroups.FirstOrDefault(y => y.Id == entity.StudentGroupFromId) : null,
                        StudentGroupToId = entity.StudentGroupToId,
                        StudentGroupTo = entity.StudentGroupToId.HasValue ? context.StudentGroups.FirstOrDefault(y => y.Id == entity.StudentGroupToId) : null,
                        Info = entity.Info
                    }));
                }
            }
            catch (Exception ex)
            {
                return ResultService<StudentOrderBlockStudentViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateStudentOrderBlockStudent(StudentOrderBlockStudentSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ModelFacotryFromBindingModel.CreateStudentOrderBlockStudent(model);

                    var exsistEntity = context.StudentOrderBlockStudents.FirstOrDefault(x => x.StudentId == entity.StudentId && x.StudentOrderBlockId == entity.StudentOrderBlockId);
                    if (exsistEntity == null)
                    {
                        context.StudentOrderBlockStudents.Add(entity);
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

        public ResultService UpdateStudentOrderBlockStudent(StudentOrderBlockStudentSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StudentOrderBlockStudents.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = ModelFacotryFromBindingModel.CreateStudentOrderBlockStudent(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteStudentOrderBlockStudent(StudentOrderBlockStudentGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StudentOrderBlockStudents.FirstOrDefault(x => x.Id == model.Id);
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
                }

                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}