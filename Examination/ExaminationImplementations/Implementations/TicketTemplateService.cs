﻿using DatabaseContext;
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

                        .Include(x => x.TicketTemplateDocumentSettings)
                        .Include(x => x.TicketTemplateFontTables)
                        .Include(x => x.TicketTemplateNumberings)
                        .Include(x => x.TicketTemplateStyleDefinitions)
                        .Include(x => x.TicketTemplateThemeParts)
                        .Include(x => x.TicketTemplateWebSettings)

                        .Include("TicketTemplateBodies")
                        .Include("TicketTemplateBodies.TicketTemplateBodyProperties")
                        .Include("TicketTemplateBodies.TicketTemplateParagraphs")
                        .Include("TicketTemplateBodies.TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                        .Include("TicketTemplateBodies.TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                        .Include("TicketTemplateBodies.TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties")
                        .Include("TicketTemplateBodies.TicketTemplateTables")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableProperties")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableGridColumns")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableRowProperties")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateTableCellProperties")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties");

                    var result = new TicketTemplatePageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ExaminationModelFactoryToViewModel.CreateTicketTemplateViewModel).ToList()
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
                                    .Include(x => x.TicketTemplateDocumentSettings)
                                    .Include(x => x.TicketTemplateFontTables)
                                    .Include(x => x.TicketTemplateNumberings)
                                    .Include(x => x.TicketTemplateStyleDefinitions)
                                    .Include(x => x.TicketTemplateThemeParts)
                                    .Include(x => x.TicketTemplateWebSettings)

                                    .Include("TicketTemplateBodies")
                                    .Include("TicketTemplateBodies.TicketTemplateBodyProperties")
                                    .Include("TicketTemplateBodies.TicketTemplateParagraphs")
                                    .Include("TicketTemplateBodies.TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                                    .Include("TicketTemplateBodies.TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                                    .Include("TicketTemplateBodies.TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties")
                                    .Include("TicketTemplateBodies.TicketTemplateTables")
                                    .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableProperties")
                                    .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableGridColumns")
                                    .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows")
                                    .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableRowProperties")
                                    .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells")
                                    .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateTableCellProperties")
                                    .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs")
                                    .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                                    .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                                    .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties")
                                    .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<TicketTemplateViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    return ResultService<TicketTemplateViewModel>.Success(ExaminationModelFactoryToViewModel.CreateTicketTemplateViewModel(entity));
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
                {
                    var entity = context.TicketTemplates
                        .Include(x => x.TicketTemplateDocumentSettings)
                        .Include(x => x.TicketTemplateFontTables)
                        .Include(x => x.TicketTemplateNumberings)
                        .Include(x => x.TicketTemplateStyleDefinitions)
                        .Include(x => x.TicketTemplateThemeParts)
                        .Include(x => x.TicketTemplateWebSettings)

                        .Include("TicketTemplateBodies")
                        .Include("TicketTemplateBodies.TicketTemplateBodyProperties")
                        .Include("TicketTemplateBodies.TicketTemplateParagraphs")
                        .Include("TicketTemplateBodies.TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                        .Include("TicketTemplateBodies.TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                        .Include("TicketTemplateBodies.TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties")
                        .Include("TicketTemplateBodies.TicketTemplateTables")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableProperties")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableGridColumns")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableRowProperties")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateTableCellProperties")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                        .Include("TicketTemplateBodies.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties")
                        .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    context.TicketTemplates.Remove(entity);
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