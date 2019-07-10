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
    public class TicketTemplateBodyService : ITicketTemplateBodyService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.ШаблоныБилетов;

        private readonly string _entity = "Шаблоны Билетов";

        public ResultService<TicketTemplateBodyPageViewModel> GetTicketTemplateBodys(TicketTemplateBodyGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.TicketTemplateBodies.AsQueryable();

                    if (model.TicketTemplateId.HasValue)
                    {
                        query = query.Where(x => x.TicketTemplateId == model.TicketTemplateId);
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
                        .Include(x => x.TicketTemplateBodyProperties)
                        .Include(x => x.TicketTemplateParagraphs)
                        .Include("TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                        .Include("TicketTemplateParagraphs.TicketTemplateTableGridColumns")
                        .Include("TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties")
                        .Include(x => x.TicketTemplateTables)
                        .Include("TicketTemplateTables.TicketTemplateTableProperties")
                        .Include("TicketTemplateTables.TicketTemplateTableGridColumns")
                        .Include("TicketTemplateTables.TicketTemplateTableRows")
                        .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableRowProperties")
                        .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells")
                        .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateTableCellProperties")
                        .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs")
                        .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                        .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                        .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties");

                    var result = new TicketTemplateBodyPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ExaminationModelFactoryToViewModel.CreateTicketTemplateBodyViewModel).ToList()
                    };

                    return ResultService<TicketTemplateBodyPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<TicketTemplateBodyPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<TicketTemplateBodyViewModel> GetTicketTemplateBody(TicketTemplateBodyGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TicketTemplateBodies
                                .Include(x => x.TicketTemplateBodyProperties)
                                .Include(x => x.TicketTemplateParagraphs)
                                .Include("TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                                .Include("TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                                .Include("TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties")
                                .Include(x => x.TicketTemplateTables)
                                .Include("TicketTemplateTables.TicketTemplateTableProperties")
                                .Include("TicketTemplateTables.TicketTemplateTableGridColumns")
                                .Include("TicketTemplateTables.TicketTemplateTableRows")
                                .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableRowProperties")
                                .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells")
                                .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateTableCellProperties")
                                .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs")
                                .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                                .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                                .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties")
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<TicketTemplateBodyViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    return ResultService<TicketTemplateBodyViewModel>.Success(ExaminationModelFactoryToViewModel.CreateTicketTemplateBodyViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<TicketTemplateBodyViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateTicketTemplateBody(TicketTemplateBodySetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ExaminationModelFacotryFromBindingModel.CreateTicketTemplateBody(model);

                    var exsistEntity = context.TicketTemplateBodies.Include(x => x.TicketTemplateBodyProperties)
                                                    .FirstOrDefault(x => x.TicketTemplateId == entity.TicketTemplateId);
                    if (exsistEntity == null)
                    {
                        context.TicketTemplateBodies.Add(entity);
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

        public ResultService UpdateTicketTemplateBody(TicketTemplateBodySetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TicketTemplateBodies.Include(x => x.TicketTemplateBodyProperties).FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    entity = ExaminationModelFacotryFromBindingModel.CreateTicketTemplateBody(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteTicketTemplateBody(TicketTemplateBodyGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TicketTemplateBodies
                                .Include(x => x.TicketTemplateBodyProperties)
                                .Include(x => x.TicketTemplateParagraphs)
                                .Include("TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                                .Include("TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                                .Include("TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties")
                                .Include(x => x.TicketTemplateTables)
                                .Include("TicketTemplateTables.TicketTemplateTableProperties")
                                .Include("TicketTemplateTables.TicketTemplateTableGridColumns")
                                .Include("TicketTemplateTables.TicketTemplateTableRows")
                                .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableRowProperties")
                                .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells")
                                .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateTableCellProperties")
                                .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs")
                                .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                                .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                                .Include("TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties")
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    context.TicketTemplateBodies.Remove(entity);
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