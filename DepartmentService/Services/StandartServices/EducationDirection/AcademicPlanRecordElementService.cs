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
    public class AcademicPlanRecordElementService : IAcademicPlanRecordElementService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        private readonly IAcademicPlanRecordService _serviceAPR;

        private readonly ITimeNormService _serviceTN;

        public AcademicPlanRecordElementService(DepartmentDbContext context, IAcademicPlanRecordService serviceAPR,
           ITimeNormService serviceTN)
        {
            _context = context;
            _serviceAPR = serviceAPR;
            _serviceTN = serviceTN;
        }

        public ResultService<AcademicPlanRecordPageViewModel> GetAcademicPlanRecords(AcademicPlanRecordGetBindingModel model)
        {
            return _serviceAPR.GetAcademicPlanRecords(model);
        }

        public ResultService<TimeNormPageViewModel> GetTimeNorms(TimeNormGetBindingModel model)
        {
            return _serviceTN.GetTimeNorms(model);
        }


        public ResultService<AcademicPlanRecordElementPageViewModel> GetAcademicPlanRecordElements(AcademicPlanRecordElementGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                int countPages = 0;
                var query = _context.AcademicPlanRecordElements.Where(apre => !apre.IsDeleted).AsQueryable();

                if(model.AcademicPlanRecordId.HasValue)
                {
                    query = query.Where(apre => apre.AcademicPlanRecordId == model.AcademicPlanRecordId);
                }
                if(model.TimeNormId.HasValue)
                {
                    query = query.Where(apre => apre.TimeNormId == model.TimeNormId);
                }

                query = query.OrderBy(apre => apre.AcademicPlanRecordId).ThenBy(apre => apre.TimeNormId);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query.Include(apre => apre.AcademicPlanRecord).Include(apre => apre.AcademicPlanRecord.Discipline).Include(apre => apre.TimeNorm);

                var result = new AcademicPlanRecordElementPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateAcademicPlanRecordElementViewModel).ToList()
                };

                return ResultService<AcademicPlanRecordElementPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<AcademicPlanRecordElementPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<AcademicPlanRecordElementPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<AcademicPlanRecordElementViewModel> GetAcademicPlanRecordElement(AcademicPlanRecordElementGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                var entity = _context.AcademicPlanRecordElements
                                .FirstOrDefault(apre => apre.Id == model.Id && !apre.IsDeleted);
                if (entity == null)
                {
                    return ResultService<AcademicPlanRecordElementViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<AcademicPlanRecordElementViewModel>.Success(ModelFactoryToViewModel.CreateAcademicPlanRecordElementViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<AcademicPlanRecordElementViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<AcademicPlanRecordElementViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateAcademicPlanRecordElement(AcademicPlanRecordElementRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = ModelFacotryFromBindingModel.CreateAcademicPlanRecordElement(model);

                _context.AcademicPlanRecordElements.Add(entity);
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

        public ResultService UpdateAcademicPlanRecordElement(AcademicPlanRecordElementRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = _context.AcademicPlanRecordElements.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity = ModelFacotryFromBindingModel.CreateAcademicPlanRecordElement(model, entity);

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

        public ResultService DeleteAcademicPlanRecordElement(AcademicPlanRecordElementGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по элементам записей учебного плана");
                }

                var entity = _context.AcademicPlanRecordElements.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
