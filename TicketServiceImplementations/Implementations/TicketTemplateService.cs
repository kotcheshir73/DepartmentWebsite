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

                    entity.TicketTemplateBodies = new List<TicketTemplateBody> { GetBody(context, entity.Id) };

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

        private TicketTemplateBody GetBody(DepartmentDbContext context, Guid ticketTemplateId)
        {
            var body = context.TicketTemplateBodies.FirstOrDefault(x => x.TicketTemplateId == ticketTemplateId);
            if(body != null)
            {
                body.BodyFormat = GetElementaryUnitById(context, body.BodyFormatId);
                body.TicketTemplateParagraphs = GetTicketTemplateParagraphs(context, bodyId: body.Id);
                body.TicketTemplateTables = GetTicketTemplateTables(context, bodyId: body.Id);
            }

            return body;
        }

        private List<TicketTemplateTable> GetTicketTemplateTables(DepartmentDbContext context, Guid? bodyId)
        {
            if(bodyId.HasValue)
            {
                List<TicketTemplateTable> tabels = context.TicketTemplateTables.Where(x => x.TicketTemplateBodyId == bodyId && !x.IsDeleted).OrderBy(x => x.Order).ToList();
                foreach(var tabel in tabels)
                {
                    tabel.Properties = GetElementaryUnitById(context, tabel.PropertiesId);
                    tabel.Columns = GetElementaryUnitById(context, tabel.ColumnsId);
                    tabel.TicketTemplateTableRows = context.TicketTemplateTableRows.Where(x => x.TicketTemplateTableId == tabel.Id && !x.IsDeleted).OrderBy(x => x.Order).ToList();
                    foreach(var row in tabel.TicketTemplateTableRows)
                    {
                        row.Properties = GetElementaryUnitById(context, row.PropertiesId);
                        row.TicketTemplateElementaryAttributes = context.TicketTemplateElementaryAttributes.Where(x => x.TicketTemplateTableRowId == row.Id && !x.IsDeleted).ToList();
                        row.TicketTemplateTableCells = context.TicketTemplateTableCells.Where(x => x.TicketTemplateTableRowId == row.Id && !x.IsDeleted).OrderBy(x => x.Order).ToList();
                        foreach(var cell in row.TicketTemplateTableCells)
                        {
                            cell.Properties = GetElementaryUnitById(context, cell.PropertiesId);
                            cell.TicketTemplateParagraphs = GetTicketTemplateParagraphs(context, cellId: cell.Id);
                        }
                    }
                }

                return tabels;
            }

            return null;
        }

        private List<TicketTemplateParagraph> GetTicketTemplateParagraphs(DepartmentDbContext context, Guid? bodyId = null, Guid? cellId = null)
        {
            List<TicketTemplateParagraph> paragraphs = null;
            if (bodyId.HasValue)
            {
                paragraphs = context.TicketTemplateParagraphs.Where(x => x.TicketTemplateBodyId == bodyId && !x.IsDeleted).OrderBy(x => x.Order).ToList();
            }
            if (cellId.HasValue)
            {
                paragraphs = context.TicketTemplateParagraphs.Where(x => x.TicketTemplateTableCellId == cellId && !x.IsDeleted).OrderBy(x => x.Order).ToList();
            }

            if (paragraphs != null)
            {
                foreach (var paragraph in paragraphs)
                {
                    paragraph.ParagraphFormat = GetElementaryUnitById(context, paragraph.ParagraphFormatId);
                    paragraph.TicketTemplateElementaryAttributes = context.TicketTemplateElementaryAttributes.Where(x => x.TicketTemplateParagraphId == paragraph.Id && !x.IsDeleted).ToList();
                    paragraph.TicketTemplateParagraphDatas = context.TicketTemplateParagraphDatas.Where(x => x.TicketTemplateParagraphId == paragraph.Id && !x.IsDeleted).OrderBy(x => x.Order).ToList();
                    foreach (var data in paragraph.TicketTemplateParagraphDatas)
                    {
                        data.Font = GetElementaryUnitById(context, data.FontId);
                        data.TicketTemplateElementaryAttributes = context.TicketTemplateElementaryAttributes.Where(x => x.TicketTemplateParagraphDataId == data.Id && !x.IsDeleted).ToList();
                        data.TicketTemplateElementaryUnits = context.TicketTemplateElementaryUnits.Where(x => x.TicketTemplateParagraphDataId == data.Id && !x.IsDeleted).OrderBy(x => x.Order).ToList();
                        foreach (var unit in data.TicketTemplateElementaryUnits)
                        {
                            GetSubElems(context, unit);
                        }
                    }
                }

                return paragraphs;
            }

            return paragraphs;
        }

        /// <summary>
        /// Поулчение элемента по идентификатору
        /// </summary>
        /// <param name="context"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        private TicketTemplateElementaryUnit GetElementaryUnitById(DepartmentDbContext context, Guid? Id)
        {
            if(Id.HasValue)
            {
                var entity = context.TicketTemplateElementaryUnits.FirstOrDefault(x => x.Id == Id);
                GetSubElems(context, entity);

                return entity;
            }

            return null;
        }

        /// <summary>
        /// Получение дочерних элементов и атрибутов элемента
        /// </summary>
        /// <param name="context"></param>
        /// <param name="unit"></param>
        private void GetSubElems(DepartmentDbContext context, TicketTemplateElementaryUnit unit)
        {
            if(unit != null)
            {
                unit.TicketTemplateElementaryAttributes = context.TicketTemplateElementaryAttributes.Where(x => x.TicketTemplateElementaryUnitId == unit.Id && !x.IsDeleted).ToList();
                unit.ChildElementaryUnits = context.TicketTemplateElementaryUnits.Where(x => x.ParentElementaryUnitId == unit.Id && !x.IsDeleted).OrderBy(x => x.Order).ToList();
                foreach (var child in unit.ChildElementaryUnits)
                {
                    GetSubElems(context, child);
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