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
    public class TicketTemplateTableService : ITicketTemplateTableService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.ШаблоныБилетов;

        private readonly string _entity = "Шаблоны Билетов";

        private readonly ITicketTemplateTableRowService _rowService;

        public TicketTemplateTableService(ITicketTemplateTableRowService rowService)
        {
            _rowService = rowService;
        }

        public ResultService<TicketTemplateTablePageViewModel> GetTicketTemplateTables(TicketTemplateTableGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.TicketTemplateTables.AsQueryable();

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

                    query = query.Include(x => x.TicketTemplateTableProperties).Include(x => x.TicketTemplateTableRows)
                        .Include("TicketTemplateTableRows.TicketTemplateTableCells")
                        .Include("TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs")
                        .Include("TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns");

                    var result = new TicketTemplateTablePageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ExaminationModelFactoryToViewModel.CreateTicketTemplateTableViewModel).ToList()
                    };

                    return ResultService<TicketTemplateTablePageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<TicketTemplateTablePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<TicketTemplateTableViewModel> GetTicketTemplateTable(TicketTemplateTableGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TicketTemplateTables
                        .Include(x => x.TicketTemplateTableProperties)
                        .Include(x => x.TicketTemplateTableRows)
                        .Include("TicketTemplateTableRows.TicketTemplateTableCells")
                        .Include("TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs")
                        .Include("TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<TicketTemplateTableViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    return ResultService<TicketTemplateTableViewModel>.Success(ExaminationModelFactoryToViewModel.CreateTicketTemplateTableViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<TicketTemplateTableViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateTicketTemplateTable(TicketTemplateTableSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ExaminationModelFacotryFromBindingModel.CreateTicketTemplateTable(model);

                    var exsistEntity = context.TicketTemplateTables.Include(x => x.TicketTemplateTableProperties)
                                                    .FirstOrDefault(x => x.TicketTemplateBodyId == entity.TicketTemplateBodyId && x.Order == model.Order);
                    if (exsistEntity == null)
                    {
                        context.TicketTemplateTables.Add(entity);
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

        public ResultService UpdateTicketTemplateTable(TicketTemplateTableSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TicketTemplateTables.Include(x => x.TicketTemplateTableProperties).FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    entity = ExaminationModelFacotryFromBindingModel.CreateTicketTemplateTable(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteTicketTemplateTable(TicketTemplateTableGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                using (var transation = context.Database.BeginTransaction())
                {
                    var entity = context.TicketTemplateTables.Include(x => x.TicketTemplateTableProperties).Include(x => x.TicketTemplateTableRows).FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    if (entity.TicketTemplateTableProperties != null)
                    {
                        context.TicketTemplateTableProperties.Remove(entity.TicketTemplateTableProperties);
                        context.SaveChanges();
                    }

                    if (entity.TicketTemplateTableRows != null)
                    {
                        foreach (var run in entity.TicketTemplateTableRows)
                        {
                            _rowService.DeleteTicketTemplateTableRow(new TicketTemplateTableRowGetBindingModel { Id = run.Id });
                        }
                    }

                    context.TicketTemplateTables.Remove(entity);
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