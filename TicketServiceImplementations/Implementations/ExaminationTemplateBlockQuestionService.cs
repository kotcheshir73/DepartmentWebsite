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
    public class ExaminationTemplateBlockQuestionService : IExaminationTemplateBlockQuestionService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.СоставлениеЭкзаменов;

        private readonly string _tagName = "вопросам блока экзамена";

        private readonly IExaminationTemplateBlockService _serviceETB;

        public ExaminationTemplateBlockQuestionService(DepartmentDbContext context, IExaminationTemplateBlockService serviceETB)
        {
            _context = context;
            _serviceETB = serviceETB;
        }

        public ResultService<ExaminationTemplateBlockPageViewModel> GetExaminationTemplateBlocks(ExaminationTemplateBlockGetBindingModel model)
        {
            return _serviceETB.GetExaminationTemplateBlocks(model);
        }

        public ResultService<ExaminationTemplateBlockQuestionPageViewModel> GetExaminationTemplateBlockQuestions(ExaminationTemplateBlockQuestionGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception(string.Format("Нет доступа на чтение данных по записям {0}", _tagName));
                }

                int countPages = 0;
                var query = _context.ExaminationTemplateBlockQuestions.Where(ed => !ed.IsDeleted).AsQueryable();

                if (model.ExaminationTemplateBlockId.HasValue)
                {
                    query = query.Where(x => x.ExaminationTemplateBlockId == model.ExaminationTemplateBlockId);
                }

                if (model.Id.HasValue)
                {
                    query = query.Where(x => x.Id == model.Id);
                }

                query = query.OrderBy(x => x.QuestionNumber);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query
                    .Include(x => x.ExaminationTemplateBlock);

                var result = new ExaminationTemplateBlockQuestionPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(TicketModelFactoryToViewModel.CreateExaminationTemplateBlockQuestionViewModel).ToList()
                };

                return ResultService<ExaminationTemplateBlockQuestionPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<ExaminationTemplateBlockQuestionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationTemplateBlockQuestionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<ExaminationTemplateBlockQuestionViewModel> GetExaminationTemplateBlockQuestion(ExaminationTemplateBlockQuestionGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception(string.Format("Нет доступа на чтение данных по записям {0}", _tagName));
                }

                var entity = _context.ExaminationTemplateBlockQuestions
                                .Include(x => x.ExaminationTemplateBlock)
                                .FirstOrDefault(x => x.Id == model.Id && !x.IsDeleted);
                if (entity == null)
                {
                    return ResultService<ExaminationTemplateBlockQuestionViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<ExaminationTemplateBlockQuestionViewModel>.Success(TicketModelFactoryToViewModel.CreateExaminationTemplateBlockQuestionViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<ExaminationTemplateBlockQuestionViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationTemplateBlockQuestionViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateExaminationTemplateBlockQuestion(ExaminationTemplateBlockQuestionSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception(string.Format("Нет доступа на изменение данных по записям {0}", _tagName));
                }

                var entity = TicketModelFacotryFromBindingModel.CreateExaminationTemplateBlockQuestion(model);

                _context.ExaminationTemplateBlockQuestions.Add(entity);
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

        public ResultService UpdateExaminationTemplateBlockQuestion(ExaminationTemplateBlockQuestionSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception(string.Format("Нет доступа на изменение данных по записям {0}", _tagName));
                }

                var entity = _context.ExaminationTemplateBlockQuestions.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity = TicketModelFacotryFromBindingModel.CreateExaminationTemplateBlockQuestion(model, entity);

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

        public ResultService DeleteExaminationTemplateBlockQuestion(ExaminationTemplateBlockQuestionGetBindingModel model)
        {
            using (var transation = _context.Database.BeginTransaction())
            {
                try
                {
                    if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                    {
                        throw new Exception(string.Format("Нет доступа на удаление данных по записям {0}", _tagName));
                    }

                    var entity = _context.ExaminationTemplateBlockQuestions.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                    }
                    entity.IsDeleted = true;
                    entity.DateDelete = DateTime.Now;
                    _context.SaveChanges();

                    var examinationTemplateTicketQuestions = _context.ExaminationTemplateTicketQuestions.Where(x => x.ExaminationTemplateBlockQuestionId == entity.Id);
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