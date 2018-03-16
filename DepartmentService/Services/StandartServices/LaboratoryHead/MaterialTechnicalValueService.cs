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
    public class MaterialTechnicalValueService : IMaterialTechnicalValueService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.МатериальноТехническиеЦенности;

        private readonly IClassroomService _serviceC;

        public MaterialTechnicalValueService(DepartmentDbContext context, IClassroomService serviceC)
        {
            _context = context;
            _serviceC = serviceC;
        }


        public ResultService<ClassroomPageViewModel> GetClassrooms(ClassroomGetBindingModel model)
        {
            return _serviceC.GetClassrooms(model);
        }


        public ResultService<MaterialTechnicalValuePageViewModel> GetMaterialTechnicalValues(MaterialTechnicalValueGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по материально-техническим ценностям");
                }

                int countPages = 0;
                var query = _context.MaterialTechnicalValues.Where(ap => !ap.IsDeleted).AsQueryable();

                if (model.ClassroomId.HasValue)
                {
                    query = query.Where(ap => ap.ClassroomId == model.ClassroomId);
                }

                query = query.OrderBy(ap => ap.Classroom.Number).ThenBy(ap => ap.InventoryNumber).ThenBy(ap => ap.DateCreate);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query.Include(ap => ap.Classroom);

                var result = new MaterialTechnicalValuePageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateMaterialTechnicalValueViewModel).ToList()
                };

                return ResultService<MaterialTechnicalValuePageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<MaterialTechnicalValuePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<MaterialTechnicalValuePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<MaterialTechnicalValueViewModel> GetMaterialTechnicalValue(MaterialTechnicalValueGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по материально-техническим ценностям");
                }

                var entity = _context.MaterialTechnicalValues
                                .Include(ap => ap.Classroom)
                                .FirstOrDefault(ap => ap.Id == model.Id && !ap.IsDeleted);
                if (entity == null)
                {
                    return ResultService<MaterialTechnicalValueViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<MaterialTechnicalValueViewModel>.Success(ModelFactoryToViewModel.CreateMaterialTechnicalValueViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<MaterialTechnicalValueViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<MaterialTechnicalValueViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateMaterialTechnicalValue(MaterialTechnicalValueRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по материально-техническим ценностям");
                }

                var entity = ModelFacotryFromBindingModel.CreateMaterialTechnicalValue(model);

                _context.MaterialTechnicalValues.Add(entity);
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

        public ResultService UpdateMaterialTechnicalValue(MaterialTechnicalValueRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по материально-техническим ценностям");
                }

                var entity = _context.MaterialTechnicalValues.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                entity = ModelFacotryFromBindingModel.CreateMaterialTechnicalValue(model, entity);

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

        public ResultService DeleteMaterialTechnicalValue(MaterialTechnicalValueRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по материально-техническим ценностям");
                }

                var entity = _context.MaterialTechnicalValues.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity.IsDeleted = true;
                entity.DateDelete = DateTime.Now;
                entity.DeleteReason = model.DeleteReason;

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
