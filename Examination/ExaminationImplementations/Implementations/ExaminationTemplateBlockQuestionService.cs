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
    public class ExaminationTemplateBlockQuestionService : IExaminationTemplateBlockQuestionService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.СоставлениеЭкзаменов;

        private readonly string _entity = "Составление Экзаменов";

        private readonly IExaminationTemplateBlockService _serviceETB;

        public ExaminationTemplateBlockQuestionService(IExaminationTemplateBlockService serviceETB)
        {
            _serviceETB = serviceETB;
        }

        public ResultService<ExaminationTemplateBlockPageViewModel> GetExaminationTemplateBlocks(ExaminationTemplateBlockGetBindingModel model)
        {
            return _serviceETB.GetExaminationTemplateBlocks(model);
        }

        public ResultService<ExaminationTemplateBlockQuestionPageViewModel> GetExaminationTemplateBlockQuestions(ExaminationTemplateBlockQuestionGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.ExaminationTemplateBlockQuestions.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.ExaminationTemplateBlockId.HasValue)
                    {
                        query = query.Where(x => x.ExaminationTemplateBlockId == model.ExaminationTemplateBlockId);
                    }

                    if (model.Id.HasValue)
                    {
                        query = query.Where(x => x.Id == model.Id);
                    }

                    query = query.OrderBy(x => x.QuestionNumber);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.ExaminationTemplateBlock);

                    var result = new ExaminationTemplateBlockQuestionPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ExaminationModelFactoryToViewModel.CreateExaminationTemplateBlockQuestionViewModel).ToList()
                    };

                    return ResultService<ExaminationTemplateBlockQuestionPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationTemplateBlockQuestionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<ExaminationTemplateBlockQuestionViewModel> GetExaminationTemplateBlockQuestion(ExaminationTemplateBlockQuestionGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ExaminationTemplateBlockQuestions
                                .Include(x => x.ExaminationTemplateBlock)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<ExaminationTemplateBlockQuestionViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<ExaminationTemplateBlockQuestionViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<ExaminationTemplateBlockQuestionViewModel>.Success(ExaminationModelFactoryToViewModel.CreateExaminationTemplateBlockQuestionViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationTemplateBlockQuestionViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateExaminationTemplateBlockQuestion(ExaminationTemplateBlockQuestionSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ExaminationModelFacotryFromBindingModel.CreateExaminationTemplateBlockQuestion(model);

                    var exsistEntity = context.ExaminationTemplateBlockQuestions.FirstOrDefault(x => x.ExaminationTemplateBlockId == entity.ExaminationTemplateBlockId && 
                                        x.QuestionNumber == model.QuestionNumber);
                    if (exsistEntity == null)
                    {
                        context.ExaminationTemplateBlockQuestions.Add(entity);
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

        public ResultService UpdateExaminationTemplateBlockQuestion(ExaminationTemplateBlockQuestionSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ExaminationTemplateBlockQuestions.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = ExaminationModelFacotryFromBindingModel.CreateExaminationTemplateBlockQuestion(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteExaminationTemplateBlockQuestion(ExaminationTemplateBlockQuestionGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                using (var transation = context.Database.BeginTransaction())
                {
                    var entity = context.ExaminationTemplateBlockQuestions.FirstOrDefault(x => x.Id == model.Id);
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

                    var examinationTemplateTicketQuestions = context.ExaminationTemplateTicketQuestions.Where(x => x.ExaminationTemplateBlockQuestionId == entity.Id);
                    foreach (var examinationTemplateTicketQuestion in examinationTemplateTicketQuestions)
                    {
                        examinationTemplateTicketQuestion.IsDeleted = true;
                        examinationTemplateTicketQuestion.DateDelete = DateTime.Now;
                        context.SaveChanges();
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