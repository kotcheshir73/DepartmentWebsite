using DatabaseContext;
using Enums;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using ExaminationInterfaces.ViewModels;
using Microsoft.EntityFrameworkCore;
using Models.Examination;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketServiceImplementations.Helpers;
using Tools;

namespace ExaminationImplementations.Implementations
{
    public class TicketTemplateService : ITicketTemplateService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.СоставлениеЭкзаменов;

        private readonly string _entity = "Составление Экзаменов";

        private readonly IExaminationTemplateService _serviceET;

        public TicketTemplateService(IExaminationTemplateService serviceET)
        {
            _serviceET = serviceET;
        }

        public ResultService<ExaminationTemplatePageViewModel> GetExaminationTemplates(ExaminationTemplateGetBindingModel model)
        {
            return _serviceET.GetExaminationTemplates(model);
        }

        public ResultService<TicketTemplatePageViewModel> GetTicketTemplates(TicketTemplateGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.TicketTemplates.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.ExaminationTemplateId.HasValue)
                    {
                        query = query.Where(x => x.ExaminationTemplateId == model.ExaminationTemplateId);
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

                    query = query.Include(x => x.ExaminationTemplate)
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
                                    .Include(x => x.ExaminationTemplate)
                                    .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<TicketTemplateViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<TicketTemplateViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    //entity.TicketTemplateBodies = new List<TicketTemplateBody> { TicketBodyGet.GetBody(entity.Id) };

                    return ResultService<TicketTemplateViewModel>.Success(ExaminationModelFactoryToViewModel.CreateTicketTemplate(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<TicketTemplateViewModel>.Error(ex, ResultServiceStatusCode.Error);
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
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
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
            {
                try
                {
                    DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                    using (var context = DepartmentUserManager.GetContext)
                    using (var transation = context.Database.BeginTransaction())
                    {
                        var entity = context.TicketTemplates.FirstOrDefault(x => x.Id == model.Id);
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

                        var ticketTemplaetBodies = context.TicketTemplateBodies.Where(x => x.TicketTemplateId == entity.Id);
                        foreach (var ticketTemplaetBody in ticketTemplaetBodies)
                        {
                            ticketTemplaetBody.IsDeleted = true;
                            ticketTemplaetBody.DateDelete = DateTime.Now;
                            context.SaveChanges();

                            var ticketTemplateTables = context.TicketTemplateTables.Where(x => x.TicketTemplateBodyId == ticketTemplaetBody.Id);
                            foreach (var ticketTemplateTable in ticketTemplateTables)
                            {
                                ticketTemplateTable.IsDeleted = true;
                                ticketTemplateTable.DateDelete = DateTime.Now;
                                context.SaveChanges();

                                var ticketTemplateTableRows = context.TicketTemplateTableRows.Where(x => x.TicketTemplateTableId == ticketTemplateTable.Id);
                                foreach (var ticketTemplateTableRow in ticketTemplateTableRows)
                                {
                                    ticketTemplateTableRow.IsDeleted = true;
                                    ticketTemplateTableRow.DateDelete = DateTime.Now;
                                    context.SaveChanges();

                                    var ticketTemplateTableCells = context.TicketTemplateTableCells.Where(x => x.TicketTemplateTableRowId == ticketTemplateTableRow.Id);
                                    foreach (var ticketTemplateTableCell in ticketTemplateTableCells)
                                    {
                                        ticketTemplateTableCell.IsDeleted = true;
                                        ticketTemplateTableCell.DateDelete = DateTime.Now;
                                        context.SaveChanges();

                                        var ticketTemplateTableParagrahs = context.TicketTemplateParagraphs.Where(x => x.TicketTemplateTableCellId == ticketTemplateTableCell.Id);
                                        foreach (var ticketTemplateTableParagrah in ticketTemplateTableParagrahs)
                                        {
                                            DeleteParagraph(context, ticketTemplateTableParagrah);
                                        }
                                    }

                                    DeleteAttribute(context, context.TicketTemplateElementaryAttributes.Where(x => x.TicketTemplateTableRowId == ticketTemplateTableRow.Id).ToList());
                                }
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

        private void DeleteParagraph(DepartmentDatabaseContext context, TicketTemplateParagraph paragraph)
        {
            paragraph.IsDeleted = true;
            paragraph.DateDelete = DateTime.Now;
            context.SaveChanges();

            //var datas = context.TicketTemplateParagraphDatas.Where(x => x.TicketTemplateParagraphId == paragraph.Id);
            //foreach (var data in datas)
            //{
            //    data.IsDeleted = true;
            //    data.DateDelete = DateTime.Now;
            //    context.SaveChanges();

            //    var ticketTemplateElementaryUnits = context.TicketTemplateElementaryUnits.Where(x => x.TicketTemplateParagraphDataId == data.Id);
            //    foreach (var ticketTemplateElementaryUnit in ticketTemplateElementaryUnits)
            //    {
            //        DeleteUnit(context, ticketTemplateElementaryUnit);
            //    }

            //    DeleteAttribute(context, context.TicketTemplateElementaryAttributes.Where(x => x.TicketTemplateParagraphDataId == data.Id).ToList());
            //}

            //DeleteAttribute(context, context.TicketTemplateElementaryAttributes.Where(x => x.TicketTemplateParagraphId == paragraph.Id).ToList());
        }

        private void DeleteUnit(DepartmentDatabaseContext context, TicketTemplateElementaryUnit unit)
        {
            unit.IsDeleted = true;
            unit.DateDelete = DateTime.Now;
            context.SaveChanges();

            var ticketTemplateElementaryUnits = context.TicketTemplateElementaryUnits.Where(x => x.ParentElementaryUnitId == unit.Id);
            foreach (var ticketTemplateElementaryUnit in ticketTemplateElementaryUnits)
            {
                ticketTemplateElementaryUnit.IsDeleted = true;
                ticketTemplateElementaryUnit.DateDelete = DateTime.Now;
                context.SaveChanges();

                DeleteUnit(context, ticketTemplateElementaryUnit);
            }

            DeleteAttribute(context, context.TicketTemplateElementaryAttributes.Where(x => x.TicketTemplateElementaryUnitId == unit.Id).ToList());
        }

        private void DeleteAttribute(DepartmentDatabaseContext context, List<TicketTemplateElementaryAttribute> attributes)
        {
            if (attributes == null)
            {
                return;
            }
            foreach (var attribute in attributes)
            {
                attribute.IsDeleted = true;
                attribute.DateDelete = DateTime.Now;
                context.SaveChanges();
            }
        }
    }
}