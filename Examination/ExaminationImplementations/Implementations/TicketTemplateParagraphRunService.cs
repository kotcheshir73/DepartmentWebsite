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
    public class TicketTemplateParagraphRunService : ITicketTemplateParagraphRunService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.ШаблоныБилетов;

        private readonly string _entity = "Шаблоны Билетов";

        public ResultService<TicketTemplateParagraphRunPageViewModel> GetTicketTemplateParagraphRuns(TicketTemplateParagraphRunGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.TicketTemplateParagraphRuns.AsQueryable();

                    if (model.TicketTemplateParagraphId.HasValue)
                    {
                        query = query.Where(x => x.TicketTemplateParagraphId == model.TicketTemplateParagraphId);
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

                    query = query.Include(x => x.TicketTemplateParagraphRunProperties);

                    var result = new TicketTemplateParagraphRunPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ExaminationModelFactoryToViewModel.CreateTicketTemplateParagraphRunViewModel).ToList()
                    };

                    return ResultService<TicketTemplateParagraphRunPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<TicketTemplateParagraphRunPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<TicketTemplateParagraphRunViewModel> GetTicketTemplateParagraphRun(TicketTemplateParagraphRunGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TicketTemplateParagraphRuns
                                .Include(x => x.TicketTemplateParagraphRunProperties)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<TicketTemplateParagraphRunViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    return ResultService<TicketTemplateParagraphRunViewModel>.Success(ExaminationModelFactoryToViewModel.CreateTicketTemplateParagraphRunViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<TicketTemplateParagraphRunViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateTicketTemplateParagraphRun(TicketTemplateParagraphRunSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ExaminationModelFacotryFromBindingModel.CreateTicketTemplateParagraphRun(model);

                    var exsistEntity = context.TicketTemplateParagraphRuns.Include(x => x.TicketTemplateParagraphRunProperties)
                                                    .FirstOrDefault(x => x.TicketTemplateParagraphId == entity.TicketTemplateParagraphId && x.Text == model.Text);
                    if (exsistEntity == null)
                    {
                        context.TicketTemplateParagraphRuns.Add(entity);
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

        public ResultService UpdateTicketTemplateParagraphRun(TicketTemplateParagraphRunSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TicketTemplateParagraphRuns.Include(x => x.TicketTemplateParagraphRunProperties).FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    entity = ExaminationModelFacotryFromBindingModel.CreateTicketTemplateParagraphRun(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteTicketTemplateParagraphRun(TicketTemplateParagraphRunGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                using (var transation = context.Database.BeginTransaction())
                {
                    var entity = context.TicketTemplateParagraphRuns.Include(x => x.TicketTemplateParagraphRunProperties).FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    
                    if(entity.TicketTemplateParagraphRunProperties != null)
                    {
                        context.TicketTemplateParagraphRunProperties.Remove(entity.TicketTemplateParagraphRunProperties);
                        context.SaveChanges();
                    }
                    context.TicketTemplateParagraphRuns.Remove(entity);
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