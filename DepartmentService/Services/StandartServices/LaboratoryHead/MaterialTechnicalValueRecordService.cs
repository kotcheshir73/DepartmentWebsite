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
    public class MaterialTechnicalValueRecordService : IMaterialTechnicalValueRecordService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.МатериальноТехническиеЦенности;

        private readonly string _titleService = "записям материально-технической ценности";

        private readonly IMaterialTechnicalValueService _serviceMTV;

        private readonly IMaterialTechnicalValueGroupService _serviceMTVG;

        public MaterialTechnicalValueRecordService(DepartmentDbContext context, IMaterialTechnicalValueService serviceMTV,
            IMaterialTechnicalValueGroupService serviceMTVG)
        {
            _context = context;
            _serviceMTV = serviceMTV;
            _serviceMTVG = serviceMTVG;
        }


        public ResultService<MaterialTechnicalValuePageViewModel> GetMaterialTechnicalValues(MaterialTechnicalValueGetBindingModel model)
        {
            return _serviceMTV.GetMaterialTechnicalValues(model);
        }

        public ResultService<MaterialTechnicalValueGroupPageViewModel> GetMaterialTechnicalValueGroups(MaterialTechnicalValueGroupGetBindingModel model)
        {
            return _serviceMTVG.GetMaterialTechnicalValueGroups(model);
        }


        public ResultService<MaterialTechnicalValueRecordPageViewModel> GetMaterialTechnicalValueRecords(MaterialTechnicalValueRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception(string.Format("Нет доступа на чтение данных по {0}", _titleService));
                }

                int countPages = 0;
                var query = _context.MaterialTechnicalValueRecords.Where(x => !x.IsDeleted).AsQueryable();

                if (model.MaterialTechnicalValueId.HasValue)
                {
                    query = query.Where(x => x.MaterialTechnicalValueId == model.MaterialTechnicalValueId);
                }

                if (model.MaterialTechnicalValueGroupId.HasValue)
                {
                    query = query.Where(x => x.MaterialTechnicalValueGroupId == model.MaterialTechnicalValueGroupId);
                }

                query = query.OrderBy(x => x.MaterialTechnicalValueGroup.Order).ThenBy(x => x.Order);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query.Include(x => x.MaterialTechnicalValue).Include(x => x.MaterialTechnicalValueGroup);

                var result = new MaterialTechnicalValueRecordPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateMaterialTechnicalValueRecordViewModel).ToList()
                };

                return ResultService<MaterialTechnicalValueRecordPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<MaterialTechnicalValueRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<MaterialTechnicalValueRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<MaterialTechnicalValueRecordViewModel> GetMaterialTechnicalValueRecord(MaterialTechnicalValueRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception(string.Format("Нет доступа на чтение данных по {0}", _titleService));
                }

                var entity = _context.MaterialTechnicalValueRecords
                                .Include(x => x.MaterialTechnicalValue)
                                .Include(x => x.MaterialTechnicalValueGroup)
                                .FirstOrDefault(ap => ap.Id == model.Id && !ap.IsDeleted);
                if (entity == null)
                {
                    return ResultService<MaterialTechnicalValueRecordViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<MaterialTechnicalValueRecordViewModel>.Success(ModelFactoryToViewModel.CreateMaterialTechnicalValueRecordViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<MaterialTechnicalValueRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<MaterialTechnicalValueRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateMaterialTechnicalValueRecord(MaterialTechnicalValueRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception(string.Format("Нет доступа на изменение данных по {0}", _titleService));
                }

                var entity = ModelFacotryFromBindingModel.CreateMaterialTechnicalValueRecord(model);

                _context.MaterialTechnicalValueRecords.Add(entity);
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

        public ResultService UpdateMaterialTechnicalValueRecord(MaterialTechnicalValueRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception(string.Format("Нет доступа на изменение данных по {0}", _titleService));
                }

                var entity = _context.MaterialTechnicalValueRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                entity = ModelFacotryFromBindingModel.CreateMaterialTechnicalValueRecord(model, entity);

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

        public ResultService DeleteMaterialTechnicalValueRecord(MaterialTechnicalValueRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception(string.Format("Нет доступа на удаление данных по {0}", _titleService));
                }

                var entity = _context.MaterialTechnicalValueRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
