﻿using Enums;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using ExaminationInterfaces.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;

namespace ExaminationImplementations.Implementations
{
    public class TicketTemplateService : ITicketTemplateService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.ШаблоныБилетов;

        private readonly string _entity = "Шаблоны Билетов";

        private readonly ITicketTemplateBodyService _bodyService;

        public TicketTemplateService(ITicketTemplateBodyService bodyService)
        {
            _bodyService = bodyService;
        }

        public ResultService<TicketTemplatePageViewModel> GetTicketTemplates(TicketTemplateGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.TicketTemplates.AsQueryable();

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

                    query = query
                        .Include(x => x.TicketTemplateBody)
                        .Include(x => x.TicketTemplateBody.TicketTemplateBodyProperties)
                        .Include(x => x.TicketTemplateBody.TicketTemplateParagraphs)
                        .Include("TicketTemplateBody.TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                        .Include("TicketTemplateBody.TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                        .Include("TicketTemplateBody.TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties");

                    var result = new TicketTemplatePageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ExaminationModelFactoryToViewModel.CreateTicketTemplate).ToList()
                    };

                    return ResultService<TicketTemplatePageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<TicketTemplatePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<TicketTemplateViewModel> GetTicketTemplate(TicketTemplateGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TicketTemplates
                                    .Include(x => x.TicketTemplateBody)
                                    .Include(x => x.TicketTemplateBody.TicketTemplateBodyProperties)
                                    .Include(x => x.TicketTemplateBody.TicketTemplateParagraphs)
                                    .Include("TicketTemplateBody.TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                                    .Include("TicketTemplateBody.TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                                    .Include("TicketTemplateBody.TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties")
                                    .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<TicketTemplateViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    return ResultService<TicketTemplateViewModel>.Success(ExaminationModelFactoryToViewModel.CreateTicketTemplate(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<TicketTemplateViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateTicketTemplate(TicketTemplateSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ExaminationModelFacotryFromBindingModel.CreateTicketTemplate(model);

                    var exsistEntity = context.TicketTemplates.FirstOrDefault(x => x.Id == entity.Id);
                    if (exsistEntity == null)
                    {
                        context.TicketTemplates.Add(entity);
                        context.SaveChanges();
                        return ResultService.Success(entity.Id);
                    }
                    else
                    {
                        return ResultService.Error("Error:", "Элемент уже существует", ResultServiceStatusCode.ExsistItem);
                    }
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService UpdateTicketTemplate(TicketTemplateSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TicketTemplates.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    entity = ExaminationModelFacotryFromBindingModel.CreateTicketTemplate(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteTicketTemplate(TicketTemplateGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                using (var transation = context.Database.BeginTransaction())
                {
                    var entity = context.TicketTemplates.Include(x => x.TicketTemplateBody).FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    if (entity.TicketTemplateBody != null)
                    {
                        _bodyService.DeleteTicketTemplateBody(new TicketTemplateBodyGetBindingModel { Id = entity.TicketTemplateBody.Id });
                    }

                    context.TicketTemplates.Remove(entity);
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