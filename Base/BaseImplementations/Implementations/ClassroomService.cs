using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using DatabaseContext;
using Enums;
using System;
using System.Linq;
using Tools;

namespace BaseImplementations.Implementations
{
    public class ClassroomService : IClassroomService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.Аудитории;

        private readonly string _entity = "Аудитории";

        public ResultService<ClassroomPageViewModel> GetClassrooms(ClassroomGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Classrooms.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.NotUseInSchedule.HasValue)
                    {
                        query = query.Where(x => x.NotUseInSchedule == model.NotUseInSchedule.Value);
                    }

                    query = query.OrderBy(x => x.Number);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    var result = new ClassroomPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ModelFactoryToViewModel.CreateClassroomViewModel).ToList()
                    };

                    return ResultService<ClassroomPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<ClassroomPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<ClassroomViewModel> GetClassroom(ClassroomGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                    var entity = context.Classrooms
                                    .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<ClassroomViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if(entity.IsDeleted)
                    {
                        return ResultService<ClassroomViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<ClassroomViewModel>.Success(ModelFactoryToViewModel.CreateClassroomViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<ClassroomViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateClassroom(ClassroomSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ModelFacotryFromBindingModel.CreateClassroom(model);

                    var exsistEntity = context.Classrooms.FirstOrDefault(x => x.Number == entity.Number);
                    if(exsistEntity == null)
                    {
                        context.Classrooms.Add(entity);
                        context.SaveChanges();
                        return ResultService.Success(entity.Id);
                    }
                    else
                    {
                        if(exsistEntity.IsDeleted)
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

        public ResultService UpdateClassroom(ClassroomSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Classrooms.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = ModelFacotryFromBindingModel.CreateClassroom(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteClassroom(ClassroomGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Classrooms.FirstOrDefault(x => x.Id == model.Id);
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