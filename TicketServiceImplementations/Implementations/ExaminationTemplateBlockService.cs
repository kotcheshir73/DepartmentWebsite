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
    public class ExaminationTemplateBlockService : IExaminationTemplateBlockService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.СоставлениеЭкзаменов;

        private readonly string _tagName = "блоков экзамена";

        private readonly IExaminationTemplateService _serviceET;

        public ExaminationTemplateBlockService(DepartmentDbContext context, IExaminationTemplateService serviceET)
        {
            _context = context;
            _serviceET = serviceET;
        }

        public ResultService<ExaminationTemplatePageViewModel> GetExaminationTemplates(ExaminationTemplateGetBindingModel model)
        {
            return _serviceET.GetExaminationTemplates(model);
        }

        public ResultService<ExaminationTemplateBlockPageViewModel> GetExaminationTemplateBlocks(ExaminationTemplateBlockGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception(string.Format("Нет доступа на чтение данных по записям {0}", _tagName));
                }

                int countPages = 0;
                var query = _context.ExaminationTemplateBlocks.Where(ed => !ed.IsDeleted).AsQueryable();

                if (model.ExaminationTemplateId.HasValue)
                {
                    query = query.Where(x => x.ExaminationTemplateId == model.ExaminationTemplateId);
                }

                if (model.Id.HasValue)
                {
                    query = query.Where(x => x.Id == model.Id);
                }

                query = query.OrderBy(x => x.BlockName);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query
                    .Include(x => x.ExaminationTemplate);

                var result = new ExaminationTemplateBlockPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(TicketModelFactoryToViewModel.CreateExaminationTemplateBlockViewModel).ToList()
                };

                return ResultService<ExaminationTemplateBlockPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<ExaminationTemplateBlockPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationTemplateBlockPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<ExaminationTemplateBlockViewModel> GetExaminationTemplateBlock(ExaminationTemplateBlockGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception(string.Format("Нет доступа на чтение данных по записям {0}", _tagName));
                }

                var entity = _context.ExaminationTemplateBlocks
                                .Include(apr => apr.ExaminationTemplate)
                                .FirstOrDefault(x => x.Id == model.Id && !x.IsDeleted);
                if (entity == null)
                {
                    return ResultService<ExaminationTemplateBlockViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<ExaminationTemplateBlockViewModel>.Success(TicketModelFactoryToViewModel.CreateExaminationTemplateBlockViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<ExaminationTemplateBlockViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationTemplateBlockViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateExaminationTemplateBlock(ExaminationTemplateBlockSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception(string.Format("Нет доступа на изменение данных по записям {0}", _tagName));
                }

                var entity = TicketModelFacotryFromBindingModel.CreateExaminationTemplateBlock(model);

                _context.ExaminationTemplateBlocks.Add(entity);
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

        public ResultService UpdateExaminationTemplateBlock(ExaminationTemplateBlockSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception(string.Format("Нет доступа на изменение данных по записям {0}", _tagName));
                }

                var entity = _context.ExaminationTemplateBlocks.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity = TicketModelFacotryFromBindingModel.CreateExaminationTemplateBlock(model, entity);

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

        public ResultService DeleteExaminationTemplateBlock(ExaminationTemplateBlockGetBindingModel model)
        {
            using (var transation = _context.Database.BeginTransaction())
            {
                try
                {
                    if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                    {
                        throw new Exception(string.Format("Нет доступа на удаление данных по записям {0}", _tagName));
                    }

                    var entity = _context.ExaminationTemplateBlocks.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                    }
                    entity.IsDeleted = true;
                    entity.DateDelete = DateTime.Now;
                    _context.SaveChanges();

                    var examinationTemplateBlockQuestions = _context.ExaminationTemplateBlockQuestions.Where(x => x.ExaminationTemplateBlockId == entity.Id);
                    foreach (var examinationTemplateBlockQuestion in examinationTemplateBlockQuestions)
                    {
                        examinationTemplateBlockQuestion.IsDeleted = true;
                        examinationTemplateBlockQuestion.DateDelete = DateTime.Now;
                        _context.SaveChanges();

                        var examinationTemplateTicketQuestions = _context.ExaminationTemplateTicketQuestions.Where(x => x.ExaminationTemplateBlockQuestionId == examinationTemplateBlockQuestion.Id);
                        foreach (var examinationTemplateTicketQuestion in examinationTemplateTicketQuestions)
                        {
                            examinationTemplateTicketQuestion.IsDeleted = true;
                            examinationTemplateTicketQuestion.DateDelete = DateTime.Now;
                            _context.SaveChanges();
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
}