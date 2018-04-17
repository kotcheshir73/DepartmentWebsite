using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.Context;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
    public class MaterialTechnicalValueGroupService : IMaterialTechnicalValueGroupService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.МатериальноТехническиеЦенности;

        private readonly string _titleService = "группам материально-техническим ценностей";

        public MaterialTechnicalValueGroupService(DepartmentDbContext context)
        {
            _context = context;
        }

        public ResultService<MaterialTechnicalValueGroupPageViewModel> GetMaterialTechnicalValueGroups(MaterialTechnicalValueGroupGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception(string.Format("Нет доступа на чтение данных по {0}", _titleService));
                }

                int countPages = 0;
                var query = _context.MaterialTechnicalValueGroups.Where(ap => !ap.IsDeleted).AsQueryable();

                query = query.OrderBy(x => x.Order);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                var result = new MaterialTechnicalValueGroupPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateMaterialTechnicalValueGroupViewModel).ToList()
                };

                return ResultService<MaterialTechnicalValueGroupPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<MaterialTechnicalValueGroupPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<MaterialTechnicalValueGroupPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<MaterialTechnicalValueGroupViewModel> GetMaterialTechnicalValueGroup(MaterialTechnicalValueGroupGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception(string.Format("Нет доступа на чтение данных по {0}", _titleService));
                }

                var entity = _context.MaterialTechnicalValueGroups
                                .FirstOrDefault(ap => ap.Id == model.Id && !ap.IsDeleted);
                if (entity == null)
                {
                    return ResultService<MaterialTechnicalValueGroupViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<MaterialTechnicalValueGroupViewModel>.Success(ModelFactoryToViewModel.CreateMaterialTechnicalValueGroupViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<MaterialTechnicalValueGroupViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<MaterialTechnicalValueGroupViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateMaterialTechnicalValueGroup(MaterialTechnicalValueGroupRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception(string.Format("Нет доступа на изменение данных по {0}", _titleService));
                }

                var entity = ModelFacotryFromBindingModel.CreateMaterialTechnicalValueGroup(model);

                _context.MaterialTechnicalValueGroups.Add(entity);
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

        public ResultService UpdateMaterialTechnicalValueGroup(MaterialTechnicalValueGroupRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception(string.Format("Нет доступа на изменение данных по {0}", _titleService));
                }

                var entity = _context.MaterialTechnicalValueGroups.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                entity = ModelFacotryFromBindingModel.CreateMaterialTechnicalValueGroup(model, entity);

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

        public ResultService DeleteMaterialTechnicalValueGroup(MaterialTechnicalValueGroupRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception(string.Format("Нет доступа на удаление данных по {0}", _titleService));
                }

                var entity = _context.MaterialTechnicalValueGroups.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
