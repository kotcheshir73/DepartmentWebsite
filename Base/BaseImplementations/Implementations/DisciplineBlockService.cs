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
    public class DisciplineBlockService : IDisciplineBlockService
	{
		private readonly AccessOperation _serviceOperation = AccessOperation.Дисциплины;

        private readonly string _entity = "Блоки дисциплин";

        public ResultService<DisciplineBlockPageViewModel> GetDisciplineBlocks(DisciplineBlockGetBindingModel model)
		{
			try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.DisciplineBlocks.Where(x => !x.IsDeleted).AsQueryable();

                    query = query.OrderBy(x => x.DisciplineBlockOrder);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    var result = new DisciplineBlockPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ModelFactoryToViewModel.CreateDisciplineBlockViewModel).ToList()
                    };

                    return ResultService<DisciplineBlockPageViewModel>.Success(result);
                }
			}
			catch (Exception ex)
			{
				return ResultService<DisciplineBlockPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<DisciplineBlockViewModel> GetDisciplineBlock(DisciplineBlockGetBindingModel model)
		{
			try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                    var entity = context.DisciplineBlocks
                                    .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<DisciplineBlockViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<DisciplineBlockViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<DisciplineBlockViewModel>.Success(ModelFactoryToViewModel.CreateDisciplineBlockViewModel(entity));
                }
			}
			catch (Exception ex)
			{
				return ResultService<DisciplineBlockViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateDisciplineBlock(DisciplineBlockSetBindingModel model)
		{
			try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                    var entity = ModelFacotryFromBindingModel.CreateDisciplineBlock(model);

                    var exsistEntity = context.DisciplineBlocks.FirstOrDefault(x => x.Title == entity.Title);
                    if (exsistEntity == null)
                    {
                        context.DisciplineBlocks.Add(entity);
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

		public ResultService UpdateDisciplineBlock(DisciplineBlockSetBindingModel model)
		{
			try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                    var entity = context.DisciplineBlocks.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = ModelFacotryFromBindingModel.CreateDisciplineBlock(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService DeleteDisciplineBlock(DisciplineBlockGetBindingModel model)
		{
			try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                    var entity = context.DisciplineBlocks.FirstOrDefault(x => x.Id == model.Id);
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