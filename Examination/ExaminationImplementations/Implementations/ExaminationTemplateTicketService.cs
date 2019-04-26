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
    public class ExaminationTemplateTicketService : IExaminationTemplateTicketService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.СоставлениеЭкзаменов;

        private readonly string _entity = "Составление Экзаменов";

        private readonly IExaminationTemplateService _serviceET;

        public ExaminationTemplateTicketService(IExaminationTemplateService serviceET)
        {
            _serviceET = serviceET;
        }

        public ResultService<ExaminationTemplatePageViewModel> GetExaminationTemplates(ExaminationTemplateGetBindingModel model)
        {
            return _serviceET.GetExaminationTemplates(model);
        }

        public ResultService<ExaminationTemplateTicketPageViewModel> GetExaminationTemplateTickets(ExaminationTemplateTicketGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.ExaminationTemplateTickets.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.ExaminationTemplateId.HasValue)
                    {
                        query = query.Where(x => x.ExaminationTemplateId == model.ExaminationTemplateId);
                    }

                    if (model.Id.HasValue)
                    {
                        query = query.Where(x => x.Id == model.Id);
                    }

                    query = query.OrderBy(x => x.TicketNumber);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.ExaminationTemplate);

                    var result = new ExaminationTemplateTicketPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ExaminationModelFactoryToViewModel.CreateExaminationTemplateTicketViewModel).ToList()
                    };

                    return ResultService<ExaminationTemplateTicketPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationTemplateTicketPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<ExaminationTemplateTicketViewModel> GetExaminationTemplateTicket(ExaminationTemplateTicketGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ExaminationTemplateTickets
                                .Include(x => x.ExaminationTemplate)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<ExaminationTemplateTicketViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<ExaminationTemplateTicketViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<ExaminationTemplateTicketViewModel>.Success(ExaminationModelFactoryToViewModel.CreateExaminationTemplateTicketViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationTemplateTicketViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateExaminationTemplateTicket(ExaminationTemplateTicketSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ExaminationModelFacotryFromBindingModel.CreateExaminationTemplateTicket(model);

                    var exsistEntity = context.ExaminationTemplateTickets.FirstOrDefault(x => x.ExaminationTemplateId == entity.ExaminationTemplateId && x.TicketNumber == model.TicketNumber);
                    if (exsistEntity == null)
                    {
                        context.ExaminationTemplateTickets.Add(entity);
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

        public ResultService UpdateExaminationTemplateTicket(ExaminationTemplateTicketSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ExaminationTemplateTickets.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = ExaminationModelFacotryFromBindingModel.CreateExaminationTemplateTicket(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteExaminationTemplateTicket(ExaminationTemplateTicketGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                using (var transation = context.Database.BeginTransaction())
                {
                    var entity = context.ExaminationTemplateTickets.FirstOrDefault(x => x.Id == model.Id);
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

                    var examinationTemplateTicketQuestions = context.ExaminationTemplateTicketQuestions.Where(x => x.ExaminationTemplateTicketId == entity.Id);
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