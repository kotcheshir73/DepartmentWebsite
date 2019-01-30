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
    public class AcademicPlanRecordMissionService : IAcademicPlanRecordMissionService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        public AcademicPlanRecordMissionService(DepartmentDbContext context)
        {
            _context = context;
        }

        public ResultService<AcademicPlanRecordMissionPageViewModel> GetAcademicPlanRecordMissions(AcademicPlanRecordMissionGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                int countPages = 0;
                var query = _context.AcademicPlanRecordMissions.Where(aprm => !aprm.IsDeleted).AsQueryable();

                if(model.AcademicPlanRecordElementId.HasValue)
                {
                    query = query.Where(aprm => aprm.AcademicPlanRecordElementId == model.AcademicPlanRecordElementId);
                }
                if(model.LecturerId.HasValue)
                {
                    query = query.Where(aprm => aprm.LecturerId == model.LecturerId);
                }

                query = query.OrderBy(apre => apre.AcademicPlanRecordElementId).ThenBy(apre => apre.LecturerId);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                //query = query.Include(apre => apre.AcademicPlanRecordElement).Include(apre => apre.AcademicPlanRecordElement.).Include(apre => apre.TimeNorm); не понятно что с этим делать

                var result = new AcademicPlanRecordMissionPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateAcademicPlanRecordMissionViewModel).ToList()
                };

                return ResultService<AcademicPlanRecordMissionPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<AcademicPlanRecordMissionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<AcademicPlanRecordMissionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<AcademicPlanRecordMissionViewModel> GetAcademicPlanRecordMission(AcademicPlanRecordMissionGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                var entity = _context.AcademicPlanRecordMissions
                                .FirstOrDefault(aprm => aprm.Id == model.Id && !aprm.IsDeleted);
                if (entity == null)
                {
                    return ResultService<AcademicPlanRecordMissionViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<AcademicPlanRecordMissionViewModel>.Success(ModelFactoryToViewModel.CreateAcademicPlanRecordMissionViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<AcademicPlanRecordMissionViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<AcademicPlanRecordMissionViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateAcademicPlanRecordMission(AcademicPlanRecordMissionSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = ModelFacotryFromBindingModel.CreateAcademicPlanRecordMission(model);

                _context.AcademicPlanRecordMissions.Add(entity);
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

        public ResultService UpdateAcademicPlanRecordMission(AcademicPlanRecordMissionSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = _context.AcademicPlanRecordMissions.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                //_context.AcademicPlanRecordMissions.Attach(entity);   //не уверен что так правильно

                entity = ModelFacotryFromBindingModel.CreateAcademicPlanRecordMission(model, entity);

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

        public ResultService DeleteAcademicPlanRecordMission(AcademicPlanRecordMissionGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по элементам записей учебного плана");
                }

                var entity = _context.AcademicPlanRecordMissions.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
