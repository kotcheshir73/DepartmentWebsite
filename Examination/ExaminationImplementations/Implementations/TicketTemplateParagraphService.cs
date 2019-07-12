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
    public class TicketTemplateParagraphService : ITicketTemplateParagraphService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.ШаблоныБилетов;

        private readonly string _entity = "Шаблоны Билетов";

        public ResultService<TicketTemplateParagraphPageViewModel> GetTicketTemplateParagraphs(TicketTemplateParagraphGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.TicketTemplateParagraphs.AsQueryable();

                    if (model.TicketTemplateBodyId.HasValue)
                    {
                        query = query.Where(x => x.TicketTemplateBodyId == model.TicketTemplateBodyId);
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

                    query = query
                        .Include(x => x.TicketTemplateParagraphProperties)
                        .Include(x => x.TicketTemplateParagraphRuns)
                        .Include("TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties");

                    var result = new TicketTemplateParagraphPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ExaminationModelFactoryToViewModel.CreateTicketTemplateParagraphViewModel).ToList()
                    };

                    return ResultService<TicketTemplateParagraphPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<TicketTemplateParagraphPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<TicketTemplateParagraphViewModel> GetTicketTemplateParagraph(TicketTemplateParagraphGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TicketTemplateParagraphs
                                .Include(x => x.TicketTemplateParagraphProperties)
                                .Include(x => x.TicketTemplateParagraphRuns)
                                .Include("TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties")
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<TicketTemplateParagraphViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    return ResultService<TicketTemplateParagraphViewModel>.Success(ExaminationModelFactoryToViewModel.CreateTicketTemplateParagraphViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<TicketTemplateParagraphViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateTicketTemplateParagraph(TicketTemplateParagraphSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ExaminationModelFacotryFromBindingModel.CreateTicketTemplateParagraph(model);

                    var exsistEntity = context.TicketTemplateParagraphs.Include(x => x.TicketTemplateParagraphProperties)
                                                    .FirstOrDefault(x => x.TicketTemplateBodyId == entity.TicketTemplateBodyId && x.Order == model.Order);
                    if (exsistEntity == null)
                    {
                        context.TicketTemplateParagraphs.Add(entity);
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

        public ResultService UpdateTicketTemplateParagraph(TicketTemplateParagraphSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TicketTemplateParagraphs.Include(x => x.TicketTemplateParagraphProperties).FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    entity = ExaminationModelFacotryFromBindingModel.CreateTicketTemplateParagraph(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteTicketTemplateParagraph(TicketTemplateParagraphGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TicketTemplateParagraphs
                        .Include(x => x.TicketTemplateParagraphProperties)
                        .Include(x => x.TicketTemplateParagraphRuns)
                        .Include("TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties")
                        .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    context.TicketTemplateParagraphs.Remove(entity);
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