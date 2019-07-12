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
    public class TicketTemplateTableRowService : ITicketTemplateTableRowService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.ШаблоныБилетов;

        private readonly string _entity = "Шаблоны Билетов";

        public ResultService<TicketTemplateTableRowPageViewModel> GetTicketTemplateTableRows(TicketTemplateTableRowGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.TicketTemplateTableRows.AsQueryable();

                    if (model.TicketTemplateTableId.HasValue)
                    {
                        query = query.Where(x => x.TicketTemplateTableId == model.TicketTemplateTableId);
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
                        .Include(x => x.TicketTemplateTableRowProperties)
                        .Include(x => x.TicketTemplateTableCells)
                        .Include("TicketTemplateTableCells.TicketTemplateTableCellProperties")
                        .Include("TicketTemplateTableCells.TicketTemplateParagraphs")
                        .Include("TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                        .Include("TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                        .Include("TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties");

                    var result = new TicketTemplateTableRowPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ExaminationModelFactoryToViewModel.CreateTicketTemplateTableRowViewModel).ToList()
                    };

                    return ResultService<TicketTemplateTableRowPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<TicketTemplateTableRowPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<TicketTemplateTableRowViewModel> GetTicketTemplateTableRow(TicketTemplateTableRowGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TicketTemplateTableRows
                                .Include(x => x.TicketTemplateTableRowProperties)
                                .Include(x => x.TicketTemplateTableCells)
                                .Include("TicketTemplateTableCells.TicketTemplateTableCellProperties")
                                .Include("TicketTemplateTableCells.TicketTemplateParagraphs")
                                .Include("TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                                .Include("TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                                .Include("TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties")
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<TicketTemplateTableRowViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    return ResultService<TicketTemplateTableRowViewModel>.Success(ExaminationModelFactoryToViewModel.CreateTicketTemplateTableRowViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<TicketTemplateTableRowViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateTicketTemplateTableRow(TicketTemplateTableRowSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ExaminationModelFacotryFromBindingModel.CreateTicketTemplateTableRow(model);

                    var exsistEntity = context.TicketTemplateTableRows.Include(x => x.TicketTemplateTableRowProperties)
                                                    .FirstOrDefault(x => x.TicketTemplateTableId == entity.TicketTemplateTableId && x.Order == model.Order);
                    if (exsistEntity == null)
                    {
                        context.TicketTemplateTableRows.Add(entity);
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

        public ResultService UpdateTicketTemplateTableRow(TicketTemplateTableRowSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TicketTemplateTableRows.Include(x => x.TicketTemplateTableRowProperties).FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    entity = ExaminationModelFacotryFromBindingModel.CreateTicketTemplateTableRow(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteTicketTemplateTableRow(TicketTemplateTableRowGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TicketTemplateTableRows
                        .Include(x => x.TicketTemplateTableRowProperties)
                        .Include(x => x.TicketTemplateTableCells)
                        .Include("TicketTemplateTableCells.TicketTemplateTableCellProperties")
                        .Include("TicketTemplateTableCells.TicketTemplateParagraphs")
                        .Include("TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                        .Include("TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                        .Include("TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties")
                        .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    context.TicketTemplateTableRows.Remove(entity);
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