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
    public class SoftwareRecordService : ISoftwareRecordService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.УстановленоеПО;

        private readonly IMaterialTechnicalValueService _serviceM;

        public SoftwareRecordService(DepartmentDbContext context, IMaterialTechnicalValueService serviceM)
        {
            _context = context;
            _serviceM = serviceM;
        }


        public ResultService<MaterialTechnicalValuePageViewModel> GetMaterialTechnicalValues(MaterialTechnicalValueGetBindingModel model)
        {
            return _serviceM.GetMaterialTechnicalValues(model);
        }


        public ResultService<SoftwareRecordPageViewModel> GetSoftwareRecords(SoftwareRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по установленному ПО");
                }

                int countPages = 0;
                var query = _context.SoftwareRecords.Where(sr => !sr.IsDeleted).AsQueryable();

                if (model.MaterialTechnicalValueId.HasValue)
                {
                    query = query.Where(sr => sr.MaterialTechnicalValueId == model.MaterialTechnicalValueId);
                }

                query = query.OrderBy(sr => sr.MaterialTechnicalValue.InventoryNumber).ThenBy(sr => sr.SoftwareName).ThenBy(sr => sr.DateCreate);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query.Include(sr => sr.MaterialTechnicalValue);

                var result = new SoftwareRecordPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateSoftwareRecordViewModel).ToList()
                };

                return ResultService<SoftwareRecordPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<SoftwareRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<SoftwareRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<SoftwareRecordViewModel> GetSoftwareRecord(SoftwareRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по установленному ПО");
                }

                var entity = _context.SoftwareRecords
                                .Include(ap => ap.MaterialTechnicalValue)
                                .FirstOrDefault(ap => ap.Id == model.Id && !ap.IsDeleted);
                if (entity == null)
                {
                    return ResultService<SoftwareRecordViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<SoftwareRecordViewModel>.Success(ModelFactoryToViewModel.CreateSoftwareRecordViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<SoftwareRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<SoftwareRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateSoftwareRecord(SoftwareRecordRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по установленному ПО");
                }

                var entity = ModelFacotryFromBindingModel.CreateSoftwareRecord(model);

                _context.SoftwareRecords.Add(entity);
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

        public ResultService UpdateSoftwareRecord(SoftwareRecordRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по установленному ПО");
                }

                var entity = _context.SoftwareRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                entity = ModelFacotryFromBindingModel.CreateSoftwareRecord(model, entity);

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

        public ResultService DeleteSoftwareRecord(SoftwareRecordRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по установленному ПО");
                }

                var entity = _context.SoftwareRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
