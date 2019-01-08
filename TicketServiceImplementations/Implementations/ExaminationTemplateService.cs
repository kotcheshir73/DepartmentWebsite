using DepartmentContext;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using TicketServiceImplementations;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketServiceInterfaces.ViewModels;

namespace TicketServiceImplementation.Implementations
{
    public class ExaminationTemplateService : IExaminationTemplateService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.СоставлениеЭкзаменов;

        private readonly string _tagName = "экзаменам";

        private readonly IDisciplineService _serviceD;

        private readonly IEducationDirectionService _serviceED;

        public ExaminationTemplateService(DepartmentDbContext context, IDisciplineService serviceD, IEducationDirectionService serviceED)
        {
            _context = context;
            _serviceD = serviceD;
            _serviceED = serviceED;
        }

        public ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model)
        {
            return _serviceD.GetDisciplines(model);
        }

        public ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model)
        {
            return _serviceED.GetEducationDirections(model);
        }

        public ResultService<ExaminationTemplatePageViewModel> GetExaminationTemplates(ExaminationTemplateGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception(string.Format("Нет доступа на чтение данных по записям {0}", _tagName));
                }

                int countPages = 0;
                var query = _context.ExaminationTemplates.Where(ed => !ed.IsDeleted).AsQueryable();

                if (model.DisciplineId.HasValue)
                {
                    query = query.Where(x => x.DisciplineId == model.DisciplineId);
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
                    .Include(x => x.Discipline)
                    .Include(x => x.EducationDirection)
                    .Include(x => x.TicketTemplate);

                var result = new ExaminationTemplatePageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(TicketModelFactoryToViewModel.CreateExaminationTemplateViewModel).ToList()
                };

                return ResultService<ExaminationTemplatePageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<ExaminationTemplatePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationTemplatePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<ExaminationTemplateViewModel> GetExaminationTemplate(ExaminationTemplateGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception(string.Format("Нет доступа на чтение данных по записям {0}", _tagName));
                }

                var entity = _context.ExaminationTemplates
                                .Include(x => x.Discipline)
                                .Include(x => x.EducationDirection)
                                .Include(x => x.TicketTemplate)
                                .FirstOrDefault(x => x.Id == model.Id && !x.IsDeleted);
                if (entity == null)
                {
                    return ResultService<ExaminationTemplateViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<ExaminationTemplateViewModel>.Success(TicketModelFactoryToViewModel.CreateExaminationTemplateViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<ExaminationTemplateViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationTemplateViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateExaminationTemplate(ExaminationTemplateSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception(string.Format("Нет доступа на изменение данных по записям {0}", _tagName));
                }

                var entity = TicketModelFacotryFromBindingModel.CreateExaminationTemplate(model);

                _context.ExaminationTemplates.Add(entity);
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

        public ResultService UpdateExaminationTemplate(ExaminationTemplateSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception(string.Format("Нет доступа на изменение данных по записям {0}", _tagName));
                }

                var entity = _context.ExaminationTemplates.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity = TicketModelFacotryFromBindingModel.CreateExaminationTemplate(model, entity);

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

        public ResultService DeleteExaminationTemplate(ExaminationTemplateGetBindingModel model)
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

                    var examinationTemplateBlocks = _context.ExaminationTemplateBlocks.Where(x => x.ExaminationTemplateId == entity.Id);
                    foreach (var examinationTemplateBlock in examinationTemplateBlocks)
                    {
                        examinationTemplateBlock.IsDeleted = true;
                        examinationTemplateBlock.DateDelete = DateTime.Now;
                        _context.SaveChanges();

                        var examinationTemplateBlockQuestions = _context.ExaminationTemplateBlockQuestions.Where(x => x.ExaminationTemplateBlockId == examinationTemplateBlock.Id);
                        foreach(var examinationTemplateBlockQuestion in examinationTemplateBlockQuestions)
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