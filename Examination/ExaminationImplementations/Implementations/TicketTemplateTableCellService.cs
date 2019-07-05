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
    public class TicketTemplateTableCellService : ITicketTemplateTableCellService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.ШаблоныБилетов;

        private readonly string _entity = "Шаблоны Билетов";

        private readonly ITicketTemplateParagraphService _paragraphService;

        public TicketTemplateTableCellService(ITicketTemplateParagraphService paragraphService)
        {
            _paragraphService = paragraphService;
        }

        public ResultService<TicketTemplateTableCellPageViewModel> GetTicketTemplateTableCells(TicketTemplateTableCellGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.TicketTemplateTableCells.AsQueryable();

                    if (model.TicketTemplateTableRowId.HasValue)
                    {
                        query = query.Where(x => x.TicketTemplateTableRowId == model.TicketTemplateTableRowId);
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

                    query = query.Include(x => x.TicketTemplateTableCellProperties).Include(x => x.TicketTemplateParagraphs).Include("TicketTemplateParagraphs.TicketTemplateParagraphRuns");

                    var result = new TicketTemplateTableCellPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ExaminationModelFactoryToViewModel.CreateTicketTemplateTableCellViewModel).ToList()
                    };

                    return ResultService<TicketTemplateTableCellPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<TicketTemplateTableCellPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<TicketTemplateTableCellViewModel> GetTicketTemplateTableCell(TicketTemplateTableCellGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TicketTemplateTableCells
                                .Include(x => x.TicketTemplateTableCellProperties)
                                .Include(x => x.TicketTemplateParagraphs)
                                .Include("TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<TicketTemplateTableCellViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    return ResultService<TicketTemplateTableCellViewModel>.Success(ExaminationModelFactoryToViewModel.CreateTicketTemplateTableCellViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<TicketTemplateTableCellViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateTicketTemplateTableCell(TicketTemplateTableCellSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ExaminationModelFacotryFromBindingModel.CreateTicketTemplateTableCell(model);

                    var exsistEntity = context.TicketTemplateTableCells.Include(x => x.TicketTemplateTableCellProperties)
                                                    .FirstOrDefault(x => x.TicketTemplateTableRowId == entity.TicketTemplateTableRowId && x.Order == model.Order);
                    if (exsistEntity == null)
                    {
                        context.TicketTemplateTableCells.Add(entity);
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

        public ResultService UpdateTicketTemplateTableCell(TicketTemplateTableCellSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TicketTemplateTableCells.Include(x => x.TicketTemplateTableCellProperties).FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    entity = ExaminationModelFacotryFromBindingModel.CreateTicketTemplateTableCell(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteTicketTemplateTableCell(TicketTemplateTableCellGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                using (var transation = context.Database.BeginTransaction())
                {
                    var entity = context.TicketTemplateTableCells.Include(x => x.TicketTemplateTableCellProperties).Include(x => x.TicketTemplateParagraphs).FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    if (entity.TicketTemplateTableCellProperties != null)
                    {
                        context.TicketTemplateTableCellProperties.Remove(entity.TicketTemplateTableCellProperties);
                        context.SaveChanges();
                    }

                    if (entity.TicketTemplateParagraphs != null)
                    {
                        foreach (var run in entity.TicketTemplateParagraphs)
                        {
                            _paragraphService.DeleteTicketTemplateParagraph(new TicketTemplateParagraphGetBindingModel { Id = run.Id });
                        }
                    }

                    context.TicketTemplateTableCells.Remove(entity);
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