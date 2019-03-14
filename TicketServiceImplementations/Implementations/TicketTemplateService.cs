using DepartmentContext;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using TicketModels.Models;
using TicketServiceImplementations.Helpers;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketServiceInterfaces.ViewModels;

namespace TicketServiceImplementations.Implementations
{
    public class TicketTemplateService : ITicketTemplateService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.СоставлениеЭкзаменов;

        private readonly string _tagName = "шаблонов билетов";

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
            using (var context = new DepartmentDbContext())
            {
                try
                {
                    if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                    {
                        throw new Exception($"Нет доступа на чтение данных по записям {_tagName}");
                    }

                    int countPages = 0;
                    var query = context.TicketTemplates.Where(ed => !ed.IsDeleted).AsQueryable();

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

                    query = query
                        .Include(x => x.ExaminationTemplate);

                    var result = new TicketTemplatePageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(TicketModelFactoryToViewModel.CreateTicketTemplate).ToList()
                    };

                    return ResultService<TicketTemplatePageViewModel>.Success(result);
                }
                catch (DbEntityValidationException ex)
                {
                    return ResultService<TicketTemplatePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
                }
                catch (Exception ex)
                {
                    return ResultService<TicketTemplatePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
                }
            }
        }

        public ResultService<TicketTemplateViewModel> GetTicketTemplate(TicketTemplateGetBindingModel model)
        {
            using (var context = new DepartmentDbContext())
            {
                try
                {
                    if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                    {
                        throw new Exception(string.Format("Нет доступа на чтение данных по записям {0}", _tagName));
                    }

                    var entity = context.TicketTemplates
                                    .Include(x => x.ExaminationTemplate)
                                    .FirstOrDefault(x => x.Id == model.Id && !x.IsDeleted);
                    if (entity == null)
                    {
                        return ResultService<TicketTemplateViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                    }

                    entity.TicketTemplateBodies = new List<TicketTemplateBody> { TicketBodyGet.GetBody(context, entity.Id) };

                    return ResultService<TicketTemplateViewModel>.Success(TicketModelFactoryToViewModel.CreateTicketTemplate(entity));
                }
                catch (DbEntityValidationException ex)
                {
                    return ResultService<TicketTemplateViewModel>.Error(ex, ResultServiceStatusCode.Error);
                }
                catch (Exception ex)
                {
                    return ResultService<TicketTemplateViewModel>.Error(ex, ResultServiceStatusCode.Error);
                }
            }
        }

        public ResultService UpdateTicketTemplate(TicketTemplateSetBindingModel model)
        {
            using (var context = new DepartmentDbContext())
            {
                try
                {
                    if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                    {
                        throw new Exception($"Нет доступа на изменение данных по записям {_tagName}");
                    }

                    var entity = context.TicketTemplates.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                    }
                    entity = TicketModelFacotryFromBindingModel.CreateTicketTemplate(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
                catch (DbEntityValidationException ex)
                {
                    return ResultService.Error(ex, ResultServiceStatusCode.Error);
                }
                catch (Exception ex)
                {
                    return ResultService.Error(ex, ResultServiceStatusCode.Error);
                }
            }
        }

        public ResultService DeleteTicketTemplate(TicketTemplateGetBindingModel model)
        {
            using (var context = new DepartmentDbContext())
            {
                using (var transation = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                        {
                            throw new Exception(string.Format("Нет доступа на удаление данных по записям шаблонов билетов"));
                        }

                        var entity = context.TicketTemplates.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                        if (entity == null)
                        {
                            return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
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
                    catch (DbEntityValidationException ex)
                    {
                        transation.Rollback();
                        return ResultService.Error(ex, ResultServiceStatusCode.Error);
                    }
                    catch (Exception ex)
                    {
                        transation.Rollback();
                        return ResultService.Error(ex, ResultServiceStatusCode.Error);
                    }
                }
            }
        }

        private void DeleteParagraph(DepartmentDbContext context, TicketTemplateParagraph paragraph)
        {
            paragraph.IsDeleted = true;
            paragraph.DateDelete = DateTime.Now;
            context.SaveChanges();

            var datas = context.TicketTemplateParagraphDatas.Where(x => x.TicketTemplateParagraphId == paragraph.Id);
            foreach (var data in datas)
            {
                data.IsDeleted = true;
                data.DateDelete = DateTime.Now;
                context.SaveChanges();

                var ticketTemplateElementaryUnits = context.TicketTemplateElementaryUnits.Where(x => x.TicketTemplateParagraphDataId == data.Id);
                foreach (var ticketTemplateElementaryUnit in ticketTemplateElementaryUnits)
                {
                    DeleteUnit(context, ticketTemplateElementaryUnit);
                }

                DeleteAttribute(context, context.TicketTemplateElementaryAttributes.Where(x => x.TicketTemplateParagraphDataId == data.Id).ToList());
            }

            DeleteAttribute(context, context.TicketTemplateElementaryAttributes.Where(x => x.TicketTemplateParagraphId == paragraph.Id).ToList());
        }

        private void DeleteUnit(DepartmentDbContext context, TicketTemplateElementaryUnit unit)
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

        private void DeleteAttribute(DepartmentDbContext context, List<TicketTemplateElementaryAttribute> attributes)
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