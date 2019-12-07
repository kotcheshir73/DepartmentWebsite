using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using DatabaseContext;
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
    public class ExaminationTemplateService : IExaminationTemplateService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.СоставлениеЭкзаменов;

        private readonly string _entity = "Составление Экзаменов";

        private readonly IDisciplineService _serviceD;

        private readonly IEducationDirectionService _serviceED;

        public ExaminationTemplateService(IDisciplineService serviceD, IEducationDirectionService serviceED)
        {
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.ExaminationTemplates.Where(x => !x.IsDeleted).AsQueryable();

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

                    query = query.Include(x => x.Discipline).Include(x => x.EducationDirection).Include(x => x.TicketTemplate);

                    var result = new ExaminationTemplatePageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ExaminationModelFactoryToViewModel.CreateExaminationTemplateViewModel).ToList()
                    };

                    return ResultService<ExaminationTemplatePageViewModel>.Success(result);
                }
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ExaminationTemplates
                                .Include(x => x.Discipline)
                                .Include(x => x.EducationDirection)
                                .Include(x => x.TicketTemplate)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<ExaminationTemplateViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<ExaminationTemplateViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<ExaminationTemplateViewModel>.Success(ExaminationModelFactoryToViewModel.CreateExaminationTemplateViewModel(entity));
                }
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ExaminationModelFacotryFromBindingModel.CreateExaminationTemplate(model);

                    var exsistEntity = context.ExaminationTemplates.FirstOrDefault(x => x.DisciplineId == entity.DisciplineId && x.EducationDirectionId == model.EducationDirectionId &&
                                            x.Semester == model.Semester);
                    if (exsistEntity == null)
                    {
                        context.ExaminationTemplates.Add(entity);
                        context.SaveChanges();
                        return ResultService.Success(entity.Id);
                    }
                    else
                    {
                        if (exsistEntity.IsDeleted)
                        {
                            exsistEntity.IsDeleted = false;
                            context.SaveChanges();
                            return ResultService.Success(exsistEntity.Id);
                        }
                        else
                        {
                            return ResultService.Error("Error:", "Элемент уже существует", ResultServiceStatusCode.ExsistItem);
                        }
                    }
                }
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ExaminationTemplates.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = ExaminationModelFacotryFromBindingModel.CreateExaminationTemplate(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteExaminationTemplate(ExaminationTemplateGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                using (var transation = context.Database.BeginTransaction())
                {
                    var entity = context.ExaminationTemplates.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity.IsDeleted = true;
                    entity.DateDelete = DateTime.Now;
                    context.SaveChanges();

                    var examinationTemplateBlocks = context.ExaminationTemplateBlocks.Where(x => x.ExaminationTemplateId == entity.Id);
                    foreach (var examinationTemplateBlock in examinationTemplateBlocks)
                    {
                        examinationTemplateBlock.IsDeleted = true;
                        examinationTemplateBlock.DateDelete = DateTime.Now;
                        context.SaveChanges();

                        var examinationTemplateBlockQuestions = context.ExaminationTemplateBlockQuestions.Where(x => x.ExaminationTemplateBlockId == examinationTemplateBlock.Id);
                        foreach (var examinationTemplateBlockQuestion in examinationTemplateBlockQuestions)
                        {
                            examinationTemplateBlockQuestion.IsDeleted = true;
                            examinationTemplateBlockQuestion.DateDelete = DateTime.Now;
                            context.SaveChanges();

                            var examinationTemplateTicketQuestions = context.ExaminationTemplateTicketQuestions.Where(x => x.ExaminationTemplateBlockQuestionId == examinationTemplateBlockQuestion.Id);
                            foreach (var examinationTemplateTicketQuestion in examinationTemplateTicketQuestions)
                            {
                                examinationTemplateTicketQuestion.IsDeleted = true;
                                examinationTemplateTicketQuestion.DateDelete = DateTime.Now;
                                context.SaveChanges();
                            }
                        }
                    }

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