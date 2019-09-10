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
    public class ExaminationTemplateTicketQuestionService : IExaminationTemplateTicketQuestionService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.СоставлениеЭкзаменов;

        private readonly string _entity = "Составление Экзаменов";

        private readonly IExaminationTemplateTicketService _serviceETT;

        public ExaminationTemplateTicketQuestionService(IExaminationTemplateTicketService serviceETT)
        {
            _serviceETT = serviceETT;
        }

        public ResultService<ExaminationTemplateTicketPageViewModel> GetExaminationTemplateTickets(ExaminationTemplateTicketGetBindingModel model)
        {
            return _serviceETT.GetExaminationTemplateTickets(model);
        }

        public ResultService<ExaminationTemplateTicketQuestionPageViewModel> GetExaminationTemplateTicketQuestions(ExaminationTemplateTicketQuestionGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.ExaminationTemplateTicketQuestions.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.ExaminationTemplateTicketId.HasValue)
                    {
                        query = query.Where(x => x.ExaminationTemplateTicketId == model.ExaminationTemplateTicketId);
                    }

                    if (model.Id.HasValue)
                    {
                        query = query.Where(x => x.Id == model.Id);
                    }

                    query = query.OrderBy(x => x.Id);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.ExaminationTemplateBlockQuestion).Include(x => x.ExaminationTemplateTicket);

                    var result = new ExaminationTemplateTicketQuestionPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ExaminationModelFactoryToViewModel.CreateExaminationTemplateTicketQuestionViewModel).ToList()
                    };

                    return ResultService<ExaminationTemplateTicketQuestionPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationTemplateTicketQuestionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<ExaminationTemplateTicketQuestionViewModel> GetExaminationTemplateTicketQuestion(ExaminationTemplateTicketQuestionGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ExaminationTemplateTicketQuestions
                                .Include(x => x.ExaminationTemplateBlockQuestion)
                                .Include(x => x.ExaminationTemplateTicket)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<ExaminationTemplateTicketQuestionViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<ExaminationTemplateTicketQuestionViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<ExaminationTemplateTicketQuestionViewModel>.Success(ExaminationModelFactoryToViewModel.CreateExaminationTemplateTicketQuestionViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationTemplateTicketQuestionViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateExaminationTemplateTicketQuestion(ExaminationTemplateTicketQuestionSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ExaminationModelFacotryFromBindingModel.CreateExaminationTemplateTicketQuestion(model);

                    var exsistEntity = context.ExaminationTemplateTicketQuestions.FirstOrDefault(x => x.ExaminationTemplateTicketId == entity.ExaminationTemplateTicketId && 
                                        x.ExaminationTemplateBlockQuestionId == model.ExaminationTemplateBlockQuestionId);
                    if (exsistEntity == null)
                    {
                        context.ExaminationTemplateTicketQuestions.Add(entity);
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

        public ResultService UpdateExaminationTemplateTicketQuestion(ExaminationTemplateTicketQuestionSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ExaminationTemplateTicketQuestions.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = ExaminationModelFacotryFromBindingModel.CreateExaminationTemplateTicketQuestion(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteExaminationTemplateTicketQuestion(ExaminationTemplateTicketQuestionGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                using (var transation = context.Database.BeginTransaction())
                {
                    var entity = context.ExaminationTemplates.FirstOrDefault(x => x.Id == model.Id);
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