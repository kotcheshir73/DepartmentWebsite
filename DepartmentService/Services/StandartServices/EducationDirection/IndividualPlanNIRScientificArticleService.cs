using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.Context;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
    public class IndividualPlanNIRScientificArticleService : IIndividualPlanNIRScientificArticleService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        public IndividualPlanNIRScientificArticleService(DepartmentDbContext context)
        {
            _context = context;
        }

        public ResultService<IndividualPlanNIRScientificArticlePageViewModel> GetIndividualPlanNIRScientificArticles(IndividualPlanNIRScientificArticleGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                int countPages = 0;
                var query = _context.IndividualPlanNIRScientificArticles.Where(record => !record.IsDeleted).AsQueryable();
                
                if(model.LecturerId.HasValue)
                {
                    query = query.Where(aprm => aprm.LecturerId == model.LecturerId);
                }
                if (model.Status != null)
                {
                    query = query.Where(aprm => aprm.Status.Equals(model.Status));
                }

                query = query.OrderBy(apre => apre.Name);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query.Include(apre => apre.Lecturer); // для вложенных запросов
                
                var result = new IndividualPlanNIRScientificArticlePageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateIndividualPlanNIRScientificArticleViewModel).ToList() 
                };

                return ResultService<IndividualPlanNIRScientificArticlePageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<IndividualPlanNIRScientificArticlePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<IndividualPlanNIRScientificArticlePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<IndividualPlanNIRScientificArticleViewModel> GetIndividualPlanNIRScientificArticle(IndividualPlanNIRScientificArticleGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                var entity = _context.IndividualPlanNIRScientificArticles
                                .FirstOrDefault(aprm => aprm.Id == model.Id && !aprm.IsDeleted);
                if (entity == null)
                {
                    return ResultService<IndividualPlanNIRScientificArticleViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<IndividualPlanNIRScientificArticleViewModel>.Success(ModelFactoryToViewModel.CreateIndividualPlanNIRScientificArticleViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<IndividualPlanNIRScientificArticleViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<IndividualPlanNIRScientificArticleViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateIndividualPlanNIRScientificArticle(IndividualPlanNIRScientificArticleSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = ModelFacotryFromBindingModel.CreateIndividualPlanNIRScientificArticle(model);

                _context.IndividualPlanNIRScientificArticles.Add(entity);
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

        public ResultService UpdateIndividualPlanNIRScientificArticle(IndividualPlanNIRScientificArticleSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = _context.IndividualPlanNIRScientificArticles.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                //_context.IndividualPlanRecords.Attach(entity);   //не уверен что так правильно

                entity = ModelFacotryFromBindingModel.CreateIndividualPlanNIRScientificArticle(model, entity);

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

        public ResultService DeleteIndividualPlanNIRScientificArticle(IndividualPlanNIRScientificArticleGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по элементам записей учебного плана");
                }

                var entity = _context.IndividualPlanNIRScientificArticles.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity.IsDeleted = true;
                entity.DateDelete = DateTime.Now;

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
    }
}
