using DepartmentContext;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketServiceInterfaces.ViewModels;

namespace TicketServiceImplementations.Implementations
{
    public class ExaminationTemplateTicketQuestionService : IExaminationTemplateTicketQuestionService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.СоставлениеЭкзаменов;

        private readonly string _tagName = "вопросам билета экзамена";

        private readonly IExaminationTemplateTicketService _serviceETT;

        public ExaminationTemplateTicketQuestionService(DepartmentDbContext context, IExaminationTemplateTicketService serviceETT)
        {
            _context = context;
            _serviceETT = serviceETT;
        }

        public ResultService<ExaminationTemplateTicketPageViewModel> GetExaminationTemplateTickets(ExaminationTemplateTicketGetBindingModel model)
        {
            return _serviceETT.GetExaminationTemplateTickets(model);
        }

        public ResultService<ExaminationTemplateTicketQuestionPageViewModel> GetExaminationTemplateTicketQuestions(ExaminationTemplateTicketQuestionGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception(string.Format("Нет доступа на чтение данных по записям {0}", _tagName));
                }

                int countPages = 0;
                var query = _context.ExaminationTemplateTicketQuestions.Where(ed => !ed.IsDeleted).AsQueryable();

                if (model.ExaminationTemplateTicketId.HasValue)
                {
                    query = query.Where(x => x.ExaminationTemplateTicketId == model.ExaminationTemplateTicketId);
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
                    .Include(x => x.ExaminationTemplateBlockQuestion)
                    .Include(x => x.ExaminationTemplateTicket);

                var result = new ExaminationTemplateTicketQuestionPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(TicketModelFactoryToViewModel.CreateExaminationTemplateTicketQuestionViewModel).ToList()
                };

                return ResultService<ExaminationTemplateTicketQuestionPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<ExaminationTemplateTicketQuestionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationTemplateTicketQuestionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<ExaminationTemplateTicketQuestionViewModel> GetExaminationTemplateTicketQuestion(ExaminationTemplateTicketQuestionGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception(string.Format("Нет доступа на чтение данных по записям {0}", _tagName));
                }

                var entity = _context.ExaminationTemplateTicketQuestions
                                .Include(x => x.ExaminationTemplateBlockQuestion)
                                .Include(x => x.ExaminationTemplateTicket)
                                .FirstOrDefault(x => x.Id == model.Id && !x.IsDeleted);
                if (entity == null)
                {
                    return ResultService<ExaminationTemplateTicketQuestionViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<ExaminationTemplateTicketQuestionViewModel>.Success(TicketModelFactoryToViewModel.CreateExaminationTemplateTicketQuestionViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<ExaminationTemplateTicketQuestionViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationTemplateTicketQuestionViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateExaminationTemplateTicketQuestion(ExaminationTemplateTicketQuestionSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception(string.Format("Нет доступа на изменение данных по записям {0}", _tagName));
                }

                var entity = TicketModelFacotryFromBindingModel.CreateExaminationTemplateTicketQuestion(model);

                _context.ExaminationTemplateTicketQuestions.Add(entity);
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

        public ResultService UpdateExaminationTemplateTicketQuestion(ExaminationTemplateTicketQuestionSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception(string.Format("Нет доступа на изменение данных по записям {0}", _tagName));
                }

                var entity = _context.ExaminationTemplateTicketQuestions.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity = TicketModelFacotryFromBindingModel.CreateExaminationTemplateTicketQuestion(model, entity);

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

        public ResultService DeleteExaminationTemplateTicketQuestion(ExaminationTemplateTicketQuestionGetBindingModel model)
        {
            using (var transation = _context.Database.BeginTransaction())
            {
                try
                {
                    if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                    {
                        throw new Exception(string.Format("Нет доступа на удаление данных по записям {0}", _tagName));
                    }

                    var entity = _context.ExaminationTemplates.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                    }
                    entity.IsDeleted = true;
                    entity.DateDelete = DateTime.Now;
                    _context.SaveChanges();

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