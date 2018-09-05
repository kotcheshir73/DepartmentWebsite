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
    public class IndividualPlanRecordService : IIndividualPlanRecordService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        public IndividualPlanRecordService(DepartmentDbContext context)
        {
            _context = context;
        }

        public ResultService<IndividualPlanRecordPageViewModel> GetIndividualPlanRecords(IndividualPlanRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                int countPages = 0;
                var query = _context.IndividualPlanRecords.Where(aprm => !aprm.IsDeleted).AsQueryable();

                if(model.IndividualPlanKindOfWorkId.HasValue)
                {
                    query = query.Where(aprm => aprm.IndividualPlanKindOfWorkId == model.IndividualPlanKindOfWorkId);
                }
                if(model.LecturerId.HasValue)
                {
                    query = query.Where(aprm => aprm.LecturerId == model.LecturerId);
                }

                query = query.OrderBy(apre => apre.IndividualPlanKindOfWorkId).ThenBy(apre => apre.LecturerId);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                //query = query.Include(apre => apre.AcademicPlanRecordElement).Include(apre => apre.AcademicPlanRecordElement.).Include(apre => apre.TimeNorm); не понятно что с этим делать

                var result = new IndividualPlanRecordPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateIndividualPlanRecordViewModel).ToList()
                };

                return ResultService<IndividualPlanRecordPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<IndividualPlanRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<IndividualPlanRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<IndividualPlanRecordViewModel> GetIndividualPlanRecord(IndividualPlanRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                var entity = _context.IndividualPlanRecords
                                .FirstOrDefault(aprm => aprm.Id == model.Id && !aprm.IsDeleted);
                if (entity == null)
                {
                    return ResultService<IndividualPlanRecordViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<IndividualPlanRecordViewModel>.Success(ModelFactoryToViewModel.CreateIndividualPlanRecordViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<IndividualPlanRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<IndividualPlanRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateIndividualPlanRecord(IndividualPlanRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = ModelFacotryFromBindingModel.CreateIndividualPlanRecord(model);

                _context.IndividualPlanRecords.Add(entity);
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

        public ResultService UpdateIndividualPlanRecord(IndividualPlanRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = _context.IndividualPlanRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                //_context.IndividualPlanRecords.Attach(entity);   //не уверен что так правильно

                entity = ModelFacotryFromBindingModel.CreateIndividualPlanRecord(model, entity);

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

        public ResultService DeleteIndividualPlanRecord(IndividualPlanRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по элементам записей учебного плана");
                }

                var entity = _context.IndividualPlanRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
