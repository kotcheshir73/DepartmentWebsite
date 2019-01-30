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
    public class IndividualPlanNIRContractualWorkService : IIndividualPlanNIRContractualWorkService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        public IndividualPlanNIRContractualWorkService(DepartmentDbContext context)
        {
            _context = context;
        }

        public ResultService<IndividualPlanNIRContractualWorkPageViewModel> GetIndividualPlanNIRContractualWorks(IndividualPlanNIRContractualWorkGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                int countPages = 0;
                var query = _context.IndividualPlanNIRContractualWorks.Where(record => !record.IsDeleted).AsQueryable();
                
                if(model.LecturerId.HasValue)
                {
                    query = query.Where(aprm => aprm.LecturerId == model.LecturerId);
                }

                query = query.OrderBy(apre => apre.JobContent);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query.Include(apre => apre.Lecturer);
                
                var result = new IndividualPlanNIRContractualWorkPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateIndividualPlanNIRContractualWorkViewModel).ToList() 
                };

                return ResultService<IndividualPlanNIRContractualWorkPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<IndividualPlanNIRContractualWorkPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<IndividualPlanNIRContractualWorkPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<IndividualPlanNIRContractualWorkViewModel> GetIndividualPlanNIRContractualWork(IndividualPlanNIRContractualWorkGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                var entity = _context.IndividualPlanNIRContractualWorks
                                .FirstOrDefault(aprm => aprm.Id == model.Id && !aprm.IsDeleted);
                if (entity == null)
                {
                    return ResultService<IndividualPlanNIRContractualWorkViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<IndividualPlanNIRContractualWorkViewModel>.Success(ModelFactoryToViewModel.CreateIndividualPlanNIRContractualWorkViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<IndividualPlanNIRContractualWorkViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<IndividualPlanNIRContractualWorkViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateIndividualPlanNIRContractualWork(IndividualPlanNIRContractualWorkSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = ModelFacotryFromBindingModel.CreateIndividualPlanNIRContractualWork(model);

                _context.IndividualPlanNIRContractualWorks.Add(entity);
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

        public ResultService UpdateIndividualPlanNIRContractualWork(IndividualPlanNIRContractualWorkSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = _context.IndividualPlanNIRContractualWorks.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                entity = ModelFacotryFromBindingModel.CreateIndividualPlanNIRContractualWork(model, entity);

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

        public ResultService DeleteIndividualPlanNIRContractualWork(IndividualPlanNIRContractualWorkGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по элементам записей учебного плана");
                }

                var entity = _context.IndividualPlanNIRContractualWorks.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
