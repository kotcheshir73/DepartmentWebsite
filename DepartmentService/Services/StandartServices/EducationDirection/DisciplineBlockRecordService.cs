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
    public class DisciplineBlockRecordService : IDisciplineBlockRecordService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Дисциплины;

        private readonly string _operationTitle = "записям блока дисциплин";

        private readonly IDisciplineBlockService _serviceDB;

        private readonly IAcademicYearService _serviceAY;

        private readonly IEducationDirectionService _serviceED;

        private readonly ITimeNormService _serviceTN;

        public DisciplineBlockRecordService(DepartmentDbContext context, IDisciplineBlockService serviceDB, IEducationDirectionService serviceED, 
            IAcademicYearService serviceAY, ITimeNormService serviceTN)
        {
            _context = context;
            _serviceDB = serviceDB;
            _serviceAY = serviceAY;
            _serviceED = serviceED;
            _serviceTN = serviceTN;
        }

        public ResultService<DisciplineBlockPageViewModel> GetDisciplineBlocks(DisciplineBlockGetBindingModel model)
        {
            return _serviceDB.GetDisciplineBlocks(model);
        }

        public ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model)
        {
            return _serviceAY.GetAcademicYears(model);
        }

        public ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model)
        {
            return _serviceED.GetEducationDirections(model);
        }

        public ResultService<TimeNormPageViewModel> GetTimeNorms(TimeNormGetBindingModel model)
        {
            return _serviceTN.GetTimeNorms(model);
        }


        public ResultService<DisciplineBlockRecordPageViewModel> GetDisciplineBlockRecords(DisciplineBlockRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception(string.Format("Нет доступа на чтение данных по {0}", _operationTitle));
                }

                int countPages = 0;
                var query = _context.DisciplineBlockRecords.Where(x => !x.IsDeleted).AsQueryable();

                if (model.AcademicYearId.HasValue)
                {
                    query = query.Where(x => x.AcademicYearId == model.AcademicYearId);
                }
                if (model.DisciplineBlockId.HasValue)
                {
                    query = query.Where(x => x.DisciplineBlockId == model.DisciplineBlockId);
                }

                query = query.OrderBy(x => x.DisciplineBlockId).ThenBy(x => x.DisciplineBlockRecordTitle);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query.Include(x => x.DisciplineBlock).Include(x => x.AcademicYear).Include(x => x.EducationDirection).Include(x => x.TimeNorm);

                var result = new DisciplineBlockRecordPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateDisciplineBlockRecordViewModel).ToList()
                };

                return ResultService<DisciplineBlockRecordPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<DisciplineBlockRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineBlockRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<DisciplineBlockRecordViewModel> GetDisciplineBlockRecord(DisciplineBlockRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception(string.Format("Нет доступа на чтение данных по {0}", _operationTitle));
                }

                var entity = _context.DisciplineBlockRecords
                                .Include(x => x.DisciplineBlock)
                                .Include(x => x.AcademicYear)
                                .Include(x => x.EducationDirection)
                                .Include(x => x.TimeNorm)
                                .FirstOrDefault(x => x.Id == model.Id && !x.IsDeleted);
                if (entity == null)
                {
                    return ResultService<DisciplineBlockRecordViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<DisciplineBlockRecordViewModel>.Success(ModelFactoryToViewModel.CreateDisciplineBlockRecordViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<DisciplineBlockRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineBlockRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateDisciplineBlockRecord(DisciplineBlockRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception(string.Format("Нет доступа на изменение данных по {0}", _operationTitle));
                }

                var entity = ModelFacotryFromBindingModel.CreateDisciplineBlockRecord(model);

                _context.DisciplineBlockRecords.Add(entity);
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

        public ResultService UpdateDisciplineBlockRecord(DisciplineBlockRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception(string.Format("Нет доступа на изменение данных по {0}", _operationTitle));
                }

                var entity = _context.DisciplineBlockRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                entity = ModelFacotryFromBindingModel.CreateDisciplineBlockRecord(model, entity);

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

        public ResultService DeleteDisciplineBlockRecord(DisciplineBlockRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception(string.Format("Нет доступа на удаление данных по {0}", _operationTitle));
                }

                var entity = _context.DisciplineBlockRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
