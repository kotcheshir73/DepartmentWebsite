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
    public class TicketTemplateService : ITicketTemplateService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.ШаблоныБилетов;

        private readonly string _entity = "Шаблоны Билетов";

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

                    var exsistEntity = context.TicketTemplates
                                                    .FirstOrDefault(x => x.Id == entity.Id);
                    if (exsistEntity == null)
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                var body = entity.TicketTemplateBody;
                                entity.TicketTemplateBodyId = null;
                                entity.TicketTemplateBody = null;

                                context.TicketTemplates.Add(entity);
                                context.SaveChanges();

                                var paragraphs = body.TicketTemplateParagraphs;
                                body.TicketTemplateParagraphs = null;

                                context.TicketTemplateBodies.Add(body);
                                context.SaveChanges();

                                paragraphs.ForEach(paragraph =>
                                {
                                    var runs = paragraph.TicketTemplateParagraphRuns;
                                    paragraph.TicketTemplateParagraphRuns = null;

                                    context.TicketTemplateParagraphs.Add(paragraph);
                                    context.SaveChanges();

                                    runs?.ForEach(run =>
                                    {
                                        context.TicketTemplateParagraphRuns.Add(run);
                                        context.SaveChanges();
                                    });

                                });

                                //foreach (var paragraph in paragraphs)
                                //{
                                //    var runs = paragraph.TicketTemplateParagraphRuns;
                                //    paragraph.TicketTemplateParagraphRuns = null;

                                //    context.TicketTemplateParagraphs.Add(paragraph);
                                //    context.SaveChanges();

                                //    if (runs != null)
                                //    {
                                //        context.TicketTemplateParagraphRuns.AddRange(runs);
                                //        context.SaveChanges();
                                //    }
                                //}

                                entity.TicketTemplateBodyId = body.Id;
                                context.SaveChanges();

                                transaction.Commit();

                                return ResultService.Success(entity.Id);
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                return ResultService.Error(ex, ResultServiceStatusCode.Error);
                            }
                        }
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
                        //else if (entity.IsDeleted)
                        //{
                        //    return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                        //}
                        //entity.IsDeleted = true;
                        //entity.DateDelete = DateTime.Now;
                        context.SaveChanges();

                        var ticketTemplaetBodies = context.TicketTemplateBodies.Where(x => x.TicketTemplateId == entity.Id);
                        foreach (var ticketTemplaetBody in ticketTemplaetBodies)
                        {
                            //ticketTemplaetBody.IsDeleted = true;
                            //ticketTemplaetBody.DateDelete = DateTime.Now;
                            //context.SaveChanges();

                            //var ticketTemplateTables = context.TicketTemplateTables.Where(x => x.TicketTemplateBodyId == ticketTemplaetBody.Id);
                            //foreach (var ticketTemplateTable in ticketTemplateTables)
                            //{
                            //    ticketTemplateTable.IsDeleted = true;
                            //    ticketTemplateTable.DateDelete = DateTime.Now;
                            //    context.SaveChanges();

                            //    var ticketTemplateTableRows = context.TicketTemplateTableRows.Where(x => x.TicketTemplateTableId == ticketTemplateTable.Id);
                            //    foreach (var ticketTemplateTableRow in ticketTemplateTableRows)
                            //    {
                            //        ticketTemplateTableRow.IsDeleted = true;
                            //        ticketTemplateTableRow.DateDelete = DateTime.Now;
                            //        context.SaveChanges();

                            //        var ticketTemplateTableCells = context.TicketTemplateTableCells.Where(x => x.TicketTemplateTableRowId == ticketTemplateTableRow.Id);
                            //        foreach (var ticketTemplateTableCell in ticketTemplateTableCells)
                            //        {
                            //            ticketTemplateTableCell.IsDeleted = true;
                            //            ticketTemplateTableCell.DateDelete = DateTime.Now;
                            //            context.SaveChanges();

                            //            var ticketTemplateTableParagrahs = context.TicketTemplateParagraphs.Where(x => x.TicketTemplateTableCellId == ticketTemplateTableCell.Id);
                            //            foreach (var ticketTemplateTableParagrah in ticketTemplateTableParagrahs)
                            //            {
                            //                DeleteParagraph(context, ticketTemplateTableParagrah);
                            //            }
                            //        }

                            //        DeleteAttribute(context, context.TicketTemplateElementaryAttributes.Where(x => x.TicketTemplateTableRowId == ticketTemplateTableRow.Id).ToList());
                            //    }
                            //}
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
}