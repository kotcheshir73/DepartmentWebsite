using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepartmentContext;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketServiceInterfaces.ViewModels;

namespace TicketServiceImplementations.Implementations
{
    public class ExaminationTemplateTicketService : IExaminationTemplateTicketService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.СоставлениеЭкзаменов;

        private readonly string _tagName = "билетов экзамена";

        private readonly IExaminationTemplateService _serviceET;

        public ExaminationTemplateTicketService(DepartmentDbContext context, IExaminationTemplateService serviceET)
        {
            _context = context;
            _serviceET = serviceET;
        }

        public ResultService<ExaminationTemplatePageViewModel> GetExaminationTemplates(ExaminationTemplateGetBindingModel model)
        {
            return _serviceET.GetExaminationTemplates(model);
        }

        public ResultService<ExaminationTemplateTicketPageViewModel> GetExaminationTemplateTickets(ExaminationTemplateTicketGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception(string.Format("Нет доступа на чтение данных по записям {0}", _tagName));
                }

                int countPages = 0;
                var query = _context.ExaminationTemplateTickets.Where(ed => !ed.IsDeleted).AsQueryable();

                if (model.ExaminationTemplateId.HasValue)
                {
                    query = query.Where(x => x.ExaminationTemplateId == model.ExaminationTemplateId);
                }

                if (model.Id.HasValue)
                {
                    query = query.Where(x => x.Id == model.Id);
                }

                query = query.OrderBy(x => x.TicketNumber);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query
                    .Include(x => x.ExaminationTemplate);

                var result = new ExaminationTemplateTicketPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(TicketModelFactoryToViewModel.CreateExaminationTemplateTicketViewModel).ToList()
                };

                return ResultService<ExaminationTemplateTicketPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<ExaminationTemplateTicketPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationTemplateTicketPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<ExaminationTemplateTicketViewModel> GetExaminationTemplateTicket(ExaminationTemplateTicketGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception(string.Format("Нет доступа на чтение данных по записям {0}", _tagName));
                }

                var entity = _context.ExaminationTemplateTickets
                                .Include(x => x.ExaminationTemplate)
                                .FirstOrDefault(x => x.Id == model.Id && !x.IsDeleted);
                if (entity == null)
                {
                    return ResultService<ExaminationTemplateTicketViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<ExaminationTemplateTicketViewModel>.Success(TicketModelFactoryToViewModel.CreateExaminationTemplateTicketViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<ExaminationTemplateTicketViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationTemplateTicketViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateExaminationTemplateTicket(ExaminationTemplateTicketSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception(string.Format("Нет доступа на изменение данных по записям {0}", _tagName));
                }

                var entity = TicketModelFacotryFromBindingModel.CreateExaminationTemplateTicket(model);

                _context.ExaminationTemplateTickets.Add(entity);
                _context.SaveChanges();

                return ResultService.Success(entity.Id);
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

        public ResultService UpdateExaminationTemplateTicket(ExaminationTemplateTicketSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception(string.Format("Нет доступа на изменение данных по записям {0}", _tagName));
                }

                var entity = _context.ExaminationTemplateTickets.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity = TicketModelFacotryFromBindingModel.CreateExaminationTemplateTicket(model, entity);

                _context.SaveChanges();

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

        public ResultService DeleteExaminationTemplateTicket(ExaminationTemplateTicketGetBindingModel model)
        {
            using (var transation = _context.Database.BeginTransaction())
            {
                try
                {
                    if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                    {
                        throw new Exception(string.Format("Нет доступа на удаление данных по записям {0}", _tagName));
                    }

                    var entity = _context.ExaminationTemplateTickets.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                    }
                    entity.IsDeleted = true;
                    entity.DateDelete = DateTime.Now;
                    _context.SaveChanges();

                    var examinationTemplateTicketQuestions = _context.ExaminationTemplateTicketQuestions.Where(x => x.ExaminationTemplateTicketId == entity.Id);
                    foreach (var examinationTemplateTicketQuestion in examinationTemplateTicketQuestions)
                    {
                        examinationTemplateTicketQuestion.IsDeleted = true;
                        examinationTemplateTicketQuestion.DateDelete = DateTime.Now;
                        _context.SaveChanges();
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
}