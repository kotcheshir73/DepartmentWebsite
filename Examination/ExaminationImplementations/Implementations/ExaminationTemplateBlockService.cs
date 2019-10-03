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
    public class ExaminationTemplateBlockService : IExaminationTemplateBlockService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.СоставлениеЭкзаменов;

        private readonly string _entity = "Составление Экзаменов";

        private readonly IExaminationTemplateService _serviceET;

        public ExaminationTemplateBlockService(IExaminationTemplateService serviceET)
        {
            _serviceET = serviceET;
        }

        public ResultService<ExaminationTemplatePageViewModel> GetExaminationTemplates(ExaminationTemplateGetBindingModel model)
        {
            return _serviceET.GetExaminationTemplates(model);
        }

        public ResultService<ExaminationTemplateBlockPageViewModel> GetExaminationTemplateBlocks(ExaminationTemplateBlockGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.ExaminationTemplateBlocks.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.ExaminationTemplateId.HasValue)
                    {
                        query = query.Where(x => x.ExaminationTemplateId == model.ExaminationTemplateId);
                    }

                    if (model.Id.HasValue)
                    {
                        query = query.Where(x => x.Id == model.Id);
                    }

                    query = query.OrderBy(x => x.BlockName);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.ExaminationTemplate);

                    var result = new ExaminationTemplateBlockPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ExaminationModelFactoryToViewModel.CreateExaminationTemplateBlockViewModel).ToList()
                    };

                    return ResultService<ExaminationTemplateBlockPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationTemplateBlockPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<ExaminationTemplateBlockViewModel> GetExaminationTemplateBlock(ExaminationTemplateBlockGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ExaminationTemplateBlocks
                                .Include(apr => apr.ExaminationTemplate)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<ExaminationTemplateBlockViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<ExaminationTemplateBlockViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<ExaminationTemplateBlockViewModel>.Success(ExaminationModelFactoryToViewModel.CreateExaminationTemplateBlockViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationTemplateBlockViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateExaminationTemplateBlock(ExaminationTemplateBlockSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ExaminationModelFacotryFromBindingModel.CreateExaminationTemplateBlock(model);

                    var exsistEntity = context.ExaminationTemplateBlocks.FirstOrDefault(x => x.ExaminationTemplateId == entity.ExaminationTemplateId && x.BlockName == model.BlockName);
                    if (exsistEntity == null)
                    {
                        context.ExaminationTemplateBlocks.Add(entity);
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

        public ResultService UpdateExaminationTemplateBlock(ExaminationTemplateBlockSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ExaminationTemplateBlocks.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = ExaminationModelFacotryFromBindingModel.CreateExaminationTemplateBlock(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteExaminationTemplateBlock(ExaminationTemplateBlockGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                using (var transation = context.Database.BeginTransaction())
                {
                    var entity = context.ExaminationTemplateBlocks.FirstOrDefault(x => x.Id == model.Id);
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

                    var examinationTemplateBlockQuestions = context.ExaminationTemplateBlockQuestions.Where(x => x.ExaminationTemplateBlockId == entity.Id);
                    foreach (var examinationTemplateBlockQuestion in examinationTemplateBlockQuestions)
                    {
                        examinationTemplateBlockQuestion.IsDeleted = true;
                        examinationTemplateBlockQuestion.DateDelete = DateTime.Now;
                        context.SaveChanges();

                        var examinationTemplateTicketQuestions = context.ExaminationTemplateTicketQuestions.Where(x => x.ExaminationTemplateBlockQuestionId == examinationTemplateBlockQuestion.Id);
                        foreach (var examinationTemplateTicketQuestion in examinationTemplateTicketQuestions)
                        {
                            examinationTemplateTicketQuestion.IsDeleted = true;
                            examinationTemplateTicketQuestion.DateDelete = DateTime.Now;
                            context.SaveChanges();
                        }
                    }

                    transation.Commit();

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