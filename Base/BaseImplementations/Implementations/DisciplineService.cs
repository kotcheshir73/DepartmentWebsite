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
    public class DisciplineService : IDisciplineService
	{
		private readonly IDisciplineBlockService _serviceDB;

        private readonly AccessOperation _serviceOperation = AccessOperation.Дисциплины;

        private readonly string _entity = "Дисциплины";

        public DisciplineService(IDisciplineBlockService serviceDB)
		{
			_serviceDB = serviceDB;
        }
        
		public ResultService<DisciplineBlockPageViewModel> GetDisciplineBlocks(DisciplineBlockGetBindingModel model)
		{
			return _serviceDB.GetDisciplineBlocks(model);
		}

		public ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model)
		{
			try
            {
                if (!DepartmentUserManager.CheckAccess(model, _serviceOperation, AccessType.View, _entity))
                {
                    return ResultService<DisciplinePageViewModel>.Error(new MethodAccessException(DepartmentUserManager.ErrorMessage), ResultServiceStatusCode.Error);
                }

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Disciplines.Where(x => !x.IsDeleted).AsQueryable();

                    query = query.OrderBy(x => x.DisciplineBlock.DisciplineBlockOrder).ThenBy(x => x.DisciplineName);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.DisciplineBlock);

                    var result = new DisciplinePageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ModelFactoryToViewModel.CreateDisciplineViewModel).ToList()
                    };

                    return ResultService<DisciplinePageViewModel>.Success(result);
                }
			}
			catch (Exception ex)
			{
				return ResultService<DisciplinePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<DisciplineViewModel> GetDiscipline(DisciplineGetBindingModel bindingModel)
		{
			try
            {
                if (!DepartmentUserManager.CheckAccess(bindingModel, _serviceOperation, AccessType.View, _entity))
                {
                    return ResultService<DisciplineViewModel>.Error(new MethodAccessException(DepartmentUserManager.ErrorMessage), ResultServiceStatusCode.Error);
                }

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Disciplines
                                .Include(x => x.DisciplineBlock);
                    Discipline model = new Discipline();
                    if (bindingModel.Id.HasValue)
                    {
                        model = entity.FirstOrDefault(x => x.Id == bindingModel.Id);
                    }
                    else if(!string.IsNullOrEmpty(bindingModel.DisciplineName))
                    {
                        model = entity.FirstOrDefault(x => x.DisciplineName == bindingModel.DisciplineName);
                    }
                                
                    if (model == null)
                    {
                        return ResultService<DisciplineViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (model.IsDeleted)
                    {
                        return ResultService<DisciplineViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<DisciplineViewModel>.Success(ModelFactoryToViewModel.CreateDisciplineViewModel(model));
                }
			}
			catch (Exception ex)
			{
				return ResultService<DisciplineViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateDiscipline(DisciplineSetBindingModel model)
		{
			try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ModelFacotryFromBindingModel.CreateDiscipline(model);

                    var exsistEntity = context.Disciplines.FirstOrDefault(x => x.DisciplineName == entity.DisciplineName);
                    if (exsistEntity == null)
                    {
                        context.Disciplines.Add(entity);
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

		public ResultService UpdateDiscipline(DisciplineSetBindingModel model)
		{
			try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Disciplines.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity = ModelFacotryFromBindingModel.CreateDiscipline(model, entity);

                    context.SaveChanges();
                }

				return ResultService.Success();
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService DeleteDiscipline(DisciplineGetBindingModel model)
		{
			try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Disciplines.FirstOrDefault(x => x.Id == model.Id);
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