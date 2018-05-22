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
    public class StreamLessonRecordService : IStreamLessonRecordService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        private readonly string _operationTitle = "записям потока";

        private readonly IStreamLessonService _serviceS;

        private readonly IAcademicPlanService _serviceAP;

        private readonly IAcademicPlanRecordService _serviceAPR;

        private readonly IAcademicPlanRecordElementService _serviceAPRE;

        public StreamLessonRecordService(DepartmentDbContext context, IStreamLessonService serviceS, IAcademicPlanService serviceAP,
            IAcademicPlanRecordService serviceAPR, IAcademicPlanRecordElementService serviceAPRE)
        {
            _context = context;
            _serviceS = serviceS;
            _serviceAP = serviceAP;
            _serviceAPR = serviceAPR;
            _serviceAPRE = serviceAPRE;
        }
        

        public ResultService<StreamLessonPageViewModel> GetStreamLessons(StreamLessonGetBindingModel model)
        {
            return _serviceS.GetStreamLessons(model);
        }

        public ResultService<AcademicPlanPageViewModel> GetAcademicPlans(AcademicPlanGetBindingModel model)
        {
            return _serviceAP.GetAcademicPlans(model);
        }

        public ResultService<AcademicPlanRecordPageViewModel> GetAcademicPlanRecords(AcademicPlanRecordGetBindingModel model)
        {
            return _serviceAPR.GetAcademicPlanRecords(model);
        }

        public ResultService<AcademicPlanRecordElementPageViewModel> GetAcademicPlanRecordElements(AcademicPlanRecordElementGetBindingModel model)
        {
            return _serviceAPRE.GetAcademicPlanRecordElements(model);
        }

        public ResultService<AcademicPlanRecordElementViewModel> GetAcademicPlanRecordElement(AcademicPlanRecordElementGetBindingModel model)
        {
            return _serviceAPRE.GetAcademicPlanRecordElement(model);
        }


        public ResultService<StreamLessonRecordPageViewModel> GetStreamLessonRecords(StreamLessonRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception(string.Format("Нет доступа на чтение данных по {0}", _operationTitle));
                }

                int countPages = 0;
                var query = _context.StreamLessonRecords.Where(x => !x.IsDeleted).AsQueryable();

                if (model.SteamLessonId.HasValue)
                {
                    query = query.Where(x => x.StreamLessonId == model.SteamLessonId);
                }

                query = query.OrderBy(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.EducationDirection.Cipher);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query
                    .Include(x => x.AcademicPlanRecordElement)
                    .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline)
                    .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.EducationDirection)
                    .Include(x => x.AcademicPlanRecordElement.TimeNorm)
                    .Include(x => x.StreamLesson);

                var result = new StreamLessonRecordPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateStreamLessonRecordViewModel).ToList()
                };

                return ResultService<StreamLessonRecordPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<StreamLessonRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<StreamLessonRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<StreamLessonRecordViewModel> GetStreamLessonRecord(StreamLessonRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception(string.Format("Нет доступа на чтение данных по {0}", _operationTitle));
                }

                var entity = _context.StreamLessonRecords
                                .Include(x => x.AcademicPlanRecordElement)
                                .Include(x => x.AcademicPlanRecordElement.TimeNorm)
                                .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline)
                                .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.EducationDirection)
                                .Include(x => x.StreamLesson)
                                .FirstOrDefault(x => x.Id == model.Id && !x.IsDeleted);
                if (entity == null)
                {
                    return ResultService<StreamLessonRecordViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<StreamLessonRecordViewModel>.Success(ModelFactoryToViewModel.CreateStreamLessonRecordViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<StreamLessonRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<StreamLessonRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateStreamLessonRecord(StreamLessonRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception(string.Format("Нет доступа на изменение данных по {0}", _operationTitle));
                }

                var entity = ModelFacotryFromBindingModel.CreateStreamLessonRecord(model);

                _context.StreamLessonRecords.Add(entity);
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

        public ResultService UpdateStreamLessonRecord(StreamLessonRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception(string.Format("Нет доступа на изменение данных по {0}", _operationTitle));
                }

                var entity = _context.StreamLessonRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                entity = ModelFacotryFromBindingModel.CreateStreamLessonRecord(model, entity);

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

        public ResultService DeleteStreamLessonRecord(StreamLessonRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception(string.Format("Нет доступа на удаление данных по {0}", _operationTitle));
                }

                var entity = _context.StreamLessonRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
