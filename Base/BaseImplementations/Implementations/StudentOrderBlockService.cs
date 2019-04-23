using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;

namespace BaseImplementations.Implementations
{
    public class StudentOrderBlockService : IStudentOrderBlockService
    {
        private readonly IEducationDirectionService _serviceED;

        private readonly IStudentOrderService _serviceSO;

        private readonly AccessOperation _serviceOperation = AccessOperation.Приказы_студентов;

        private readonly string _entity = "Приказы студентов";

        public StudentOrderBlockService(IEducationDirectionService serviceED, IStudentOrderService serviceSO)
        {
            _serviceED = serviceED;
            _serviceSO = serviceSO;
        }

        public ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model)
        {
            return _serviceED.GetEducationDirections(model);
        }

        public ResultService<StudentOrderPageViewModel> GetStudentOrders(StudentOrderGetBindingModel model)
        {
            return _serviceSO.GetStudentOrders(model);
        }

        public ResultService<StudentOrderBlockPageViewModel> GetStudentOrderBlocks(StudentOrderBlockGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.StudentOrderBlocks.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.StudentOrderId.HasValue)
                    {
                        query = query.Where(x => x.StudentOrderId == model.StudentOrderId.Value);
                    }
                    if (model.StudentOrderType.HasValue)
                    {
                        query = query.Where(x => x.StudentOrderType == model.StudentOrderType.Value);
                    }

                    query = query.OrderBy(x => x.DateCreate);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.StudentOrder).Include(x => x.EducationDirection);

                    var result = new StudentOrderBlockPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ModelFactoryToViewModel.CreateStudentOrderBlockViewModel).ToList()
                    };

                    return ResultService<StudentOrderBlockPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<StudentOrderBlockPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<StudentOrderBlockViewModel> GetStudentOrderBlock(StudentOrderBlockGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                    var entity = context.StudentOrderBlocks
                                    .Include(x => x.StudentOrder)
                                    .Include(x => x.EducationDirection)
                                    .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<StudentOrderBlockViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<StudentOrderBlockViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<StudentOrderBlockViewModel>.Success(ModelFactoryToViewModel.CreateStudentOrderBlockViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<StudentOrderBlockViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateStudentOrderBlock(StudentOrderBlockSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ModelFacotryFromBindingModel.CreateStudentOrderBlock(model);

                    var exsistEntity = context.StudentOrderBlocks.FirstOrDefault(x => x.StudentOrderId == entity.StudentOrderId && x.StudentOrderType == entity.StudentOrderType);
                    if (exsistEntity == null)
                    {
                        context.StudentOrderBlocks.Add(entity);
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

        public ResultService UpdateStudentOrderBlock(StudentOrderBlockSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StudentOrderBlocks.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = ModelFacotryFromBindingModel.CreateStudentOrderBlock(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteStudentOrderBlock(StudentOrderBlockGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StudentOrderBlocks.FirstOrDefault(x => x.Id == model.Id);
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